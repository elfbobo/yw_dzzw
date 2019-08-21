using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Support;
using Yawei.Common;
using Yawei.FacadeCore.Project;
using System.Configuration;
using WebApp.AppCode;


namespace WebApp.TZ_XMGL
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

            //查看本部门的项目
            bmWhere = " StartDeptGuid='" + bmGuid + "'";

            //此处查看所有审核通过的项目
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                roleWhere = "ProState='申报'";
            }

            if (roleWhere != "")
            {
                sqlWhere = baseWhere + "(" + bmWhere + " or " + roleWhere + ")";
            }
            
        }
    }
}