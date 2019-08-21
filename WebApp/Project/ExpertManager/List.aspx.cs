using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Project.ExpertManager
{
    public partial class List : Yawei.Common.SharedPage
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