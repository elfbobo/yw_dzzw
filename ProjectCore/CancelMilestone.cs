using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 取消里程碑
    /// </summary>
    public class CancelMilestone
    {
        /// <summary>
        /// 通过里程碑的Guid来取消里程碑
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public int UpdateCancelMilestoneByGuid(string guid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "update [GovProjSupevise].[dbo].[Busi_Milestone] set HandleStatus=0,HandlingDate=null,MakedDate=null where guid='" + guid + "'";
            return db.ExecuteNonQuery(sql);
        }
    }
}
