import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';  
import * as XLSX from 'xlsx';
import { InventoryDetails } from '../_modals/InventoryDetails';
import { InventoryService } from '../_services/index';
import { UserDetails } from '../_modals/UserDetails';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {

  searchUserVal: string;
  arrayBuffer:any;
  file:File;
  inventoryDetailsWorkSheetArray = new Array();
  inventoryDetailsList: InventoryDetails[];
  invDetails: InventoryDetails;
  selectedSrNo: string;
  display: any;
  displayDataSaveModel: any;
  newRowInventoryDetails : InventoryDetails;
  invProducts: any = [];
  searchUserDetails: any = [];
  userDetailsList: UserDetails[];
  selectedUser: UserDetails;
  selectedRowSerialNo: any;
  alertMessage:string;
  loggedInuserDetails: UserDetails;
  loggedInUserInfo: any =[];
  isDetailsFetchedFromDB: boolean = false;
  isRowInEditMode: number = 0;

  constructor(private http: HttpClient, public invService:InventoryService) {
    this.inventoryDetailsList;
    this.invDetails;
    this.display = "none";
   }

  ngOnInit() {
  }

  changeListener(event): void {
    this.file = event.target.files[0];
  }

  Upload(): void {
    this.isDetailsFetchedFromDB = false;
    let fileReader = new FileReader();
    this.inventoryDetailsList = [];
      fileReader.onload = (e) => {
          this.arrayBuffer = fileReader.result;
          var data = new Uint8Array(this.arrayBuffer);
          var arr = new Array();
          for(var i = 0; i != data.length; ++i) {
            arr[i] = String.fromCharCode(data[i]);            
          } 
          var bstr = arr.join("");
          var workbook = XLSX.read(bstr, {type:"binary"});
          var first_sheet_name = workbook.SheetNames[1];
          var worksheet = workbook.Sheets[first_sheet_name];
          this.inventoryDetailsWorkSheetArray = XLSX.utils.sheet_to_json(worksheet,{raw:true});
          //console.log(this.inventoryDetailsWorkSheetArray);
          for(let inv in this.inventoryDetailsWorkSheetArray){
            if(Number(inv) > 0) {
              //console.log(this.inventoryDetailsList[inv]);
              this.invDetails = {
                Id : this.inventoryDetailsWorkSheetArray[inv]["Id"],
                SrNo: this.inventoryDetailsWorkSheetArray[inv]["Sr. No."],
                ModelName: this.inventoryDetailsWorkSheetArray[inv]["Model Name/Description"],
                SlNo: this.inventoryDetailsWorkSheetArray[inv]["Sl. No."],
                Quantity: this.inventoryDetailsWorkSheetArray[inv]["Quantity"],
                AssetTag: this.inventoryDetailsWorkSheetArray[inv]["Asset Tag"],
                WorkstationOrLab: this.inventoryDetailsWorkSheetArray[inv]["Location \r\n(Building-Tower-Floor-Wing etc.)"],
                RackNumber: this.inventoryDetailsWorkSheetArray[inv]["__EMPTY"],
                RailNumber: this.inventoryDetailsWorkSheetArray[inv]["__EMPTY_1"],
                UserID: "",
                DisplayName: this.inventoryDetailsWorkSheetArray[inv]["__EMPTY_2"],
                Location: this.inventoryDetailsWorkSheetArray[inv]["Location \r\n(Building-Tower-Floor-Wing etc.)_1"],
                AssetType: this.inventoryDetailsWorkSheetArray[inv]["Asset Type"],
                Project: this.inventoryDetailsWorkSheetArray[inv]["Project"],
                InvoiceNo: this.inventoryDetailsWorkSheetArray[inv]["Invoice No."],
                InvoiceValue: this.inventoryDetailsWorkSheetArray[inv]["Value As per Invoice"],
                PoNumber: this.inventoryDetailsWorkSheetArray[inv]["PO Number"],
                Date: this.inventoryDetailsWorkSheetArray[inv]["Date"],
                Status: ""
              };
               this.inventoryDetailsList.push(this.invDetails);
            }
          }
          //console.log(this.inventoryDetailsList);
      }
       fileReader.readAsArrayBuffer(this.file);
  }

  remove(id: any, invId: any): void {
    var res = confirm("Record with Id "+ id +" will get deleted permanently!")
    if(res) {
          this.inventoryDetailsList.splice(id,1);
          if(this.isDetailsFetchedFromDB) {
            this.invService.deleteInventory(invId);
          }
    }
  }

  openModal(srNo: string, modalId: string): void {
    this.selectedRowSerialNo = srNo;
     this.display = "block";
  }

  assignUser(): void {
    this.display = "none";
    for(var inv of this.inventoryDetailsList) {
      if(inv.SrNo == this.selectedRowSerialNo) {
        inv.UserID = this.selectedUser.AccountId;
        inv.DisplayName = this.selectedUser.DisplayName;
      }
    }

  }

  closeModal(): void {
    this.display = "none";
  }

  closeDataSaveModel(): void {
    this.displayDataSaveModel = "none";
    this.alertMessage = "";
  }

  searchUser(searchUserVal: string) {

    this.invService.searchUser(searchUserVal).subscribe((data: {}) => {
      this.searchUserDetails = data;
      this.userDetailsList = this.searchUserDetails;
    });
  }

  onSelectionChange(user: any) {
    this.selectedUser = user;
}

  AddRow(): void {
    if(this.inventoryDetailsList != null && this.inventoryDetailsList.length > 0) {
      this.newRowInventoryDetails = new InventoryDetails;
      let lastInventoryDetails = this.inventoryDetailsList[this.inventoryDetailsList.length - 1];
      console.log(lastInventoryDetails);
      let srNo = (lastInventoryDetails["SrNo"]) + 1;
      this.newRowInventoryDetails.SrNo = srNo;
      this.newRowInventoryDetails.AssetTag = lastInventoryDetails["AssetTag"];
      this.newRowInventoryDetails.AssetType = lastInventoryDetails["AssetType"];
      this.newRowInventoryDetails.Date = lastInventoryDetails["Date"];
      this.newRowInventoryDetails.DisplayName = lastInventoryDetails["DisplayName"];
      this.newRowInventoryDetails.InvoiceNo = lastInventoryDetails["InvoiceNo"];
      this.newRowInventoryDetails.Location = lastInventoryDetails["Location"];
      this.newRowInventoryDetails.ModelName = lastInventoryDetails["ModelName"];
      this.newRowInventoryDetails.PoNumber = lastInventoryDetails["PoNumber"];
      this.newRowInventoryDetails.Project = lastInventoryDetails["Project"];
      this.newRowInventoryDetails.Quantity = lastInventoryDetails["Quantity"];
      this.newRowInventoryDetails.RackNumber = lastInventoryDetails["RackNumber"];
      this.newRowInventoryDetails.RailNumber = lastInventoryDetails["RailNumber"];
      this.newRowInventoryDetails.SlNo = lastInventoryDetails["SlNo"];
      this.newRowInventoryDetails.UserID = lastInventoryDetails["UserID"];
      this.newRowInventoryDetails.InvoiceValue = lastInventoryDetails["InvoiceValue"];
      this.newRowInventoryDetails.WorkstationOrLab = lastInventoryDetails["WorkstationOrLab"];
    
      this.inventoryDetailsList.push(this.newRowInventoryDetails);
    }
  }

  GetAllDevices(): void {
    this.isDetailsFetchedFromDB = true;
    this.invProducts = [];
    this.invService.getAllInventory().subscribe((data: {}) => {
      this.invProducts = data;
      console.log(data);
      this.inventoryDetailsList = this.invProducts;
    });
  }

  GetUsersDevices(): void {
    this.isDetailsFetchedFromDB = true;
    this.invProducts = [];
    var userId = localStorage.getItem("UserZId");
    this.invService.getUsersInventory(userId).subscribe((data: {}) => {
      this.invProducts = data;
      this.inventoryDetailsList = this.invProducts;
    });
  }

  Edit(): void {
    this.isRowInEditMode += 1;
  }

  Update(): void {
    this.isRowInEditMode -= 1;
  }

  save() : void {  
    if(this.isRowInEditMode == 0)  {
        this.displayDataSaveModel = "block";
        var userId = localStorage.getItem("userZId");
        if(this.inventoryDetailsList != null && this.inventoryDetailsList.length > 0) {
        var data = this.invService.saveInventoryData(this.inventoryDetailsList, userId);
        this.inventoryDetailsList = null;
        this.alertMessage = "Data Saved Successfully";
      }
      else {
        this.alertMessage = "There is no Data to Save";
      }
    }
    else{
      alert("Row is in Edit mode, Please update the row and then Save.");
    }
  }

}
