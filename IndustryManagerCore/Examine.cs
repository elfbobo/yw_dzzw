using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.IndustryManagerCore
{
    /// <summary>
    /// 考核
    /// </summary>
    public class Examine
    {
        Database db = DatabaseFactory.CreateDatabase();
        string sql = "";

        /// <summary>
        /// 得到DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            return db.ExecuteDataSet(cmd);
        }

    }
}
