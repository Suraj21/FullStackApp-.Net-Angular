using SysOneInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SysOneInventoryAPI.Controllers
{
    public class UserController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Action Method to Get all the users List from Search Modal Pop up
        /// </summary>
        /// <param name="searchUser"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetUserDetails(string searchUser)
        {
            //Log.Info("GetUserList Method called start");
            List<UserModel> usersList = new List<UserModel>();
            try
            {
                usersList = Utility.Utility.SearchUser(searchUser);
                //Log.Info("GetUserList. No of Users Found: " + usersList.Count);
            }
            catch (Exception ex)
            {
                Log.Error("GetUserList Method called", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occured while calling api");
            }
            return Request.CreateResponse(HttpStatusCode.OK, usersList);
        }
    }
}
