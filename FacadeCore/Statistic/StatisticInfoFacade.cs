using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.StatisticsCore;
using System.Data;

namespace Yawei.FacadeCore.Statistic
{
    /// <summary>
    /// 信息统计
    /// </summary>
  public class StatisticInfoFacade
    {
        StatisticsInfo baseDB = new StatisticsInfo();

        /// <summary>
        /// 获取年度和项目数量
        /// </summary>
        /// <returns></returns>
        public DataSet GetProStatics()
        {
            return baseDB.GetProStatics();
        }
      /// <summary>
      /// 获取文件类型和大小
      /// </summary>
      /// <returns></returns>
      public DataSet GetFileStatics()
      {
          return baseDB.GetFileStatics();
      }
      /// <summary>
      /// 获取项目进度和数量
      /// </summary>
      /// <returns></returns>
      public DataSet GetProjProgressStatics()
      {
          return baseDB.GetProjProgressStatics();
      }
      /// <summary>
      /// 获取概算数量
      /// </summary>
      /// <returns></returns>
      public DataSet GetEstimateStatics()
      {
          return baseDB.GetEstimateStatics();
      }
    }
}
