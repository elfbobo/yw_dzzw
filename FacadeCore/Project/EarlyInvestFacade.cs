using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 前期投资事项
    /// </summary>
    public static class EarlyInvestFacade
    {
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
            return EarlyInvestInfo.UpdateEarlyInvest(ProjOrCostName, InvestAccount, CostType, Remark, DealCase, PlanAmount, Code);
        }

        /// <summary>
        /// 删除前期投资事项
        /// </summary>
        /// <param name="Code">事项编号</param>
        /// <returns></returns>
        public static int DeleteEarlyInvest(string Code)
        {
            return EarlyInvestInfo.DeleteEarlyInvest(Code);
        }

        /// <summary>
        /// 插入前期投资事项和概算关联关系
        /// </summary>
        /// <param name="EarlyInvestGuid"></param>
        /// <param name="EstimateGuid"></param>
        /// <returns></returns>
        public static int InsertEarlyInvestAndEstimateRelevance(string EarlyInvestGuid, string EstimateGuid)
        {
            return EarlyInvestInfo.InsertEarlyInvestAndEstimateRelevance(EarlyInvestGuid, EstimateGuid);
        }
    }
}
