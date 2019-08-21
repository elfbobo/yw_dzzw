using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore
{
    /// <summary>
    /// 首页显示统计信息
    /// </summary>
    public static class IndexGetStatisticsFacade
    {
        /// <summary>
        /// 根据查询条件统计
        /// </summary>
        /// <param name="condtion">查询调教</param>
        /// <param name="Datas">返回统计图数据</param>
        /// <returns>结果集</returns>
        public static string GetIndexStatisticsHtml(string condtion, out string Datas)
        {
            return IndexGetStatistics.GetIndexStatisticsHtml(condtion, out Datas);
        }
    }
}
