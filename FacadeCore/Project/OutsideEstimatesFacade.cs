using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 概算外事项
    /// </summary>
    public static class OutsideEstimatesFacade
    {
         /// <summary>
        /// 概算外事项修改
        /// </summary>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="Quantity"></param>
        /// <param name="Unit"></param>
        /// <param name="Remark"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int UpdateOutsideEstimates(string ProjOrCostName, string InvestAccount, string Quantity, string Unit, string Remark, string Code)
        {
            return OutsideEstimatesInfo.UpdateOutsideEstimates(ProjOrCostName, InvestAccount, Quantity, Unit, Remark, Code);
        }

        /// <summary>
        /// 删除概算外事项
        /// </summary>
        /// <param name="Code">事项编号</param>
        /// <returns></returns>
        public static int DeleteOutsideEstimates(string Code)
        {
            return OutsideEstimatesInfo.DeleteOutsideEstimates(Code);
        }
    }
}
