using LMSApi.Helpers;
using LMSApi.Models;
using LMSApi.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace LMSApi.IServices
{
     public interface ICourseService
    {
        //Response<string> InsertCourse(COURSE request,COURSE_MODULE module);
        Response<List<COURSE>> GetCourse(string CREATED_BY);
        Response<COURSE> GetCourseId(int COURSE_ID);
        Response<List<COURSE>> GetSearch(string? COURSE_NAME, int? NO_OF_MODULES, string? CATEGORY, string? APPROVER, string? LEVEL_OF_COURSE, string? CREATED_BY);
        Response<CommonResponse> DeleteCourse(int COURSE_ID);
       List<int> InsertCourses(RootObject<COURSE> request);
        //Response<string> insertCourse(COURSE request);
        //Response<string> updateCourse(COURSE request);
        //Response<string> insertModule(COURSE_MODULE request);
        //Response<CommonResponse> DeleteModule(int MODULE_ID);
        
              Response<CommonResponse> DeleteModuleById(int COURSE_ID);
        void InsertImagePath(string filePath, int MODULE_ID);
        void InsertVideoPath(string filePath, int MODULE_ID);
        Response<List<MASTER_DETAILS>> GetMasterDetails(string STR);
    }
}
