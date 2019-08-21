using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore
{
    /// <summary>
    /// 图片滚动页面
    /// </summary>
    public static class ImagesShowFacade
    {
        /// <summary>
        /// 获取年以及对应月份
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetYears(string ProjGuid)
        {
            return ImagesShow.GetYears(ProjGuid);
        }

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="PageNum"></param>
        /// <returns></returns>
        public static DataSet GetImagesContent(string ProjGuid, int PageNum)
        {
            return ImagesShow.GetImagesContent(ProjGuid, PageNum);
        }

        /// <summary>
        /// 获取带条件的图片信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="Condition"></param>
        /// <param name="PageNum"></param>
        /// <returns></returns>
        public static DataSet GetImagesContent(string ProjGuid, string Condition,int PageNum)
        {
            return ImagesShow.GetImagesContent(ProjGuid, Condition, PageNum);
        }

        /// <summary>
        /// 获取图片数量
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static int GetImagesCount(string ProjGuid)
        {
            return ImagesShow.GetImagesCount(ProjGuid);
        }

        /// <summary>
        /// 获取带条件图片数量
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static int GetImagesCount(string ProjGuid, string Condition)
        {
            return ImagesShow.GetImagesCount(ProjGuid, Condition);
        }
    }
}
