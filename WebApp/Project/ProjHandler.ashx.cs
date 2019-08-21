using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Yawei.DataAccess;

namespace WebApp.Project
{
    /// <summary>
    /// ProjHandler 的摘要说明
    /// </summary>
    public class ProjHandler : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"] ?? "";
            if (action == "changepswd")
            {
                changePswd(context);
            }
            else if (action == "filesearch")
            {
                fileSearch(context);
            }
            else if (action == "ndtj")
            {
                ndtj(context);
            }
        }

        private void ndtj(HttpContext context)
        {
            DataSet bmDs = database.ExecuteDataSet("select distinct StartDeptGuid,StartDeptName from tz_Project where sysstatus!=-1 and ProState='申报'");
            DataSet proDs = database.ExecuteDataSet("select year(startDate) as proyear,StartDeptGuid,startdeptname,proguid from tz_Project where sysstatus!=-1 and ProState='申报'");

            DataSet allDs = database.ExecuteDataSet("select year(startDate) as proyear,StartDeptGuid,startdeptname,proguid,htje from V_TZ_ProjectOverview where  ProState='申报'");
            if (proDs.Tables[0].Rows.Count == 0)
            {
                context.Response.Write("{}");
                return;
            }
            int curYear = DateTime.Now.Year;
            string yearTitle = "'" + (curYear - 2) + "','" + (curYear - 1) + "','" + curYear + "'";
            string bmTitle = "";
            string dataTitle = "";

            DataSet yearDs = database.ExecuteDataSet("select max(year(StartDate)) as maxyear,min(year(StartDate)) as minyear from tz_Project where sysstatus!=-1 and ProState='申报'");
            int maxYear = Convert.ToInt32(yearDs.Tables[0].Rows[0]["maxyear"].ToString());
            int minYear = Convert.ToInt32(yearDs.Tables[0].Rows[0]["minyear"].ToString());

            string resultJson = "{";
            for (int i = 0; i < bmDs.Tables[0].Rows.Count; i++)
            {
                resultJson += "\"" + bmDs.Tables[0].Rows[i]["StartDeptName"].ToString() + "\":{";
                string xmsTooltipStr = "<div><p>年份：  项目数（个）</p></div>";
                string htTooltipStr = "<div><p>年份：  合同额（万元）</p></div>";
                for (int year = minYear; year <= maxYear; year++)
                {
                    int xms = Convert.ToInt32(proDs.Tables[0].Compute("count(proguid)", "StartDeptGuid='" + bmDs.Tables[0].Rows[i]["StartDeptGuid"].ToString() + "' AND proyear=" + (year)));
                    
                    if (xms != 0)
                    {
                        xmsTooltipStr += "<p>" + year + ": &nbsp;&nbsp;&nbsp;&nbsp; " + xms + "</p>";
                        string je = allDs.Tables[0].Compute("sum(htje)", "StartDeptGuid='" + bmDs.Tables[0].Rows[i]["StartDeptGuid"].ToString() + "' AND proyear=" + (year)).ToString();
                        if (Convert.ToDouble(je) != 0)
                        {
                            htTooltipStr += "<p>" + year + ":  &nbsp;&nbsp;&nbsp;&nbsp;" + je + "</p>";
                        }

                    }
                    
                }
               
                resultJson += "\"xms\":\"" + xmsTooltipStr + "\",\"je\":\"" + htTooltipStr + "\"";
                resultJson += "}";
                if (i != bmDs.Tables[0].Rows.Count - 1)
                {
                    resultJson += ",";
                }
            }




            resultJson += "}";
            context.Response.Write(resultJson);
        }

        private void fileSearch(HttpContext context)
        {
            DataSet ds;
            string xmguid = context.Request.Params["xmguid"] ?? "";
            string result = "{\"data\":[";
            result += "{\"refGuid\":\"" + xmguid + "\",\"fileSign\":\"tz_Project_Fa\"},";//项目方案
            result += "{\"refGuid\":\"" + xmguid + "\",\"fileSign\":\"tz_Project_Sbb\"},";//项目申报表
            result += "{\"refGuid\":\"" + xmguid + "\",\"fileSign\":\"tz_Project_Fj\"},";//项目附件
            //获取评审附件
            ds = database.ExecuteDataSet("select * from tz_ProjectHistory where ProGuid='" + xmguid + "' order by createdate");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    if(ds.Tables[0].Rows[i]["proaction"].ToString()=="通过")
                    {
                        result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_Project_PsTg\"},";
                    }
                    else if(ds.Tables[0].Rows[i]["proaction"].ToString()=="退回")
                    {
                        result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_Project_PsTh\"},";
                    }
                    
                }
            }
            result += "{\"refGuid\":\"" + xmguid + "\",\"fileSign\":\"xmys_spcl\"},";//项目预算计划申报表
            result += "{\"refGuid\":\"" + xmguid + "\",\"fileSign\":\"xmys_fj\"},";//项目预算其他附件

            //公开招标、询价、单一来源、直接采购、竞争性磋商、竞争性谈判、邀请招标
            ds = database.ExecuteDataSet("select * from(select guid,'gkzb' as type,sysstatus,xmguid from tz_gkzb union all select guid,'xj' as type,sysstatus,xmguid from tz_xj union all select guid,'dyly' as type,sysstatus,xmguid from tz_dyly union all select guid,'zjcg' as type,sysstatus,xmguid from tz_zjcg union all select guid,'jzxcs' as type,sysstatus,xmguid from tz_jzxcs union all select guid,'yqzb' as type,sysstatus,xmguid from tz_yqzb union all select guid,'jzxtp' as type,sysstatus,xmguid from tz_jzxtp) as a where a.xmguid='" + xmguid + "' and sysstatus!=-1");
            if (ds != null&&ds.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    switch(ds.Tables[0].Rows[i]["type"].ToString())
                    {
                        case "gkzb":
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_gkzb_zhaob\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_gkzb_zhongb\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_gkzb_ht\"},";

                            break;
                        case "xj":
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_xj_xjd\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_xj_fj\"},";
                            break;
                        case "dyly":
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_dyly\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_dyly_zjyj\"},";
                            break;
                        case "zjcg":
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"tz_zb_zjcg\":\"tz_zb_dyly_zjyj\"},";
                            break;
                        case "jzxcs":
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_jzxcs_zhaob\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_jzxcs_zhongb\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_jzxcs_ht\"},";
                            break;
                        case "yqzb":
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_yqzb_zhaob\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_yqzb_zhongb\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_yqzb_ht\"},";
                            break;
                        case "jzxtp":
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_jzxtp_zhaob\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_jzxtp_zhongb\"},";
                            result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_zb_jzxtp_ht\"},";
                            break;
                    }
                }
            }

            ds = database.ExecuteDataSet("select * from tz_xmys where xmGuid='" + xmguid + "'");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"TZ_XMYS1\"},";
                    result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"TZ_XMYS2\"},";
                    result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"TZ_XMYS3\"},";
                }
            }

            ds = database.ExecuteDataSet("select * from tz_xmyw where xmGuid='" + xmguid + "' and sysstatus!=-1");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    result += "{\"refGuid\":\"" + ds.Tables[0].Rows[i]["guid"].ToString() + "\",\"fileSign\":\"tz_xmyw\"},";
                }
            }

            result = result.Substring(0, result.Length - 1);
            result = result + "]}";
            context.Response.Write(result);
        }

        private void changePswd(HttpContext context)
        {
            string pswd = context.Request.Params["password"] ?? "";
            string userguid = context.Request.Params["userguid"] ?? "";
            int totalRow = 0;
            try
            {
                totalRow += database.ExecuteNonQuery("update Sys_LocalUser set password='" + pswd + "'  where guid='" + userguid + "'");
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