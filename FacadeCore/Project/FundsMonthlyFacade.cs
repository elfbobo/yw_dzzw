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
    /// 到位月报
    /// </summary>
    public class FundsMonthlyFacade
    {
        /// <summary>
        /// 获取预算文号
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetPayNum(string projGuid, string type)
        {
            FundsMonthly fundsMonthly = new FundsMonthly();
            return fundsMonthly.GetPayNum(projGuid, type);
        }
    }
}
