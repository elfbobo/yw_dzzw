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
    /// 项目进度推进
    /// </summary>
    public static class ProjAdvanceInfoFacade
    {
        /// <summary>
        /// 获取项目名称和项目总投资
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjNameAndInvest(string ProjGuid)
        {
            return ProjAdvanceInfo.GetProjNameAndInvest(ProjGuid);
        }

        /// <summary>
        /// 获取责任体系中的数据
        /// </summary>
        /// <param name="ProjGuid"></param>
        ///  <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetResponsibility(string ProjGuid, int DepartType)
        {
            return ProjAdvanceInfo.GetResponsibility(ProjGuid, DepartType);
        }

        /// <summary>
        /// 获取项目年度计划信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjPlanYear(string ProjGuid)
        {
            return ProjAdvanceInfo.GetProjPlanYear(ProjGuid);

        }

        /// <summary>
        /// 获取该项目的主要手续
        /// </summary>
        /// <param name="projGuid">项目Guid</param>
        /// <returns></returns>
        public static DataSet GetProjMainProcedure(string projGuid)
        {
            return ProjAdvanceInfo.GetProjMainProcedure(projGuid);
        }

        /// <summary>
        /// 获取该项目其他手续的类型
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjOtherConfig(string projGuid)
        {
            return ProjAdvanceInfo.GetProjOtherConfig(projGuid);
        }


        /// <summary>
        /// 获取该项目的其他手续
        /// </summary>
        /// <param name="projGuid">项目Guid</param>
        /// <param name="Type">手续类型</param>
        /// <returns></returns>
        public static DataSet GetProjOtherProcedure(string projGuid, string Type)
        {
            return ProjAdvanceInfo.GetProjOtherProcedure(projGuid, Type);
        }

        /// <summary>
        /// 获取该项目手续中的子手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjChildProcedure(string ProjGuid,string ApprovalGuid)
        {
            return ProjAdvanceInfo.GetProjChildProcedure(ProjGuid,ApprovalGuid);

        }

        /// <summary>
        /// 获取手续的编制单位
        /// </summary>
        /// <returns></returns>
        public static string GetCompileUnit(string ProjGuid, string ApprovalGuid)
        {
            return ProjAdvanceInfo.GetCompileUnit(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 获取手续的审批单位
        /// </summary>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static string GetApplyUnit(string ApprovalGuid)
        {
            return ProjAdvanceInfo.GetApplyUnit(ApprovalGuid);
        }

        /// <summary>
        /// 获取手续的修订单位
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static string GetReviseUnit(string ProjGuid, string ApprovalGuid)
        {
            return ProjAdvanceInfo.GetReviseUnit(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 获取手续的批复单位、批复文号
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="ApprovalGuid">手续主键</param>
        /// <returns>批复基本信息DataTable</returns>
        public static DataSet GetCstrctProcessReply(string ProjGuid, string ApprovalGuid)
        {
            return ProjAdvanceInfo.GetCstrctProcessReply(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 获取项目手续的审批开始时间
        /// </summary>
        /// <returns></returns>
        public static string GetProjMainPrepStartDate(string ProjGuid, string ApprovalGuid)
        {
            return ProjAdvanceInfo.GetProjMainPrepStartDate(ProjGuid, ApprovalGuid);

        }

        /// <summary>
        /// 获取项目手续的审批办结时间
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static string GetProjMainPrepEndDate(string ProjGuid, string ApprovalGuid)
        {
            return ProjAdvanceInfo.GetProjMainPrepEndDate(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 获取项目子手续的审批开始时间
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static string GetProjChildPrepStartDate(string ProjGuid, string ApprovalGuid, string ApprovalChildType)
        {
            return ProjAdvanceInfo.GetProjChildPrepStartDate(ProjGuid, ApprovalGuid, ApprovalChildType);
        }

        /// <summary>
        /// 获取项目子手续的审批办结时间
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static string GetProjChildPrepEndDate(string ProjGuid, string ApprovalGuid, string ApprovalChildType)
        {
            return ProjAdvanceInfo.GetProjChildPrepEndDate(ProjGuid, ApprovalGuid, ApprovalChildType);
        }

        /// <summary>
        /// 获取项目主手续的月度进展信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjMainMonthPrepInfo(string ProjGuid, string ApprovalGuid)
        {
            return ProjAdvanceInfo.GetProjMainMonthPrepInfo(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 获取项目子手续的月度进展信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static DataSet GetProjChildMonthPrepInfo(string ProjGuid, string ApprovalGuid, string ApprovalChildType)
        {
            return ProjAdvanceInfo.GetProjChildMonthPrepInfo(ProjGuid, ApprovalGuid, ApprovalChildType);

        }

        /// <summary>
        /// 获取当前年度的项目月度计划、计划完成投资、资金使用计划
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataSet GetProjMonthPlan(string projGuid,int month)
        {
            return ProjAdvanceInfo.GetProjMonthPlan(projGuid,month);
        }

        
        /// <summary>
        /// 计划使用资金小计
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static decimal GetPlanFinancial(string projGuid, int month)
        {
            return ProjAdvanceInfo.GetPlanFinancial(projGuid, month);
        }

        /// <summary>
        /// 项目实际进展情况（形象进度月报）
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static string GetProjConstructReport(string projGuid, int Month)
        {
            return ProjAdvanceInfo.GetProjConstructReport(projGuid, Month);

        }

        /// <summary>
        /// 项目实际月完成投资
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static decimal GetProjMonthInvestComplete(string projGuid, int Month)
        {
            return ProjAdvanceInfo.GetProjMonthInvestComplete(projGuid, Month);

        }

        /// <summary>
        /// 资金到位合计
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="Month"></param>
        /// <param name="monthType"></param>
        /// <returns></returns>
        public static decimal GetProjFundsMonthly(string projGuid, int Month, string monthType)
        {
            return ProjAdvanceInfo.GetProjFundsMonthly(projGuid, Month, monthType);

        }

        /// <summary>
        /// 月资金支付合计
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static decimal GetProjFundPayment(string projGuid, int month)
        {
            return ProjAdvanceInfo.GetProjFundPayment(projGuid, month);

        }

        /// <summary>
        /// 支付金额来源
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataSet GetProjInvestFromDetails(string projGuid, int month)
        {
            return ProjAdvanceInfo.GetProjInvestFromDetails(projGuid, month);

        }
    }
}
