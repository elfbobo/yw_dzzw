using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;


namespace Yawei.SupportCore
{
    public class SysFileCore
    {
        #region 基本信息
        public static DataSet GetInfor(string guid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand command = database.CreateCommand(CommandType.Text, "select * from Sys_FileInfo where Guid=@Guid order by UploadDate desc");
            database.AddInParameter(command, "Guid", DbType.String, guid);
            DataSet doc = database.ExecuteDataSet(command);
            doc.Tables[0].TableName = "Sys_FileInfo";
            return doc;
        }

        public static DataSet GetInforByRefGuid(string refGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand command;
            //根据多个refGuid获取文件信息
            if (refGuid.IndexOf(',') >= 0)
            {
                string[] refguids = refGuid.Split(',');
                string refwhere = "";
                for (int i = 0; i < refguids.Length; i++)
                {
                    refwhere += "'" + refguids[i] + "'";
                    if (i != refguids.Length - 1)
                    {
                        refwhere += ",";
                    }
                }
                command = database.CreateCommand(CommandType.Text, "select * from Sys_FileInfo where RefGuid in (" + refwhere + ") order by UploadDate desc");
            }
            else
            {
                command = database.CreateCommand(CommandType.Text, "select * from Sys_FileInfo where RefGuid=@RefGuid order by UploadDate desc");
                database.AddInParameter(command, "RefGuid", DbType.String, refGuid);
            }

            DataSet doc = database.ExecuteDataSet(command);
            doc.Tables[0].TableName = "Sys_FileInfo";
            return doc;
        }


        public static DataSet GetInforByRefGuid(string refGuid, string flieSign)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet doc = new DataSet();
            if (flieSign == "8A223E20-6687-4515-A4E1-AD117A3A48C0" || flieSign == "BFDF4241-8965-4062-BF91-C79E766893C3")
            {
                DbCommand command = database.CreateCommand(CommandType.Text, "SELECT * FROM Sys_FileInfo WHERE RefGuid =(SELECT Guid FROM Busi_LicenseReply WHERE ProjGuid=@refGuid AND ApprovalGuid=@FileSign AND SysStatus<>-1) order by UploadDate desc");
                database.AddInParameter(command, "RefGuid", DbType.String, refGuid);
                database.AddInParameter(command, "FileSign", DbType.String, flieSign);
                doc = database.ExecuteDataSet(command);
            }
            else
            {

                DbCommand command = database.CreateCommand(CommandType.Text, "select * from Sys_FileInfo where RefGuid=@RefGuid and FileSign=@FileSign order by UploadDate desc");
                database.AddInParameter(command, "RefGuid", DbType.String, refGuid);
                database.AddInParameter(command, "FileSign", DbType.String, flieSign);
                doc = database.ExecuteDataSet(command);
            }
            doc.Tables[0].TableName = "Sys_FileInfo";
            return doc;
        }

        #endregion

        #region 内容信息
        public static DataSet GetBlob(string guid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand command = database.CreateCommand(CommandType.Text, "select * from Sys_FileBlob where Guid=@Guid");
            database.AddInParameter(command, "Guid", DbType.String, guid);
            DataSet doc = database.ExecuteDataSet(command);
            doc.Tables[0].TableName = "Sys_FileBlob";
            return doc;
        }

        public static DataSet GetBlobByRefGuid(string refGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand command = database.CreateCommand(CommandType.Text, "select * from Sys_FileBlob where RefGuid=@RefGuid");
            database.AddInParameter(command, "RefGuid", DbType.String, refGuid);
            DataSet doc = database.ExecuteDataSet(command);
            doc.Tables[0].TableName = "Sys_FileBlob";
            return doc;
        }
        #endregion

        #region 综合信息
        public static DataSet GetFile(string guid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet ds;
            DataSet doc = new DataSet();
            DbCommand command;

            command = database.CreateCommand(CommandType.Text, "select * from Sys_FileInfo where Guid=@Guid order by UploadDate desc");
            database.AddInParameter(command, "Guid", DbType.String, guid);
            ds = database.ExecuteDataSet(command);
            ds.Tables[0].TableName = "Sys_FileInfo";
            doc.Merge(ds);

            command = database.CreateCommand(CommandType.Text, "select * from Sys_FileBlob where Guid=@Guid");
            database.AddInParameter(command, "Guid", DbType.String, guid);
            ds = database.ExecuteDataSet(command);
            ds.Tables[0].TableName = "Sys_FileBlob";
            doc.Merge(ds);

            return doc;
        }

        public static DataSet GetFileByRefGuid(string refGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet ds;
            DataSet doc = new DataSet();
            DbCommand command;

            command = database.CreateCommand(CommandType.Text, "select * from Sys_FileInfo where RefGuid=@RefGuid order by UploadDate desc");
            database.AddInParameter(command, "RefGuid", DbType.String, refGuid);
            ds = database.ExecuteDataSet(command);
            ds.Tables[0].TableName = "Sys_FileInfo";
            doc.Merge(ds);

            command = database.CreateCommand(CommandType.Text, "select * from Sys_FileBlob where RefGuid=@RefGuid");
            database.AddInParameter(command, "RefGuid", DbType.String, refGuid);
            ds = database.ExecuteDataSet(command);
            ds.Tables[0].TableName = "Sys_FileBlob";
            doc.Merge(ds);

            return doc;
        }

        public static void DeleteFile(string guid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand command = database.CreateCommand(CommandType.Text, "delete from Sys_FileInfo where Guid=@Guid;delete from Sys_FileBlob where Guid=@Guid");
            database.AddInParameter(command, "Guid", DbType.String, guid);
            database.ExecuteNonQuery(command);
        }

        public static void DeleteFileByRefGuid(string refGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand command = database.CreateCommand(CommandType.Text, "delete from Sys_FileInfo where RefGuid=@RefGuid;delete from Sys_FileBlob where RefGuid=@RefGuid");
            database.AddInParameter(command, "RefGuid", DbType.String, refGuid);
            database.ExecuteNonQuery(command);
        }

        public static void DeleteFileByManyRefGuid(string refGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand command = database.CreateCommand(CommandType.Text, "delete from Sys_FileInfo where RefGuid in(" + refGuid + ");delete from Sys_FileBlob where RefGuid in(" + refGuid + ")");
            // database.AddInParameter(command, "RefGuid", DbType.String, refGuid);
            database.ExecuteNonQuery(command);
        }

        public static DataSet GetModel()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet ds;
            DataSet doc = new DataSet();
            ds = database.ExecuteDataSet("select * from Sys_FileInfo where 1=0");
            ds.Tables[0].TableName = "Sys_FileInfo";
            doc.Merge(ds);

            ds = database.ExecuteDataSet("select * from Sys_FileBlob where 1=0");
            ds.Tables[0].TableName = "Sys_FileBlob";
            doc.Merge(ds);

            return doc;
        }

        #endregion

        #region 数据更新
        public static void Update(DataSet doc)
        {
            Database database = DatabaseFactory.CreateDatabase();
            database.UpdateDataSet(doc);
        }

        public static void Update(DataSet doc, DbTransaction tran)
        {
            Database database = DatabaseFactory.CreateDatabase();
            database.UpdateDataSet(doc, tran);
        }
        #endregion
    }
}
