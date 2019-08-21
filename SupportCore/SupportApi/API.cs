//2013-10
//田飞飞第一版

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.SupportCore.SupportApi.DBContext;
using System.Configuration;
using Yawei.SupportCore.SupportApi.Entity;
using Yawei.SupportCore.SupportApi.EntityHelper;

namespace Yawei.SupportCore.SupportApi
{
    public  class API
    {
        string connStr = "";

        //public string ProjGuid
        //{
        //    get { return projGuid; }
        //    set { this.projGuid = value; }
        //}

        public API()
        {
            if (ConfigurationManager.GetSection("Yawei.SupportCore.SupportApi") == null)
                throw new Exception("配置文件中找不到Yawei.Application.SystemAPI节点");
            Dictionary<string, string> systemInfo = ConfigurationManager.GetSection("Yawei.SupportCore.SupportApi") as Dictionary<string, string>;
            if (string.IsNullOrEmpty(systemInfo["defaultDatabase"]))
                throw new Exception("主键和数据库连接字符串不能为空");
            connStr = systemInfo["defaultDatabase"];

        }

        public API( string conStr)
        {
            if (string.IsNullOrEmpty(connStr))
                throw new Exception("主键和数据库连接字符串不能为空");
            connStr = conStr;
        
        }

        public SysDbContext CreateDbContext()
        {
            return new SysDbContext(connStr);
        }

        public SysDbContext CreateDbContext(string conStr)
        {
            return new SysDbContext(conStr);
        }

        /// <summary>
        /// 根据用户主键获取用户信息
        /// </summary>
        /// <param name="guid">用户主键</param>
        /// <returns>用户实体类</returns>
        public  User GetUser(string userGuid)
        {
            return User.GetUser(userGuid);
        }

        /// <summary>
        /// 创建一个用户
        /// </summary>
        /// <returns></returns>
        public User CreateUser()
        {
            return new User();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public Role GetRole(string roleGuid)
        {
            return Role.GetRoleByGuid(roleGuid);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <returns></returns>
        public Role CreateRole()
        {
            return new Role();
        }

        /// <summary>
        /// 获取工作组
        /// </summary>
        /// <param name="groupGuid"></param>
        /// <returns></returns>
        public Group GetGroup(string groupGuid)
        {
            return Group.GetGroupByGuid(groupGuid);
        }

        /// <summary>
        /// 创建工作组
        /// </summary>
        /// <returns></returns>
        public Group CreateGroup()
        {
            return new Group();
        }

        
    }
}
