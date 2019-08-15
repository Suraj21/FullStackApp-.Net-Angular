using SysOneInventoryAPI.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SysOneInventoryAPI.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region CONSTANTS
        private const string SIEMENS_COM = "siemens.com";
        private const string MAIL_FILTER = "(&(objectClass=user)(objectcategory=person)(mail={0}*))";
        private const string USERID_FILTER = "(&(objectClass=user)(objectcategory=person)(sAMAccountName={0}*))";
        private const string MAILPROP_NAME = "mail";
        private const string ZIDPROP_NAME = "sAMAccountName";
        private const string FULL_NAME = "cn";
        private const char ONESPACE_CHAR = ' ';
        private const string ONESPACE_STRING = " ";
        #endregion

        /// <summary>
        /// Method to get the Logged In User Details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        public HttpResponseMessage GetLoggedInUserDetails(string userId)
        {
            Log.Info("GetLoggedInUserDetails Method called start");
            UserModel userModel = new UserModel();

            try
            {
                userModel = Utility.Utility.SearchUser(userId).FirstOrDefault();
                Log.Info("GetLoggedInUserDetails.Users Found: " + userModel.EmailId);
            }
            catch (Exception ex)
            {
                Log.Error("Outer - GetLoggedInUserDetails Method called", ex);
                throw;
            }
            Log.Info("GetLoggedInUserDetails Method called end");
            return Request.CreateResponse(HttpStatusCode.OK, userModel);
        }

    }
}
