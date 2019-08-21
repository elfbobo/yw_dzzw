using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.Common;
using Yawei.DataAccess;

namespace Yawei.SupportCore
{
    public static class ExcelHelperCore
    {
        public static string GetDataJson(string tableName, string where, string conName)
        {
            Database database = null;
            if (!string.IsNullOrEmpty(conName))
                database = DatabaseFactory.CreateDatabase(conName);
            else
                database = DatabaseFactory.CreateDatabase();
            DataSet ds = new DataSet();
            if (tableName == "V_ProjAssessment")
            {
                ds = database.ExecuteDataSet("select * from " + tableName + " ORDER BY constructionScore DESC ");
            }
            else
            {
                ds = database.ExecuteDataSet("select * from " + tableName + " where 1=1 " + where);
            }
            return ConvertJson.ToJson(ds.Tables[0]);
        }

        public static int InportData(string pathFileName, string guid, string filed, int rows, string table, string conName, string key)
        {
            Database database = null;
            if (!string.IsNullOrEmpty(conName))
                database = DatabaseFactory.CreateDatabase(conName);
            else
                database = DatabaseFactory.CreateDatabase();


            DbConnection connt = database.CreateConnection();
            Database.OpenConnection(connt);
            DbTransaction tran = Database.BeginTransaction(connt);

            try
            {


                string strConn = "";
                if (Path.GetExtension(pathFileName) == ".xls")
                {
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathFileName + ";Extended Properties=Excel 8.0;";
                }
                else
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathFileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";

                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter odda = new OleDbDataAdapter("select * from [Sheet1$]", conn);
                odda.Fill(ds, "ExcelTable");

                string[] fliedArr = filed.Split(',');
                DataSet dat= database.ExecuteDataSet("select * from " + table + " where 1=0");

                string keyStr = "";
                for (int i = rows; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dat.Tables[0].NewRow();
                    dr["Guid"] = Guid.NewGuid();
                    dr["Status"] = 0;
                    dr["SysStatus"] = 0;
                    for (int j = 0; j < fliedArr.Length; j++)
                    {
                        if (!string.IsNullOrWhiteSpace(key) && fliedArr[j].ToLower() == key.ToLower())
                        {
                            if (keyStr != "")
                                keyStr += ",";
                            keyStr += "'" + ds.Tables[0].Rows[i][j] + "'";
                        }
                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i][j].ToString()))
                            dr[fliedArr[j]] = ds.Tables[0].Rows[i][j];
                    }
                    dat.Tables[0].Rows.Add(dr);
                }
                dat.Tables[0].TableName = table;
                if (key != "")
                {
                    database.ExecuteNonQuery("delete from " + table + " where " + key + " in(" + keyStr + ")", tran);
                }
                database.UpdateDataSet(dat, tran);

                tran.Commit();
                odda.Dispose();
                conn.Close();
                File.Delete(pathFileName);
                database.ExecuteNonQuery("delete from Sys_FileInfo where guid='" + guid + "'");
                return 1;

            }
            catch
            {
                Database.RollbackTransaction(tran);
                return 0;
            }
            finally
            {
                Database.CloseConnection(connt);
            }
        }

    }
}
