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
    /// 项目封存
    /// </summary>
    public class SealedOpinionProjGuidFacade
    {
        SealedOpinionProjGuid sealedOpinionProjGuid = new SealedOpinionProjGuid();
        /// <summary>
        /// 根据项目Guid查询项目是否已申请封存
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public string IsExistByProjGuid(string projGuid)
        {
            return sealedOpinionProjGuid.IsExistByProjGuid(projGuid);
        }
        /// <summary>
        /// 获取发改回复的验收封存信息
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public DataSet GetSealedOpinion(string projGuid)
        {
            return sealedOpinionProjGuid.GetSealedOpinion(projGuid);
        }
    }
}
