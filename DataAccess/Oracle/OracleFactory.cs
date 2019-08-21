//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System.Data.Common;
using Oracle.DataAccess.Client;
using Yawei.DataAccess.Common;

namespace Yawei.DataAccess.Oracle
{
    /// <summary>
    /// 表示一组方法，这些方法用于创建 Oracle 提供程序对数据源类的实现的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public sealed class OracleFactory:DbFactory
    {
        #region 属性

        /// <summary>
        /// 已重写。如果可以创建 OracleDataSourceEnumerator，则返回 true。否则为 false。
        /// </summary>
        public override bool CanCreateDataSourceEnumerator
        {
            get { return true; }
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化 OracleFactory 类的新实例。
        /// </summary>
        public OracleFactory() : base() { }

        #endregion

        #region 方法

        /// <summary>
        /// 已重写。创建强类型的 OracleCommand 实例。 
        /// </summary>
        /// <returns>返回强类型的 OracleCommand 实例。</returns>
        public override DbCommand CreateCommand()
        {
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.BindByName = true;
            return oracleCommand;
        }

        /// <summary>
        /// 已重写。创建强类型的 OracleCommandBuilder 实例。 
        /// </summary>
        /// <returns>返回强类型的 OracleCommandBuilder 实例。</returns>
        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new OracleCommandBuilder();
        }

        /// <summary>
        /// 已重写。创建强类型的 OracleConnection 实例。
        /// </summary>
        /// <returns>返回强类型的 OracleConnection 实例。</returns>
        public override DbConnection CreateConnection()
        {
            return new OracleConnection();
        }

        /// <summary>
        /// 已重写。创建强类型的 OracleConnectionStringBuilder 实例。
        /// </summary>
        /// <returns>返回强类型的 OracleConnectionStringBuilder 实例。</returns>
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new OracleConnectionStringBuilder();
        }

        /// <summary>
        /// 已重写。创建强类型的 OracleDataAdapter 实例。
        /// </summary>
        /// <returns>返回强类型的 OracleDataAdapter 实例。</returns>
        public override DbDataAdapter CreateDataAdapter()
        {
            return new OracleDataAdapter();
        }

        /// <summary>
        /// 已重写。创建一个新 OracleDataSourceEnumerator。 
        /// </summary>
        /// <returns>返回一个新 OracleDataSourceEnumerator。</returns>
        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return new OracleDataSourceEnumerator();
        }

        /// <summary>
        /// 已重写。创建强类型的 OracleParameter 实例。 
        /// </summary>
        /// <returns>返回强类型的 OracleParameter 实例。</returns>
        public override DbParameter CreateParameter()
        {
            return new OracleParameter();
        }

        /// <summary>
        /// 已重写。创建一个参数前缀。 
        /// </summary>
        /// <returns>返回一个参数前缀。</returns>
        public override char CreateParameterToken()
        {
            return ':';
        }

        #endregion
    }
}
