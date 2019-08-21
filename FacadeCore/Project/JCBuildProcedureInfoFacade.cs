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
    /// 机场项目办理计划
    /// </summary>
    public static class JCBuildProcedureInfoFacade
    {
         /// <summary>
        /// 根据投资类型和审批类型获取相对应的主要审批手续
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static DataSet GetMainByType( string ProjGuid)
        {
            return JCBuildProcedureInfo.GetMainByType(ProjGuid);
        }

        /// <summary>
        /// 主项目的主从关系是否是集中立项可研，子项目独立批复概算，显示的子手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetJzOther(string ProjGuid)
        {
            return JCBuildProcedureInfo.GetJzOther(ProjGuid);
        }

        /// <summary>
        /// 子项目的子手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetZother(string ProjGuid)
        {
            return JCBuildProcedureInfo.GetZother(ProjGuid);
        }

        /// <summary>
        /// 获取机场项目的标记
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetProjType(string ProjGuid)
        {
            return JCBuildProcedureInfo.GetProjType(ProjGuid);
        }
    }
}
