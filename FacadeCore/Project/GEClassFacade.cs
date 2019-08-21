using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 通用方法
    /// </summary>
    public static class GEClassFacade
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
            return GEClass.UpdateRelateTable(JudgeTable1, JudgeTable2, Field1, Field2, ProjGuid, DataSourceTable, DataAimTable);
        }
    }
}
