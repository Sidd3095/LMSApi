﻿using Microsoft.Extensions.Configuration;
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

                    if (data.Tables[2].Rows.Count != 0)
                    {
                        course.VALUES = CourseRepo.GetListFromDataSet<VALUE>(data.Tables[2]);
                    }
                }
                if (data.Tables.Contains("Table3"))

                {
                    if (data.Tables[3].Rows.Count != 0)
                    {
                        var quiz = CourseRepo.GetListFromDataSet<Formarrayquizoption>(data.Tables[3]);
                        //course.VALUES[0].formArrayQuizOption = quiz;

                        foreach (var question in course.VALUES)
                        {
                            question.formArrayQuizOption = quiz.Where(x => x.QUESTION_ID == question.QUESTION_ID).ToList();
                        }
                    }

                }
                if (data.Tables.Contains("Table4"))
                {

                    if (data.Tables[4].Rows.Count != 0)
                    {
                        course.BUSINESS = CourseRepo.GetListFromDataSet<BUSINESS_DETAILS>(data.Tables[4]);
                    }
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

        public Response<List<COURSE>> GetSearch(string? COURSE_NAME, int? NO_OF_MODULES, string ?CATEGORY, string?APPROVER 
            , string? LEVEL_OF_COURSE, string? CREATED_BY)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<COURSE>> response = new Response<List<COURSE>>();
            var data = DbClientFactory<CourseRepo>.Instance.GetSearch(dbConn, COURSE_NAME, NO_OF_MODULES, CATEGORY, APPROVER, LEVEL_OF_COURSE, CREATED_BY);

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

        public DataSet InsertCourses(RootObject<COURSE> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");
            DataTable moduleIds = new DataTable();
            DataTable courseIds = new DataTable();
            //int ModuleId=0;
            var data = DbClientFactory<CourseRepo>.Instance.InsertCourses(dbConn, request);
             Response<int> response = new Response<int>();
            if ((data != null) && (data.Tables[0].Rows.Count > 0))
            {
                if (data.Tables.Contains("Table1"))
                {
                    if (data.Tables["Table1"].Rows.Count > 0)
                    {
                        moduleIds = data.Tables["Table1"];
                        //foreach (DataRow row in data.Tables["Table1"].Rows)
                        //{
                        //    int moduleId = (int)row["MODULE_ID"];
                        //    moduleIds?.Add(moduleId);
                        //}
                    }
                }
                 if (data.Tables.Contains("Table"))
                {
                    if (data.Tables["Table"].Rows.Count > 0)
                    {

                        courseIds = data.Tables["Table"];
                        //foreach (DataRow row in data.Tables["Table"].Rows)
                        //{
                        //    int courseId = (int)row["COURSE_ID"];
                        //    courseIds.Add(courseId);
                        //}
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
            //if (moduleIds.Count>0)
            //{
            //    return moduleIds;
            //}
            //else
            //{
            //    return courseIds;
            //}

            return data;
           

        }
        public void InsertImagePath(string filePath, int MODULE_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<CourseRepo>.Instance.InsertImagePath(dbConn, filePath, MODULE_ID);
        }
        public void InsertVideoPath(string filePath, int MODULE_ID)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            DbClientFactory<CourseRepo>.Instance.InsertVideoPath(dbConn, filePath, MODULE_ID);
        }

        public Response<List<BUSINESS_DETAILS>> GetBusinessDetails(string STR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<BUSINESS_DETAILS>> response = new Response<List<BUSINESS_DETAILS>>();
            var data = DbClientFactory<CourseRepo>.Instance.GetBusinessDetails(dbConn, STR);

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

        public Response<List<COURSE_MODULE>> GetFiles(int moduleId)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<COURSE_MODULE>> response = new Response<List<COURSE_MODULE>>();
            var data = DbClientFactory<CourseRepo>.Instance.GetFile(dbConn, moduleId);

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

        public Response<CommonResponse> AssignCourse(RootObject<ASSIGN_COURSE> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");
            Response<CommonResponse> response = new Response<CommonResponse>();
            var res=DbClientFactory<CourseRepo>.Instance.AssignCourse(dbConn, request);

            if (res != null)
            {
                
                response.Succeeded = true;
                response.ResponseMessage = "Course assigned Successfully.";
                response.ResponseCode = 200;
                return response;

            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
                return response;
            }
           
        }
        public Response<List<ASSIGN_COURSE>> GetCourseEmployeeDetails(string? OPERATION, int? COURSE_EMPLOYEE_ID, string? EMPLOYEE_NAME, string? COURSE_NAME, string ASSIGNED_BY, DateTime? START_TIME, DateTime? END_TIME, string? STATUS)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<ASSIGN_COURSE>> response = new Response<List<ASSIGN_COURSE>>();
            var data = DbClientFactory<CourseRepo>.Instance.GetCourseEmployeeDetails(dbConn, OPERATION, COURSE_EMPLOYEE_ID,EMPLOYEE_NAME, COURSE_NAME, ASSIGNED_BY, START_TIME, END_TIME, STATUS);

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
        public Response<List<EMPLOYEE_DETAILS>> GetEmployeeDropDown(string STR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            Response<List<EMPLOYEE_DETAILS>> response = new Response<List<EMPLOYEE_DETAILS>>();
            var data = DbClientFactory<CourseRepo>.Instance.GetEmployeeDropDown(dbConn, STR);

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
        public DataTable GetCourseDropDown(string STR)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");
            // DataTable response = new  DataTable();
            //DataTable response = new DataTable();
            //Response DataTable response = new Response DataTable();
            var data = DbClientFactory<CourseRepo>.Instance.GetCourseDropDown(dbConn, STR);

            

            return data;
        }
        public Response<CommonResponse> DeleteAssignedCourse(RootObject<ASSIGN_COURSE> request)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");
            Response<CommonResponse> response = new Response<CommonResponse>();
            var res = DbClientFactory<CourseRepo>.Instance.AssignCourse(dbConn, request);

            if (res == "Record Deleted Successfully")
            {

                response.Succeeded = true;
                response.ResponseMessage = "Record Deleted Successfully";
                response.ResponseCode = 200;
                return response;

            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
                return response;
            }

        }
    }
   

}


