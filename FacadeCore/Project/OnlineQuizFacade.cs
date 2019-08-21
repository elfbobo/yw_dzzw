using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;
using System.Data;
using Yawei.DataAccess;
using Yawei.SupportCore.SupportApi.DBContext;
using Yawei.SupportCore.SupportApi;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 在线问答
    /// </summary>
    public static class OnlineQuizFacade
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
        /// 提交问题
        /// </summary>
        /// <param name="AnswerGuid">回答问题主键</param>
        /// <param name="QuerstionContent">提问内容</param>
        /// <param name="UserGuid">用户主键</param>
        /// <param name="TopicGuid">帖子主键</param>
        /// <returns>影响行数</returns>
        public static int QuestionSumbit(string AnswerGuid, string QuerstionContent, string UserGuid, string TopicGuid)
        {
            return OnlineQuiz.QuestionSumbit(AnswerGuid, QuerstionContent, UserGuid, TopicGuid);
        }

        /// <summary>
        /// 提交回答
        /// </summary>
        /// <param name="QuestionGuid">问题主键</param>
        /// <param name="QuestionContent">问题内容</param>
        /// <param name="UserGuid">用户主键</param>
        /// <returns>影响行数</returns>
        public static int AnswerSumbit(string QuestionGuid, string QuestionContent, string UserGuid)
        {
            return OnlineQuiz.AnswerSumbit(QuestionGuid, QuestionContent, UserGuid);
        }

        /// <summary>
        /// 获取当前页数据
        /// </summary>
        /// <param name="page">当前第几页</param>
        /// <param name="TopicGuid">帖子主键</param>
        /// <returns>数据字符串</returns>
        public static string GetDatas(int page, string TopicGuid)
        {
            API api = new API();
            SysDbContext context = api.CreateDbContext();
            Database db = DatabaseFactory.CreateDatabase();
            int pagenum = 5;//每页显示的问题个数
            string sql = string.Format("SELECT * FROM Busi_OnlineQuestions where Guid='{0}';select * from V_OnlineInfo order by PubTime asc;select * from V_OnlineQuestionPerson;", TopicGuid);
            DataSet ds = db.ExecuteDataSet(sql);
            DataTable Questions = ds.Tables[0];
            DataTable AllInfo = ds.Tables[1];
            DataTable QuestionPerson = ds.Tables[2];
            string data = "<div class='tables'>";
            data += "<div style='color: #1e5f85;font-family: 宋体;font-size: 17px; font-weight: bold;'>&nbsp;&nbsp;问题详情：</div>";
            data += "<div class='showtable'><div class='indexcontent'>" + Questions.Rows[0]["QuerstionContent"].ToString() + "</div>";
            data += "<div class='indextime'>" + context.Users.Find(Questions.Rows[0]["QuerstionPerson"].ToString()).UserCN + "&nbsp;&nbsp;" + Questions.Rows[0]["QuerstionTime"].ToString() + "</div></div>";
            data += "<br/><br/>";
            data += "<div style='color: #1e5f85;font-family: 宋体; font-size: 17px; font-weight: bold;'>&nbsp;&nbsp;热门回复：</div>";
            data += GetData(AllInfo, QuestionPerson, Questions.Rows[0]["Guid"].ToString(), 1, context);
            //data += "<div class='qabtn'><input type='button' value='追问' guid='" + Questions.Rows[0]["Guid"].ToString() + "' onclick='QuestionAgain(this)' /></div>";
            data += "</div>";
            return data;
        }

        ///// <summary>
        ///// 获取当前用户数据
        ///// </summary>
        ///// <param name="page">当前第几页</param>
        ///// <param name="UserGuid">用户主键</param>
        ///// <param name="TopicGuid">帖子主键</param>
        ///// <returns>数据字符串</returns>
        //public static string GetDatas(int page, string UserGuid, string TopicGuid)
        //{
        //    API api = new API();
        //    SysDbContext context = api.CreateDbContext();
        //    //return context.Users.Find(userGuid);
        //    Database db = DatabaseFactory.CreateDatabase();
        //    int pagenum = 5;//每页显示的问题个数
        //    string sql = string.Format("SELECT * FROM(SELECT *, row_number() OVER(ORDER BY QuerstionTime desc) rownum,count(*) OVER() as allnum FROM Busi_OnlineQuestions where QuerstionPerson='{0}' and AnswerGuid is NULL) AS q WHERE rownum BETWEEN " + ((page - 1) * pagenum + 1) + " AND " + page * pagenum + " order by QuerstionTime desc;select * from V_OnlineInfo order by PubTime asc;select * from V_OnlineQuestionPerson;", UserGuid);
        //    DataSet ds = db.ExecuteDataSet(sql);
        //    DataTable Questions = ds.Tables[0];
        //    DataTable AllInfo = ds.Tables[1];
        //    DataTable QuestionPerson = ds.Tables[2];
        //    string data = "<div class='tables'>";
        //    foreach (DataRow dr in Questions.Select("AnswerGuid is null"))
        //    {
        //        data += "<div class='showtable'><div class='indexcontent'>" + dr["QuerstionContent"].ToString() + "</div>";
        //        data += "<div class='indextime'>" + context.Users.Find(dr["QuerstionPerson"].ToString()).UserCN + "&nbsp;&nbsp;" + dr["QuerstionTime"].ToString() + "</div>";
        //        data += GetData(AllInfo, QuestionPerson, dr["Guid"].ToString(), 1, context);
        //        data += "<div class='qabtn'><input type='button' value='追问' guid='" + dr["Guid"].ToString() + "' onclick='QuestionAgain(this)' /></div>";
        //        data += "</div>";
        //    }
        //    int allnum = 0;
        //    if (Questions.Rows.Count > 0)
        //    {
        //        allnum = Convert.ToInt32(Questions.Rows[0]["allnum"].ToString());
        //    }
        //    data += "</div>@" + allnum + "@" + (allnum % pagenum == 0 ? allnum / pagenum : (allnum / pagenum + 1));
        //    return data;
        //}

        private static string GetData(DataTable dt, DataTable qp, string Guid, int count, SysDbContext context)
        {
            string data = string.Empty;
            if (dt.Select(string.Format("TopGuid='{0}'", Guid)).Count() > 0)
            {
                string typename = string.Empty;
                foreach (DataRow dr in dt.Select(string.Format("TopGuid='{0}'", Guid)))
                {
                    if (count == 1)
                    {
                        if (dr["type"].ToString() == "Question")
                            typename = "";
                        else if (dr["type"].ToString() == "Answer")
                            typename = "";
                        data += "<div class='addtable'>";
                        data += "<div class='firstrow'><div style='font-family: 宋体;font-size:14px;margin-top: 20px;'>" + typename + dr["PubContent"].ToString() + "</div>";
                        data += "<div class='antime'>" + context.Users.Find(dr["PubPerson"].ToString()).UserCN + "&nbsp;&nbsp;" + dr["PubTime"].ToString() + "<a id='" + dr["Guid"].ToString() + "' style='margin-left:10px;' onclick='showtext(this)'>回复</a></div></div>";
                        count++;
                        data += GetDatadata(dt, qp, dr["Guid"].ToString(), count, context);
                        data += "</div><br/>";
                    }
                    else
                    {
                        if (dr["type"].ToString() == "Question")
                            typename = "";
                        else if (dr["type"].ToString() == "Answer")
                            typename = "";
                        data += "<div class='addtable'>";
                        data += "<div class='rows' ><div  style='font-family: 宋体;font-size:14px;margin-top: 20px;'>" + typename + dr["PubContent"].ToString() + "</div>";
                        data += "<div class='antime'>" + context.Users.Find(dr["PubPerson"].ToString()).UserCN + "&nbsp;&nbsp;" + dr["PubTime"].ToString() + "<a id='" + dr["Guid"].ToString() + "' style='margin-left:10px;' onclick='showtext(this)' >回复</a></div></div>";
                        count++;
                        data += GetDatadata(dt, qp, dr["Guid"].ToString(), count, context);
                        data += "</div><br/>";
                    }
                }
                return data;
            }
            else
            {
                return "";
            }
        }

        private static string GetDatadata(DataTable dt, DataTable qp, string Guid, int count, SysDbContext context)
        {
            string data = string.Empty;
            if (dt.Select(string.Format("TopGuid='{0}'", Guid)).Count() > 0)
            {
                string typename = string.Empty;
                foreach (DataRow dr in dt.Select(string.Format("TopGuid='{0}'", Guid)))
                {
                    if (count == 1)
                    {
                        if (dr["type"].ToString() == "Question")
                            typename = "";
                        else if (dr["type"].ToString() == "Answer")
                            typename = "";
                        data += "<div class='firstrow'><div  style='font-family: 宋体;font-size:14px;'>" + typename + dr["PubContent"].ToString() + "</div>";
                        data += "<div class='antime'>" + context.Users.Find(dr["PubPerson"].ToString()).UserCN + "&nbsp;&nbsp;" + dr["PubTime"].ToString() + "<a id='" + dr["Guid"].ToString() + "' style='margin-left:10px;' onclick='showtext(this)'>回复</a></div></div>";
                        count++;
                        data += GetDatadata(dt, qp, dr["Guid"].ToString(), count, context);
                    }
                    else
                    {
                        if (dr["type"].ToString() == "Question")
                            typename = "";
                        else if (dr["type"].ToString() == "Answer")
                            typename = "";
                        data += "<div class='rows'><div  style='font-family: 宋体;font-size:14px;'>" + typename + dr["PubContent"].ToString() + "</div>";
                        data += "<div class='antime'>" + context.Users.Find(dr["PubPerson"].ToString()).UserCN + "&nbsp;&nbsp;" + dr["PubTime"].ToString() + "<a id='" + dr["Guid"].ToString() + "' style='margin-left:10px;' onclick='showtext(this)'>回复</a></div></div>";
                        count++;
                        data += GetDatadata(dt, qp, dr["Guid"].ToString(), count, context);
                    }
                }
                return data;
            }
            else
            {
                return "";
            }
        }
    }
}
