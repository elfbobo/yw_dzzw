using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.StatisticsCore;

namespace Yawei.FacadeCore
{
    /// <summary>
    /// 安全监督
    /// </summary>
    public class SafeSupFacade
    {
        SafeSupStatistics safesup = new SafeSupStatistics();

        /// <summary>
        /// 根据sql语句获取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSetBySql(string sql)
        {
            return safesup.GetDataSetBySql(sql);
        }

        /// <summary>
        /// 根据sql语句获取object
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object GetObjBySql(string sql)
        {
            return safesup.GetObjBySql(sql);
        }
    }
}
