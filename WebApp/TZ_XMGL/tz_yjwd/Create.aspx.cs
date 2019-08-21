using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.TZ_XMGL.tz_yjwd
{
    public partial class Create : SharedPage
    {
        CommonForm form = new CommonForm();
        protected string strGuid = string.Empty;
        protected string proGuid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strGuid = Request["Guid"] != null ? Request["Guid"] : "";
            proGuid = Request["xmguid"] != null ? Request["xmguid"] : "";

        }

        protected void Page_SaveData(object sender, EventArgs e)
        {
            //string[,] arr = { { "xmguid", proGuid } };
            //form.SaveData(this.Request, arr); //保存数据
            Response.Redirect("View.aspx?guid=" + form.KeyValue + "&ProjGuid=" + proGuid);
        }
    }
}