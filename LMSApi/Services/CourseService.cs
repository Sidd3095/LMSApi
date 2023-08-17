using Microsoft.Extensions.Configuration;
using LMSApi.Helpers;
using LMSApi.IServices;
using LMSApi.Models;
using LMSApi.Repository;
using LMSApi.Response;
using LMSApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.IdentityModel.Tokens;

namespace LMSApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly IConfiguration _config;
        public CourseService(IConfiguration config)
        {
            _config = config;
        }
        public Response<string> InsertCourse(COURSE request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            var ID = DbClientFactory<CourseRepo>.Instance.InsertCourse(dbConn, request);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            response.ResponseMessage = "Course Saved Successfully !";
            response.ResponseCode = 200;
            response.Data = ID;
            return response;
        }


        public Response<List<COURSE>> GetCourse(string CREATED_BY)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<COURSE>> response = new Response<List<COURSE>>();
            var data = DbClientFactory<CourseRepo>.Instance.GetCourse(dbConn, CREATED_BY);

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

        public Response<COURSE> GetCourseId(int COURSE_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<COURSE> response = new Response<COURSE>();
            if (COURSE_ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide COURSE_ID";
                return response;
            }
            var data = DbClientFactory<CourseRepo>.Instance.GetCourseId(dbConn, COURSE_ID);

            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                COURSE course = new COURSE();

                course = CourseRepo.GetSingleDataFromDataSet<COURSE>(data.Tables[0]);

                if (data.Tables.Contains("Table1"))
                {
                    course.MODULE = CourseRepo.GetListFromDataSet<COURSE_MODULE>(data.Tables[1]);
                }
                response.Data = course;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }


            return response;
        }
    
        public Response<List<COURSE>> GetSearch(string? COURSE_NAME, int? NO_OF_MODULES, string ?CATEGORY, string? SUB_CATEGORY, string? LEVEL_OF_COURSE, string? CREATED_BY)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<COURSE>> response = new Response<List<COURSE>>();
            var data = DbClientFactory<CourseRepo>.Instance.GetSearch(dbConn, COURSE_NAME, NO_OF_MODULES, CATEGORY, SUB_CATEGORY, LEVEL_OF_COURSE, CREATED_BY);

            if (data.Count > 0)
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

        public Response<CommonResponse> DeleteCourse(int COURSE_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (COURSE_ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide COURSE_ID ";
                return response;
            }

            DbClientFactory<CourseRepo>.Instance.DeleteCourse(dbConn, COURSE_ID);

            response.Succeeded = true;
            response.ResponseMessage = "course deleted Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public Response<CommonResponse> DeleteModuleById(int MODULE_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<CommonResponse> response = new Response<CommonResponse>();

            if (MODULE_ID == 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Please provide MODULE_ID ";
                return response;
            }

            DbClientFactory<CourseRepo>.Instance.DeleteModuleById(dbConn, MODULE_ID);

            response.Succeeded = true;
            response.ResponseMessage = "Module deleted Successfully";
            response.ResponseCode = 200;

            return response;
        }


    }
}


