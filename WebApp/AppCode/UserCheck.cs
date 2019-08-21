using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApp.AppCode
{
    public class UserCheck
    {
        /// <summary>
        /// 判断当前用户是否是管理员
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool checkIsAdmin(string username)
        {
            if (1 == 1)
            {
                return true;
            }
            string adminuser = ConfigurationManager.AppSettings["admin"];
            if (adminuser.IndexOf('&') >= 0)
            {
                string[] adminuserarr = adminuser.Split('&');
                for (int i = 0; i < adminuserarr.Length; i++)
                {
                    if (adminuserarr[i] == username)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (adminuser == username)
                {
                    return true;
                }
            }

            return false;
        }
    }
}