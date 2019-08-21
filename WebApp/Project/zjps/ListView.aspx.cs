using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Support;

namespace WebApp.Project.zjps
{
    public partial class ListView : Yawei.Common.SharedPage
    {
        protected string strGuid = "";
        protected string xmguid = "";
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected CommonForm form = new CommonForm();
        private Yawei.DataAccess.Database db = null;
        protected System.Data.DataTable dtExperts = new System.Data.DataTable();

        protected string strIssueRect = string.Empty;//问题整改动态行信息

        protected void Page_Load(object sender, EventArgs e)
        {
            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            strGuid = this.Request.Params["xmguid"] ?? "";
            xmguid = this.Request.Params["xmguid"] ?? "";

            form.TableName = "tz_Project"; //表名
            form.Key = "ProGuid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;
            document = form.SetViewData(null);

            strIssueRect = form.GetRowDataXml("v_tz_ProjectExpert", "ProGuid", form.KeyValue, null, " order by pssj asc ");
        }

        protected void Page_SaveData(object sender, EventArgs e)
        {
            //保存问题整改动态行
            DataSet ds = form.RowData(Request["IssueRowData"], "tz_ProjectExpertPS", "ProGuid", form.KeyValue);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["pssj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            ds.AcceptChanges();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string sqlinsert = String.Format(@"insert into tz_ProjectExpertPS(Guid,ProGuid,ExpertGuid,psyj,pssj,sysstatus,Status)
values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", dr["Guid"], dr["ProGuid"], dr["ExpertGuid"], dr["psyj"], dr["pssj"], 0, 0);
                db.ExecuteNonQuery(sqlinsert);
            }

            Response.Redirect("List.aspx?xmguid=" + form.KeyValue);
        }

    }
}