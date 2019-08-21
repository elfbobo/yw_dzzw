//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System;
using System.Configuration;
using Yawei.DataAccess.Common;
using Yawei.DataAccess.Odbc;
using Yawei.DataAccess.OleDb;
using Yawei.DataAccess.Oracle;
using Yawei.DataAccess.SqlClient;
using System.Xml;

namespace Yawei.DataAccess
{
    /// <summary>
    /// 表示一组静态方法，这些方法用于创建数据库的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public static class DatabaseFactory
    {
        #region 变量

        private const string _defaultDatabaseProperty = "defaultDatabase";
        private const string _sectionName = "connectionName";

        #endregion

        #region 方法

        /// <summary>
        /// 创建一个数据库的实例。
        /// </summary>
        /// <param name="connectionString">表示数据库连接字符串。</param>
        /// <param name="providerName">表示数据库提供程序。</param>
        /// <returns>返回一个数据库的实例。</returns>
        public static Database CreateDatabase(string connectionString,string providerName)
        {
            Type type = Type.GetType(providerName);
            Type[] constructorParams = new Type[1] { typeof(String)};
            Database database = (Database)type.GetConstructor(constructorParams).Invoke(new object[1] { connectionString });

            return database;
        }

        /// <summary>
        /// 创建一个数据库的实例。
        /// </summary>
        /// <param name="connectionName">配置文件中 ConnectionStrings 节点中的配置名称。</param>
        /// <returns>返回一个数据库的实例。</returns>
        public static Database CreateDatabase(string connectionName)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings == null) throw new Exception("配置文件中找不到connectionStrings节点");
            if (settings[connectionName] == null) throw new Exception("connectionStrings节点中找不到名为 " + connectionName + " 的配置");

            Type type = Type.GetType(settings[connectionName].ProviderName);
            Type[] constructorParams = new Type[1] { typeof(String) };
            Database database = (Database)type.GetConstructor(constructorParams).Invoke(new object[1] { settings[connectionName].ConnectionString });

            return database;
        }

        /// <summary>
        /// 从配置文件中读取默认的 ConnectionStrings 配置创建一个数据库的实例。
        /// </summary>
        /// <returns>返回一个数据库的实例。</returns>
        public static Database CreateDatabase()
        {
            if (ConfigurationManager.GetSection("Yawei.DataAccess") == null) throw new Exception("配置文件中找不到Yawei.DataAccess.Data节点");
            string defaultDatabase = ConfigurationManager.GetSection("Yawei.DataAccess").ToString();
            return CreateDatabase(defaultDatabase.ToString());
        }

        #endregion
    }
}
