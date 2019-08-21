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
    /// 项目推进责任
    /// </summary>
    public static class ProjAdvanceInfo
    {
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }


        /// <summary>
        /// 获取项目名称和项目总投资
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjNameAndInvest(string ProjGuid)
        {
            string sql = "SELECT r.ProjName,b.ApprovalInvest FROM Busi_ProjRegister r,Busi_ProjBaseInfo b WHERE r.Guid=b.ProjGuid AND r.Guid='" + ProjGuid + "'";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ProjName"] = DBNull.Value;
                dr["ApprovalInvest"] = DBNull.Value;
                ds.Tables[0].Rows.Add(dr);
            }
            return ds;
        }

        /// <summary>
        /// 获取责任体系中的数据
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetResponsibility(string ProjGuid, int DepartType)
        {
            string sql = "SELECT Dept,Leader,MainDept,MainUser FROM Busi_Responsibility WHERE ProjGuid='" + ProjGuid + "' AND SysStatus<>-1 AND DepartType='" + DepartType + "'";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["Dept"] = DBNull.Value;
                dr["Leader"] = DBNull.Value;
                dr["MainDept"] = DBNull.Value;
                dr["MainUser"] = DBNull.Value;
                ds.Tables[0].Rows.Add(dr);
            }
            return ds;
        }

        /// <summary>
        /// 获取项目年度计划信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjPlanYear(string ProjGuid) 
        {
            string sql = "SELECT * FROM Busi_ProjectYearPlan WHERE SysStatus<>-1 AND Year=" + DateTime.Now.Year + " and ProjGuid='" + ProjGuid + "'";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["YearObjectives"] = DBNull.Value;
                dr["CentralFinance"] = 0;
                dr["ProvinceFinance"] = 0;
                dr["CityFinance"] = 0;
                dr["OtherFinance"] = 0;
                ds.Tables[0].Rows.Add(dr);
            }
            return ds;
        }

        /// <summary>
        /// 获取该项目的主要手续
        /// </summary>
        /// <param name="projGuid">项目Guid</param>
        /// <returns></returns>
        public static DataSet GetProjMainProcedure(string projGuid)
        {
            DataSet MainDs = GetDatabase().ExecuteDataSet("SELECT Guid,ApprovalGuid,PlanStartDate,PlanEndDate,Name,Type,code,OrderCode,TypeCode FROM V_Procedure_Config WHERE ProjGuid='" + projGuid + "' and TypeCode=0  ORDER BY OrderCode ,TypeCode");
            if (MainDs.Tables[0].Rows.Count == 0)
            {
                DataRow dr = MainDs.Tables[0].NewRow();
                dr["Guid"] = DBNull.Value;
                dr["ApprovalGuid"] = DBNull.Value;
                dr["PlanStartDate"] = DBNull.Value;
                dr["PlanEndDate"] = DBNull.Value;
                dr["Name"] = DBNull.Value;
                dr["Type"] = DBNull.Value;
                dr["code"] = DBNull.Value;
                dr["OrderCode"] = DBNull.Value;
                dr["TypeCode"] = DBNull.Value;
                MainDs.Tables[0].Rows.Add(dr);
            }
            return MainDs;
        }

        /// <summary>
        /// 获取该项目其他手续的类型
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjOtherConfig(string projGuid)
        {
            DataSet OtherConfigDs = GetDatabase().ExecuteDataSet("SELECT DISTINCT Type ,TypeCode FROM V_Procedure_Config  WHERE Typecode!=0 and  ProjGuid='" + projGuid + "' ORDER BY TypeCode");

            if (OtherConfigDs.Tables[0].Rows.Count == 0)
            {
                DataRow dr = OtherConfigDs.Tables[0].NewRow();
                dr["Type"] = DBNull.Value;
                dr["TypeCode"] = DBNull.Value;
                OtherConfigDs.Tables[0].Rows.Add(dr);
            }
            return OtherConfigDs;
        }


        /// <summary>
        /// 获取该项目的其他手续
        /// </summary>
        /// <param name="projGuid">项目Guid</param>
        /// <param name="Type">手续类型</param>
        /// <returns></returns>
        public static DataSet GetProjOtherProcedure(string projGuid, string Type)
        {
            DataSet OtherDs = GetDatabase().ExecuteDataSet("SELECT Guid,ApprovalGuid,PlanStartDate,PlanEndDate,Name,Type,code,OrderCode,TypeCode FROM V_Procedure_Config WHERE ProjGuid='" + projGuid + "' and Type='" + Type + "'  ORDER BY convert(INT, Code),TypeCode");
            return OtherDs;
        }

        /// <summary>
        /// 获取该项目手续中的子手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjChildProcedure(string ProjGuid, string ApprovalGuid) 
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from Busi_ProjProcedureChild where ProjGuid='{0}' AND MApprovalGuid='{1}' ORDER BY CProcedureType", ProjGuid, ApprovalGuid));
            return ds;
        }

        /// <summary>
        /// 获取手续的编制单位
        /// </summary>
        /// <returns></returns>
        public static string GetCompileUnit(string ProjGuid, string ApprovalGuid) 
        {
            string sql = "SELECT UnitName FROM Busi_Con_ProjJointAcceptanceUnit WHERE SysStatus<>-1 AND Guid IN (SELECT EstablishmentUnitGuid FROM Busi_CstrctProcessCompile WHERE ProjGuid='" + ProjGuid + "' AND ApprovalGuid='" + ApprovalGuid + "')";
            string Unit = "";
            if (GetDatabase().ExecuteScalar(sql) != null)
            {
                Unit = GetDatabase().ExecuteScalar(sql).ToString();

            }
            return Unit;
        }

        /// <summary>
        /// 获取手续的审批单位
        /// </summary>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static string GetApplyUnit(string ApprovalGuid) 
        {
            string sql = "SELECT Name FROM Sys_Mapping WHERE Guid IN (SELECT UnitGuid FROM Idty_UnitAndFileTypeConfig WHERE FileTypeGuid='" + ApprovalGuid + "')";
            string Unit = "";
            if (GetDatabase().ExecuteScalar(sql) != null)
            {
                Unit = GetDatabase().ExecuteScalar(sql).ToString();

            }
            return Unit;
        }

        /// <summary>
        /// 获取手续的修订单位
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static string GetReviseUnit(string ProjGuid, string ApprovalGuid) 
        {
            string sql = "SELECT UnitName FROM Busi_Con_ProjJointAcceptanceUnit WHERE SysStatus<>-1 AND Guid IN ( SELECT ReviseUnitGuid FROM Busi_CstrctProcessRevise WHERE ProjGuid='" + ProjGuid + "' AND ApprovalGuid='" + ApprovalGuid + "')";
            string Unit = "";
            if (GetDatabase().ExecuteScalar(sql) != null)
            {
                Unit = GetDatabase().ExecuteScalar(sql).ToString();

            }
            return Unit;
        }

        /// <summary>
        /// 获取手续的批复单位、批复文号
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="ApprovalGuid">手续主键</param>
        /// <returns>批复基本信息DataTable</returns>
        public static DataSet  GetCstrctProcessReply(string ProjGuid, string ApprovalGuid)
        {
            string sql = string.Format("select ReplyCode, ReplyUnit from V_CstrctProcess_Reply where ProjGuid='{0}' and charindex('{1}',ApprovalGuid)>0", ProjGuid, ApprovalGuid);
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ReplyCode"] = DBNull.Value;
                dr["ReplyUnit"] = DBNull.Value;
                ds.Tables[0].Rows.Add(dr);
            }
            return ds;
        }

        /// <summary>
        /// 获取项目手续的审批开始时间
        /// </summary>
        /// <returns></returns>
        public static string GetProjMainPrepStartDate(string ProjGuid, string ApprovalGuid)
        {
            string sql = "select DateOfCompletion from Busi_ProjPrep where ProjGuid='" + ProjGuid + "' and ApprovalGuid='" + ApprovalGuid + "' and (ApprovalChildType is NULL or ApprovalChildType='') AND DateOfCompletion is not null";
            string DateOfCompletion = "";
            if (GetDatabase().ExecuteScalar(sql) != null)
            {
                DateOfCompletion = GetDatabase().ExecuteScalar(sql).ToString();
            }
            return DateOfCompletion.Split(' ')[0].ToString();
        }

        /// <summary>
        /// 获取项目手续的审批办结时间
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static string GetProjMainPrepEndDate(string ProjGuid, string ApprovalGuid)
        {
            string sql = "select ActualCompletionDate from Busi_ProjPrep where ProjGuid='" + ProjGuid + "' and ApprovalGuid='" + ApprovalGuid + "' and (ApprovalChildType is NULL or ApprovalChildType='') and ApplyForStatus='1'";
            string ActualCompletionDate = "";
            if (GetDatabase().ExecuteScalar(sql) != null)
            {
                ActualCompletionDate = GetDatabase().ExecuteScalar(sql).ToString();
            }
            return ActualCompletionDate.Split(' ')[0].ToString();
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
            string sql = "select DateOfCompletion from Busi_ProjPrep where ProjGuid='" + ProjGuid + "' and ApprovalGuid='" + ApprovalGuid + "' and ApprovalChildType='" + ApprovalChildType + "' and DateOfCompletion is not null";
            string DateOfCompletion = "";
            if (GetDatabase().ExecuteScalar(sql) != null)
            {
                DateOfCompletion = GetDatabase().ExecuteScalar(sql).ToString();
            }
            return DateOfCompletion.Split(' ')[0].ToString();
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
            string sql = "select ActualCompletionDate from Busi_ProjPrep where ProjGuid='" + ProjGuid + "' and ApprovalGuid='" + ApprovalGuid + "' and ApprovalChildType='" + ApprovalChildType + "' and ApplyForStatus='1'";
            string ActualCompletionDate = "";
            if (GetDatabase().ExecuteScalar(sql) != null)
            {
                ActualCompletionDate = GetDatabase().ExecuteScalar(sql).ToString();
            } return ActualCompletionDate.Split(' ')[0].ToString();
        }

        /// <summary>
        /// 获取项目主手续的月度进展信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjMainMonthPrepInfo(string ProjGuid, string ApprovalGuid) 
        {
            string sql = "select Guid,Problems,Solutions, ProgressSituation from Busi_ProjPrep where ProjGuid='" + ProjGuid + "' and ApprovalGuid='" + ApprovalGuid + "' and Year=" + DateTime.Now.Year + " and Month=" + DateTime.Now.Month + " and (ApprovalChildType is NULL or ApprovalChildType='')";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["Guid"] = DBNull.Value;
                dr["Problems"] = DBNull.Value;
                dr["Solutions"] = DBNull.Value;
                dr["ProgressSituation"] = DBNull.Value;
                ds.Tables[0].Rows.Add(dr);
            }
            return ds;
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
            string sql = "select Guid,Problems,Solutions, ProgressSituation from Busi_ProjPrep where ProjGuid='" + ProjGuid + "' and ApprovalGuid='" + ApprovalGuid + "' and Year=" + DateTime.Now.Year + " and Month=" + DateTime.Now.Month + " and ApprovalChildType='" + ApprovalChildType + "'";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["Guid"] = DBNull.Value;
                dr["Problems"] = DBNull.Value;
                dr["Solutions"] = DBNull.Value;
                dr["ProgressSituation"] = DBNull.Value;
                ds.Tables[0].Rows.Add(dr);
            }
            return ds;
        }

        /// <summary>
        /// 获取当前年度的项目月度计划、计划完成投资、资金使用计划
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataSet GetProjMonthPlan(string projGuid,int month) 
        {
            string sql = "SELECT Guid, Year, Month, MonthEvolve, MonthPlanFinancial, CentralPlaceFinance, CityPlaceFinance, OtherPlaceFinance, CentralPlanFinance, CityPlanFinance, OtherPlanFinance FROM Busi_ProjectMonthPlan WHERE ProjGuid='" + projGuid + "' AND SysStatus<>-1 AND Year=" + DateTime.Now.Year + " AND Month=" + month + "";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0) 
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["Guid"] = DBNull.Value;
                dr["Year"] = DBNull.Value;
                dr["Month"] = DBNull.Value;
                dr["MonthEvolve"] = DBNull.Value;
                dr["MonthPlanFinancial"] = 0;
                dr["CentralPlaceFinance"] = 0;
                dr["CityPlaceFinance"] = 0;
                dr["OtherPlaceFinance"] = 0;
                dr["CentralPlanFinance"] = 0;
                dr["CityPlanFinance"] = 0;
                dr["OtherPlanFinance"] = 0;
                ds.Tables[0].Rows.Add(dr);
            }
            return ds;
        }

        /// <summary>
        /// 计划使用资金小计
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static decimal GetPlanFinancial(string projGuid, int month)
        {
            string sql = "SELECT sum( CentralPlanFinance+CityPlanFinance+otherPlanFinance) AS PlanFinancial FROM Busi_ProjectMonthPlan WHERE ProjGuid='" + projGuid + "' AND SysStatus<>-1 AND Year=" + DateTime.Now.Year + " AND Month=" + month + "";
            decimal PlanFinancial = 0;
            if (GetDatabase().ExecuteScalar(sql).ToString()!="")
            {
                PlanFinancial = Convert.ToDecimal(GetDatabase().ExecuteScalar(sql).ToString());
            }
            return PlanFinancial;
        }

        /// <summary>
        /// 项目实际进展情况（形象进度月报）
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static string GetProjConstructReport(string projGuid, int Month) 
        {
            string sql = "SELECT ImageDesp FROM busi_ConstructMonthReport WHERE ProjGuid='" + projGuid + "' AND ReportYear=" + DateTime.Now.Year + " AND ReportMonth=" + Month + " AND SysStatus<>-1";
            string ImageDesp = "";
            if (GetDatabase().ExecuteScalar(sql) != null)
            {
                ImageDesp = GetDatabase().ExecuteScalar(sql).ToString();
            }
            return ImageDesp;
        }

        /// <summary>
        /// 项目实际月完成投资
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static decimal GetProjMonthInvestComplete(string projGuid, int Month)
        {
            string sql = "SELECT sum(CompletedAmount) FROM Busi_InvestComplete WHERE ProjGuid='" + projGuid + "' AND Year='" + DateTime.Now.Year + "' AND Month='" + Month + "' AND SysStatus<>-1";
            decimal CompletedAmount = 0;
            if (GetDatabase().ExecuteScalar(sql).ToString() != "")
            {
                CompletedAmount = Convert.ToDecimal(GetDatabase().ExecuteScalar(sql).ToString());
            }
            return CompletedAmount;
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
            string sql = "SELECT sum(InPlaceAmount) FROM Busi_FundsMonthly WHERE ProjGuid='" + projGuid + "' AND Year='" + DateTime.Now.Year + "' AND Month='" + Month + "' AND MonthlyType IN (" + monthType + ") AND sysstatus<>-1";
            decimal InPlaceAmount = 0;
            
            if (GetDatabase().ExecuteScalar(sql).ToString() != "")
            {
                InPlaceAmount = Convert.ToDecimal(GetDatabase().ExecuteScalar(sql).ToString());
            }
            return InPlaceAmount;
        }

        /// <summary>
        /// 月资金支付合计
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static decimal GetProjFundPayment(string projGuid, int month)
        {
            string sql = "SELECT sum(PaymentAmount) FROM Busi_FundPayment WHERE ProjGuid='" + projGuid + "' AND Year=" + DateTime.Now.Year + " AND Month=" + month + " AND SysStatus<>-1";
            decimal PaymentAmount = 0;
            if (GetDatabase().ExecuteScalar(sql).ToString() != "")
            {
                PaymentAmount = Convert.ToDecimal(GetDatabase().ExecuteScalar(sql).ToString());
            }
            return PaymentAmount;
        }

        /// <summary>
        /// 支付金额来源
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataSet GetProjInvestFromDetails(string projGuid, int month) 
        {
            string sql = "SELECT sum(NatInvest) AS NatInvest ,sum(ConInvest) AS ConInvest,(sum(ProvInvest)+sum(localInvest)+sum(CityInvest)+sum(EntInvests)+sum(BackInvests)+sum(OthInvest)) AS  OthInvest FROM Busi_InvestFromDetails WHERE RefGuid IN (SELECT Guid FROM Busi_FundPayment WHERE ProjGuid='" + projGuid + "' AND Year=" + DateTime.Now.Year + " AND Month=" + month + " AND SysStatus<>-1)";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            return ds;
        }
    }
}
