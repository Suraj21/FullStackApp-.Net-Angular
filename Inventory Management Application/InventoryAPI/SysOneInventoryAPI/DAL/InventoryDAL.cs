using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SysOneInventoryAPI.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace SysOneInventoryAPI.DAL
{
    public class InventoryDAL
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Variable  
        /// <summary>  
        /// Specify the Database variable  
        /// </summary>  
        Database objDB;
        /// <summary>  
        /// Specify the static variable  
        /// </summary>  
        static string ConnectionString;
        #endregion

        /// <summary>
        /// Constructor to initialize the connection string
        /// </summary>
        public InventoryDAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
        }

        /// <summary>  
        /// This method is used to get the User Specific Inventory Details
        /// </summary>  
        /// <returns></returns>  
        public DataSet GetInventoryDetails(string userName)
        {
            try
            {
                objDB = new SqlDatabase(ConnectionString);
                using (DbCommand objcmd = objDB.GetStoredProcCommand("USP_GetUserSpecificInventoryDetails"))
                {
                    objDB.AddInParameter(objcmd, "@UserID", DbType.String, userName);
                     return objDB.ExecuteDataSet(objcmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// This method is used to get the All Users Inventory Details
        /// </summary>  
        /// <returns></returns>  
        public DataSet GetAllInventoryDetails()
        {
            try
            {
                objDB = new SqlDatabase(ConnectionString);
                using (DbCommand objcmd = objDB.GetStoredProcCommand("USP_GetALLInventoryDetails"))
                {
                    return objDB.ExecuteDataSet(objcmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>  
        /// This method is used to Save the Inventory Details to Database
        /// </summary>  
        /// <param name="collegeDetails"></param>  
        /// <returns></returns>  
        public bool SaveInventoryDetails(InventoryModel inventoryViewModel, string userName)
        {
            bool result = false;
            try
            {
                objDB = new SqlDatabase(ConnectionString);
                using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_InsertUpdateInventoryDetails"))
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, inventoryViewModel.Id);
                    objDB.AddInParameter(objCMD, "@ModelName", DbType.String, inventoryViewModel.ModelName ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@Quantity", DbType.String, inventoryViewModel.Quantity ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@SlNo", DbType.String, inventoryViewModel.SlNo ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@AssetTag", DbType.String, inventoryViewModel.AssetTag ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@WorkstationOrLab", DbType.String, inventoryViewModel.WorkstationOrLab ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@RackNumber", DbType.String, inventoryViewModel.RackNumber ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@RailNumber", DbType.String, inventoryViewModel.RailNumber ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@DisplayName", DbType.String, inventoryViewModel.DisplayName ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@UserID", DbType.String, inventoryViewModel.UserID ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@Location", DbType.String, inventoryViewModel.Location ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@AssetType", DbType.String, inventoryViewModel.AssetType ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@Project", DbType.String, inventoryViewModel.Project ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@InvoiceNo", DbType.String, inventoryViewModel.InvoiceNo ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@InvoiceValue", DbType.String, inventoryViewModel.InvoiceValue ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@PoNumber", DbType.String, inventoryViewModel.PoNumber ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@Date", DbType.String, inventoryViewModel.Date ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@Status", DbType.String, inventoryViewModel.Status ?? string.Empty);
                    objDB.AddInParameter(objCMD, "@CreatedDate", DbType.String, DateTime.Now.ToShortDateString());
                    objDB.AddInParameter(objCMD, "@CreatedBy", DbType.String, userName);
                    objDB.AddInParameter(objCMD, "@ModifiedDate", DbType.String, DateTime.Now.ToShortDateString());
                    objDB.AddInParameter(objCMD, "@ModifiedBy", DbType.String, userName);
                    objDB.AddInParameter(objCMD, "@Result", DbType.Int32, 0);

                    var data = objDB.ExecuteNonQuery(objCMD);
                    var res = objDB.GetParameterValue(objCMD, "@Result");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Class : InventoryDAL -> Action Method : SaveInventoryDetails(). Exception Occured", ex);

                throw;
            }
            return result;
        }

        /// <summary>  
        /// This method checks whether the user is admin or not
        /// </summary>  
        /// <returns></returns>  
        public DataSet IsUserAdmin(string userName)
        {
            try
            {
                objDB = new SqlDatabase(ConnectionString);
                using (DbCommand objcmd = objDB.GetStoredProcCommand("USP_GetAdminDetails"))
                {
                    objDB.AddInParameter(objcmd, "@UserID", DbType.String, userName);
                    return objDB.ExecuteDataSet(objcmd);   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to delte the inventory based on the AssetTag value
        /// </summary>
        /// <param name="assetTag"></param>
        /// <returns></returns>
        public void DeleteInventory(string invId)
        {
            try
            {
                Log.Info("Class : InventoryDAL -> Action Method : DeleteInventory(). Exception Occured" + invId);
                objDB = new SqlDatabase(ConnectionString);
                using (DbCommand objcmd = objDB.GetStoredProcCommand("USP_DeleteInventoryDetails"))
                {
                    objDB.AddInParameter(objcmd, "@Id", DbType.String, invId.ToString());
                    var res = objDB.ExecuteDataSet(objcmd);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Class : InventoryDAL -> Action Method : DeleteInventory(). Exception Occured", ex);
                throw ex;
            }
        }
    }
}