using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.TZ_XMGL.tz_zjcg
{
    public partial class List : System.Web.UI.Page
    {
        protected string strProjGuid = string.Empty;
        protected string strOtherSql = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strProjGuid = Request["xmguid"] == null ? "" : Request["xmguid"];

            if (strProjGuid != "")
                strOtherSql = " and xmguid='" + strProjGuid + "'";
        }
    }
}