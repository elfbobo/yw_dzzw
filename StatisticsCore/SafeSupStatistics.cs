using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.StatisticsCore
{
    public class SafeSupStatistics
    {
        Database db = DatabaseFactory.CreateDatabase();

        /// <summary>
        /// 根据sql语句获取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSetBySql(string sql)
        {
            DataSet doc = db.ExecuteDataSet(sql);
            return doc;
        }

        /// <summary>
        /// 根据sql语句获取object
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object GetObjBySql(string sql)
        {
            object obj = db.ExecuteScalar(sql);
            return obj;
        }
    }
}
