using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Yawei.DataAccess;
using Yawei.Common;

namespace Yawei.SupportCore
{
    public class SysGridCore
    {
        public string ConnectionName { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        public string TableName { get; set; }
        public string IdField { get; set; }
        public string Page { get; set; }
        public string Rows { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
        public string Condition { get; set; }
        public string Where { get; set; }
        public string ParentField { get; set; }
        public string ChildField { get; set; }
        public string ChildWhere { get; set; }
        //是否关联查询
        public string RelevanSearch { get; set; }

        public string CreateDataJson()
        {
            if (string.IsNullOrEmpty(TableName))
                throw new Exception("tableName不能为空");
            try
            {
                return GetData();
            }
            catch
            {
                return "{\"total\":0,\"rows\":[]}";
            }
        }

        Database GetDatabase()
        {
            Database database = null;

            // 根据数据库连接字符串与数据库类型获取
            if (database == null)
            {
                if (ConnectionString != "" && ProviderName != "")
                {
                    switch (ProviderName)
                    {
                        case "Yawei.DataAccess.SqlClient.SqlDatabase":
                            database = DatabaseFactory.CreateDatabase(ConnectionString, "Yawei.DataAccess.SqlClient.SqlDatabase");
                            break;
                        case "Yawei.DataAccess.Oracle.OracleDatabase":
                            database = DatabaseFactory.CreateDatabase(ConnectionString, "Yawei.DataAccess.Oracle.OracleDatabase");
                            break;
                        case "Yawei.DataAccess.OleDb.OleDbDatabase":
                            database = DatabaseFactory.CreateDatabase(ConnectionString, "Yawei.DataAccess.OleDb.OleDbDatabase");
                            break;
                        case "Yawei.DataAccess.Odbc.OdbcDatabase":
                            database = DatabaseFactory.CreateDatabase(ConnectionString, "Yawei.DataAccess.Odbc.OdbcDatabase");
                            break;
                    }
                }
            }

            //根据数据库配置名称获取
            if (database == null)
            {
                if (ConnectionName != "")
                {
                    database = DatabaseFactory.CreateDatabase(ConnectionName);
                }
            }

            //根据默认的数据库配置名称获取
            if (database == null)
            {
                database = DatabaseFactory.CreateDatabase();
            }

            return database;
        }

        public static string ForTable(DataTable Datas, string ParentKeyValue, string ParentKey, string ChildKey, string Json)
        {
            DataRow[] drs = Datas.Select(string.Format("{0}='{1}'", ParentKey, ParentKeyValue));
            foreach (DataRow dr in drs)
            {
                if (Datas.Select(string.Format("{0}='{1}'", ParentKey, dr[ChildKey].ToString())).Length > 0)
                {
                    Json += ConvertJson.ToParentJson(dr, Datas);
                    string jsons = string.Empty;
                    Json += ForTable(Datas, dr[ChildKey].ToString(), ParentKey, ChildKey, jsons);
                    Json = Json.TrimEnd(',');
                    Json += "]},";
                }
                else
                {
                    Json += ConvertJson.ToNoParentJson(dr, Datas);
                }
            }
            return Json;
        }

        /// <summary>
        /// 获取列表数据库
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            string json = "";
            Database database = GetDatabase();
            object obj = "0";
            if (database != null)
            {
                string[] idFields = IdField.Split(',');
                for (int i = 0; i < idFields.Length; i++)
                {
                    idFields[i] = "[" + idFields[i] + "]";
                }

                string pkField = string.Join("+", idFields);

                string[] sortFields = Sort.Split(',');
                string[] orderFields = Order.Split(',');
                for (int i = 0; i < sortFields.Length; i++)
                {
                    sortFields[i] = "[" + sortFields[i] + "]";
                    if (orderFields.Length > i)
                    {
                        sortFields[i] = sortFields[i] + " " + orderFields[i];
                    }
                }

                string sortField = string.Join(",", sortFields);
                Condition = Condition + Where;

                string sql = string.Empty;
                //多行递归
                if (ParentField != "" && ChildField != "")
                {
                    string lstr = string.Empty;
                    string sqls = string.Empty;
                    if (int.Parse(Page) > 1)
                    {
                        //是否关联查询
                        if (RelevanSearch == "true")
                        {
                            sql = "select * from (select row_number() over (order by " + sortField + ") as rowNum,* from " + TableName + " where 1=1 and ([" + ParentField + "] is NULL or [" + ParentField + "]='') " + Condition + " or (" + ChildField + " in (select " + ParentField + " from [" + TableName + "] where 1=1 " + Condition + ")) ) as t where rowNum between " + (int.Parse(Rows) * (int.Parse(Page) - 1) + 1) + " and " + (int.Parse(Rows) * (int.Parse(Page))) + " order by rowNum;";
                        }
                        else
                        {
                            sql = "select * from (select row_number() over (order by " + sortField + ") as rowNum,* from " + TableName + " where 1=1 and ([" + ParentField + "] is NULL or [" + ParentField + "]='') " + Condition + " ) as t where rowNum between " + (int.Parse(Rows) * (int.Parse(Page) - 1) + 1) + " and " + (int.Parse(Rows) * (int.Parse(Page))) + " order by rowNum;";
                        }
                    }
                    else
                    {
                        //是否关联查询
                        if (RelevanSearch == "true")
                        {
                            sql = "select top " + Rows + " * from (select row_number() over (order by " + sortField + ") as rowNum, * from [" + TableName + "] where 1=1 and ([" + ParentField + "] is NULL or [" + ParentField + "]='') " + Condition + " or (" + ChildField + " in (select " + ParentField + " from [" + TableName + "] where 1=1 " + Condition + "))  ) AS t ORDER BY rowNum;";
                        }
                        else
                        {
                            sql = "select top " + Rows + " * from (select row_number() over (order by " + sortField + ") as rowNum, * from [" + TableName + "] where 1=1 and ([" + ParentField + "] is NULL or [" + ParentField + "]='') " + Condition + "  ) AS t ORDER BY rowNum;";
                        }
                    }

                    //是否关联查询
                    if (RelevanSearch == "true")
                    {
                        obj = database.ExecuteScalar("select count(*) from " + TableName + " where 1=1 and ([" + ParentField + "] is NULL or [" + ParentField + "]='') " + Condition + " or (" + ChildField + " in (select " + ParentField + " from [" + TableName + "] where 1=1 " + Condition + "))");
                        sql = sql + "select * from [" + TableName + "] where  1=1 " + Condition + " or (" + ChildField + " in (select " + ParentField + " from [" + TableName + "] where 1=1 " + Condition + ") and sysstatus<>-1) or (" + ParentField + " in (select " + ChildField + " from [" + TableName + "] where 1=1 " + Condition + ") and sysstatus<>-1) order by " + sortField;
                    }
                    else
                    {
                        obj = database.ExecuteScalar("select count(*) from " + TableName + " where 1=1 and ([" + ParentField + "] is NULL or [" + ParentField + "]='') " + Condition);
                        sql = sql + "select * from [" + TableName + "] where  1=1 " + Condition + " order by " + sortField;
                    }
                    DataSet doc = database.ExecuteDataSet(sql);
                    if (doc.Tables[0].Rows.Count > 0)
                    {
                        //ParentField   TopGuid
                        //ChildField    Guid
                        json = "[";
                        var temp = doc.Tables[0].Select();
                        var temp2 = doc.Tables[1].Select();
                        foreach (DataRow dr in doc.Tables[0].Select())
                        {
                            lstr = "";
                            DataRow[] drs = doc.Tables[1].Select(string.Format("{0}='{1}'", ParentField, dr[ChildField].ToString()));
                            if (drs.Length > 0)
                            {
                                json += ConvertJson.ToParentJson(dr, doc.Tables[0]);
                                json += ForTable(doc.Tables[1], dr[ChildField].ToString(), ParentField, ChildField, lstr);
                                json = json.TrimEnd(',');
                                json += "]},";
                            }
                            else
                            {
                                json += ConvertJson.ToNoParentJson(dr, doc.Tables[0]);
                            }
                        }
                        json = json.TrimEnd(',');
                        json += "]";
                    }
                    else
                    {
                        json = "[]";
                    }
                }
                //无递归
                else
                {
                    if (int.Parse(Page) > 1)
                    {
                        sql = "select * from (select row_number() over (order by " + sortField + ") as rowNum,* from " + TableName + " where 1=1" + Condition + " ) as t where rowNum between " + (int.Parse(Rows) * (int.Parse(Page) - 1) + 1) + " and " + (int.Parse(Rows) * (int.Parse(Page))) + " order by rowNum";
                        //sql = "select top " + rows + " *  from [" + tableName + "] where " + pkField + " not in (select top " + (int.Parse(rows) * (int.Parse(page) - 1)).ToString() + " " + pkField + " from [" + tableName + "] where 1=1 " + condition + " order by " + sortField + " " + order + ") " + condition + " order by " + sortField + " " + order;
                    }
                    else
                        sql = "select top " + Rows + " * from (select row_number() over (order by " + sortField + ") as rowNum, * from [" + TableName + "] where 1=1 " + Condition + "  ) AS t ORDER BY rowNum";



                    DataSet doc = database.ExecuteDataSet(sql);
                    obj = database.ExecuteScalar("select count(*) from " + TableName + " where 1=1 " + Condition);

                    if (doc.Tables[0].Rows.Count > 0)
                    {
                        json = ConvertJson.ToJson(doc.Tables[0]);
                    }
                    else
                    {
                        json = "[]";
                    }
                }
            }
            else
            {
                json = "[]";
                obj = "0";
            }
            json = "{\"total\":" + obj.ToString() + ",\"rows\":" + json + "}";
            return json;
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="conName"></param>
        /// <param name="tabel"></param>
        /// <param name="where"></param>
        /// <param name="refWhere"></param>
        /// <returns></returns>
        public static int DeleteRow(string conName, string tabel, string where, string refWhere)
        {
            try
            {
                Database database = null;
                if (!string.IsNullOrEmpty(conName))
                {
                    database = DatabaseFactory.CreateDatabase(conName);
                }
                else
                    database = DatabaseFactory.CreateDatabase();

                string sql = string.Empty;
                if (tabel == "Busi_ProjRegister")
                    sql = "delete Busi_ProjRegisterDeparts where ProjGuid in (select Guid from Busi_ProjRegister where 1=1" + where + " " + refWhere + ");";

                return database.ExecuteNonQuery("update " + tabel + " set sysstatus=-1 " + " where 1=1" + where + " " + refWhere + ";" + sql);
            }
            catch
            {
                return -1;
            }
        }
    }
}
