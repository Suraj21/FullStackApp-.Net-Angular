using SysOneInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Web.Hosting;

namespace SysOneInventoryAPI.Utility
{
    public static class Utility
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region CONSTANTS
        private const string SIEMENS_COM = "siemens.com";
        private const string MAIL_FILTER = "(&(objectClass=user)(objectcategory=person)(mail={0}*))";
        private const string USERID_FILTER = "(&(objectClass=user)(objectcategory=person)(sAMAccountName={0}*))";
        private const string MAILPROP_NAME = "mail";
        private const string ZIDPROP_NAME = "sAMAccountName";
        private const string FULL_NAME = "cn";
        private const string DISPLAY_NAME = "displayname";
        private const string DEPARTMENT = "department";
        private const char ONESPACE_CHAR = ' ';
        private const string ONESPACE_STRING = " ";
        #endregion

        /// <summary>
        /// Method to search the user in Siemens LDAP/Directory
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static List<UserModel> SearchUser(string searchText)
        {
            //Log.Info("Class: Utility -> Method: SearchUser -> SearchText : " + searchText);
            List<UserModel> userModels = new List<UserModel>();

            try
            {
                if (searchText.Contains(SIEMENS_COM) || (searchText.ToLower().Contains("z00") && searchText.Length == 8))
                {
                    return SearchUserInDirectory(searchText);
                }
                else
                {
                    return SearchUserInLdap(searchText.ToLower());
                }
            }
            catch (Exception ex)
            {
                Log.Error("Utility -> SearchUser -> Exception Occured : " + ex);
                return userModels;
            }
        }

        /// <summary>
        /// Method to search the User in the siemens directory
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static List<UserModel> SearchUserInDirectory(string searchText)
        {
            //Log.Info("Utility -> SearchUserInDirectory -> Method Called");

            List<UserModel> userModelList = new List<UserModel>(); ;
            UserModel userModel = null;
            DirectorySearcher dirSearcher = new DirectorySearcher();

            try
            {
                if (searchText.Contains(SIEMENS_COM))
                {
                    dirSearcher.Filter = string.Format(MAIL_FILTER, searchText);
                }
                else
                {
                    dirSearcher.Filter = string.Format(USERID_FILTER, searchText);
                }

                SearchResult srEmail = dirSearcher.FindOne();
                ResultPropertyValueCollection emailColl = srEmail.Properties[MAILPROP_NAME];
                ResultPropertyValueCollection displayName = srEmail.Properties[DISPLAY_NAME];
                ResultPropertyValueCollection department = srEmail.Properties[DEPARTMENT];
                ResultPropertyValueCollection zidColl = srEmail.Properties[ZIDPROP_NAME];
                ResultPropertyValueCollection cn = srEmail.Properties[FULL_NAME];
                string[] names = cn[0].ToString().Split(ONESPACE_CHAR);
                string lastName = names[0].Trim();
                string firstName = names[1].Trim();
                string fullName = firstName + " " + lastName;
                userModel = new UserModel
                {
                    AccountId = zidColl[0].ToString(),
                    DisplayName = displayName[0].ToString(),
                    OrganisationCode_BU = department[0].ToString(),
                    EmailId = emailColl[0].ToString()
                };

                userModelList.Add(userModel);
                return userModelList;
            }
            catch (Exception ex)
            {
                Log.Error("Utility -> SearchUserInDirectory -> Exception Occured : " + ex);
                return userModelList;
            }
        }


        /// <summary>
        /// Method to search the user in the LDAP
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static List<UserModel> SearchUserInLdap(string searchText)
        {
            Log.Info("Class: Utility -> Method: SearchUserInLdap -> SearchText : " + searchText);
            List<UserModel> userList = new List<UserModel>();
            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    using (HostingEnvironment.Impersonate())
                    {
                        //Create a shortcut to the appropriate Windows domain
                        PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, "129.103.98.215");

                        //Create a "user object" in the context
                        UserPrincipal user = new UserPrincipal(domainContext)
                        {
                            //Specify the search parameters
                            GivenName = searchText
                        };

                        //Create the searcher
                        PrincipalSearcher principalSearcher = new PrincipalSearcher();
                        principalSearcher.QueryFilter = user;

                        //Perform the search
                        PrincipalSearchResult<Principal> principalSearchResults = principalSearcher.FindAll();
                        foreach (var item in principalSearchResults)
                        {
                            UserModel users = new UserModel { AccountId = item.SamAccountName, DisplayName = item.DisplayName };
                            userList.Add(users);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Class : Utility -> Action Method : SearchUserInLdap(). Exception Occured: Message", ex);
                userList = null;
            }
            Log.Info("Class: Utility -> Method: SearchUserInLdap -> No of Uses Found : " + userList.Count);
            return userList;
        }

    }
}