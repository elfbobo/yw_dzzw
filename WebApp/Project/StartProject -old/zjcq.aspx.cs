using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Support;

namespace WebApp.Project.StartProject
{
    public partial class zjcq : Yawei.Common.SharedPage
    {
        protected string strGuid = "";
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected CommonForm form = new CommonForm();
        private Yawei.DataAccess.Database db = null;
        protected System.Data.DataTable dtExperts = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            strGuid = this.Request.Params["xmguid"] ?? "";

            form.TableName = "tz_Project"; //表名
            form.Key = "ProGuid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;
            document = form.SetViewData(null);

            //获取当前项目的上次抽取的评审专家
            DataSet ds = db.ExecuteDataSet(" select * from tz_ProjectExpert where exists(select 1 from tz_ProjectAndExpert where tz_ProjectExpert.guid=tz_ProjectAndExpert.ExpertGuid and tz_ProjectAndExpert.proguid='" + strGuid + "') ");
            dtExperts = ds.Tables[0];
            list_experts.DataSource = dtExperts;
            list_experts.DataBind();
        }
    }
}