using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Yawei.SupportCore.SupportApi.EntityHelper;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_Roles")]
    public class Role
    {
        [Key]
        public string Guid { set; get; }
        public string TopGuid { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }



        /// <summary>
        /// 判断用户角色
        /// </summary>
        /// <param name="roleGuid">角色主键</param>
        /// <returns></returns>
        public bool IsHasRole(string userGuid)
        {
            return ModelHelper.IsHasRole(Guid, userGuid);
        }

        /// <summary>
        /// 获取角色的所有用户
        /// </summary>
        /// <returns></returns>
        public List<User> GetUser()
        {
            return ModelHelper.GetRoleUser(Guid);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public static Role GetRoleByGuid(string roleGuid)
        {
            return ModelHelper.GetRoleByGuid(roleGuid);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public static List<Role> GetRoleByName(string roleName)
        {
            return ModelHelper.GetRoleByName(roleName);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int AddRole(Role role)
        {
            return ModelHelper.AddRole(role);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int AddRole(List<Role> role)
        {
            return ModelHelper.AddRole(role);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int UpdateRole(Role role)
        {
            return ModelHelper.UpdateRole(role);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int DeleteRole(Role role)
        {
            return ModelHelper.DeleteRole(role);
        }
    }
}
