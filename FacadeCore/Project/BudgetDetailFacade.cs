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
    /// 预算明细
    /// </summary>
   public  class BudgetDetailFacade
    {
        /// <summary>
        /// 根据项目Guid获取预算信息
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="HasGuid"></param>
        /// <returns></returns>
        public String getData(String projGuid, Boolean HasGuid)
        {
            BudgetDetailCore bd = new BudgetDetailCore();
            DataSet ds = bd.getData(projGuid);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return "";
            }
            int i = 0;
            String json = "";
            if (HasGuid)
            {
                foreach (DataRow dr in ds.Tables[0].Select(" (TopGuid is null or TopGuid='')"))
                {
                    i++;
                    if (json != "")
                    {
                        json += ",";
                    }
                    json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',SubmitAccount:'" + dr["SubmitAccount"] + "',AuditingAccount:'" + dr["AuditingAccount"] + "',AuthorizeAccount:'" + dr["AuthorizeAccount"] + "',TrialReductionAccount:'" + dr["TrialReductionAccount"] + "'";
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
                foreach (DataRow dr in ds.Tables[0].Select(" (TopGuid is null or TopGuid='')"))
                {
                    i++;
                    if (json != "")
                    {
                        json += ",";
                    }
                    json += "{ Guid:'" + dr["Guid"] + "', code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',SubmitAccount:'" + dr["SubmitAccount"] + "',AuditingAccount:'" + dr["AuditingAccount"] + "',AuthorizeAccount:'" + dr["AuthorizeAccount"] + "',TrialReductionAccount:'" + dr["TrialReductionAccount"] + "'";
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
        /// 递归给预算明细编号
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
                    nowStr += "{Guid:'" + dr["Guid"] + "',code:'" + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',SubmitAccount:'" + dr["SubmitAccount"] + "',AuditingAccount:'" + dr["AuditingAccount"] + "',AuthorizeAccount:'" + dr["AuthorizeAccount"] + "',TrialReductionAccount:'" + dr["TrialReductionAccount"] + "'";
                    if (ds.Tables[0].Select("topGuid = '" + dr["Guid"] + "'").Length > 0)
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
                    nowStr += "{  code:'" + code + "." + i + "',ProjOrCostName:'" + dr["ProjOrCostName"] + "',SubmitAccount:'" + dr["SubmitAccount"] + "',AuditingAccount:'" + dr["AuditingAccount"] + "',AuthorizeAccount:'" + dr["AuthorizeAccount"] + "',TrialReductionAccount:'" + dr["TrialReductionAccount"] + "'";
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

    }
}
