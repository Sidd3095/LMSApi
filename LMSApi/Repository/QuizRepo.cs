using IdentityModel.Client;
using LMSApi.Helpers;
using LMSApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
namespace LMSApi.Repository
{
    public class QuizRepo
    {

        public string InsertQuiz(string connstring, Rootobject1 root)
        {
            try
            {
                for (int i = 0; i < root.VALUES.Count; i++)
                {
                    Rootobject1 obj = new Rootobject1();
                    obj.OPERATION = root.OPERATION;
                    obj.USER_ID = root.USER_ID;
                    obj.COURSE_ID = root.COURSE_ID;
                    obj.VALUES.Add(root.VALUES[i]);
                   
                    string payload = JsonConvert.SerializeObject(obj);
                    SqlParameter[] parameters = { new SqlParameter("@Json",payload) };

                    var ID =  SqlHelper.ExecuteProcedureReturnString(connstring, "JCRUD_QUIZ", parameters);

                }

                return "Inserted";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteSingleQuestion(string connstring, Rootobject1 root)
        {
            try
            {
                for (int i = 0; i < root.VALUES.Count; i++)
                {
                    Rootobject1 obj = new Rootobject1();
                    obj.OPERATION = root.OPERATION;
                    obj.USER_ID = root.USER_ID;
                    obj.COURSE_ID = root.COURSE_ID;
                    obj.VALUES.Add(root.VALUES[i]);

                    string payload = JsonConvert.SerializeObject(obj);
                    SqlParameter[] parameters = { new SqlParameter("@Json", payload) };

                    var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "JCRUD_QUIZ", parameters);
                   
                    }
                return "Question deleted Successfully !";
            }

            catch (Exception)
            {
                throw;
            }



        }
        
    }
}
