using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;
using Yawei.SupportCore.SupportApi.Entity;
using WebApp.AppCode;

namespace WebApp.Project.StartProject
{
    public partial class List : SharedPage
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

            //只能查看未删除的
            baseWhere = " and sysstatus!=-1";

            //部门只能查看自己的项目
            bmwhere = " StartDeptGuid='" + bmGuid + "' ";

            //根据用户角色的查询条件
            if (roleCheck.isAdmin())
            {
                //管理员查看所有提交或者退回或者申报通过的项目
                //roleWhere = " (ProState='提交' or ProState='申报' or ProState='退回' or ProState is NULL)";
                roleWhere = " (ProState='提交' or ProState='申报' or ProState='退回' or ProState='暂缓' or ProState='整合')";
            }
            //政府办用户（非管理员）
            else if (!roleCheck.isAdmin()&&roleCheck.isZfb())
            {
                //政府办查看所有提交或者退回或者申报通过的项目
                roleWhere = " (ProState='提交' or ProState='申报' or ProState='退回' or ProState='暂缓' or ProState='整合')";

            }
            //审计局
            else if (!roleCheck.isAdmin() && !roleCheck.isZfb() && roleCheck.isSjj())
            {
                //审计局只能查看所有审核通过的项目，不可编辑更改
                roleWhere = " ProState='申报'";
            }

            if (roleWhere != "")
            {
                sqlWhere = baseWhere + " and " + "(" + bmwhere + "or" + roleWhere + ")";
            }
            else
            {
                sqlWhere = baseWhere + " and " + "(" + bmwhere + ")";
            }
            
            //能查看自己的

            //if (RoleCheck.hasRole("adminrole", CurrentUser))
            //{
            //    isAdmin = true;
            //    //管理员能查看自己新建的项目或者 部门已提交或者通过或者退回的项目
            //    roleWhere = " and (StartDeptGuid='" + bm + "' or (ProState is not null and ProState!='退回'))";
            //}
            //else
            //{
            //    if (RoleCheck.hasRole("checkrole", CurrentUser))
            //    {
            //        isCheck = true;
            //    }
            //    else
            //    {
            //        isBaseRole = true;
            //        //部门只能查看本部门的项目
            //        roleWhere = " and StartDeptGuid='" + bm + "'";
            //    }
            //} 
        }
    }
}