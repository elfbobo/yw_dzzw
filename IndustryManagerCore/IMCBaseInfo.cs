using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.IndustryManagerCore
{
    /// <summary>
    /// 初始化信息
    /// </summary>
    public class IMCBaseInfo
    {
        static Database db = DatabaseFactory.CreateDatabase();
        /// <summary>
        /// CheckNewMileStone
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static object CheckNewMileStone(string ProjGuid)
        {
            string sql = "select count(*) from Busi_Milestone where ProjGuid=@ProjGuid and SysStatus<>-1 and Mark in (100,200)";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 获取所有日常检查附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDailyCheckUp()
        {
            string sql = "select *,Year(Date) as Y from Idty_DailyCheckUp where SysStatus<>-1";
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取所有检查附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetCheckUp()
        {
            string sql = "select *,Year(Date) as Y from Idty_CheckUp where SysStatus<>-1";
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取所有资金支付附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetIvstPaymentUp()
        {
            string sql = "select * from Idty_IvstPaymentUp where SysStatus<>-1";
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取所有支出预算下达附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDeptFilesUp()
        {
            string sql = "select *,Year([Date]) as Y from Idty_DeptFilesUp where SysStatus<>-1";
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取所有审计通知书附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAuditNoticeUp()
        {
            string sql = "select *,Year(IssuedDate) as Y from Idty_AuditNoticeUp where SysStatus<>-1";
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取所有审计报告附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAuditReporUp()
        {
            string sql = "select *,Year(ReportDate) as Y from Idty_AuditReporUp where SysStatus<>-1";
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取所有招标投标 、计划管理 、会议纪要 附件信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetFileUp()
        {
            string sql = "select *,Year([Date]) as Y from Idty_FileUp where SysStatus<>-1";
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取该项目下年度计划下达计划金额
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static object GetInvestPlanAccount(string ProjGuid)
        {
            string sql = "select ISNULL(sum(PlanAccount),0) from Busi_InvestPlan where ProjGuid=@ProjGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 获取该项目下年度计划安排安排金额
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static object GetFundArrangeAccount(string ProjGuid)
        {
            string sql = "select ISNULL(sum(ArrangeAccount),0) from Busi_FundArrange where ProjGuid=@ProjGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 获取所有部门名称
        /// </summary>
        /// <returns></returns>
        public static DataSet GetMapUnitName()
        {
            string sql = "SELECT Guid,Name FROM sys_mapping WHERE directoryguid='04EB017F-A1FC-4FE3-B61C-300655CBE53E'";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 获取行业监管责任系统下的部门
        /// </summary>
        /// <returns></returns>
        public static DataSet GetSupUnitName()
        {
            string sql = "SELECT DepartmentName,UnitGuid FROM Busi_IndustrySupervisionDepartment";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            return db.ExecuteDataSet(cmd);
        }


        /// <summary>
        /// 获取该项目下的所有手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetAllProcedureConfig(string ProjGuid)
        {
            string sql = "select * from V_Procedure_Config where ProjGuid=@ProjGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 获取该项目下的所有子手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetAllProcedureChildConfig(string ProjGuid)
        {
            string sql = "select * from V_ProcedureChild_Config where ProjGuid=@ProjGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteDataSet(cmd);
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
            string sql = "insert into Busi_ProjPrep(ProgressSituation,Guid,ProjGuid,ApprovalGuid,ApprovalChildType,DateOfCompletion,ActualCompletionDate,ActualDuration,ApplyForStatus,Problems,Solutions,SolveResults,UndoneReason,Status,SysStatus,CreateDate)";
            sql += " values(@ProgressSituation,@Guid,@ProjGuid,@ApprovalGuid,@ApprovalChildType,@DateOfCompletion,@ActualCompletionDate,@ActualDuration,@ApplyForStatus,@Problems,@Solutions,@SolveResults,@UndoneReason,0,0,@CreateDate)";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            if (ApprovalChildType != "")
                cmd.Parameters.Add(new SqlParameter("@ApprovalChildType", ApprovalChildType));
            else
                cmd.Parameters.Add(new SqlParameter("@ApprovalChildType", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@DateOfCompletion", DateOfCompletion));
            cmd.Parameters.Add(new SqlParameter("@ActualCompletionDate", ActualCompletionDate));
            cmd.Parameters.Add(new SqlParameter("@ActualDuration", ActualDuration));
            cmd.Parameters.Add(new SqlParameter("@ApplyForStatus", ApplyForStatus));
            cmd.Parameters.Add(new SqlParameter("@Problems", Problems));
            cmd.Parameters.Add(new SqlParameter("@Solutions", Solutions));
            cmd.Parameters.Add(new SqlParameter("@SolveResults", SolveResults));
            cmd.Parameters.Add(new SqlParameter("@UndoneReason", UndoneReason));
            cmd.Parameters.Add(new SqlParameter("@CreateDate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@ProgressSituation", ProgressSituation));
            return db.ExecuteNonQuery(cmd);
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
            string sql = "update Busi_ProjPrep set ProgressSituation=@ProgressSituation,DateOfCompletion=@DateOfCompletion,ActualCompletionDate=@ActualCompletionDate,ActualDuration=@ActualDuration,ApplyForStatus=@ApplyForStatus,Problems=@Problems,Solutions=@Solutions,SolveResults=@SolveResults,UndoneReason=@UndoneReason where Guid=@Guid";

            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", PrepGuid));
            cmd.Parameters.Add(new SqlParameter("@DateOfCompletion", DateOfCompletion));
            cmd.Parameters.Add(new SqlParameter("@ActualCompletionDate", ActualCompletionDate));
            cmd.Parameters.Add(new SqlParameter("@ActualDuration", ActualDuration));
            cmd.Parameters.Add(new SqlParameter("@ApplyForStatus", ApplyForStatus));
            cmd.Parameters.Add(new SqlParameter("@Problems", Problems));
            cmd.Parameters.Add(new SqlParameter("@Solutions", Solutions));
            cmd.Parameters.Add(new SqlParameter("@SolveResults", SolveResults));
            cmd.Parameters.Add(new SqlParameter("@UndoneReason", UndoneReason));
            cmd.Parameters.Add(new SqlParameter("@ProgressSituation", ProgressSituation));
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 获取所有审批进度
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAppProgress()
        {
            string sql = "select * from Busi_ProjPrep where SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            return db.ExecuteDataSet(cmd);
        }

       /// <summary>
        /// 获取该项目下的第一条建的进度月报
       /// </summary>
       /// <param name="ProjGuid"></param>
       /// <param name="ApprovalGuid"></param>
       /// <param name="ApprovalChildType"></param>
       /// <returns></returns>
        public static DataSet GetFirstAppProgress(string ProjGuid, string ApprovalGuid, string ApprovalChildType
)
        {
            string sql = "select top 1 Guid,DateOfCompletion FROM Busi_ProjPrep WHERE ProjGuid=@ProjGuid and ApprovalGuid=@ApprovalGuid " + (ApprovalChildType == "" ? " and ApprovalChildType is null" : " and ApprovalChildType=@ApprovalChildType") + " and SysStatus<>-1 ORDER BY CreateDate asc";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalChildType", ApprovalChildType));
            return db.ExecuteDataSet(cmd);
        }


        /// <summary>
        /// 根据主键获取审批进度信息
        /// </summary>
        /// <param name="PrepGuid"></param>
        /// <returns></returns>
        public static DataSet GetAppProgressByGuid(string PrepGuid)
        {
            string sql = "select * from Busi_ProjPrep where Guid=@PrepGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@PrepGuid", PrepGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据主键获取审批进度信息
        /// </summary>
        /// <param name="PrepGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static DataSet GetAppProgressByGuid(string PrepGuid, string ApprovalChildType)
        {
            string sql = "select * from Busi_ProjPrep where Guid=@PrepGuid and ApprovalChildType=@ApprovalChildType and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@PrepGuid", PrepGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalChildType", ApprovalChildType));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 生成进度（施工进度）
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ItemsPlanGuid"></param>
        /// <param name="ConstructYear"></param>
        /// <param name="ConstructMonth"></param>
        public static void CstrctScheduleByPlan(string ProjGuid, string ItemsPlanGuid, string ConstructYear, string ConstructMonth)
        {
            string sql = "insert into Busi_ConstructionSchedule(Guid,ProjGuid,ItemsPlanGuid,ConstructYear,ConstructMonth,Status,SysStatus,CreateDate，ApplyingDate) ";
            sql += " values(@Guid,@ProjGuid,@ItemsPlanGuid,@ConstructYear,@ConstructMonth,0,0,@CreateDate,@ApplyingDate)";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ItemsPlanGuid", ItemsPlanGuid));
            cmd.Parameters.Add(new SqlParameter("@ConstructYear", ConstructYear));
            cmd.Parameters.Add(new SqlParameter("@ConstructMonth", ConstructMonth));
            cmd.Parameters.Add(new SqlParameter("@CreateDate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@ApplyingDate", DateTime.Now));
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据项目主键获取所有的项目概算信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetAllEstimateDetailByProjGuid(string ProjGuid)
        {
            string sql = "select * from Busi_Con_EstimateDetail where ProjGuid=@ProjGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据项目主键获取所有的施工计划信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetCstrctPlanByProjGuid(string ProjGuid)
        {
            string sql = "select * from Busi_ConstructionPlan where ProjGuid=@ProjGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据项目主键获取所有的施工进度信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetCstrctScheduleByProjGuid(string ProjGuid)
        {
            string sql = "select * from Busi_ConstructionSchedule where ProjGuid=@ProjGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteDataSet(cmd);
        }


        /// <summary>
        /// 生成计划（施工计划）
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="FundDetailGuid"></param>
        /// <param name="FundDetailName"></param>
        /// <param name="EstimateCode"></param>
        /// <param name="EstimateOrderNum"></param>
        /// <param name="TaskNum"></param>
        /// <param name="TaskName"></param>
        /// <param name="Desp"></param>
        public static void CstrctPlanByEstimateDetail(string ProjGuid, string FundDetailGuid, string FundDetailName, string EstimateCode, string EstimateOrderNum, string TaskNum, string TaskName, string Desp)
        {
            string sql = "insert into Busi_ConstructionPlan(Guid,ProjGuid,FundDetailGuid,FundDetailName,Status,SysStatus,FunSource,EstimateCode,EstimateOrderNum,TaskNum,TaskName,Desp,CreateDate) ";
            sql += " values(@Guid,@ProjGuid,@FundDetailGuid,@FundDetailName,0,0,1,@EstimateCode,@EstimateOrderNum,@TaskNum,@TaskName,@Desp,@CreateDate)";

            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@FundDetailGuid", FundDetailGuid));
            cmd.Parameters.Add(new SqlParameter("@FundDetailName", FundDetailName));
            cmd.Parameters.Add(new SqlParameter("@EstimateCode", EstimateCode));
            cmd.Parameters.Add(new SqlParameter("@EstimateOrderNum", EstimateOrderNum));
            cmd.Parameters.Add(new SqlParameter("@TaskNum", TaskNum));
            cmd.Parameters.Add(new SqlParameter("@TaskName", TaskName));
            cmd.Parameters.Add(new SqlParameter("@Desp", Desp));
            cmd.Parameters.Add(new SqlParameter("@CreateDate", DateTime.Now));
            db.ExecuteNonQuery(cmd);

        }


        /// <summary>
        /// 读取变更申请信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static DataSet GetChangeRequest(string ProjGuid, string ApprovalGuid)
        {
            string sql = "select ApplyReason,PlanStartDate,PlanEndDate from Busi_ChangeRequest where ProjGuid=@ProjGuid and ApprovalGuid=@ApprovalGuid and SysStatus<>-1 and (ApprovalChildType is NULL or ApprovalChildType='')";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            return db.ExecuteDataSet(cmd);
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
            string sql = "select ApplyReason,PlanStartDate,PlanEndDate from Busi_ChangeRequest where ProjGuid=@ProjGuid and ApprovalGuid=@ApprovalGuid and ApprovalChildType=@ApprovalChildType and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalChildType", ApprovalChildType));
            return db.ExecuteDataSet(cmd);
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
        public static int AddChangeRequest(string ProjGuid, string ApprovalGuid, string ApprovalChildType, string ApplyReason, string ApplicantGuid, string ApplicantName, string PlanStartDate, string PlanEndDate)
        {
            string sql = "insert into Busi_ChangeRequest(Guid,ProjGuid,ApprovalGuid,ApprovalChildType,ApplyReason,ApplicantGuid,ApplicantName,ApplyDate,PlanStartDate,PlanEndDate,CreateDate,Status,SysStatus) values";
            sql += " (@Guid,@ProjGuid,@ApprovalGuid,@ApprovalChildType,@ApplyReason,@ApplicantGuid,@ApplicantName,@ApplyDate,@PlanStartDate,@PlanEndDate,@CreateDate,0,0)";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            if (ApprovalChildType != "")
                cmd.Parameters.Add(new SqlParameter("@ApprovalChildType", ApprovalChildType));
            else
                cmd.Parameters.Add(new SqlParameter("@ApprovalChildType", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@ApplyReason", ApplyReason));
            cmd.Parameters.Add(new SqlParameter("@ApplicantGuid", ApplicantGuid));
            cmd.Parameters.Add(new SqlParameter("@ApplicantName", ApplicantName));
            cmd.Parameters.Add(new SqlParameter("@ApplyDate", DateTime.Now));
            if (PlanStartDate != "")
                cmd.Parameters.Add(new SqlParameter("@PlanStartDate", PlanStartDate));
            else
                cmd.Parameters.Add(new SqlParameter("@PlanStartDate", DBNull.Value));

            if (PlanEndDate != "")
                cmd.Parameters.Add(new SqlParameter("@PlanEndDate", PlanEndDate));
            else
                cmd.Parameters.Add(new SqlParameter("@PlanEndDate", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@CreateDate", DateTime.Now));
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 环节是否已经有变更申请信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static object CheckChangeRequest(string ProjGuid, string ApprovalGuid)
        {
            string sql = "select count(*) from Busi_ChangeRequest where ProjGuid=@ProjGuid and ApprovalGuid=@ApprovalGuid and SysStatus<>-1 and (ApprovalChildType is NULL or ApprovalChildType='')";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 环节是否已经有变更申请信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <param name="ApprovalChildType"></param>
        /// <returns></returns>
        public static object CheckChangeRequest(string ProjGuid, string ApprovalGuid, string ApprovalChildType)
        {
            string sql = "select count(*) from Busi_ChangeRequest where ProjGuid=@ProjGuid and ApprovalGuid=@ApprovalGuid and ApprovalChildType=@ApprovalChildType and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalChildType", ApprovalChildType));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 环节手续是否在建设过程中
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static object CheckCstrctProcessByGuid(string ProjGuid, string ApprovalGuid)
        {
            string sql = "select count(*) from Busi_CstrctProcess where ProjGuid=@ProjGuid and ApprovalGuid=@ApprovalGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 环节手续是否在证照批复中
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static object CheckApproveByGuid(string ProjGuid, string ApprovalGuid)
        {
            string sql = "select count(*) from Busi_LicenseReply where ProjGuid=@ProjGuid and ApprovalGuid=@ApprovalGuid and SysStatus<>-1";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ApprovalGuid", ApprovalGuid));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 环节手续是否在项目建议书、项目可行性研究报告、项目初步设计及概算、项目预算中
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static object CheckApproveByProjGuid(string Table, string ProjGuid)
        {
            string sql = "";
            switch (Table)
            {
                case "Busi_ProjProposal":
                    sql = "select count(*) from Busi_ProjProposal where ProjGuid=@ProjGuid and SysStatus<>-1";
                    break;
                case "Busi_ProjFsbtyStudy":
                    sql = "select count(*) from Busi_ProjFsbtyStudy where ProjGuid=@ProjGuid and SysStatus<>-1";
                    break;
                case "Busi_ProjInitialDesign":
                    sql = "select count(*) from Busi_ProjInitialDesign where ProjGuid=@ProjGuid and SysStatus<>-1";
                    break;
                case "Busi_ProjButget":
                    sql = "select count(*) from Busi_ProjButget where ProjGuid=@ProjGuid and SysStatus<>-1";
                    break;
            }
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 返回子手续计划
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="ApprovalGuid">主手续主键</param>
        /// <returns>返回子手续计划</returns>
        public static DataTable GetApproveChildStatus(string ProjGuid, string ApprovalGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select * from Busi_ProjProcedureChild where MApprovalGuid='{0}' and ProjGuid='{1}'", ApprovalGuid, ProjGuid)).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取所有子手续数据
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="ApprovalGuid">主手续主键</param>
        /// <returns>所有子手续数据集</returns>
        public static DataSet GetApproveChildDatas(string ProjGuid, string ApprovalGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from Busi_CstrctProcessCompile where ApprovalGuid='{0}' and ProjGuid='{1}';select * from Busi_ExpertReviewMain where ApprovalGuid='{0}' and ProjGuid='{1}';select * from Busi_CstrctProcessApply where ApprovalGuid='{0}' and ProjGuid='{1}';select * from Busi_CstrctProcessRevise where ApprovalGuid='{0}' and ProjGuid='{1}';", ApprovalGuid, ProjGuid));
            return ds;
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
            string sql = "update Busi_ConstructionPlan set BegDate=@BegDate,CompleteDate=@CompleteDate,BudgetAmount=@BudgetAmount,";
            sql += "ActualTaskLimit=@ActualTaskLimit,IsComplete=@IsComplete where [Guid]=@PlanGuid";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@BegDate", BegDate));
            if (CompleteDate == "")
                cmd.Parameters.Add(new SqlParameter("@CompleteDate", DBNull.Value));
            else
                cmd.Parameters.Add(new SqlParameter("@CompleteDate", CompleteDate));
            cmd.Parameters.Add(new SqlParameter("@BudgetAmount", BudgetAmount));
            cmd.Parameters.Add(new SqlParameter("@ActualTaskLimit", ActualTaskLimit));
            cmd.Parameters.Add(new SqlParameter("@IsComplete", IsComplete));
            cmd.Parameters.Add(new SqlParameter("@PlanGuid", PlanGuid));
            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 根据主键获取施工计划信息
        /// </summary>
        /// <param name="PlanGuid"></param>
        /// <returns></returns>
        public static DataSet GetCstrctPlanInfoByGuid(string PlanGuid)
        {
            string sql = "select * from Busi_ConstructionPlan where Guid=@Guid";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", PlanGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 获取施工计划的所有前置计划序号
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="Guid"></param>
        /// <returns></returns>
        public static DataSet GetCstrctPlanPreNum(string ProjGuid, string Guid)
        {
            string sql = "select TaskNum from Busi_ConstructionPlan where ProjGuid=@ProjGuid and Guid<>@Guid group by TaskNum";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 检测概算明细参考表是否有该项目的信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static int CheckRefEstimate(string ProjGuid)
        {
            string sql = "select count(*) from tb_temp_KJFF where projcode=@ProjGuid";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return int.Parse(db.ExecuteScalar(cmd).ToString());
        }

        /// <summary>
        /// 检测概算明细是否有该项目的信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static int CheckEstimate(string ProjGuid)
        {
            string sql = "select count(*) from Busi_Con_EstimateDetail where projguid=@ProjGuid";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return int.Parse(db.ExecuteScalar(cmd).ToString());
        }


        /// <summary>
        /// 获取形象进度
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="InvestCompleteGuid"></param>
        /// <returns></returns>
        public static DataSet GetContractPayDataSet(string ProjGuid, string InvestCompleteGuid)
        {
            string sql = "SELECT pl.ContractPayTypeGuid,pl.InvestCompleteGuid,pl.ProjGuid,pl.ContractGuid,p.PaymentNum,p.PaymentName,p.PaymentCondition,p.PayTimeControl,PayProperty=(SELECT NAME FROM Sys_Mapping WHERE guid=p.PayProperty),p.PaymentScale,p.PayementCost FROM Busi_ContractPayType p,Busi_ContractPayList pl WHERE p.ProjGuid=pl.ProjGuid AND p.ContractGuid=pl.ContractGuid AND pl.InvestCompleteGuid=@InvestCompleteGuid AND pl.ProjGuid=@ProjGuid AND  p.Guid=pl.ContractPayTypeGuid and p.SysStatus<>-1 AND pl.SysStatus<>-1 order by p.PaymentNum asc";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@InvestCompleteGuid", InvestCompleteGuid));
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 获取项目的建设单位名称
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetProjCstrctUnitName(string ProjGuid)
        {
            string value = "";

            DbCommand cmd = db.CreateCommand(CommandType.Text, "select ConstructionUnitName from Busi_ProjRegister where Guid=@KeyValue");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", ProjGuid));
            object obj = db.ExecuteScalar(cmd);
            if (obj != null && obj.ToString() != "")
                value = obj.ToString();
            return value;
        }

        /// <summary>
        /// 获取项目名称
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static string GetProjNameByGuid(string ProjGuid)
        {
            string value = "";
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select ProjName from Busi_ProjRegister where Guid=@KeyValue");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", ProjGuid));
            object obj = db.ExecuteScalar(cmd);
            if (obj != null && obj.ToString() != "")
                value = obj.ToString();
            return value;
        }

        /// <summary>
        /// 获取建设过程名称
        /// </summary>
        /// <param name="AppGuid">建设过程主键</param>
        /// <returns></returns>
        public static DataSet GetAppConfigInfoByGuid(string AppGuid)
        {
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select * from Busi_ApprovalConfig where Guid=@KeyValue and SysStatus<>-1");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", AppGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 检查该建设过程是否附带专家评审
        /// </summary>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static bool CheckIfContainExpertReview(string ApprovalGuid)
        {
            bool result = false;
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select Name from Busi_ApprovalConfig where Guid=@KeyValue and SysStatus<>-1");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", ApprovalGuid));
            object obj = db.ExecuteScalar(cmd);
            if (obj != null && obj.ToString() != "")
            {
                string AppName = obj.ToString();
                switch (AppName)
                {
                    case "项目建议书":
                    case "项目可行性研究报告":
                    case "项目初步设计及概算":
                    case "项目施工图设计":
                    case "项目预算":
                    case "项目核准申请报告":
                        result = true;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取对应手续的批复基本信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="ApprovalGuid">手续主键</param>
        /// <returns>批复基本信息DataTable</returns>
        public static DataTable GetCstrctProcessReply(string ProjGuid, string ApprovalGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select * from V_CstrctProcess_Reply where ProjGuid='{0}' and charindex('{1}',ApprovalGuid)>0", ProjGuid, ApprovalGuid)).Tables[0];
            return dt;
        }

        /// <summary>
        /// 通过项目主键获取项目信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static DataSet GetProjInfoByGuid(string ProjGuid)
        {
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select * from Busi_ProjRegister where Guid=@KeyValue and SysStatus<>-1");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", ProjGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据字典表的主键获取Name
        /// </summary>
        /// <param name="MapGuid">字典表主键</param>
        /// <returns></returns>
        public static string GetMapNameByGuid(string MapGuid)
        {
            string value = "";
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select Name from Sys_Mapping where Guid=@KeyValue");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", MapGuid));
            object obj = db.ExecuteScalar(cmd);
            if (obj != null && obj.ToString() != "")
                value = obj.ToString();
            return value;
        }

        /// <summary>
        /// 检查项目是否已经存在项目里程碑信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static int CheckProjMileStone(string ProjGuid)
        {
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select count(*) from Busi_Milestone where ProjGuid=@KeyValue");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", ProjGuid));
            return int.Parse(db.ExecuteScalar(cmd).ToString());
        }

        /// <summary>
        /// 通过DirectoryGuid获取一类型的字典信息
        /// </summary>
        /// <param name="DirectoryGuid"></param>
        /// <param name="OtherSql">其它条件</param>
        /// <returns></returns>
        public static DataSet GetMapGuidsByDirectoryGuid(string DirectoryGuid, string OtherSql)
        {
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select Guid,Mark from Sys_Mapping where DirectoryGuid=@KeyValue " + OtherSql + " Order By Mark asc");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", DirectoryGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 添加项目里程碑信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="MapGuid">字典表的主键</param>
        /// <param name="Mark">排序</param>
        public static void InitialProjMileStone(string ProjGuid, string MapGuid, string Mark)
        {
            DataSet doc = db.ExecuteDataSet("select * from Busi_Milestone where 1=2");
            DataRow dr = doc.Tables[0].NewRow();
            dr["Guid"] = Guid.NewGuid().ToString();
            dr["ProjGuid"] = ProjGuid;
            dr["MapGuid"] = MapGuid;
            dr["HandleStatus"] = 0;
            dr["Mark"] = Mark;
            dr["CreateDate"] = DateTime.Now;
            dr["Status"] = 0;
            dr["SysStatus"] = 0;
            doc.Tables[0].Rows.Add(dr);
            doc.Tables[0].TableName = "Busi_Milestone";
            doc.Merge(doc);
            db.UpdateDataSet(doc);
        }

        /// <summary>
        /// 获取项目的里程碑信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjMileStone(string ProjGuid)
        {
            string sql = "select m.*,p.name as MapName,p.DirectoryGuid from Busi_Milestone m,Sys_Mapping p WHERE m.MapGuid=p.Guid and ProjGuid=@KeyValue AND m.SysStatus<>-1 and m.Mark not in (100,200) ORDER BY P.Mark ASC;";
            sql += "select m.*,p.name as MapName,p.DirectoryGuid from Busi_Milestone m,Sys_Mapping p WHERE m.MapGuid=p.Guid and ProjGuid=@KeyValue AND m.SysStatus<>-1 and p.DirectoryGuid='072C5AC3-F1EE-49B9-B7A2-D8EEF7A6B407' ORDER BY P.Mark ASC;";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@KeyValue", ProjGuid));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 设置项目里程碑的在办时间、办结时间
        /// </summary>
        /// <param name="MileStoneGuid">里程碑主键</param>
        /// <param name="Column">用于区分是在办时间还是办结时间</param>
        public static void InitialMileStoneDate(string MileStoneGuid, string Column)
        {
            string sql = "update Busi_Milestone set HandlingDate=@Date,HandleStatus=1 where Guid=@KeyValue";
            if (Column == "MakedDate")
                sql = "update Busi_Milestone set MakedDate=@Date,HandleStatus=2 where Guid=@KeyValue";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Date", DateTime.Now.ToString()));
            cmd.Parameters.Add(new SqlParameter("@KeyValue", MileStoneGuid));
            db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 设置项目里程碑的开工时间、竣工时间
        /// </summary>
        /// <param name="MileStoneGuid">里程碑主键</param>
        /// <param name="Column">用于区分是在办时间还是办结时间</param>
        /// <param name="WorkTime"></param>
        public static void InitialMileStoneDate(string MileStoneGuid, string Column, string WorkTime)
        {
            string sql = "update Busi_Milestone set HandlingDate=@Date,HandleStatus=1 where Guid=@KeyValue";
            if (Column == "MakedDate")
                sql = "update Busi_Milestone set MakedDate=@Date,HandleStatus=2 where Guid=@KeyValue";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Date", WorkTime));
            cmd.Parameters.Add(new SqlParameter("@KeyValue", MileStoneGuid));
            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 获取Busi_ApprovalConfig的所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAppConfigData()
        {
            return db.ExecuteDataSet("select Guid,Name,Type from Busi_ApprovalConfig where SysStatus<>-1 order by TypeCode,OrderCode");
        }


        /// <summary>
        /// 根据Busi_ApprovalConfig的主键获取Name
        /// </summary>
        /// <param name="AppGuid"></param>
        /// <returns></returns>
        public static string GetAppConfigNameByGuid(string AppGuid)
        {
            string value = "";
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select Name from Busi_ApprovalConfig where Guid=@KeyValue");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", AppGuid));
            object obj = db.ExecuteScalar(cmd);
            if (obj != null && obj.ToString() != "")
                value = obj.ToString();
            return value;
        }

        /// <summary>
        /// 通过项目主键获取项目的TopGuid
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static string GetProjTopGuid(string ProjGuid)
        {
            string value = "";
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select TopGuid from Busi_ProjRegister where Guid=@KeyValue");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", ProjGuid));
            object obj = db.ExecuteScalar(cmd);
            if (obj != null && obj.ToString() != "")
                value = obj.ToString();
            return value;
        }

        /// <summary>
        /// 通过项目主键获取项目的项目主从关系
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static string GetProjAffiliationByGuid(string ProjGuid)
        {
            string value = "";
            DbCommand cmd = db.CreateCommand(CommandType.Text, "select ProjAffiliation from Busi_ProjRegister where Guid=@KeyValue and SysStatus<>-1");
            cmd.Parameters.Add(new SqlParameter("@KeyValue", ProjGuid));
            object obj = db.ExecuteScalar(cmd);
            if (obj != null && obj.ToString() != "")
                value = obj.ToString();
            return value;
        }

        /// <summary>
        /// 根据项目主键删除明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="Table"></param>
        public static void DelDetailByProjGuid(string ProjGuid, string Table)
        {
            DbCommand cmd = db.CreateCommand(CommandType.Text, "update " + Table + " set SysStatus=-1 where ProjGuid=@ProjGuid");
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据项目主键删除概算明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        public static void DelEstimateDetailByProjGuid(string ProjGuid)
        {
            DbCommand cmd = db.CreateCommand(CommandType.Text, "delete from Busi_Con_EstimateDetail where ProjGuid=@ProjGuid");
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 根据Code删除估算明细
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int DelReckonDetailDataByCode(string Code)
        {
            string Sql = "Update Busi_Con_ReckonDetail set Sysstatus=-1 where Code=" + Code + "";//更新主节点
            int i = db.ExecuteNonQuery(Sql);
            if (i > 0)
            {
                int count = Convert.ToInt32(db.ExecuteScalar("select count(*) from Busi_Con_ReckonDetail where TopGuid=(select Guid from Busi_EarlyInvest where Code=" + Code + ")"));
                if (count > 0)
                {
                    string childrenSql = "Update Busi_Con_ReckonDetail set Sysstatus=-1 where TopGuid=(select Guid from Busi_Con_ReckonDetail where Code=" + Code + ")";
                    db.ExecuteNonQuery(childrenSql);
                }
            }
            return i;
        }

        /// <summary>
        /// 根据Code删除概算明细
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static int DelEstimateDetailDataByCode(string guid)
        {
            string Sql = "Update Busi_Con_EstimateDetail set Sysstatus=-1 where guid='" + guid + "'";//更新主节点
            int i = db.ExecuteNonQuery(Sql);
            if (i > 0)
            {
                int count = Convert.ToInt32(db.ExecuteScalar("select count(*) from Busi_Con_EstimateDetail where TopGuid='" + guid + "'"));
                //int count = Convert.ToInt32(db.ExecuteScalar("select count(*) from Busi_Con_EstimateDetail where TopGuid=(select Guid from Busi_EarlyInvest where Guid=" + guid + ")"));
                if (count > 0)
                {
                    string childrenSql = "Update Busi_Con_EstimateDetail set Sysstatus=-1 where TopGuid='" + guid + "'";
                    //string childrenSql = "Update Busi_Con_EstimateDetail set Sysstatus=-1 where TopGuid=(select Guid from Busi_Con_EstimateDetail where Guid=" + guid + ")";
                    db.ExecuteNonQuery(childrenSql);
                }
            }
            return i;
        }

        /// <summary>
        /// 根据Code删除预算明细
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int DelBudgetDetailDataByCode(string Code)
        {
            string Sql = "Update Busi_Con_BuggetDetails set Sysstatus=-1 where Code=" + Code + "";//更新主节点
            int i = db.ExecuteNonQuery(Sql);
            if (i > 0)
            {
                int count = Convert.ToInt32(db.ExecuteScalar("select count(*) from Busi_Con_BuggetDetails where TopGuid=(select Guid from Busi_EarlyInvest where Code=" + Code + ")"));
                if (count > 0)
                {
                    string childrenSql = "Update Busi_Con_BuggetDetails set Sysstatus=-1 where TopGuid=(select Guid from Busi_Con_BuggetDetails where Code=" + Code + ")";
                    db.ExecuteNonQuery(childrenSql);
                }
            }
            return i;
        }

        /// <summary>
        /// 根据Code删除竣工决算概算明细
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int DelEndmateDetailDataByCode(string Code)
        {
            string Sql = "Update Busi_Con_EndmateDetail set Sysstatus=-1 where Code=" + Code + "";//更新主节点
            int i = db.ExecuteNonQuery(Sql);
            if (i > 0)
            {
                int count = Convert.ToInt32(db.ExecuteScalar("select count(*) from Busi_Con_EndmateDetail where TopGuid=(select Guid from Busi_EarlyInvest where Code=" + Code + ")"));
                if (count > 0)
                {
                    string childrenSql = "Update Busi_Con_EndmateDetail set Sysstatus=-1 where TopGuid=(select Guid from Busi_Con_EndmateDetail where Code=" + Code + ")";
                    db.ExecuteNonQuery(childrenSql);
                }
            }
            return i;
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
            string sql = "update Busi_Con_ReckonDetail set ProjOrCostName=@ProjOrCostName,InvestAccount=@InvestAccount,CostType=@CostType,Remark=@Remark where Code=@Code";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjOrCostName", ProjOrCostName));
            cmd.Parameters.Add(new SqlParameter("@InvestAccount", InvestAccount));
            cmd.Parameters.Add(new SqlParameter("@CostType", CostType));
            cmd.Parameters.Add(new SqlParameter("@Remark", Remark));
            cmd.Parameters.Add(new SqlParameter("@Code", Code));
            return db.ExecuteNonQuery(cmd);
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
            string sql = "insert into Busi_Con_ReckonDetail(Guid,TopGuid,ProjGuid,ReckonGuid,ProjOrCostName,InvestAccount,Remark,CostType,status,SysStatus) ";
            sql += " values(@Guid,@TopGuid,@ProjGuid,@ReckonGuid,@ProjOrCostName,@InvestAccount,@Remark,@CostType,0,0)";

            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            if (TopGuid != "")
                cmd.Parameters.Add(new SqlParameter("@TopGuid", TopGuid));
            else
                cmd.Parameters.Add(new SqlParameter("@TopGuid", DBNull.Value));

            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@ReckonGuid", ReckonGuid));
            cmd.Parameters.Add(new SqlParameter("@ProjOrCostName", ProjOrCostName));
            cmd.Parameters.Add(new SqlParameter("@InvestAccount", InvestAccount));
            cmd.Parameters.Add(new SqlParameter("@Remark", Remark));
            cmd.Parameters.Add(new SqlParameter("@CostType", CostType));
            return db.ExecuteNonQuery(cmd);
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
            string sql = "insert into Busi_Con_EstimateDetail(Guid,TopGuid,ProjGuid,EstimateGuid,ProjOrCostName,InvestAccount,Quantity,Unit,InvesAmount,Remark,CostType,status,SysStatus) ";
            sql += " values(@Guid,@TopGuid,@ProjGuid,@EstimateGuid,@ProjOrCostName,@InvestAccount,@Quantity,@Unit,@InvesAmount,@Remark,@CostType,0,0)";

            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            if (TopGuid != "")
                cmd.Parameters.Add(new SqlParameter("@TopGuid", TopGuid));
            else
                cmd.Parameters.Add(new SqlParameter("@TopGuid", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@EstimateGuid", EstimateGuid));
            cmd.Parameters.Add(new SqlParameter("@ProjOrCostName", ProjOrCostName));
            cmd.Parameters.Add(new SqlParameter("@InvestAccount", InvestAccount));
            cmd.Parameters.Add(new SqlParameter("@Quantity", Quantity));
            cmd.Parameters.Add(new SqlParameter("@Unit", Unit));
            cmd.Parameters.Add(new SqlParameter("@InvesAmount", InvesAmount));
            cmd.Parameters.Add(new SqlParameter("@Remark", Remark));
            cmd.Parameters.Add(new SqlParameter("@CostType", CostType));
            return db.ExecuteNonQuery(cmd);
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
            string sql = "update Busi_Con_EstimateDetail set ProjOrCostName=@ProjOrCostName,InvestAccount=@InvestAccount,Quantity=@Quantity,Unit=@Unit,InvesAmount=@InvesAmount,CostType=@CostType,Remark=@Remark where Guid=@Guid";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjOrCostName", ProjOrCostName));
            cmd.Parameters.Add(new SqlParameter("@InvestAccount", InvestAccount));
            cmd.Parameters.Add(new SqlParameter("@Quantity", Quantity));
            cmd.Parameters.Add(new SqlParameter("@Unit", Unit));
            cmd.Parameters.Add(new SqlParameter("@InvesAmount", InvesAmount));
            cmd.Parameters.Add(new SqlParameter("@Remark", Remark));
            cmd.Parameters.Add(new SqlParameter("@CostType", CostType));
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid));
            int x = db.ExecuteNonQuery(cmd);
            return x;
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
            string sql = "insert into Busi_Con_BuggetDetails(Guid,TopGuid,ProjGuid,BuggetGuid,ProjOrCostName,SubmitAccount,AuditingAccount,AuthorizeAccount,TrialReductionAccount,status,SysStatus) ";
            sql += " values(@Guid,@TopGuid,@ProjGuid,@BuggetGuid,@ProjOrCostName,@SubmitAccount,@AuditingAccount,@AuthorizeAccount,@TrialReductionAccount,0,0)";

            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            if (TopGuid != "")
                cmd.Parameters.Add(new SqlParameter("@TopGuid", TopGuid));
            else
                cmd.Parameters.Add(new SqlParameter("@TopGuid", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@BuggetGuid", BudgetGuid));
            cmd.Parameters.Add(new SqlParameter("@ProjOrCostName", ProjOrCostName));
            cmd.Parameters.Add(new SqlParameter("@SubmitAccount", SubmitAccount));
            cmd.Parameters.Add(new SqlParameter("@AuditingAccount", AuditingAccount));
            cmd.Parameters.Add(new SqlParameter("@AuthorizeAccount", AuthorizeAccount));
            cmd.Parameters.Add(new SqlParameter("@TrialReductionAccount", TrialReductionAccount));
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 修改预算明细
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
            string sql = "update Busi_Con_BuggetDetails set ProjOrCostName=@ProjOrCostName,SubmitAccount=@SubmitAccount,AuditingAccount=@AuditingAccount,AuthorizeAccount=@AuthorizeAccount,TrialReductionAccount=@TrialReductionAccount where Code=@Code";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjOrCostName", ProjOrCostName));
            cmd.Parameters.Add(new SqlParameter("@SubmitAccount", SubmitAccount));
            cmd.Parameters.Add(new SqlParameter("@AuditingAccount", AuditingAccount));
            cmd.Parameters.Add(new SqlParameter("@AuthorizeAccount", AuthorizeAccount));
            cmd.Parameters.Add(new SqlParameter("@TrialReductionAccount", TrialReductionAccount));
            cmd.Parameters.Add(new SqlParameter("@Code", Code));
            return db.ExecuteNonQuery(cmd);
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
            string sql = "insert into Busi_Con_EndmateDetail(Guid,TopGuid,ProjGuid,EstimateGuid,ProjOrCostName,InvestAccount,Quantity,Unit,InvesAmount,Remark,CostType,status,SysStatus) ";
            sql += " values(@Guid,@TopGuid,@ProjGuid,@EstimateGuid,@ProjOrCostName,@InvestAccount,@Quantity,@Unit,@InvesAmount,@Remark,@CostType,0,0)";

            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString()));
            if (TopGuid != "")
                cmd.Parameters.Add(new SqlParameter("@TopGuid", TopGuid));
            else
                cmd.Parameters.Add(new SqlParameter("@TopGuid", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@ProjGuid", ProjGuid));
            cmd.Parameters.Add(new SqlParameter("@EstimateGuid", EstimateGuid));
            cmd.Parameters.Add(new SqlParameter("@ProjOrCostName", ProjOrCostName));
            cmd.Parameters.Add(new SqlParameter("@InvestAccount", InvestAccount));
            cmd.Parameters.Add(new SqlParameter("@Quantity", Quantity));
            cmd.Parameters.Add(new SqlParameter("@Unit", Unit));
            cmd.Parameters.Add(new SqlParameter("@InvesAmount", InvesAmount));
            cmd.Parameters.Add(new SqlParameter("@Remark", Remark));
            cmd.Parameters.Add(new SqlParameter("@CostType", CostType));
            return db.ExecuteNonQuery(cmd);
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
            string sql = "update Busi_Con_EndmateDetail set ProjOrCostName=@ProjOrCostName,InvestAccount=@InvestAccount,Quantity=@Quantity,Unit=@Unit,InvesAmount=@InvesAmount,CostType=@CostType,Remark=@Remark where Code=@Code";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@ProjOrCostName", ProjOrCostName));
            cmd.Parameters.Add(new SqlParameter("@InvestAccount", InvestAccount));
            cmd.Parameters.Add(new SqlParameter("@Quantity", Quantity));
            cmd.Parameters.Add(new SqlParameter("@Unit", Unit));
            cmd.Parameters.Add(new SqlParameter("@InvesAmount", InvesAmount));
            cmd.Parameters.Add(new SqlParameter("@Remark", Remark));
            cmd.Parameters.Add(new SqlParameter("@CostType", CostType));
            cmd.Parameters.Add(new SqlParameter("@Code", Code));
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取DataSet结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecNonQuery(string sql)
        {
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 通过预算下达文号找到下达的项目
        /// </summary>
        /// <param name="FileNo"></param>
        /// <returns></returns>
        public static DataSet GetProjNameByFileNo(string FileNo) 
        {
            string sql = "SELECT p.ProjName,f.FINANCIALFUND,f.FINANCIALREACHTIME FROM Busi_ProjRegister p,YW_V_FINANCIALINFO f WHERE p.Guid=f.PROJCODE AND f.FINANCIALNO='" + FileNo + "'";
            return db.ExecuteDataSet(sql);
        }
    }

}
