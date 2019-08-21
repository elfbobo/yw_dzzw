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
    /// 项目考核
    /// </summary>
    public static class AssessmentDetails
    {
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// 考核模块类型
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessmentType(string ProjGuid, string DepartType) 
        {
            string TypeSql = "SELECT Type,TypeOrder FROM ProjAssessmentDetails WHERE ProjGuid='" + ProjGuid + "' and DepartType=" + DepartType + " GROUP BY Type,TypeOrder Order By TypeOrder";
            DataSet TypeDs = GetDatabase().ExecuteDataSet(TypeSql);
            return TypeDs;
        }

        /// <summary>
        /// 项目考核分类
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessmentDetailsType(string ProjGuid, string DepartType) 
        {
            string DetailsSql = "SELECT AssessmentType ,Type,convert(int,AssessmentTypeOrder) as AssessmentTypeOrder,TypeOrder FROM ProjAssessmentDetails WHERE ProjGuid='" + ProjGuid + "' and DepartType=" + DepartType + " GROUP BY AssessmentType,Type,convert(int,AssessmentTypeOrder),TypeOrder ORDER BY TypeOrder ";
            DataSet DetailsDs = GetDatabase().ExecuteDataSet(DetailsSql);

            return DetailsDs;
        }

        /// <summary>
        /// 获取考核明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessmentDetails(string ProjGuid, string DepartType)
        {
            string DetailsSql = "SELECT * FROM ProjAssessmentDetails WHERE ProjGuid='" + ProjGuid + "' and DepartType=" + DepartType + " ORDER BY AssessmentDate,TypeOrder";
            DataSet DetailsDs = GetDatabase().ExecuteDataSet(DetailsSql);

            return DetailsDs;
        }

        /// <summary>
        /// 获取日常考核明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessment_DailyApplication(string ProjGuid, string DepartType)
        {
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            string firstDay = lastMonth.AddDays(2 - lastMonth.Day).ToString("yyyy-MM-dd");//当上个月份的1号
            string lastDay = lastMonth.AddDays(1 - lastMonth.Day).AddMonths(1).ToString("yyyy-MM-dd");//当期个月1号

            string DailyApplicationSql = "SELECT * FROM ProjAssessment_DailyApplication WHERE ProjGuid='" + ProjGuid + "' and AssessmentDate BETWEEN '" + firstDay + "' AND '" + lastDay + "'  ORDER BY AssessmentDate, ScoreReasons";
            DataSet DailyApplicationDs = GetDatabase().ExecuteDataSet(DailyApplicationSql);

            return DailyApplicationDs;
        }

        /// <summary>
        /// 获取每个小项的扣分数
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <param name="type"></param>
        /// <param name="IsDay"></param>
        /// <returns></returns>
        public static double GetEveryAssessmentScore(string ProjGuid, string DepartType, string type, int IsDay)
        {
            string sql = "";
            if (IsDay == 0)
            {
                sql = "SELECT sum(score) FROM ProjAssessmentDetails WHERE ProjGuid='" + ProjGuid + "' AND AssessmentType LIKE '%" + type + "%' AND  DepartType=" + DepartType + "";
            }
            else
            {
                //日常考核
                DateTime lastMonth = DateTime.Now.AddMonths(-1);
                string firstDay = lastMonth.AddDays(2 - lastMonth.Day).ToString("yyyy-MM-dd");//当上个月份的1号
                string lastDay = lastMonth.AddDays(1 - lastMonth.Day).AddMonths(1).ToString("yyyy-MM-dd");//当期个月1号

                sql = "SELECT sum(score) FROM ProjAssessment_DailyApplication WHERE ProjGuid='" + ProjGuid + "' and AssessmentDate BETWEEN '" + firstDay + "' AND '" + lastDay + "'";
            }
            double EveryScore = 0;
            if (GetDatabase().ExecuteScalar(sql).ToString() != "")
            {
                EveryScore = Convert.ToDouble(GetDatabase().ExecuteScalar(sql));
            }
            return EveryScore;
        }

        /// <summary>
        /// 合计总扣分数
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static double GetHjAssessmentScore(string ProjGuid, string DepartType)
        {
            string sql = "SELECT sum(score) FROM ProjAssessmentDetails WHERE ProjGuid='" + ProjGuid + "' AND DepartType=" + DepartType + "";
            double EveryScore = 0;
            if (GetDatabase().ExecuteScalar(sql).ToString() != "")
            {
                EveryScore = Convert.ToDouble(GetDatabase().ExecuteScalar(sql));
            }

            //日常考核
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            string firstDay = lastMonth.AddDays(2 - lastMonth.Day).ToString("yyyy-MM-dd");//当上个月份的1号
            string lastDay = lastMonth.AddDays(1 - lastMonth.Day).AddMonths(1).ToString("yyyy-MM-dd");//当期个月1号

            string  Daysql = "SELECT sum(score) FROM ProjAssessment_DailyApplication WHERE ProjGuid='" + ProjGuid + "' and AssessmentDate BETWEEN '" + firstDay + "' AND '" + lastDay + "'";
            double DayScore = 0;
            if (GetDatabase().ExecuteScalar(Daysql).ToString() != "")
            {
                DayScore = Convert.ToDouble(GetDatabase().ExecuteScalar(Daysql));
            }

            return EveryScore + DayScore;
        }

        /// <summary>
        /// 获取每个大项的得分情况
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessmentScore(string ProjGuid, string DepartType) 
        {
            string AssessmentScoreSql = "SELECT sum(Score) AS Score,AssessmentType FROM ProjAssessment WHERE ProjGuid='" + ProjGuid + "' AND DepartType=" + DepartType + "  GROUP BY AssessmentType, AssessmentOrder ORDER BY AssessmentOrder";
            DataSet AssessmentScoreDs = GetDatabase().ExecuteDataSet(AssessmentScoreSql);

            return AssessmentScoreDs;
        }


        /// <summary>
        /// 获取行业监管部门考核模块
        /// </summary>
        /// <param name="DepartGuid"></param>
        /// <returns></returns>
        public static DataSet GetIndustryAssessmentType(string DepartGuid)
        {
            string sql = "SELECT Type, TypeOrder FROM ProjIndustryAssessmentDetails WHERE DepartGuid='" + DepartGuid + "' GROUP BY Type,TypeOrder Order By TypeOrder  ";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            return ds;
        }

        /// <summary>
        /// 获取扣分明细
        /// </summary>
        /// <param name="DepartGuid"></param>
        /// <returns></returns>
        public static DataSet GetIndustryAssessmentDetails(string DepartGuid)
        {
            string sql = "SELECT * FROM ProjIndustryAssessmentDetails WHERE DepartGuid='" + DepartGuid + "'";
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            return ds;
        }

        /// <summary>
        /// 获取子项目中建设单位得分
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static double GetConstructionScore(string projGuid) 
        {
            string sql = "SELECT constructionScore FROM V_ProjAssessment WHERE Guid='" + projGuid + "'";
            double constructionScore = Convert.ToInt32(GetDatabase().ExecuteScalar(sql));

            return constructionScore;
        }



        /// <summary>
        /// 获取主管部门所有项目的平均分数
        /// </summary>
        /// <param name="deptGuid"></param>
        /// <returns></returns>
        public static string AverageAssessmentScore(string deptGuid)
        {
            string avgSql = "SELECT avg(DepartAvgScore) AS DepartAvgScore,departName1, departGuid FROM V_ProjAllDepartAvg WHERE 1=1  " + deptGuid + " GROUP BY departGuid, departName1";
            string Score = Convert.ToDouble(GetDatabase().ExecuteScalar(avgSql)).ToString("N2");
            return Score;
        }


        /// <summary>
        /// 所有区市主管部门和平均分
        /// </summary>
        /// <returns></returns>
        public static DataSet DepartAvg(string where)
        {
            string sql = "";
            if (where == "")
            {
                sql = "SELECT avg(DepartAvgScore) AS DepartAvgScore,departName1, departGuid FROM V_ProjAllDepartAvg GROUP BY departGuid, departName1";
            }
            else
            {
                sql = "SELECT avg(DepartAvgScore) AS DepartAvgScore,departName1, departGuid FROM V_ProjAllDepartAvg where 1=1 " + where + " GROUP BY departGuid, departName1";
            }
            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            return ds;

        }

        /// <summary>
        /// 其他部门
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataSet QTDepartAvg(string where)
        {
            string sql = "";
            if (where == "")
            {
                sql = "SELECT avg(DepartAvgScore) AS DepartAvgScore,departName1, departGuid FROM V_ProjAllDepartAvg WHERE departName1 NOT IN (SELECT departName1 FROM V_ProjDepartAvg WHERE departName1 LIKE '%即墨市%' OR  departName1 LIKE '%市北区%' OR departName1 LIKE '%市南区%' OR departName1 LIKE '%李沧区%' OR departName1 LIKE '%崂山区%' OR departName1 LIKE '%黄岛区%' OR departName1 LIKE '%开发区%' OR departName1 LIKE '%城阳区%' OR departName1 LIKE '%胶州市%' OR departName1 LIKE '%莱西市%' OR departName1 LIKE '%平度市%') GROUP BY departGuid, departName1";
            }
            else
            {
                sql = "SELECT avg(DepartAvgScore) AS DepartAvgScore,departName1, departGuid FROM V_ProjAllDepartAvg WHERE departName1 NOT IN (SELECT departName1 FROM V_ProjDepartAvg WHERE departName1 LIKE '%即墨市%' OR  departName1 LIKE '%市北区%' OR departName1 LIKE '%市南区%' OR departName1 LIKE '%李沧区%' OR departName1 LIKE '%崂山区%' OR departName1 LIKE '%黄岛区%' OR departName1 LIKE '%开发区%' OR departName1 LIKE '%城阳区%' OR departName1 LIKE '%胶州市%' OR departName1 LIKE '%莱西市%' OR departName1 LIKE '%平度市%') and " + where + " GROUP BY departGuid, departName1";
            }

            DataSet ds = GetDatabase().ExecuteDataSet(sql);
            return ds;
        }

        /// <summary>
        /// 上级部门平均分
        /// </summary>
        /// <param name="depart"></param>
        /// <returns></returns>
        public static string GetAvg(string depart)
        {
            string sql = "";
            if (depart == "黄岛区")
            {
                sql = "SELECT avg(DepartAvgScore) AS DepartAvgScore,departName1, departGuid FROM V_ProjAllDepartAvg WHERE departName1 like '%黄岛区%' or departName1 LIKE '%开发区%' GROUP BY departGuid, departName1";
            }
            else
            {
                sql = "SELECT avg(DepartAvgScore) AS DepartAvgScore,departName1, departGuid FROM V_ProjAllDepartAvg WHERE departName1 like '%" + depart + "%' GROUP BY departGuid, departName1";
            }
            return Convert.ToDouble(GetDatabase().ExecuteScalar(sql)).ToString("N2");
        }

        /// <summary>
        /// 获取所有主管部门的上级部门
        /// </summary>
        /// <returns></returns>
        public static DataSet GetTopDepartment( string where) 
        {
            string sql = "SELECT * FROM Sys_Departments WHERE TopGuid='-1' and 1=1 " + where + " ORDER BY Sort ";
            return GetDatabase().ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取市直部门的平均得分
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDepartmentsAvgScore(string TopGuid,  string Condition)
        {
            string sql = "SELECT dt.DepartGuid, dt.DepartName, avg(pa.Score) AS AvgScore FROM Sys_Departments dt,View_ProjActualScore pa WHERE charindex(dt.DepartGuid,pa.DepartmentGuid)>0 AND dt.TopGuid='" + TopGuid + "' " + Condition + " GROUP BY dt.DepartGuid,dt.DepartName";
            return GetDatabase().ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取单个部门的平均分
        /// </summary>
        /// <returns></returns>
        public static DataSet GetSingletDepartmentsAvgScore(string DepartGuid,  string Condition)
        {
            string sql = "SELECT dt.DepartGuid, dt.DepartName, avg(pa.Score) AS AvgScore FROM Sys_Departments dt,View_ProjActualScore pa WHERE charindex(dt.DepartGuid,pa.DepartmentGuid)>0 AND dt.TopGuid='-1'  AND dt.DepartGuid='" + DepartGuid + "' " + Condition + " GROUP BY dt.DepartGuid,dt.DepartName";
            return GetDatabase().ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取主管部门名称
        /// </summary>
        /// <param name="departGuid"></param>
        /// <returns></returns>
        public static string GetDepartNameByDepartGuid(string departGuid)
        {
            string sql = "SELECT DepartName FROM Sys_Departments where DepartGuid='" + departGuid + "'";
            return GetDatabase().ExecuteScalar(sql).ToString();
        }


        /// <summary>
        /// 获取主管部门对应的项目
        /// </summary>
        /// <param name="DepartGuid"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static DataSet GetProjDeparts(string DepartGuid, int Year, string Month)
        {
            string sql = "SELECT Guid, ProjName FROM View_ProjActualScore WHERE Year=" + Year + "" + Month + " AND DepartmentGuid  like '%" + DepartGuid + "%' GROUP BY Guid, ProjName ";
            return GetDatabase().ExecuteDataSet(sql);
        }



        /// <summary>
        ///通过主管部门获取所对应的项目得分
        /// </summary>
        /// <param name="DepartGuid"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static DataSet GetProjActualScoreByDepartments(string DepartGuid, int Year, string Month)
        {
            string sql = "SELECT * FROM View_ProjActualScore WHERE Year=" + Year + "" + Month + " AND DepartmentGuid  like '%" + DepartGuid + "%'";
            return GetDatabase().ExecuteDataSet(sql);
        }      
    }
}
