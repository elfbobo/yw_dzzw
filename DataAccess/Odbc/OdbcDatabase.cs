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
using System.Data.Odbc;
using Yawei.DataAccess.Common;

namespace Yawei.DataAccess.Odbc
{
    /// <summary>
    /// 表示 Odbc 数据库提供程序对数据库的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public class OdbcDatabase:Database
    {
        #region 构造器

        public OdbcDatabase(string connectionString)
            : base(connectionString,new OdbcFactory())
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取一个符合 Odbc 数据库标准的数据参数名称的令牌。
        /// </summary>
        protected char ParameterToken
        {
            get { return '?'; }
        }

        #endregion

        #region 基础方法

        /// <summary>
        /// 创建一个符合 Odbc 数据库标准的数据参数名称。
        /// </summary>
        /// <param name="name">参数名。</param>
        /// <returns>一个符合 Odbc 数据库提供程序标准的数据参数名称。</returns>
        public override string CreateParameterName(string name)
        {
            if (name[0] != ParameterToken)
            {
                return name.Insert(0, new string(ParameterToken, 1));
            }
            return name;
        }

        /// <summary>
        /// 设置 DataAdapter 实例的数据更新错误处理方式。
        /// </summary>
        /// <param name="adapter"> DbDataAdapter 实例。</param>
        protected override void SetUpRowUpdatedEvent(DbDataAdapter adapter)
        {
            ((OdbcDataAdapter)adapter).RowUpdated += OnRowUpdated;
        }

        /// <summary>
        /// 处理 DataAdapter 实例的数据更新错误处理方式。
        /// </summary>
        private void OnRowUpdated(object sender, OdbcRowUpdatedEventArgs args)
        {
            if (args.RecordsAffected == 0)
            {
                if (args.Errors != null)
                {
                    args.Row.RowError = args.Errors.Message;
                    args.Status = UpdateStatus.SkipCurrentRow;
                }
            }
        }

        /// <summary>
        /// 检查 DbCommand 实例是否为 OdbcCommand 类型。
        /// </summary>
        /// <param name="command">一个 DbCommand 实例。</param>
        /// <returns>如果是 OdbcCommand 类型则返回一个 OdbcCommand 实例。</returns>
        public static OdbcCommand CheckIfOracleCommand(DbCommand command)
        {
            OdbcCommand odbcCommand = command as OdbcCommand;
            if (odbcCommand == null) throw new ArgumentNullException("command");
            return odbcCommand;
        }

        #endregion

        #region 参数方法

        /// <summary>
        /// 创建一个新的 OleDbParameter 实例。
        /// </summary>
        /// <param name="name">名称。</param>
        /// <param name="odbcType">数据类型。</param>
        /// <param name="size">数据长度。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="nullable">表示参数是否可以接受null值。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本。</param>
        /// <param name="value">参数值。</param>
        /// <returns>返回一个新的 OracleParameter 实例。</returns>
        public OdbcParameter CreateParameter(string name, OdbcType odbcType, int size, ParameterDirection direction, bool nullable, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            OdbcParameter param = CreateParameter(name) as OdbcParameter;
            if (param == null) throw new ArgumentNullException("command");
            ConfigureParameter(param, name, odbcType, size, direction, nullable, sourceColumn, sourceVersion, value);
            return param;
        }

        /// <summary>
        /// 为 OleDbParameter 实例配置属性值。
        /// </summary>
        /// <param name="param">参数。</param>
        /// <param name="name">名称。</param>
        /// <param name="odbcType">数据类型。</param>
        /// <param name="size">数据长度。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="nullable">表示参数是否可以接受null值。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本。</param>
        /// <param name="value">参数值。</param>
        public void ConfigureParameter(OdbcParameter param, string name, OdbcType odbcType, int size, ParameterDirection direction, bool nullable, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.OdbcType = odbcType;
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
        /// <param name="odbcType">数据类型。</param>
        /// <param name="size">长度。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="nullable">表示参数是否可以接受null值。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本。</param>
        /// <param name="value">参数值。</param>
        public void AddParameter(DbCommand command, string name, OdbcType odbcType, int size, ParameterDirection direction, bool nullable, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter parameter = CreateParameter(name, odbcType, size, direction, nullable, sourceColumn, sourceVersion, value);
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="odbcType">数据类型。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本。</param>
        /// <param name="value">参数值。</param>
        public void AddParameter(DbCommand command, string name, OdbcType odbcType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            AddParameter(command, name, odbcType, 0, direction, false, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="odbcType">数据类型。</param>
        /// <param name="direction">参数类型。</param>
        /// <param name="value">参数值。</param>
        public void AddParameter(DbCommand command, string name, OdbcType odbcType, ParameterDirection direction, object value)
        {
            AddParameter(command, name, odbcType, 0, direction, false, String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个值为DBNull.Value参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="odbcType">数据类型。</param>
        public void AddInParameter(DbCommand command, string name, OdbcType odbcType)
        {
            AddParameter(command, name, odbcType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, DBNull.Value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个输入参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="odbcType">数据类型。</param>
        /// <param name="value">参数值。</param>
        public void AddInParameter(DbCommand command, string name, OdbcType odbcType, object value)
        {
            AddParameter(command, name, odbcType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个输入参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="odbcType">数据类型。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">数据行的版本号。</param>
        /// <param name="value">参数值。</param>
        public void AddInParameter(DbCommand command, string name, OdbcType odbcType, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            AddParameter(command, name, odbcType, 0, ParameterDirection.Input, true, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// 为一个 DbCommand 实例添加一个值为DBNull.Value的输出参数。
        /// </summary>
        /// <param name="command">表示要对数据源执行的 SQL 语句或存储过程。</param>
        /// <param name="name">参数名。</param>
        /// <param name="odbcType">数据类型。</param>
        /// <param name="size">长度。</param>
        public void AddOutParameter(DbCommand command, string name, OdbcType odbcType, int size)
        {
            AddParameter(command, name, odbcType, size, ParameterDirection.Output, true, String.Empty, DataRowVersion.Default, DBNull.Value);
        }

        #endregion
    }
}
