using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;

namespace WebApp.TZ_XMJS.tz_jzxcs
{
    public partial class List : SharedPage
    {
        protected string strProjGuid = string.Empty;
        protected string strOtherSql = string.Empty;
        protected RoleCheck roleCheck;
        protected void Page_Load(object sender, EventArgs e)
        {
            roleCheck = new RoleCheck(CurrentUser);
            strProjGuid = Request["xmguid"] == null ? "" : Request["xmguid"];

            if (strProjGuid != "")
                strOtherSql = " and xmguid='" + strProjGuid + "'";
        }
    }
}