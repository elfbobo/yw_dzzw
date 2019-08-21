using SupportLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApp.FundManage
{
    /// <summary>
    /// FundHandler 的摘要说明
    /// </summary>
    public class FundHandler : IHttpHandler
    {
        Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Params["action"] ?? "";
            string proGuid = context.Request.Params["proguid"] ?? "";

            if (action == "zjzfsubmit")
            {
               // xmguid:'<%=strProjGuid%>', zfje:$("#zfje").val(),zfsj:$("#zfsj").val(),skdw:$("#skdw").val()
                ZjzfSubmit(context);
            }
            else if (action == "zfjhsubmit")
            {
                Zfjhsubmit(context);
            }
            else if (action == "zjzfdel")
            {
                string guid = context.Request.Params["guid"] ?? "";
                context.Response.Write(db.ExecuteNonQuery("delete from tz_zjzf where guid='" + guid + "'"));
            }
            else if (action == "zftj")
            {
                string dataJson = "";
                string yzf = "{";
                string jhzf = "";
                int curYear = DateTime.Now.Year;
                DataSet ds = db.ExecuteDataSet("select xmguid,zfsj,case when cast(zfje as float) is null then 0 else cast(zfje as float) end as zfje from tz_zjzf where xmguid='" + proGuid + "' and sysstatus!=-1");


                dataJson += "[{";

                int title = 0;
                for (int i = curYear - 2; i <= curYear + 1; i++)
                {

                    
                    //string condition = "zfsj>=#" + i + "-01-01#";
                    string condition = "zfsj>=#" + i + "-01-01# AND zfsj<=#" + i + "-12-31#";
                    if (ds.Tables[0].Select(condition).Count() == 0)
                    {
                        dataJson += "\"" + "data" + title + "\":0";
                    }
                    else
                    {
                        Object obj = ds.Tables[0].Compute("Sum(zfje)", condition);
                        if (obj == null)
                        {
                            dataJson += "\"" + "data" + title + "\":0";
                        }
                        else
                        {
                            double total = Convert.ToDouble(ds.Tables[0].Compute("Sum(zfje)", condition));
                            dataJson += "\"" + "data" + title + "\":" + total + "";
                        }
                        
                    }
                    if (i != curYear + 1)
                    {
                        dataJson += ",";
                    }
                    title++;
                }

                dataJson += "},{";

                DataSet ds2 = db.ExecuteDataSet("select xmguid,nd,case when cast(zfje as float) is null then 0 else cast(zfje as float) end as zfje from tz_zjzfjh where xmguid='" + proGuid + "' and sysstatus!=-1");
                title = 0;
                for (int i = curYear - 2; i <= curYear + 1; i++)
                {


                    //string condition = "zfsj>=#" + i + "-01-01#";
                    string condition = "nd='" + i + "'";
                    if (ds2.Tables[0].Select(condition).Count() == 0)
                    {
                        dataJson += "\"" + "data" + title + "\":0";
                    }
                    else
                    {
                        Object obj = ds2.Tables[0].Compute("Sum(zfje)", condition);
                        if (obj == null)
                        {
                            dataJson += "\"" + "data" + title + "\":0";
                        }
                        else
                        {
                            double total = Convert.ToDouble(ds2.Tables[0].Compute("Sum(zfje)", condition));
                            dataJson += "\"" + "data" + title + "\":" + total + "";
                        }

                    }
                    if (i != curYear + 1)
                    {
                        dataJson += ",";
                    }
                    title++;
                }
                dataJson += "},{";
                DataSet ds3 = db.ExecuteDataSet("select * from V_TZ_ProjectOverview where ProGuid='" + proGuid + "'");
                dataJson += "\"yzf\":" + Convert.ToDouble(ds3.Tables[0].Rows[0]["zfje"]) + ",";
                double dzf = Convert.ToDouble(ds3.Tables[0].Rows[0]["htje"]) - Convert.ToDouble(ds3.Tables[0].Rows[0]["zfje"]);
                if (dzf < 0) dzf = 0;
                dataJson += "\"dzf\":" + dzf;
                dataJson += "}]";
                context.Response.Write(dataJson);
               
            }
            else if (action == "zfjhdel")
            {
                string guid = context.Request.Params["guid"] ?? "";
                context.Response.Write(db.ExecuteNonQuery("delete from tz_zjzfjh where guid='" + guid + "'"));
            }
            else if (action == "export")
            {
                string roletype = context.Request.Params["roletype"] ?? "";
                string year = context.Request.Params["year"] ?? "";
                //string querysql = "select a.*,case when cast(b.zfje as float) is not null then cast(b.zfje as float) else 0 end as zfje,case when cast(c.jhzfje as float) is not null then cast(c.jhzfje as float) else 0 end as jhzfje,c.beizhu,d.SortNum,CASE WHEN CAST(l.htje_gkzb AS FLOAT) IS NOT NULL THEN CAST(l.htje_gkzb AS FLOAT) ELSE 0 END + CASE WHEN CAST(m.htje_xj AS FLOAT) IS NOT NULL THEN CAST(m.htje_xj AS FLOAT) ELSE 0 END + CASE WHEN CAST(n.htje_jzxcs AS FLOAT) IS NOT NULL THEN CAST(n.htje_jzxcs AS FLOAT) ELSE 0 END + CASE WHEN CAST(o.htje_dyly AS FLOAT) IS NOT NULL THEN CAST(o.htje_dyly AS FLOAT) ELSE 0 END + CASE WHEN CAST(p.htje_zjcg AS FLOAT) IS NOT NULL THEN CAST(p.htje_zjcg AS FLOAT) ELSE 0 END as htje from tz_Project a left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_gkzb,xmguid  from tz_gkzb where sysstatus!=-1 group by xmguid ) as l on a.ProGuid=l.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_xj,xmguid  from tz_xj where sysstatus!=-1 group by xmguid ) as m on a.ProGuid=m.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_jzxcs,xmguid  from tz_jzxcs where sysstatus!=-1 group by xmguid ) as n on a.ProGuid=n.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_dyly,xmguid  from tz_dyly where sysstatus!=-1 group by xmguid ) as o on a.ProGuid=o.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_zjcg,xmguid  from tz_zjcg where sysstatus!=-1 group by xmguid ) as p on a.ProGuid=p.xmguid left join (select sum(case when cast(zfje as float) is null then 0 else cast(zfje as float) end) as zfje,xmguid from [dbo].[tz_zjzf] where year(zfsj)=" + year + " and sysstatus!=-1 group by xmguid) b on a.ProGuid=b.xmguid left join (select sum(case when cast(zfje as float) is null then 0 else cast(zfje as float) end) as jhzfje,xmguid,dbo.MergeCharField(xmguid,'"+year+"') as beizhu from tz_zjzfjh where sysstatus!=-1 and nd='" + year + "' group by xmguid) c on a.ProGuid=c.xmguid left join Sys_UserGroups d on a.StartDeptGuid=d.Guid where a.ProState!='' and a.ProState is not null and a.sysstatus!=-1 ";

                string querysql = "select a.*,case when cast(h.ndzfje as float) is null then 0 else cast(h.ndzfje as float) end as ndzfje,case when cast(b.zfje as float) is not null then cast(b.zfje as float) else 0 end as zfje,case when cast(c.jhzfje as float) is not null then cast(c.jhzfje as float) else 0 end as jhzfje,c.beizhu,d.SortNum,CASE WHEN CAST(l.htje_gkzb AS FLOAT) IS NOT NULL THEN CAST(l.htje_gkzb AS FLOAT) ELSE 0 END + CASE WHEN CAST(m.htje_xj AS FLOAT) IS NOT NULL THEN CAST(m.htje_xj AS FLOAT) ELSE 0 END + CASE WHEN CAST(n.htje_jzxcs AS FLOAT) IS NOT NULL THEN CAST(n.htje_jzxcs AS FLOAT) ELSE 0 END + CASE WHEN CAST(o.htje_dyly AS FLOAT) IS NOT NULL THEN CAST(o.htje_dyly AS FLOAT) ELSE 0 END + CASE WHEN CAST(p.htje_zjcg AS FLOAT) IS NOT NULL THEN CAST(p.htje_zjcg AS FLOAT) ELSE 0 END + CASE WHEN CAST(x.htje_yqzb AS FLOAT) IS NOT NULL THEN CAST(x.htje_yqzb AS FLOAT) ELSE 0 END + CASE WHEN CAST(y.htje_jzxtp AS FLOAT) IS NOT NULL THEN CAST(y.htje_jzxtp AS FLOAT) ELSE 0 END as htje from tz_Project a left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_gkzb,xmguid  from tz_gkzb where sysstatus!=-1 group by xmguid ) as l on a.ProGuid=l.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_xj,xmguid  from tz_xj where sysstatus!=-1 group by xmguid ) as m on a.ProGuid=m.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_jzxcs,xmguid  from tz_jzxcs where sysstatus!=-1 group by xmguid ) as n on a.ProGuid=n.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_yqzb,xmguid  from tz_yqzb where sysstatus!=-1 group by xmguid ) as x on a.ProGuid=x.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_jzxtp,xmguid  from tz_jzxtp where sysstatus!=-1 group by xmguid ) as y on a.ProGuid=y.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_dyly,xmguid  from tz_dyly where sysstatus!=-1 group by xmguid ) as o on a.ProGuid=o.xmguid left outer join (select sum(CASE WHEN CAST(htje AS FLOAT) IS NOT NULL THEN CAST(htje AS FLOAT) ELSE 0 END) as htje_zjcg,xmguid  from tz_zjcg where sysstatus!=-1 group by xmguid ) as p on a.ProGuid=p.xmguid left join (select sum(case when cast(zfje as float) is null then 0 else cast(zfje as float) end) as zfje,xmguid from [dbo].[tz_zjzf] where  sysstatus!=-1 group by xmguid) b on a.ProGuid=b.xmguid left join (select sum(case when cast(zfje as float) is null then 0 else cast(zfje as float) end) as ndzfje,xmguid from [dbo].[tz_zjzf] where  sysstatus!=-1 and year(zfsj)=" + year + " group by xmguid) h on a.ProGuid=h.xmguid left join (select sum(case when cast(zfje as float) is null then 0 else cast(zfje as float) end) as jhzfje,xmguid,dbo.MergeCharField(xmguid,'" + year + "') as beizhu from tz_zjzfjh where sysstatus!=-1 and nd='" + year + "' group by xmguid) c on a.ProGuid=c.xmguid left join Sys_UserGroups d on a.StartDeptGuid=d.Guid where a.ProState!='' and a.ProState is not null and a.sysstatus!=-1 ";
                string ordersql = " order by cast(SortNum as int),StartDate";
                //导出所有部门报表
                if (roletype == "0")
                {

                }
                //导出部门报表
                else
                {
                    string startdeptguid = context.Request.Params["bmguid"] ?? "";
                    querysql += " and startdeptguid='" + startdeptguid + "' ";
                }
                DataSet ds = db.ExecuteDataSet(querysql + ordersql);
                ExportHelper e;
                string path = ConfigurationManager.AppSettings["excelpath"];

                context.Response.Write(ExportHelper.ExportToExcel2(ds, year, path));
            }
        }

        public void Zfjhsubmit(HttpContext context)
        {
            string xmguid = context.Request.Params["xmguid"];
            string zfje = context.Request.Params["zfje"];
            string nd = context.Request.Params["nd"];
            string skdw = context.Request.Params["skdw"];
            string guid = context.Request.Params["guid"];
            string beizhu = context.Request.Params["beizhu"];

            string mergesql = "  if exists (select 1 from tz_zjzfjh where guid = '" + guid + "') update tz_zjzfjh set zfje = '" + zfje + "',skdw='" + skdw + "',nd='" + nd + "',beizhu='" + beizhu + "' where guid = '" + guid + "' else insert into tz_zjzfjh(Guid,xmguid,zfje,nd,skdw,beizhu,createdate,sysstatus) values('" + guid + "','" + xmguid + "','" + zfje + "','" + nd + "','" + skdw + "','" + beizhu + "',getdate(),0)";


            int count = db.ExecuteNonQuery(mergesql);
            context.Response.Write(count);
        }

        public void ZjzfSubmit(HttpContext context)
        {
            string xmguid = context.Request.Params["xmguid"];
            string zfje = context.Request.Params["zfje"];
            string zfsj = context.Request.Params["zfsj"];
            string skdw = context.Request.Params["skdw"];
            string guid = context.Request.Params["guid"];
            string beizhu = context.Request.Params["beizhu"];

            string mergesql = "if exists (select 1 from tz_zjzf where guid = '"+guid+"') update tz_zjzf set zfje = '"+zfje+"',zfsj='"+zfsj+"',skdw='"+skdw+"',updatetime=getdate(),beizhu='"+beizhu+"' where guid = '"+guid+"' else insert into tz_zjzf(Guid,xmguid,zfje,zfsj,skdw,beizhu,createdate,sysstatus) values('"+guid+"','"+xmguid+"','"+zfje+"','"+zfsj+"','"+skdw+"','"+beizhu+"',getdate(),0)";
            //string sql = " insert into tz_zjzf(Guid,xmguid,zfje,zfsj,skdw,beizhu,createdate,sysstatus) values(@Guid,@xmguid,@zfje,@zfsj,@skdw,@beizhu,getdate(),0)";
            //var dbcmd = db.CreateCommand(System.Data.CommandType.Text, sql);
            //db.AddInParameter(dbcmd, "@Guid", System.Data.DbType.String, guid);
            //db.AddInParameter(dbcmd, "@xmguid", System.Data.DbType.String, xmguid);
            //db.AddInParameter(dbcmd, "@zfje", System.Data.DbType.String, zfje);
            //db.AddInParameter(dbcmd, "@zfsj", System.Data.DbType.String, zfsj);
            //db.AddInParameter(dbcmd, "@skdw", System.Data.DbType.String, skdw);
            //db.AddInParameter(dbcmd, "@beizhu", System.Data.DbType.String, beizhu);
            int count = db.ExecuteNonQuery(mergesql);
            context.Response.Write(count);
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