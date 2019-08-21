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
    /// 首页显示统计信息
    /// </summary>
    public class IndexGetStatistics
    {
        /// <summary>
        /// 根据查询条件统计
        /// </summary>
        /// <param name="Condition">查询调教</param>
        /// <param name="Datas">返回统计图数据</param>
        /// <returns>结果集</returns>
        public static string GetIndexStatisticsHtml(string Condition, out string Datas)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "select [guid] from [V_Busi_ProjRegisterNew] where sysstatus<>-1 and ProjAffiliation='FA173C37-BF98-4AAD-9997-817835165E29'";
            DataSet DsTemp = db.ExecuteDataSet(sql);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
            {
                sb.Append("'");
                sb.Append(DsTemp.Tables[0].Rows[i]["guid"].ToString());
                sb.Append("',");
            }
            string temp = sb.ToString();
            string guid = temp.Substring(0, temp.Length - 1);
            sb.Clear();
            Datas = "[";
            sql = " select * from [V_Busi_ProjRegisterNew] where  1=1 " + Condition + " ";
            DataSet ds = db.ExecuteDataSet(sql);
            DataTable dtAll = ds.Tables[0];//所有符合条件的项目基本信息

            Dictionary<string, string> dic = new Dictionary<string, string>();
            StringBuilder sbAllGuid = new StringBuilder();
            StringBuilder sbAllTopGuid = new StringBuilder();
            StringBuilder sbAllGuidTemp = new StringBuilder();
            for (int i = 0; i < dtAll.Rows.Count; i++)
            {
                dic.Add(dtAll.Rows[i]["Guid"].ToString(), dtAll.Rows[i]["TopGuid"].ToString());
            }
            foreach (KeyValuePair<string, string> kv in dic)
            {
                if (kv.Value == "")
                {
                    sbAllGuid.Append("'");
                    sbAllGuid.Append(kv.Key);
                    sbAllGuid.Append("',");
                }
                else
                {
                    sbAllTopGuid.Append("'");
                    sbAllTopGuid.Append(kv.Value);
                    sbAllTopGuid.Append("',");
                    sbAllGuidTemp.Append("'");
                    sbAllGuidTemp.Append(kv.Key);
                    sbAllGuidTemp.Append("',");
                }
            }
            string strAllGuid = sbAllGuid.ToString();
            string strAllTopGuid = sbAllTopGuid.ToString();
            string strAllGuidTemp = sbAllGuidTemp.ToString();
            sql = "select * from [V_Busi_ProjRegisterNew] where 1=1  ";
            if (!string.IsNullOrEmpty(strAllGuid))
            {
                strAllGuid = strAllGuid.Substring(0, strAllGuid.Length - 1);
                sql += " and (topguid in (" + strAllGuid + ") or guid in (" + strAllGuid + ")) ";
            }
            if (!string.IsNullOrEmpty(strAllTopGuid))
            {
                strAllTopGuid = strAllTopGuid.Substring(0, strAllTopGuid.Length - 1);
                strAllGuidTemp = strAllGuidTemp.Substring(0, strAllGuidTemp.Length - 1);
                if (!string.IsNullOrEmpty(strAllGuid))
                {
                    sql += " or  ";
                }
                else
                {
                    sql += " and ";
                }
                sql += " Guid in (" + strAllGuidTemp + "," + strAllTopGuid + " ) ";
            }
            if (!string.IsNullOrEmpty(strAllGuid) || !string.IsNullOrEmpty(strAllTopGuid))
            {
                dtAll = db.ExecuteDataSet(sql).Tables[0];
            }


            sb.Append("<table class='table' cellpadding='0' cellspacing='0'><tr>");
            sb.Append("<td class='TdContent' colspan='4' style='font-weight:bolder;height: 30px;'>项目总数：" + dtAll.Select("(ProjAffiliation is null and TopGuid is not null and TopGuid not in (" + guid + "))or(ProjAffiliation in('CC4B768A-2A2E-4EA0-B3EA-1C6E2765C221','FA173C37-BF98-4AAD-9997-817835165E29')and ProjAffiliation is not null and TopGuid is null)").Count() + "</td></tr>");
            sb.Append("<tr><td class='TdContent' style='width:25%'>主项目：" + dtAll.Select("TopGuid is null and ProjAffiliation is not null and ProjAffiliation='FA173C37-BF98-4AAD-9997-817835165E29'").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>子项目：" + dtAll.Select("TopGuid is not null and ProjAffiliation is  null and TopGuid not in (" + guid + ")").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>无子项目：" + dtAll.Select("TopGuid is null and ProjAffiliation is not null and ProjAffiliation='CC4B768A-2A2E-4EA0-B3EA-1C6E2765C221'").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'></td></tr>");
            //项目总数
            DataTable dt = new DataTable();
            if (dtAll.Rows.Count > 0)
            {
                DataRow[] dr = dtAll.Select("(ProjAffiliation  is null and TopGuid is not null and TopGuid not in (" + guid + "))or(ProjAffiliation in('CC4B768A-2A2E-4EA0-B3EA-1C6E2765C221','FA173C37-BF98-4AAD-9997-817835165E29')and ProjAffiliation is not null and TopGuid is null)");
                if (dr.Length > 0)
                {
                    dt = dr.CopyToDataTable();
                }
                else
                {
                    dt = dtAll;
                }
            }
            else
            {
                dt = dtAll;
            }
            sb.Append("<tr><td class='TdContent' style='width:25%'>市财力：" + dt.Select("InvestType= '41DA1FBE-7970-42E8-8F16-EC25BB55B4A9'").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>中央投资：" + dt.Select("InvestType= '8E4AEDEA-8EEF-469D-980B-97F9275C6507'").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>省级投资：" + dt.Select("InvestType= 'E0174390-C8B6-47FC-94B8-6F33B3AD48D8'").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>专项资金：" + dt.Select("InvestType= '5181B945-3482-4E1F-B787-3FF570899238'").Count() + "</td></tr>");

            sb.Append("<tr><td class='TdContent' style='width:25%'>全审批项目：" + dt.Select("NationType='6CDCC6AA-ED28-4616-96D9-6E39229BB956'").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>只审批初步设计项目：" + dt.Select("NationType='9D3BEE82-53D9-45E0-8C11-651BE5E754AC'").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>核准项目：" + dt.Select("NationType='A1816C6C-4433-4123-B7A4-EA166D2649A0'").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>备案项目：" + dt.Select("NationType='F8913E85-1F23-4E9B-BF6E-594B9E070CA5'").Count() + "</td></tr>");

            sb.Append("<tr><td class='TdContent' style='width:25%'>正常考核项目：" + dt.Select("Status IN (0,1)").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>终止项目：" + dt.Select("Status =5").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>缓建项目：" + dt.Select("Status =7").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>撤销项目：" + dt.Select(" Status =6").Count() + "</td></tr>");

            StringBuilder sbTemp = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sbTemp.Append("'");
                sbTemp.Append(dt.Rows[i]["guid"].ToString());
                sbTemp.Append("',");
            }
            temp = sbTemp.ToString();
            if (!string.IsNullOrEmpty(temp))
            {
                guid = temp.Substring(0, temp.Length - 1);
            }
            else
            {
                guid = "''";
            }
            sbTemp.Clear();
            //里程碑视图（mark为100,200的状态）
            DataTable dtMilestone = db.ExecuteDataSet(" select * from View_Busi_Milestone where projguid in (" + guid + ") ").Tables[0];
            sb.Append("<tr><td class='TdContent' style='width:25%'>前期审批项目：" + dtMilestone.Select(" ProjGuid in (" + guid + ") AND HandlingDate IS NULL AND HandlingDateEnd IS NULL ").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>已开工项目：" + dtMilestone.Select(" ProjGuid in (" + guid + ") AND HandlingDate IS NOT NULL AND HandlingDateEnd IS NULL ").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>已竣工项目：" + dtMilestone.Select(" ProjGuid in (" + guid + ") AND HandlingDateEnd IS NOT NULL").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>已封存项目：" + dtAll.Select(" ProjSealed=1  AND MiestoneEndTime is not null ").Count() + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'></td></tr>");

            sb.Append("<td class='TdContent' colspan='4' style='font-weight:bolder;height: 30px;'>项目总投资：" + GetMoney(dt.Compute("Sum(ApprovalInvest)", null)) + "</td></tr>");
            sb.Append("<tr><td class='TdContent' style='width:25%'>资金到位金额：" + GetMoney(dt.Compute("Sum(PlaceAmount)", null)) + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>资金支付金额：" + GetMoney(dt.Compute("Sum(PaymentAmount)", null)) + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'>投资完成金额：" + GetMoney(dt.Compute("Sum(CompletedAmount)", null)) + "</td>");
            sb.Append("<td class='TdContent' style='width:25%'></td></tr>");

            Datas = Datas.TrimEnd(',');
            Datas += "]";
            return sb.ToString();
        }

        private static string GetMoney(object Money)
        {
            try
            {
                double money = Convert.ToDouble(Money);
                if ((money / 10000) >= 1)
                {
                    return (money / 10000).ToString("0.00") + "&nbsp;&nbsp;亿";
                }
                else
                    return Convert.ToDouble(Money).ToString("0.00") + "&nbsp;&nbsp;万元";
            }
            catch
            {
                return "0.00&nbsp;&nbsp;万元";
            }
        }
    }
}
