using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Yawei.SupportCore.SupportApi.EntityHelper;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_RoleUsers")]
    public class RoleUser
    {
        [Key, Column(Order = 0)]
        public string UserGuid { set; get; }
        [Key, Column(Order = 1)]
        public string RoleGuid { set; get; }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="roleUser"></param>
        /// <returns></returns>
        public static int AddRoleUser(RoleUser roleUser)
        {
            return ModelHelper.AddRoleUser(roleUser);
        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="roleUser"></param>
        /// <returns></returns>
        public static int AddRoleUser(List<RoleUser> roleUser)
        {
            return ModelHelper.AddRoleUser(roleUser);
        }

        /// <summary>
        /// 删除组用户
        /// </summary>
        /// <param name="roleUser"></param>
        /// <returns></returns>
        public static int DeleteRoleUser(RoleUser roleUser)
        {
            return ModelHelper.DeleteRoleUser(roleUser);
        }
    }
}
