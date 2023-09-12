using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LMSApi.Helpers;
using LMSApi.IServices;
using LMSApi.Models;
using LMSApi.Response;
using LMSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using NPOI.SS.Formula.Functions;

namespace LMSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CourseController : ControllerBase
    {
        private ICourseService _icourseservice;
        private readonly IWebHostEnvironment _environment;
        public CourseController(ICourseService courseService, IWebHostEnvironment environment)
        {
            _icourseservice = courseService;
            _environment = environment;
        }

        //[HttpPost("InsertCourse")]

        //public ActionResult<Response<CommonResponse>> InsertCourse([FromForm] CourseInsert request, IFormFile file)
        //{
        //    var formFile = Request.Form.Files;

        //    string path = Path.Combine(_environment.ContentRootPath, "Uploads", "Thumbnail");
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    List<string> uploadedFiles = new List<string>();
        //    foreach (IFormFile postedFile in formFile)
        //    {
        //        string fileName = Path.GetFileName(1 + "_" + postedFile.FileName);
        //        using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, path, fileName), FileMode.Create))
        //        {
        //            postedFile.CopyTo(stream);
        //            uploadedFiles.Add(fileName);
        //        }
        //    }

        //    return Ok(_icourseservice.InsertCourse(request.Course, request.Module));
        //}


        [HttpGet("GetAllCourse")]
        public ActionResult<Response<List<COURSE>>> GetCourse(string CREATED_BY)
        {
            //test
            return Ok(JsonConvert.SerializeObject(_icourseservice.GetCourse(CREATED_BY)));
        }

        [HttpGet("GetCourseById")]
        public ActionResult<Response<COURSE>> GetCourseId(int COURSE_ID)
        {
            return Ok(JsonConvert.SerializeObject(_icourseservice.GetCourseId(COURSE_ID)));
        }
        [HttpGet("GetSearchCourse")]
        public ActionResult<Response<List<COURSE>>> GetSearch(string? COURSE_NAME, int? NO_OF_MODULES, string? CATEGORY, string? APPROVER, string? LEVEL_OF_COURSE, string? CREATED_BY)
        {
            return Ok(JsonConvert.SerializeObject(_icourseservice.GetSearch(COURSE_NAME, NO_OF_MODULES, CATEGORY, APPROVER, LEVEL_OF_COURSE, CREATED_BY)));
        }

        [HttpDelete("DeleteCourseById")]
        public ActionResult<Response<CommonResponse>> DeleteCourse(int COURSE_ID)
        {

            return Ok(JsonConvert.SerializeObject(_icourseservice.DeleteCourse(COURSE_ID)));
        }

        [HttpDelete("DeleteModuleById")]
        public ActionResult<Response<CommonResponse>> DeleteModuleById(int MODULE_ID)
        {

            return Ok(JsonConvert.SerializeObject(_icourseservice.DeleteModuleById(MODULE_ID)));
        }
        [HttpPost("UploadVideo")]
        public ActionResult<Response<string>> UploadCOURSEFiles(int MODULE_ID, IFormFile file)
        {
            var formFile = Request.Form.Files;

            string path = Path.Combine(_environment.ContentRootPath, "Uploads", "CourseFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in formFile)
            {
                string fileName = Path.GetFileName(MODULE_ID + "_" + postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
            }
            Response<string> response = new Response<string>();
            response.ResponseMessage = "Course Uploaded SuccessFully!";
            return response;
        }

        [HttpGet("GetUploadCourse")]
        public ActionResult<Response<List<ALL_FILES>>> Getcourses(int MODULE_ID)
        {
            string[] array1 = Directory.GetFiles("Uploads/CourseFiles/");

            List<ALL_FILES> imgFiles = new List<ALL_FILES>();
            Response<List<ALL_FILES>> response = new Response<List<ALL_FILES>>();

            // Get list of files.
            List<string> filesList = array1.ToList();

            foreach (var file in filesList)
            {
                if (file.Contains(MODULE_ID.ToString()))
                {
                    ALL_FILES img = new ALL_FILES();
                    long length = new System.IO.FileInfo(file).Length / 1024;
                    img.FILE_NAME = file.Split('/')[2];
                    img.FILE_SIZE = length.ToString() + "KB";
                    img.FILE_PATH = file;

                    imgFiles.Add(img);

                }
            }

            if (imgFiles.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = imgFiles;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }
            return response;
        }

        [HttpPost("UploadThumbnail")]
        public ActionResult<Response<string>> UploadThumbnail(int MODULE_ID, IFormFile file)
        {
            var formFile = Request.Form.Files;

            string path = Path.Combine(_environment.ContentRootPath, "Uploads", "Thumbnail");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in formFile)
            {
                string fileName = Path.GetFileName(MODULE_ID + "_" + postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
            }
            Response<string> response = new Response<string>();
            response.ResponseMessage = "Thumbnail Uploaded SuccessFully!";
            return response;
        }

        [HttpGet("GetUploadedCourseImgAndVideo")]
        public ActionResult<Response<List<COURSE_MODULE>>> GetUploadedCourseImgAndVideo(int MODULE_ID)

        {

            return Ok(JsonConvert.SerializeObject(_icourseservice.GetFiles(MODULE_ID)));
            ////string[] imgArray = Directory.GetFiles("Uploads/Thumbnail/");
            //string[] videoArray = Directory.GetFiles("Uploads/CourseFiles/");


            //List<ALL_FILES> imgFiles = new List<ALL_FILES>();//Thumbnail
            //List<ALL_FILES> videoFiles = new List<ALL_FILES>(); //Video

            //Response<List<ALL_FILES>> response = new Response<List<ALL_FILES>>();
            ////List<string> imgList = imgArray.ToList();
            //List<string> videosList = videoArray.ToList();



            ////foreach (var file in imgList)
            ////{
            ////    if (file.Contains(MODULE_ID.ToString()))
            ////    {
            ////        ALL_FILES img = new ALL_FILES();
            ////        long length = new System.IO.FileInfo(file).Length / 1024;
            ////        img.FILE_NAME = file.Split('/')[2];
            ////        img.FILE_SIZE = length.ToString() + "KB";
            ////        img.FILE_PATH = file;

            ////        imgFiles.Add(img);

            ////    }
            ////}
            //foreach (var file in videosList)
            //{
            //    if (file.Contains(MODULE_ID.ToString()))
            //    {
            //        ALL_FILES img = new ALL_FILES();
            //        long length = new System.IO.FileInfo(file).Length / 1024;
            //        img.FILE_NAME = file.Split('/')[2];
            //        img.FILE_SIZE = length.ToString() + "KB";
            //        //img.FILE_PATH = file;
            //        string baseUrl = "https://localhost:7148/"; // Replace 'port' with the actual port number
            //        /*string baseUrl = "https://etariff.jmbaxi.com/LMSAPI/";*/ // Replace 'port' with the actual port number
            //        string relativePath = file.Replace("\\", "/"); // Replace backslashes with forward slashes
            //        string fullPath = baseUrl + relativePath;
            //        img.FILE_PATH= fullPath;
            //        videoFiles.Add(img);

            //    }
            //}

            //if (videoFiles.Count > 0)
            //{
            //    response.Succeeded = true;
            //    response.ResponseCode = 200;
            //    response.ResponseMessage = "Success";
            //    response.Data = videoFiles;

            //    //response.Data1 = videoFiles;

            //}
            //else
            //{
            //    response.Succeeded = false;
            //    response.ResponseCode = 500;
            //    response.ResponseMessage = "No Data";
            //}
            
         

        }

        [HttpPost("insertCourse")]
        [RequestFormLimits(MultipartBodyLengthLimit = 300 * 1024 * 1024)]
        public ActionResult<Response<CommonResponse>> InsertCourses()
        {
            var formCollection = Request.Form;
            string payload = formCollection["payload"];
            List<int> moduleIds = new List<int>();
            Response<DataTable> response = new Response<DataTable>();
            var a = JsonConvert.DeserializeObject<Rootobject1>(payload)?.OPERATION; //to check if operation is only
                                                                                    //to add course or add both(course and module)

            var data = JsonConvert.DeserializeObject<RootObject<COURSE>>(payload);

            DataSet res = _icourseservice.InsertCourses(data);
            if (res.Tables.Contains("Table1"))
            {
                if (res.Tables["Table1"].Rows.Count > 0)
                {
                   
                   foreach (DataRow row in res.Tables["Table1"].Rows)
                   {
                       int moduleId = (int)row["MODULE_ID"];
                       moduleIds?.Add(moduleId);
                   }
                }
            }
            if ((moduleIds.Count>0 && a== "Insert"))
            {
                var formFile = Request.Form.Files;

                string path = Path.Combine(_environment.ContentRootPath, "Uploads", "CourseFiles");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();
                for (int i = 0; i < moduleIds.Count; i++)
                {
                    //data.VALUES[0].MODULES[i].MODULE_ID = moduleIds[i];


                    if (data.VALUES[0].MODULES[i].MODULE_ID == null || data.VALUES[0].MODULES[i].MODULE_ID == 0)
                    {
                        data.VALUES[0].MODULES[i].MODULE_ID = moduleIds[i];
                    }
                }
                foreach (var i in data.VALUES[0].MODULES)
                {
                    var file = i.FILE_DATA;
                    foreach (var f in file)
                    {
                        var d = f.name;
                        foreach (IFormFile postedFile in Request.Form.Files)
                        {
                            if (d == postedFile.FileName && f.module == i.MODULE_NUMBER)
                            {
                                string fileName = Path.GetFileName(i.MODULE_ID + "_" + postedFile.FileName);
                                string filePathToCheck = Path.Combine(path, fileName);
                                string fileExtension = Path.GetExtension(fileName).ToLower();

                                if (!System.IO.File.Exists(filePathToCheck))
                                {
                                    using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, path, fileName), FileMode.Create))
                                    {
                                        postedFile.CopyTo(stream);
                                        uploadedFiles.Add(fileName);
                                        if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                                        {
                                            //string filePath = Path.Combine(path, fileName);
                                            string filePath = Path.Combine("Uploads", "CourseFiles", fileName);


                                            int MODULE_ID = i.MODULE_ID;
                                            _icourseservice.InsertImagePath(filePath, MODULE_ID);
                                        }
                                        else
                                        {
                                            //string filePath = Path.Combine(path, fileName);
                                            string filePath = Path.Combine("Uploads", "CourseFiles", fileName);
                                            int MODULE_ID = i.MODULE_ID;
                                            _icourseservice.InsertVideoPath(filePath, MODULE_ID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //Response<string> response = new Response<string>();
                
                {
                    response.Succeeded = true;
                    response.ResponseCode = 200;
                    response.ResponseMessage = "Success";
                    response.Data = res.Tables["Table"];
                    response.Data1 = res.Tables["Table1"];

                };
                return Ok(JsonConvert.SerializeObject(response));

            }

            else if (a == "InsertOnlyCourse")
            {
                {
                    response.Succeeded = true;
                    response.ResponseCode = 200;
                    response.ResponseMessage = "Success";
                    response.Data = res.Tables["Table"];
                    response.Data1 = res.Tables["Table1"];

                };
                return Ok(JsonConvert.SerializeObject(response));
            }
            else
            {
                
                {
                    response.Succeeded = false;
                    response.ResponseCode = 500;
                    response.ResponseMessage = "Insertion Failed";

                };
                return BadRequest(response);

            }

            //return Ok(JsonConvert.SerializeObject((_icourseservice.InsertCourses(jsoOaylo))));
        }

        [HttpGet("getBusinessDetails")]
        public  ActionResult<Response<List<BUSINESS_DETAILS>>> GetBusinessDetails(string STR)
        {
            
            return Ok(JsonConvert.SerializeObject(_icourseservice.GetBusinessDetails(STR)));
        }

        [HttpPost("AssignCourse")]
        public ActionResult<Response<CommonResponse>> AssignCourse(RootObject<ASSIGN_COURSE> request)
        {
            return Ok(JsonConvert.SerializeObject((_icourseservice.AssignCourse(request))));
        }
        [HttpGet("GetCourseEmployeeDetails")]
        public ActionResult<Response<List<ASSIGN_COURSE>>> GetCourseEmployeeDetails(string? OPERATION, int? COURSE_EMPLOYEE_ID, string?  EMPLOYEE_NAME, string? COURSE_NAME, string ASSIGNED_BY, DateTime? START_TIME, DateTime? END_TIME,string? STATUS)
        {

            return Ok(JsonConvert.SerializeObject(_icourseservice.GetCourseEmployeeDetails( OPERATION,  COURSE_EMPLOYEE_ID, EMPLOYEE_NAME, COURSE_NAME, ASSIGNED_BY, START_TIME, END_TIME, STATUS)));
        }

        [HttpGet("getEmployeeDropDown")]
        public ActionResult<Response<List<EMPLOYEE_DETAILS>>> GetEmployeeDropDown(string STR)
        {

            return Ok(JsonConvert.SerializeObject(_icourseservice.GetEmployeeDropDown(STR)));
        }
        [HttpGet("getCourseDropDown")]
        public ActionResult <Response<DataTable>> GetCourseDropDown(string STR)
        {
            Response<DataTable> response = new Response<DataTable>();
            DataTable res = _icourseservice.GetCourseDropDown(STR);
            if (res != null)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = res;
            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }
            return Ok(JsonConvert.SerializeObject(response));
        }
        [HttpPost("deleteAssignedCourse")]
        public ActionResult<Response<CommonResponse>> DeleteAssignedCourse(RootObject<ASSIGN_COURSE> request)
        {

            return Ok(JsonConvert.SerializeObject(_icourseservice.DeleteAssignedCourse(request)));
        }
        //[HttpPost("inserCourse")]
        //public ActionResult<Response<CommonResponse>> insertCourse(COURSE request)
        //{
        //    return Ok(_icourseservice.insertCourse(request));
        //}

        //[HttpPost("insermodule")]
        //public ActionResult<Response<CommonResponse>> insertModule(COURSE_MODULE request)
        //{
        //    return Ok(_icourseservice.insertModule(request));
        //}

        //[HttpPost("updateCourse")]
        //public ActionResult<Response<CommonResponse>> updateCourse(COURSE request)
        //{
        //    return Ok(_icourseservice.updateCourse(request));
        //}

        //[HttpPost("Deletemodule")]
        //public ActionResult<Response<CommonResponse>> DeleteModule(int MODULE_ID))
        //{
        //    return Ok(_icourseservice.DeleteModule(request));
        //}
    }
}
