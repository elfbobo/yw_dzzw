//2013-10  田飞飞
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yawei.SupportCore.SupportApi.Entity;
using System.Web;

namespace Yawei.Common
{
    public class CurrentUser
    {
        public string UserGuid { get; set; }
        public string UserDN { set; get; }
        public string UserCN { set; get; }
        public string UserLoginName { set; get; }
        public string UserProperty { set; get; }
        public string UserType { get; set; }
        public List<Role> UserRole { get; set; }
        public Group UserGroup { get; set; }
        public List<List<Role>> UserRoleTree { get; set; }

        /// <summary>
        /// 只在在线用户时有用
        /// </summary>
        public DateTime LoginDate { get; set; }
        public User user = null;


        public CurrentUser()
        {
            
        }

        public CurrentUser(string userGuid)
        {
            user = User.GetUser(userGuid);
            if (user != null)
            {
                SetUsetAttribute();
            }
        }

        void SetUsetAttribute(CurrentUser caheUser)
        {
            user = caheUser.user;
            UserCN = caheUser.UserCN;
            UserDN = caheUser.UserDN;
            UserGuid = caheUser.UserGuid;
            UserLoginName = caheUser.UserLoginName;
            UserProperty = caheUser.UserProperty;
            UserType = caheUser.UserType;
            UserRole = caheUser.UserRole;
            UserGroup = caheUser.UserGroup;
            UserRoleTree = caheUser.UserRoleTree;
        }

        void SetUsetAttribute()
        {

            UserCN = user.UserCN;
            UserDN = user.UserDN;
            UserGuid = user.UserGuid;
            UserLoginName = user.UserLoginName;
            UserProperty = user.UserProperty;
            UserType = user.UserType;
            UserGroup = user.GetGroup();

            UserRole = user.GetRole();
            UserRoleTree = user.GetRoleTree();
           
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns>返回用户集合</returns>
        public List<Role> GetRole()
        {

            if (user == null)
                user = User.GetUser(UserGuid);

            return user.GetRole();
        }

        /// <summary>
        /// 获取用户租
        /// </summary>
        /// <returns></returns>
        public Group GetGroup()
        {
            if (user == null)
                user = User.GetUser(UserGuid);
            return user.GetGroup();
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public System.Xml.XmlDocument GetMenuXml()
        {
            //var cahe = HttpRuntime.Cache;
            var xml = new System.Xml.XmlDocument();
            user = User.GetUser(UserGuid);
            xml = user.GetMenuXml();
            //if (cahe[UserRole[0].Guid + "xml"] == null)
            //{
            //    if (user == null)
            //        user = User.GetUser(UserGuid);
            //    xml = user.GetMenuXml();
            //    System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency("YAWEISysApiStr", "Sys_MenuLicenses");
            //    cahe.Insert(UserRole[0].Guid + "xml", xml, null, DateTime.Now.AddMinutes(22), TimeSpan.Zero);
            //}
            //else
            //{
            //    xml = cahe[UserRole[0].Guid + "xml"] as System.Xml.XmlDocument;

            //}
            return xml;
        }

        /// <summary>
        /// 指定节点的用户权限菜单
        /// </summary>
        /// <param name="menuGuid">菜单节点</param>
        /// <returns></returns>
        public XmlDocument GetMenuXml(string menuGuid)
        {

            if (user == null)
                user = User.GetUser(UserGuid);
            return user.GetMenuXml(menuGuid);
        }

        /// <summary>
        /// 获取菜单json
        /// </summary>

        /// <returns></returns>
        public string GetMenuJSON()
        {

            string json = "";
            user = User.GetUser(UserGuid);
            json = user.GetMenuJSON(AppSupport.AppPath);
            //var cahe = HttpRuntime.Cache;
            //if (cahe[UserRole[0].Guid + "json"] == null)
            //{
            //    if (user == null)
            //        user = User.GetUser(UserGuid);
            //    json = user.GetMenuJSON(AppSupport.AppPath);
            //    System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency("YAWEISysApiStr", "Sys_MenuLicenses");
            //    cahe.Insert(UserRole[0].Guid + "json", json, dep, DateTime.Now.AddMinutes(22), TimeSpan.Zero);
            //}
            //else
            //{
            //    json = cahe[UserRole[0].Guid + "json"].ToString();
            //}
            return json;
        }

        /// <summary>
        /// 获取菜单json
        /// </summary>
        /// <param name="path">虚拟目录</param>
        /// <returns></returns>
        public string GetMenuJSON(string menuGuid)
        {
            if (user == null)
                user = User.GetUser(UserGuid);
            string json = user.GetMenuJSON( AppSupport.AppPath,menuGuid);
            return json;
        }

        /// <summary>
        /// 判断用户角色
        /// </summary>
        /// <param name="roleGuid">角色主键</param>
        /// <returns></returns>
        public bool IsHasRole(string roleGuid)
        {
            if (UserRole != null && UserRole.Count > 0)
            {
                for (int i = 0; i < UserRole.Count; i++)
                {
                    if (UserRole[i].Guid == roleGuid)
                        return true;
                }
                return false;
            }
            else
            {

                if (user == null)
                    user = User.GetUser(UserGuid);

                return user.IsHasRole(roleGuid);
            }
        }

        /// <summary>
        /// 判断用户角色
        /// </summary>
        /// <param name="roleGuids">角色数组</param>
        /// <returns></returns>
        public bool IsHasRole(string[] roleGuids)
        {
            if (UserRole != null && UserRole.Count > 0)
            {
                for (int i = 0; i < UserRole.Count; i++)
                {
                    if (roleGuids.Contains(UserRole[i].Guid))
                        return true;
                }
                return false;
            }
            else
            {
                if (user == null)
                    user = User.GetUser(UserGuid);
                return user.IsHasRole(roleGuids);
            }
        }

        /// <summary>
        /// 判断用户模块权限
        /// </summary>
        /// <param name="mid">模块号</param>
        /// <param name="classPath">命名空间全名</param>
        /// <returns></returns>
        public bool IsHasModel(string mid, string classPath)
        {
            if (user == null)
                user = User.GetUser(UserGuid);
            return user.IsHasModel(mid, classPath);
        }

        /// <summary>
        /// 根据用户主键获取用户信息
        /// </summary>
        /// <param name="guid">用户主键</param>
        /// <returns>用户实体类</returns>
        public static User GetUser(string userGuid)
        {
            return User.GetUser(userGuid);
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int AddUser(User user)
        {
            return User.AddUser(user);
        }

        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int UpdateUser(User user)
        {
            return User.UpdateUser(user);
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int DeleteUser(User user)
        {
            return User.UpdateUser(user);
        }


        public System.Web.HttpCookie SetCookieUserInfo()
        {

            return SetCookieUserInfo(UserGuid, UserCN, UserDN, UserLoginName);
        }

        public static System.Web.HttpCookie SetCookieUserInfo(string userGuid, string CN, string DN, string ID)
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("CurrentUserInfo");
            cookie.HttpOnly = false;

            cookie.Values.Add("CurrentUserGuid", userGuid);

            cookie.Values.Add("CurrentUserID", ID);

            cookie.Values.Add("CurrentUserCN", CN);
            cookie.Values.Add("CurrentUserDN", DN);
            System.Web.HttpContext.Current.Response.AppendCookie(cookie);
            return cookie;
        }


        #region  菜单权限  2013-11-1


        /// <summary>
        /// 判断编辑菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasEdit()
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid)||IsHasRole(AppSupport.editRole))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool HasEdit(LogicDelegate le)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || le(UserGuid))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断编辑菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasOrEdit(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.editRole) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断编辑菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasOrEdit(LogicDelegateArr del, object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.editRole) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 判断编辑菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasAndEdit(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.editRole) && del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }   
        }

        /// <summary>
        /// 判断编辑菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasAndEdit(LogicDelegateArr del,object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.editRole) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断删除菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasDelete()
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.deleteRole))
                    return true;
                else
                    return false;
            }
            catch
            {
                return true;
            }
        }

        public bool HasDelete(LogicDelegate le)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || le(UserGuid))
                    return true;
                else
                    return false;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// 判断删除菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasAndDelete(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.deleteRole) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断删除菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasAndDelete(LogicDelegateArr del,  object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.deleteRole) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断删除菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasOrDelete(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.deleteRole) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断删除菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasOrDelete(LogicDelegateArr del,  object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.deleteRole) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 判断保存菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasSave()
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.saveRole))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool HasSave(LogicDelegate le)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || le(UserGuid))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断保存菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasAndSave(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.saveRole) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断保存菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasAndSave(LogicDelegateArr del,  object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.saveRole) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断删除菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasOrSave(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.saveRole) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断保存菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasOrSave(LogicDelegateArr del,  object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.saveRole) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 其他菜单
        /// </summary>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool OtherOrMenu(LogicDelegateArr del,  object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.adminGuid) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 其他菜单
        /// </summary>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool OtherOrMenu(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.adminGuid) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 其他菜单
        /// </summary>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool OtherAndMenu(LogicDelegateArr del,  object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.adminGuid) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 其他菜单
        /// </summary>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool OtherAndMenu(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.adminGuid) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }





        ////////

        /// <summary>
        /// 判断审核菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasConfrim()
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.confrimRole))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool HasConfrim(LogicDelegate le)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || le(UserGuid))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断审核菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasAndConfrim(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.confrimRole) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断审核菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasAndConfrim(LogicDelegateArr del, object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || (IsHasRole(AppSupport.confrimRole) && del(obj)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断审核菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasOrConfrim(LogicDelegate del, object obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.confrimRole) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///判断审核菜单
        /// </summary>
        /// <param name="current"></param>
        /// <param name="del"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasOrConfrim(LogicDelegateArr del, object[] obj)
        {
            try
            {
                if (IsHasRole(AppSupport.adminGuid) || IsHasRole(AppSupport.confrimRole) || del(obj))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion


    }

    public delegate bool LogicDelegate(object obj);

    public delegate bool LogicDelegateArr(object[] obj);
}
