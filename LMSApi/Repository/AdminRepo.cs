using LMSApi.Helpers;
using LMSApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace LMSApi.Repository
{
    public class AdminRepo
    {
        public List<EMPLOYEE_DETAILS> GetAllEmployeeDetails(string dbConn, string STR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@STR", SqlDbType.VarChar, 100) { Value = "EMPLOYEE_DETAILS" },


                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "GET_MASTER_DETAILS", parameters);
                List<EMPLOYEE_DETAILS> response = SqlHelper.CreateListFromTable<EMPLOYEE_DETAILS>(dataTable);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet Login(string dbConn, string EMPLOYEE_ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@EMPLOYEE_ID", SqlDbType.Float) { Value = EMPLOYEE_ID },


                };


                return SqlHelper.ExtecuteProcedureReturnDataSet(dbConn, "EMPLOYEE_LOGIN", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }
    }
}
