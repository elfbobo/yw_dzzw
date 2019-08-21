using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 签订合同
    /// </summary>
    public static class SignedContractFacade
    {
        /// <summary>
        /// 通过合同主键返回合同关联概算主键
        /// </summary>
        /// <param name="strGuid">合同主键</param>
        /// <returns>概算主键集合</returns>
        public static string GetGuidsByStrGuid(string strGuid)
        {
            return SignedContract.GetGuidsByStrGuid(strGuid);
        }

        /// <summary>
        /// 通过项目Guid取招标事项Json串
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns>招标事项Json数据集</returns>
        public static string GetJsonByProjGuid(string ProjGuid)
        {
            return SignedContract.GetJsonByProjGuid(ProjGuid);
        }

        /// <summary>
        /// 通过招标主键获取招标名称
        /// </summary>
        /// <param name="InviteGuid">招标主键</param>
        /// <returns>招标名称</returns>
        public static string GetInviteNameByGuid(string InviteGuid)
        {
            return SignedContract.GetInviteNameByGuid(InviteGuid);
        }

        /// <summary>
        /// 通过招标主键获取招标名称集合
        /// </summary>
        /// <param name="InviteGuids">招标主键集合</param>
        /// <returns>招标名称集合</returns>
        public static string GetInviteJsonByGuid(string InviteGuids)
        {
            return SignedContract.GetInviteJsonByGuid(InviteGuids);
        }

        /// <summary>
        /// 通过合同主键返回合同名称和合同编号
        /// </summary>
        /// <param name="ContractGuid">合同主键</param>
        /// <returns>合同名称和编号</returns>
        public static string GetContractInfoByGuid(string ContractGuid)
        {
            return SignedContract.GetContractInfoByGuid(ContractGuid);
        }
    }
}
