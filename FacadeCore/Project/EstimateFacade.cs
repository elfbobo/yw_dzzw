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
    /// 概算明细业务操作
    /// </summary>
    public class EstimateFacade
    {

        EstimateDetail ed = new EstimateDetail();

        /// <summary>
        /// 根据项目Guid从中间表导入概算明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public String ImportData(String ProjGuid)
        {
            return ed.importData(ProjGuid);
        }
        /// <summary>
        /// 根据项目Guid获取概算信息
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="HasGuid"></param>
        /// <returns></returns>
        public String getData(String projGuid, Boolean HasGuid)
        {
            DataSet ds = ed.getData(projGuid);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            int i = 0;
            String json = "";
            if (HasGuid)
            {
                foreach (DataRow dr in ds.Tables[0].Select(" TopGuid is null  or TopGuid=''"))
                {
                    i++;

                    if (json != "")
                    {
                        json += ",";
                    }

                    json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Quantity:'" + dr["Quantity"] + "',Unit:'" + dr["Unit"] + "',InvesAmount:'" + dr["InvesAmount"] + "',CostType:'" + dr["CostType"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        json += ",children:[" + setData(dr, ds, i.ToString(), true) + "] }";

                    }
                    else
                    {
                        json += " }";
                    }

                }
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Select(" TopGuid is null or TopGuid='' "))
                {
                    i++;

                    if (json != "")
                    {
                        json += ",";
                    }

                    json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Quantity:'" + dr["Quantity"] + "',Unit:'" + dr["Unit"] + "',InvesAmount:'" + dr["InvesAmount"] + "',CostType:'" + dr["CostType"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        json += ",children:[" + setData(dr, ds, i.ToString(), false) + "] }";

                    }
                    else
                    {
                        json += " }";
                    }

                }
            }
            return "{\"total\":0,\"rows\":[" + json + "]}";
            //  return "["+json+"]";


        }

        /// <summary>
        /// 递归给概算明细编号
        /// </summary>
        /// <param name="topDr"></param>
        /// <param name="ds"></param>
        /// <param name="code"></param>
        /// <param name="hasGuid"></param>
        /// <returns></returns>
        public string setData(DataRow topDr, DataSet ds, string code, Boolean hasGuid)
        {
            int i = 0;
            string nowStr = "";
            foreach (DataRow dr in ds.Tables[0].Select(" topGuid = '" + topDr["Guid"] + "'"))
            {
                i++;
                if (i > 1)
                {
                    nowStr += ",";
                }
                if (hasGuid)
                {
                    nowStr += "{Guid:'" + dr["Guid"] + "', code:'" + code + "." + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Quantity:'" + dr["Quantity"] + "',Unit:'" + dr["Unit"] + "',InvesAmount:'" + dr["InvesAmount"] + "',CostType:'" + dr["CostType"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        nowStr += ",children:[" + setData(dr, ds, code + "." + i, hasGuid) + "] }";

                    }
                    else
                    {
                        nowStr += " }";
                    }
                }
                else
                {
                    nowStr += "{  code:'" + code + "." + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Quantity:'" + dr["Quantity"] + "',Unit:'" + dr["Unit"] + "',InvesAmount:'" + dr["InvesAmount"] + "',CostType:'" + dr["CostType"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        nowStr += ",children:[" + setData(dr, ds, code + "." + i, hasGuid) + "] }";

                    }
                    else
                    {
                        nowStr += " }";
                    }
                }

            }
            return nowStr;
        }

        /// <summary>
        /// 通过批复关系得到所对应概算明细
        /// </summary>
        /// <param name="ChangeBudgetaryGuid"></param>
        /// <returns></returns>
        public DataSet GetOldEstimateDetail(string ChangeBudgetaryGuid)
        {
            return ed.GetOldEstimateDetail(ChangeBudgetaryGuid);
        }

        /// <summary>
        /// 插入新增概算
        /// </summary>
        /// <param name="Guid"></param>
        /// <param name="TopGuid"></param>
        /// <param name="ProjGuid"></param>
        /// <param name="ProjOrCostName"></param>
        /// <param name="InvestAccount"></param>
        /// <param name="Remark"></param>
        /// <param name="Quantity"></param>
        /// <param name="Unit"></param>
        /// <param name="CostType"></param>
        /// <returns></returns>
        public int InsertAddedBudget(string Guid, string TopGuid, string ProjGuid, string ProjOrCostName, string InvestAccount, string Remark, string Quantity, string Unit, string CostType)
        {
            return ed.InsertAddedBudget(Guid, TopGuid, ProjGuid, ProjOrCostName, InvestAccount, Remark, Quantity, Unit, CostType);
        }

        /// <summary>
        /// 获取空的关联表
        /// </summary>
        /// <returns></returns>
        public DataSet getBudgetAdjustmentRelevanceDs()
        {
            return ed.getBudgetAdjustmentRelevanceDs();
        }

        /// <summary>
        /// 删除项目批复关联的概算
        /// </summary>
        /// <param name="ChangeBudgetaryGuid"></param>
        /// <returns></returns>
        public int DeleteAddedBudget(string ChangeBudgetaryGuid)
        {
            return ed.DeleteAddedBudget(ChangeBudgetaryGuid);
        }

        /// <summary>
        /// 插入批复和新增概算关联数据
        /// </summary>
        /// <param name="ChangeBudgetaryGuid"></param>
        /// <param name="EstimatesGuid"></param>
        /// <returns></returns>
        public int InsertBudgetAdjustmentRelevance(string ChangeBudgetaryGuid, string EstimatesGuid)
        {
            return ed.InsertBudgetAdjustmentRelevance(ChangeBudgetaryGuid, EstimatesGuid);
        }

        /// <summary>
        /// 根据项目Guid获取前期投资事项信息
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="HasGuid"></param>
        /// <returns></returns>
        public String getEarInvestData(String projGuid, Boolean HasGuid)
        {
            DataSet ds = ed.getEarlyInvestData(projGuid);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            int i = 0;
            String json = "";
            if (HasGuid)
            {
                foreach (DataRow dr in ds.Tables[0].Select(" TopGuid is null  or TopGuid=''"))
                {
                    i++;
                    if (json != "")
                    {
                        json += ",";
                    }
                    json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',PlanAmount:'" + dr["PlanAmount"] + "',CostType:'" + dr["CostType"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        json += ",children:[" + setEarlyData(dr, ds, i.ToString(), true) + "] }";
                    }
                    else
                    {
                        json += " }";
                    }
                }
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Select(" TopGuid is null or TopGuid='' "))
                {
                    i++;
                    if (json != "")
                    {
                        json += ",";
                    }
                    json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',PlanAmount:'" + dr["PlanAmount"] + "',CostType:'" + dr["CostType"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        json += ",children:[" + setEarlyData(dr, ds, i.ToString(), false) + "] }";
                    }
                    else
                    {
                        json += " }";
                    }
                }
            }
            return "{\"total\":0,\"rows\":[" + json + "]}";
            //  return "["+json+"]";
        }

        /// <summary>
        /// 根据项目Guid获取概算外事项信息
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public String getOutsideData(String projGuid)
        {
            DataSet ds = ed.getOutsideInvestData(projGuid);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            int i = 0;
            String json = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                i++;
                if (json != "")
                {
                    json += ",";
                }
                json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'}";
            }
            return "{\"total\":0,\"rows\":[" + json + "]}";
            //  return "["+json+"]";
        }

        /// <summary>
        /// 递归给前期投资事项、概算外事项明细编号
        /// </summary>
        /// <param name="topDr"></param>
        /// <param name="ds"></param>
        /// <param name="code"></param>
        /// <param name="hasGuid"></param>
        /// <returns></returns>
        public string setEarlyData(DataRow topDr, DataSet ds, string code, Boolean hasGuid)
        {
            int i = 0;
            string nowStr = "";
            foreach (DataRow dr in ds.Tables[0].Select(" topGuid = '" + topDr["Guid"] + "'"))
            {
                i++;
                if (i > 1)
                {
                    nowStr += ",";
                }
                if (hasGuid)
                {
                    nowStr += "{Guid:'" + dr["Guid"] + "', code:'" + code + "." + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',PlanAmount:'" + dr["PlanAmount"] + "',CostType:'" + dr["CostType"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        nowStr += ",children:[" + setEarlyData(dr, ds, code + "." + i, hasGuid) + "] }";

                    }
                    else
                    {
                        nowStr += " }";
                    }
                }
                else
                {
                    nowStr += "{  code:'" + code + "." + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',PlanAmount:'" + dr["PlanAmount"] + "',CostType:'" + dr["CostType"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        nowStr += ",children:[" + setEarlyData(dr, ds, code + "." + i, hasGuid) + "] }";

                    }
                    else
                    {
                        nowStr += " }";
                    }
                }

            }
            return nowStr;
        }


         /// <summary>
        /// 该概算调整是否已批复
        /// </summary>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public int IsApproval(string ApprovalGuid)
        {
            return ed.IsApproval(ApprovalGuid);
        }

        /// <summary>
        /// 删除批复和新增概算关联数据
        /// </summary>
        /// <param name="ChangeBudgetaryGuid"></param>
        /// <returns></returns>
        public int DeleteBudgetAdjustmentRelevance(string ChangeBudgetaryGuid)
        {
            return ed.DeleteBudgetAdjustmentRelevance(ChangeBudgetaryGuid);
        }


        /// <summary>
        /// 获取该项目的最终概算明细
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public DataSet GetFinalEstimates(string projGuid)
        {
            DataSet EstimatesDs = ed.getData(projGuid);//获取该项目的概算明细
            DataSet OutsideEstimatesDs = ed.GetOutsideEstimates(projGuid);//获取概算外事项
            DataSet RriginalBudgetAdjustmentDs = ed.GetRriginalBudgetAdjustment(projGuid);//获取原概算调整概算

            //将概算外事项加到概算明细中
            if (OutsideEstimatesDs.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < OutsideEstimatesDs.Tables[0].Rows.Count; i++)
                {
                    DataRow EstimatesDr = EstimatesDs.Tables[0].NewRow();
                    EstimatesDr["Guid"] = OutsideEstimatesDs.Tables[0].Rows[i]["Guid"];
                    EstimatesDr["TopGuid"] = OutsideEstimatesDs.Tables[0].Rows[i]["EstimateGuid"];
                    EstimatesDr["ProjOrCostName"] = OutsideEstimatesDs.Tables[0].Rows[i]["ProjOrCostName"];
                    EstimatesDr["ProjGuid"] = OutsideEstimatesDs.Tables[0].Rows[i]["ProjGuid"];
                    EstimatesDr["InvestAccount"] = OutsideEstimatesDs.Tables[0].Rows[i]["InvestAccount"];
                    EstimatesDr["Quantity"] = OutsideEstimatesDs.Tables[0].Rows[i]["Quantity"];
                    EstimatesDr["Unit"] = OutsideEstimatesDs.Tables[0].Rows[i]["Unit"];
                    EstimatesDr["Remark"] = OutsideEstimatesDs.Tables[0].Rows[i]["Remark"];
                    EstimatesDr["Status"] = OutsideEstimatesDs.Tables[0].Rows[i]["Status"];
                    EstimatesDr["Sysstatus"] = 8;//8是标识概算外事项
                    EstimatesDs.Tables[0].Rows.Add(EstimatesDr);
                }
            }

            if (EstimatesDs.Tables[0].Rows.Count > 0) 
            {
                for (int i = 0; i < EstimatesDs.Tables[0].Rows.Count; i++)
                {
                    //查出调整概算对应的原概算
                    DataRow[] dr = RriginalBudgetAdjustmentDs.Tables[0].Select("EstimateDetailGuid='" + EstimatesDs.Tables[0].Rows[i]["Guid"] + "'");
                    
                    if (dr.Count() > 0) 
                    {
                        //将原概算数据替换成调整的概算
                        //EstimatesDs.Tables[0].Rows[i].Delete();
                        EstimatesDs.Tables[0].Rows[i]["InvestAccount"] = dr[0]["InvestAccount"];
                        EstimatesDs.Tables[0].Rows[i]["Quantity"] = dr[0]["Quantity"];
                        EstimatesDs.Tables[0].Rows[i]["Unit"] = dr[0]["Unit"];
                        EstimatesDs.Tables[0].Rows[i]["Sysstatus"] = 9;//9是标识原概算调整的
                    }
                }
            }
            return EstimatesDs;
        }

        /// <summary>
        /// 根据项目Guid获取最终概算信息
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="HasGuid"></param>
        /// <returns></returns>
        public String getFinalEstimatesData(String projGuid, Boolean HasGuid)
        {
            DataSet ds = GetFinalEstimates(projGuid);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            int i = 0;
            String json = "";
            if (HasGuid)
            {
                foreach (DataRow dr in ds.Tables[0].Select(" TopGuid is null  or TopGuid=''"))
                {
                    i++;

                    if (json != "")
                    {
                        json += ",";
                    }

                    json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Quantity:'" + dr["Quantity"] + "',Unit:'" + dr["Unit"] + "',InvesAmount:'" + dr["InvesAmount"] + "',CostType:'" + dr["CostType"] + "',Sysstatus:'" + dr["Sysstatus"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        json += ",children:[" + setFinalData(dr, ds, i.ToString(), true) + "] }";

                    }
                    else
                    {
                        json += " }";
                    }

                }
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Select(" TopGuid is null or TopGuid='' "))
                {
                    i++;

                    if (json != "")
                    {
                        json += ",";
                    }

                    json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Quantity:'" + dr["Quantity"] + "',Unit:'" + dr["Unit"] + "',InvesAmount:'" + dr["InvesAmount"] + "',CostType:'" + dr["CostType"] + "',Sysstatus:'" + dr["Sysstatus"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        json += ",children:[" + setFinalData(dr, ds, i.ToString(), false) + "] }";

                    }
                    else
                    {
                        json += " }";
                    }

                }
            }
            return "{\"total\":0,\"rows\":[" + json + "]}";
            //  return "["+json+"]";
        }

        /// <summary>
        /// 递归给概算明细编号
        /// </summary>
        /// <param name="topDr"></param>
        /// <param name="ds"></param>
        /// <param name="code"></param>
        /// <param name="hasGuid"></param>
        /// <returns></returns>
        public string setFinalData(DataRow topDr, DataSet ds, string code, Boolean hasGuid)
        {
            int i = 0;
            string nowStr = "";
            foreach (DataRow dr in ds.Tables[0].Select(" topGuid = '" + topDr["Guid"] + "'"))
            {
                i++;
                if (i > 1)
                {
                    nowStr += ",";
                }
                if (hasGuid)
                {
                    nowStr += "{Guid:'" + dr["Guid"] + "', code:'" + code + "." + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Quantity:'" + dr["Quantity"] + "',Unit:'" + dr["Unit"] + "',InvesAmount:'" + dr["InvesAmount"] + "',CostType:'" + dr["CostType"] + "',Sysstatus:'" + dr["Sysstatus"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        nowStr += ",children:[" + setFinalData(dr, ds, code + "." + i, hasGuid) + "] }";

                    }
                    else
                    {
                        nowStr += " }";
                    }
                }
                else
                {
                    nowStr += "{  code:'" + code + "." + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',InvestAccount:'" + dr["InvestAccount"] + "',Quantity:'" + dr["Quantity"] + "',Unit:'" + dr["Unit"] + "',InvesAmount:'" + dr["InvesAmount"] + "',CostType:'" + dr["CostType"] + "',Sysstatus:'" + dr["Sysstatus"] + "',Remark:'" + (dr["Remark"] != null ? dr["Remark"].ToString().Replace("\n", " ") : "") + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["GUid"] + "'").Length > 0)
                    {
                        nowStr += ",children:[" + setFinalData(dr, ds, code + "." + i, hasGuid) + "] }";

                    }
                    else
                    {
                        nowStr += " }";
                    }
                }

            }
            return nowStr;
        }
        /// <summary>
        /// 2015-11-11
        /// 通过项目Guid找到建设程序中证照批复的Guid
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="fileSign"></param>
        /// <returns></returns>
        public string GetRefGuid(string ProjGuid, string fileSign)
        {
            return ed.GetRefGuid(ProjGuid, fileSign);
        }

        /// <summary>
        /// 获取项目投资概算总额
        /// </summary>
        /// <returns></returns>
        public decimal GetInvestEstimate(string ProjGuid)
        {
            return ed.GetInvestEstimate(ProjGuid);
        }
        /// <summary>
        /// 投资完成月报金额合计
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public decimal GetInvestComplete(string ProjGuid)
        {
            return ed.GetInvestComplete(ProjGuid);
        }

        /// <summary>
        /// 资金支付月报合计
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public decimal GetFundPayment(string ProjGuid)
        {
            return ed.GetFundPayment(ProjGuid);
        }
    }
}
