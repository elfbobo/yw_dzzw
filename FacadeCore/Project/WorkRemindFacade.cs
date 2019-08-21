using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;
using Yawei.DataAccess;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 项目催报
    /// </summary>
    public static class WorkRemindFacade
    {
        /// <summary>
        /// 是否显示催报信息
        /// </summary>
        /// <param name="RemindGuid">催报主键</param>
        /// <param name="UserGuid">用户主键</param>
        /// <param name="html">页面显示</param>
        /// <returns>返回bool</returns>
        public static bool GetIsShowRemind(out string RemindGuid, string UserGuid, out string html)
        {
            html = "";
            RemindGuid = "";
            DataTable Remind = WorkRemind.GetThiMonthRemind(out RemindGuid);
            if (Remind.Rows.Count > 0)
            {
                DateTime comp = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-01 00:00:00");
                DataTable RemindStatus = WorkRemind.GetRemindStatus(Remind.Rows[0]["Guid"].ToString(), UserGuid);
                if (RemindStatus.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(RemindStatus.Rows[0]["OptionDate"]) > comp)
                        return false;
                    else
                        html = GetHtml(Remind);
                    return true;
                }
                else
                    html = GetHtml(Remind);
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// 返回当月催报
        /// </summary>
        /// <returns>催报集合</returns>
        public static DataTable GetThiMonthRemind(out string RemindGuid)
        {
            return WorkRemind.GetThiMonthRemind(out RemindGuid);
        }

        /// <summary>
        /// 更新催报状态
        /// </summary>
        /// <param name="RemindGuid">催报主键</param>
        /// <param name="UserGuid">用户主键</param>
        /// <returns>影响行数</returns>
        public static int UpdateRemindStatus(string RemindGuid, string UserGuid)
        {
            if (WorkRemind.UpdateRemindSysStatus(RemindGuid) > 0)
                return WorkRemind.UpdateRemindStatus(RemindGuid, UserGuid);
            else
                return 0;
        }

        /// <summary>
        /// 更新催报已读
        /// </summary>
        /// <param name="RemindGuid">催报主键</param>
        /// <returns>影响行数</returns>
        public static int UpdateRemindSysStatus(string RemindGuid)
        {
            return WorkRemind.UpdateRemindSysStatus(RemindGuid);
        }

        private static string GetHtml(DataTable Remind)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string html = string.Empty;
            if (Remind.Rows.Count > 0)
            {
                if (Remind.Rows[0]["Title"].ToString().Length > 30)
                    html += "<div class='adtitle'>" + Remind.Rows[0]["Title"].ToString().Substring(0, 30) + "...</div>";
                else
                    html += "<div class='adtitle'>" + Remind.Rows[0]["Title"].ToString() + "</div>";
                if (Remind.Rows[0]["Content"] == DBNull.Value || Remind.Rows[0]["Content"].ToString() == "")
                {
                    DataTable dt = db.ExecuteDataSet(string.Format("select OrginFileName from Sys_FileInfo where refguid='{0}' and filesign='Busi_ProjWorkRemind'", Remind.Rows[0]["Guid"].ToString())).Tables[0];
                    html += "<div class='adcontent'>";
                    foreach (DataRow dr in dt.Select())
                    {
                        html += "&nbsp;&nbsp;&nbsp;&nbsp;附件：" + dr["OrginFileName"].ToString() + "<br/>";
                    }
                    html += "</div>";
                }
                else if (Remind.Rows[0]["Content"].ToString().Length > 130)
                {
                    html += "<div class='adcontent'>&nbsp;&nbsp;&nbsp;&nbsp;" + Remind.Rows[0]["Content"].ToString().Substring(0, 130) + "...</div>";
                }
                else
                {
                    html += "<div class='adcontent'>&nbsp;&nbsp;&nbsp;&nbsp;" + Remind.Rows[0]["Content"].ToString() + "</div>";
                }
            }
            return html;
        }
    }
}
