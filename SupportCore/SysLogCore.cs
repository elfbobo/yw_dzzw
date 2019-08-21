
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;
using System.Xml;
using System.Data;
using Yawei.Common;


namespace Yawei.SupportCore
{
    public class SysLogCore
    {
        public bool isHasErrorTabel()
        {
            Database database = DatabaseFactory.CreateDatabase();
            object count = database.ExecuteScalar("select count(1) from sys.tables where name='Sys_ErrorLog'");
            if (count.ToString() != "0")
            {
                return true;
            }
            return false;
        }

        public bool isHasLoginTabel()
        {
            Database database = DatabaseFactory.CreateDatabase();
            object count = database.ExecuteScalar("select count(1) from sys.tables where name='Sys_LoginLog'");
            if (count.ToString() != "0")
            {
                return true;
            }
            return false;
        }

        public bool isHasOperatorTabel()
        {
            Database database = DatabaseFactory.CreateDatabase();
            object count = database.ExecuteScalar("select count(1) from sys.tables where name='Sys_OperatorLog'");
            if (count.ToString() != "0")
            {
                return true;
            }
            return false;
        }


        public void Create(string sql)
        {
            if (sql != "")
            {
                Database database = DatabaseFactory.CreateDatabase();
                database.ExecuteNonQuery(sql);
            }
        }

        /// <summary>数据操作日志
        /// 
        /// </summary>
        /// <param name="projGuid">项目guid</param>
        /// <param name="tableName">数据表名</param>
        /// <param name="dataGuid">数据id</param>
        /// <param name="refGuid">主表id</param>
        /// <param name="operatorType">操作类型（保存、删除、修改，审核。。。。）</param>
        /// <returns></returns>
        public DataSet OperatorLog(string tableName, string dataGuid, string refGuid, string operatorType, string CurrentUserGuid, string CurrentUserDN, string CurrentUserCN)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet doc = database.ExecuteDataSet("select * from Sys_OperatorLog where 1=0");
            doc.Tables[0].TableName = "Sys_OperatorLog";
            DataRow dr = doc.Tables[0].NewRow(); ;
            dr["Guid"] = Guid.NewGuid().ToString();

            dr["TableName"] = tableName;
            dr["DataGuid"] = dataGuid;
            dr["RefGuid"] = refGuid;
            dr["OperatorType"] = operatorType;
            dr["OperatorTime"] = DateTime.Now;
            dr["UserGuid"] = CurrentUserGuid;
            dr["UserDN"] = CurrentUserDN;
            dr["UserCN"] = CurrentUserCN;
            dr["Status"] = 0;
            dr["SysStatus"] = 0;
            doc.Tables[0].Rows.Add(dr);
            return doc;
        }

        public DataSet OperatorLog(string tableName, string dataGuid, string refGuid, string operatorType, string CurrentUserGuid, string CurrentUserDN, string CurrentUserCN,string projGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet doc = database.ExecuteDataSet("select * from Sys_OperatorLog where 1=0");
            doc.Tables[0].TableName = "Sys_OperatorLog";
            DataRow dr = doc.Tables[0].NewRow(); ;
            dr["Guid"] = Guid.NewGuid().ToString();

            dr["TableName"] = tableName;
            dr["DataGuid"] = dataGuid;
            dr["RefGuid"] = refGuid;
            dr["OperatorType"] = operatorType;
            dr["OperatorTime"] = DateTime.Now;
            dr["UserGuid"] = CurrentUserGuid;
            dr["UserDN"] = CurrentUserDN;
            dr["UserCN"] = CurrentUserCN;
            dr["projGuid"] = projGuid;
            dr["Status"] = 0;
            dr["SysStatus"] = 0;
            doc.Tables[0].Rows.Add(dr);
            return doc;
        }

        /// <summary>数据操作日志
        /// 
        /// </summary>
        /// <param name="projGuid">项目guid</param>
        /// <param name="tableName">数据表名</param>
        /// <param name="dataGuid">数据id</param>
        /// <param name="refGuid">主表id</param>
        /// <param name="operatorType">操作类型（保存、删除、修改，审核。。。。）</param>
        /// <returns></returns>
        public DataSet OperatorLog(string tableName, string[] dataGuid, string refGuid, string operatorType, string CurrentUserGuid, string CurrentUserDN, string CurrentUserCN)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet doc = database.ExecuteDataSet("select * from Sys_OperatorLog where 1=0");
            doc.Tables[0].TableName = "Sys_OperatorLog";
            for (int i = 0; i < dataGuid.Length; i++)
            {
                DataRow dr = doc.Tables[0].NewRow();
                dr["Guid"] = Guid.NewGuid().ToString();

                dr["TableName"] = tableName;
                dr["DataGuid"] = dataGuid[i];
                dr["RefGuid"] = refGuid;
                dr["OperatorType"] = operatorType;
                dr["OperatorTime"] = DateTime.Now;
                dr["UserGuid"] = CurrentUserGuid;
                dr["UserDN"] = CurrentUserDN;
                dr["UserCN"] = CurrentUserCN;
                dr["Status"] = 0;
                dr["SysStatus"] = 0;
                doc.Tables[0].Rows.Add(dr);
            }
            return doc;
        }


        public void LoginLog(CurrentUser currentUser, string ip)
        {
            Database database = DatabaseFactory.CreateDatabase();

            database.ExecuteNonQuery("insert into Sys_LoginLog select '" + Guid.NewGuid() + "','" + currentUser.UserGuid + "','" + currentUser.UserCN + "','" + currentUser.UserDN + "','" + ip + "','" + DateTime.Now + "','" + currentUser.UserType + "'");
        }

       
    }
}
