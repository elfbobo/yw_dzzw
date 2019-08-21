using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 审批手续保存
    /// </summary>
    public static class CstrctProcessFacade
    {
        /// <summary>
        /// 执行更新语句
        /// </summary>
        /// <param name="SelectSql"></param>
        /// <param name="InsertSql"></param>
        /// <param name="UpdataSql"></param>
        /// <returns></returns>
        public static int DoInsertUpdateSqlCommand(string SelectSql, string InsertSql, string UpdataSql)
        {
            return CstrctProcess.DoInsertUpdateSqlCommand(SelectSql, InsertSql, UpdataSql);
        }
    }
}
