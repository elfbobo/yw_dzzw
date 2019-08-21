using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Yawei.Common;
using Yawei.SupportCore.SupportApi.Entity;


namespace WebApp.AppCode
{
    public class RoleCheck
    {
        CurrentUser curUser;
        string strRole;
        public RoleCheck(CurrentUser curUser)
        {
            this.curUser = curUser;
            this.strRole = "";
            List<Role> roles = this.curUser.GetRole();
            foreach (Role role in roles)
            {
                strRole += role.Name + ",";
            }
        }

        /// <summary>
        /// 是否管理员用户
        /// </summary>
        /// <returns></returns>
        public bool isAdmin()
        {
            if (strRole.IndexOf("管理员") >= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否政府办用户
        /// </summary>
        /// <returns></returns>
        public bool isZfb()
        {
            if (strRole.IndexOf("政府办") >= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否审计局用户
        /// </summary>
        /// <returns></returns>
        public bool isSjj()
        {
            if (strRole.IndexOf("审计局") >= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否部门用户
        /// </summary>
        /// <returns></returns>
        public bool isBm()
        {
            if (strRole.IndexOf("部门") >= 0)
            {
                return true;
            }
            return false;
        }

        public bool isFgwAdmin() {
            if (strRole.IndexOf("政府投资项目管理员") >= 0)
            {
                return true;
            }
            return false;
        }

        public  bool hasRole(string checkRole,CurrentUser curUser)
        {

            string allRoles = ConfigurationManager.AppSettings[checkRole];

            List<Role> userRoles = curUser.GetRole();

            foreach(Role role in userRoles)
            {
                if (allRoles.IndexOf(role.Name)>=0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}