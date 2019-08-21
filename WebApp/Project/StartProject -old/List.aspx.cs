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
        protected string bm = "";
        protected string bmwhere = "";
        protected bool isAdmin = false;

        protected bool isCheck = false;

        protected bool isBaseRole = false;

        protected string baseWhere = "";
        protected string roleWhere = "";
        protected string sqlWhere = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RoleCheck.hasRole("adminrole", CurrentUser))
            {
                isAdmin = true;
                //管理员能查看自己新建的项目或者 部门已提交或者通过或者退回的项目
                roleWhere = " and (StartDeptGuid='" + bm + "' or (ProState is not null and ProState!='退回'))";
            }
            else
            {
                if (RoleCheck.hasRole("checkrole", CurrentUser))
                {
                    isCheck = true;
                }
                else
                {
                    isBaseRole = true;
                    //部门只能查看本部门的项目
                    roleWhere = " and StartDeptGuid='" + bm + "'";
                }
            }

            //未删除
            baseWhere = " and sysstatus!=-1";

            sqlWhere = baseWhere + roleWhere;
        }
    }
}