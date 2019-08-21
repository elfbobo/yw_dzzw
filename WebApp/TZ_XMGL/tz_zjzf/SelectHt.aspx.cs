using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.TZ_XMGL.tz_zjzf
{
    public partial class SelectHt : System.Web.UI.Page
    {
        protected string strProjGuid = string.Empty;
        protected string strOtherSql = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            strProjGuid = Request["proGuid"] == null ? "" : Request["proGuid"];

            if (strProjGuid != "")
                strOtherSql = " and xmguid='" + strProjGuid + "'";
        }
    }
}