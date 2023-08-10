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
        public string SUB_CATEGORY { get; set; }
        public string LEVEL_OF_COURSE { get; set; }
        public string INSTRUCTOR_NAME { get; set; }
        public string COURSE_OUTCOME { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }
        public List<COURSE_MODULE> MODULE { get; set; } = new List<COURSE_MODULE>();
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
    }

    //public class CourseGetByID {
    //    public int COURSE_ID { get; set; }
    //}
}
