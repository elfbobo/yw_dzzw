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
    /// 图片滚动页面
    /// </summary>
    public static class ImagesShow
    {
        /// <summary>
        /// 获取年以及对应月份
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetYears(string ProjGuid)
        {
            //Year @ Month , Month & Year @ Month , Month , Month
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from Busi_ConstructMonthReport where ProjGuid='{0}';select distinct ReportYear from Busi_ConstructMonthReport where ProjGuid='{0}';", ProjGuid));
            DataTable ConstructMonthReport = ds.Tables[0];
            DataTable Years = ds.Tables[1];
            string result = string.Empty;
            foreach (DataRow dr in Years.Rows)
            {
                result += dr["ReportYear"] + "@";
                foreach (DataRow drs in ConstructMonthReport.Select("ReportYear='" + dr["ReportYear"] + "'"))
                {
                    result += drs["ReportMonth"] + ",";
                }
                result = result.TrimEnd(',') + "&";
            }
            result = result.TrimEnd('&');
            return result;
        }

        /// <summary>
        /// 获取图片数量
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static int GetImagesCount(string ProjGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int Count = Convert.ToInt32(db.ExecuteScalar(string.Format("select Count(*) from View_FileUpData where RefGuid in (select Guid from Busi_ConstructMonthReport where ProjGuid='{0}') and [ExtName] in ('.png','.gif','.bmp','.jpeg','.jpg')", ProjGuid)));
            return Count;
        }

        /// <summary>
        /// 获取带条件图片数量
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static int GetImagesCount(string ProjGuid, string Condition)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int Count = Convert.ToInt32(db.ExecuteScalar(string.Format("select Count(*) from View_FileUpData where RefGuid in (select Guid from Busi_ConstructMonthReport where ProjGuid='{0}' and {1}) and [ExtName] in ('.png','.gif','.bmp','.jpeg','.jpg')", ProjGuid, Condition)));
            return Count;
        }

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="PageNum"></param>
        /// <returns></returns>
        public static DataSet GetImagesContent(string ProjGuid, int PageNum)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from (select row_number() over (order by UploadDate desc) as rowNum,* from View_FileUpData where RefGuid in (select Guid from Busi_ConstructMonthReport where ProjGuid='{0}')) result where rowNum={1} and [ExtName] in ('.png','.gif','.bmp','.jpeg','.jpg')", ProjGuid, PageNum));
            return ds;
        }

        /// <summary>
        /// 获取带条件的图片信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="Condition"></param>
        /// <param name="PageNum"></param>
        /// <returns></returns>
        public static DataSet GetImagesContent(string ProjGuid, string Condition, int PageNum)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from (select row_number() over (order by UploadDate desc) as rowNum,* from View_FileUpData where RefGuid in (select Guid from Busi_ConstructMonthReport where ProjGuid='{0}' and {1})) result where rowNum={2} and [ExtName] in ('.png','.gif','.bmp','.jpeg','.jpg')", ProjGuid, Condition, PageNum));
            return ds;
        }
    }
}
