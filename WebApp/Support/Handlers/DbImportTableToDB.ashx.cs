using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// DbImportTableToDB 的摘要说明
    /// </summary>
    public class DbImportTableToDB : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();
        Database db = DatabaseFactory.CreateDatabase();
        string Tables = string.Empty;
        string DbConn = string.Empty;
        string ModelGuid = string.Empty;
        string sql = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            DbConn = context.Request["DbConn"] == null ? "" : context.Request["DbConn"];
            Tables = context.Request["Tables"] == null ? "" : context.Request["Tables"];
            ModelGuid = context.Request["ModelGuid"] == null ? "" : context.Request["ModelGuid"];
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase(DbConn, "Yawei.DataAccess.SqlClient.SqlDatabase");
                string[] Arr = Tables.Split(',');
                for (int i = 0; i < Arr.Length; i++)
                {
                    TableOpt(Arr[i]);
                    ColumnsOpt(Arr[i]);
                }
                count = database.ExecuteNonQuery(sql);
            }
            catch
            {
                count = -1;
            }
            context.Response.Clear();
            context.Response.Write(count);
            context.Response.End();
        }

        public void ColumnsOpt(string Table)
        {
            DataSet doc = null;
            DbCommand cmd = new SqlCommand();
            cmd.CommandText = "TableDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@table", Table));
            doc = db.ExecuteDataSet(cmd);
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                    ColumnSql(Table, doc.Tables[0].Rows[i]);
            }
        }

        void ColumnSql(string Table, DataRow Row)
        {
            string ExtendProperty = "<xml />";
            DataRow[] rows = null;
            rows = GetFieldDesc(Table, Row["FieldName"].ToString()).Tables[0].Select("name<>'MS_Description'");
            if (rows != null && rows.Length > 0)
                ExtendProperty = SetProperty(rows);
            string FieldPK = "1";
            string FieldNull = "0";
            if (Row["FieldPK"].ToString() == "是")
                FieldPK = "0";
            if (Row["FieldNull"].ToString() == "不空")
                FieldNull = "1";
            sql += "INSERT INTO [Sys_DataColumn]([ModelGuid],[TableName],[Name],[Type],[Description],[Lengh],[Precision],[IsPrimaryKey],[IsNull],[DefaultValue],[ExtendProperty])";
            sql += " values('" + ModelGuid + "','" + Table + "','" + Row["FieldName"].ToString() + "','" + Row["FieldDataType"].ToString() + "'";
            sql += ",'" + Row["FieldDesc"].ToString() + "','" + Row["FieldLength"].ToString() + "','" + Row["FieldDecDigits"].ToString() + "'";
            sql += ",'" + FieldPK + "','" + FieldNull + "','" + Row["FieldValueDefault"].ToString().Replace("(", "").Replace(")", "") + "','" + ExtendProperty + "');";
        }


        void TableOpt(string Table)
        {
            DataRow[] rows = null;
            string Display = "";
            string ExtendProperty = "<xml />";
            rows = GetTableDesc(Table).Tables[0].Select("name='MS_Description'");
            if (rows != null && rows.Length > 0)
                Display = rows[0]["value"].ToString();
            rows = GetTableDesc(Table).Tables[0].Select("name<>'MS_Description'");
            if (rows != null && rows.Length > 0)
                ExtendProperty = SetProperty(rows);
            sql += "INSERT INTO [Sys_DataTable]([ModelGuid],[TableName],[Display],[ExtendProperty],[owner])";
            sql += " values('" + ModelGuid + "','" + Table + "','" + Display + "','" + ExtendProperty + "','dbo');";
        }


        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        string SetProperty(DataRow[] Rows)
        {
            string Property = "";
            if (Rows != null && Rows.Length > 0)
            {
                Property = "<xml>";
                for (int i = 0; i < Rows.Length; i++)
                    Property += "<Property " + Rows[i]["name"].ToString() + "=\"" + Rows[i]["value"].ToString() + "\" />";
                Property += "</xml>";
            }
            else
                Property = "<xml />";
            return Property;
        }

        DataSet GetDataSet(string sql)
        {
            DataSet doc = db.ExecuteDataSet(sql);
            return doc;
        }

        /// <summary>
        /// 读取表所有描述
        /// </summary>
        /// <param name="CNName"></param>
        /// <param name="EngName"></param>
        /// <returns></returns>
        protected DataSet GetTableDesc(string Table)
        {
            return GetDataSet("SELECT name,value FROM ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table','" + Table + "',  NULL, NULL);");
        }

        /// <summary>
        /// 读取字段所有描述
        /// </summary>
        /// <param name="NewDesc"></param>
        /// <param name="Field"></param>
        /// <returns></returns>
        protected DataSet GetFieldDesc(string Table, string Field)
        {
            return GetDataSet("SELECT name,value FROM ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table','" + Table + "', 'column', '" + Field + "');");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}