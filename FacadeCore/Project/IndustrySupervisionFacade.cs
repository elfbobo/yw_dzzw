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
    /// 行业监管部门责任体系
    /// </summary>
    public static class IndustrySupervisionFacade
    {
        /// <summary>
        /// 获取行业部门
        /// </summary>
        /// <param name="DepartmentGuid"></param>
        /// <returns></returns>
        public static DataSet GetIndustrySupervisionDepartment(string DepartmentGuid) 
        {
            return IndustrySupervision.GetIndustrySupervisionDepartment(DepartmentGuid);
        }

        /// <summary>
        /// 获取行业部门的分管处室
        /// </summary>
        /// <param name="DepartmentGuid"></param>
        /// <returns></returns>
        public static DataSet GetIndustrySupervisionOffice(string DepartmentGuid)
        {
            return IndustrySupervision.GetIndustrySupervisionOffice(DepartmentGuid);
        }
    }
}
