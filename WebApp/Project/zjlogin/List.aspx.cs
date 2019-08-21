using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Support;

namespace WebApp.Project.zjlogin
{
    public partial class List : Yawei.Common.SharedPage
    {
        protected string zjguid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            zjguid=this.Context.Request.Params["zjguid"]??"";
        }
    }
}