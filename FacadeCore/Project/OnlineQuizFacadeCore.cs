using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;
using Yawei.ProjectCore;
using Yawei.SupportCore.SupportApi;
using Yawei.SupportCore.SupportApi.DBContext;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 在线问答
    /// </summary>
    public static class OnlineQuizFacadeCore
    {

        /// <summary>
        /// 提交问题
        /// </summary>
        /// <param name="AnswerGuid">回答问题主键</param>
        /// <param name="QuerstionContent">提问内容</param>
        /// <param name="UserGuid">用户主键</param>
        /// <returns>影响行数</returns>
        public static int QuestionSumbit(string AnswerGuid, string QuerstionContent, string UserGuid)
        {
            return OnlineQuiz.QuestionSumbit(AnswerGuid, QuerstionContent, UserGuid);
        }

        /// <summary>
        /// 获取当前页数据
        /// </summary>
        /// <param name="page">当前第几页</param>
        /// <param name="sign">是否只看自己标记</param>
        /// <returns>数据字符串</returns>
        public static string GetDatas(int page, string sign)
        {
            API api = new API();
            SysDbContext context = api.CreateDbContext();
            Database db = DatabaseFactory.CreateDatabase();
            int pagenum = 12;//每页显示的问题个数
            string sql = string.Format("SELECT * FROM(SELECT *, row_number() OVER(ORDER BY QuerstionTime desc) rownum,count(*) OVER() as allnum FROM Busi_OnlineQuestions where AnswerGuid is NULL) AS q WHERE rownum BETWEEN " + ((page - 1) * pagenum + 1) + " AND " + page * pagenum + " order by QuerstionTime desc;");
            DataSet ds = db.ExecuteDataSet(sql);
            DataTable Questions = ds.Tables[0];
            string data = "<div class='showtable' style='border:none'><table style='width: 100%;' class='table' cellpadding='0' cellspacing='0'>";
            data += "<tr><td class='TdLabel' style='width:50%;text-align:left'>提问内容</td><td class='TdLabel' style='width: 25%;text-align:left'>提问人</td><td class='TdLabel' style='width: 25%;text-align:left'>提问时间</td></tr>";
            foreach (DataRow dr in Questions.Select("AnswerGuid is null"))
            {
                data += "<tr><td class='TdContent' style='text-align:left'><a href='Index.aspx?Guid=" + dr["Guid"].ToString() + "&sign=" + sign + "' style='font: medium '>" + dr["QuerstionContent"].ToString() + "</a></td>";
                data += "<td class='TdContent' style='text-align:left'>" + context.Users.Find(dr["QuerstionPerson"].ToString()).UserCN + "</td><td class='TdContent' style='text-align:left'>" + dr["QuerstionTime"].ToString() + "</td>";              
                data += "</tr>";
            }
            int allnum = 0;
            if (Questions.Rows.Count > 0)
            {
                allnum = Convert.ToInt32(Questions.Rows[0]["allnum"].ToString());
            }
            data += "</table></div>@" + allnum + "@" + (allnum % pagenum == 0 ? allnum / pagenum : (allnum / pagenum + 1));
            return data;
        }


        /// <summary>
        /// 获取当前用户数据
        /// </summary>
        /// <param name="page">当前第几页</param>
        /// <param name="UserGuid">用户主键</param>
        /// <param name="sign">是否只看自己标记</param>
        /// <returns>数据字符串</returns>
        public static string GetDatas(int page, string UserGuid,string sign)
        {
            API api = new API();
            SysDbContext context = api.CreateDbContext();
            Database db = DatabaseFactory.CreateDatabase();
            int pagenum = 12;//每页显示的问题个数
            string sql = string.Format("SELECT * FROM(SELECT *, row_number() OVER(ORDER BY QuerstionTime desc) rownum,count(*) OVER() as allnum FROM Busi_OnlineQuestions where QuerstionPerson='{0}' and AnswerGuid is NULL) AS q WHERE rownum BETWEEN " + ((page - 1) * pagenum + 1) + " AND " + page * pagenum + " order by QuerstionTime desc;", UserGuid);
            DataSet ds = db.ExecuteDataSet(sql);
            DataTable Questions = ds.Tables[0];
            string data = "<div class='tables'><div class='showtable'><table style='width: 99%;margin-left:5px' class='table' cellpadding='0' cellspacing='0'>";
            data += "<tr><td class='TdLabel' style='width:50%;text-align:left;font-size: medium;'>提问内容</td><td class='TdLabel' style='width: 25%;text-align:left;font-size: medium;'>提问人</td><td class='TdLabel' style='width: 25%;text-align:left;font-size: medium;'>提问时间</td></tr>";
            foreach (DataRow dr in Questions.Select("AnswerGuid is null"))
            {
                data += "<tr><td class='TdContent' style='text-align:left;'><a href='Index.aspx?Guid=" + dr["Guid"].ToString() + "&sign=" + sign + "'>" + dr["QuerstionContent"].ToString() + "</a></td>";
                data += "<td class='TdContent' style='text-align:left'>" + context.Users.Find(dr["QuerstionPerson"].ToString()).UserCN + "</td><td class='TdContent' style='text-align:left'>" + dr["QuerstionTime"].ToString() + "</td>";
                data += "</tr>";
            }
            int allnum = 0;
            if (Questions.Rows.Count > 0)
            {
                allnum = Convert.ToInt32(Questions.Rows[0]["allnum"].ToString());
            }
            data += "</table></div></div>@" + allnum + "@" + (allnum % pagenum == 0 ? allnum / pagenum : (allnum / pagenum + 1));
            return data;
        }
    }
}
