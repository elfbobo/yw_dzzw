using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yawei.App.Shared
{
    public partial class PlanSelect : System.Web.UI.Page
    {
        protected string strProjGuid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strProjGuid = Request["ProjGuid"] == null ? "" : Request["ProjGuid"];
        }
    }
}