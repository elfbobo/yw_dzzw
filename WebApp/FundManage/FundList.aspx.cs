using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;
using Yawei.DataAccess;

namespace WebApp.FundManage
{
    public partial class FundList : SharedPage
    {
        protected RoleCheck roleCheck;
        protected string roleWhere = "";
        protected string baseWhere = "";
        protected string sqlWhere = "";
        protected string roletype = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            roleCheck = new RoleCheck(CurrentUser);
             string type = Request["type"] != null ? Request["type"] : "";
             if (roleCheck.isAdmin() || roleCheck.isZfb() || roleCheck.isSjj())
             {
                 roleWhere = "";
                 roletype = "0";
             }
             //部门用户只显示本部门金额数据
             else
             {
                 roletype = "1";
                 roleWhere += " and StartDeptGuid='" + CurrentUser.UserGroup.Guid + "'";
             }
             sqlWhere += roleWhere+" and Prostate='申报'";
            if (type == "post")
            {
                Database db = DatabaseFactory.CreateDatabase();
               
                string result = string.Empty;
                DataTable dt;
                if (roleCheck.isSjj() || roleCheck.isAdmin() || roleCheck.isZfb())
                {
                    dt = db.ExecuteDataSet("SELECT sum(htje) AS htje,sum(Quota) AS ygje,sum(zfje) AS zfje FROM V_FundSummary").Tables[0];
                }
                else
                {
                    dt = db.ExecuteDataSet("SELECT sum(htje) AS htje,sum(Quota) AS ygje,sum(zfje) AS zfje FROM V_FundSummary  where StartDeptGuid='" + CurrentUser.UserGroup.Guid + "'").Tables[0];
                }
                
                if (dt.Rows.Count > 0)
                    result = dt.Rows[0]["ygje"].ToString() + "," + dt.Rows[0]["htje"].ToString() + "," + dt.Rows[0]["zfje"].ToString();
                Response.Clear();
                Response.Write(result);
                Response.Flush();
                Response.End();
            }
        }
    }
}