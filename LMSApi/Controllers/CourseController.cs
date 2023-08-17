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

        [HttpPost("InsertCourse")]
        public ActionResult<Response<CommonResponse>> InsertCourse(COURSE request)
        {
            return Ok(_icourseservice.InsertCourse(request));
        }
        [HttpPost("InsertCourse1")]
        //public ActionResult<Response<CommonResponse>> InsertCourse1()
        //{


        //    //    var a = JsonConvert.DeserializeObject(jsonString);
        //    var formCollection = Request.Form;
        //    IFormFileCollection file = Request.Form.Files;
        //    // Accessing form valuess

          
        //    string payload = formCollection["payload"];
        //    var files = formCollection["images"];
        //    var videos = formCollection["videos"];
        //    var jsoOaylo = JsonConvert.DeserializeObject<COURSE>(payload);

        //    return Ok();

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
        public ActionResult<Response<List<COURSE>>> GetSearch(string? COURSE_NAME, int? NO_OF_MODULES, string? CATEGORY, string? SUB_CATEGORY, string? LEVEL_OF_COURSE, string? CREATED_BY)
        {
            return Ok(JsonConvert.SerializeObject(_icourseservice.GetSearch(COURSE_NAME, NO_OF_MODULES, CATEGORY, SUB_CATEGORY, LEVEL_OF_COURSE, CREATED_BY)));
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
        public ActionResult<Response<List<ALL_FILES>>> Getcourses(int MODULE_ID)
        {
            string[] imgArray = Directory.GetFiles("Uploads/Thumbnail/");
            string[] videoArray = Directory.GetFiles("Uploads/CourseFiles/");


            List<ALL_FILES> imgFiles = new List<ALL_FILES>();//Thumbnail
            List<ALL_FILES> videoFiles = new List<ALL_FILES>(); //Video

            Response<List<ALL_FILES>> response = new Response<List<ALL_FILES>>();
            List<string> imgList = imgArray.ToList();
            List<string> videosList = videoArray.ToList();



            foreach (var file in imgList)
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
            foreach (var file in videosList)
            {
                if (file.Contains(MODULE_ID.ToString()))
                {
                    ALL_FILES img = new ALL_FILES();
                    long length = new System.IO.FileInfo(file).Length / 1024;
                    img.FILE_NAME = file.Split('/')[2];
                    img.FILE_SIZE = length.ToString() + "KB";
                    img.FILE_PATH = file;

                    videoFiles.Add(img);

                }
            }

            if (imgFiles.Count > 0)
            {
                response.Succeeded = true;
                response.ResponseCode = 200;
                response.ResponseMessage = "Success";
                response.Data = imgFiles;

                response.Data1 = videoFiles;

            }
            else
            {
                response.Succeeded = false;
                response.ResponseCode = 500;
                response.ResponseMessage = "No Data";
            }
            return response;
        }

        //[HttpGet("GetThumbnail")]
        //public ActionResult<Response<List<ALL_FILES>>> GetThumbnail(int COURSE_ID)
        //{
        //    string[] array1 = Directory.GetFiles("Uploads/Thumbnail/");

        //    List<ALL_FILES> imgFiles = new List<ALL_FILES>();
        //    Response<List<ALL_FILES>> response = new Response<List<ALL_FILES>>();

        //    // Get list of files.
        //    List<string> filesList = array1.ToList();

        //    foreach (var file in filesList)
        //    {
        //        if (file.Contains(COURSE_ID.ToString()))
        //        {
        //            ALL_FILES img = new ALL_FILES();
        //            long length = new System.IO.FileInfo(file).Length / 1024;
        //            img.FILE_NAME = file.Split('/')[2];
        //            img.FILE_SIZE = length.ToString() + "KB";
        //            img.FILE_PATH = file;

        //            imgFiles.Add(img);

        //        }
        //    }

        //    if (imgFiles.Count > 0)
        //    {
        //        response.Succeeded = true;
        //        response.ResponseCode = 200;
        //        response.ResponseMessage = "Success";
        //        response.Data = imgFiles;
        //    }
        //    else
        //    {
        //        response.Succeeded = false;
        //        response.ResponseCode = 500;
        //        response.ResponseMessage = "No Data";
        //    }
        //    return response;
        //}

    }
}
