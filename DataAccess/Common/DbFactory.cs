//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System.Data.Common;

namespace Yawei.DataAccess.Common
{
    /// <summary>
    /// 表示一组方法，这些方法用于创建提供程序对数据源类的实现的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public abstract class DbFactory
    {
        #region 属性

        /// <summary>
        /// 获取或指定特定的数据库工厂是否支持 DbDataSourceEnumerator 类。
        /// </summary>
        public virtual bool CanCreateDataSourceEnumerator 
        {
            get { return true; }
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化数据库工厂的新实例。
        /// </summary>
        protected DbFactory() { }

        #endregion

        #region 方法

        /// <summary>
        /// 创建实现 DbCommand 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>返回 DbCommand 的新实例。</returns>
        public virtual DbCommand CreateCommand()
        {
            return null;
        }

        /// <summary>
        /// 创建实现 DbCommandBuilder 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>返回 DbCommandBuilder 的新实例。</returns>
        public virtual DbCommandBuilder CreateCommandBuilder()
        {
            return null;
        }

        /// <summary>
        /// 创建实现 DbConnection 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>返回 DbConnection 的新实例。</returns>
        public virtual DbConnection CreateConnection()
        {
            return null;
        }

        /// <summary>
        /// 创建实现 DbConnectionStringBuilder 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>返回 DbConnectionStringBuilder 的新实例。</returns>
        public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return null;
        }

        /// <summary>
        /// 创建实现 DbDataAdapter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>返回 DbDataAdapter 的新实例。</returns>
        public virtual DbDataAdapter CreateDataAdapter()
        {
            return null;
        }

        /// <summary>
        /// 创建实现 DbDataSourceEnumerator 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>返回 DbDataSourceEnumerator 的新实例。</returns>
        public virtual DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return null;
        }

        /// <summary>
        /// 创建实现 DbParameter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>返回 DbParameter 的新实例。</returns>
        public virtual DbParameter CreateParameter()
        {
            return null;
        }

        /// <summary>
        /// 创建一个参数前缀。
        /// </summary>
        /// <returns>返回一个参数前缀。</returns>
        public virtual char CreateParameterToken()
        {
            return '\0';
        }

        #endregion
    }
}
