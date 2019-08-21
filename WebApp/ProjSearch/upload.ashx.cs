using SupportLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.ProjSearch
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型

                //WriteLogs(files.Count+"file长度");
                HttpPostedFile file1 = context.Request.Files["file1"];

                String guidStr = Guid.NewGuid().ToString();
                String fileName = guidStr + ".xls";
                //String fileName =  file1.FileName;
                //WriteLogs(files.Count + "savaas qian");
                try
                {
                    //file1.SaveAs(ConfigurationManager.AppSettings["excelpath"] + "\\" + fileName);
                    file1.SaveAs(ConfigurationManager.AppSettings["excelpath"] + "\\" + fileName);
                }
                catch (Exception e)
                {
                    //WriteLogs(e.Message);
                    //WriteLogs(e.InnerException.ToString());
                    //WriteLogs(e.Source);
                }
                
                
                try
                {
                    String sql = ExportHelper.ExcelToDataTable(ConfigurationManager.AppSettings["excelpath"] + "\\" + fileName);

                    Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();

                    if (db.ExecuteNonQuery(sql) >= 0)
                    {
                        context.Response.Write("0");
                    }
                }
                catch (Exception e)
                {
                    //WriteLogs("upload异常"+ e.Message);
                    context.Response.Write(e.Message);
                }

            }
            else
            {
            }
        }

        //public static void WriteLogs(string content)
        //{
        //    string path = "C:";

        //    path = "C:\\1.txt";

        //            StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
        //            sw.WriteLine(content);
        //            //  sw.WriteLine("----------------------------------------");
        //            sw.Close();
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}