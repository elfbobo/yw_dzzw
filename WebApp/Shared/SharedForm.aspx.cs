using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Project;
using System.Web.Configuration;

namespace Yawei.App.Shared
{
    public partial class SharedForm : SharedPage
    {
        public string menuJson = string.Empty;
        public string userCn = string.Empty;
        public string returl = "";
        protected bool isshow = true;
        protected string html = "";
        protected string RemindGuid = "";
        protected string userType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentUser == null || CurrentUser.UserRole.Count <= 0)
            {
                Response.Redirect("UserError.html");
            }

            string type = Request["type"] != null ? Request["type"] : "";
            userType = WebConfigurationManager.AppSettings["SystemUser"];
            if (type == "post")
            {
                string guid = Request["guid"] != null ? Request["guid"] : "";
                int result = WorkRemindFacade.UpdateRemindStatus(guid, CurrentUser.UserGuid);
                Response.Clear();
                Response.Flush();
                Response.Write(result);
                Response.End();
            }
            else if (type == "jumppost")
            {
                string guid = Request["guid"] != null ? Request["guid"] : "";
                int result = WorkRemindFacade.UpdateRemindSysStatus(guid);
                Response.Clear();
                Response.Flush();
                Response.Write(result);
                Response.End();
            }
            else
            {
                userCn = CurrentUser.UserCN.Length > 9 ? CurrentUser.UserCN.Substring(0, 8) + "..." : CurrentUser.UserCN;
                menuJson = CurrentUser.GetMenuJSON();
             
                if (userType.ToLower() == "ad")
                {
                }
                else
                {
                    returl = "../Support/Login/Default.aspx";
                }

                isshow = WorkRemindFacade.GetIsShowRemind(out RemindGuid, CurrentUser.UserGuid, out html);
                html = html.Replace("\r\n", "<br/>&nbsp;&nbsp;&nbsp;&nbsp;").Replace("'", "\"");
            }
        }
    }
}