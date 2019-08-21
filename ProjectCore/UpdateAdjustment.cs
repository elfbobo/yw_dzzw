using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 更新批复关联的概算调整
    /// </summary>
    public class UpdateAdjustment
    {
        Database db = DatabaseFactory.CreateDatabase();
        /// <summary>
        /// 批复关联的概算调整
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <param name="CheckGuid"></param>
        /// <returns></returns>
        public int UpdateAdjustmentRelevance(string tableName, string where, string CheckGuid) 
        {
            string sql="Update "+tableName+" set CheckGuid='"+CheckGuid+"' where 1=1 "+where+"";
            int i = db.ExecuteNonQuery(sql);
            return i;
        }
    }
}
