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
    /// 通用方法
    /// </summary>
    public static class GEClass
    {
        /// <summary>
        /// 手续同步金额和资金来源
        /// </summary>
        /// <param name="JudgeTable1">查询判断手续表</param>
        /// <param name="JudgeTable2">查询判断投资表</param>
        /// <param name="Field1">Table1金额字段</param>
        /// <param name="Field2">Table2金额字段</param>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="DataSourceTable">资金来源手续表标识</param>
        /// <param name="DataAimTable">资金来源投资表标识</param>
        /// <returns>新数据主键</returns>
        public static string UpdateRelateTable(string JudgeTable1, string JudgeTable2, string Field1, string Field2, string ProjGuid, string DataSourceTable, string DataAimTable)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select {3} from {0} where ProjGuid='{1}' and SysStatus<>-1;select {4} from {2} where ProjGuid='{1}' and SysStatus<>-1;select * from Busi_InvestmentSource where ProjGuid='{1}' and InfoType='{5}' and SysStatus<>-1", JudgeTable1, ProjGuid, JudgeTable2, Field1, Field2, DataSourceTable));
            string result = "";
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count == 0)
            {
                if (ds.Tables[0].Rows[0][Field1] != DBNull.Value)
                {
                    result = Guid.NewGuid().ToString();
                    string sql = string.Format("insert into {0} ([Guid],[ProjGuid],{1},[CreateDate],[Status],[SysStatus]) values ('{2}','{3}','{4}','{5}',0,0)", JudgeTable2, Field2, result.ToString(), ProjGuid, ds.Tables[0].Rows[0][Field1], DateTime.Now);
                    if (ds.Tables[2].Rows.Count > 0)
                        sql += string.Format("insert into Busi_InvestmentSource ([Guid],[ProjGuid],[CentreFund],[ProvinceFund],[CityFund],[PlaceFund],[TownFund],[BusinessFund],[BankFund],[OtherFund],[SysStatus],[InfoType],[test]) select [Guid]='{3}',[ProjGuid],[CentreFund],[ProvinceFund],[CityFund],[PlaceFund],[TownFund],[BusinessFund],[BankFund],[OtherFund],[SysStatus],'{0}',[test] from Busi_InvestmentSource where ProjGuid='{1}' and InfoType='{2}' and SysStatus<>-1;", DataAimTable, ProjGuid, DataSourceTable, result);
                    db.ExecuteNonQuery(sql);
                }
            }
            return result;
        }
    }
}
