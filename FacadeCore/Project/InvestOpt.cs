using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Yawei.IndustryManagerCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 投资管理
    /// </summary>
    public class InvestOpt
    {
        /// <summary>
        /// 获取所有日常检查附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDailyCheckUp()
        {
            return IMCBaseInfo.GetDailyCheckUp();
        }

        /// <summary>
        /// 获取所有检查附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetCheckUp()
        {
            return IMCBaseInfo.GetCheckUp();
        }

        /// <summary>
        /// 获取所有资金支付附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetIvstPaymentUp()
        {
            return IMCBaseInfo.GetIvstPaymentUp();
        }

        /// <summary>
        /// 获取所有支出预算下达附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDeptFilesUp()
        {
            return IMCBaseInfo.GetDeptFilesUp();
        }

        /// <summary>
        /// 获取所有审计通知书附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAuditNoticeUp()
        {
            return IMCBaseInfo.GetAuditNoticeUp();
        }

        /// <summary>
        /// 获取所有审计报告附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAuditReporUp()
        {
            return IMCBaseInfo.GetAuditReporUp();
        }

        /// <summary>
        /// 获取所有招标投标 、计划管理 、会议纪要 附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetFileUp()
        {
            return IMCBaseInfo.GetFileUp();
        }

        /// <summary>
        /// 获取该项目下年度计划下达计划金额
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static object GetInvestPlanAccount(string ProjGuid)
        {
            return IMCBaseInfo.GetInvestPlanAccount(ProjGuid);
        }

        /// <summary>
        /// 获取该项目下年度计划安排安排金额
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static object GetFundArrangeAccount(string ProjGuid)
        {
            return IMCBaseInfo.GetFundArrangeAccount(ProjGuid);
        }

        /// <summary>
        /// 获取所有部门名称
        /// </summary>
        /// <returns></returns>
        public static DataSet GetMapUnitName()
        {
            return IMCBaseInfo.GetMapUnitName();
        }

        /// <summary>
        /// 获取该项目下的所有手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetAllProcedure_Config(string ProjGuid)
        {
            return IMCBaseInfo.GetAllProcedureConfig(ProjGuid);
        }

        /// <summary>
        /// 获取该项目下的所有子手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetAllProcedureChildConfig(string ProjGuid)
        {
            return IMCBaseInfo.GetAllProcedureChildConfig(ProjGuid);
        }

        /// <summary>
        /// 根据项目主键获取该项目的TopGuid
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetProjTopGuid(string ProjGuid)
        {
            return IMCBaseInfo.GetProjTopGuid(ProjGuid);
        }

        /// <summary>
        /// 插入一条审批进度记录
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="DateOfCompletion"></param>
        /// <param name="ActualCompletionDate"></param>
        /// <param name="ApplyForStatus"></param>
        /// <param name="SolveResults"></param>
        /// <param name="ActualDuration"></param>
        /// <param name="UndoneReason"></param>
        /// <param name="Problems"></param>
        /// <param name="Solutions"></param>
        /// <param name="ApprovalChildType"></param>
        /// <param name="ProgressSituation"></param>
        /// <returns></returns>
        public static int AddAppProgress(string ProjGuid, string ApprovalGuid, string ApprovalChildType, string DateOfCompletion, string ActualCompletionDate, string ApplyForStatus, string SolveResults, string ActualDuration, string UndoneReason, string Problems, string Solutions, string ProgressSituation)
        {
            return IMCBaseInfo.AddAppProgress(ProjGuid, ApprovalGuid, ApprovalChildType, DateOfCompletion, ActualCompletionDate, ApplyForStatus, SolveResults, ActualDuration, UndoneReason, Problems, Solutions, ProgressSituation);
        }

        /// <summary>
        /// 修改一条审批进度记录
        /// </summary>
        /// <param name="PrepGuid"></param>
        /// <param name="DateOfCompletion"></param>
        /// <param name="ActualCompletionDate"></param>
        /// <param name="ApplyForStatus"></param>
        /// <param name="SolveResults"></param>
        /// <param name="ActualDuration"></param>
        /// <param name="UndoneReason"></param>
        /// <param name="Problems"></param>
        /// <param name="Solutions"></param>
        /// <param name="ProgressSituation"></param>
        /// <returns></returns>
        public static int UpdateAppProgress(string PrepGuid, string DateOfCompletion, string ActualCompletionDate, string ApplyForStatus, string SolveResults, string ActualDuration, string UndoneReason, string Problems, string Solutions, string ProgressSituation)
        {
            return IMCBaseInfo.UpdateAppProgress(PrepGuid, DateOfCompletion, ActualCompletionDate, ApplyForStatus, SolveResults, ActualDuration, UndoneReason, Problems, Solutions, ProgressSituation);
        }

        /// <summary>
        /// 获取所有审批进度
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAppProgress()
        {
            return IMCBaseInfo.GetAppProgress();
        }

        /// <summary>
        /// 获取该项目下的第一条建的进度月报
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static DataSet GetFirstAppProgress(string ProjGuid, string ApprovalGuid, string ApprovalChildType)
        {
            return IMCBaseInfo.GetFirstAppProgress(ProjGuid, ApprovalGuid, ApprovalChildType);
        }

        /// <summary>
        /// 根据主键获取审批进度信息
        /// </summary>
        /// <param name="PrepGuid"></param>
        /// <returns></returns>
        public static DataSet GetAppProgressByGuid(string PrepGuid)
        {
            return IMCBaseInfo.GetAppProgressByGuid(PrepGuid);
        }

        /// <summary>
        /// 根据主键获取审批进度信息
        /// </summary>
        /// <param name="PrepGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static DataSet GetAppProgressByGuid(string PrepGuid, string ApprovalChildType)
        {
            return IMCBaseInfo.GetAppProgressByGuid(PrepGuid, ApprovalChildType);
        }

        /// <summary>
        /// 读取变更申请信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static DataSet GetChangeRequest(string ProjGuid, string ApprovalGuid)
        {
            return IMCBaseInfo.GetChangeRequest(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 读取变更申请信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static DataSet GetChangeRequest(string ProjGuid, string ApprovalGuid, string ApprovalChildType)
        {
            return IMCBaseInfo.GetChangeRequest(ProjGuid, ApprovalGuid, ApprovalChildType);
        }

        /// <summary>
        /// 插入变更申请信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <param name="ApplyReason"></param>
        /// <param name="ApplicantGuid"></param>
        /// <param name="ApplicantName"></param>
        /// <param name="PlanStartDate"></param>
        /// <param name="PlanEndDate"></param>
        /// <returns></returns>
        public static int AddChangeRequest(string ProjGuid, string ApprovalGuid, string ApprovalChildType, string ApplyReason, string ApplicantGuid, string ApplicantName, string PlanStartDate, string PlanEndDate)
        {
            return IMCBaseInfo.AddChangeRequest(ProjGuid, ApprovalGuid, ApprovalChildType, ApplyReason, ApplicantGuid, ApplicantName, PlanStartDate, PlanEndDate);
        }

        /// <summary>
        /// 环节是否已经有变更申请信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static bool CheckChangeRequest(string ProjGuid, string ApprovalGuid)
        {
            int v = int.Parse(IMCBaseInfo.CheckChangeRequest(ProjGuid, ApprovalGuid).ToString());
            if (v > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 环节是否已经有变更申请信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static bool CheckChangeRequest(string ProjGuid, string ApprovalGuid, string ApprovalChildType)
        {
            int v = int.Parse(IMCBaseInfo.CheckChangeRequest(ProjGuid, ApprovalGuid, ApprovalChildType).ToString());
            if (v > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 环节手续是否在建设过程中
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static bool CheckCstrctProcess(string ProjGuid, string ApprovalGuid)
        {
            int v = int.Parse(IMCBaseInfo.CheckCstrctProcessByGuid(ProjGuid, ApprovalGuid).ToString());
            if (v > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 返回子手续计划
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="ApprovalGuid">主手续主键</param>
        /// <returns>返回子手续计划</returns>
        public static DataTable GetApproveChildStatusDT(string ProjGuid, string ApprovalGuid)
        {
            return IMCBaseInfo.GetApproveChildStatus(ProjGuid, ApprovalGuid); ;
        }

        /// <summary>
        /// 获取所有子手续数据
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="ApprovalGuid">主手续主键</param>
        /// <returns>所有子手续数据集</returns>
        public static DataSet GetApproveChildDatas(string ProjGuid, string ApprovalGuid)
        {
            return IMCBaseInfo.GetApproveChildDatas(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 环节手续是否在证照批复中
        /// </summary>
        /// <param name="ApproveName"></param>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static bool CheckApprove(string ApproveName, string ProjGuid, string ApprovalGuid)
        {
            int v = 0;
            switch (ApproveName)
            {
                case "项目建议书":
                    v = int.Parse(IMCBaseInfo.CheckApproveByProjGuid("Busi_ProjProposal", ProjGuid).ToString());
                    break;
                case "项目可行性研究报告":
                    v = int.Parse(IMCBaseInfo.CheckApproveByProjGuid("Busi_ProjFsbtyStudy", ProjGuid).ToString());
                    break;
                case "项目初步设计及概算":
                    v = int.Parse(IMCBaseInfo.CheckApproveByProjGuid("Busi_ProjInitialDesign", ProjGuid).ToString());
                    break;
                case "项目预算":
                    v = int.Parse(IMCBaseInfo.CheckApproveByProjGuid("Busi_ProjButget", ProjGuid).ToString());
                    break;
                default:
                    v = int.Parse(IMCBaseInfo.CheckApproveByGuid(ProjGuid, ApprovalGuid).ToString());
                    break;

            }

            if (v > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 施工进度更新施工计划的计划整体完成情况
        /// </summary>
        /// <param name="PlanGuid"></param>
        /// <param name="BegDate"></param>
        /// <param name="CompleteDate"></param>
        /// <param name="BudgetAmount"></param>
        /// <param name="ActualTaskLimit"></param>
        /// <param name="IsComplete"></param>
        public static void InitialCstrctPlanBySchedule(string PlanGuid, string BegDate, string CompleteDate, string BudgetAmount, string ActualTaskLimit, string IsComplete)
        {
            IMCBaseInfo.InitialCstrctPlanBySchedule(PlanGuid, BegDate, CompleteDate, BudgetAmount, ActualTaskLimit, IsComplete);
        }

        /// <summary>
        /// 根据主键获取施工计划信息
        /// </summary>
        /// <param name="PlanGuid"></param>
        /// <returns></returns>
        public static DataSet GetCstrctPlanInfoByGuid(string PlanGuid)
        {
            return IMCBaseInfo.GetCstrctPlanInfoByGuid(PlanGuid);
        }

        /// <summary>
        /// 获取施工计划的所有前置计划序号
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="Guid"></param>
        /// <param name="ddl"></param>
        public static void GetCstrctPlanPreNum(string ProjGuid, string Guid, DropDownList ddl)
        {
            DataSet doc = IMCBaseInfo.GetCstrctPlanPreNum(ProjGuid, Guid);
            ddl.Items.Add(new ListItem("", ""));
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(doc.Tables[0].Rows[i][0].ToString(), doc.Tables[0].Rows[i][0].ToString()));
                }
            }
        }

        /// <summary>
        /// 检测概算明细参考表是否有该项目的信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static bool CheckRefEstimate(string ProjGuid)
        {
            int count = IMCBaseInfo.CheckRefEstimate(ProjGuid);
            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检测概算明细是否有该项目的信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static bool CheckEstimate(string ProjGuid)
        {
            int count = IMCBaseInfo.CheckEstimate(ProjGuid);
            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取支付条件
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="InvestCompleteGuid"></param>
        /// <param name="DataOpt"></param>
        /// <returns></returns>
        public static string GetContractPayDataSet(string ProjGuid, string InvestCompleteGuid, string DataOpt)
        {
            string html = "";
            DataSet doc = IMCBaseInfo.GetContractPayDataSet(ProjGuid, InvestCompleteGuid);
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                if (DataOpt == "View")
                {
                    for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                    {
                        html += "<tr id='PaymentRow'>";
                        html += "<td node='PaymentNum' class='TdContent' style='width:9%;text-align:left'>" + doc.Tables[0].Rows[i]["PaymentNum"] + "</td>";
                        html += "<td node='PaymentName' class='TdContent' style='width:22%;text-align:left'>" + doc.Tables[0].Rows[i]["PaymentName"] + "</td>";
                        html += "<td node='PaymentCondition' class='TdContent' style='width:22%;text-align:left'>" + doc.Tables[0].Rows[i]["PaymentCondition"] + "</td>";
                        html += "<td node='PayTimeControl' class='TdContent' style='width:18%;text-align:left'>" + doc.Tables[0].Rows[i]["PayTimeControl"] + "</td>";
                        html += "<td node='PayProperty' class='TdContent' style='width:10%;text-align:left'>" + doc.Tables[0].Rows[i]["PayProperty"] + "</td>";
                        html += "<td node='PaymentScale' class='TdContent' style='width:9%;text-align:left'>" + doc.Tables[0].Rows[i]["PaymentScale"] + "</td>";
                        html += "<td node='PayementCost' class='TdContent' style='width:10%;text-align:left''>" + doc.Tables[0].Rows[i]["PayementCost"] + "</td>";
                        html += "<td node='ContractPayTypeGuid' class='TdContent' style='width:0;display:none'>" + doc.Tables[0].Rows[i]["ContractPayTypeGuid"] + "</td>";
                        html += "</tr>";
                    }
                }
                else
                {
                    for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                    {
                        html += "<tr id='PaymentRow'>";
                        html += "<td node='PaymentNum' class='TdContent' style='width:9%;text-align:left'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["PaymentNum"] + "' /></td>";
                        html += "<td node='PaymentName' class='TdContent' style='width:22%;text-align:left'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["PaymentName"] + "' /></td>";
                        html += "<td node='PaymentCondition' class='TdContent' style='width:22%;text-align:left'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["PaymentCondition"] + "' /></td> ";
                        html += "<td node='PayTimeControl' class='TdContent' style='width:18%;text-align:left'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["PayTimeControl"] + "' /></td>";
                        html += "<td node='PayProperty' class='TdContent' style='width:10%;text-align:left'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["PayProperty"] + "' /></td>";
                        html += "<td node='PaymentScale' class='TdContent' style='width:9%;text-align:left'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["PaymentScale"] + "' /></td>";
                        html += "<td node='PayementCost' class='TdContent' style='width:10%;text-align:left''>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["PayementCost"] + "' /></td>";
                        html += "<td node='ContractPayTypeGuid' class='TdContent' style='width:0;display:none'>";
                        html += "<input type='hidden' value='" + doc.Tables[0].Rows[i]["ContractPayTypeGuid"] + "' /></td>";
                        html += "<td node='ContractGuid' class='TdContent' style='width:0;display:none'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["ContractGuid"] + "' /></td>";
                        html += "<td node='ProjGuid' class='TdContent' style='width:0;display:none'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["ProjGuid"] + "' /></td>";
                        html += "<td node='InvestCompleteGuid' class='TdContent' style='width:0;display:none'>";
                        html += "<input type='text' value='" + doc.Tables[0].Rows[i]["InvestCompleteGuid"] + "' /></td>";
                        html += "</tr>";
                    }
                }

            }
            return html;
        }

        /// <summary>
        /// 根据项目主键删除明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="Table"></param>
        public static void DelDetailByProjGuid(string ProjGuid, string Table)
        {
            IMCBaseInfo.DelDetailByProjGuid(ProjGuid, Table);
        }

        /// <summary>
        /// 根据项目主键删除概算明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        public static void DelEstimateDetailByProjGuid(string ProjGuid)
        {
            IMCBaseInfo.DelEstimateDetailByProjGuid(ProjGuid);
        }

        /// <summary>
        /// 根据Code删除估算明细
        /// </summary>
        /// <param name="Code"></param>
        public static int DelReckonDetailDataByCode(string Code)
        {
            return IMCBaseInfo.DelReckonDetailDataByCode(Code);
        }

        /// <summary>
        /// 根据Code删除概算明细
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int DelEstimateDetailDataByCode(string guid)
        {
            return IMCBaseInfo.DelEstimateDetailDataByCode(guid);
        }

        /// <summary>
        /// 根据Code删除预算明细
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int DelBudgetDetailDataByCode(string Code)
        {
            return IMCBaseInfo.DelBudgetDetailDataByCode(Code);
        }

        /// <summary>
        /// 根据Code删除竣工决算概算明细
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int DelEndmateDetailDataByCode(string Code)
        {
            return IMCBaseInfo.DelEndmateDetailDataByCode(Code);
        }

        /// <summary>
        /// 根据Code修改估算明细
        /// </summary>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="CostType"></param>
        /// <param name="Remark"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int UpdateReckonDetail(string ProjOrCostName, string InvestAccount, string CostType, string Remark, string Code)
        {
            return IMCBaseInfo.UpdateReckonDetail(ProjOrCostName, InvestAccount, CostType, Remark, Code);
        }

        /// <summary>
        /// 新建估算明细
        /// </summary>
        /// <param name="TopGuid"></param>
        /// <param name="ProjGuid"></param>
        /// <param name="ReckonGuid"></param>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="Remark"></param>
        /// <param name="CostType"></param>
        /// <returns></returns>
        public static int InsertReckonDetail(string TopGuid, string ProjGuid, string ReckonGuid, string ProjOrCostName, string InvestAccount, string Remark, string CostType)
        {
            return IMCBaseInfo.InsertReckonDetail(TopGuid, ProjGuid, ReckonGuid, ProjOrCostName, InvestAccount, Remark, CostType);
        }

        /// <summary>
        /// 新建概算明细
        /// </summary>
        /// <param name="TopGuid"></param>
        /// <param name="ProjGuid"></param>
        /// <param name="EstimateGuid"></param>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="Quantity"></param>
        /// <param name="Unit"></param>
        /// <param name="InvesAmount"></param>
        /// <param name="Remark"></param>
        /// <param name="CostType"></param>
        /// <returns></returns>
        public static int InsertEstimateDetail(string TopGuid, string ProjGuid, string EstimateGuid, string ProjOrCostName, string InvestAccount, string Quantity, string Unit, string InvesAmount, string Remark, string CostType)
        {
            return IMCBaseInfo.InsertEstimateDetail(TopGuid, ProjGuid, EstimateGuid, ProjOrCostName, InvestAccount, Quantity, Unit, InvesAmount, Remark, CostType);
        }




        /// <summary>
        /// //根据Code修改概算明细
        /// </summary>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="Quantity"></param>
        /// <param name="Unit"></param>
        /// <param name="InvesAmount"></param>
        /// <param name="CostType"></param>
        /// <param name="Remark"></param>
        /// <param name="Code"></param>
        /// <param name="Guid"></param>
        /// <returns></returns>
        public static int UpdateEstimateDetail(string ProjOrCostName, string InvestAccount, string Quantity, string Unit, string InvesAmount, string CostType, string Remark, string Code, string Guid)
        {
            return IMCBaseInfo.UpdateEstimateDetail(ProjOrCostName, InvestAccount, Quantity, Unit, InvesAmount, CostType, Remark, Code, Guid);
        }

        /// <summary>
        /// 新建预算明细
        /// </summary>
        /// <param name="TopGuid"></param>
        /// <param name="ProjGuid"></param>
        /// <param name="BudgetGuid"></param>
        /// <param name="ProjOrCostName"></param>
        /// <param name="SubmitAccount"></param>
        /// <param name="AuditingAccount"></param>
        /// <param name="AuthorizeAccount"></param>
        /// <param name="TrialReductionAccount"></param>
        /// <returns></returns>
        public static int InsertBudgetDetail(string TopGuid, string ProjGuid, string BudgetGuid, string ProjOrCostName, string SubmitAccount, string AuditingAccount, string AuthorizeAccount, string TrialReductionAccount)
        {
            return IMCBaseInfo.InsertBudgetDetail(TopGuid, ProjGuid, BudgetGuid, ProjOrCostName, SubmitAccount, AuditingAccount, AuthorizeAccount, TrialReductionAccount);
        }

        /// <summary>
        /// 根据Code修改预算明细
        /// </summary>
        /// <param name="ProjOrCostName"></param>
        /// <param name="SubmitAccount"></param>
        /// <param name="AuditingAccount"></param>
        /// <param name="AuthorizeAccount"></param>
        /// <param name="TrialReductionAccount"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int UpdateBudgetDetail(string ProjOrCostName, string SubmitAccount, string AuditingAccount, string AuthorizeAccount, string TrialReductionAccount, string Code)
        {
            return IMCBaseInfo.UpdateBudgetDetail(ProjOrCostName, SubmitAccount, AuditingAccount, AuthorizeAccount, TrialReductionAccount, Code);
        }

        /// <summary>
        /// 新建竣工决算概算明细
        /// </summary>
        /// <param name="TopGuid"></param>
        /// <param name="ProjGuid"></param>
        /// <param name="EstimateGuid"></param>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="Quantity"></param>
        /// <param name="Unit"></param>
        /// <param name="InvesAmount"></param>
        /// <param name="Remark"></param>
        /// <param name="CostType"></param>
        /// <returns></returns>
        public static int InsertEndmateDetail(string TopGuid, string ProjGuid, string EstimateGuid, string ProjOrCostName, string InvestAccount, string Quantity, string Unit, string InvesAmount, string Remark, string CostType)
        {
            return IMCBaseInfo.InsertEndmateDetail(TopGuid, ProjGuid, EstimateGuid, ProjOrCostName, InvestAccount, Quantity, Unit, InvesAmount, Remark, CostType);
        }

        /// <summary>
        /// 根据Code修改竣工决算概算明细
        /// </summary>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="Quantity"></param>
        /// <param name="Unit"></param>
        /// <param name="InvesAmount"></param>
        /// <param name="CostType"></param>
        /// <param name="Remark"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int UpdateEndmateDetail(string ProjOrCostName, string InvestAccount, string Quantity, string Unit, string InvesAmount, string CostType, string Remark, string Code)
        {
            return IMCBaseInfo.UpdateEndmateDetail(ProjOrCostName, InvestAccount, Quantity, Unit, InvesAmount, CostType, Remark, Code);
        }

        /// <summary>
        /// 根据概算信息自动生成施工计划
        /// </summary>
        /// <param name="ProjGuid"></param>
        public static void CstrctPlanByEstimateDetail(string ProjGuid)
        {
            //DataSet DataEstimate = IMCBaseInfo.GetAllEstimateDetailByProjGuid(ProjGuid);
            DataTable EstimateDt = GetEstimateDetail(ProjGuid);
            DataSet CstrctPlan = IMCBaseInfo.GetCstrctPlanByProjGuid(ProjGuid);

            if (EstimateDt != null && EstimateDt.Rows.Count > 0)
            {
                for (int i = 0; i < EstimateDt.Rows.Count; i++)
                {
                    DataRow Row = EstimateDt.Rows[i];
                    string FundDetailGuid = Row["Guid"].ToString();//概算明细主键
                    string FundDetailName = Row["ProjOrCostName"].ToString(); ;//概算明细名称
                    DataRow[] row = CstrctPlan.Tables[0].Select("FundDetailGuid='" + FundDetailGuid + "'");
                    if (row.Length == 0)
                    {
                        IMCBaseInfo.CstrctPlanByEstimateDetail(ProjGuid, FundDetailGuid, FundDetailName, Row["Code"].ToString(), Row["OrderNum"].ToString(), Row["Code"].ToString(), FundDetailName, FundDetailName);
                    }
                }
            }
        }

        /// <summary>
        /// 获取项目概算
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataTable GetEstimateDetail(string ProjGuid)
        {
            DataSet DataEstimate = IMCBaseInfo.GetAllEstimateDetailByProjGuid(ProjGuid);

            DataTable dt = new DataTable();
            dt.Columns.Add("Guid");
            dt.Columns.Add("ProjOrCostName");
            dt.Columns.Add("Code");
            dt.Columns.Add("OrderNum");
            if (DataEstimate.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in DataEstimate.Tables[0].Select(" TopGuid is null  or TopGuid=''", "Code asc"))
                {
                    i++;
                    if (i < 3)//概算前两项
                    {
                        //判断主概算下面有没有子概算
                        if (DataEstimate.Tables[0].Select("topGuid = '" + dr["Guid"] + "'").Length > 0)
                        {
                            DataRow Newdr = dt.NewRow();
                            Newdr[0] = dr["Guid"];
                            Newdr[1] = dr["ProjOrCostName"];
                            Newdr[2] = dr["Code"];
                            Newdr[3] = dr["OrderNum"];
                            dt.Rows.Add(Newdr);

                            setData(dr, DataEstimate, dt);
                        }
                    }
                }
            }
            return dt;
        }


        /// <summary>
        /// 递归概算明细
        /// </summary>
        /// <param name="topDr"></param>
        /// <param name="ds"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable setData(DataRow topDr, DataSet ds, DataTable dt)
        {
            foreach (DataRow dr in ds.Tables[0].Select(" topGuid = '" + topDr["Guid"] + "'"))
            {
                DataRow Newdr = dt.NewRow();
                Newdr[0] = dr["Guid"];
                Newdr[1] = dr["ProjOrCostName"];
                Newdr[2] = dr["Code"];
                Newdr[3] = dr["OrderNum"];
                dt.Rows.Add(Newdr);

                if (ds.Tables[0].Select("topGuid = '" + dr["Guid"] + "'").Length > 0)
                {
                    setData(dr, ds, dt);
                }
            }
            return dt;
        }


        /// <summary>
        /// 获取概算事项的完整名称
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string GetName(DataSet doc, DataRow dr)
        {
            string name = "";
            if (dr["TopGuid"].ToString() == "")
            {
                name = dr["ProjOrCostName"].ToString();
            }
            else
            {
                DataRow drtop = doc.Tables[0].Select("Guid='" + dr["TopGuid"] + "'")[0];
                name = GetName(doc, drtop) + ">>" + dr["ProjOrCostName"].ToString();
            }
            return name;
        }


        /// <summary>
        /// 根据计划信息自动生成施工进度
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ConstructYear"></param>
        /// <param name="ConstructMonth"></param>
        public static void CstrctScheduleByPlan(string ProjGuid, string ConstructYear, string ConstructMonth)
        {
            DataSet DataPlan = IMCBaseInfo.GetCstrctPlanByProjGuid(ProjGuid);
            DataSet DataSchedule = IMCBaseInfo.GetCstrctScheduleByProjGuid(ProjGuid);
            if (DataPlan != null && DataPlan.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DataPlan.Tables[0].Rows.Count; i++)
                {
                    string planguid = DataPlan.Tables[0].Rows[i]["Guid"].ToString();
                    if (planguid != "")
                    {
                        DataRow[] rows = DataSchedule.Tables[0].Select("ItemsPlanGuid='" + planguid + "' and ConstructMonth='" + ConstructMonth + "'");
                        if (rows.Length == 0)
                        {
                            IMCBaseInfo.CstrctScheduleByPlan(ProjGuid, planguid, ConstructYear, ConstructMonth);//planguid 概算明细主键
                        }
                    }
                }
            }
        }
    }
}
