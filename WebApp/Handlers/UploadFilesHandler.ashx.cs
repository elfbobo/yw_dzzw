using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Yawei.SupportCore;
using Yawei.DataAccess;

namespace Yawei.App.Handlers
{
    /// <summary>
    /// UploadFilesHandler 的摘要说明
    /// </summary>
    public class UploadFilesHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                context.Request.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

                #region 接收数据
                string refGuid = context.Request["RefGuid"] != null ? context.Request["RefGuid"] : "";
                string type = context.Request["Type"] != null ? context.Request["Type"] : "database";
                string fileType = context.Request["FileType"] != null ? context.Request["FileType"] : "document";
                string FileSign = context.Request["FileSign"] != null ? context.Request["FileSign"] : "";
                string extend = context.Request["extend"] != null ? context.Request["extend"] : "";
                HttpPostedFile file = context.Request.Files["Filedata"];
                #endregion


                #region 处理信息

                if (type == "server")
                {
                    #region 上传到服务器

                    object obj = "/Content/Temp";
                    if (obj == null) throw new Exception("没有在配置管理中配置服务器存储目录，配置分组为：系统配置，配置节点为：FilesServer");

                    string filePath = context.Server.MapPath(obj.ToString());

                    if (file != null)
                    {
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        int extIndex = file.FileName.LastIndexOf('.');
                        string fileName = file.FileName.Substring(0, extIndex);
                        string extName = file.FileName.Substring(extIndex);
                        int length = file.ContentLength;
                        DateTime uploadTime = DateTime.Now;
                        string orginFileName = fileName + extName;


                        if (!string.IsNullOrEmpty(extend) && !extend.ToLower().Contains(extName.ToLower()))
                        {
                            context.Response.Write("false");
                        }
                        else
                        {
                            string strGuid = Guid.NewGuid().ToString();
                            string newfileName = strGuid + extName;
                            file.SaveAs(filePath + "\\" + newfileName);

                            #region 存储信息

                            DataSet doc = SysFileCore.GetModel();
                            DataRow dr = doc.Tables[0].NewRow();

                            dr["Guid"] = strGuid;
                            dr["RefGuid"] = refGuid;
                            dr["FileType"] = fileType;
                            dr["FileSign"] = FileSign;
                            dr["OrginFileName"] = orginFileName;
                            dr["NewFileName"] = newfileName;
                            dr["ExtName"] = extName;
                            dr["FileSize"] = length;
                            dr["PhysicsPath"] = filePath + "\\" + newfileName;
                            dr["UploadDate"] = uploadTime;
                            dr["Type"] = "server";
                            doc.Tables[0].Rows.Add(dr);
                            SysFileCore.Update(doc);

                            #endregion

                            string json = "{Guid:'" + strGuid + "',OrginFileName:'" + file.FileName + "'";
                            json += ",FileExtName:'" + extName + "',FileSize:'" + length + "',FileNewName:'" + newfileName + "'}";

                            context.Response.Write(json);
                        }
                    }
                    else
                    {
                        context.Response.Write("0");
                    }
                    #endregion
                }
                else if (type == "database")
                {
                    #region 上传到数据库

                    if (file != null)
                    {
                        int extIndex = file.FileName.LastIndexOf('.');
                        string fileName = file.FileName.Substring(0, extIndex);
                        string extName = file.FileName.Substring(extIndex);
                        int length = file.ContentLength;
                        DateTime uploadTime = DateTime.Now;
                        string orginFileName = fileName + extName;
                        string newfileName = fileName + uploadTime.ToFileTime().ToString() + extName;

                        if (!string.IsNullOrEmpty(extend) && !extend.ToLower().Contains(extName.ToLower()))
                        {
                            context.Response.Write("false");
                        }
                        else
                        {

                            #region 存储信息

                            DataSet doc = SysFileCore.GetModel();
                            string strGuid = Guid.NewGuid().ToString();
                            DataRow dr = doc.Tables[0].NewRow();
                            dr["Guid"] = strGuid;
                            dr["RefGuid"] = refGuid;
                            dr["FileType"] = fileType;
                            dr["FileSign"] = FileSign;
                            dr["OrginFileName"] = orginFileName;
                            dr["NewFileName"] = newfileName;
                            dr["ExtName"] = extName;
                            dr["FileSize"] = length;
                            dr["PhysicsPath"] = "Sys_FileBlob";
                            dr["UploadDate"] = uploadTime;
                            dr["Type"] = "database";
                            doc.Tables[0].Rows.Add(dr);

                            dr = doc.Tables[1].NewRow();
                            dr["Guid"] = strGuid;
                            dr["RefGuid"] = refGuid;
                            byte[] fileContent = new byte[length];
                            Stream fileStream = file.InputStream;
                            fileStream.Read(fileContent, 0, length);
                            dr["Content"] = fileContent;
                            doc.Tables[1].Rows.Add(dr);
                            SysFileCore.Update(doc);

                            #endregion

                            #region 构造返回XML



                            string json = "{Guid:'" + strGuid + "',OrginFileName:'" + file.FileName + "'";
                            json += ",FileExtName:'" + extName + "',FileSize:'" + length + "'}";

                            #endregion

                            context.Response.Write(context.Server.UrlEncode(json));
                        }
                    }
                    else
                    {
                        context.Response.Write("0");
                    }
                    #endregion
                }
                else if (type == "delete")
                {
                    SysFileCore.DeleteFile(context.Request["guid"]);
                }
                else if (type == "get")
                {
                    string json = string.Empty;
                    if (refGuid == "")
                    {
                        json = "[]";
                        context.Response.Write(json);
                    }
                    else
                    {
                        DataSet ds = null;
                        if (FileSign != "")
                            ds = SysFileCore.GetInforByRefGuid(refGuid, FileSign);
                        else
                            ds = SysFileCore.GetInforByRefGuid(refGuid);
                       
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (i > 0)
                                    json += ",";
                                json += "{Guid:'" + ds.Tables[0].Rows[i]["Guid"] + "',OrginFileName:'" + ds.Tables[0].Rows[i]["OrginFileName"].ToString().Replace("%", "％") + "'";
                                json += ",FileExtName:'" + ds.Tables[0].Rows[i]["ExtName"] + "',FileSize:'" + ds.Tables[0].Rows[i]["FileSize"] + "'}";
                            }
                        }
                        json = "[" + json + "]";
                        context.Response.Write(json);
                    }
                }
                else if (type == "download")
                {
                    string guid = context.Request["guid"];
                    DataSet doc = SysFileCore.GetInfor(guid);
                    if (doc.Tables[0].Rows.Count > 0)
                    {
                        type = doc.Tables[0].Rows[0]["Type"].ToString();
                        if (type == "database")
                        {
                            string fileName = doc.Tables[0].Rows[0]["OrginFileName"].ToString();
                            DataSet ds = SysFileCore.GetBlob(guid);
                            byte[] content = (byte[])ds.Tables[0].Rows[0]["Content"];
                            context.Response.Clear();
                            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + context.Server.UrlEncode(fileName));
                            context.Response.BinaryWrite(content);
                            context.Response.Flush();
                            context.Response.End();
                        }
                        else if (type == "server")
                        {
                            string fileName = doc.Tables[0].Rows[0]["OrginFileName"].ToString();
                            string physicsPath = doc.Tables[0].Rows[0]["PhysicsPath"].ToString();
                            if (System.IO.File.Exists(physicsPath))
                            {
                                FileStream fs = new FileStream(physicsPath, FileMode.Open, FileAccess.Read);
                                byte[] content = new byte[fs.Length];
                                fs.Read(content, 0, (int)fs.Length);
                                context.Response.Clear();
                                context.Response.AddHeader("Content-Disposition", "attachment;filename=" + context.Server.UrlEncode(fileName));
                                context.Response.BinaryWrite(content);
                                context.Response.Flush();
                                context.Response.End();
                            }
                        }
                        else
                        {
                            context.Response.Write("<script language='JavaScript'>alert('附件无法下载，请与管理员联系');</script>");
                        }
                    }
                    else
                    {
                        context.Response.Write("<script language='JavaScript'>alert('找不到附件，请与管理员联系');</script>");
                    }
                }
                else
                {
                    context.Response.Write("0");
                }

                #endregion
            }
            catch (Exception ex)
            {
                Database db = DatabaseFactory.CreateDatabase();
                db.ExecuteNonQuery("insert into Sys_ErrorLog values (newid(),'" + ex.Message + "','','','',getdate())");
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