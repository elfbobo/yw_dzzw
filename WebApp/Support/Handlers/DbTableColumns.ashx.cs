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
    /// DbTableColumns 的摘要说明
    /// </summary>
    public class DbTableColumns : IHttpHandler
    {
        protected string strTable = "";
        protected string strModelGuid = "";
        protected string strDBConnection = "";
        protected Database database = DatabaseFactory.CreateDatabase();
        protected Database db = DatabaseFactory.CreateDatabase();
        public void ProcessRequest(HttpContext context)
        {
            string strHtml = "";
            #region  接收参数
            strTable = context.Request["Table"] == null ? "" : context.Request["Table"];
            strModelGuid = context.Request["ModelGuid"] == null ? "" : context.Request["ModelGuid"];
            GetSqlConnText();
            #endregion
            database = DatabaseFactory.CreateDatabase(strDBConnection, "Yawei.DataAccess.SqlClient.SqlDatabase");
            DataSet doc = null;
            DbCommand cmd = new SqlCommand();
            cmd.CommandText = "TableDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@table", strTable));
            doc = database.ExecuteDataSet(cmd);

            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = doc.Tables[0].Rows[i];
                    strHtml += "<option value='" + dr["FieldName"].ToString() + "'>" + dr["FieldDesc"].ToString() + "</option>";
                }
            }
            context.Response.Clear();
            context.Response.Write(strHtml);
            context.Response.End();
        }

        /// <summary>
        /// 获取该项目的数据库连接串
        /// </summary>
        protected void GetSqlConnText()
        {
            strDBConnection = database.ExecuteScalar("select DBConnection from Sys_DataModel where guid='" + strModelGuid + "'").ToString();
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