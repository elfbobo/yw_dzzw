using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApp.XM_ZFTZ.Project.StartProject
{
    /// <summary>
    /// ZftzWebFunction 的摘要说明
    /// </summary>
    public class ZftzWebFunction : IHttpHandler
    {

        JavaScriptSerializer jsonSerial = new JavaScriptSerializer();
        Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request.Params["action"] ?? "";
            string proGuid = context.Request.Params["proguid"] ?? "";

            //退回申请
            if (action == "return")
            {
                ResultStatus rs = new ResultStatus();
                rs.IsSucess = "0";
                rs.ErrorInfo = "";
                int returnCount = 0;
                try
                {
                    string returninfo = context.Request.Params["returninfo"] ?? "";
                    string userguid = context.Request.Params["userguid"] ?? "";
                    string depguid = context.Request.Params["depguid"] ?? "";
                    string returnguid = context.Request.Params["returnguid"] ?? "";
                    //string username = context.Request.Params["username"] ?? "";
                    //tz_projecthistory
                    //tz_projecthistory

                    string sql = " insert into tz_projecthistory(Guid,ProGuid,ProAction,InfoData,CreateDate,CreateUserGuid,CreateDepGuid,PsFile) values(@Guid,@ProGuid,@ProAction,@InfoData,getdate(),@CreateUserGuid,@CreateDepGuid,@PsFile)";
                    var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
                    db.AddInParameter(dbcmd, "@Guid", System.Data.DbType.String, returnguid);
                    db.AddInParameter(dbcmd, "@ProGuid", System.Data.DbType.String, proGuid);
                    db.AddInParameter(dbcmd, "@ProAction", System.Data.DbType.String, "退回");
                    db.AddInParameter(dbcmd, "@InfoData", System.Data.DbType.String, returninfo);
                    db.AddInParameter(dbcmd, "@CreateUserGuid", System.Data.DbType.String, userguid);
                    db.AddInParameter(dbcmd, "@CreateDepGuid", System.Data.DbType.String, depguid);
                    db.AddInParameter(dbcmd, "@PsFile", System.Data.DbType.String, returnguid);
                    db.ExecuteNonQuery(dbcmd);


                    //string sql2 = " update tz_Project set ProState='' where ProGuid='" + proGuid + "'";
                    string sql2 = " update tz_Project set ProState='退回' where ProState='提交' and ProGuid='" + proGuid + "'";
                    returnCount = db.ExecuteNonQuery(sql2);
                    rs.IsSucess = "1";
                    rs.Data = returnCount + "";
                }
                catch (Exception ex)
                {
                    rs.IsSucess = "0";
                    rs.Data = "0";
                    rs.ErrorInfo = context.Server.HtmlDecode(ex.Message + "\r\n" + ex.StackTrace);
                }
                string retinfo = jsonSerial.Serialize(rs);
                context.Response.Write(retinfo);
            }
            else if (action == "merge")
            {
                handlePro(context, "整合");
            }
            else if (action == "hold")
            {
                handlePro(context, "暂缓");
            }
            //申报
            else if (action == "success")
            {
                ResultStatus rs = new ResultStatus();
                rs.IsSucess = "0";
                rs.ErrorInfo = "";
                try
                {
                    string successinfo = context.Request.Params["successinfo"] ?? "";
                    string userguid = context.Request.Params["userguid"] ?? "";
                    string depguid = context.Request.Params["depguid"] ?? "";
                    string successguid = context.Request.Params["successguid"] ?? "";

                    string successgse = context.Request.Params["successgse"] ?? "";

                    string gse = context.Request.Params["successgse"] ?? "";

                    //string username = context.Request.Params["username"] ?? "";
                    //tz_projecthistory
                    //tz_projecthistory

                    string sql = " insert into tz_projecthistory(Guid,ProGuid,ProAction,InfoData,CreateDate,CreateUserGuid,CreateDepGuid,PsFile) values(@Guid,@ProGuid,@ProAction,@InfoData,getdate(),@CreateUserGuid,@CreateDepGuid,@PsFile)";
                    var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
                    db.AddInParameter(dbcmd, "@Guid", System.Data.DbType.String, successguid);
                    db.AddInParameter(dbcmd, "@ProGuid", System.Data.DbType.String, proGuid);
                    db.AddInParameter(dbcmd, "@ProAction", System.Data.DbType.String, "申报");
                    db.AddInParameter(dbcmd, "@InfoData", System.Data.DbType.String, successinfo);
                    db.AddInParameter(dbcmd, "@CreateUserGuid", System.Data.DbType.String, userguid);
                    db.AddInParameter(dbcmd, "@CreateDepGuid", System.Data.DbType.String, depguid);
                    db.AddInParameter(dbcmd, "@PsFile", System.Data.DbType.String, successguid);
                    db.ExecuteNonQuery(dbcmd);


                    string sqlje = " insert into tz_psje(psguid,je) values(@PsGuid,@Je)";
                    var dbcmd2 = db.CreateCommand(System.Data.CommandType.Text, sqlje);

                    db.AddInParameter(dbcmd2, "@PsGuid", System.Data.DbType.String, successguid);
                    db.AddInParameter(dbcmd2, "@Je", System.Data.DbType.String, gse);
                    db.ExecuteNonQuery(dbcmd2);


                    string sql2 = " update tz_Project set ProState='申报' where ProState='提交' and ProGuid='" + proGuid + "'";
                    int successCount = db.ExecuteNonQuery(sql2);
                    rs.IsSucess = "1";
                    rs.Data = successCount + "";
                    //string operatorhistory = "insert into tz_OperatorHistory values('" + proGuid + "','" + proGuid + "','" + userguid + "','" + depguid + "','申报',getdate());";

                    //db.ExecuteNonQuery(operatorhistory);

                }
                catch (Exception ex)
                {
                    rs.IsSucess = "0";
                    rs.ErrorInfo = context.Server.HtmlDecode(ex.Message + "\r\n" + ex.StackTrace);
                }
                string retinfo = jsonSerial.Serialize(rs);
                context.Response.Write(retinfo);
            }
            else if (action == "delete")
            {
                ResultStatus rs = new ResultStatus();
                rs.IsSucess = "0";
                rs.ErrorInfo = "";
                try
                {
                    string userguid = context.Request.Params["userguid"] ?? "";
                    string username = context.Request.Params["username"] ?? "";
                    //tz_projecthistory
                    if (proGuid != "")
                    {
                        proGuid = proGuid.Substring(0, proGuid.Length - 1);
                    }
                    string[] id = proGuid.Split(',');
                    string sql = " insert into tz_zftz_ProjectHistory(ProGuid,ProAction,InfoData,CreateDate,CreateUserGuid,CreateUserName) values(@ProGuid,@ProAction,@InfoData,getdate(),@CreateUserGuid,@CreateUserName)";
                    var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
                    db.AddInParameter(dbcmd, "@ProGuid", System.Data.DbType.String, proGuid.Replace("'", ""));
                    db.AddInParameter(dbcmd, "@ProAction", System.Data.DbType.String, "删除");
                    db.AddInParameter(dbcmd, "@InfoData", System.Data.DbType.String, "删除");
                    db.AddInParameter(dbcmd, "@CreateUserGuid", System.Data.DbType.String, userguid);
                    db.AddInParameter(dbcmd, "@CreateUserName", System.Data.DbType.String, username);
                    //db.ExecuteNonQuery(dbcmd);
                    string sql2 = " update tz_zftz_Project set sysstatus='-1' where ProGuid in(" + proGuid + ")";
                    int deleteCount = db.ExecuteNonQuery(sql2);
                    rs.IsSucess = "1";
                    rs.Data = deleteCount + "";

                    StringBuilder sb = new StringBuilder();
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
            //提交项目
            else if (action == "commit")
            {
                commitPro(context);
            }
            else if (action == "cqzj")
            {
                ResultStatus rs = new ResultStatus();
                rs.IsSucess = "0";
                rs.ErrorInfo = "";
                int maxnum = 0;
                if (int.TryParse(context.Request.Params["maxnum"] ?? "", out maxnum))
                {
                    rs.Data = zjcqAction(maxnum);
                }
                else
                {
                    rs.ErrorInfo = "参数错误";
                }
                string retinfo = jsonSerial.Serialize(rs);
                context.Response.Write(retinfo);
            }
            else if (action == "savezjs")
            {
                ResultStatus rs = new ResultStatus();
                rs.IsSucess = "0";
                rs.ErrorInfo = "";
                string guids = context.Request.Params["guids"] ?? "";
                string proguid = context.Request.Params["proguid"] ?? "";
                string[] guidss = guids.Split('$');
                string insertsql = " insert into tz_ProjectAndExpert(ProGuid,ExpertGuid,Guid) values(@ProGuid,@ExpertGuid,@Guid);";
                try
                {
                    using (var cmd = db.CreateCommand(CommandType.Text, insertsql))
                    {
                        cmd.Connection = db.CreateConnection();
                        cmd.Connection.Open();
                        db.ExecuteNonQuery(" delete tz_ProjectAndExpert where ProGuid='" + proGuid + "' ");
                        foreach (string s in guidss)
                        {
                            if (s.Trim().Length > 0)
                            {
                                cmd.Parameters.Clear();
                                db.AddInParameter(cmd, "ProGuid", DbType.String, proguid);
                                db.AddInParameter(cmd, "ExpertGuid", DbType.String, s);
                                db.AddInParameter(cmd, "Guid", DbType.String, Guid.NewGuid().ToString());
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    rs.IsSucess = "1";
                }
                catch (Exception ex)
                {
                    rs.IsSucess = "0";
                    rs.ErrorInfo = context.Server.HtmlDecode(ex.Message + "\r\n" + ex.StackTrace);
                }
                string retinfo = jsonSerial.Serialize(rs);
                context.Response.Write(retinfo);
            }
            //项目名称校验
            else if (action == "namevalid")
            {
                string projname = context.Request.Params["proname"] ?? "";
                string proguid = context.Request.Params["proguid"] ?? "";
                if (projname != "")
                {
                    string querysql = "select count(*) as procount from tz_zftz_Project where sysstatus!=-1 and  proguid!='" + proguid + "' and  proname='" + projname + "'";
                    DataSet ds = db.ExecuteDataSet(querysql);
                    int projcount = Convert.ToInt16(ds.Tables[0].Rows[0]["procount"].ToString());
                    //无重复
                    if (projcount == 0)
                    {
                        context.Response.Write("0");
                    }
                    //有重复
                    else
                    {
                        context.Response.Write("1");
                    }
                }
            }
        }

        private void handlePro(HttpContext context, string handletype)
        {
            string proGuid = context.Request.Params["proguid"] ?? "";
            ResultStatus rs = new ResultStatus();
            rs.IsSucess = "0";
            rs.ErrorInfo = "";
            int returnCount = 0;
            try
            {
                string returninfo = context.Request.Params["returninfo"] ?? "";
                string userguid = context.Request.Params["userguid"] ?? "";
                string depguid = context.Request.Params["depguid"] ?? "";
                string returnguid = context.Request.Params["returnguid"] ?? "";
                //string username = context.Request.Params["username"] ?? "";
                //tz_projecthistory
                //tz_projecthistory

                string sql = " insert into tz_projecthistory(Guid,ProGuid,ProAction,InfoData,CreateDate,CreateUserGuid,CreateDepGuid,PsFile) values(@Guid,@ProGuid,@ProAction,@InfoData,getdate(),@CreateUserGuid,@CreateDepGuid,@PsFile)";
                var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
                db.AddInParameter(dbcmd, "@Guid", System.Data.DbType.String, returnguid);
                db.AddInParameter(dbcmd, "@ProGuid", System.Data.DbType.String, proGuid);
                db.AddInParameter(dbcmd, "@ProAction", System.Data.DbType.String, handletype);
                db.AddInParameter(dbcmd, "@InfoData", System.Data.DbType.String, returninfo);
                db.AddInParameter(dbcmd, "@CreateUserGuid", System.Data.DbType.String, userguid);
                db.AddInParameter(dbcmd, "@CreateDepGuid", System.Data.DbType.String, depguid);
                db.AddInParameter(dbcmd, "@PsFile", System.Data.DbType.String, returnguid);
                db.ExecuteNonQuery(dbcmd);


                //string sql2 = " update tz_Project set ProState='' where ProGuid='" + proGuid + "'";
                string sql2 = " update tz_Project set ProState='" + handletype + "' where ProState='提交' and ProGuid='" + proGuid + "'";
                returnCount = db.ExecuteNonQuery(sql2);
                rs.IsSucess = "1";
                rs.Data = returnCount + "";
            }
            catch (Exception ex)
            {
                rs.IsSucess = "0";
                rs.Data = "0";
                rs.ErrorInfo = context.Server.HtmlDecode(ex.Message + "\r\n" + ex.StackTrace);
            }
            string retinfo = jsonSerial.Serialize(rs);
            context.Response.Write(retinfo);
        }

        private void commitPro(HttpContext context)
        {
            ResultStatus rs = new ResultStatus();
            rs.IsSucess = "0";
            rs.ErrorInfo = "";
            string proGuid = context.Request.Params["proguid"] ?? "";

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

                string sql = " insert into tz_zftz_ProjectHistory(Guid,ProGuid,ProAction,InfoData,CreateDate,CreateUserGuid,CreateDepGuid) values(newid(),@ProGuid,@ProAction,@InfoData,getdate(),@CreateUserGuid,@CreateDepGuid)";
                var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
                db.AddInParameter(dbcmd, "@ProGuid", System.Data.DbType.String, proGuid.Replace("'", ""));
                db.AddInParameter(dbcmd, "@ProAction", System.Data.DbType.String, "提交");
                db.AddInParameter(dbcmd, "@InfoData", System.Data.DbType.String, "提交");
                db.AddInParameter(dbcmd, "@CreateUserGuid", System.Data.DbType.String, userguid);
                db.AddInParameter(dbcmd, "@CreateDepGuid", System.Data.DbType.String, bmguid);
                db.ExecuteNonQuery(dbcmd);

                //db.ExecuteNonQuery(dbcmd);
                //只能提交未提交的或者暂缓的    退回的不可以提交
                string sql2 = "";
                if (ConfigurationManager.AppSettings["verify"] == "1")
                {
                    sql2 = " update tz_zftz_Project set ProState='提交' where (ProState is null or ProState='暂缓')  and ProGuid in(" + proGuid + ")";
                }
                else
                {
                    sql2 = " update tz_zftz_Project set ProState='申报' where (ProState is null or ProState='暂缓')  and ProGuid in(" + proGuid + ")";
                }


                //获取成功提交的项目数量
                int commitCount = db.ExecuteNonQuery(sql2);

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

        /// <summary>
        /// 抽取专家
        /// </summary>
        private string zjcqAction(int maxnum)
        {
            string str = "{}";
            int maxCount = maxnum;
            //获取所有专家信息
            DataSet ds = db.ExecuteDataSet(" select * from tz_ProjectExpert ");
            DataTable dt = ds.Tables[0];
            DataTable dtSelect = dt.Clone();
            //抽取范围
            int max = ds.Tables[0].Rows.Count;
            for (int i = 0; i < maxCount; i++)
            {
                Random ran = new Random(DateTime.Now.Millisecond);
                int a = ran.Next(max);
                if (a >= dt.Rows.Count)
                {
                    break;
                }
                else
                {
                    dtSelect.Rows.Add(dt.Rows[a].ItemArray);
                    dt.Rows.RemoveAt(a);
                    dt.AcceptChanges();
                    max = dt.Rows.Count;
                }
            }

            //dtSelect 为最后抽取的数
            str = Yawei.Common.ConvertJson.ToJson(dtSelect);
            return str;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class ResultStatus
    {
        public String IsSucess
        {
            get;
            set;
        }

        public String ErrorInfo
        {
            get;
            set;
        }

        public string Data { get; set; }
    }
}