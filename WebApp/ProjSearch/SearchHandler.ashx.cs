using SupportLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApp.ProjSearch
{
    /// <summary>
    /// SearchHandler 的摘要说明
    /// </summary>
    public class SearchHandler : IHttpHandler
    {
        Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"] ?? "";
            if (action == "file")
            {
                exportFile(context);
            }
            else if (action == "table")
            {
                initTable(context);
            }
        }


        private void initTable(HttpContext context)
        {
            //<tr><td rowspan="5">1</td>        <td rowspan="5">云防护</td>        <td rowspan="5">0</td><td rowspan="5">0</td>          <td>平台资金</td>          <td>0</td>          <td>2018</td>          <td>0</td>      </tr>        <tr>            <td>财政资金</td>            <td>0</td>            <td>2018</td>          <td>0</td>        </tr>        <tr>            <td>财政资金</td>            <td>0</td>            <td>2018</td>          <td>0</td>        </tr>        <tr>            <td>财政资金</td>            <td>0</td>            <td>2018</td>          <td>0</td>        </tr>        <tr>            <td>财政资金</td>            <td>0</td>            <td>2018</td>          <td>0</td>        </tr>"
            string tableStr = "";
            string bmguid = context.Request.Params["bmguid"] ?? "";
            string querysql = "select proguid,proname,startdate,StartDeptGuid,StartDeptName,ProState,htje,zfje ,c.zczjly,case when c.ptzj is null then '0' else c.ptzj end as ptzj,case when c.cazj is null then '0' else c.cazj end as cazj,case when c.szzj is null then '0' else c.szzj end as szzj,case when c.zczj is null then '0' else c.zczj end as zczj,case when c.qtzj is null then '0' else c.qtzj end as qtzj,cast(b.SortNum as int) as sortnum from V_TZ_ProjectOverview a left join Sys_UserGroups b on a.StartDeptGuid=b.Guid left join tz_zjzc c on a.ProGuid=c.xmguid where ProState='申报' and startdeptguid='" + bmguid + "' order by sortnum,startdate";
            DataSet ds = db.ExecuteDataSet(querysql);

            DataSet dsjh = db.ExecuteDataSet("select * from tz_zfjhb");


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                int index = i + 1;
                tableStr += "<tr><td rowspan='5'>" + index + "</td>";
                tableStr += "<td rowspan='5'>" + row["proname"].ToString() + "</td>";
                tableStr += "<td rowspan='5'>" + row["htje"].ToString() + "</td>";
                tableStr += "<td rowspan='5'>" + row["zfje"].ToString() + "</td>";
                tableStr += "<td>平台资金</td><td>" + row["ptzj"] + "</td><td>2018</td>";
                DataRow[] drArr=dsjh.Tables[0].Select("nd='2018' and xmguid='" + row["proguid"] + "'");
                if (drArr.Count() == 0)
                {
                    tableStr += "<td></td></tr>";
                }
                else
                {
                    tableStr += "<td>" + drArr[0]["je"].ToString() + "</td></tr>";
                }

                tableStr += "<tr><td>财政资金</td><td>" + row["cazj"] + "</td><td>2019</td>";
                drArr = dsjh.Tables[0].Select("nd='2019' and xmguid='" + row["proguid"] + "'");
                if (drArr.Count() == 0)
                {
                    tableStr += "<td></td></tr>";
                }
                else
                {
                    tableStr += "<td>" + drArr[0]["je"].ToString() + "</td></tr>";
                }

                tableStr += "<tr><td>上争资金</td><td>" + row["szzj"] + "</td><td>2020</td>";
                drArr = dsjh.Tables[0].Select("nd='2020' and xmguid='" + row["proguid"] + "'");
                if (drArr.Count() == 0)
                {
                    tableStr += "<td>&nbsp</td></tr>";
                }
                else
                {
                    tableStr += "<td>" + drArr[0]["je"].ToString() + "</td></tr>";
                }


                
                string lystr = "";
                if (row["zczjly"].ToString().Trim() == "")
                {
                    lystr = "";
                }
                else
                {
                    lystr = "<br/>(" + row["zczjly"] + ")";
                }
                drArr = dsjh.Tables[0].Select("nd='2021' and xmguid='" + row["proguid"] + "'");
                tableStr += "<tr><td>自筹资金<br/>(资金来源)</td><td>" + row["zczj"] + lystr + "</td><td>2021</td>";
                if (drArr.Count() == 0)
                {
                    tableStr += "<td>&nbsp</td></tr>";
                }
                else
                {
                    tableStr += "<td>" + drArr[0]["je"].ToString() + "</td></tr>";
                }

                tableStr += "<tr><td>其他</td><td>" + row["qtzj"] +"</td>";
                
                drArr = dsjh.Tables[0].Select("nd='2022' and xmguid='" + row["proguid"] + "'");

                if (drArr.Count() == 0)
                {
                    tableStr += "<td>...</td>";
                    tableStr += "<td>&nbsp</td></tr>";
                }
                else
                {
                    tableStr += "<td>2022</td>";
                    tableStr += "<td>" + drArr[0]["je"].ToString() + "</td></tr>";
                }


               
            }
            context.Response.Write(tableStr);

        }

        private void exportFile(HttpContext context)
        {
            string roleType = context.Request.Params["roletype"] ?? "";

            DataSet dsZj;
            DataSet dsZjjh;
           
            //导出本部门已申报的项目数据
            string path = ConfigurationManager.AppSettings["excelpath"];
            //部门用户
            if (roleType == "0")
            {
                string bmguid = context.Request.Params["bmguid"] ?? "";
                string querysql = "select proguid,proname,startdate,StartDeptGuid,StartDeptName,ProState,htje,zfje ,cast(b.SortNum as int) as sortnum from V_TZ_ProjectOverview a left join Sys_UserGroups b on a.StartDeptGuid=b.Guid where ProState='申报' and startdeptguid='" + bmguid + "' order by sortnum,startdate";
                DataSet ds = db.ExecuteDataSet(querysql);

                dsZj = db.ExecuteDataSet("select * from tz_zjzc where xmguid in (select proguid from tz_project where startdeptguid='" + bmguid + "')");
                dsZjjh = db.ExecuteDataSet("select * from tz_zfjhb where xmguid in (select proguid from tz_project where startdeptguid='" + bmguid + "' ) order by cast(nd as int)");

                ExportHelper.ExportToExcelTemplate(ds, path, context.Request.Params["bmname"] ?? "", dsZj, dsZjjh);
            }
            //管理员用户
            else
            {
                string bmguid = context.Request.Params["bmguid"] ?? "";
                string querysql = "select proguid,proname,startdate,StartDeptGuid,StartDeptName,ProState,htje,zfje ,cast(b.SortNum as int) as sortnum from V_TZ_ProjectOverview a left join Sys_UserGroups b on a.StartDeptGuid=b.Guid where ProState='申报' order by sortnum,startdate";
                DataSet ds = db.ExecuteDataSet(querysql);

                dsZj = db.ExecuteDataSet("select * from tz_zjzc");
                dsZjjh = db.ExecuteDataSet("select * from tz_zfjhb order by cast(nd as int)");
                
                ExportHelper.ExportToExcelTemplate(ds, path, context.Request.Params["bmname"] ?? "", dsZj, dsZjjh);
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