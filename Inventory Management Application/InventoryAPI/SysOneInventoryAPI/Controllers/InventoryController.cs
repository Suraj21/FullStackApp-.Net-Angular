using SysOneInventoryAPI.Business;
using SysOneInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SysOneInventoryAPI.Controllers
{
    //[EnableCors(origins:"*", headers:"*", methods:"*")]
    public class InventoryController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        InventoryBusiness inventoryBusiness = new InventoryBusiness();

        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetInventory()
        {
            Log.Info("GetInventory Method called start");
            List<InventoryModel> inventoryViewModelList = new List<InventoryModel>();
            try
            {
                Log.Info("GetInventory Method called inside try");
                inventoryViewModelList = inventoryBusiness.GetAllInventoryDetails();
            }
            catch (Exception ex)
            {
                Log.Error("GetInventory Method called", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occured while calling api");
            }
            return Request.CreateResponse(HttpStatusCode.OK, inventoryViewModelList);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUsersInventory(string userId)
        {
            Log.Info("GetUsersInventory Method called start");
            List<InventoryModel> inventoryViewModelList = new List<InventoryModel>();
            try
            {
                Log.Info("GetUsersInventory Method called inside try");
                inventoryViewModelList = inventoryBusiness.GetInventoryDetails(userId);
            }
            catch (Exception ex)
            {
                Log.Error("GetUsersInventory Method called", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occured while calling api");
            }
            return Request.CreateResponse(HttpStatusCode.OK, inventoryViewModelList);
        }

        /// <summary>
        /// Action Method to Get all the users List from Search Modal Pop up
        /// </summary>
        /// <param name="searchUser"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetUserList(string searchUser)
        {
            Log.Info("GetUserList Method called start");
            List<UserModel> usersList = new List<UserModel>();
            try
            {
                usersList = Utility.Utility.SearchUser(searchUser);
                Log.Info("GetUserList. No of Users Found: " + usersList.Count);
            }
            catch (Exception ex)
            {
                Log.Error("GetUserList Method called", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occured while calling api");
            }
            return Request.CreateResponse(HttpStatusCode.OK, usersList);
        }

        /// <summary>
        /// Action Method to save the Save Inventory Details
        /// </summary>
        /// <param name="inventoryList"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public HttpResponseMessage SaveInventory(string userName, [FromBody]List<InventoryModel> inventoryList)
        {
            Log.Info("SaveInventory(). Parameter value- inventoryList count: " + inventoryList.Count);

            try
            {
                if (inventoryList.Count > 0)
                {
                    foreach (InventoryModel item in inventoryList)
                    {
                        inventoryBusiness.SaveInventoryDetails(item, userName);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("SaveInventory Method called", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occured while calling api");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Records Saved Successfully");
        }

        /// <summary>
        /// Method to get the user Specific Inventory Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetInventoryDetails(string userId)
        {
            Log.Info("GetInventoryDetails. Parameter value- " + userId);
            List<InventoryModel> inventoryViewModelList = new List<InventoryModel>();

            try
            {
                inventoryViewModelList = inventoryBusiness.GetInventoryDetails(userId);
            }
            catch (Exception ex)
            {
                Log.Error("GetInventoryDetails Method called", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occured while calling api");

            }
            return Request.CreateResponse(HttpStatusCode.OK, inventoryViewModelList);
        }

        /// <summary>
        /// Method to get the user Specific Inventory Details
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public HttpResponseMessage DeleteInventory(string invId)
        {
            Log.Info("DeleteInventory. Parameter value (assetTag)- " + invId);
            List<InventoryModel> inventoryViewModelList = new List<InventoryModel>();

            try
            {
                inventoryBusiness.DeleteInventory(invId);
            }
            catch (Exception ex)
            {
                Log.Error("DeleteInventory Method called", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occured while calling api");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
