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
using NPOI.POIFS.Crypt.Dsig;
using System.Data;
using NPOI.SS.Formula.Functions;
using System.Reflection;

namespace LMSApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly IConfiguration _config;
        public CourseService(IConfiguration config)
        {
            _config = config;
        }
        //public Response<string> InsertCourse(COURSE request, COURSE_MODULE module)
        //{
        //    string dbConn = _config.GetConnectionString("ConnectionString");

        //    var ID = DbClientFactory<CourseRepo>.Instance.InsertCourse(dbConn, request, module);

        //    Response<string> response = new Response<string>();
        //    response.Succeeded = true;
        //    response.ResponseMessage = "Course Saved Successfully !";
        //    response.ResponseCode = 200;
        //    response.Data = ID;
        //    return response;
        //}


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
                    course.MODULES = CourseRepo.GetListFromDataSet<COURSE_MODULE>(data.Tables[1]);
                }
                if (data.Tables.Contains("Table2"))
                {
                    course.VALUES = CourseRepo.GetListFromDataSet<VALUE>(data.Tables[2]);
                }
                if (data.Tables.Contains("Table3"))
                {
                    var quiz = CourseRepo.GetListFromDataSet<Formarrayquizoption>(data.Tables[3]);
                    course.VALUES[0].formArrayQuizOption = quiz;
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

        public List<int> InsertCourses(RootObject<COURSE> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");
            List<int> moduleIds = new List<int>();
            List<int> courseIds = new List<int>();
            //int ModuleId=0;
            var data = DbClientFactory<CourseRepo>.Instance.InsertCourses(dbConn, request);
             Response<int> response = new Response<int>();
            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                if (data.Tables.Contains("Table1"))
                {
                    if (data.Tables["Table1"].Rows.Count > 0)
                    {
                        foreach (DataRow row in data.Tables["Table1"].Rows)
                        {
                            int moduleId = (int)row["MODULE_ID"];
                            moduleIds.Add(moduleId);
                        }
                    }
                }
                 if (data.Tables.Contains("Table"))
                {
                    if (data.Tables["Table"].Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Tables["Table"].Rows)
                        {
                            int courseId = (int)row["COURSE_ID"];
                            courseIds.Add(courseId);
                        }
                    }
                }

                //ModuleId = (int)data.Tables["Table1"].Rows[0]["MODULE_ID"];
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
            }
           else
           {
               response.Succeeded = false;
               response.ResponseCode = 500;
               response.ResponseMessage = "No Data";
           }
            if (moduleIds.Count>0)
            {
                return moduleIds;
            }
            else
            {
                return courseIds;
            }
            


        }

        //public Response<string> insertCourse(COURSE request)
        //{
        //    string dbConn = _config.GetConnectionString("ConnectionString");

        //    var ID = DbClientFactory<CourseRepo>.Instance.insertCourse(dbConn, request);

        //    Response<string> response = new Response<string>();
        //    response.Succeeded = true;
        //    response.ResponseMessage = "Course Saved Successfully !";
        //    response.ResponseCode = 200;
        //    response.Data = ID;
        //    return response;
        //}
        //public Response<string> updateCourse(COURSE request)
        //{
        //    string dbConn = _config.GetConnectionString("ConnectionString");

        //    Response<string> response = new Response<string>();
        //    DbClientFactory<CourseRepo>.Instance.updateCourse(dbConn, request);

        //    response.Succeeded = true;
        //    response.ResponseMessage =" updated Successfully.";
        //    response.ResponseCode = 200;

        //    return response;
        //}

        //public Response<string> insertModule(COURSE_MODULE request)
        //{
        //    string dbConn = _config.GetConnectionString("ConnectionString");

        //    var ID = DbClientFactory<CourseRepo>.Instance.insertModule(dbConn, request);

        //    Response<string> response = new Response<string>();
        //    response.Succeeded = true;
        //    response.ResponseMessage = "Module Saved Successfully !";
        //    response.ResponseCode = 200;
        //    response.Data = ID;
        //    return response;
        //}

        //public Response<CommonResponse> DeleteModule(int MODULE_ID)
        //{
        //    string dbConn = _config.GetConnectionString("ConnectionString");

        //    Response<CommonResponse> response = new Response<CommonResponse>();

        //    if (MODULE_ID == 0)
        //    {
        //        response.ResponseCode = 500;
        //        response.ResponseMessage = "Please provide MODULE_ID ";
        //        return response;
        //    }

        //    DbClientFactory<CourseRepo>.Instance.DeleteModule(dbConn, MODULE_ID);

        //    response.Succeeded = true;
        //    response.ResponseMessage = "module deleted Successfully.";
        //    response.ResponseCode = 200;

        //    return response;
        //}
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


