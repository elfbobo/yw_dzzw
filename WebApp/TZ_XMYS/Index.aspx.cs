using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.DataAccess;

namespace WebApp.TZ_XMYS
{
    public partial class Index : Yawei.Common.SharedPage
    {
        public string YSCount = string.Empty;
        public string Year = string.Empty;
        public string s = string.Empty;
        public string es = string.Empty;
        public string table = string.Empty;

        protected RoleCheck roleCheck;
        protected DataSet dsProj;
        protected void Page_Load(object sender, EventArgs e)
        {
            roleCheck = new RoleCheck(CurrentUser);
            

            Database db = DatabaseFactory.CreateDatabase();
            string projSql = "";
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                projSql = "select a.*,year(startdate) as xmyear,year(b.YsDate) as ysyear,b.ysresult as ysresult,b.ysdate as ysdate  from V_TZ_ProjectOverview a left join tz_xmys b on a.proguid=b.xmguid";
            }
            else
            {
                projSql = "select a.*,year(startdate) as xmyear,year(b.YsDate) as ysyear,b.ysresult as ysresult,b.ysdate as ysdate  from V_TZ_ProjectOverview a left join tz_xmys b on a.proguid=b.xmguid where startdeptguid='" + CurrentUser.UserGroup.Guid + "' order by  startdate desc,createdate desc";
            }
            dsProj = db.ExecuteDataSet(projSql);



            //DataSet ds = db.ExecuteDataSet("select *,year(startdate) as st,year(ysdate) as yt from View_Ys_tz_Project where sysstatus<>-1 order by ysdate desc");
            YSCount = "[" + dsProj.Tables[0].Rows.Count + "," + dsProj.Tables[0].Select("yscount<>0").Length + "]";

            for (int i = DateTime.Now.Year -4; i <= DateTime.Now.Year; i++)
            {
                Year += i + ",";
                s += dsProj.Tables[0].Select("xmyear=" + i).Length + ",";
                es += dsProj.Tables[0].Select("ysyear=" + i).Length + ",";
            }
            Year = "[" + Year.TrimEnd(',') + "]";
            s = "[" + s.TrimEnd(',') + "]";
            es = "[" + es.TrimEnd(',') + "]";

            //ds = db.ExecuteDataSet("select top 10 *,year(startdate) as st,year(ysdate) as yt from View_Ys_tz_Project where sysstatus<>-1 and ysdate is not null order by ysdate desc");

            for (int i = 0; i < dsProj.Tables[0].Select("yscount<>0").Count(); i++)
            {
                DataRow row = dsProj.Tables[0].Select("yscount<>0")[i];
                table += "<tr>";
                if (!roleCheck.isBm())
                {
                    table += "<td>" + row["StartDeptName"] + "</td>";
                }
                
                table += "<td>" + row["ProName"] + "</td>";
                table += "<td>" + row["Quota"] + "</td>";
                table += "<td>" + row["MoneySource"] + "</td>";
                table += "<td>" + row["ProType"] + "</td>";
                table += "<td>" + row["ysdate"].ToString().Substring(0, row["ysdate"].ToString().Length - 7) + "</td>";
                table += "<td>" + row["ysresult"] + "</td>";
                table += "</tr>";
            }
        }
    }
}