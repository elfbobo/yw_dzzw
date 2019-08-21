using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 更新批复关联的概算调整
    /// </summary>
    public  class UpdateAdjustmentFacade
    {
        UpdateAdjustment ua = new UpdateAdjustment();
        /// <summary>
        /// 批复关联的概算调整
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <param name="CheckGuid"></param>
        /// <returns></returns>
        public int UpdateAdjustment(string tableName, string where, string CheckGuid)
        {
            return ua.UpdateAdjustmentRelevance(tableName, where, CheckGuid);
        }
    }
}
