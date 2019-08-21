using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;
using System.Web;
using Yawei.FacadeCore.Support;
using System.Data;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 项目管理公共类表现层
    /// </summary>
    public static class FormatProjectFacade
    {
        /// <summary>
        /// 根据项目主键获取项目名称
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static string GetProjNameByGuid(string projGuid)
        {
            return FormatProject.GetProjNameByGuid(projGuid);
        }
        /// <summary>
        /// 根据项目主键获取项目名称
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetProjDicByGuid(string projGuid)
        {
            return FormatProject.GetProjDicByGuid(projGuid);
        }
        /// <summary>
        /// 判断该表中是否存在该项目
        /// </summary>
        /// <param name="table"></param>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static bool GetIsExistProject(string table, string projGuid)
        {
            return FormatProject.IsExistProject(table, projGuid);
        }

        /// <summary>
        /// 项目信息审核退回
        /// </summary>
        /// <param name="table">表名称</param>
        /// <param name="status">审核状态 1 审核 2 退回</param>
        /// <param name="refGuid">数据主键</param>
        /// <param name="projGuid">项目主键</param>
        /// <param name="remark">意见</param>
        /// <param name="url">当前地址</param>
        /// <returns></returns>
        public static void InfoAction(string table, string status, string refGuid, string projGuid, string remark, string url)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["CurrentUserInfo"];
            DataSet ds = null;
            if (cookie != null)
            {
                LogFacade log = new LogFacade();
                ds = log.OperatorLog(table, refGuid, refGuid, status == "1" ? "审核" : "退回",HttpContext.Current.Server.UrlDecode( cookie["CurrentUserGuid"]), HttpContext.Current.Server.UrlDecode(cookie["CurrentUserDN"]),HttpContext.Current.Server.UrlDecode( cookie["CurrentUserCN"]),projGuid);
            }
            FormatProject.InfoAction(table, status, refGuid, projGuid, remark, ds, url);
        }

        /// <summary>
        /// 取消审核功能
        /// </summary>
        /// <param name="table">表名称</param>
        /// <param name="refGuid">数据主键</param>
        /// <param name="projGuid">项目主键</param>
        /// <param name="url">当前url</param>
        public static void CannelConfirm(string table, string refGuid, string projGuid, string url)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["CurrentUserInfo"];
            DataSet ds = null;
            if (cookie != null)
            {
                LogFacade log = new LogFacade();
                ds = log.OperatorLog(table, refGuid, refGuid, "取消审核", HttpContext.Current.Server.UrlDecode(cookie["CurrentUserGuid"]), HttpContext.Current.Server.UrlDecode(cookie["CurrentUserDN"]), HttpContext.Current.Server.UrlDecode(cookie["CurrentUserCN"]), projGuid);
            }
            FormatProject.CannelConfirm(table, refGuid, projGuid, url, ds);
        }

        /// <summary>
        /// 获取审核意见
        /// </summary>
        /// <param name="guid">数据主键</param>
        /// <param name="table">表名</param>
        /// <param name="projGuid">项目主键</param>
        /// <returns>json格式的意见</returns>
        public static string GetProjConfirmInfo(string guid, string table, string projGuid)
        {
            return FormatProject.GetProjConfirmInfo(guid, table, projGuid);
        }
    }
}
