using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.SupportCore
{
    public static class TableEditCore
    {
        public static string LoadTableInfo(string tableName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.CreateCommand(CommandType.StoredProcedure, "TableDetails");
            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@table", tableName));
            DataSet ds = database.ExecuteDataSet(cmd);
            return Common.ConvertJson.ToJson(ds.Tables[0]);

        }


        public static void CreatePage(string path, string name, string content)
        {
            if (File.Exists(path + name))
            {
                string test = File.ReadAllText(path + name);
                File.WriteAllText(path + name.Replace(".aspx", DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString().Replace(":", " ") + ".aspx"), test, Encoding.UTF8);
            }

            File.WriteAllText(path + name, content, Encoding.UTF8);
        }

        public static string GetDataStruct()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string json = "";
            DataSet ds = database.ExecuteDataSet("select * from Sys_DataModel ");
            DataSet dat = database.ExecuteDataSet("select * from Sys_DataTable  order by TableName desc");
            DataSet tab = database.ExecuteDataSet("select * from Sys_TableEdit ");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                    json += ",";
                json += "{name:'" + ds.Tables[0].Rows[i]["DBName"] + "'";
                DataRow[] datRow = dat.Tables[0].Select("ModelGuid='" + ds.Tables[0].Rows[i]["Guid"] + "'");
                if (datRow.Length > 0)
                {
                    json += ",children:[";
                    for (int j = 0; j < datRow.Length; j++)
                    {
                        if (j > 0)
                            json += ",";
                        var tabrow= tab.Tables[0].Select("TableName='" + datRow[j]["TableName"] + "'");
                        if (tabrow.Length > 0)
                        { json += "{name:'" + datRow[j]["Display"] + "',table:'" + datRow[j]["TableName"] + "',f:true}"; }
                        else
                        {
                            json += "{name:'" + datRow[j]["Display"] + "',table:'" + datRow[j]["TableName"] + "'}"; 
                        }
                    }
                    json += "]}";
                }
                else
                {
                    json += "}";
                }
            }
            return "[" + json + "]";
        }

        public static string GetDataStructTree()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string json = "";
            DataSet ds = database.ExecuteDataSet("select * from Sys_DataModel ");
            DataSet dat = database.ExecuteDataSet("select * from Sys_DataTable  order by TableName desc");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                    json += ",";
                json += "{id:'" + ds.Tables[0].Rows[i]["Guid"] + "',name:'" + ds.Tables[0].Rows[i]["DBDisplay"] + "',title:'" + ds.Tables[0].Rows[i]["DBName"] + "'";
                DataRow[] datRow = dat.Tables[0].Select("ModelGuid='" + ds.Tables[0].Rows[i]["Guid"] + "'");
                if (datRow.Length > 0)
                {
                    json += ",children:[";
                    for (int j = 0; j < datRow.Length; j++)
                    {
                        if (j > 0)
                            json += ",";
                        json += "{id:'" + datRow[j]["TableName"] + "',name:'" + datRow[j]["Display"] + "',title:'" + datRow[j]["TableName"] + "'}";
                    }
                    json += "]}";
                }
                else
                {
                    json += "}";
                }
            }
            return "[" + json + "]";
        }

        public static string GetMappingDirectory()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet ds = database.ExecuteDataSet("select * from Sys_Mapping");
            DataRow[] drArr = ds.Tables[0].Select("DirectoryGuid is null");
            string json = string.Empty;
            for (int i = 0; i < drArr.Length; i++)
            {
                if (i > 0)
                    json += ",";
                json += "{id:'" + drArr[i]["Guid"] + "',name:'" + drArr[i]["Name"] + "'";
                json += GetMappingDirectory(drArr[i]["Guid"].ToString(), ds);
            }
            return "[" + json + "]";
        }

        static string GetMappingDirectory(string guid, DataSet ds)
        {
            DataRow[] drArr = ds.Tables[0].Select("DirectoryGuid='" + guid + "'");
            if (drArr.Length > 0)
            {
                string json = ",children:[";
                for (int i = 0; i < drArr.Length; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{id:'" + drArr[i]["Guid"] + "',name:'" + drArr[i]["Name"] + "'";
                    json += GetMappingDirectory(drArr[i]["Guid"].ToString(), ds);
                }
                return json + "]}";
            }
            else
            {
                return "}";
            }
        }


        public static string GetPathJson(string path)
        {
            path = path.Replace("WebApp", "$").Split('$')[0];
            path = path + "WebApp";
            string[] arrPath = Directory.GetDirectories(path);
            string json = "[{id:1,name:'WebApp',open:true,icon:'img/folder_edit.png',children:[";
            var b = false;
            for (int i = 0; i < arrPath.Length; i++)
            {
                string[] t = arrPath[i].Split('\\');
                string str = t[t.Length - 1];
                if (str.ToLower() != "properties" && str.ToLower() != "obj" && str.ToLower() != "bin" && str.ToLower() != "images" && str.ToLower() != "content" && str.ToLower() != "scripts" && str.ToLower() != "plugins" && str.ToLower() != "script" && str.ToLower() != "img" && str.ToLower() != "image")
                {
                    if (b)
                        json += ",";
                    json += "{id:'" + Guid.NewGuid().ToString() + "',name:'" + str + "',icon:'img/folder_edit.png',path:'" + arrPath[i].Replace("\\", "$") + "'";
                    json += GetChildPathJson(arrPath[i]);
                    json += "}";
                    b = true;
                }
            }
            json += "]}]";
            return json;
        }

        static string GetChildPathJson(string path)
        {
            string json = "";
            string[] arrPath = Directory.GetDirectories(path);
            if (arrPath.Length > 0)
            {
                json += ",children:[";
                for (int i = 0; i < arrPath.Length; i++)
                {
                    if (i > 0)
                        json += ",";
                    string[] t = arrPath[i].Split('\\');
                    string str = t[t.Length - 1];
                    json += "{id:'" + Guid.NewGuid().ToString() + "',name:'" + str + "',icon:'img/folder_edit.png',path:'" + arrPath[i].Replace("\\", "$") + "'";
                    json += GetChildPathJson(arrPath[i]);
                    json += "}";
                }
                json += "]";
            }
            return json;
        }

        public static DataSet GetEditTableSetting(string tableName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            return database.ExecuteDataSet("select * from Sys_TableEdit where TableName='" + tableName + "'");
        }
    }
}
