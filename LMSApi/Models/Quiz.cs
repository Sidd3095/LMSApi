namespace LMSApi.Models
{
    //public class QUIZ_QUESTION
    //{
    //    public int QUESTION_ID { get; set; }
    //    public int QUESTION_NUMBER { get; set; }
    //    public int COU
    //    RSE_ID { get; set; }
    //    public string? USER_ID { get; set; }
    //    public string? QUESTION { get; set; }
    //    public DateTime CREATED_DATE { get; set; }
    //    public string? CREATED_BY { get; set; }
    //    public List<QUIZ_QUESTION> QUESTIONS { get; set; } = new List<QUIZ_QUESTION>();
    //    //public List<QUIZ_QUESTION> VALUES { get; set; } = new List<QUIZ_QUESTION>();
    //    public List<QUIZ_OPTIONS> formArrayQuizOption { get; set; } = new List<QUIZ_OPTIONS>();

    //}
    //public class QUIZ_OPTIONS
    //{
    //    public int OPTION_ID { get; set; }
    //    public int QUESTION_ID { get; set; }
    //    public int OPTION_NUMBER { get; set; }

    //    public string? OPTIONS { get; set; }
    //    public bool IS_CORRECT { get; set; }


    //}


    public class Rootobject1
    {
        public string OPERATION { get; set; }
        public string USER_ID { get; set; }
        public int COURSE_ID { get; set; }
        public List<VALUE> VALUES { get; set; } = new List<VALUE>();
    }

    public class VALUE
    {
        public int QUESTION_ID { get; set; }
        public int QUESTION_NUMBER { get; set; }
        public string QUESTION { get; set; }
        //public DateTime ? CREATED_DATE { get; set; }
        //public string CREATED_BY { get; set; }
        public List<Formarrayquizoption> formArrayQuizOption { get; set; } = new List<Formarrayquizoption>();
    }

    public class Formarrayquizoption
    {
        public int OPTION_ID { get; set; }
        public int QUESTION_ID { get; set; }
        public string OPTIONS { get; set; }
        public bool IS_CORRECT { get; set; }
    }

}
