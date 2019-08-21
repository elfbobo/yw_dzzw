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
    /// 
    /// </summary>
    public class ShowConfrimMileStoneFacadeCore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public DataSet ShowConfrim(string guid)
        {
            ShowConfrimMileStone showConfrimMileStone = new ShowConfrimMileStone();
            return showConfrimMileStone.ShowConfrim(guid);
        }
    }
}
