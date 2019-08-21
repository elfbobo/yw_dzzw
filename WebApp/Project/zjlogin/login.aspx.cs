using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Project.zjlogin
{
    public partial class login : Yawei.Common.SharedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Redirect("List.aspx?zjguid=5");
        }
    }
}