using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;


namespace Yawei.ProjectCore
{
    /// <summary>
    /// 项目管理公共类
    /// </summary>
    public class FormatProject
    {
        /// <summary>
        /// 根据项目名称获取项目主键
        /// </summary>
        /// <param name="projGuid">项目主键以&分隔</param>
        /// <returns></returns>
        public static string GetProjNameByGuid(string projGuid)
        {
            projGuid = "'" + projGuid.Replace("&", "','") + "'";
            var database = GetDatabase();
            DataSet ds = database.ExecuteDataSet("select * from Busi_ProjRegister where guid in(" + projGuid + ")");
            var projJson = "";
            foreach (DataRow dt in ds.Tables[0].Rows)
            {
                projJson += "{ProjGuid:'" + dt["guid"] + "',ProjName:'" + dt["ProjName"] + "'},";
            }
            return "[" + projJson.TrimEnd(',') + "]";
        }
        /// <summary>
        /// 根据项目名称获取项目主键
        /// </summary>
        /// <param name="projGuid">项目主键以&分隔</param>
        /// <returns></returns>
        public static Dictionary<string,string> GetProjDicByGuid(string projGuid)
        {
            var database = GetDatabase();
            DataSet ds = database.ExecuteDataSet("select * from tz_Project where ProGuid ='" + projGuid + "'");
            Dictionary<string, string> document = new Dictionary<string, string>();
            if(ds.Tables[0].Rows.Count>0)
            {
                document["ProGuid"] = ds.Tables[0].Rows[0]["ProGuid"].ToString();
                document["ProName"] = ds.Tables[0].Rows[0]["ProName"].ToString();
                //document["InvestType"] = ds.Tables[0].Rows[0]["InvestType"].ToString();
                //document["NationType"] = ds.Tables[0].Rows[0]["NationType"].ToString();
            }
            return document;
        }
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// 判断table中是否已经存在了该项目
        /// </summary>
        /// <param name="table">要判断的表</param>
        /// <param name="ProjGuid">该表中的项目Guid</param>
        /// <returns></returns>
        public static bool IsExistProject(string table, string ProjGuid)
        {
            bool isProj = false;
            int i = Convert.ToInt32(GetDatabase().ExecuteScalar("select count(*) from " + table + " where ProjGuid='" + ProjGuid + "' and SysStatus <>-1"));
            if (i > 0)
            {
                isProj = true;
            }

            return isProj;
        }

        /// <summary>
        /// 项目信息审核退回
        /// </summary>
        /// <param name="table">表名称</param>
        /// <param name="status">审核状态 1 审核 2 退回</param>
        /// <param name="refGuid">数据主键</param>
        /// <param name="projGuid">项目主键</param>
        /// <param name="remark">意见</param>
        /// <param name="logDs">操作日志程序集</param>
        /// <param name="url">当前地址</param>
        /// <returns></returns>
        public static void InfoAction(string table, string status, string refGuid, string projGuid, string remark, DataSet logDs, string url)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DataSet ds = db.ExecuteDataSet("select * from Busi_ProjConfirmList where 1=2"); //想审核意见表添加意见
            DataRow dr = ds.Tables[0].NewRow();
            dr["Guid"] = Guid.NewGuid().ToString();
            dr["ProjGuid"] = projGuid;
            dr["RefGuid"] = refGuid;
            dr["Remark"] = remark;
            dr["Date"] = DateTime.Now.ToString();
            dr["_Table"] = table;
            dr["Status"] = status;
            ds.Tables[0].Rows.Add(dr);
            ds.Tables[0].TableName = "Busi_ProjConfirmList";
            if (status == "2") //想退回信息表插入数据
            {
                DataSet doc = db.ExecuteDataSet("select * from Busi_RebackInfo where 1=2");
                DataRow drw = doc.Tables[0].NewRow();
                drw["Guid"] = Guid.NewGuid().ToString();
                drw["ProjGuid"] = projGuid;
                drw["RefGuid"] = refGuid;
                drw["_Table"] = table;
                drw["Date"] = DateTime.Now.ToString();
                drw["url"] = url;
                doc.Tables[0].Rows.Add(drw);
                doc.Tables[0].TableName = "Busi_RebackInfo";
                ds.Merge(doc);
            }

            if (logDs != null)
            {
                ds.Merge(logDs);
            }
            if (status == "1")
            {
                IDbConnection conn = db.CreateConnection();
                conn.Open();
                IDbCommand cmd = db.CreateCommand(CommandType.Text, "delete from Busi_AdminConfirm where refGuid=@refGuid");
                cmd.Parameters.Add(new SqlParameter("@refGuid", refGuid));
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            DbCommand cmad = db.CreateCommand(CommandType.Text, "select * from " + table + " where guid=@guid");
            cmad.Parameters.Add(new SqlParameter("@guid", refGuid));
            DataSet baseDs = db.ExecuteDataSet(cmad, table);
            if (baseDs.Tables[0].Rows.Count > 0)
            {
                baseDs.Tables[0].Rows[0]["Status"] = status;
                baseDs.Tables[0].TableName = table;
                ds.Merge(baseDs);
            }
            db.UpdateDataSet(ds);
        }

        /// <summary>
        /// 取消审核功能
        /// </summary>
        /// <param name="table">表名称</param>
        /// <param name="refGuid">数据主键</param>
        /// <param name="projGuid">项目主键</param>
        /// <param name="url">当前url</param>
        /// /// <param name="log">当前url</param>
        public static void CannelConfirm(string table, string refGuid, string projGuid, string url,DataSet log)
        {
            Database db = GetDatabase();
            DbCommand cmad = db.CreateCommand(CommandType.Text, "select * from " + table + " where guid=@guid");
            cmad.Parameters.Add(new SqlParameter("@guid", refGuid));
            DataSet ds = db.ExecuteDataSet(cmad);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet confirmDoc = db.ExecuteDataSet("select * from Busi_AdminConfirm where 1=2");
                DataRow d = confirmDoc.Tables[0].NewRow();
                d["Guid"] = Guid.NewGuid().ToString();
                d["RefGuid"] = refGuid;
                d["projGuid"] = projGuid;
                d["_table"] = table;
                d["url"] = url;
                confirmDoc.Tables[0].Rows.Add(d);
                confirmDoc.Tables[0].TableName = "Busi_AdminConfirm";

                ds.Tables[0].Rows[0]["Status"] = "0";
                ds.Tables[0].TableName = table;

                ds.Merge(confirmDoc);
                if (log != null)
                    ds.Merge(log);
                db.UpdateDataSet(ds);
            }
        }

        /// <summary>
        /// 获取审核意见
        /// </summary>
        /// <param name="guid">数据主键</param>
        /// <param name="table">表名</param>
        /// <param name="projGuid">项目主键</param>
        /// <returns>json格式的意见</returns>
        public static string GetProjConfirmInfo(string guid, string table,string projGuid)
        {
            Database db = GetDatabase();
            DataSet ds = db.ExecuteDataSet("select * from Busi_ProjConfirmList where projGuid='"+projGuid+"' and refGuid='"+guid+"' and _table='"+table+"' order by date desc");
            string json = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                    json += ",";
                json += "{date:'" + ds.Tables[0].Rows[i]["date"].ToString() + "',status:'" + ds.Tables[0].Rows[i]["status"].ToString() + "',remark:'" + ds.Tables[0].Rows[i]["remark"].ToString() + "'}";
            }
            return "[" + json + "]";
        }
    }
}
