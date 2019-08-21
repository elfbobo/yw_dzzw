using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 资金来源明细对象
    /// </summary>
    public class InvestFromDetailsFacade
    {
        /// <summary>
        /// 根据月报guid获取资金来源明细guid
        /// </summary>
        /// <param name="refGuid"></param>
        /// <returns></returns>
        public string GetGuidByRefGuid(string refGuid)
        {
            InvestFromDetails investFromDetails = new InvestFromDetails();
            if (investFromDetails.GetGuidByRefGuid(refGuid) == null)
            {
                return "";
            }
            return investFromDetails.GetGuidByRefGuid(refGuid).Tables[0].Rows[0]["Guid"].ToString();
        }

        /// <summary>
        /// 根据概算主键返回概算单项名称
        /// </summary>
        /// <param name="Guids">主键集合</param>
        /// <returns>名称集合</returns>
        public string GetFundNameByGuid(string Guids)
        {
            InvestFromDetails investFromDetails = new InvestFromDetails();
            return investFromDetails.GetFundNameByGuid(Guids);
        }

        /// <summary>
        /// 根据概算主键返回概算单项名称
        /// </summary>
        /// <param name="Guids">主键集合</param>
        /// <param name="Datas">名称集合</param>
        public void GetFundNameByGuid(string Guids,out string Datas)
        {
            InvestFromDetails investFromDetails = new InvestFromDetails();
            investFromDetails.GetFundNameByGuid(Guids, out Datas);
        }
       

        /// <summary>
        /// 11-18
        /// 支付条件已填，但没有合同，删除支付条件
        /// </summary>
        /// <param name="Guid"></param>
        /// <param name="fieldName">字段</param>
        /// <param name="tableName"></param>
        public int DeleteContractPay(string Guid, string fieldName, string tableName)
        {
            InvestFromDetails investFromDetails = new InvestFromDetails();
            return investFromDetails.DeleteContractPay(Guid, fieldName, tableName);
        }
    }
}
