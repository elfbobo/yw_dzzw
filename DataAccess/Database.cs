//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Transactions;
using Yawei.DataAccess.Common;

namespace Yawei.DataAccess
{
    /// <summary>
    /// 表示数据库提供程序对数据库的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public abstract class Database
    {
        #region 属性

        /// <summary>
        /// 获取或指定数据库访问字符串。
        /// </summary>
        public string ConnectionString
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或指定创建提供程序对数据源类的实现的实例。
        /// </summary>
        protected DbFactory Factory
        {
            get;
            private set;
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化对数据库的访问的新实例。
        /// </summary>
        /// <param name="connectionString">表示数据库访问字符串。</param>
        /// <param name="providerName">表示数据库提供程序的枚举。</param>
        protected Database(string connectionString, DbFactory factory)
        {
            ConnectionString = connectionString;
            Factory = factory;
        }

        #endregion

        #region 数据库链接方法

        /// <summary>
        /// 创建一个新的 DbConnection 实例。
        /// </summary>
        /// <returns>返回一个新的 DbConnection 对实例。</returns>
        public DbConnection CreateConnection()
        {
            DbConnection newConnection = Factory.CreateConnection();
            newConnection.ConnectionString = ConnectionString;
            return newConnection;
        }

        /// <summary>
        /// 创建一个新的 DbConnection 实例并打开。
        /// </summary>
        /// <returns>返回一个新的 DbConnection 对实例并打开。</returns>
        public DbConnection CreateOpenConnection()
        {
            DbConnection connection = null;
            try
            {
                connection = CreateConnection();
                connection.Open();
            }
            catch
            {
                if (connection != null)
                    connection.Close();
                throw;
            }

            return connection;
        }

        /// <summary>
        /// 获取一个 DbConnection 实例的包装。
        /// </summary>
        /// <param name="disposeInnerConnection">是否在 ConnectionWrapper 实例销毁时销毁 DbConnection 对象。</param>
        /// <returns>返回一个 DbConnection 实例的包装。</returns>
        public ConnectionWrapper GetOpenConnection(bool disposeInnerConnection)
        {
            DbConnection connection = TransactionScopeConnections.GetConnection(this);
            if (connection != null)
                return new ConnectionWrapper(connection, false);
            else
                return new ConnectionWrapper(CreateOpenConnection(), disposeInnerConnection);
        }

        /// <summary>
        /// 获取一个 DbConnection 实例的包装。
        /// </summary>
        /// <returns>返回一个 DbConnection 实例的包装。</returns>
        public ConnectionWrapper GetOpenConnection()
        {
            return GetOpenConnection(true);
        }

        /// <summary>
        /// 打开一个 DbConnection 实例。
        /// </summary>
        /// <param name="connection"> DbConnection 实例。</param>
        public static void OpenConnection(DbConnection connection)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
            }
            catch
            {
                if (connection != null)
                    connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 关闭一个 DbConnection 实例。
        /// </summary>
        /// <param name="connection"> DbConnection 实例。</param>
        public static void CloseConnection(DbConnection connection)
        {
            try
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch
            {
                if (connection != null)
                    connection.Close();
                throw;
            }
        }

        #endregion

        #region 数据库事务方法

        /// <summary>
        /// 为 DbConnection 实例创建一个 DbTransaction 事务。
        /// </summary>
        /// <param name="connection"> DbConnection 实例。</param>
        /// <returns>DbTransaction 事务对象。</returns>
        public static DbTransaction BeginTransaction(DbConnection connection)
        {
            DbTransaction transaction = connection.BeginTransaction();
            return transaction;
        }

        /// <summary>
        /// 提交 IDbTransaction 事务。
        /// </summary>
        /// <param name="transaction"></param>
        public static void CommitTransaction(IDbTransaction transaction)
        {
            transaction.Commit();
        }

        /// <summary>
        /// 回滚 IDbTransaction 事务。
        /// </summary>
        /// <param name="transaction"></param>
        public static void RollbackTransaction(IDbTransaction transaction)
        {
            transaction.Rollback();
        }

        #endregion

        #region 数据库适配器方法

        /// <summary>
        /// 创建一个新的 DbDataAdapter 实例。
        /// </summary>
        /// <param name="updateBehavior">表示更新数据库时,提供数据适配器的更新命令时遇到一个错误时的处理方式的枚举。</param>
        /// <returns>一个新的 DbDataAdapter 实例。</returns>
        public DbDataAdapter CreateDataAdapter(UpdateBehavior updateBehavior)
        {
            DbDataAdapter adapter = Factory.CreateDataAdapter();
            if(updateBehavior == UpdateBehavior.Continue)
                SetUpRowUpdatedEvent(adapter);
            return adapter;
        }

        /// <summary>
        /// 创建一个新的并且 UpdateBehavior 的值为None的 DbDataAdapter 实例。
        /// </summary>
        /// <returns>一个新的并且 UpdateBehavior 的值为None的 DbDataAdapter 实例。</returns>
        public DbDataAdapter CreateDataAdapter()
        {
            return CreateDataAdapter(UpdateBehavior.Standard);
        }

        /// <summary>
        /// 设置 DataAdapter 实例的数据更新错误处理方式。
        /// </summary>
        /// <param name="adapter"> DbDataAdapter 实例。</param>
        protected virtual void SetUpRowUpdatedEvent(DbDataAdapter adapter)
        {
            
        }

        #endregion

        #region 参数方法

        /// <summary>
        /// 创建一个符合据库提供程序标准的数据参数名称。
        /// </summary>
        /// <param name="name">参数名。</param>
        /// <returns>一个符合据库提供程序标准的数据参数名称。</returns>
        public virtual string CreateParameterName(string name)
        {
            char token = Factory.CreateParameterToken();
            if (name[0] != token)
            {
                return name.Insert(0, new string(token, 1));
            }
            return name;
        }

        /// <summary>
        /// 创建一个新的 DbParameter 实例。
        /// </summary>
        /// <param name="name">参数名。</param>
        /// <returns>一个新的 DbParameter 实例。</returns>
        public DbParameter CreateParameter(string name)
        {
            DbParameter param = Factory.CreateParameter();
            param.ParameterName = CreateParameterName(name);
            return param;
        }

        /// <summary>
        /// 创建一个新的 DbParameter 实例。
        /// </summary>
        /// <param name="name">名称。</param>
        /// <param name="dbType">数据类型。</param>
        /// <param name="size">数据长度。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="nullable">表示参数是否可以接受null值。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本。</param>
        /// <param name="value">参数值。</param>
        /// <returns>返回一个新的 DbParameter 实例。</returns>
        public DbParameter CreateParameter(string name, DbType dbType, int size, ParameterDirection direction, bool nullable, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter param = CreateParameter(name);
            ConfigureParameter(param, name, dbType, size, direction, nullable, sourceColumn, sourceVersion, value);
            return param;
        }

        /// <summary>
        /// 为 DbParameter 实例配置属性值。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="name">名称。</param>
        /// <param name="dbType">数据类型。</param>
        /// <param name="size">数据长度。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="nullable">表示参数是否可以接受null值。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本。</param>
        /// <param name="value">参数值。</param>
        public void ConfigureParameter(DbParameter param, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.DbType = dbType;
            param.Size = size;
            param.Value = value ?? DBNull.Value;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="dbType">数据类型。</param>
        /// <param name="size">长度。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="nullable">表示参数是否可以接受null值。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本。</param>
        /// <param name="value">参数值。</param>
        public void AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter parameter = CreateParameter(name, dbType, size, direction, nullable, sourceColumn, sourceVersion, value);
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="dbType">数据类型。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本。</param>
        /// <param name="value">参数值。</param>
        public void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            AddParameter(command, name, dbType, 0, direction, false, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="dbType">数据类型。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="value">参数值。</param>
        public void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, object value)
        {
            AddParameter(command, name, dbType, 0, direction, false, String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个值为DBNull.Value参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="dbType">数据类型。</param>
        public void AddInParameter(DbCommand command, string name, DbType dbType)
        {
            AddParameter(command, name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, DBNull.Value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个输入参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="dbType">数据类型。</param>
        /// <param name="value">参数值。</param>
        public void AddInParameter(DbCommand command, string name, DbType dbType, object value)
        {
            AddParameter(command, name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个输入参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="dbType">数据类型。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本号。</param>
        /// <param name="value">参数值。</param>
        public void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            AddParameter(command, name, dbType, 0, ParameterDirection.Input, true, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个值为DBNull.Value的输出参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="dbType">数据类型。</param>
        /// <param name="size">长度。</param>
        public void AddOutParameter(DbCommand command, string name, DbType dbType, int size)
        {
            AddParameter(command, name, dbType, size, ParameterDirection.Output, true, String.Empty, DataRowVersion.Default, DBNull.Value);
        }

        /// <summary>
        /// 返回读取参数的开始索引。
        /// </summary>
        /// <returns>读取参数的开始索引。</returns>
        public int ParametersStartIndex()
        {
            return 0;
        }

        /// <summary>
        /// 为一个 DbCommand 实例设置参数值。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="parameterName">参数名。</param>
        /// <param name="value">参数值。</param>
        public void SetParameterValue(DbCommand command, string parameterName, object value)
        {
            command.Parameters[CreateParameterName(parameterName)].Value = value ?? DBNull.Value;
        }

        /// <summary>
        /// 获取一个 DbCommand 实例中的参数值。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <returns>参数值。</returns>
        public object GetParameterValue(DbCommand command, string name)
        {
            return command.Parameters[CreateParameterName(name)].Value;
        }

        /// <summary>
        /// 为一个 DbCommand 实例从第一个参数开始设置参数值。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="values">参数值数组。</param>
        public void AssignParameterValues(DbCommand command, object[] values)
        {
            int parameterIndexShift = ParametersStartIndex();
            for (int i = 0; i < values.Length; i++)
            {
                IDataParameter parameter = command.Parameters[i + parameterIndexShift];
                SetParameterValue(command, parameter.ParameterName, values[i]);
            }
        }

        /// <summary>
        /// 表示 DbCommand 实例中的参数总数和参数值总数是否相等。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="values">参数值数组。</param>
        /// <returns>返回命令对象中的参数总数和参数值总数是否相等。</returns>
        public virtual bool SameNumberOfParametersAndValues(DbCommand command, object[] values)
        {
            int numberOfParametersToStoredProcedure = command.Parameters.Count;
            int numberOfValuesProvidedForStoredProcedure = values.Length;
            return numberOfParametersToStoredProcedure == numberOfValuesProvidedForStoredProcedure;
        }

        #endregion

        #region 命令方法

        /// <summary>
        /// 创建一个新的 DbCommand 实例。
        /// </summary>
        /// <param name="commandType">命令解释类型。</param>
        /// <param name="commandText">命令文本。</param>
        /// <returns>一个新的 DbCommand 实例。</returns>
        public DbCommand CreateCommand(CommandType commandType, string commandText)
        {
            DbCommand command = Factory.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }

        /// <summary>
        /// 为 DbCommand 实例设置到数据库连接。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="connection">表示到数据的连接。</param>
        public static void PrepareCommand(DbCommand command, DbConnection connection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (connection == null) throw new ArgumentNullException("connection");

            command.Connection = connection;
        }

        /// <summary>
        /// 为 DbCommand 实例设置事务。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="transaction">事务。</param>
        public static void PrepareCommand(DbCommand command, DbTransaction transaction)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (transaction == null) throw new ArgumentNullException("transaction");

            PrepareCommand(command, transaction.Connection);
            command.Transaction = transaction;
        }

        /// <summary>
        /// 为一个查询语句建立 DbCommand 实例。
        /// </summary>
        /// <param name="query">命令文本</param>
        /// <returns> DbCommand 实例。</returns>
        public DbCommand GetSqlStringCommand(string query)
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentException("查询语句不能为null或空", "query");
            return CreateCommand(CommandType.Text, query);
        }

        /// <summary>
        /// 为一个存储过程建立 DbCommand 实例。
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <returns> DbCommand 实例。</returns>
        public DbCommand GetStoredProcCommand(string storedProcedureName)
        {
            if (string.IsNullOrEmpty(storedProcedureName)) throw new ArgumentException("存储过程名称不能为空", "storedProcedureName");
            return CreateCommand(CommandType.StoredProcedure, storedProcedureName);
        }

        #endregion

        #region 操作管理

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <returns>受影响的行数。</returns>
        protected int DoExecuteNonQuery(DbCommand command)
        {
            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取一个或多个通过在数据源执行命令所获得的只进结果集流。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="cmdBehavior">提供对查询结果和查询对数据库的影响的枚举。</param>
        /// <returns>返回一个或多个通过在数据源执行命令所获得的只进结果集流。</returns>
        protected IDataReader DoExecuteReader(DbCommand command, CommandBehavior cmdBehavior)
        {
            try
            {
                IDataReader reader = command.ExecuteReader(cmdBehavior);
                return reader;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <returns>返回查询所返回的结果集中第一行的第一列。</returns>
        protected object DoExecuteScalar(DbCommand command)
        {
            try
            {
                object returnValue = command.ExecuteScalar();
                return returnValue;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="dataSet"> DataSet 实例。</param>
        /// <param name="tableNames">数据表名称。</param>
        protected void DoLoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames)
        {
            if (tableNames == null) throw new ArgumentNullException("tableNames");
            if (tableNames.Length == 0)
                throw new ArgumentException("数据表名数组长度等于0", "tableNames");
            for (int i = 0; i < tableNames.Length; i++)
                if (string.IsNullOrEmpty(tableNames[i])) throw new ArgumentException("表名不能为空", string.Concat("tableNames[", i, "]"));

            using (DbDataAdapter adapter = CreateDataAdapter())
            {
                ((IDbDataAdapter)adapter).SelectCommand = command;
                try
                {
                    string systemCreatedTableNameRoot = "Table";
                    for (int i = 0; i < tableNames.Length; i++)
                    {
                        string systemCreatedTableName = (i == 0) ? systemCreatedTableNameRoot : systemCreatedTableNameRoot + i;
                        adapter.TableMappings.Add(systemCreatedTableName, tableNames[i]);
                    }
                    adapter.Fill(dataSet);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="behavior">表示更新数据库时,提供数据适配器的更新命令时遇到一个错误时的处理方式的枚举。</param>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="insertCommand">表示用于从数据集中添加记录的 SQL 语句或存储过程。</param>
        /// <param name="updateCommand">表示用于从数据集中更新记录的 SQL 语句或存储过程。</param>
        /// <param name="deleteCommand">表示用于从数据集中删除记录的 SQL 语句或存储过程。</param>
        /// <param name="updateBatchSize">表示一个值，该值启用或禁用批处理支持，并且指定可在一次批处理中执行的命令的数量。</param>
        /// <returns>返回更新行数。</returns>
        protected int DoUpdateDataSet(UpdateBehavior behavior, DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, int? updateBatchSize)
        {
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentException("表名称不能是null或空值", "tableName");
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            if (insertCommand == null && updateCommand == null && deleteCommand == null)
                throw new ArgumentException("InsertCommand或UpdateCommand或DeleteCommand不能为null值");

            using (DbDataAdapter adapter = CreateDataAdapter(behavior))
            {
                if (insertCommand != null)
                {
                    adapter.InsertCommand = insertCommand;
                }
                if (updateCommand != null)
                {
                    adapter.UpdateCommand = updateCommand;
                }
                if (deleteCommand != null)
                {
                    adapter.DeleteCommand = deleteCommand;
                }

                if (updateBatchSize != null)
                {
                    adapter.UpdateBatchSize = (int)updateBatchSize;
                    if (insertCommand != null)
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    if (updateCommand != null)
                        adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                    if (deleteCommand != null)
                        adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
                }

                try
                {
                    int rows = adapter.Update(dataSet.Tables[tableName]);
                    return rows;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="adapter">数据更新接口。</param>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="updateBatchSize">表示一个值，该值启用或禁用批处理支持，并且指定可在一次批处理中执行的命令的数量。</param>
        /// <returns>返回更新行数。</returns>
        protected int DoUpdateDataSet(UpdateBehavior behavior, DbDataAdapter adapter, DataSet dataSet, string tableName, int? updateBatchSize)
        {
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentException("表名称不能是null或空值", "tableName");
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            if (updateBatchSize != null)
            {
                adapter.UpdateBatchSize = (int)updateBatchSize;
                adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
            }

            try
            {
                int rows = adapter.Update(dataSet.Tables[tableName]);
                return rows;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 获取一个或多个通过在数据源执行命令所获得的只进结果集流

        /// <summary>
        /// 获取一个或多个通过在数据源执行命令所获得的只进结果集流。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <returns>返回一个或多个通过在数据源执行命令所获得的只进结果集流。</returns>
        public IDataReader ExecuteReader(DbCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");
            ConnectionWrapper wrapper = GetOpenConnection(false);
            try
            {
                PrepareCommand(command, wrapper.Connection);
                if (Transaction.Current != null)
                    return DoExecuteReader(command, CommandBehavior.Default);
                else
                    return DoExecuteReader(command, CommandBehavior.CloseConnection);
            }
            catch
            {
                wrapper.Connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 获取一个或多个通过在数据源执行命令所获得的只进结果集流。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个或多个通过在数据源执行命令所获得的只进结果集流。</returns>
        public IDataReader ExecuteReader(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return DoExecuteReader(command, CommandBehavior.Default);
        }

        /// <summary>
        /// 获取一个或多个通过在数据源执行命令所获得的只进结果集流。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <returns>返回一个或多个通过在数据源执行命令所获得的只进结果集流。</returns>
        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteReader(command);
            }
        }

        /// <summary>
        /// 获取一个或多个通过在数据源执行命令所获得的只进结果集流。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个或多个通过在数据源执行命令所获得的只进结果集流。</returns>
        public IDataReader ExecuteReader(CommandType commandType, string commandText, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteReader(command, transaction);
            }
        }

        /// <summary>
        /// 获取一个或多个通过在数据源执行命令所获得的只进结果集流。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <returns>返回一个或多个通过在数据源执行命令所获得的只进结果集流。</returns>
        public IDataReader ExecuteReader(string query)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteReader(command);
            }
        }

        /// <summary>
        /// 获取一个或多个通过在数据源执行命令所获得的只进结果集流。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个或多个通过在数据源执行命令所获得的只进结果集流。</returns>
        public IDataReader ExecuteReader(string query, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteReader(command, transaction);
            }
        }

        #endregion

        #region 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <returns>结果集中第一行的第一列。</returns>
        public object ExecuteScalar(DbCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");
            using (ConnectionWrapper wrapper = GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                return DoExecuteScalar(command);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>结果集中第一行的第一列。</returns>
        public object ExecuteScalar(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return DoExecuteScalar(command);
        }

        /// <summary>
        ///  执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <returns>结果集中第一行的第一列。</returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteScalar(command);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>结果集中第一行的第一列。</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteScalar(command, transaction);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <returns>结果集中第一行的第一列。</returns>
        public object ExecuteScalar(string query)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteScalar(command);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>结果集中第一行的第一列。</returns>
        public object ExecuteScalar(string query, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteScalar(command, transaction);
            }
        }

        #endregion

        #region 对连接对象执行 SQL 语句

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteNonQuery(DbCommand command)
        {
            using (ConnectionWrapper wrapper = GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                return DoExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteNonQuery(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return DoExecuteNonQuery(command);
        }

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteNonQuery(command, transaction);
            }
        }

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteNonQuery(string query)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>受影响的行数。</returns>
        public int ExecuteNonQuery(string query, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteNonQuery(command, transaction);
            }
        }

        #endregion

        #region 执行查询，返回查询所返回的结果集并加载到 DataSet 实例中

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableNames">数据表名称。</param>
        public void LoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames)
        {
            using (ConnectionWrapper wrapper = GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                DoLoadDataSet(command, dataSet, tableNames);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        public void LoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            DoLoadDataSet(command, dataSet, tableNames);
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableName">数据表名称。</param>
        public void LoadDataSet(DbCommand command, DataSet dataSet, string tableName)
        {
            LoadDataSet(command, dataSet, new string[] { tableName });
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        public void LoadDataSet(DbCommand command, DataSet dataSet, string tableName, DbTransaction transaction)
        {
            LoadDataSet(command, dataSet, new string[] { tableName }, transaction);
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableNames">数据表名称。</param>
        public void LoadDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                LoadDataSet(command, dataSet, tableNames);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        public void LoadDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                LoadDataSet(command, dataSet, tableNames, transaction);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableName">数据表名称。</param>
        public void LoadDataSet(CommandType commandType, string commandText, DataSet dataSet, string tableName)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                LoadDataSet(command, dataSet, tableName);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        public void LoadDataSet(CommandType commandType, string commandText, DataSet dataSet, string tableName, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                LoadDataSet(command, dataSet, tableName, transaction);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableNames">数据表名称。</param>
        public void LoadDataSet(string query, DataSet dataSet, string[] tableNames)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                LoadDataSet(command, dataSet, tableNames);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        public void LoadDataSet(string query, DataSet dataSet, string[] tableNames, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                LoadDataSet(command, dataSet, tableNames, transaction);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableName">数据表名称。</param>
        public void LoadDataSet(string query, DataSet dataSet, string tableName)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                LoadDataSet(command, dataSet, tableName);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="dataSet">表示 DataSet 实例。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        public void LoadDataSet(string query, DataSet dataSet, string tableName, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                LoadDataSet(command, dataSet, tableName, transaction);
            }
        }

        #endregion

        #region 执行查询，返回查询所返回的结果集并加载到 DataSet 实例中

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(DbCommand command, string[] tableNames)
        {
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;
            LoadDataSet(command, dataSet, tableNames);
            return dataSet;
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(DbCommand command, string[] tableNames, DbTransaction transaction)
        {
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;
            LoadDataSet(command, dataSet, tableNames, transaction);
            return dataSet;
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(DbCommand command, string tableName)
        {
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;
            LoadDataSet(command, dataSet, tableName);
            return dataSet;
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(DbCommand command, string tableName, DbTransaction transaction)
        {
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;
            LoadDataSet(command, dataSet, tableName, transaction);
            return dataSet;
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(DbCommand command)
        {
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;
            LoadDataSet(command, dataSet, "Table");
            return dataSet;
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(DbCommand command, DbTransaction transaction)
        {
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;
            LoadDataSet(command, dataSet, "Table", transaction);
            return dataSet;
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, string[] tableNames)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteDataSet(command, tableNames);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, string[] tableNames, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteDataSet(command, tableNames, transaction);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, string tableName)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteDataSet(command, tableName);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, string tableName, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteDataSet(command, tableName, transaction);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteDataSet(command);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="commandType">指定如何解析命令字符串。</param>
        /// <param name="commandText">命令文本。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(commandType, commandText))
            {
                return ExecuteDataSet(command, transaction);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(string query, string[] tableNames)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteDataSet(command, tableNames);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="tableNames">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(string query, string[] tableNames, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteDataSet(command, tableNames, transaction);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(string query, string tableName)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteDataSet(command, tableName);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="tableName">数据表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(string query, string tableName, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteDataSet(command, tableName, transaction);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(string query)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteDataSet(command);
            }
        }

        /// <summary>
        /// 执行查询，返回查询所返回的结果集并并缓存到 DataSet 实例中。
        /// </summary>
        /// <param name="query">查询语句。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回一个 DataSet 实例。</returns>
        public DataSet ExecuteDataSet(string query, DbTransaction transaction)
        {
            using (DbCommand command = CreateCommand(CommandType.Text, query))
            {
                return ExecuteDataSet(command, transaction);
            }
        }

        #endregion

        #region 将 DataSet 实例转换为 DataView 实例

        /// <summary>
        /// 将 DataSet 实例转换为 DataView 实例。
        /// </summary>
        /// <param name="dataSet">一个 DataSet 实例。</param>
        /// <returns>返回一个 DataViewManager 实例。</returns>
        public DataViewManager ConvertDataSetToDataView(DataSet dataSet)
        {
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            DataViewManager dvManager = new DataViewManager(dataSet);
            return dvManager;
        }

        /// <summary>
        /// 将 DataSet 实例转换为 DataView 实例。
        /// </summary>
        /// <param name="dataSet">一个 DataSet 实例。</param>
        /// <param name="tableName">表名称</param>
        /// <returns>返回一个 DataView 实例。</returns>
        public DataView ConvertDataSetToDataView(DataSet dataSet, string tableName)
        {
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentException("表名称不能是null或空值", "tableName");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            DataView dataView = new DataView(dataSet.Tables[tableName]);
            return dataView;
        }

        /// <summary>
        /// 将 DataSet 实例转换为 DataView 实例。
        /// </summary>
        /// <param name="dataTable">一个 dataTable 实例。</param>
        /// <returns>返回一个 DataView 实例。</returns>
        public DataView ConvertDataSetToDataView(DataTable dataTable)
        {
            if (dataTable == null) throw new ArgumentNullException("dataTable");
            DataView dataView = new DataView(dataTable);
            return dataView;
        }

        #endregion

        #region 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="insertCommand">表示用于从数据集中添加记录的 SQL 语句或存储过程。</param>
        /// <param name="updateCommand">表示用于从数据集中更新记录的 SQL 语句或存储过程。</param>
        /// <param name="deleteCommand">表示用于从数据集中删除记录的 SQL 语句或存储过程。</param>
        /// <param name="updateBatchSize">表示一个值，该值启用或禁用批处理支持，并且指定可在一次批处理中执行的命令的数量。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回更新行数。</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, int? updateBatchSize, DbTransaction transaction)
        {
            if (insertCommand != null)
            {
                PrepareCommand(insertCommand, transaction);
            }
            if (updateCommand != null)
            {
                PrepareCommand(updateCommand, transaction);
            }
            if (deleteCommand != null)
            {
                PrepareCommand(deleteCommand, transaction);
            }

            return DoUpdateDataSet(UpdateBehavior.Transactional, dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBatchSize);
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="insertCommand">表示用于从数据集中添加记录的 SQL 语句或存储过程。</param>
        /// <param name="updateCommand">表示用于从数据集中更新记录的 SQL 语句或存储过程。</param>
        /// <param name="deleteCommand">表示用于从数据集中删除记录的 SQL 语句或存储过程。</param>
        /// <param name="updateBatchSize">表示一个值，该值启用或禁用批处理支持，并且指定可在一次批处理中执行的命令的数量。</param>
        /// <param name="behavior">表示更新数据库时,提供数据适配器的更新命令时遇到一个错误时的处理方式的枚举。</param>
        /// <returns>返回更新行数。</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, int? updateBatchSize, UpdateBehavior behavior)
        {
            using (ConnectionWrapper wrapper = GetOpenConnection())
            {
                if (behavior == UpdateBehavior.Transactional && Transaction.Current == null)
                {
                    DbTransaction transaction = BeginTransaction(wrapper.Connection);
                    try
                    {
                        int rowsAffected = UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBatchSize, transaction);
                        CommitTransaction(transaction);
                        return rowsAffected;
                    }
                    catch
                    {
                        RollbackTransaction(transaction);
                        throw;
                    }
                }
                else
                {
                    if (insertCommand != null)
                    {
                        PrepareCommand(insertCommand, wrapper.Connection);
                    }
                    if (updateCommand != null)
                    {
                        PrepareCommand(updateCommand, wrapper.Connection);
                    }
                    if (deleteCommand != null)
                    {
                        PrepareCommand(deleteCommand, wrapper.Connection);
                    }

                    return DoUpdateDataSet(behavior, dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBatchSize);
                }
            }
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="insertCommand">表示用于从数据集中添加记录的 SQL 语句或存储过程。</param>
        /// <param name="updateCommand">表示用于从数据集中更新记录的 SQL 语句或存储过程。</param>
        /// <param name="deleteCommand">表示用于从数据集中删除记录的 SQL 语句或存储过程。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回更新行数。</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, DbTransaction transaction)
        {
            return UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, null,transaction);
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="insertCommand">表示用于从数据集中添加记录的 SQL 语句或存储过程。</param>
        /// <param name="updateCommand">表示用于从数据集中更新记录的 SQL 语句或存储过程。</param>
        /// <param name="deleteCommand">表示用于从数据集中删除记录的 SQL 语句或存储过程。</param>
        /// <param name="behavior">表示更新数据库时,提供数据适配器的更新命令时遇到一个错误时的处理方式的枚举。</param>
        /// <returns>返回更新行数。</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, UpdateBehavior behavior)
        {
            return UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, null, behavior);
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="updateBatchSize">表示启用或禁用批处理支持，并且指定可在一次批处理中执行的命令的数量。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回更新行数。</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, int? updateBatchSize, DbTransaction transaction)
        {
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentException("表名称不能是null或空值", "tableName");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            using (DbDataAdapter adapter = CreateDataAdapter(UpdateBehavior.Transactional))
            {
                DbCommandBuilder builder = Factory.CreateCommandBuilder();
                DbCommand selectCommand = CreateCommand(CommandType.Text, "select * from " + String.Concat("[", tableName, "]"));
                PrepareCommand(selectCommand, transaction);
                adapter.SelectCommand = selectCommand;
                builder.DataAdapter = adapter;
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();

                return DoUpdateDataSet(UpdateBehavior.Transactional, adapter, dataSet, tableName, updateBatchSize);
            }
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="updateBatchSize">表示启用或禁用批处理支持，并且指定可在一次批处理中执行的命令的数量。</param>
        /// <param name="behavior">表示更新数据库时,提供数据适配器的更新命令时遇到一个错误时的处理方式的枚举。</param>
        /// <returns>返回更新行数。</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, int? updateBatchSize, UpdateBehavior behavior)
        {
            using (ConnectionWrapper wrapper = GetOpenConnection())
            {
                if (behavior == UpdateBehavior.Transactional && Transaction.Current == null)
                {
                    DbTransaction transaction = BeginTransaction(wrapper.Connection);
                    try
                    {
                        int rowsAffected = UpdateDataSet(dataSet, tableName, updateBatchSize, transaction);
                        CommitTransaction(transaction);
                        return rowsAffected;
                    }
                    catch
                    {
                        RollbackTransaction(transaction);
                        throw;
                    }
                }
                else
                {
                    using (DbDataAdapter adapter = CreateDataAdapter(behavior))
                    {
                        DbCommandBuilder builder = Factory.CreateCommandBuilder();
                        DbCommand selectCommand = CreateCommand(CommandType.Text, "select * from " + String.Concat("[", tableName, "]"));
                        PrepareCommand(selectCommand, wrapper.Connection);
                        adapter.SelectCommand = selectCommand;
                        builder.DataAdapter = adapter;
                        adapter.InsertCommand = builder.GetInsertCommand();
                        adapter.UpdateCommand = builder.GetUpdateCommand();
                        adapter.DeleteCommand = builder.GetDeleteCommand();

                        return DoUpdateDataSet(behavior,adapter, dataSet, tableName, updateBatchSize);
                    }
                }
            }
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回更新行数。</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, DbTransaction transaction)
        {
            return UpdateDataSet(dataSet, tableName, null, transaction);
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="tableName">表名称。</param>
        /// <param name="behavior">表示更新数据库时,提供数据适配器的更新命令时遇到一个错误时的处理方式的枚举。</param>
        /// <returns>返回更新行数。</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, UpdateBehavior behavior)
        {
            return UpdateDataSet(dataSet, tableName, null, behavior);
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="transaction">表示一个事务。</param>
        /// <returns>返回更新行数。</returns>
        public void UpdateDataSet(DataSet dataSet, DbTransaction transaction)
        {
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                UpdateDataSet(dataSet, dataSet.Tables[i].TableName, transaction);
            }
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <param name="behavior">表示更新数据库时,提供数据适配器的更新命令时遇到一个错误时的处理方式的枚举。</param>
        /// <returns>返回更新行数。</returns>
        public void UpdateDataSet(DataSet dataSet, UpdateBehavior behavior)
        {
            using (ConnectionWrapper wrapper = GetOpenConnection())
            {
                if (behavior == UpdateBehavior.Transactional && Transaction.Current == null)
                {
                    DbTransaction transaction = BeginTransaction(wrapper.Connection);
                    try
                    {
                        for (int i = 0; i < dataSet.Tables.Count; i++)
                        {
                            UpdateDataSet(dataSet, transaction);
                        }
                        CommitTransaction(transaction);
                    }
                    catch
                    {
                        RollbackTransaction(transaction);
                        throw;
                    }
                }
                else
                {
                    for (int i = 0; i < dataSet.Tables.Count; i++)
                    {
                        UpdateDataSet(dataSet,behavior);
                    }
                }
            }
        }

        /// <summary>
        /// 为指定 DataTable 中每个已插入、已更新或已删除的行调用相应的 INSERT、UPDATE 或 DELETE 语句。
        /// </summary>
        /// <param name="dataSet">数据缓存 DataSet 实例。</param>
        /// <returns>返回更新行数。</returns>
        public void UpdateDataSet(DataSet dataSet)
        {
            UpdateDataSet(dataSet, UpdateBehavior.Transactional);
        }

        #endregion
    }
}
