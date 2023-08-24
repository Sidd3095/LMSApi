using LMSApi.Helpers;
using LMSApi.Models;
using LMSApi.Response;

namespace LMSApi.IServices
{
    public interface IQuizService
    {
        //Response<string> InsertQuiz(QUIZ_QUESTION question);
        Response<string> InsertQuiz(Rootobject1 root);
    }
}
