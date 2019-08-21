using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;

namespace WebApp.FundManage
{
    public partial class FundManage : SharedPage
    {
        protected RoleCheck roleCheck;
        protected string strProjGuid;
        protected DataSet dsZfqk;
        protected DataSet dsZfjh;
        protected DataSet dsProj;
        protected int projSbYear;
        protected string type = "";
        protected int curYear = 2018;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            curYear = DateTime.Now.Year;
            roleCheck = new RoleCheck(CurrentUser);
            strProjGuid = Request["xmguid"] != null ? Request["xmguid"] : "";
            type = Request["type"] != null ? Request["type"] : "";


            Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            //获取资金支付情况
            string zfqksql = "select * from tz_zjzf where sysstatus!=-1 and  xmguid='" + strProjGuid + "' order by createdate desc";
            dsZfqk = db.ExecuteDataSet(zfqksql);

            //获取资金支付计划
            string zfjhsql = "select * from tz_zjzfjh where sysstatus!=-1 and xmguid='" + strProjGuid + "' order by createdate desc";
            dsZfjh = db.ExecuteDataSet(zfjhsql);

            //获取项目基本信息
            string projsql = "select * from tz_project where proguid='" + strProjGuid + "'";
            dsProj = db.ExecuteDataSet(projsql);

            string startdate = dsProj.Tables[0].Rows[0]["StartDate"].ToString();
            DateTime dt = DateTime.Parse(startdate);
            projSbYear = dt.Year;

           
        }
    }
}