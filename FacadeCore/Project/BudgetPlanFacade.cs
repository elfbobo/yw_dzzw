using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 预算下达计划业务对象
    /// </summary>
    public class BudgetPlanFacade
    {
        BudgetPlan bp = new BudgetPlan();
        /// <summary>
        /// 根据项目Guid 获取项目名称
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public string getProjNameByGuid(string projGuid)
        {
            return bp.getProjNameByGuid(projGuid);
        }
    }
}
