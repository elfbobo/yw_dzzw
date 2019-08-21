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
    /// 项目封存
    /// </summary>
    public class SealedOpinionProjGuid
    {
        Database db = DatabaseFactory.CreateDatabase();
        /// <summary>
        /// 根据项目Guid查询项目是否已申请封存
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public string IsExistByProjGuid(string projGuid)
        {
            string sql = "select guid from Busi_con_ProjSealedOpinion where ProjGuid='" + projGuid + "'";
            return db.ExecuteScalar(sql) == null ? "" : db.ExecuteScalar(sql).ToString();
        }

        /// <summary>
        /// 获取发改回复的验收封存信息
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public DataSet GetSealedOpinion(string projGuid)
        {
            string sql = "SELECT TOP 1 * FROM Busi_con_ProjSealedOpinion WHERE ProjGuid='" + projGuid + "' AND SysStatus<>-1 AND Status<>0 ORDER BY CreateDate";
            DataSet ds = db.ExecuteDataSet(sql);
            return ds;
        }
    }
}
