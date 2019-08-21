using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WebApp.Project.StartProject;
using Yawei.DataAccess;

namespace WebApp.TZ_XMJS
{
    /// <summary>
    /// XMJSHandler 的摘要说明
    /// </summary>
    public class XMJSHandler : IHttpHandler
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
            else if (action == "changesuccess")
            {
                successChange(context);
            }
            else if (action == "changereturn")
            {
                returnChange(context);
            }
            else if (action == "commitchange")
            {
                commitChange(context);
            }
            else if (action == "change")
            {
                string guid = context.Request.Params["guid"];
                string xmguid = context.Request.Params["xmguid"];
                string dqjd = context.Request.Params["dqjd"];

                string bgsj = context.Request.Params["bgsj"];
                string bglx = context.Request.Params["bglx"];
                string bgyy = context.Request.Params["bgyy"];
                string bgyy_qt = context.Request.Params["bgyy_qt"];
                string beizhu = context.Request.Params["beizhu"];

                string last_val = context.Request.Params["last_val"];
                string bgr = context.Request.Params["bgr"];
                string bgrguid = context.Request.Params["bgrguid"];
                string bgbm = context.Request.Params["bgbm"];
                string bgsm = context.Request.Params["bgsm"];
                string bgnr = context.Request.Params["bgnr"]; //为附件时 为对应的变更数据guid

                if (bglx == "建设方案" || bglx == "项目申报表" || bglx == "云资源使用申请表")
                {
                    string newGuid = guid;//使用新建页面文件对应的refguid替换为正式的refguid

                    string oldGuid = xmguid;

                    string midGuid = Guid.NewGuid().ToString();
                    string filesign = "tz_Project_Fa";
                    last_val = midGuid;

                    if (bglx == "建设方案")
                    {
                        filesign = "tz_Project_Fa";

                    }
                    else if (bglx == "项目申报表")
                    {
                        filesign = "tz_Project_Sbb";
                    }
                    else if (bglx == "云资源使用申请表")
                    {
                        filesign = "tz_Project_Yzy";
                    }

                    DataSet dsFile = database.ExecuteDataSet("select guid from sys_fileinfo where refguid='" + oldGuid + "' and filesign='" + filesign + "'");

                    for (int fileindex = 0; fileindex < dsFile.Tables[0].Rows.Count; fileindex++)
                    {
                        string newfileguid = System.Guid.NewGuid().ToString();
                        string oldfileguid = dsFile.Tables[0].Rows[fileindex]["guid"].ToString();
                        string baksql = "	insert into Sys_FileInfo(guid,refguid,OrginFileName,NewFileName,ExtName,FileSize,PhysicsPath,FileType,FileDesp,FileSign,Type,UploadDate,ProjGuid,SysStatus) select '" + newfileguid + "','" + midGuid + "',OrginFileName,NewFileName,ExtName,FileSize,PhysicsPath,FileType,FileDesp,FileSign,Type,UploadDate,ProjGuid,SysStatus from Sys_FileInfo where guid='" + oldfileguid + "';";

                        baksql += "	insert into Sys_FileBlob select '" + newfileguid + "','" + midGuid + "',content,neworold from Sys_FileBlob where guid='" + oldfileguid + "' ;";

                        database.ExecuteNonQuery(baksql);
                    }
                    last_val = midGuid;
                }

                //保存变更信息  状态为0
                string insertsql = "insert into tz_xmgz(guid,xmguid,dqjd,bgsj,bglx,bgnr,bgyy,bgyy_qt,beizhu,last_val,bgr,bgrguid,bgbm,bgsm,updatetime,sysstatus,createdate,status) values('" + guid + "','" + xmguid + "','" + dqjd + "','" + bgsj + "','" + bglx + "','" + bgnr + "','" + bgyy + "','" + bgyy_qt + "','" + beizhu + "','" + last_val + "','" + bgr + "','" + bgrguid + "','" + bgbm + "','" + bgsm + "',getdate(),'0',getdate(),'0')";
                int result = database.ExecuteNonQuery(insertsql);

                context.Response.Write(result);
            }
        }

        private void deleteItem(HttpContext context)
        {
            string ids = context.Request.Params["ids"] ?? "";

            int totalRow = 0;
            string updatesql = "update " + tableName + " set sysstatus=-1  where guid in (" + ids.Substring(0, ids.Length - 1) + ")";
            if (tableName == "tz_xmgz")
            {
                updatesql += " and (status='0' or status='2')";
            }
            try
            {
                totalRow += database.ExecuteNonQuery(updatesql);
            }
            catch
            {

            }
            context.Response.Write(totalRow);
            context.Response.End();
        }

        private void successChange(HttpContext context)
        {
            Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            JavaScriptSerializer jsonSerial = new JavaScriptSerializer();

            ResultStatus rs = new ResultStatus();
            rs.IsSucess = "0";
            rs.ErrorInfo = "";
            string changeid = context.Request.Params["changeguid"] ?? "";
            string info = context.Request.Params["info"] ?? "";

            try
            {
                string userguid = context.Request.Params["userguid"] ?? "";
                string bmguid = context.Request.Params["depguid"] ?? "";


                int commitCount = 0;

                string sql = " insert into tz_changehistory(Guid,changeguid,changeaction,InfoData,CreateDate,CreateUserGuid,CreateDepGuid) values(newid(),@changeguid,@changeaction,@InfoData,getdate(),@CreateUserGuid,@CreateDepGuid)";
                var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
                db.AddInParameter(dbcmd, "@changeguid", System.Data.DbType.String, changeid);
                db.AddInParameter(dbcmd, "@changeaction", System.Data.DbType.String, "审核");
                db.AddInParameter(dbcmd, "@InfoData", System.Data.DbType.String, info);
                db.AddInParameter(dbcmd, "@CreateUserGuid", System.Data.DbType.String, userguid);
                db.AddInParameter(dbcmd, "@CreateDepGuid", System.Data.DbType.String, bmguid);
                db.ExecuteNonQuery(dbcmd);

                //只能提交未提交的或者退回的变更
                string sql2 = "";

                sql2 = " update tz_xmgz set status='3' where  guid='" + changeid + "'";

                //获取成功提交的项目数量
                commitCount += db.ExecuteNonQuery(sql2);
                rs.IsSucess = "1";
                rs.Data = commitCount + "";
                //将变更同步到项目数据
                saveChange(changeid);

            }
            catch (Exception ex)
            {
                rs.IsSucess = "0";
                rs.ErrorInfo = context.Server.HtmlDecode(ex.Message + "\r\n" + ex.StackTrace);
                rs.Data = "0";
            }
            string retinfo = jsonSerial.Serialize(rs);
            context.Response.Write(retinfo);
        }


        private void saveChange(string guid)
        {
            Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet("select guid,xmguid,bglx,bgnr,bgyy,last_val from tz_xmgz where guid='" + guid + "'");
            if (ds.Tables==null||ds.Tables.Count <= 0)
            {
                return;
            }

            if (ds.Tables[0].Rows.Count <= 0)
            {
                return;
            }
            DataRow row = ds.Tables[0].Rows[0];


            string xmguid = row["xmguid"].ToString();

            string bglx = row["bglx"].ToString();
            string bgnr = row["bgnr"].ToString(); //为附件时refguid 为对应的变更数据guid
            string bgyy = row["bgyy"].ToString();
            string last_val = row["last_val"].ToString();

            //变更附件
            if (bglx == "建设方案" || bglx == "项目申报表" || bglx == "云资源使用申请表")
            {
                string filesign = "tz_Project_Fa";

                if (bglx == "建设方案")
                {
                    filesign = "tz_Project_Fa";

                }
                else if (bglx == "项目申报表")
                {
                    filesign = "tz_Project_Sbb";
                }
                else if (bglx == "云资源使用申请表")
                {
                    filesign = "tz_Project_Yzy";
                }

                db.ExecuteNonQuery("delete from Sys_FileBlob where guid in (select guid from Sys_FileInfo where fileSign='" + filesign + "' and refguid='" + xmguid + "');");
                //删除以前的附件数据
                db.ExecuteNonQuery("delete from Sys_FileInfo where fileSign='" + filesign + "' and refguid='" + xmguid + "'");


                DataSet dsFile = db.ExecuteDataSet("select guid from sys_fileinfo where filesign='" + filesign + "' and refguid='" + bgnr + "'");
                for (int fileindex = 0; fileindex<dsFile.Tables[0].Rows.Count; fileindex++)
                {
                    string fileguid = dsFile.Tables[0].Rows[fileindex]["guid"].ToString();
                    string newfileguid = System.Guid.NewGuid().ToString();
                    string backFileInfo = "	insert into Sys_FileBlob select '" + newfileguid + "','" + xmguid + "',content,neworold from Sys_FileBlob where guid='" + fileguid + "';";

                    backFileInfo += "	insert into Sys_FileInfo(guid,refguid,OrginFileName,NewFileName,ExtName,FileSize,PhysicsPath,FileType,FileDesp,FileSign,Type,UploadDate,ProjGuid,SysStatus) select '" + newfileguid + "','" + xmguid + "',OrginFileName,NewFileName,ExtName,FileSize,PhysicsPath,FileType,FileDesp,FileSign,Type,UploadDate,ProjGuid,SysStatus from Sys_FileInfo where guid='" + fileguid + "';";

                    database.ExecuteNonQuery(backFileInfo);
                }
            }
            else
            {
                string changesql = "";
                if (bglx == "项目名称")
                {
                    changesql = "proname" + "='" + bgnr + "'";
                }
                else if (bglx == "项目简介")
                {
                    changesql = "prosummary" + "='" + bgnr + "'";
                }
                else if (bglx == "预估金额")
                {
                    changesql = "quota" + "='" + bgnr + "'";
                }
                else if (bglx == "资金来源")
                {
                    changesql = "moneysource" + "='" + bgnr + "'";
                }
                else if (bglx == "项目属性")
                {
                    changesql = "proproperty" + "='" + bgnr + "'";
                }
                else if (bglx == "项目类型")
                {
                    changesql = "protype" + "='" + bgnr + "'";
                }
                else if (bglx == "申报时间")
                {
                    changesql = "startDate" + "='" + bgnr + "'";
                }
                else if (bglx == "联系方式")
                {
                    changesql = "contactname" + "='" + bgnr.Split(':')[0] + "',contacttel='" + bgnr.Split(':')[1];

                    //changesql=""
                }
                else if (bglx == "是否部署云平台")
                {
                    changesql = "isincloudplat" + "='" + bgnr + "'";
                }
                string updateProSql = "update tz_Project set " + changesql + " where proguid='" + xmguid + "'";
                database.ExecuteNonQuery(updateProSql);

            }
        }


        private void returnChange(HttpContext context)
        {
            Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            JavaScriptSerializer jsonSerial = new JavaScriptSerializer();

            ResultStatus rs = new ResultStatus();
            rs.IsSucess = "0";
            rs.ErrorInfo = "";
            string changeid = context.Request.Params["changeguid"] ?? "";
            string info = context.Request.Params["info"] ?? "";

            try
            {
                string userguid = context.Request.Params["userguid"] ?? "";
                string bmguid = context.Request.Params["depguid"] ?? "";

  
                int commitCount = 0;

                    string sql = " insert into tz_changehistory(Guid,changeguid,changeaction,InfoData,CreateDate,CreateUserGuid,CreateDepGuid) values(newid(),@changeguid,@changeaction,@InfoData,getdate(),@CreateUserGuid,@CreateDepGuid)";
                    var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
                    db.AddInParameter(dbcmd, "@changeguid", System.Data.DbType.String, changeid);
                    db.AddInParameter(dbcmd, "@changeaction", System.Data.DbType.String, "退回");
                    db.AddInParameter(dbcmd, "@InfoData", System.Data.DbType.String, info);
                    db.AddInParameter(dbcmd, "@CreateUserGuid", System.Data.DbType.String, userguid);
                    db.AddInParameter(dbcmd, "@CreateDepGuid", System.Data.DbType.String, bmguid);
                    db.ExecuteNonQuery(dbcmd);

                    string sql2 = "";

                    sql2 = " update tz_xmgz set status='2' where  guid='" + changeid + "'";

                    //获取成功提交的项目数量
                    commitCount += db.ExecuteNonQuery(sql2);
                rs.IsSucess = "1";
                rs.Data = commitCount + "";
            }
            catch (Exception ex)
            {
                rs.IsSucess = "0";
                rs.ErrorInfo = context.Server.HtmlDecode(ex.Message + "\r\n" + ex.StackTrace);
                rs.Data = "0";
            }
            string retinfo = jsonSerial.Serialize(rs);
            context.Response.Write(retinfo);

        }

        private void commitChange(HttpContext context)
        {
            Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            JavaScriptSerializer jsonSerial = new JavaScriptSerializer();
            ResultStatus rs = new ResultStatus();
            rs.IsSucess = "0";
            rs.ErrorInfo = "";
            string proGuid = context.Request.Params["changeids"] ?? "";

            try
            {
                string userguid = context.Request.Params["userguid"] ?? "";
                string bmguid = context.Request.Params["bmguid"] ?? "";
                //tz_projecthistory
                if (proGuid != "")
                {
                    proGuid = proGuid.Substring(0, proGuid.Length - 1);
                }
                string[] id = proGuid.Split(',');
                int commitCount = 0;
                for (int i = 0; i < id.Length; i++)
                {
                    //只能提交未提交的或者退回的变更
                    string sql2 = "";

                    sql2 = " update tz_xmgz set status='1' where  guid=" + id[i] + " and (status='0' or status='2')";
                    int result = db.ExecuteNonQuery(sql2);
                    if (result != 0)
                    {
                        string sql = " insert into tz_changehistory(Guid,changeguid,changeaction,InfoData,CreateDate,CreateUserGuid,CreateDepGuid) values(newid(),@changeguid,@changeaction,@InfoData,getdate(),@CreateUserGuid,@CreateDepGuid)";
                        var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
                        db.AddInParameter(dbcmd, "@changeguid", System.Data.DbType.String, proGuid.Replace("'", ""));
                        db.AddInParameter(dbcmd, "@changeaction", System.Data.DbType.String, "提交");
                        db.AddInParameter(dbcmd, "@InfoData", System.Data.DbType.String, "提交");
                        db.AddInParameter(dbcmd, "@CreateUserGuid", System.Data.DbType.String, userguid);
                        db.AddInParameter(dbcmd, "@CreateDepGuid", System.Data.DbType.String, bmguid);
                        db.ExecuteNonQuery(dbcmd);

                    }
                    //获取成功提交的项目数量
                    commitCount += result;
                }
               

                rs.IsSucess = "1";
                rs.Data = commitCount + "";
            }
            catch (Exception ex)
            {
                rs.IsSucess = "0";
                rs.ErrorInfo = context.Server.HtmlDecode(ex.Message + "\r\n" + ex.StackTrace);
                rs.Data = "0";
            }
            string retinfo = jsonSerial.Serialize(rs);
            context.Response.Write(retinfo);
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