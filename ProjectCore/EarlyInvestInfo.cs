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
    /// 前期投资事项
    /// </summary>
    public static  class EarlyInvestInfo
    {
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// 前期投资事项修改
        /// </summary>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="CostType"></param>
        /// <param name="Remark"></param>
        /// <param name="DealCase"></param>
        /// <param name="PlanAmount"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int UpdateEarlyInvest(string ProjOrCostName, string InvestAccount, string CostType, string Remark, string DealCase, string PlanAmount, string Code)
        {
            int i = 0;
            string sql = string.Format("UPDATE dbo.Busi_EarlyInvest SET ProjOrCostName='{0}',InvestAccount={1},CostType='{2}',Remark='{3}' ,DealCase='{4}',PlanAmount={5} where Code ='{6}'", ProjOrCostName, InvestAccount, CostType, Remark, DealCase, PlanAmount, Code);
            i = GetDatabase().ExecuteNonQuery(sql);
            return i;
        }

        /// <summary>
        /// 删除前期投资事项
        /// </summary>
        /// <param name="Code">事项编号</param>
        /// <returns></returns>
        public static int DeleteEarlyInvest(string Code)
        {
            string Sql = "Update Busi_EarlyInvest set Sysstatus=-1 where Code='" + Code + "'";//更新主节点
            int i = GetDatabase().ExecuteNonQuery(Sql);
            if (i > 0) 
            {
                int count = Convert.ToInt32(GetDatabase().ExecuteScalar("select count(*) from Busi_EarlyInvest where TopGuid=(select Guid from Busi_EarlyInvest where Code='" + Code + "')"));
                if (count > 0)
                {
                    string childrenSql = "Update Busi_EarlyInvest set Sysstatus=-1 where TopGuid=(select Guid from Busi_EarlyInvest where Code='" + Code + "')";
                    GetDatabase().ExecuteNonQuery(childrenSql);
                }
            }
            return i;
        }

        /// <summary>
        /// 插入前期投资事项和概算关联关系
        /// </summary>
        /// <param name="EarlyInvestGuid"></param>
        /// <param name="EstimateGuid"></param>
        /// <returns></returns>
        public static int InsertEarlyInvestAndEstimateRelevance(string EarlyInvestGuid,string EstimateGuid) 
        {
            GetDatabase().ExecuteNonQuery("delete from Busi_Con_EarlyInvestAndEstimateRelevance where EarlyInvestGuid='" + EarlyInvestGuid + "'");
            string[] Estimate = EstimateGuid.Split(',');
            for (int i = 0; i < Estimate.Length; i++)
            {
                string sql = "insert into Busi_Con_EarlyInvestAndEstimateRelevance  (Guid, EarlyInvestGuid, EstimateGuid) VALUES ('" + Guid.NewGuid().ToString() + "', '" + EarlyInvestGuid + "', '" + Estimate[i] + "')";
                GetDatabase().ExecuteNonQuery(sql);
            }
            return 0;
        }
    }
}
