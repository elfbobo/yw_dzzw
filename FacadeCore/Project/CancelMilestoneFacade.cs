using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 取消里程碑
    /// </summary>
    public class CancelMilestoneFacade
    {
        /// <summary>
        /// 根据里程碑Guid取消里程碑
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public int UpdaateCancelMilestoneByGuid(string guid)
        {
            CancelMilestone cancelMilestone = new CancelMilestone();
            return cancelMilestone.UpdateCancelMilestoneByGuid(guid);
        }
    }
}
