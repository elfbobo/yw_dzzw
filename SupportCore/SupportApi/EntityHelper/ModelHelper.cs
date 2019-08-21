using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Yawei.SupportCore.SupportApi.DBContext;
using System.Data.SqlClient;
using Yawei.SupportCore.SupportApi.Entity;
using System.Xml;

namespace Yawei.SupportCore.SupportApi.EntityHelper
{
    public static class ModelHelper
    {
        #region  用户
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static User GetUser(string userGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                return context.Users.Find(userGuid);
            }
        }


        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static User GetUser(SysDbContext context, string userGuid)
        {
            return context.Users.Find(userGuid);
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int AddUser(User user)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Users.Add(user);
                return context.SaveChanges();
            }

        }

        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int UpdateUser(User user)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(user).State = System.Data.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int DeleteUser(User user)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(user).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }

        }
        #endregion

        #region 角色
        /// <summary>
        ///根据用户主键获取用户角色
        /// </summary>
        /// <param name="userGuid">用户主键</param>
        /// <returns>用户集合</returns>
        public static List<Role> GetUserRole(string userGuid)
        {
            API api = new API();
            List<Role> roleList = null;
            using (SysDbContext context = api.CreateDbContext())
            {
                var role = from r in context.Roles
                           where
                               (
                               from ru in context.RoleUsers where ru.UserGuid == userGuid select ru.RoleGuid
                               ).Contains(r.Guid) 
                           select r;

                roleList = role.ToList();
                context.Dispose();
            }
            return roleList;
        }


        /// <summary>
        ///根据用户主键获取用户角色
        /// </summary>
        /// <param name="userGuid">用户主键</param>
        /// <returns>用户集合</returns>
        public static List<List<Role>> GetUserRoleTree(string userGuid)
        {
            API api = new API();
            List<List<Role>> roleTree = new List<List<Role>>();
            List<Role> roleList = null;
            using (SysDbContext context = api.CreateDbContext())
            {
                var role = from r in context.Roles
                           where
                               (
                               from ru in context.RoleUsers where ru.UserGuid == userGuid select ru.RoleGuid
                               ).Contains(r.Guid)
                           select r;

                roleList = role.ToList();

                var allRole = context.Roles.ToList();


                List<Role> roleTemp = null;
                for (int i = 0; i < roleList.Count; i++)
                {
                    var topguid = roleList[i].TopGuid;
                    var rolet = new List<Role>();
                    rolet.Add(roleList[i]);
                    while (true)
                    {
                        roleTemp = allRole.Where(r => r.Guid == topguid).ToList();
                        if (roleTemp.Count > 0)
                        {
                            rolet.Add(roleTemp[0]);
                            topguid = roleTemp[0].TopGuid;
                        }
                        else
                        {
                            break;
                        }
                    }
                    rolet.Reverse();
                    roleTree.Add(rolet);
                }

                context.Dispose();
            }
            return roleTree;
        }

        /// <summary>
        /// 判断用户的角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static bool IsHasRole(string roleGuid, string userGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var count = context.RoleUsers.Where(ru => ru.RoleGuid == roleGuid && ru.UserGuid == userGuid && context.Roles.Where(r => r.Guid == roleGuid).Count() > 0).Count();
                if (count > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public static Role GetRoleByGuid(string roleGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var roleList = context.Roles.Where(r => r.Guid == roleGuid ).ToList();
                if (roleList.Count > 0)
                {
                    return roleList[0];
                }
                return null;
            }
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public static List<Role> GetRoleByName(string name)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var roleList = context.Roles.Where(r => r.Name.Contains(name) ).ToList();
                if (roleList.Count > 0)
                {
                    return roleList;
                }
                return null;
            }
        }

        /// <summary>
        /// 判断用户是否存在角色数据中
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static bool IsHasRole(string[] roleGuid, string userGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var newRole = context.Roles.Where(r => roleGuid.Contains(r.Guid)).Select(r=>r.Guid).ToList();
                if (newRole.Count > 0)
                {
                    var count = context.RoleUsers.Count(c => c.UserGuid == userGuid && newRole.Contains(c.RoleGuid));
                    if (count > 0)
                        return true;
                    else
                        return false;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 获取角色的用户
        /// </summary>
        /// <param name="roleGuid">角色主键</param>
        /// <returns>用户集合</returns>
        public static List<User> GetRoleUser(string roleGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var list = (from user in context.Users
                            where (
                                from ug in context.RoleUsers where ug.RoleGuid == roleGuid && (from r in context.Roles where r.Guid == roleGuid  select r.Guid).Count() > 0 select ug.UserGuid
                                ).Contains(user.UserGuid)
                            select user).ToList();
                return list;
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int AddRole(Role role)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Roles.Add(role);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int AddRole(List<Role> roles)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                foreach (Role r in roles)
                    context.Roles.Add(r);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int UpdateRole(Role role)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(role).State = System.Data.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int DeleteRole(Role role)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(role).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }
        }


        /// <summary>
        /// 用户添加到组
        /// </summary>
        /// <param name="roleUser"></param>
        /// <returns></returns>
        public static int AddRoleUser(RoleUser roleUser)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.RoleUsers.Add(roleUser);
                return context.SaveChanges();
            }
        }


        /// <summary>
        /// 用户添加到组
        /// </summary>
        /// <param name="roleUser"></param>
        /// <returns></returns>
        public static int AddRoleUser(List<RoleUser> roleUser)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                foreach (RoleUser ru in roleUser)
                    context.RoleUsers.Add(ru);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 组删除用户
        /// </summary>
        /// <param name="roleUser"></param>
        /// <returns></returns>
        public static int DeleteRoleUser(RoleUser roleUser)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(roleUser).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        #endregion

        #region 工作组

        /// <summary>
        /// 获取所有工作组
        /// </summary>
        /// <returns></returns>
        public static List<Group> GetGroup()
        {
            API api = new API();

            using (SysDbContext context = api.CreateDbContext())
            {
                return context.Groups.OrderBy(g => g.SortNum).ToList();
            }
        }

        /// <summary>
        /// 根据用户主键获取用户工作组
        /// </summary>
        /// <param name="userGuid">用户主键</param>
        /// <returns>返回工作实体类</returns>
        public static Group GetUserGroup(string userGuid)
        {
            API api = new API();

            using (SysDbContext context = api.CreateDbContext())
            {
                var groupUser = context.Database.SqlQuery<string>("select UserGroupGuid from Sys_UserAndGroup where UserGuid=@UserGuid", new SqlParameter[] {
                    new SqlParameter("@UserGuid",userGuid),
                }).ToArray();
                var group = from g in context.Groups where groupUser.Contains(g.Guid)  select g;
                if (group.Count() > 0)
                    return group.First();
                else
                    return null;
            }
        }

        /// <summary>
        /// 根据Guid获取组
        /// </summary>
        /// <param name="groupGuid"></param>
        /// <returns></returns>
        public static Group GetGroupByGuid(string groupGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var groupList = context.Groups.Where(g => g.Guid == groupGuid ).ToList();
                if (groupList.Count > 0)
                {
                    return groupList[0];
                }
                return null;
            }
        }

        /// <summary>
        /// 获取组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Group> GetGroupByName(string name)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var groupList = context.Groups.Where(g => g.Name.Contains(name) ).ToList();
                if (groupList.Count > 0)
                {
                    return groupList;
                }
                return null;
            }
        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static int AddGroup(Group group)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Groups.Add(group);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 更新组
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static int UpdateGroup(Group group)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(group).State = System.Data.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static int DeleteGroup(Group group)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(group).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }
        }


        /// <summary>
        /// 获取组的全部用户
        /// </summary>
        /// <param name="groupGuid"></param>
        /// <returns></returns>
        public static List<User> GetGroupUser(string groupGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                if (context.Groups.Where(g => g.Guid == groupGuid).Count() > 0)
                {
                    var userGuid = context.Database.SqlQuery<string>("select userGuid from Sys_UserAndGroup where UserGroupGuid=@UserGroupGuid", new SqlParameter("@UserGroupGuid", groupGuid)).ToArray();
                    var userList = from u in context.Users
                                   where userGuid.Contains(u.UserGuid)
                                   select u;

                    return userList.ToList();
                }
                return null;
            }
        }

        /// <summary>
        /// 添加用户到组
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="groupGuid"></param>
        /// <returns></returns>
        public static int AddGroupUser(string userGuid, string groupGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                return context.Database.ExecuteSqlCommand("insert into Sys_UserAndGroup (UserGuid,UserGroupGuid) values(@UserGuid,@UserGroupGuid)", new SqlParameter[]{
                    new SqlParameter("@UserGuid",userGuid),new SqlParameter("@UserGroupGuid",groupGuid)
                });

            }
        }


        public static XmlDocument GetGroupAndUserXML()
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var userInGroup = (from ug in context.UserInGroup
                                   where
                                   (
                                        from g in context.Groups
                                       
                                        select g.Guid

                                   ).Contains(ug.UserGroupGuid)
                                   select ug).ToList();
                var group = GetGroup();
                var user = (from u in context.Users
                            where
                            (from ug in context.UserInGroup
                             where
                             (
                                  from g in context.Groups
                                 
                                  select g.Guid

                             ).Contains(ug.UserGroupGuid)
                             select ug.UserGuid
                                ).Contains(u.UserGuid)
                            select u).ToList();

                XmlDocument document = new XmlDocument();

                var gropRoot = group.Where(g => g.TopGuid == "-1").ToList();
                XmlElement ele = document.CreateElement("Groups");
                document.AppendChild(ele);
                for (int i = 0; i < gropRoot.Count; i++)
                {
                    XmlElement child = document.CreateElement("Group");
                    child.SetAttribute("Guid", gropRoot[i].Guid);
                    child.SetAttribute("Name", gropRoot[i].Name);
                    GetRoleUser(userInGroup, user, gropRoot[i].Guid, child, document);
                    GetChidGroup(group, gropRoot[i].Guid, child, document, userInGroup, user);
                    ele.AppendChild(child);
                }

                return document;
            }
        }

        public static string GetGroupAndUserJson()
        {
            XmlDocument document = GetGroupAndUserXML();

            return Action.GetNodeJson(document.FirstChild);
        }

        static void GetRoleUser(List<UserInGroup> userInGroup, List<User> user, string roleGuid, XmlElement parent, XmlDocument document)
        {
            //
            var groupUser = user.Where(u => userInGroup.Where(ug => ug.UserGroupGuid == roleGuid).Select(us => us.UserGuid).Contains(u.UserGuid));
            XmlElement ele = null;
            foreach (User u in groupUser)
            {
                ele = document.CreateElement("User");
                ele.SetAttribute("Guid", u.UserGuid);
                ele.SetAttribute("UserCN", u.UserCN);
                ele.SetAttribute("UserDN", u.UserDN);
                parent.AppendChild(ele);
            }
        }

        static void GetChidGroup(List<Group> groupList, string topGuid, XmlElement parent, XmlDocument document, List<UserInGroup> userInGroup, List<User> user)
        {
            var group = groupList.Where(g => g.TopGuid == topGuid).ToList();
            if (group.Count > 0)
            {
                XmlElement ele = null;
                foreach (Group g in group)
                {
                    ele = document.CreateElement("Group");
                    ele.SetAttribute("Guid", g.Guid);
                    ele.SetAttribute("Name", g.Name);
                    GetRoleUser(userInGroup, user, g.Guid, ele, document);
                    GetChidGroup(groupList, g.Guid, ele, document, userInGroup, user);
                    parent.AppendChild(ele);
                }
            }
        }

        #endregion

        #region 菜单
        /// <summary>
        /// 根据用户主键返回用户的的权限菜单
        /// </summary>
        /// <param name="userGuid">用户主键</param>
        /// <returns>返回有层级关系的XML对象</returns>
        public static System.Xml.XmlDocument GetMenuXml(string userGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var menu = from m in context.Menus
                           where (
                               from mr in context.MenusLicenses
                               where (
                                   from ru in context.RoleUsers where ru.UserGuid == userGuid select ru.RoleGuid
                                   ).Contains(mr.RoleGuid)
                               select mr.MenuGuid
                               ).Contains(m.Guid) 
                           orderby m.SortNum
                           select m;


                return Action.ForeachMenuList(menu.ToList(), "-1");
            }

        }


        /// <summary>
        /// 根据指定菜单的用户权限
        /// </summary>
        /// <param name="userGuid">用户主键</param>
        /// <param name="menuGuid">菜单主键</param>
        /// <returns>层级的XML</returns>
        public static System.Xml.XmlDocument GetMenuXml(string userGuid, string menuGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var menu = from m in context.Menus
                           where (
                               from mr in context.MenusLicenses
                               where (
                                   from ru in context.RoleUsers where ru.UserGuid == userGuid select ru.RoleGuid
                                   ).Contains(mr.RoleGuid)
                               select mr.MenuGuid
                               ).Contains(m.Guid) 
                           select m;

                return Action.ForeachMenuList(menu.ToList(), menuGuid);
            }

        }



        /// <summary>
        /// 根据用户主键获取菜单json
        /// </summary>
        /// <param name="userGuid">用户主键</param>
        /// <returns>json</returns>
        public static string GetMenuJSON(string userGuid, string path)
        {
            XmlDocument xml = GetMenuXml(userGuid);
            string json = Action.GetNodeJson(xml.FirstChild, path);
            return json;
        }

        /// <summary>
        /// 根据用户主键获取菜单json
        /// </summary>
        /// <param name="userGuid">用户主键</param>
        /// <returns>json</returns>
        public static string GetMenuJSON(string userGuid, string menuGuid, string path)
        {
            XmlDocument xml = GetMenuXml(userGuid, menuGuid);
            string json = Action.GetNodeJson(xml.FirstChild, path);
            return json;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static int AddMenu(Menu menu)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Menus.Add(menu);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static int DeleteMenu(Menu menu)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(menu).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static int UpdateMenu(Menu menu)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(menu).State = System.Data.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 添加菜单权限
        /// </summary>
        /// <param name="ml"></param>
        /// <returns></returns>
        public static int AddMenuLicenses(MenusLicenses ml)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.MenusLicenses.Add(ml);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除菜单权限
        /// </summary>
        /// <param name="ml"></param>
        /// <returns></returns>
        public static int DeleteMenuLicenses(MenusLicenses ml)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(ml).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }
        }
        #endregion

        #region 模块
        /// <summary>
        /// 判断用户的模块的权限
        /// </summary>
        /// <param name="mid">模块号</param>
        /// <param name="classPath">类的命名空间</param>
        /// <param name="userGuid">户名主键</param>
        /// <returns></returns>
        public static bool IsHasModel(string mid, string classPath, string userGuid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                var mlList = (from ml in context.ModelLicenses
                              where (
                                  from model in context.Models where model.Code == mid  && model.FullName.ToLower() == classPath.ToLower() select model.Guid
                                  ).Contains(ml.ModelGuid)
                              select ml).ToList();
                if (mlList.Count < 0)
                    return true;
                else
                {
                    var role = (from ru in context.RoleUsers where ru.UserGuid == userGuid select ru.RoleGuid).ToArray();
                    var licenses = (from tt in mlList where role.Contains(tt.RoleGuid) select tt).ToList();
                    if (licenses.Count < 0)
                        return false;
                    else
                    {
                        foreach (var mlcenses in licenses)
                        {
                            if (mlcenses.Type == 0)
                                return false;
                        }
                        return true;
                    }
                }
            }

        }


        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddModel(Model model)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Models.Add(model);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int DeleteModel(Model model)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(model).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateModel(Model model)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(model).State = System.Data.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 添加模块权限
        /// </summary>
        /// <param name="ml"></param>
        /// <returns></returns>
        public static int AddModelLicense(ModelLicenses ml)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.ModelLicenses.Add(ml);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除模块权限
        /// </summary>
        /// <param name="ml"></param>
        /// <returns></returns>
        public static int DeleteModelLicense(ModelLicenses ml)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(ml).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }
        }
        #endregion

        #region  字典

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int AddMapping(Mapping mapping)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Mapping.Add(mapping);
                return context.SaveChanges();
            }
        }


        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int AddMapping(List<Mapping> mapping)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                foreach (Mapping d in mapping)
                    context.Mapping.Add(d);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int DeleteMapping(Mapping mapping)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(mapping).State = System.Data.EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 更新字典
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static int UpdateMapping(Mapping dic)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                context.Entry(dic).State = System.Data.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 获取一组字典集合
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static List<Mapping> GetMappingByGuid(string guid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                return context.Mapping.Where(d => d.DirectoryGuid == guid).OrderBy(m => m.Mark).ToList();
            }
        }

        /// <summary>
        /// 根据code获取name
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string GetMappingNameByGuid(string guid)
        {
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                List<string> guidarr;
                if (guid.Contains(","))
                {
                    guidarr=guid.Split(',').ToList();
                    var dic = context.Mapping.Where(d => guidarr.Contains(d.Guid)).ToList();
                    if (dic.Count > 0)
                    {
                        string str = "";
                        foreach (var d in dic)
                        {
                            str +=d.Name+ ",";
                        }
                        return str.TrimEnd(',');
                    }
                }
                else
                {
                    var dic = context.Mapping.Where(d => d.Guid == guid).ToList();
                    if (dic.Count > 0)
                    {
                        return dic[0].Name;
                    }
                }
                return "";
            }
        }

        /// <summary>
        /// 根据code获取name
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string GetDicNameByCode(string code, string group)
        {
            //API api = new API();
            //using (SysDbContext context = api.CreateDbContext())
            //{
            //    var arr = Action.ConverToIntArr(code.Split(','));
            //    var dic = context.Dictionary.Where(d => d.Group == group && arr.Contains(d.Code) ).ToList();
            //    string str = string.Empty;
            //    for (var i = 0; i < dic.Count; i++)
            //    {
            //        if (i > 0)
            //            str += ",";
            //        str += dic[i].Name;
            //    }
            //    return str;
            //}
            return "";
        }

        /// <summary>
        /// 根据Value获取name
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string GetDicNameByValue(string guid)
        {
            //API api = new API();
            //using (SysDbContext context = api.CreateDbContext())
            //{
            //    if (guid.Contains(","))
            //    {
            //        var arr = guid.Split(',');
            //        var dic = context.Mapping.Where(d => d.Guid == guid).ToList();
            //        string str = string.Empty;
            //        for (var i = 0; i < dic.Count; i++)
            //        {
            //            if (i > 0)
            //                str += ",";
            //            str += dic[i].Name;
            //        }
            //        return str;
            //    }
            //    else
            //    {
            //        var dic = context.Mapping.Where(d => d.Guid == guid).ToList();
            //        if (dic.Count > 0)
            //        {
            //            return dic[0].Name;
            //        }
            //    }
            //    return "";
            //}

            return "";
        }

        /// <summary>
        /// 获取所有字典
        /// </summary>
        /// <returns></returns>
        public static List<Mapping> GetMappings()
        { 
            API api = new API();
            using (SysDbContext context = api.CreateDbContext())
            {
                return context.Mapping.ToList();
            }
        }
        #endregion
    }
}
