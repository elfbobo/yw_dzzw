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
    /// 参建人员
    /// </summary>
    public static class CooperatedBuildPersonFacade
    {
        /// <summary>
        /// 根据项目主键返回参建单位
        /// </summary>
        /// <param name="projGuid">项目主键</param>
        /// <param name="uName">参建单位名字查询条件</param>
        /// <returns></returns>
        public static DataTable GetBuilderUnitsByProjGuid(string projGuid, string uName)
        {
            return CooperatedBuildPersonnel.GetBuilderUnitsByProjGuid(projGuid, uName);
        }
        
        /// <summary>
        /// 根据参建单位主键查询参建单位名称
        /// </summary>
        /// <param name="Guids">参建单位主键</param>
        /// <returns>参建单位名称</returns>
        public static string GetBuilderUnitsNameByGuid(string Guids)
        {
            return CooperatedBuildPersonnel.GetBuilderUnitsNameByGuid(Guids);
        }

        /// <summary>
        /// 根据参建单位主键查询参建单位名称
        /// </summary>
        /// <param name="Guids">参建单位主键</param>
        /// <param name="Names">名称集合</param>
        public static void GetBuilderUnitsNameByGuid(string Guids,out string Names)
        {
            CooperatedBuildPersonnel.GetBuilderUnitsNameByGuid(Guids,out Names);
        }

        /// <summary>
        /// 根据工商注册号去企业库中检查是否存在，存在返回中介机构信息的主键
        /// </summary>
        /// <param name="GSZCH">工商注册号</param>
        /// <returns></returns>
        public static string CheckGszchEqual(string GSZCH)
        {
            return CooperatedBuildPersonnel.CheckGszchEqual(GSZCH);
        }
    }
}
