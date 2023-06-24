using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IMMSOQAMaintain_Api.common
{
    public class MysqlHelper<T>
    {
        readonly static string conStr = System.Configuration.ConfigurationManager.AppSettings["MysqlConnStr"].ToString();
        public static int Execute(string sqlSmd)
        {
            int res = 0;
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sqlSmd, connection);
                res = cmd.ExecuteNonQuery();
            }
            return res;
        }

        public static DataTable SqlDataTable(string sqlSmd)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sqlSmd, connection);
                MySqlDataAdapter myAdpt = new MySqlDataAdapter(cmd);
                DataTable myDt = new DataTable();
                myAdpt.Fill(myDt);
                return myDt;
            }
        }

        public static List<T> Query(string sqlCmd)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sqlCmd, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                return GetList<T>(reader);
            }
        }

        private static List<T> GetList<T>(MySqlDataReader sdr)
        {
            //声明泛型列表
            List<T> list = new List<T>();

            //获取泛型类型
            Type type = typeof(T);

            //获取泛型对象的属性
            PropertyInfo[] properties = type.GetProperties();
            while (sdr.Read())
            {
                T model = Activator.CreateInstance<T>();
                for (int i = 0; i < properties.Length; i++)
                {
                    for (int j = 0; j < sdr.FieldCount; j++)
                    {
                        //判断属性的名称和字段的名称是否相同
                        if (properties[i].Name == sdr.GetName(j))
                        {
                            Object value = sdr[j];

                            //将字段的值赋值给User中的属性
                            properties[i].SetValue(model, value, null);
                        }
                    }
                }
                list.Add(model);
            }
            return list;
        }
    }
}