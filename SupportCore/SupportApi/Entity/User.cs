using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.SupportCore.SupportApi.EntityHelper;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Yawei.SupportCore.SupportApi.Entity
{
    [Table("Sys_Users")]
    public class User
    {
        [Key]
        public string UserGuid { set; get; }
        public string UserDN { set; get; }
        public string UserCN { set; get; }
        public string UserLoginName { set; get; }
        public string UserProperty { set; get; }
        public string UserType { get; set; }



        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns>返回用户集合</returns>
        public List<Role> GetRole()
        {
            return ModelHelper.GetUserRole(UserGuid);
        }


        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns>返回用户集合</returns>
        public List<List<Role>> GetRoleTree()
        {
            return ModelHelper.GetUserRoleTree(UserGuid);
        }

        /// <summary>
        /// 获取用户租
        /// </summary>
        /// <returns></returns>
        public Group GetGroup()
        {
            return ModelHelper.GetUserGroup(UserGuid);
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns><menus><menu Href='' Name='Name'><menu></menu></menu></menus></returns>
        public System.Xml.XmlDocument GetMenuXml()
        {
            return ModelHelper.GetMenuXml(UserGuid);
        }

        /// <summary>
        /// 指定节点的用户权限菜单
        /// </summary>
        /// <param name="menuGuid">菜单节点</param>
        /// <returns><menus><menu Href='' Name='Name'><menu></menu><menu></menu></menu></menus></returns>
        public XmlDocument GetMenuXml(string menuGuid)
        {
            return ModelHelper.GetMenuXml(UserGuid, menuGuid);
        }

        /// <summary>
        /// 获取菜单json
        /// </summary>
        /// <param name="path">虚拟目录</param>
        /// <returns></returns>
        public string GetMenuJSON(string path)
        {
            string json = ModelHelper.GetMenuJSON(UserGuid, path);
            return json;
        }

        /// <summary>
        /// 获取菜单json
        /// </summary>
        /// <param name="path">虚拟目录</param>
        /// <returns></returns>
        public string GetMenuJSON(string path,string menuGuid)
        {
            string json = ModelHelper.GetMenuJSON(UserGuid,menuGuid, path);
            return json;
        }

        /// <summary>
        /// 判断用户角色
        /// </summary>
        /// <param name="roleGuid">角色主键</param>
        /// <returns></returns>
        public bool IsHasRole(string roleGuid)
        {
            return ModelHelper.IsHasRole(roleGuid, UserGuid);
        }

        /// <summary>
        /// 判断用户角色
        /// </summary>
        /// <param name="roleGuids">角色数组</param>
        /// <returns></returns>
        public bool IsHasRole(string[] roleGuids)
        {
            return ModelHelper.IsHasRole(roleGuids, UserGuid);
        }

        /// <summary>
        /// 判断用户模块权限
        /// </summary>
        /// <param name="mid">模块号</param>
        /// <param name="classPath">命名空间全名</param>
        /// <returns></returns>
        public bool IsHasModel(string mid,string classPath)
        {
            return ModelHelper.IsHasModel(mid,classPath,UserGuid);
        }

        /// <summary>
        /// 根据用户主键获取用户信息
        /// </summary>
        /// <param name="guid">用户主键</param>
        /// <returns>用户实体类</returns>
        public static User GetUser(string userGuid)
        {
            return ModelHelper.GetUser(userGuid);
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int AddUser(User user)
        {
            return ModelHelper.AddUser(user);
        }

        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int UpdateUser(User user)
        {
            return ModelHelper.UpdateUser(user);
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int DeleteUser(User user)
        {
            return ModelHelper.UpdateUser(user);
        }
    }
}
