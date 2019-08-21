using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.TZ_XMGL.tz_xmht
{
    public partial class View : SharedPage
    {
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected string strProjGuid = string.Empty;
        protected string strGuid = string.Empty;
        protected CommonForm form = new CommonForm();
        protected string strTitle = string.Empty;

        protected string strIssueRect = string.Empty;//问题整改动态行信息
        private Yawei.DataAccess.Database db = null;

        protected DataSet dsHtfkfs = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            strProjGuid = Request["ProjGuid"] != null ? Request["ProjGuid"] : "";
            strGuid = Request["Guid"] != null ? Request["Guid"] : "";

            form.TableName = "tz_xmht"; //表名
            form.Key = "guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            document = form.SetViewData(null);

            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            dsHtfkfs = db.ExecuteDataSet(" select * from tz_xmht_zffs where htguid='" + strGuid + "' order by updatesj");

            System.GC.Collect();
        }

        protected void Page_DeleteData(object sender, EventArgs e)
        {
            form.DeleteData();
            Response.Redirect("List.aspx?xmguid=" + strProjGuid);
        }
    }
}