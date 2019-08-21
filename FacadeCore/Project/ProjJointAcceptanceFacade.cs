using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 联合验收
    /// </summary>
    public class ProjJointAcceptanceFacade
    {
        ProjJointAcceptance projJointAcceptance = new ProjJointAcceptance();
        /// <summary>
        /// 根据项目guid获取guid
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public String GetGuidByProjGuid(string projGuid, string type)
        {
            return projJointAcceptance.GetGuidByProjGuid(projGuid, type);
        }
    }
}
