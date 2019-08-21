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
    /// 联合验收
    /// </summary>
    public class ProjJointAcceptance
    {
        Database db = DatabaseFactory.CreateDatabase();
        /// <summary>
        /// 根据项目Guid获取Guid
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public String GetGuidByProjGuid(string projGuid, string type)
        {
            DataSet ds = db.ExecuteDataSet("select * from Busi_Con_ProjJointAcceptance where type='" + type + "' and ProjGuid ='" + projGuid + "'");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Guid"].ToString();
            }
            return "";
        }
    }
}
