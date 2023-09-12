using LMSApi.Helpers;
using LMSApi.IServices;
using LMSApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _iadminservice;
        private readonly IWebHostEnvironment _environment;
        public AdminController(IAdminService adminService, IWebHostEnvironment environment)
        {
            _iadminservice = adminService;
            _environment = environment;
        }
        [HttpGet("getAllEmployeeDetails")]
        public ActionResult<Response<List<EMPLOYEE_DETAILS>>> GetAllEmployeeDetails(string STR)
        {

            return Ok(JsonConvert.SerializeObject(_iadminservice.GetAllEmployeeDetails(STR)));
        }
        [HttpGet("login")]

        public ActionResult<Response<List<EMPLOYEE_DETAILS>>> Login(string EMPLOYEE_ID)
        {

            return Ok(JsonConvert.SerializeObject(_iadminservice.Login(EMPLOYEE_ID)));
        }
    }
}
