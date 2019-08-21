using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;

namespace WebApp.TZ_XMYW
{
    public partial class List : Yawei.Common.SharedPage
    {
        protected string bmGuid = "";
        protected string currnetUser = "";
        protected string bmwhere = "";
        protected bool isAdmin = true;

        protected RoleCheck roleCheck;
        protected string baseWhere = " and sysstatus!=-1";//显示未删除的项目
        protected string roleWhere = "";
        protected string bmWhere = "";

        protected string sqlWhere = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            string username = CurrentUser.user.UserLoginName;
            roleCheck = new RoleCheck(CurrentUser);

            bmGuid = CurrentUser.UserGroup.Guid;

            //查看本部门审核通过的项目
            bmWhere = " xmguid in  ( select proguid from tz_Project  where StartDeptGuid='" + bmGuid + "' and ProState='申报')";

            //此处查看所有审核通过的项目
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                roleWhere = " xmguid in  ( select proguid from tz_Project  where  ProState='申报') ";
            }

            if (roleWhere != "")
            {
                sqlWhere = baseWhere + "and (" + bmWhere + " or " + roleWhere + ")";
            }
            else
            {
                sqlWhere = baseWhere + "and (" + bmWhere + ")";
            }
            getOldestProj();
        }

        protected int StartYear = DateTime.Now.Year;
        protected int CurYear = DateTime.Now.Year;
        protected void getOldestProj()
        {

            string querysql = "";
            //查找所有申报成功的项目
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                querysql = "select top 1 startdate from tz_project where startdate is not null and sysstatus!=-1 and ProState='申报' order by startdate";
            }
            //查找本部门申报成功的项目
            else
            {
                querysql = "select top 1 startdate from tz_project where startdeptguid='" + CurrentUser.UserGroup.Guid + "' and startdate is not null and sysstatus!=-1 and ProState='申报' order by startdate";
            }
            Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            //获取资金支付情况

            DataSet ds = db.ExecuteDataSet(querysql);
            if (ds != null&&ds.Tables[0].Rows.Count>0)
            {
                string strdate = ds.Tables[0].Rows[0]["StartDate"].ToString();
                DateTime date = DateTime.Parse(strdate);
                StartYear = date.Year;
            }
        }
    }
}