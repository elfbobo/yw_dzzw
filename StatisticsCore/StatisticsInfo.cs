using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;
namespace Yawei.StatisticsCore
{
    /// <summary>
    /// 信息统计
    /// </summary>
   public class StatisticsInfo
    {
        Database db = DatabaseFactory.CreateDatabase();

        /// <summary>
        /// 获取年度和项目数量
        /// </summary>
        /// <returns></returns>
        public DataSet GetProStatics()
        {
            string sql = "select ApprovalYear,count(ProjName) as count from Sta_ProjRegister_BaseInfo where SysStatus!=-1 group by ApprovalYear";
            DataSet ds = db.ExecuteDataSet(sql);
            return ds;
        }
        /// <summary>
        /// 获取文件类型和大小
        /// </summary>
        /// <returns></returns>
        public DataSet GetFileStatics()
        {
            string sql = "select ExtName, sum(FileSize/1024.0/1024.0) as count from Sys_FileInfo group by ExtName";
            DataSet ds = db.ExecuteDataSet(sql);
            return ds;
        }
       /// <summary>
       /// 获取项目进度和数量
       /// </summary>
       /// <returns></returns>
        public DataSet GetProjProgressStatics()
        {
            string sql = "select Name ,COUNT(Name) as count from Sta_Milestone_Map group by Name";
            DataSet ds = db.ExecuteDataSet(sql);
            return ds;
        }
        /// <summary>
        /// 获取概算数量
        /// </summary>
        /// <returns></returns>
        public DataSet GetEstimateStatics()
        {
            string sql = "select a.count1,b.count2,c.count3 from (select COUNT(distinct ProjGuid)as count1 from  Busi_Con_EstimateDetail)a,(select COUNT(distinct PROJCODE) as count2 from tb_temp_KJFF where PROJCODE not in(select distinct ProjGuid from  Busi_Con_EstimateDetail ))b,(select COUNT(distinct Guid) as count3 from Busi_ProjRegister where Busi_ProjRegister.guid not in( select PROJCODE from  tb_temp_KJFF) and Busi_ProjRegister.guid not in(select ProjGuid from  Busi_Con_EstimateDetail) and Busi_ProjRegister.SysStatus!=-1)c  ";
            DataSet ds = db.ExecuteDataSet(sql);
            return ds;
        }
    }
}
