using LMSApi.Helpers;
using LMSApi.IServices;
using LMSApi.Models;
using LMSApi.Repository;
using LMSApi.Utility;

namespace LMSApi.Services
{
    public class AdminService:IAdminService
    {
        private readonly IConfiguration _config;
        public AdminService(IConfiguration config)
        {
            _config = config;
        }
        public Response<List<EMPLOYEE_DETAILS>> GetAllEmployeeDetails(string STR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<EMPLOYEE_DETAILS>> response = new Response<List<EMPLOYEE_DETAILS>>();
            var data = DbClientFactory<AdminRepo>.Instance.GetAllEmployeeDetails(dbConn, STR);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = data;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
        public Response<EMPLOYEE_DETAILS> Login(string EMPLOYEE_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");


            Response<EMPLOYEE_DETAILS> response = new Response<EMPLOYEE_DETAILS>();
            var data = DbClientFactory<AdminRepo>.Instance.Login(dbConn, EMPLOYEE_ID);

            if (data != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                EMPLOYEE_DETAILS employee = new EMPLOYEE_DETAILS();

                employee = CourseRepo.GetSingleDataFromDataSet<EMPLOYEE_DETAILS>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    employee.ROLE = AdminRepo.GetListFromDataSet<ROLE>(data.Tables[1]);
                }
                response.Data = employee;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }

            return response;
        }
    }
}
