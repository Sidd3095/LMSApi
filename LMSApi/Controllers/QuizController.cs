using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LMSApi.Helpers;
using LMSApi.IServices;
using LMSApi.Models;
using LMSApi.Response;
using LMSApi.Services;
using Org.BouncyCastle.Asn1.Ocsp;

namespace LMSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class QuizController : ControllerBase
    {
        private IQuizService _iquizservice;
        private readonly IWebHostEnvironment _environment;
        public QuizController(IQuizService quizService, IWebHostEnvironment environment)
        {
            _iquizservice = quizService;
            _environment = environment;
        }

        [HttpPost("InsertQuiz")]
       public ActionResult<Response<CommonResponse>> InsertQuiz(Rootobject1 root)
       {

            //RootObject<QUIZ_QUESTION> root = new RootObject<QUIZ_QUESTION>();
            //root.OPERATION = "Insert";
            //root.USER_ID = question.USER_ID;
            //root.COURSE_ID = question.COURSE_ID;
            //root.VALUES.Add(question);
            //string b = JsonConvert.SerializeObject(root);
            return Ok(_iquizservice.InsertQuiz(root));
        }
        [HttpPost("DeleteSingleQuestion")]
        public ActionResult<Response<CommonResponse>> DeleteSingleQuestion(Rootobject1 root) { 
            return Ok(_iquizservice.DeleteSingleQuestion(root)); 
        }
            
    }
}
