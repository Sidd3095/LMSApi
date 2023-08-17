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
        public ActionResult<Response<List<COURSE>>> GetSearch(string COURSE_NAME, int NO_OF_MODULES, string CATEGORY, string SUB_CATEGORY, string LEVEL_OF_COURSE, string CREATED_BY)
        {
            return Ok(JsonConvert.SerializeObject(_icourseservice.GetSearch(COURSE_NAME, NO_OF_MODULES, CATEGORY, SUB_CATEGORY, LEVEL_OF_COURSE, CREATED_BY)));
        }

        [HttpDelete("DeleteCourseById")]
        public ActionResult<Response<CommonResponse>> DeleteCourse(int COURSE_ID)
        {

            return Ok(JsonConvert.SerializeObject(_icourseservice.DeleteCourse(COURSE_ID)));
        }

        [HttpPost("UploadCourse")]
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


        [HttpGet("GetThumbnail")]
        public ActionResult<Response<List<ALL_FILES>>> GetThumbnail(int MODULE_ID)
        {
            string[] array1 = Directory.GetFiles("Uploads/Thumbnail/");

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

        [HttpPost("insertCourse")]
        public ActionResult<Response<CommonResponse>> InsertCourses()
        {
             var formCollection = Request.Form;
            string payload = formCollection["payload"];
            var data = JsonConvert.DeserializeObject<RootObject<COURSE>>(payload);

           List<int> res = (_icourseservice.InsertCourses(data));
        
                if (res != null)
            {
                var formFile = Request.Form.Files;

                string path = Path.Combine(_environment.ContentRootPath, "Uploads", "CourseFiles");
             
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();
                foreach(var i in res)
                {
                    foreach (IFormFile postedFile in formFile)
                    {
                        string fileName = Path.GetFileName(i + "_" + postedFile.FileName);
                        string filePathToCheck = Path.Combine(path, fileName);

                        if (!System.IO.File.Exists(filePathToCheck))
                        {
                            using (FileStream stream = new FileStream(Path.Combine(_environment.ContentRootPath, path, fileName), FileMode.Create))
                            {
                                postedFile.CopyTo(stream);
                                uploadedFiles.Add(fileName);
                            }
                        }

                    }
                }
              
                Response<string> response = new Response<string>();
                {
                    response.Succeeded = true;
                    response.ResponseCode = 200;
                    response.ResponseMessage = "Success";

                };
                return Ok(res);

            }
            else
            {
                Response<string> response = new Response<string>();
                {
                    response.Succeeded = false;
                    response.ResponseCode = 500;
                    response.ResponseMessage = "Insertion Failed";

                };
                return BadRequest(response);

            }

            //return Ok(JsonConvert.SerializeObject((_icourseservice.InsertCourses(jsoOaylo))));
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
