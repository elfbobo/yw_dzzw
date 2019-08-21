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
    public class GetConfirmFacade
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="remark"></param>
        /// <param name="sign"></param>
        /// <param name="confirmStatus"></param>
        /// <returns></returns>
        public DataSet GetConfirmInfo(string projGuid, string remark, string sign, string confirmStatus)
        {
            GetConfirm getConfirm = new GetConfirm();
            return getConfirm.GetConfirmInfo(projGuid, remark, sign, confirmStatus);
        }
    }
}
