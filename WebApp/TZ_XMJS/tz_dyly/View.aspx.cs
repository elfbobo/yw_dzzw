using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.TZ_XMJS.tz_dyly
{
    public partial class View : SharedPage
    {
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected string strProjGuid = string.Empty;
        protected string strGuid = string.Empty;
        protected CommonForm form = new CommonForm();
        protected string strTitle = string.Empty;

        protected RoleCheck roleCheck;
        protected void Page_Load(object sender, EventArgs e)
        {
            roleCheck = new RoleCheck(CurrentUser);
            strProjGuid = Request["xmguid"] != null ? Request["xmguid"] : "";
            strGuid = Request["Guid"] != null ? Request["Guid"] : "";

            form.TableName = "tz_dyly"; //表名
            form.Key = "guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            document = form.SetViewData(null);


            System.GC.Collect();
        }

        protected void Page_DeleteData(object sender, EventArgs e)
        {
            form.DeleteData();
            Response.Redirect("List.aspx?xmguid=" + strProjGuid);
        }
    }
}