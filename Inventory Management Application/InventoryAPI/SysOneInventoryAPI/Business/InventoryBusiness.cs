using SysOneInventoryAPI.DAL;
using SysOneInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SysOneInventoryAPI.Business
{
    public class InventoryBusiness
    {
        InventoryDAL inventoryDAL = new InventoryDAL();
        /// <summary>
        /// Method to Insert and update the Inventory Details
        /// </summary>
        /// <param name="inventoryViewModel"></param>
        /// <returns></returns>
        public bool SaveInventoryDetails(InventoryModel inventoryViewModel,string userName)
        {
            bool status =  inventoryDAL.SaveInventoryDetails(inventoryViewModel, userName);

            return status;
        }

        /// <summary>
        /// Method to Get the User specific Inventory Details
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<InventoryModel> GetInventoryDetails(string userName)
        {
            List<InventoryModel> inventoryViewModelList = new List<InventoryModel>();

            DataSet dataSet =  inventoryDAL.GetInventoryDetails(userName);
            var dataTable = dataSet.Tables[0];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                InventoryModel inventoryViewModel = new InventoryModel
                {
                    SrNo = i + 1,
                    Id = Convert.ToInt32(dataTable.Rows[i]["Id"]),
                    ModelName = Convert.ToString(dataTable.Rows[i]["ModelName"]),
                    Quantity = Convert.ToString(dataTable.Rows[i]["Quantity"]),
                    SlNo = Convert.ToString(dataTable.Rows[i]["SlNo"]),
                    AssetTag = Convert.ToString(dataTable.Rows[i]["AssetTag"]),
                    WorkstationOrLab = Convert.ToString(dataTable.Rows[i]["WorkstationOrLab"]),
                    RackNumber = Convert.ToString(dataTable.Rows[i]["RackNumber"]),
                    RailNumber = Convert.ToString(dataTable.Rows[i]["RailNumber"]),
                    UserID = Convert.ToString(dataTable.Rows[i]["UserID"]),
                    DisplayName = Convert.ToString(dataTable.Rows[i]["DisplayName"]),
                    Location = Convert.ToString(dataTable.Rows[i]["Location"]),
                    AssetType = Convert.ToString(dataTable.Rows[i]["AssetType"]),
                    Project = Convert.ToString(dataTable.Rows[i]["Project"]),
                    InvoiceNo = Convert.ToString(dataTable.Rows[i]["InvoiceNo"]),
                    InvoiceValue = Convert.ToString(dataTable.Rows[i]["InvoiceValue"]),
                    PoNumber = Convert.ToString(dataTable.Rows[i]["PoNumber"]),
                    Date = Convert.ToString(dataTable.Rows[i]["Date"]),
                    Status = Convert.ToString(dataTable.Rows[i]["Status"])
                };
                inventoryViewModelList.Add(inventoryViewModel);
            }

            return inventoryViewModelList;
        }

        /// <summary>
        /// Method to Get All the Inventory Details
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<InventoryModel> GetAllInventoryDetails()
        {
            List<InventoryModel> inventoryViewModelList = new List<InventoryModel>();

            DataSet dataSet = inventoryDAL.GetAllInventoryDetails();
            var dataTable = dataSet.Tables[0];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                InventoryModel inventoryViewModel = new InventoryModel
                {
                    SrNo = i + 1,
                    Id = Convert.ToInt32(dataTable.Rows[i]["Id"]),
                    ModelName = Convert.ToString(dataTable.Rows[i]["ModelName"]),
                    Quantity = Convert.ToString(dataTable.Rows[i]["Quantity"]),
                    SlNo = Convert.ToString(dataTable.Rows[i]["SlNo"]),
                    AssetTag = Convert.ToString(dataTable.Rows[i]["AssetTag"]),
                    WorkstationOrLab = Convert.ToString(dataTable.Rows[i]["WorkstationOrLab"]),
                    RackNumber = Convert.ToString(dataTable.Rows[i]["RackNumber"]),
                    RailNumber = Convert.ToString(dataTable.Rows[i]["RailNumber"]),
                    UserID = Convert.ToString(dataTable.Rows[i]["UserID"]),
                    DisplayName = Convert.ToString(dataTable.Rows[i]["DisplayName"]),
                    Location = Convert.ToString(dataTable.Rows[i]["Location"]),
                    AssetType = Convert.ToString(dataTable.Rows[i]["AssetType"]),
                    Project = Convert.ToString(dataTable.Rows[i]["Project"]),
                    InvoiceNo = Convert.ToString(dataTable.Rows[i]["InvoiceNo"]),
                    InvoiceValue = Convert.ToString(dataTable.Rows[i]["InvoiceValue"]),
                    PoNumber = Convert.ToString(dataTable.Rows[i]["PoNumber"]),
                    Date = Convert.ToString(dataTable.Rows[i]["Date"]),
                    Status = Convert.ToString(dataTable.Rows[i]["Status"])
                };
                inventoryViewModelList.Add(inventoryViewModel);
            }

            return inventoryViewModelList;
        }

        /// <summary>
        /// Method to Delete the Inventory Details
        /// </summary>
        /// <param name="inventoryViewModel"></param>
        /// <returns></returns>
        public void DeleteInventory(string invId)
        {
            inventoryDAL.DeleteInventory(invId);
        }

    }
}