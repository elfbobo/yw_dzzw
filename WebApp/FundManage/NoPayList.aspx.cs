using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;

namespace WebApp.FundManage
{
    public partial class NoPayList : SharedPage
    {
        protected RoleCheck roleCheck;
        protected string roleWhere = "";
        protected string baseWhere = "";
        protected string sqlWhere = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            roleCheck = new RoleCheck(CurrentUser);
            if (roleCheck.isAdmin() || roleCheck.isZfb() || roleCheck.isSjj())
            {
                roleWhere = "";
            }
            //部门用户只显示本部门金额数据
            else
            {
                roleWhere += " and StartDeptGuid='" + CurrentUser.UserGroup.Guid + "'";
            }
            sqlWhere += roleWhere;
        }
    }
}