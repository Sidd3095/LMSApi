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

        public string InsertCourse(string connstring, COURSE course)
        {
            try
            {

                  SqlParameter[] parameters =
                 {
                   new SqlParameter("@OPERATION",    SqlDbType.VarChar,50) { Value = "INSERT_COURSE" },
                   new SqlParameter("@COURSE_NAME",   SqlDbType.VarChar,100) { Value = course.COURSE_NAME},
                   new SqlParameter("@COURSE_DESCRIPTION",   SqlDbType.VarChar, 100) { Value = course.COURSE_DESCRIPTION},
                   new SqlParameter("@NO_OF_MODULES", SqlDbType.Int) { Value = course.NO_OF_MODULES },
                   new SqlParameter("@CATEGORY",      SqlDbType.VarChar, 100) { Value = course.CATEGORY },
                   new SqlParameter("@SUB_CATEGORY",    SqlDbType.VarChar, 100) { Value = course.SUB_CATEGORY },
                   new SqlParameter("@LEVEL_OF_COURSE", SqlDbType.NVarChar, 100) { Value = course.LEVEL_OF_COURSE },
                   new SqlParameter("@INSTRUCTOR_NAME", SqlDbType.VarChar, 100) { Value = course.INSTRUCTOR_NAME },
                   new SqlParameter("@COURSE_OUTCOME", SqlDbType.VarChar, 100) { Value = course.COURSE_OUTCOME },
                   new SqlParameter("@CREATED_BY", SqlDbType.VarChar,100) {Value = course.CREATED_BY },
                  // new SqlParameter("@CREATED_DATE", SqlDbType.DateTime) {Value = course.CREATED_DATE },
                   new SqlParameter("@UPDATED_BY", SqlDbType.VarChar,100) {Value = course.UPDATED_BY },
                   //new SqlParameter("@UPDATED_DATE", SqlDbType.DateTime) {Value = course.UPDATED_DATE },
                   new SqlParameter("@STATUS", SqlDbType.Bit) {Value = course.STATUS },

                 };
                var ID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_COURSE", parameters);

                string[] columns = new string[9];
                columns[0] = "MODULE_ID";
                columns[1] = "COURSE_ID";
                columns[2] = "MODULE_NUMBER";
                columns[3] = "MODULE_NAME";
                columns[4] = "MODULE_DESCRIPTION";
                columns[5] = "MODULE_DURATION";
                columns[6] = "THUMBNAIL_PATH";
                columns[7] = "VIDEO_PATH";
                columns[8] = "STATUS";

                if (ID != "NULL")
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add(new DataColumn("MODULE_ID", typeof(int)));
                    tbl.Columns.Add(new DataColumn("COURSE_ID", typeof(int)));
                    tbl.Columns.Add(new DataColumn("MODULE_NUMBER", typeof(int)));
                    tbl.Columns.Add(new DataColumn("MODULE_NAME", typeof(string)));
                    tbl.Columns.Add(new DataColumn("MODULE_DESCRIPTION", typeof(string)));
                    tbl.Columns.Add(new DataColumn("MODULE_DURATION", typeof(string)));
                    tbl.Columns.Add(new DataColumn("THUMBNAIL_PATH", typeof(string)));
                    tbl.Columns.Add(new DataColumn("VIDEO_PATH", typeof(string)));
                    tbl.Columns.Add(new DataColumn("STATUS", typeof(bool)));



                    foreach (var i in course.MODULE)
                    {
                        DataRow dr = tbl.NewRow();

                        dr["MODULE_ID"] = i.MODULE_ID;
                        dr["COURSE_ID"] = Convert.ToInt32(ID);
                        dr["MODULE_NUMBER"] = i.MODULE_NUMBER;
                        dr["MODULE_NAME"] = i.MODULE_NAME;
                        dr["MODULE_DESCRIPTION"] = i.MODULE_DESCRIPTION;
                        dr["MODULE_DURATION"] = i.MODULE_DURATION;
                        dr["THUMBNAIL_PATH"] = i.THUMBNAIL_PATH;
                        dr["VIDEO_PATH"] = i.VIDEO_PATH;
                        dr["STATUS"] = i.STATUS;


                        tbl.Rows.Add(dr);
                    }

                    SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_MODULE_MASTER", columns);

                   
                }
          
                else
                {
                    SqlHelper.UpdateCourseModule<COURSE_MODULE>(course.MODULE, "TB_MODULE_MASTER", connstring, columns);
                }
                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
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


        public List<COURSE> GetSearch(string dbConn, string COURSE_NAME, int NO_OF_MODULES, string CATEGORY, string SUB_CATEGORY, string LEVEL_OF_COURSE, string CREATED_BY)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "Search" },
                  new SqlParameter("@COURSE_NAME", SqlDbType.VarChar,100){ Value = COURSE_NAME },
                  new SqlParameter("@NO_OF_MODULES", SqlDbType.Int) { Value = NO_OF_MODULES },
                  new SqlParameter("@CATEGORY", SqlDbType.VarChar, 100){Value = CATEGORY },
                  new SqlParameter("@SUB_CATEGORY", SqlDbType.VarChar, 100) { Value = SUB_CATEGORY },
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

        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }

        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }
    }
}

