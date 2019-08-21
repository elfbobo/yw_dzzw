//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;

namespace Yawei.DataAccess.Common
{
    /// <summary>
    /// 表示一组方法，这些方法用于管理对数据库的访问实例中的 DbConnection 实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public static class TransactionScopeConnections
    {
        #region 变量

        // 表示在同一个事务中 DbConnection 实例的字典。
        static readonly Dictionary<Transaction, Dictionary<string, DbConnection>> transactionConnections = new Dictionary<Transaction, Dictionary<string, DbConnection>>();

        #endregion

        #region 方法

        public static DbConnection GetConnection(Database db)
        {
            Transaction currentTransaction = Transaction.Current;
            if (currentTransaction == null)
                return null;
            Dictionary<string, DbConnection> connectionList;
            DbConnection connection;
            lock (transactionConnections)
            {
                if (!transactionConnections.TryGetValue(currentTransaction, out connectionList))
                {
                    connectionList = new Dictionary<string, DbConnection>();
                    transactionConnections.Add(currentTransaction, connectionList);
                    currentTransaction.TransactionCompleted += OnTransactionCompleted;
                }
            }

            lock (connectionList)
            {
                if (!connectionList.TryGetValue(db.ConnectionString, out connection))
                {
                    connection = db.CreateOpenConnection();
                    connectionList.Add(db.ConnectionString, connection);
                }
            }

            return connection;
        }

        #endregion

        #region 事件

        static void OnTransactionCompleted(object sender, TransactionEventArgs e)
        {
            Dictionary<string, DbConnection> connectionList;

            lock (transactionConnections)
            {
                if (!transactionConnections.TryGetValue(e.Transaction, out connectionList))
                {
                    return;
                }

                transactionConnections.Remove(e.Transaction);
            }

            lock (connectionList)
            {
                foreach (DbConnection connection in connectionList.Values)
                {
                    connection.Dispose();
                }
            }
        }

        #endregion
    }
}
