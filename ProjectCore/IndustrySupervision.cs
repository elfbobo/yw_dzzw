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
    /// 行业监管部门责任体系
    /// </summary>
    public static  class IndustrySupervision
    {
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// 获取行业部门
        /// </summary>
        /// <param name="DepartmentGuid"></param>
        /// <returns></returns>
        public static DataSet GetIndustrySupervisionDepartment(string DepartmentGuid) 
        {
            string sql = "select * from Busi_IndustrySupervisionDepartment where Guid='" + DepartmentGuid + "'";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            return ds;
        }

        /// <summary>
        /// 获取行业部门的分管处室
        /// </summary>
        /// <param name="DepartmentGuid"></param>
        /// <returns></returns>
        public static DataSet GetIndustrySupervisionOffice(string DepartmentGuid)
        {
            string sql = "select * from Busi_IndustrySupervisionOffice where DepartGuid='" + DepartmentGuid + "'";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            return ds;
        }
    }
}
