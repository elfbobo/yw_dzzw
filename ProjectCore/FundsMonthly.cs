using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 资金到位月报
    /// </summary>
    public class FundsMonthly
    {
        /// <summary>
        /// 获取预算文号
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetPayNum(string projGuid, string type)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = new DataSet();
            string where = string.Empty;
            switch (type)
            {
                case "1":
                    where = " planType=2";
                    break;
                case "2":
                    where = " planType=4 ";
                    break;
                case "3":
                    where = " planType=5 ";
                    break;
                case "4":
                    where = " planType=6 ";
                    break;
                default:
                    where = " 1=2 ";
                    break;
            }
            ds = db.ExecuteDataSet("SELECT  PlanDocNum FROM [Busi_BudgetPlan] where sysstatus<>-1 and  ProjGuid='" + projGuid + "' and " + where);
            return ds.Tables[0];
        }
    }
}
