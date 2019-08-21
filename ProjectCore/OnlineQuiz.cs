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
    /// 在线问答
    /// </summary>
    public static class OnlineQuiz
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
            if (AnswerGuid != "NULL")
            {
                AnswerGuid = "'" + AnswerGuid + "'";
            }
            Database db = DatabaseFactory.CreateDatabase();
            int result = db.ExecuteNonQuery(string.Format("insert into Busi_OnlineQuestions (Guid,AnswerGuid,QuerstionTime,QuerstionPerson,QuerstionContent)" +
                " values ('{0}',{1},'{2}','{3}','{4}')", Guid.NewGuid(), AnswerGuid, DateTime.Now, UserGuid, QuerstionContent));
            return result;
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
            Database db = DatabaseFactory.CreateDatabase();
            int result = db.ExecuteNonQuery(string.Format("insert into Busi_OnlineQuestions (Guid,TopicGuid,AnswerGuid,QuerstionTime,QuerstionPerson,QuerstionContent)" +
                " values ('{0}',{1},'{2}','{3}','{4}','{5}')", Guid.NewGuid(), TopicGuid, AnswerGuid, DateTime.Now, UserGuid, QuerstionContent));
            return result;
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
            Database db = DatabaseFactory.CreateDatabase();
            int result = db.ExecuteNonQuery(string.Format("insert into Busi_OnlineAnswers (Guid,QuestionGuid,AnswerTime,AnswerContent,AnswerPerson)" +
                " values ('{0}',{1},'{2}','{3}','{4}')", Guid.NewGuid(), QuestionGuid, DateTime.Now, QuestionContent, UserGuid));
            return result;
        }

        //public static DataSet GetShowListDataTable(int page)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DataSet ds = db.ExecuteDataSet("select * from Busi_OnlineQuestions where TopicGuid is not NULL and ");
        //    return ds;
        //}
    }
}
