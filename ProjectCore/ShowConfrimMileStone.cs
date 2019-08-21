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
    /// 
    /// </summary>
    public class ShowConfrimMileStone
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public DataSet ShowConfrim(string guid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "select * from [Busi_ProjConfirmList] where  RefGuid='" + guid + "'";
            DataSet dt = db.ExecuteDataSet(sql);

            return dt;
        }
    }
}
