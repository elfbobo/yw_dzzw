//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System.Data.Common;
using System.Data.Odbc;
using Yawei.DataAccess.Common;

namespace Yawei.DataAccess.Odbc
{
    /// <summary>
    /// 表示一组方法，这些方法用于创建 Odbc 提供程序对数据源类的实现的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public sealed class OdbcFactory:DbFactory
    {
        #region 属性

        /// <summary>
        /// 已重写。如果可以创建 OdbcDataSourceEnumerator，则返回 true。否则为 false。
        /// </summary>
        public override bool CanCreateDataSourceEnumerator
        {
            get { return false; }
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化 OdbcFactory 类的新实例。
        /// </summary>
        public OdbcFactory() : base() { }

        #endregion

        #region 方法

        /// <summary>
        /// 已重写。创建强类型的 OdbcCommand 实例。 
        /// </summary>
        /// <returns>返回强类型的 OdbcCommand 实例。</returns>
        public override DbCommand CreateCommand()
        {
            return new OdbcCommand();
        }

        /// <summary>
        /// 已重写。创建强类型的 OdbcCommandBuilder 实例。 
        /// </summary>
        /// <returns>返回强类型的 OdbcCommandBuilder 实例。</returns>
        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new OdbcCommandBuilder();
        }

        /// <summary>
        /// 已重写。创建强类型的 OdbcConnection 实例。
        /// </summary>
        /// <returns>返回强类型的 OdbcConnection 实例。</returns>
        public override DbConnection CreateConnection()
        {
            return new OdbcConnection();
        }

        /// <summary>
        /// 已重写。创建强类型的 OdbcConnectionStringBuilder 实例。
        /// </summary>
        /// <returns>返回强类型的 OdbcConnectionStringBuilder 实例。</returns>
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new OdbcConnectionStringBuilder();
        }

        /// <summary>
        /// 已重写。创建强类型的 OdbcDataAdapter 实例。
        /// </summary>
        /// <returns>返回强类型的 OdbcDataAdapter 实例。</returns>
        public override DbDataAdapter CreateDataAdapter()
        {
            return new OdbcDataAdapter();
        }

        /// <summary>
        /// 已重写。创建一个新 OdbcDataSourceEnumerator。 
        /// </summary>
        /// <returns>返回一个新 OdbcDataSourceEnumerator。</returns>
        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return null;
        }

        /// <summary>
        /// 已重写。创建强类型的 OdbcParameter 实例。 
        /// </summary>
        /// <returns>返回强类型的 OdbcParameter 实例。</returns>
        public override DbParameter CreateParameter()
        {
            return new OdbcParameter();
        }

        /// <summary>
        /// 已重写。创建一个参数前缀。 
        /// </summary>
        /// <returns>返回一个参数前缀。</returns>
        public override char CreateParameterToken()
        {
            return '?';
        }

        #endregion
    }
}
