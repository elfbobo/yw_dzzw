using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.SupportCore;

namespace Yawei.App.Support.UserManage
{
    /// <summary>
    /// DbOpt 的摘要说明
    /// </summary>
    public class DbOpt : IHttpHandler
    {
        string users = string.Empty;
        string addusersAndGroupsql = string.Empty;
        string groupid = string.Empty;
        string userid = string.Empty;
        string isusercaninmoregroup = string.Empty;
        bool isinmoregroup = false;
        public void ProcessRequest(HttpContext context)
        {
            users = context.Request["users"] == null ? "" : context.Request["users"];
            addusersAndGroupsql = context.Request["addusersAndGroupsql"] == null ? "" : context.Request["addusersAndGroupsql"];
            groupid = context.Request["groupid"] == null ? "" : context.Request["groupid"];
            userid = context.Request["userid"] == null ? "" : context.Request["userid"];
            isusercaninmoregroup =context.Request["isusercaninmoregroup"] == null ? "" : context.Request["isusercaninmoregroup"].ToLower();
            if(isusercaninmoregroup=="true")
                isinmoregroup=true;
            context.Response.Clear();
            context.Response.Write(UserManageCore.CreateProjUser(users, addusersAndGroupsql, groupid, userid, isinmoregroup));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}