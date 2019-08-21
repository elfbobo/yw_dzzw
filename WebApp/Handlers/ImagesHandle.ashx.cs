using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Yawei.FacadeCore;

namespace Yawei.App.Handlers
{
    /// <summary>
    /// ImagesHandle 的摘要说明
    /// </summary>
    public class ImagesHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ProjGuid = context.Request.QueryString["ProjGuid"];
            string PageNum = context.Request.QueryString["PageNum"];
            string Condition = context.Request.QueryString["Condition"];
            byte[] imagebytes = null;

            DataSet ds = new DataSet();

            if (Condition == null)
                ds = ImagesShowFacade.GetImagesContent(ProjGuid, Convert.ToInt32(PageNum));
            else
            {
                if (Condition.Contains(','))
                {
                    string[] list = Condition.Split(',');
                    Condition = " ReportYear='" + list[0] + "' and ReportMonth='" + list[1] + "'";
                }
                else
                {
                    string year = Condition;
                    Condition = " ReportYear='" + year + "'";
                }
                ds = ImagesShowFacade.GetImagesContent(ProjGuid, Condition, Convert.ToInt32(PageNum));
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                imagebytes = (byte[])ds.Tables[0].Rows[0]["Content"];
                if (imagebytes == null) return;
                MemoryStream memstream = new MemoryStream(imagebytes);
                Bitmap bitmap = (Bitmap)Bitmap.FromStream(memstream);
                context.Response.ContentType = "image/jpeg";
                context.Response.Clear();

                context.Response.BufferOutput = true;
                bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                bitmap.Dispose();
                memstream.Dispose();
                context.Response.Flush();
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