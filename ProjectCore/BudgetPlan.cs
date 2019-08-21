using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;
using System.Data;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 预算计划下达操作对象
    /// </summary>
   public class BudgetPlan
    {
        Database db = DatabaseFactory.CreateDatabase();
        /// <summary>
        /// 根据项目Guid获取项目名称
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public String getProjNameByGuid(String projGuid) {
            DataSet ds = db.ExecuteDataSet("select * from Busi_ProjRegister where Guid ='"+projGuid+"'");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["ProjName"].ToString();
            }
        return "";
        }
    }
}
