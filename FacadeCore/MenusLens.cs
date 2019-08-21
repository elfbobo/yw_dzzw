using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.SupportCore.SupportApi.Entity;
using System.Web;

namespace Yawei.FacadeCore
{
    /// <summary>
    /// 判断行业监管菜单
    /// </summary>
    public static class MenusLens
    {
        /// <summary>
        /// 判断行业监管是否具有权限
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static bool UserIsIndustryMage(object userGuid)
        {
            bool b = false;
            if (HttpRuntime.Cache[userGuid + "asd"] == null)
            {
                User user = User.GetUser(userGuid.ToString());
                b = user.IsHasRole(Yawei.Common.AppSupport.industryMage);
                HttpRuntime.Cache[userGuid + "asd"] = b;
            }
            else
            {
                b = (bool)HttpRuntime.Cache[userGuid + "asd"];
            }
            return b;
        }
    }
}
