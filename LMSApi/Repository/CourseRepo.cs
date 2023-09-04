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
    public class CourseRepo
    {

        //public string InsertCourse(string connstring, COURSE course, COURSE_MODULE module)
        //{
        //    try
        //    {

        //        SqlParameter[] parameters =
        //       {
        //           new SqlParameter("@OPERATION",    SqlDbType.VarChar,50) { Value = "INSERT_COURSE" },
        //           new SqlParameter("@COURSE_NAME",   SqlDbType.VarChar,100) { Value = course.COURSE_NAME},
        //           new SqlParameter("@COURSE_ID", SqlDbType.Int) {Value = course.COURSE_ID},
        //           new SqlParameter("@COURSE_DESCRIPTION",   SqlDbType.VarChar, 100) { Value = course.COURSE_DESCRIPTION},
        //           new SqlParameter("@NO_OF_MODULES", SqlDbType.Int) { Value = course.NO_OF_MODULES },
        //           new SqlParameter("@CATEGORY",      SqlDbType.VarChar, 100) { Value = course.CATEGORY },
        //           new SqlParameter("@SUB_CATEGORY",    SqlDbType.VarChar, 100) { Value = course.SUB_CATEGORY },
        //           new SqlParameter("@LEVEL_OF_COURSE", SqlDbType.NVarChar, 100) { Value = course.LEVEL_OF_COURSE },
        //           new SqlParameter("@INSTRUCTOR_NAME", SqlDbType.VarChar, 100) { Value = course.INSTRUCTOR_NAME },
        //           new SqlParameter("@COURSE_OUTCOME", SqlDbType.VarChar, 100) { Value = course.COURSE_OUTCOME },
        //           new SqlParameter("@CREATED_BY", SqlDbType.VarChar,100) {Value = course.CREATED_BY },
        //          // new SqlParameter("@CREATED_DATE", SqlDbType.DateTime) {Value = course.CREATED_DATE },
        //           new SqlParameter("@UPDATED_BY", SqlDbType.VarChar,100) {Value = course.UPDATED_BY },
        //           //new SqlParameter("@UPDATED_DATE", SqlDbType.DateTime) {Value = course.UPDATED_DATE },
        //           new SqlParameter("@STATUS", SqlDbType.Bit) {Value = course.STATUS },

        //         };
        //        var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COURSE", parameters);

        //        string[] columns = new string[9];
        //        columns[0] = "MODULE_ID";
        //        columns[1] = "COURSE_ID";
        //        columns[2] = "MODULE_NUMBER";
        //        columns[3] = "MODULE_NAME";
        //        columns[4] = "MODULE_DESCRIPTION";
        //        columns[5] = "MODULE_DURATION";
        //        columns[6] = "THUMBNAIL_PATH";
        //        columns[7] = "VIDEO_PATH";
        //        columns[8] = "STATUS";

        //        if (ID != "NULL")
        //        {
        //            DataTable tbl = new DataTable();
        //            tbl.Columns.Add(new DataColumn("MODULE_ID", typeof(int)));
        //            tbl.Columns.Add(new DataColumn("COURSE_ID", typeof(int)));
        //            tbl.Columns.Add(new DataColumn("MODULE_NUMBER", typeof(int)));
        //            tbl.Columns.Add(new DataColumn("MODULE_NAME", typeof(string)));
        //            tbl.Columns.Add(new DataColumn("MODULE_DESCRIPTION", typeof(string)));
        //            tbl.Columns.Add(new DataColumn("MODULE_DURATION", typeof(string)));
        //            tbl.Columns.Add(new DataColumn("THUMBNAIL_PATH", typeof(string)));
        //            tbl.Columns.Add(new DataColumn("VIDEO_PATH", typeof(string)));
        //            tbl.Columns.Add(new DataColumn("STATUS", typeof(bool)));



        //            foreach (COURSE_MODULE module in course.MODULES)
        //            {
        //                DataRow dr = tbl.NewRow();

        //                dr["MODULE_ID"] = i.MODULE_ID;
        //                dr["COURSE_ID"] = Convert.ToInt32(ID);
        //                dr["MODULE_NUMBER"] = i.MODULE_NUMBER;
        //                dr["MODULE_NAME"] = i.MODULE_NAME;
        //                dr["MODULE_DESCRIPTION"] = i.MODULE_DESCRIPTION;
        //                dr["MODULE_DURATION"] = i.MODULE_DURATION;
        //                dr["THUMBNAIL_PATH"] = i.THUMBNAIL_PATH;
        //                dr["VIDEO_PATH"] = i.VIDEO_PATH;
        //                dr["STATUS"] = i.STATUS;


        //                tbl.Rows.Add(dr);
        //            }

        //            SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_MODULE_MASTER", columns);


        //        }

        //        else
        //        {
        //            SqlHelper.UpdateCourseModule<COURSE_MODULE>(course.MODULES, "TB_MODULE_MASTER", connstring, columns);
        //        }
        //        return ID;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public List<COURSE> GetCourse(string dbConn, string CREATED_BY)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GetGridData" },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 100) { Value = CREATED_BY }

                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "GET_COURSE_DETAILS", parameters);
                List<COURSE> response = SqlHelper.CreateListFromTable<COURSE>(dataTable);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetCourseId(string dbConn, int COURSE_ID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GetById" },
                  new SqlParameter("@COURSE_ID", SqlDbType.Int) { Value = COURSE_ID }

                };

                //DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_CRUD_COURSE", parameters);
                //List<COURSE> response = SqlHelper.CreateListFromTable<COURSE>(dataTable);

                return SqlHelper.ExtecuteProcedureReturnDataSet(dbConn, "GET_COURSE_DETAILS", parameters);

            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<COURSE> GetSearch(string dbConn, string? COURSE_NAME, int? NO_OF_MODULES, string? CATEGORY, string? APPROVER, string? LEVEL_OF_COURSE, string? CREATED_BY)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "Search" },
                  new SqlParameter("@COURSE_NAME", SqlDbType.VarChar,100){ Value = COURSE_NAME },
                  new SqlParameter("@NO_OF_MODULES", SqlDbType.Int) { Value = NO_OF_MODULES },
                  new SqlParameter("@CATEGORY", SqlDbType.VarChar, 100){Value = CATEGORY },
                  new SqlParameter("@APPROVER", SqlDbType.VarChar, 100) { Value = APPROVER },
                  new SqlParameter("@LEVEL_OF_COURSE", SqlDbType.VarChar, 100) { Value = LEVEL_OF_COURSE },
                  new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 100) { Value = CREATED_BY }



                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "GET_COURSE_DETAILS", parameters);
                List<COURSE> response = SqlHelper.CreateListFromTable<COURSE>(dataTable);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
       

        public void DeleteCourse(string dbConn, int COURSE_ID)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE" },
               new SqlParameter("@COURSE_ID", SqlDbType.Int) { Value = COURSE_ID }
            };

            SqlHelper.ExecuteProcedureReturnString(dbConn, "SP_CRUD_COURSE", parameters);
        }
        public void DeleteModuleById(string dbConn, int MODULE_ID)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE_MODULE_BY_ID" },
               new SqlParameter("@MODULE_ID", SqlDbType.Int) { Value = MODULE_ID }
            };

            SqlHelper.ExecuteProcedureReturnString(dbConn, "SP_CRUD_COURSE", parameters);
        }
        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }

        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }
        public DataSet InsertCourses(string connstring, RootObject<COURSE> course)
        {
            try
            {
                string payload = JsonConvert.SerializeObject(course);
                SqlParameter[] parameters =
                 {
                   new SqlParameter("@Json",payload)

                 };

                return SqlHelper.ExtecuteProcedureReturnDataSet (connstring, "JCRUD_COURSE", parameters);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertImagePath(string connstring, string filepath, int MODULE_ID)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_IMAGE" },
               new SqlParameter("@MODULE_ID", SqlDbType.Int) { Value = MODULE_ID },
              new SqlParameter("@THUMBNAIL_PATH", SqlDbType.VarChar, 200) { Value = filepath },
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COURSE", parameters);
        }
        public void InsertVideoPath(string connstring, string filepath, int MODULE_ID)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "INSERT_VIDEO" },
              new SqlParameter("@MODULE_ID", SqlDbType.Int) { Value = MODULE_ID },
              new SqlParameter("@VIDEO_PATH", SqlDbType.VarChar, 200) { Value = filepath },
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COURSE", parameters);
        }

        public List<BUSINESS_DETAILS> GetBusinessDetails(string dbConn, string STR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@STR", SqlDbType.VarChar, 100) { Value = "BUSINESS_DETAILS" },


                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "GET_MASTER_DETAILS", parameters);
                List<BUSINESS_DETAILS> response = SqlHelper.CreateListFromTable<BUSINESS_DETAILS>(dataTable);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<COURSE_MODULE> GetFile(string dbConn, int moduleID)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GetFilesPath" },
                  new SqlParameter("@MODULE_ID", SqlDbType.VarChar,100){ Value = moduleID },




                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "GET_COURSE_DETAILS", parameters);
                List<COURSE_MODULE> response = SqlHelper.CreateListFromTable<COURSE_MODULE>(dataTable);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string AssignCourse(string connstring, RootObject<ASSIGN_COURSE> assigncourse)
        {
            try
            {


                string payload = JsonConvert.SerializeObject(assigncourse);
                SqlParameter[] parameters =
                 {
                   new SqlParameter("@Json",payload)


                 };
                var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "JCRUD_ASSIGN_COURSE", parameters);

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ASSIGN_COURSE> GetCourseEmployeeDetails(string dbConn, string? OPERATION, int? COURSE_EMPLOYEE_ID, string? EMPLOYEE_NAME, string? COURSE_NAME, string ASSIGNED_BY, DateTime? START_TIME, DateTime? END_TIME, string? STATUS)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 100) { Value = "Search_GetGridData" },
                  new SqlParameter("@COURSE_EMPLOYEE_ID", SqlDbType.Int) { Value = COURSE_EMPLOYEE_ID },
                   new SqlParameter("@EMPLOYEE_NAME", SqlDbType.VarChar, 100) { Value = EMPLOYEE_NAME },
                  new SqlParameter("@COURSE_NAME", SqlDbType.VarChar, 100) { Value = COURSE_NAME },
                  new SqlParameter("@ASSIGNED_BY", SqlDbType.VarChar, 100) { Value = ASSIGNED_BY },
                  new SqlParameter("@START_TIME", SqlDbType.DateTime) { Value = START_TIME },
                   new SqlParameter("@END_TIME", SqlDbType.DateTime) { Value =END_TIME },


                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "GET_COURSE_EMPLOYEE_DETAILS", parameters);
                List<ASSIGN_COURSE> response = SqlHelper.CreateListFromTable<ASSIGN_COURSE>(dataTable);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<EMPLOYEE_DETAILS> GetEmployeeDropDown(string dbConn, string STR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@STR", SqlDbType.VarChar, 100) { Value = "EMPLOYEE_DETAILS" },


                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "GET_MASTER_DETAILS", parameters);
                List<EMPLOYEE_DETAILS> response = SqlHelper.CreateListFromTable<EMPLOYEE_DETAILS>(dataTable);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetCourseDropDown(string dbConn, string STR)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@STR", SqlDbType.VarChar, 100) { Value = "GET_COURSES_DROPDOWN" },


                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "GET_MASTER_DETAILS", parameters);
                DataTable response = (dataTable);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string DeleteAssignedCourse(string connstring, RootObject<ASSIGN_COURSE> assigncourse)
        {
            try
            {


                string payload = JsonConvert.SerializeObject(assigncourse);
                SqlParameter[] parameters =
                 {
                   new SqlParameter("@Json",payload)


                 };
                var str = SqlHelper.ExecuteProcedureReturnString(connstring, "JCRUD_ASSIGN_COURSE", parameters);

                return str;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public string insertCourse(string connstring, COURSE course)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters =
        //    {
        //       new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "INSERT_COURSES" },
        //          new SqlParameter("@COURSE_NAME",   SqlDbType.VarChar,100) { Value = course.COURSE_NAME},
        //           new SqlParameter("@COURSE_ID", SqlDbType.Int) {Value = course.COURSE_ID},
        //           new SqlParameter("@COURSE_DESCRIPTION",   SqlDbType.VarChar, 100) { Value = course.COURSE_DESCRIPTION},
        //           new SqlParameter("@NO_OF_MODULES", SqlDbType.Int) { Value = course.NO_OF_MODULES },
        //           new SqlParameter("@CATEGORY",      SqlDbType.VarChar, 100) { Value = course.CATEGORY },
        //           new SqlParameter("@APPROVER",    SqlDbType.VarChar, 100) { Value = course.APPROVER },
        //           new SqlParameter("@LEVEL_OF_COURSE", SqlDbType.NVarChar, 100) { Value = course.LEVEL_OF_COURSE },
        //           new SqlParameter("@INSTRUCTOR_NAME", SqlDbType.VarChar, 100) { Value = course.INSTRUCTOR_NAME },
        //           new SqlParameter("@COURSE_OUTCOME", SqlDbType.VarChar, 100) { Value = course.COURSE_OUTCOME },
        //           new SqlParameter("@CREATED_BY", SqlDbType.VarChar,100) {Value = course.CREATED_BY },
        //          // new SqlParameter("@CREATED_DATE", SqlDbType.DateTime) {Value = course.CREATED_DATE },
        //           new SqlParameter("@UPDATED_BY", SqlDbType.VarChar,100) {Value = course.UPDATED_BY },
        //           //new SqlParameter("@UPDATED_DATE", SqlDbType.DateTime) {Value = course.UPDATED_DATE },
        //           new SqlParameter("@STATUS", SqlDbType.Bit) {Value = course.STATUS },
        //    };
        //        var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COURSE", parameters);
        //        return ID;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public string updateCourse(string connstring, COURSE course)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters =
        //    {
        //           new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "UPDATE_COURSES" },
        //          new SqlParameter("@COURSE_NAME",   SqlDbType.VarChar,100) { Value = course.COURSE_NAME},
        //           new SqlParameter("@COURSE_ID", SqlDbType.Int) {Value = course.COURSE_ID},
        //           new SqlParameter("@COURSE_DESCRIPTION",   SqlDbType.VarChar, 100) { Value = course.COURSE_DESCRIPTION},
        //           new SqlParameter("@NO_OF_MODULES", SqlDbType.Int) { Value = course.NO_OF_MODULES },
        //           new SqlParameter("@CATEGORY",      SqlDbType.VarChar, 100) { Value = course.CATEGORY },
        //           new SqlParameter("@APPROVER",    SqlDbType.VarChar, 100) { Value = course.APPROVER },
        //           new SqlParameter("@LEVEL_OF_COURSE", SqlDbType.NVarChar, 100) { Value = course.LEVEL_OF_COURSE },
        //           new SqlParameter("@INSTRUCTOR_NAME", SqlDbType.VarChar, 100) { Value = course.INSTRUCTOR_NAME },
        //           new SqlParameter("@COURSE_OUTCOME", SqlDbType.VarChar, 100) { Value = course.COURSE_OUTCOME },
        //           new SqlParameter("@CREATED_BY", SqlDbType.VarChar,100) {Value = course.CREATED_BY },
        //          // new SqlParameter("@CREATED_DATE", SqlDbType.DateTime) {Value = course.CREATED_DATE },
        //           new SqlParameter("@UPDATED_BY", SqlDbType.VarChar,100) {Value = course.UPDATED_BY },
        //           //new SqlParameter("@UPDATED_DATE", SqlDbType.DateTime) {Value = course.UPDATED_DATE },
        //           new SqlParameter("@STATUS", SqlDbType.Bit) {Value = course.STATUS },

        //    };
        //        var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COURSE", parameters);
        //        return ID;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        //public string insertModule(string connstring, COURSE_MODULE module)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters =
        //    {
        //   new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "Insert_module" },
        //       new SqlParameter("@COURSE_ID", SqlDbType.Int) {Value = module.COURSE_ID},
        //      new SqlParameter("@MODULE_NUMBER",   SqlDbType.Int) { Value = module.MODULE_NUMBER},
        //       new SqlParameter("@MODULE_NAME",   SqlDbType.VarChar, 100) { Value = module.MODULE_NAME},
        //       new SqlParameter("@MODULE_DESCRIPTION", SqlDbType.VarChar, 200) { Value = module.MODULE_DESCRIPTION },
        //       new SqlParameter("@MODULE_DURATION",      SqlDbType.VarChar, 200) { Value = module.MODULE_DURATION },
        //       new SqlParameter("@STATUS", SqlDbType.Bit) {Value = module.STATUS },
        //};
        //        var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COURSE", parameters);

        //        return ID;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void DeleteModule(string dbConn, int MODULE_ID)
        //{
        //    SqlParameter[] parameters =
        //    {
        //       new SqlParameter("@OPERATION", SqlDbType.VarChar, 255) { Value = "DELETE" },
        //       new SqlParameter("@MODULE_ID", SqlDbType.Int) { Value = MODULE_ID }
        //    };

        //    SqlHelper.ExecuteProcedureReturnString(dbConn, "SP_CRUD_COURSE", parameters);
        //}


    }
}

