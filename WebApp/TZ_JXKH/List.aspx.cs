using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;

namespace WebApp.TZ_JXKH.tz_jxkh
{
    public partial class List : SharedPage
    {
        protected string strProjGuid = string.Empty;
        protected string strOtherSql = string.Empty;
        protected RoleCheck roleCheck;

        protected void Page_Load(object sender, EventArgs e)
        {
            strProjGuid = Request["xmguid"];
            roleCheck = new RoleCheck(CurrentUser);
        }
    }
}