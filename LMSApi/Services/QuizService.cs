using LMSApi.Helpers;
using LMSApi.IServices;
using LMSApi.Models;
using LMSApi.Repository;
using LMSApi.Response;
using LMSApi.Utility;
using System.Data;
namespace LMSApi.Services
{
    public class QuizService: IQuizService
    {
        private readonly IConfiguration _config;
        public QuizService(IConfiguration config)
        {
            _config = config;
        }
        public Response<string> InsertQuiz(Rootobject1 root)
       {
           string dbConn = _config.GetConnectionString("ConnectionString");

           var ID = DbClientFactory<QuizRepo>.Instance.InsertQuiz(dbConn, root);

           Response<string> response = new Response<string>();
           response.Succeeded = true;
           response.ResponseMessage = "Quiz Saved Successfully !";
           response.ResponseCode = 200;
           response.Data = ID;
           return response;
       }

     

        public Response<string> InsertQuiz(string b)
        {
            throw new NotImplementedException();
        }

        public Response<string> DeleteSingleQuestion(Rootobject1 root)
        {
            string dbConn = _config.GetConnectionString("ConnectionString");

            var msg = DbClientFactory<QuizRepo>.Instance.DeleteSingleQuestion(dbConn, root);

            Response<string> response = new Response<string>();
            response.Succeeded = true;
            //response.ResponseMessage = "Question deleted Successfully !";
            response.ResponseCode = 200;
            response.ResponseMessage = msg;
            return response;
        }

    }
}
