using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;
using Yawei.DataAccess;

namespace WebApp.Project.tz_ProjOverview
{
    public partial class Index : SharedPage
    {
        protected StringBuilder yearSb = new StringBuilder();//近五年项目概况
        protected StringBuilder fwSb = new StringBuilder();//资金使用范围统计
        protected StringBuilder zbSb = new StringBuilder();//招标使用范围
        protected StringBuilder sySb = new StringBuilder();//资金使用情况
        protected StringBuilder bmSb = new StringBuilder();//部门项目信息统计

        Database db = DatabaseFactory.CreateDatabase();

        protected bool isFirstView = false;//判断是否首次登录

        protected RoleCheck roleCheck;
        protected string tableTitle = "";

        protected DataSet dszftj;
        protected DataSet dsprotj;

        protected DataTable dtXms=null;
        protected DataTable dtHte=null;
        

        protected void Page_Load(object sender, EventArgs e)
        {

            roleCheck = new RoleCheck(CurrentUser);

            isFirstView = checkFirstView();//判断该用户是否首次浏览
            insertViewHistory();//插入浏览记录

            string projSql = "";
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                projSql = "select a.*,year(startdate) as xmyear from V_TZ_ProjectOverview a";
            }
            else
            {
                projSql = "select a.*,year(startdate) as xmyear from V_TZ_ProjectOverview a where startdeptguid='" + CurrentUser.UserGroup.Guid + "'";
            }
            
                //查询近五年项目数据
                //yearSql = "SELECT year(StartDate) AS year,'申报' AS type FROM tz_Project WHERE ProState IN ('申报','立项','招标','实施','验收') AND sysstatus<>-1 AND year(StartDate) BETWEEN (year(getdate())-4) AND year(getdate()) GROUP BY  year(StartDate) UNION SELECT year(pfsj) AS year,'立项' AS type FROM tz_xmlx WHERE xmguid IN (SELECT ProGuid FROM tz_Project WHERE  sysstatus<>-1) AND year(pfsj) BETWEEN (year(getdate())-4) AND year(getdate()) GROUP BY  year(pfsj)UNION SELECT year(yssj) AS year,'验收' AS type FROM tz_xmys WHERE xmguid IN (SELECT ProGuid FROM tz_Project WHERE  sysstatus<>-1) AND year(yssj) BETWEEN (year(getdate())-4) AND year(getdate()) GROUP BY  year(yssj)";



            DataSet projDs = db.ExecuteDataSet(projSql);
            if (projDs.Tables[0].Rows.Count > 0)
            {
                int nowYear = DateTime.Now.Year;
                for (int i = 0; i < 5; i++)
                {
                    yearSb.Append("<tr>");
                    yearSb.Append("<td>" + (nowYear - i) + "</td>");
                    //申报数
                    yearSb.Append("<td>" + projDs.Tables[0].Select("xmyear=" + (nowYear - i) + " and prostate in('提交','退回','申报')").Count() + "</td>");
                    //立项数
                    yearSb.Append("<td>" + projDs.Tables[0].Select("xmyear=" + (nowYear - i) + " and prostate='申报'").Count() + "</td>");
                    //验收数
                    yearSb.Append("<td>" + projDs.Tables[0].Select("xmyear=" + (nowYear - i) + " and prostate='申报' and  (yscount<>0 or ywcount<>0)").Count() + "</td>");
                    yearSb.Append("</tr>");
                }
            }
            else
            {
                yearSb.Append("<tr><td>暂无统计信息</td></tr>");
            }

            

            //资金使用范围统计
            //string fwSql = "select * from tz_Project a left join (SELECT xmguid, sum( CAST (htje AS FLOAT))*10000 AS htje FROM tz_xmht  GROUP BY xmguid) b on a.ProGuid=b.xmguid where a.sysstatus!=-1";
            //DataSet fwDs = db.ExecuteDataSet(fwSql);
            if (projDs!=null&&projDs.Tables[0].Rows.Count > 0)
            {
                fwSb.Append("[" + projDs.Tables[0].Select("htje<100").Count() + "," + projDs.Tables[0].Select("htje<200 and htje>=100").Count() + "," + projDs.Tables[0].Select("htje>=200 and htje<500").Count() + "," + projDs.Tables[0].Select("htje>=500").Count() + "]");
            }
            else
            {
                fwSb.Append("[]");
            }



            //招标方式统计
            if (projDs != null && projDs.Tables[0].Rows.Count > 0)
            {
                zbSb.Append("[{value:" + projDs.Tables[0].Select("gkzbcount>0").Count() + ", name:'公开招标'},{value:" + projDs.Tables[0].Select("xjcount>0").Count() + ", name:'询价'},{value:" + projDs.Tables[0].Select("dylycount>0").Count() + ", name:'单一来源'},{value:" + projDs.Tables[0].Select("zjcgcount>0").Count() + ", name:'自行采购'},{value:" + projDs.Tables[0].Select("jzxcscount>0").Count() + ", name:'竞争性磋商'},{value:" + projDs.Tables[0].Select("yqzbcount>0").Count() + ", name:'邀请招标'},{value:" + projDs.Tables[0].Select("jzxtpcount>0").Count() + ", name:'竞争性谈判'}]");
                //zbSb.Append("[{value:" + projDs.Tables[0].Select("gkzbcount>0").Count() + "},{value:" + projDs.Tables[0].Select("xjcount>0").Count() + "},{value:" + projDs.Tables[0].Select("dylycount>0").Count() + "},{value:" + projDs.Tables[0].Select("zjcgcount>0").Count() + "},{value:" + projDs.Tables[0].Select("jzxcscount>0").Count() + "}]");
            }
            //if (zbDs.Tables[0].Rows.Count > 0)
            //{
            //    zbSb.Append("[{value:" + zbDs.Tables[0].Rows[0]["gkzb"] + ", name:'公开招标'},{value:" + zbDs.Tables[0].Rows[0]["xj"] + ", name:'询价'},{value:" + zbDs.Tables[0].Rows[0]["dyly"] + ", name:'单一来源'},{value:" + zbDs.Tables[0].Rows[0]["zjcg"] + ", name:'直接采购'},{value:" + zbDs.Tables[0].Rows[0]["jzxcs"] + ", name:'竞争性磋商'}]");
            //}
            else
            {
                zbSb.Append("[{value:0,name:\"公开招标\"},{value:0,name:\"询价\"},{value:0,name:\"单一来源\"},{value:0,name:\"自行采购\"},{value:0,name:\"竞争性磋商\"},{value:0,name:\"邀请招标\"},{value:0,name:\"竞争性谈判\"}]");
            }

            //资金使用情况
            if (projDs!=null&&projDs.Tables[0].Rows.Count > 0)
            {
                sySb.Append("[" + projDs.Tables[0].Select("zfje>=htje").Count() + "," + projDs.Tables[0].Select("zfje<htje").Count() + "]");
            }
            else
            {
                sySb.Append("[]");
            }


            string tjquerysql = "";
            //非部门用户展示部门统计信息
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                tableTitle = "部门项目概况";
                tjquerysql = "select count(*) as xmcount,sum(Quota) as total_ys,sum(htje) as total_htje,sum(zfje) as total_zfje,startdeptname from V_TZ_ProjectOverview  group by startdeptname,sortnum order by sortnum desc";
                dszftj= db.ExecuteDataSet(tjquerysql);


                DataSet ds = db.ExecuteDataSet("select count(*) as xmcount,sum(Quota) as total_ys,sum(htje) as total_htje,sum(zfje) as total_zfje,startdeptname from V_TZ_ProjectOverview  where ProState='申报' group by startdeptname,sortnum order by sortnum desc");
                ds.Tables[0].DefaultView.Sort = "xmcount desc";
                dtXms = ds.Tables[0].DefaultView.ToTable();

                ds.Tables[0].DefaultView.Sort = "total_htje desc";
                dtHte = ds.Tables[0].DefaultView.ToTable();


                dsprotj = db.ExecuteDataSet("select * from V_TZ_ProjectOverview");
                if (dszftj != null && dszftj.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dszftj.Tables[0].Rows.Count; i++)
                    {
                        bmSb.Append("<tr>");
                        bmSb.Append("<td style='text-align:left'>" + dszftj.Tables[0].Rows[i]["startdeptname"] + "</td>");//部门名称
                        bmSb.Append("<td>" + dszftj.Tables[0].Rows[i]["xmcount"].ToString() + "</td>");//项目申报总数
                        bmSb.Append("<td>" + dsprotj.Tables[0].Select("StartDeptName='" + dszftj.Tables[0].Rows[i]["startdeptname"] + "' and ProState='申报'").Count() + "</td>");//立项项目数
                        bmSb.Append("<td>" + dsprotj.Tables[0].Select("StartDeptName='" + dszftj.Tables[0].Rows[i]["startdeptname"] + "' and ProState='申报' and yscount>0").Count() + "</td>");//验收项目数
                        bmSb.Append("<td style='text-align:left'>" + dszftj.Tables[0].Rows[i]["total_ys"] + "</td>");//预算总额
                        bmSb.Append("<td style='text-align:left'>" + dszftj.Tables[0].Rows[i]["total_htje"] + "</td>");//合同总额
                        bmSb.Append("<td style='text-align:left'>" + dszftj.Tables[0].Rows[i]["total_zfje"] + "</td>");//支付总额

                        bmSb.Append("</tr>");
                    }
                }
            }
            //部门用户展示项目信息
            else
            {

                DataSet dsprotj = db.ExecuteDataSet("select * from V_TZ_ProjectOverview where startdeptguid='" + CurrentUser.UserGroup.Guid + "' order by startdate desc,createdate desc");
                tableTitle = "项目信息";

                if (dsprotj != null && dsprotj.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsprotj.Tables[0].Rows.Count; i++)
                    {
                        int index = i + 1;
                        bmSb.Append("<tr>");
                        bmSb.Append("<td>" + index + "</td>");//部门名称
                        bmSb.Append("<td style='text-align:left'>" + dsprotj.Tables[0].Rows[i]["proname"] + "</td>");//项目名称
                        bmSb.Append("<td>" + dsprotj.Tables[0].Rows[i]["startdate"].ToString().Substring(0, dsprotj.Tables[0].Rows[i]["startdate"].ToString().Length-7) + "</td>");//申报日期
                        bmSb.Append("<td style='text-align:left'>" + dsprotj.Tables[0].Rows[i]["quota"].ToString() + "</td>");//预算金额（万元）
                        bmSb.Append("<td style='text-align:left'>" + dsprotj.Tables[0].Rows[i]["htje"].ToString() + "</td>");//合同金额（万元）
                        bmSb.Append("<td style='text-align:left'>" + dsprotj.Tables[0].Rows[i]["zfje"].ToString() + "</td>");//支付金额（万元）
                        string xmzt = "";
                        if (dsprotj.Tables[0].Rows[i]["prostate"].ToString() == "提交" || dsprotj.Tables[0].Rows[i]["zfje"].ToString() == "退回")
                        {
                            xmzt = "申报";
                        }
                        else
                        {
                            if (dsprotj.Tables[0].Rows[i]["jscount"].ToString() == "0" && dsprotj.Tables[0].Rows[i]["yscount"].ToString() == "0" && dsprotj.Tables[0].Rows[i]["ywcount"].ToString() == "0")
                            {
                                xmzt = "立项";
                            }
                            else if (dsprotj.Tables[0].Rows[i]["jscount"].ToString() != "0" && dsprotj.Tables[0].Rows[i]["yscount"].ToString() == "0" && dsprotj.Tables[0].Rows[i]["ywcount"].ToString() == "0")
                            {
                                xmzt = "建设";
                            }
                            else if ( dsprotj.Tables[0].Rows[i]["yscount"].ToString() != "0" && dsprotj.Tables[0].Rows[i]["ywcount"].ToString() == "0")
                            {
                                xmzt = "验收";
                            }
                            else if ( dsprotj.Tables[0].Rows[i]["ywcount"].ToString() != "0")
                            {
                                xmzt = "运维";
                            }

                        }
                        bmSb.Append("<td style='text-align:left'>" + xmzt + "</td>");//项目阶段


                        bmSb.Append("</tr>");
                    }
                }
            }

               
           
        }

        private bool checkFirstView()
        {
            string queryloghistory = "select count(*) as viewcount from tz_PageViewHistory where userguid='" + CurrentUser.UserGuid + "' and pageindex='/WebApp/Project/tz_ProjOverview/Index.aspx'";
            DataSet yearDs = db.ExecuteDataSet(queryloghistory);
            if (Convert.ToInt32(yearDs.Tables[0].Rows[0]["viewcount"].ToString()) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void insertViewHistory()
        {
            db.ExecuteNonQuery("insert into tz_PageViewHistory values('"+CurrentUser.UserGuid+"','/WebApp/Project/tz_ProjOverview/Index.aspx')");
        }
    }
}