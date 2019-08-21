using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 概算外事项
    /// </summary>
    public static class OutsideEstimatesInfo
    {
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }

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
            string sql = string.Format("UPDATE dbo.Busi_OutsideEstimates SET ProjOrCostName='{0}',InvestAccount={1},Quantity='{2}',Unit='{3}' ,Remark='{4}' where Code = {5}", ProjOrCostName, InvestAccount, Quantity, Unit, Remark, Code);
            int i = GetDatabase().ExecuteNonQuery(sql);
            return i;
        }

        /// <summary>
        /// 删除概算外事项
        /// </summary>
        /// <param name="Code">事项编号</param>
        /// <returns></returns>
        public static int DeleteOutsideEstimates(string Code)
        {
            int count = Convert.ToInt32(GetDatabase().ExecuteScalar("select count(*) from Busi_Con_OutsideAdjustmentRelevance where OutsideEstimatesGuid in (select Guid from Busi_OutsideEstimates where Code=" + Code + ")"));
            int i = 0;
            if (count > 0)
            {
                i = -1;
            }
            else
            {
                string Sql = "Update UpdateOutsideEstimates set Sysstatus=-1 where Code=" + Code + "";
                i = GetDatabase().ExecuteNonQuery(Sql);
            }
            return i;
        }
    }
}
