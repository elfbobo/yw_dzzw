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
    /// 培训登记
    /// </summary>
    public static class TrainingFacade
    {
        /// <summary>
        /// 通过项目名称集合获取项目主键，名字集合
        /// </summary>
        /// <param name="ProjName">项目名称集合</param>
        /// <returns>主键，名字DataTable</returns>
        public static DataTable GetDataTableByProjGuids(string ProjName)
        {
            return Training.GetDataTableByProjGuids(ProjName);
        }

        /// <summary>
        /// 根据主键获取分管处室
        /// </summary>
        /// <param name="OfficeGuid"></param>
        /// <returns></returns>
        public static string GetSupOfficeByGuid(string OfficeGuid)
        {
            object obj = Training.GetSupOfficeByGuid(OfficeGuid);
            if (obj != null && obj.ToString() != "")
                return obj.ToString();
            else
                return "";
        }

        /// <summary>
        /// 根据部门名称获取分管处室
        /// </summary>
        /// <param name="UserTitle"></param>
        /// <returns></returns>
        public static DataSet GetSupOffice(string UserTitle)
        {
            return Training.GetSupOffice(UserTitle);
        }
    }
}
