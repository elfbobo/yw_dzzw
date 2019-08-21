using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 概算明细
    /// </summary>
    public class EstimateDetail
    {
        /// <summary>
        /// 概算明细操作
        /// </summary>
        Database db = DatabaseFactory.CreateDatabase();//获取数据库连接
      /// <summary>
        /// 从中间表导入数据
      /// </summary>
      /// <param name="projGuid"></param>
      /// <returns></returns>
        public String importData(String projGuid)
        {
            DataSet KJFFDS = db.ExecuteDataSet("select * from  tb_temp_KJFF where ProjCode ='" + projGuid + "' order by  ENGINEERINGTYPE ");//获取中间表中项目概算明细
            DataSet EDDS = db.ExecuteDataSet("select * from Busi_Con_EstimateDetail where ProjGuid = '" + projGuid + "'");//查询当前表中是否含有该项目明细
            //如果已经存在返回1
            if (EDDS != null && EDDS.Tables[0].Rows.Count > 0)
            {
                return "1";
            }
            //如果中间表不存在该项目返回0
            if (KJFFDS == null || KJFFDS.Tables[0].Rows.Count == 0)
            {
                return "0";
            }
            try
            {
                EDDS.Tables[0].TableName = "Busi_Con_EstimateDetail";
                //遍历并插入数据
                foreach (DataRow dr in KJFFDS.Tables[0].Rows)
                {
                    DataRow newDr = EDDS.Tables[0].NewRow();
                    newDr["Guid"] = dr["Guid"];
                    String topGuid = "";
                    DataRow[] drs = KJFFDS.Tables[0].Select(" ENGINEERINGTYPE = '" + dr["PARENTCODE"] + "'");
                    if (drs == null || drs.Length == 0)
                    {
                        topGuid = null;
                    }
                    else
                    {
                        topGuid = drs[0]["guid"].ToString();
                    }
                    newDr["TopGuid"] = topGuid;//根据上级编号查找Guid
                    newDr["ProjGuid"] = dr["PROJCODE"];
                    newDr["OrderNum"] = dr["ENGINEERINGTYPE"];
                    newDr["ProjOrCostName"] = dr["COSTNAME"];
                    newDr["InvestAccount"] = dr["INVESTAMOUNT"];
                    newDr["Remark"] = dr["REMARK"];
                    newDr["Quantity"] = dr["QUANTITY"];
                    newDr["Unit"] = dr["UNIT"];
                    newDr["InvesAmount"] = dr["INVESTAMOUNT"];
                    newDr["CostType"] = dr["COSTTYPE"];
                    newDr["SysStatus"] = 0;
                    newDr["status"] = 0;
                    newDr["Code"] = dr["ENGINEERINGTYPE"];
                    EDDS.Tables[0].Rows.Add(newDr);
                }
                db.UpdateDataSet(EDDS);
                //导入成功返回
                return "3";
            }
            catch (Exception)
            {
                //导入报错返回2
                return "2";
            }
        }

    
        //}

        /// 根据项目Guid获取概算信息
        /// </summary>
        /// <param name="projGuid">项目主键</param>
        /// <returns></returns>
        public DataSet getData(String projGuid)
        {
            DataSet EDDS = db.ExecuteDataSet("select * from Busi_Con_EstimateDetail where ProjGuid = '" + projGuid + "'");//查询概算明细
            return EDDS;
        }

        /// <summary>
        /// 通过批复关系得到所对应概算明细
        /// </summary>
        /// <param name="ChangeBudgetaryGuid"></param>
        /// <returns></returns>
        public DataSet GetOldEstimateDetail(string ChangeBudgetaryGuid) 
        {
            string sql = "SELECT * FROM Busi_Con_EstimateDetail WHERE Guid in (SELECT EstimatesGuid FROM Busi_Con_BudgetAdjustmentRelevance WHERE ChangeBudgetaryGuid='" + ChangeBudgetaryGuid + "') ";
            DataSet OldDs = db.ExecuteDataSet(sql);
            return OldDs;
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
        public int InsertAddedBudget(string Guid,string TopGuid,string ProjGuid,string ProjOrCostName,string InvestAccount,string Remark,string Quantity,string Unit,string CostType) 
        {
            string sql = "INSERT INTO dbo.Busi_Con_EstimateDetail (Guid, TopGuid, ProjGuid, ProjOrCostName, InvestAccount, Remark, Quantity, Unit, CostType, status, SysStatus) VALUES ('" + Guid + "', '" + TopGuid + "', '" + ProjGuid + "','" + ProjOrCostName + "', " + InvestAccount + ", '" + Remark + "', " + Quantity + ", '" + Unit + "', '" + CostType + "', 0, 0)";
            int i = db.ExecuteNonQuery(sql);
            return i;
        }

        /// <summary>
        /// 删除项目批复所关联的概算
        /// </summary>
        /// <param name="ChangeBudgetaryGuid"></param>
        /// <returns></returns>
        public int DeleteAddedBudget(string ChangeBudgetaryGuid)
        {
            string sql = "DELETE FROM Busi_Con_EstimateDetail WHERE Guid IN (SELECT EstimatesGuid FROM Busi_Con_BudgetAdjustmentRelevance WHERE ChangeBudgetaryGuid='" + ChangeBudgetaryGuid + "')";
            int i = db.ExecuteNonQuery(sql);
            return i;
        }

        /// <summary>
        /// 插入批复和新增概算关联数据
        /// </summary>
        /// <param name="ChangeBudgetaryGuid"></param>
        /// <param name="EstimatesGuid"></param>
        /// <returns></returns>
        public int InsertBudgetAdjustmentRelevance(string ChangeBudgetaryGuid, string EstimatesGuid) 
        {
            string sql = "insert into Busi_Con_BudgetAdjustmentRelevance (Guid,ChangeBudgetaryGuid,EstimatesGuid,Sysstatus,Status) values ('" + Guid.NewGuid().ToString() + "','" + ChangeBudgetaryGuid + "','" + EstimatesGuid + "',0,0)";
            int i= db.ExecuteNonQuery(sql);
            return i;
        }

        /// <summary>
        /// 获取空的关联表
        /// </summary>
        /// <returns></returns>
        public DataSet getBudgetAdjustmentRelevanceDs() 
        {
            DataSet ds = db.ExecuteDataSet("select * from Busi_Con_BudgetAdjustmentRelevance where 1=2");
            return ds;
        }

        /// 根据项目Guid获取前期投资事项信息
        /// </summary>
        /// <param name="projGuid">项目主键</param>
        /// <returns></returns>
        public DataSet getEarlyInvestData(String projGuid)
        {
            DataSet EDDS = db.ExecuteDataSet("select * from Busi_EarlyInvest where ProjGuid = '" + projGuid + "'");//查询概算明细
            return EDDS;
        }

        /// 根据项目Guid获取概算外事项信息
        /// </summary>
        /// <param name="projGuid">项目主键</param>
        /// <returns></returns>
        public DataSet getOutsideInvestData(String projGuid)
        {
            DataSet EDDS = db.ExecuteDataSet("select * from Busi_OutsideEstimates where ProjGuid = '" + projGuid + "'");//查询概算明细
            return EDDS;
        }

        /// <summary>
        /// 该概算调整是否已批复
        /// </summary>
        /// <param name="ApprovalGuid"></param>
        /// <returns></returns>
        public int IsApproval(string ApprovalGuid) 
        {
            string sql = "SELECT COUNT(*) FROM Busi_Con_ProjChangeBudgetaryEstimate WHERE ApprovalTitle=NULL OR ApprovalTitle='' AND Guid='" + ApprovalGuid + "' ";
            int i = Convert.ToInt32(db.ExecuteReader(sql));
            return i;
        }



        /// <summary>
        /// 删除批复和新增概算关联数据
        /// </summary>
        /// <param name="ChangeBudgetaryGuid"></param>
        /// <returns></returns>
        public int DeleteBudgetAdjustmentRelevance(string ChangeBudgetaryGuid)
        {
            string sql = "DELETE FROM Busi_Con_BudgetAdjustmentRelevance WHERE ChangeBudgetaryGuid='" + ChangeBudgetaryGuid + "'";
            int i = db.ExecuteNonQuery(sql);
            return i;
        }

        /// <summary>
        /// 获取该项目的概算外事项
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public DataSet GetOutsideEstimates(string ProjGuid)
        {
            //通过projguid找到项目批复，再通过批复guid找到它所关联的概算外事项
            string sql = "SELECT * FROM Busi_OutsideEstimates WHERE sysstatus<>-1 and Guid IN (SELECT OutsideEstimatesGuid FROM Busi_Con_OutsideAdjustmentRelevance WHERE sysstatus<>-1 and  ChangeBudgetaryGuid IN (SELECT Guid FROM Busi_Con_ProjChangeBudgetaryEstimate WHERE sysstatus<>-1 and  ProjGuid='" + ProjGuid + "')) OR ProjGuid='DB43192C-F3CA-4C12-BFBB-02C1F7DDD5F3'";
            DataSet ds = db.ExecuteDataSet(sql);
            return ds;
        }

        /// <summary>
        /// 获取该项目原概算调整后的概算
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public DataSet GetRriginalBudgetAdjustment(string ProjGuid) 
        {
            string sql = "SELECT * FROM  V_EstimateDetail_RriginalBudgetAdjustment WHERE sysstatus<>-1 and  ProjGuid='" + ProjGuid + "'";
            DataSet ds = db.ExecuteDataSet(sql);
            return ds;
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
            string sql = "";
            string strAffiliation = BuildProcedureInfo.GetProjAffiliation(ProjGuid);

            if (strAffiliation == "集中立项可研")
            {
                //判断是否是项目建议书、可行性研究报告
                if (fileSign == "Busi_ProjProposal" || fileSign == "Busi_ProjFsbtyStudy")
                {
                    //获取项目的TopGuid主键
                    string topGuid = "select TopGuid from Busi_ProjRegister where Guid='" + ProjGuid + "' and sysstatus<>-1 ";
                    if (db.ExecuteScalar(topGuid).ToString() == "")//TopGuid为空是主项目
                    {
                        sql = "SELECT TOP 1 Guid FROM " + fileSign + " WHERE ProjGuid='" + ProjGuid + "' AND SysStatus<>-1 ORDER BY tstamp DESC";
                    }
                    else//子项目
                    {
                        sql = "SELECT TOP 1 Guid FROM " + fileSign + " WHERE ProjGuid='" + db.ExecuteScalar(topGuid).ToString() + "' AND SysStatus<>-1 ORDER BY tstamp DESC";
                    }
                }
                else
                {
                    sql = "SELECT TOP 1 Guid FROM " + fileSign + " WHERE ProjGuid='" + ProjGuid + "' AND SysStatus<>-1 ORDER BY tstamp DESC";
                }
            }
            else if (strAffiliation == "集中批复立项可研概算")
            {
                //获取项目的TopGuid主键
                string topGuid = "select TopGuid from Busi_ProjRegister where Guid='" + ProjGuid + "' and sysstatus<>-1 ";
                if (db.ExecuteScalar(topGuid).ToString() == "")//TopGuid为空是主项目
                {
                    sql = "SELECT TOP 1 Guid FROM " + fileSign + " WHERE ProjGuid='" + ProjGuid + "' AND SysStatus<>-1 ORDER BY tstamp DESC";
                }
                else//子项目
                {
                    sql = "SELECT TOP 1 Guid FROM " + fileSign + " WHERE ProjGuid='" + db.ExecuteScalar(topGuid).ToString() + "' AND SysStatus<>-1 ORDER BY tstamp DESC";
                }
            }
            else
            {

                sql = "SELECT TOP 1 Guid FROM " + fileSign + " WHERE ProjGuid='" + ProjGuid + "' AND SysStatus<>-1 ORDER BY tstamp DESC";
            }
            string refGuid = "";
            if (db.ExecuteScalar(sql) != null)
            {
                refGuid = db.ExecuteScalar(sql).ToString();
            }
            return refGuid;
        }
        /// <summary>
        /// 获取项目投资概算总额
        /// </summary>
        /// <returns></returns>
        public decimal GetInvestEstimate(string ProjGuid)
        {
            decimal InvestEstimate = -1;
            string sql = "SELECT InvestEstimate FROM Busi_ProjEstimate WHERE ProjGuid='" + ProjGuid + "' AND SysStatus<>-1";
            if (db.ExecuteScalar(sql) != null)
            {
                InvestEstimate = Convert.ToDecimal(db.ExecuteScalar(sql));
            }
            return InvestEstimate;
        }
        /// <summary>
        /// 投资完成月报金额合计
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public decimal GetInvestComplete(string ProjGuid)
        {
            decimal CompletedAmount = 0;
            string sql = "SELECT sum(CompletedAmount) FROM Busi_InvestComplete WHERE ProjGuid='" + ProjGuid + "' AND SysStatus<>-1";
            if (db.ExecuteScalar(sql).ToString() != "")
            {
                CompletedAmount = Convert.ToDecimal(db.ExecuteScalar(sql));
            }
            return CompletedAmount;
        }

        /// <summary>
        /// 资金支付月报合计
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public decimal GetFundPayment(string ProjGuid)
        {
            decimal PaymentAmount = 0;
            string sql = "SELECT sum(PaymentAmount) FROM Busi_FundPayment WHERE  ProjGuid='" + ProjGuid + "' AND SysStatus<>-1";
            if (db.ExecuteScalar(sql).ToString() != "")
            {
                PaymentAmount = Convert.ToDecimal(db.ExecuteScalar(sql));
            }
            return PaymentAmount;
        }
    }
}
