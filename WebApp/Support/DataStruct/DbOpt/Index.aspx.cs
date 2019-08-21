using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Yawei.DataAccess;
using Yawei.SupportCore;

namespace Yawei.App.Support.DataStruct.DbOpt
{
    public partial class Index : System.Web.UI.Page
    {
        protected string strModelGuid = string.Empty;
        protected Database database = DatabaseFactory.CreateDatabase();
        protected Database db = DatabaseFactory.CreateDatabase();
        protected string strDBName = string.Empty;//数据库名
        protected string DBConnection = string.Empty;//数据库连接串
        protected string treeJson = string.Empty;
        protected string strOpt = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 接收参数
            strModelGuid = Request["ModelGuid"] == null ? "" : Request["ModelGuid"];
            strOpt = Request["Opt"] == null ? "" : Request["Opt"];
            #endregion
            treeJson = "";
            #region 初始化
            GetDBConnection();
            treeJson = DatabaseMng.GetTreeJson();

            #endregion

            switch (strOpt)
            {
                case "buildtable":
                    BuildTable();
                    break;
                case "inserttable":
                    InsertTable();
                    break;
                case "updatetable":
                    UpdateTable();
                    break;
                case "tableattr":
                    TableAttr();
                    break;
                case "insertcolumn":
                    InsertColumn();
                    break;
                case "updatecolumn":
                    UpdateColumn();
                    break;
                case "columnattr":
                    ColumnAttr();
                    break;
            }

        }

        /// <summary>
        /// 保存时拼凑字段的扩展属性
        /// </summary>
        /// <returns></returns>
        string SetColunmnAttr()
        {
            string[] ExtendProperty = null;
            string Attr = "<xml/>";
            if (!string.IsNullOrEmpty(Request["ExtendProperty"].ToString()))
            {
                Attr = "<xml>";
                ExtendProperty = Request["ExtendProperty"].ToString().Split('Ω');
                if (ExtendProperty.Length > 0)
                {
                    for (int i = 0; i < ExtendProperty.Length; i++)
                        Attr += "<Property " + ExtendProperty[i].Split('^')[0] + "=\"" + ExtendProperty[i].Split('^')[1] + "\" />";
                }
                Attr += "</xml>";
            }
            return Attr;
        }

        /// <summary>
        /// 修改字段属性信息
        /// </summary>
        void ColumnAttr()
        {
            string otherSql = "";
            string ModelGuid = Request["ModelGuid"].ToString();
            string TableName = Request["TableName"].ToString();
            string Name = Request["Name"].ToString();
            string Type = Request["Type"].ToString();
            string Description = Request["Description"].ToString();
            string Lengh = Request["Lengh"].ToString();
            string Precision = Request["Precision"].ToString();
            string IsPrimaryKey = Request["IsPrimaryKey"].ToString();
            string IsNull = Request["IsNull"].ToString();
            string DefaultValue = Request["DefaultValue"].ToString();
            string ExtendProperty = SetColunmnAttr();
            string[] Arr = { "bigint", "char", "date", "datetime", "datetime2", "decimal", "float", "int", "nchar", "nvarchar", "smallint", "text", "varchar" };
            string sql = "update Sys_DataColumn set Type='" + Type + "',Description='" + Description + "'";
            sql += ",Lengh='" + Lengh + "',Precision='" + Precision + "',IsPrimaryKey='" + IsPrimaryKey + "'";
            sql += ",IsNull='" + IsNull + "'";
            for (int i = 0; i < Arr.Length; i++)
            {
                if (Arr[i] == Type)
                {
                    sql += ",DefaultValue='" + DefaultValue + "'";
                    break;
                }
            }
            sql += ",ExtendProperty='" + ExtendProperty + "' where ModelGuid='" + ModelGuid + "' and TableName='" + TableName + "' and Name='" + Name + "';";
            db = GetDb(Request["ModelGuid"].ToString());
            object obj = db.ExecuteScalar("select COUNT(*) from sys.tables WHERE name='" + TableName + "'");
            if (int.Parse(obj.ToString()) > 0)//代表已经生成表结构
            {
                int c = int.Parse(db.ExecuteScalar("select COUNT(*) from sys.columns where object_id=object_id('" + TableName.ToLower() + "') AND name='" + Name.ToLower() + "'").ToString());
                if (c == 0)//代表生成的表结构中无与该字段重命名的字段
                {
                    otherSql = "alter table " + TableName + " add [" + Name + "] ";
                    otherSql += GetDataType(Type, Lengh, Precision, IsNull, IsPrimaryKey) + ";";//设置字段数据类型
                    otherSql += SetColumnDefaultValue(TableName, Name, DefaultValue, Type);//设置字段默认值
                    otherSql += SetColumnDescription(TableName, Name, Description);//字段描述
                    otherSql += GetColumnProperty(ExtendProperty, TableName, Name);//扩展属性
                    otherSql += BuildedTablePksSql(db, TableName, Name, IsPrimaryKey);//重新设置表的主键
                    c = db.ExecuteNonQuery(otherSql) + database.ExecuteNonQuery(sql);
                    Response.Clear();
                    Response.Write(c);
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write(-1);
                    Response.End();
                }
            }
            else//代表未生成表结构
            {
                Response.Clear();
                Response.Write(database.ExecuteNonQuery(sql));
                Response.End();
            }
        }

        string BuildedTablePksSql(Database db, string Table, string Name, string IsPrimaryKey)
        {
            string sql = "";
            string Pk_Name = "";
            string Pk_Columns = "";
            DataSet doc = database.ExecuteDataSet("sp_pkeys '" + Table + "'");
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                Pk_Name = doc.Tables[0].Rows[0]["PK_NAME"].ToString();
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    if (i > 0)
                        Pk_Columns += ",";
                    Pk_Columns += doc.Tables[0].Rows[0]["COLUMN_NAME"].ToString();
                }
            }

            if (IsPrimaryKey == "0")//为0代表是主键
            {
                if (Pk_Name != "")
                {
                    Pk_Columns = Pk_Columns + "," + Name;
                    sql = "ALTER TABLE " + Table + " DROP CONSTRAINT " + Pk_Name + ";";
                    sql += "ALTER TABLE " + Table + " ADD CONSTRAINT PK_" + Table + " PRIMARY KEY (" + Pk_Columns + ");";
                }
                else
                    sql = "ALTER TABLE " + Table + " ADD CONSTRAINT PK_" + Table + " PRIMARY KEY (" + Name + ");";
            }
            return sql;
        }


        /// <summary>
        /// 修改字段信息
        /// </summary>
        void UpdateColumn()
        {
            string Name = Request["Name"].ToString();
            string OldName = Request["OldName"].ToString();
            string sql = "update Sys_DataColumn set Name='" + Request["Name"].ToString() + "' where ModelGuid='" + Request["ModelGuid"].ToString() + "' and TableName='" + Request["TableName"].ToString() + "' and Name='" + Request["OldName"].ToString() + "'";
            if (Name != OldName)
            {
                object obj = database.ExecuteScalar("select count(*) from Sys_DataColumn where TableName='" + Request["TableName"].ToString() + "' and ModelGuid='" + Request["ModelGuid"].ToString() + "' and Name='" + Request["Name"].ToString() + "'");
                Response.Clear();
                if (int.Parse(obj.ToString()) > 0)
                    Response.Write(-1);
                else
                    Response.Write(database.ExecuteNonQuery(sql));
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write(-2);
                Response.End();
            }
        }

        Database GetDb(string ModelGuid)
        {
            string DBConnection = database.ExecuteScalar("select DBConnection from Sys_DataModel where guid='" + ModelGuid + "'").ToString();
            db = DatabaseFactory.CreateDatabase(DBConnection, "Yawei.DataAccess.SqlClient.SqlDatabase");
            return db;
        }

        /// <summary>
        /// 新建字段
        /// </summary>
        void InsertColumn()
        {
            db = GetDb(Request["ModelGuid"].ToString());
            AddColumnByTableNoBuilded();
        }

        /// <summary>
        /// 未生成表结构时添加字段
        /// </summary>
        void AddColumnByTableNoBuilded()
        {
            string sql = "insert into Sys_DataColumn(ModelGuid,TableName,Name,Type,Lengh,Description,";
            sql += "IsPrimaryKey,IsNull,ExtendProperty)";
            sql += " values('" + Request["ModelGuid"].ToString() + "','" + Request["TableName"].ToString() + "','" + Request["Name"].ToString() + "','varchar',10,'" + Request["Name"].ToString() + "',1,0,'<xml/>')";
            object obj = database.ExecuteScalar("select count(*) from Sys_DataColumn where TableName='" + Request["TableName"].ToString() + "' and ModelGuid='" + Request["ModelGuid"].ToString() + "' and Name='" + Request["Name"].ToString() + "'");
            Response.Clear();
            if (int.Parse(obj.ToString()) > 0)
                Response.Write(-1);
            else
                Response.Write(database.ExecuteNonQuery(sql));
            Response.End();
        }

        /// <summary>
        /// 修改表属性信息
        /// </summary>
        void TableAttr()
        {
            string[] ExtendProperty = null;
            string Attr = "<xml/>";
            if (!string.IsNullOrEmpty(Request["ExtendProperty"].ToString()))
            {
                Attr = "<xml>";
                ExtendProperty = Request["ExtendProperty"].ToString().Split('Ω');
                if (ExtendProperty.Length > 0)
                {
                    for (int i = 0; i < ExtendProperty.Length; i++)
                    {
                        Attr += "<Property " + ExtendProperty[i].Split('^')[0] + "=\"" + ExtendProperty[i].Split('^')[1] + "\" />";
                    }
                }
                Attr += "</xml>";
            }

            string sql = "update Sys_DataTable set Display='" + Request["Display"].ToString() + "',ExtendProperty='" + Attr + "' where ModelGuid='" + Request["ModelGuid"].ToString() + "' and TableName='" + Request["TableName"].ToString() + "';";
            Response.Clear();
            Response.Write(database.ExecuteNonQuery(sql));
            Response.End();
        }

        /// <summary>
        /// 修改表信息
        /// </summary>
        void UpdateTable()
        {
            string TableName = Request["TableName"].ToString();
            string OldName = Request["OldName"].ToString();
            string sql = "update Sys_DataTable set TableName='" + Request["TableName"].ToString() + "' where ModelGuid='" + Request["ModelGuid"].ToString() + "' and TableName='" + Request["OldName"].ToString() + "';";
            sql += "update Sys_DataColumn set TableName='" + Request["TableName"].ToString() + "' where ModelGuid='" + Request["ModelGuid"].ToString() + "' and TableName='" + Request["OldName"].ToString() + "'";
            if (TableName != OldName)
            {
                object obj = database.ExecuteScalar("select count(*) from Sys_DataTable where TableName='" + Request["TableName"].ToString() + "' and ModelGuid='" + Request["ModelGuid"].ToString() + "'");
                Response.Clear();
                if (int.Parse(obj.ToString()) > 0)
                    Response.Write(-1);
                else
                    Response.Write(database.ExecuteNonQuery(sql));
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write(-2);
                Response.End();
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        void InsertTable()
        {
            string sql = "insert into Sys_DataTable(ModelGuid,TableName,Display,ExtendProperty,owner)";
            sql += " values('" + Request["ModelGuid"].ToString() + "','" + Request["TableName"].ToString() + "','" + Request["Display"].ToString() + "','<xml/>','dbo')";
            object obj = database.ExecuteScalar("select count(*) from Sys_DataTable where TableName='" + Request["TableName"].ToString() + "' and ModelGuid='" + Request["ModelGuid"].ToString() + "'");
            Response.Clear();
            if (int.Parse(obj.ToString()) > 0)
                Response.Write(-1);
            else
                Response.Write(database.ExecuteNonQuery(sql));
            Response.End();
        }

        /// <summary>
        /// 生成表
        /// </summary>
        void BuildTable()
        {
            string DBConnection = database.ExecuteScalar("SELECT DBConnection FROM Sys_DataModel WHERE Guid='" + strModelGuid + "'").ToString();
            db = DatabaseFactory.CreateDatabase(DBConnection, "Yawei.DataAccess.SqlClient.SqlDatabase");
            object obj = db.ExecuteScalar("select COUNT(*) from sys.tables WHERE name='" + Request["TableName"].ToString() + "'");
            if (int.Parse(obj.ToString()) > 0)
                db.ExecuteNonQuery("drop table " + Request["TableName"].ToString() + "");
            TableNotBuilded();
        }

        /// <summary>
        /// 第一次生成表
        /// </summary>
        void TableNotBuilded()
        {
            string strTable = "";
            string strColumn = "";
            string strPk = "";//主键字段
            string strTableProperty = "";//表的扩展属性
            string strTableDisplay = "";//表的描述
            string strColumnProperty = "";//字段的扩展属性
            string strColumnDescription = "";//字段说明
            string strColumnDefaultValue = "";//字段默认值
            string TableName = Request["TableName"].ToString();
            DataRow row = database.ExecuteDataSet("select * from Sys_DataTable where ModelGuid='" + strModelGuid + "' and TableName='" + TableName + "'").Tables[0].Rows[0];
            string TableDisplay = row["Display"].ToString();//表说明
            string TableExtendProperty = row["ExtendProperty"].ToString();//表扩展属性
            DataSet doc = database.ExecuteDataSet("select * from Sys_DataColumn where ModelGuid='" + strModelGuid + "' and TableName='" + TableName + "' ORDER BY Tstamp ASC ");
            strTableProperty = GetTableProperty(TableExtendProperty, TableName);//表扩展属性
            strTableDisplay = SetTableDescription(TableName, TableDisplay);//表说明
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                strTable = "CREATE TABLE [dbo].[" + TableName + "]";
                strColumn = "(";
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    string Name = doc.Tables[0].Rows[i]["Name"].ToString();
                    string Type = doc.Tables[0].Rows[i]["Type"].ToString();
                    string Description = doc.Tables[0].Rows[i]["Description"].ToString();
                    string Lengh = doc.Tables[0].Rows[i]["Lengh"].ToString();
                    string Precision = doc.Tables[0].Rows[i]["Precision"].ToString();
                    string IsPrimaryKey = doc.Tables[0].Rows[i]["IsPrimaryKey"].ToString();
                    string IsNull = doc.Tables[0].Rows[i]["IsNull"].ToString();
                    string DefaultValue = doc.Tables[0].Rows[i]["DefaultValue"].ToString();
                    string ColumnExtendProperty = doc.Tables[0].Rows[i]["ExtendProperty"].ToString();
                    strColumnProperty += GetColumnProperty(ColumnExtendProperty, TableName, Name);//扩展属性
                    strColumnDescription += SetColumnDescription(TableName, Name, Description);//字段说明
                    if (i > 0)
                        strColumn += ",";
                    strColumn += "[" + Name + "] " + GetDataType(Type, Lengh, Precision, IsNull, IsPrimaryKey);//字段数据类型，长度，是否为空
                    if (IsPrimaryKey == "0")//主键字段
                        strPk += Name + ",";
                    strColumnDefaultValue += SetColumnDefaultValue(TableName, Name, DefaultValue, Type);//字段默认值
                }
                strPk = strPk.TrimEnd(',');
                strPk = SetTablePrimarks(strPk, TableName);
                strColumn += ");";
                database = DatabaseFactory.CreateDatabase(DBConnection, "Yawei.DataAccess.SqlClient.SqlDatabase");
                database.ExecuteNonQuery(strTable + strColumn);//创建表
                database.ExecuteNonQuery(strTableDisplay + strTableProperty + strColumnDescription + strColumnProperty + strPk + strColumnDefaultValue);//表及字段的一些属性
            }
        }

        /// <summary>
        /// 字段默认值
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        string SetColumnDefaultValue(string Table, string Name, string DefaultValue, string Type)
        {
            string[] Arr = { "bigint", "char", "date", "datetime", "datetime2", "decimal", "float", "int", "nchar", "nvarchar", "smallint", "text", "varchar" };
            string sql = "";
            if (DefaultValue != "")
            {
                if (Type.ToLower().Contains("int") || Type.ToLower() == "float" || Type.ToLower() == "decimal")
                    sql = "ALTER TABLE " + Table + " ADD DEFAULT (" + DefaultValue + ") FOR " + Name + ";";
                if (Type.ToLower().Contains("date"))
                {
                    if (DefaultValue.ToLower() == "getdate()")
                        sql = "ALTER TABLE " + Table + " ADD DEFAULT (" + DefaultValue + ") FOR " + Name + ";";
                    else
                        sql = "ALTER TABLE " + Table + " ADD DEFAULT ('" + DefaultValue + "') FOR " + Name + ";";
                }
                if (Type.ToLower().Contains("char") || Type.ToLower() == "text")
                    sql = "ALTER TABLE " + Table + " ADD DEFAULT ('" + DefaultValue + "') FOR " + Name + ";";
            }
            return sql;
        }

        /// <summary>
        /// 表的主键
        /// </summary>
        /// <param name="Keys"></param>
        /// <param name="Table"></param>
        /// <returns></returns>
        string SetTablePrimarks(string Keys, string Table)
        {
            string sql = "";
            if (Keys != "")
                sql = "ALTER TABLE " + Table + " ADD CONSTRAINT PK_" + Table + " PRIMARY KEY (" + Keys + ");";
            return sql;
        }

        /// <summary>
        /// 表的所有扩展属性
        /// </summary>
        /// <param name="ExtendProperty"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        string GetTableProperty(string ExtendProperty, string Name)
        {
            string sql = "";
            if (ExtendProperty != "<xml />")
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ExtendProperty);
                XmlNode docNode = doc.DocumentElement;

                XmlNodeList nodes = docNode.SelectNodes("Property");
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        XmlAttributeCollection attrCol = node.Attributes;
                        foreach (XmlAttribute xa in attrCol)
                        {
                            sql += "EXECUTE sp_addextendedproperty N'" + xa.Name + "', N'" + xa.Value + "', N'user', N'dbo', N'table', N'" + Name + "', NULL, NULL;";
                        }
                    }
                }
            }
            return sql;
        }

        /// <summary>
        /// 字段的所有扩展属性
        /// </summary>
        /// <param name="ExtendProperty"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        string GetColumnProperty(string ExtendProperty, string TableName, string Name)
        {
            string sql = "";
            if (ExtendProperty != "<xml />" && !string.IsNullOrWhiteSpace(ExtendProperty))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ExtendProperty);
                XmlNode docNode = doc.DocumentElement;
                XmlNodeList nodes = docNode.SelectNodes("Property");
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        XmlAttributeCollection attrCol = node.Attributes;
                        foreach (XmlAttribute xa in attrCol)
                        {
                            sql += "EXECUTE sp_addextendedproperty N'" + xa.Name + "',N'" + xa.Value + "',N'user', N'dbo',N'table',N'" + TableName + "',N'column', N'" + Name + "';";
                        }
                    }
                }
            }
            return sql;
        }

        /// <summary>
        /// 添加表说明
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Description">描述</param>
        /// <returns></returns>
        string SetTableDescription(string Name, string Description)
        {
            string sql = "EXECUTE sp_addextendedproperty N'MS_Description', N'" + Description + "', N'user', N'dbo', N'table', N'" + Name + "', NULL, NULL;";
            return sql;
        }

        /// <summary>
        /// 添加字段说明
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Name">字段名</param>
        /// <param name="Description">描述</param>
        /// <returns></returns>
        string SetColumnDescription(string TableName, string Name, string Description)
        {
            string sql = "EXECUTE sp_addextendedproperty N'MS_Description',N'" + Description + "',N'user', N'dbo',N'table',N'" + TableName + "',N'column', N'" + Name + "';";
            return sql;
        }

        string GetDataType(string strType, string strLength, string strPrecision, string IsNull, string IsPk)
        {
            string value = "";
            string[] ArrNum = { "decimal", "numeric" };
            string[] ArrChar = { "binary", "char", "nchar", "nvarchar", "varbinary", "varchar", "time", "datetime2", "datetimeoffset" };
            string[] ArrOther = { "bigint", "bit", "date", "datetime", "float", "geography", "geometry", "hierarchyid", "image", "int", "money", "ntext", "real", "smalldatetime", "smallint", "smallmoney", "sql_variant", "text", "timestamp", "tinyint", "uniqueidentifier  ", "xml" };
            for (int i = 0; i < ArrNum.Length; i++)
            {
                if (strType == ArrNum[i])
                {
                    value = "[" + strType + "] (18," + strPrecision + ")";
                    break;
                }
            }

            if (value == "")
            {
                for (int i = 0; i < ArrChar.Length; i++)
                {
                    if (strType == ArrChar[i])
                    {
                        value = "[" + strType + "] (" + strLength + ")";
                        break;
                    }
                }
            }

            if (value == "")
            {
                for (int i = 0; i < ArrOther.Length; i++)
                {
                    if (strType == ArrOther[i])
                    {
                        value = "[" + strType + "]";
                        break;
                    }
                }
            }

            if (IsPk == "0")
                value += " not null";
            else
            {
                if (IsNull == "0")
                    value += " not null";
                if (IsNull == "1")
                    value += " null";
            }

            return value;
        }

        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        void GetDBConnection()
        {
            object obj = database.ExecuteScalar("select DBConnection from Sys_DataModel where guid='" + strModelGuid + "'");
            if (obj != null && (obj.ToString() != ""))
            {
                DBConnection = obj.ToString();
                string[] Arr = obj.ToString().Split(';');
                if (Arr != null && Arr.Length > 0)
                {
                    for (int i = 0; i < Arr.Length; i++)
                    {
                        if (Arr[i].Split('=')[0].ToLower() == "initial catalog")
                        {
                            strDBName = Arr[i].Split('=')[1];
                            break;
                        }
                    }
                }
            }
        }

        protected void btnDataModel_Click(object sender, EventArgs e)
        {
            string DBName = Request["DBName"].ToString();
            string DBDisplay = Request["DBDisplay"].ToString();
            string DbSource = Request["DbSource"].ToString();
            string DbCatalog = Request["DbCatalog"].ToString();
            string DbUserID = Request["DbUserID"].ToString();
            string DbPassword = Request["DbPassword"].ToString();
            string DBConnection = "Data Source=" + DbSource + ";Initial Catalog=" + DbCatalog + ";User ID=" + DbUserID + ";Password=" + DbPassword + ";";
            database = DatabaseFactory.CreateDatabase("Data Source=" + DbSource + ";User ID=" + DbUserID + ";Password=" + DbPassword + "", "Yawei.DataAccess.SqlClient.SqlDatabase");

            int count = GetCount(database, "select count(*) from sysdatabases where name='" + DbCatalog + "'");
            if (count == 0)
                database.ExecuteNonQuery("CREATE DATABASE [" + DBName + "]");

            database = DatabaseFactory.CreateDatabase("Data Source=" + DbSource + ";Initial Catalog=" + DbCatalog + ";User ID=" + DbUserID + ";Password=" + DbPassword + "", "Yawei.DataAccess.SqlClient.SqlDatabase");
            count = GetCount(database, "SELECT COUNT(*) FROM SYS.VIEWS WHERE NAME='TableBaseInfo'");
            if (count == 0)
            {
                string ViewSql = "CREATE VIEW [dbo].[TableBaseInfo] AS ";
                ViewSql += "SELECT SO.Name,ISNULL(EP.VALUE,'-') Value,CONVERT(varchar, SO.crdate, 120) as CrDate,";
                ViewSql += "CONVERT(varchar, SO.refdate, 120) as ModifiedTime FROM SYSCOLUMNS SC ";
                ViewSql += "INNER JOIN SYSOBJECTS SO ON SC.ID = SO.ID AND SO.XTYPE = 'U' ";
                ViewSql += "AND SO.NAME <> 'SYSDIAGRAMS' LEFT JOIN SYS.EXTENDED_PROPERTIES EP ";
                ViewSql += "ON SC.ID = EP.MAJOR_ID AND EP.MINOR_ID = 0 AND EP.name='MS_Description'";
                ViewSql += " WHERE (CASE WHEN SC.COLORDER = 1 THEN SO.NAME ELSE ' ' END) <> '';";
                database.ExecuteNonQuery(ViewSql);
            }

            count = GetCount(database, "SELECT COUNT(*) from SYSOBJECTS WHERE ID = object_id(N'TableDetails')");
            if (count == 0)
            {
                string ViewSql = "CREATE PROCEDURE [dbo].[TableDetails] ";
                ViewSql += " @table varchar(100) as declare @sql varchar(max) set @sql='SELECT FieldName=a.name,FieldPK=case when ";
                ViewSql += "exists(SELECT 1 FROM sysobjects where xtype=''PK'' and parent_obj=a.id and name in (SELECT name FROM sysindexes ";
                ViewSql += "WHERE indid in (SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) ";
                ViewSql += "then ''是'' else ''否'' end,FieldDataType=b.name,FieldLength=COLUMNPROPERTY(a.id,a.name,''PRECISION''),";
                ViewSql += "FieldDecDigits=isnull(COLUMNPROPERTY(a.id,a.name,''Scale''),0),FieldNull=case when a.isnullable=1 ";
                ViewSql += "then ''空'' else ''不空'' end,FieldValueDefault=isnull(REPLACE(REPLACE(e.text,''('''''',''''),'''''')'',''''),''''),";
                ViewSql += " FieldDesc=isnull(g.[value],'''') FROM syscolumns a left join systypes b on a.xusertype=b.xusertype ";
                ViewSql += " inner join sysobjects d on a.id=d.id  and d.xtype=''U'' and  d.name!= ''dtproperties'' ";
                ViewSql += " left join syscomments e on a.cdefault=e.id left join sys.extended_properties g on a.id=g.major_id ";
                ViewSql += " and a.colid=g.minor_id  AND g.name=''MS_Description'' left join sys.extended_properties f on d.id=f.major_id ";
                ViewSql += " and f.minor_id=0 AND f.name=''MS_Description'' where d.name='''+@table+'''' exec( @sql)";
                database.ExecuteNonQuery(ViewSql);
            }

            database = DatabaseFactory.CreateDatabase();
            string sql = "insert into Sys_DataModel(Guid,DBName,DBDisplay,DBConnection)";
            sql += " values('" + Guid.NewGuid().ToString() + "','" + DBName + "','" + DBDisplay + "','" + DBConnection + "')";
            database.ExecuteNonQuery(sql);
            Response.Redirect("Index.aspx");
        }

        int GetCount(Database database, string sql)
        {
            return int.Parse(database.ExecuteScalar(sql).ToString());
        }
    }
}