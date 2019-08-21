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
    /// 各单位检查信息
    /// </summary>
    public static class UnitsExamineFacade
    {
        /// <summary>
        /// 通过动态行主键更新整改结果及时间
        /// </summary>
        /// <param name="Guid">动态行主键</param>
        /// <param name="Results">整改结果</param>
        /// <param name="RectTime">整改时间</param>
        /// <returns>影响行数</returns>
        public static int UpdateResultByGuid(string Guid, string Results, string RectTime)
        {
            return UnitsExamine.UpdateResultByGuid(Guid, Results, RectTime);
        }

        /// <summary>
        /// 插入一个问题的整改结果信息
        /// </summary>
        /// <param name="RefGuid"></param>
        /// <param name="Results"></param>
        /// <param name="RectTime"></param>
        /// <param name="DoneSituation"></param>
        /// <returns></returns>
        public static int InsertResult(string RefGuid, string Results, string RectTime, string DoneSituation)
        {
            return UnitsExamine.InsertResult(RefGuid, Results, RectTime, DoneSituation);
        }

        /// <summary>
        /// 获取某一问题的所有整改结果信息
        /// </summary>
        /// <param name="RefGuid"></param>
        /// <returns></returns>
        public static DataSet GetIssueResults(string RefGuid)
        {
            return UnitsExamine.GetIssueResults(RefGuid);
        }

    }
}
