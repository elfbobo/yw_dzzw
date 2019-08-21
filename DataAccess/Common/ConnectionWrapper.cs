//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System;
using System.Data.Common;

namespace Yawei.DataAccess.Common
{
    /// <summary>
    /// 表示一个 DbConnection 实例的包装。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public class ConnectionWrapper : IDisposable
    {
        #region 变量

        // 表示 DbConnection 实例 。
        private readonly DbConnection _connection;

        // 表示是否在此包装销毁时是否清理 DbConnection 实例。
        private readonly bool _isDisposeConnection;

        #endregion

        #region 属性

        /// <summary>
        /// 获取一个 DbConnection 实例。
        /// </summary>
        public DbConnection Connection
        {
            get { return _connection; }
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化一个 DbConnection 实例的包装。
        /// </summary>
        /// <param name="connection">表示 DbConnection 实例。</param>
        /// <param name="isDisposeConnection">表示是否在此包装销毁时是否清理 DbConnection 实例。</param>
        public ConnectionWrapper(DbConnection connection, bool isDisposeConnection)
        {
            _connection = connection;
            _isDisposeConnection = isDisposeConnection;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 在此包装销毁时是否清理 DbConnection 实例。
        /// </summary>
        public void Dispose()
        {
            if (_isDisposeConnection)
                Connection.Dispose();
        }
        #endregion
    }
}
