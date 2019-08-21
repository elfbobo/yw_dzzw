using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 各单位检查信息
    /// </summary>
    public static class UnitsExamine
    {
        /// <summary>
        /// 通过动态行主键更新整改结果及时间
        /// </summary>
        /// <param name="Guid">动态行主键</param>
        /// <param name="Results">整改结果</param>
        /// <param name="RectTime">整改时间</param>
        /// <returns>影响行数</returns>
        public static int UpdateResultByGuid(string Guid, string Results, string RectTime)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int effect = 0;
            effect = db.ExecuteNonQuery(string.Format("update Idty_IssueRect set Results='{0}',RectTime='{1}' where Guid='{2}'", Results, RectTime, Guid));
            return effect;
        }

        /// <summary>
        /// 插入一个问题的整改结果信息
        /// </summary>
        /// <param name="RefGuid"></param>
        /// <param name="Results"></param>
        /// <param name="RectTime"></param>
        /// <param name="DoneSituation"></param>
        /// <returns></returns>
        public static int InsertResult(string RefGuid, string Results, string RectTime, string DoneSituation)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int count = 0;
            string sql = "insert into Idty_IssueResults(Guid, RefGuid, Results, RectTime, Status, SysStatus, CreateDate, DoneSituation) values(@Guid,@RefGuid,@Results,@RectTime,@Status,@SysStatus,@CreateDate,@DoneSituation)";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            cmd.Parameters.Add(new SqlParameter("@RefGuid", RefGuid));
            cmd.Parameters.Add(new SqlParameter("@Results",Results));
            cmd.Parameters.Add(new SqlParameter("@RectTime",RectTime));
            cmd.Parameters.Add(new SqlParameter("@Status", "0"));
            cmd.Parameters.Add(new SqlParameter("@SysStatus", "0"));
            cmd.Parameters.Add(new SqlParameter("@CreateDate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@DoneSituation", DoneSituation));
            count = db.ExecuteNonQuery(cmd);
            return count;
        }

        /// <summary>
        /// 获取某一问题的所有整改结果信息
        /// </summary>
        /// <param name="RefGuid"></param>
        /// <returns></returns>
        public static DataSet GetIssueResults(string RefGuid)
        {
            string sql = "select * from Idty_IssueResults where RefGuid=@RefGuid order by CreateDate desc";
            DataSet doc;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@RefGuid", RefGuid));
            doc = db.ExecuteDataSet(cmd);
            return doc;
        }
    }
}
