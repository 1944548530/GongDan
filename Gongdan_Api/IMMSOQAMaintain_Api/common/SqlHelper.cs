using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Web;

namespace IMMSOQAMaintain_Api.common
{
    public class SqlHelper<T>
    {
        readonly static string conStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();

        public static int Execute(string sqlSmd) {
            int res = 0;
            using (IDbConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                res = connection.Execute(sqlSmd);
            }
            return res;
        }

        public static IEnumerable<T> Query(string sqlCmd)
        {
            IEnumerable<T> res = null;
            using (IDbConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                res = connection.Query<T>(sqlCmd);
            }
            return res;
        }

        public static DataTable sqlTable(string sqlStr)
        {
            SqlConnection myConn = new SqlConnection(conStr);
            myConn.Open();
            try
            {
                SqlCommand myCmd = new SqlCommand(sqlStr, myConn);
                SqlDataAdapter myAdpt = new SqlDataAdapter(myCmd);
                DataTable myDt = new DataTable();
                myAdpt.Fill(myDt);
                return myDt;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                myConn.Close();
            }
        }

    }
}