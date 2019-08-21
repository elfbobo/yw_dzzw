using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 审批手续保存
    /// </summary>
    public static class CstrctProcess
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
            Database db = DatabaseFactory.CreateDatabase();
            int result = 0;
            result = Convert.ToInt32(db.ExecuteScalar(SelectSql));
            if (result == 0)
                result = db.ExecuteNonQuery(InsertSql);
            else
                result = db.ExecuteNonQuery(UpdataSql);
            return result;
        }
    }
}
