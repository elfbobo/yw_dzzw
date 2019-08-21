using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 项目催报
    /// </summary>
    public static class WorkRemind
    {
        /// <summary>
        /// 返回当月催报
        /// </summary>
        /// <returns>催报集合</returns>
        public static DataTable GetThiMonthRemind(out string RemindGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet("select top 1 * from Busi_ProjWorkRemind where PubTime>'" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-01 00:00:00" + "' and SysStatus<>-1 order by PubTime desc;select top 1 * from Busi_ProjWorkRemind where SysStatus<>-1 order by PubTime desc");
            DataTable dt = new DataTable();
            RemindGuid = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                RemindGuid = dt.Rows[0]["Guid"].ToString();
            }
            else
            {
                dt = ds.Tables[1];
                if (dt.Rows.Count > 0)
                    RemindGuid = dt.Rows[0]["Guid"].ToString();
            }
            return dt;
        }

        /// <summary>
        /// 返回催报状态
        /// </summary>
        /// <param name="RemindGuid">催报主键</param>
        /// <param name="UserGuid">用户主键</param>
        /// <returns>催报状态集合</returns>
        public static DataTable GetRemindStatus(string RemindGuid, string UserGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select top 1 * from Busi_ProjWorkRemindStatus where RemindGuid='{0}' and OptionUserGuid='{1}' order by OptionDate desc", RemindGuid, UserGuid)).Tables[0];
            return dt;
        }

        /// <summary>
        /// 更新催报状态
        /// </summary>
        /// <param name="RemindGuid">催报主键</param>
        /// <param name="UserGuid">用户主键</param>
        /// <returns>影响行数</returns>
        public static int UpdateRemindStatus(string RemindGuid, string UserGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int result = db.ExecuteNonQuery(string.Format("insert into Busi_ProjWorkRemindStatus (RemindGuid,OptionDate,OptionUserGuid) values ('{0}','{1}','{2}');", RemindGuid, DateTime.Now, UserGuid));
            return result;
        }

        /// <summary>
        /// 更新催报已读
        /// </summary>
        /// <param name="RemindGuid">催报主键</param>
        /// <returns>影响行数</returns>
        public static int UpdateRemindSysStatus(string RemindGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int result = db.ExecuteNonQuery(string.Format("update Busi_ProjWorkRemind set SysStatus=1 where Guid='{0}';", RemindGuid));
            return result;
        }
    }
}
