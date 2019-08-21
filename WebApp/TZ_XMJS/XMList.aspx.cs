using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;

namespace WebApp.TZ_XMJS
{
    public partial class XMList : SharedPage
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
            bmWhere = " (StartDeptGuid='" + bmGuid + "' and ProState='申报')";

            //此处查看所有审核通过的项目
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                roleWhere = " ProState='申报'";
            }

            if (roleWhere != "")
            {
                sqlWhere = baseWhere + " and (" + bmWhere + " or " + roleWhere + ")";
            }
            else
            {
                sqlWhere = baseWhere + " and (" + bmWhere + ")";
            }

        }
    }
}