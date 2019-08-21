using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.DataAccess;

namespace Yawei.App.Support.DataStruct.DbOpt
{
    public partial class DataColumns : System.Web.UI.Page
    {
        protected string sql = "";
        protected string json = "";
        protected string strTable = "";
        protected string strModelGuid = "";
        protected string strDBConnection = "";
        protected Database database = DatabaseFactory.CreateDatabase();
        protected Database db = DatabaseFactory.CreateDatabase();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region  接收参数
            strTable = Request["Table"] == null ? "" : Request["Table"];
            strModelGuid = Request["ModelGuid"] == null ? "" : Request["ModelGuid"];
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
                json += "{id:\"-1\",pId:\"0\", name: \"字段名称\", isParent: true,children:[";
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = doc.Tables[0].Rows[i];
                    json += string.Format("{{id:'{0}',pId:'{1}',name:'{2}',value:'{3}'}},", (i + 1), "-1", dr["FieldDesc"].ToString() + "(" + dr["FieldName"] + ")", dr["FieldDesc"].ToString() + "^" + dr["FieldName"]);
                }
                json += "]}";
                json = json.TrimEnd(',');
            }
            json = "[" + json + "]";
        }

        /// <summary>
        /// 获取该项目的数据库连接串
        /// </summary>
        protected void GetSqlConnText()
        {
            strDBConnection = database.ExecuteScalar("select DBConnection from Sys_DataModel where guid='" + strModelGuid + "'").ToString();
        } 
    }
}