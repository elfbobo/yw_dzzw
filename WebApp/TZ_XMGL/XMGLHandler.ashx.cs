using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace WebApp.TZ_XMGL.tz_xmlx
{
    /// <summary>
    /// XMGLHandler 的摘要说明
    /// </summary>
    public class XMGLHandler : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();

        private string tableName = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"] ?? "";
            if (action == "deleteitem")
            {
                tableName = context.Request.Params["tablename"] ?? "";
                deleteItem(context);
            }
        }

        private void deleteItem(HttpContext context)
        {
            string ids = context.Request.Params["ids"] ?? "";

            int totalRow = 0;
            try
            {
                totalRow += database.ExecuteNonQuery("delete from " + tableName + "  where guid in (" + ids.Substring(0, ids.Length - 1) + ")");
            }
            catch
            {

            }
            context.Response.Write(totalRow);
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