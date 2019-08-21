using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.DataAccess;
using Yawei.SupportCore;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// DbTreeJson 的摘要说明
    /// </summary>
    public class DbTreeJson : IHttpHandler
    {
        protected Database database = DatabaseFactory.CreateDatabase();
        protected string treeJson = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            treeJson = DatabaseMng.GetTreeJson();
            context.Response.Clear();
            context.Response.Write(treeJson);
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