using LMSApi.Helpers;
using LMSApi.Models;
using LMSApi.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LMSApi.IServices
{
     public interface ICourseService
    {
        Response<string> InsertCourse(COURSE request);
        Response<List<COURSE>> GetCourse(string CREATED_BY);
        Response<COURSE> GetCourseId(int COURSE_ID);
        Response<List<COURSE>> GetSearch(string? COURSE_NAME, int? NO_OF_MODULES, string? CATEGORY, string? SUB_CATEGORY, string? LEVEL_OF_COURSE, string? CREATED_BY);
        Response<CommonResponse> DeleteCourse(int COURSE_ID);
        
              Response<CommonResponse> DeleteModuleById(int COURSE_ID);
    }
}
