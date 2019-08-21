//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System.Data.Common;
using System.Data.OleDb;
using Yawei.DataAccess.Common;

namespace Yawei.DataAccess.OleDb
{
    /// <summary>
    /// 表示一组方法，这些方法用于创建 OleDb 提供程序对数据源类的实现的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public sealed class OleDbFactory:DbFactory
    {
        #region 属性

        /// <summary>
        /// 已重写。如果可以创建 OleDbDataSourceEnumerator，则返回 true。否则为 false。
        /// </summary>
        public override bool CanCreateDataSourceEnumerator
        {
            get { return false; }
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化 OleDbFactory 类的新实例。
        /// </summary>
        public OleDbFactory() : base() { }

        #endregion

        #region 方法

        /// <summary>
        /// 已重写。创建强类型的 OleDbCommand 实例。 
        /// </summary>
        /// <returns>返回强类型的 OleDbCommand 实例。</returns>
        public override DbCommand CreateCommand()
        {
            return new OleDbCommand();
        }

        /// <summary>
        /// 已重写。创建强类型的 OleDbCommandBuilder 实例。 
        /// </summary>
        /// <returns>返回强类型的 OleDbCommandBuilder 实例。</returns>
        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new OleDbCommandBuilder();
        }

        /// <summary>
        /// 已重写。创建强类型的 OleDbConnection 实例。
        /// </summary>
        /// <returns>返回强类型的 OleDbConnection 实例。</returns>
        public override DbConnection CreateConnection()
        {
            return new OleDbConnection();
        }

        /// <summary>
        /// 已重写。创建强类型的 OleDbConnectionStringBuilder 实例。
        /// </summary>
        /// <returns>返回强类型的 OleDbConnectionStringBuilder 实例。</returns>
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new OleDbConnectionStringBuilder();
        }

        /// <summary>
        /// 已重写。创建强类型的 OleDbDataAdapter 实例。
        /// </summary>
        /// <returns>返回强类型的 OleDbDataAdapter 实例。</returns>
        public override DbDataAdapter CreateDataAdapter()
        {
            return new OleDbDataAdapter();
        }

        /// <summary>
        /// 已重写。创建一个新 OleDbDataSourceEnumerator。 
        /// </summary>
        /// <returns>返回一个新 OleDbDataSourceEnumerator。</returns>
        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return null;
        }

        /// <summary>
        /// 已重写。创建强类型的 OleDbParameter 实例。 
        /// </summary>
        /// <returns>返回强类型的 OleDbParameter 实例。</returns>
        public override DbParameter CreateParameter()
        {
            return new OleDbParameter();
        }

        /// <summary>
        /// 已重写。创建一个参数前缀。 
        /// </summary>
        /// <returns>返回一个参数前缀。</returns>
        public override char CreateParameterToken()
        {
            return '@';
        }

        #endregion
    }
}
