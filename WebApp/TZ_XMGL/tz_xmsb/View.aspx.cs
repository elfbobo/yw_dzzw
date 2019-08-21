using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.TZ_XMGL.tz_xmsb
{
    public partial class View : SharedPage
    {
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected string strProjGuid = string.Empty;
        protected string strGuid = string.Empty;
        protected CommonForm form = new CommonForm();
        protected string strTitle = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            // strProjGuid = Request["Guid"] != null ? Request["Guid"] : "";
            strGuid = Request["xmguid"] != null ? Request["xmguid"] : "";

            form.TableName = "tz_Project"; //表名
            form.Key = "ProGuid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            document = form.SetViewData(null);


            System.GC.Collect();
        }

        protected string getIsCloud(string value)
        {
            if (value == "0")
            {
                return "是";
            }
            else
            {
                return "否";
            }
        }

    }
}