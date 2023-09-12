using LMSApi.Helpers;
using LMSApi.Models;
using System.Data;

namespace LMSApi.IServices
{
    public interface IAdminService
    {
        Response<List<EMPLOYEE_DETAILS>> GetAllEmployeeDetails(string STR);

        Response<EMPLOYEE_DETAILS> Login(string EMPLOYEE_ID);
    }
}
