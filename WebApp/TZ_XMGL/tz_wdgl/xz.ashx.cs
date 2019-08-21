using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Yawei.DataAccess;
using Yawei.SupportCore;

namespace WebApp.TZ_XMGL.tz_wdgl
{
    /// <summary>
    /// xz 的摘要说明
    /// </summary>
    public class xz : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SysLogCore log = new SysLogCore();
            Database filesDb = DatabaseFactory.CreateDatabase("Support");
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            string type = context.Request["type"] != null ? context.Request["type"] : "";
            string guid = context.Request["Guid"] != null ? context.Request["Guid"] : "";
            if (type == "download")
            {

                string sqlstr = "select * from sys_fileblob where Guid='" + guid + "'";
                string sqlname = "select OrginFileName from sys_fileinfo where Guid='" + guid + "'";
                DataTable dt = filesDb.ExecuteDataSet(sqlstr).Tables[0];
                string name = filesDb.ExecuteScalar(sqlname).ToString();
                byte[] content = (byte[])dt.Rows[0]["Content"];
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" + context.Server.UrlEncode(name));
                context.Response.BinaryWrite(content);
                context.Response.Flush();
                context.Response.End();
            }
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