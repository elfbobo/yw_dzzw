using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;

namespace WebApp.TZ_JXKH
{

    public partial class XMList : SharedPage
    {
        protected string bmGuid = "";

        protected string baseWhere = "";
        protected string bmwhere = "";
        protected string roleWhere = "";
        protected string sqlWhere = "";

        protected RoleCheck roleCheck;

        protected void Page_Load(object sender, EventArgs e)
        {

            //获取角色检查器
            roleCheck = new RoleCheck(CurrentUser);
            bmGuid = CurrentUser.UserGroup.Guid;

            //只能查看未删除的以及已经验收过的
            baseWhere = " and sysstatus!=-1 and ProState='申报' ";

            //部门只能查看自己的项目
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                sqlWhere = baseWhere + " ";
            }
            else
            {
                sqlWhere = baseWhere+" and StartDeptGuid='" + bmGuid + "' ";
            }

        }
    }
}