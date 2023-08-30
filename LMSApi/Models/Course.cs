using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LMSApi.Models
{
    public class COURSE
    {
        public int COURSE_ID { get; set; }
        public string COURSE_NAME { get; set; }
        public string COURSE_DESCRIPTION { get; set; }
        public int NO_OF_MODULES { get; set; }
        public string CATEGORY { get; set; }
        public string APPROVER { get; set; }
        public string LEVEL_OF_COURSE { get; set; }
        public string INSTRUCTOR_NAME { get; set; }
        //public string COURSE_OUTCOME { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }
        public List<MASTER_DETAILS> BUSINESS { get; set; } = new List<MASTER_DETAILS>();
        public List<COURSE_MODULE> MODULES { get; set; } = new List<COURSE_MODULE>();
        public List<VALUE> VALUES { get; set; } = new List<VALUE>();
       
        //public Formarrayquizoption[] formArrayQuizOption { get; set; }
    }

    

    public class COURSE_MODULE
    {
        public int MODULE_ID { get; set; }
        public int COURSE_ID { get; set; }
        public int MODULE_NUMBER { get; set; }
        public string MODULE_NAME { get; set; }
        public string MODULE_DESCRIPTION { get; set; }
        public string MODULE_DURATION { get; set; }
        public string THUMBNAIL_PATH { get; set; }
        public string VIDEO_PATH { get; set; }
        public bool STATUS { get; set; }
        public int SEQ_NO { get; set; }
        public List<FileData> FILE_DATA = new List<FileData>();
       //public IFormFile file { get; set; }
    }
    public class FileData {

        public string name { get; set; }
        public int module { get; set; }
    
    }
    public class MASTER_DETAILS
    {
        public int BUSINESS_ID { get; set; }
        public string BUSINESS_NAME { get; set; }
    }
   //public class FormData
   // {
        
   //     string payload { get; set; }
        
   //     IFormFileCollection? images { get; set; }

       
   //     IFormFileCollection? videos { get; set; }
   // }
    //public class IdeaDto{
    //    IFormFile file { get; set; }
    //    public string payload { get; set; }
    //    }

    //public class Arrfile {
    //    int index { get; set; }
    //    IFormFile? Ifile { get; set; }
    //}
    //public class CourseGetByID {
    //    public int COURSE_ID { get; set; }
    //}
}
