using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 项目考核
    /// </summary>
    public static class AssessmentDetailsFacade
    {

         /// <summary>
        /// 考核模块类型
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessmentType(string ProjGuid, string DepartType)
        {
            return AssessmentDetails.GetAssessmentType(ProjGuid, DepartType);
        }

        /// <summary>
        /// 项目考核分类
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessmentDetailsType(string ProjGuid, string DepartType)
        {
            return AssessmentDetails.GetAssessmentDetailsType(ProjGuid, DepartType);
        }

        /// <summary>
        /// 获取项目考核明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessmentDetails(string ProjGuid, string DepartType)
        {
            return AssessmentDetails.GetAssessmentDetails(ProjGuid, DepartType);
        }

        /// <summary>
        /// 获取日常考核明细
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessment_DailyApplication(string ProjGuid, string DepartType)
        {
            return AssessmentDetails.GetAssessment_DailyApplication(ProjGuid, DepartType);
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
            return AssessmentDetails.GetEveryAssessmentScore(ProjGuid, DepartType, type, IsDay);
        }

        /// <summary>
        /// 合计总扣分数
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static double GetHjAssessmentScore(string ProjGuid, string DepartType)
        {
            return AssessmentDetails.GetHjAssessmentScore(ProjGuid, DepartType);
        }

        /// <summary>
        /// 获取每个大项的得分情况
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <param name="DepartType"></param>
        /// <returns></returns>
        public static DataSet GetAssessmentScore(string ProjGuid, string DepartType)
        {
            return AssessmentDetails.GetAssessmentScore(ProjGuid, DepartType);
        }

        /// <summary>
        /// 获取主管部门所有项目的平均分数
        /// </summary>
        /// <param name="deptGuid"></param>
        /// <returns></returns>
        public static string AverageAssessmentScore(string deptGuid)
        {
            return AssessmentDetails.AverageAssessmentScore(deptGuid);
        }

        /// <summary>
        /// 获取行业监管部门考核模块
        /// </summary>
        /// <param name="DepartGuid"></param>
        /// <returns></returns>
        public static DataSet GetIndustryAssessmentType(string DepartGuid)
        {
            return AssessmentDetails.GetIndustryAssessmentType(DepartGuid);
        }

        /// <summary>
        /// 获取扣分明细
        /// </summary>
        /// <param name="DepartGuid"></param>
        /// <returns></returns>
        public static DataSet GetIndustryAssessmentDetails(string DepartGuid)
        {
            return AssessmentDetails.GetIndustryAssessmentDetails(DepartGuid);
        }

        /// <summary>
        /// 获取子项目中建设单位得分
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static double GetConstructionScore(string projGuid)
        {
            return AssessmentDetails.GetConstructionScore(projGuid);
        }



        /// <summary>
        /// 政府json
        /// </summary>
        /// <returns></returns>
        public static string GetJsonDepart(string where)
        {
            string json = "";
            //string childJson = "";
            DataSet ds = AssessmentDetails.DepartAvg(where);
            DataRow[] dr;
            dr = ds.Tables[0].Select("departName1 like '%即墨市%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'即墨市政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("即墨市") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%市北区%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'市北区政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("市北区") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%市南区%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'市南区政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("市南区") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%李沧区%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'李沧区政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("李沧区") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%崂山区%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'崂山区政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("崂山区") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%黄岛区%' or departName1 like '%开发区%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'黄岛区政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("黄岛区") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%城阳区%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'城阳区政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("城阳区") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%胶州市%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'胶州市政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("胶州市") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%莱西市%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'莱西市政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("莱西市") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }
            dr = ds.Tables[0].Select("departName1 like '%平度市%'");
            if (dr.Count() > 0)
            {
                json += "{departName1:'平度市政府',DepartAvgScore:'" + AssessmentDetails.GetAvg("平度市") + "'";
                json += ",children:[";
                for (int i = 0; i < dr.Count(); i++)
                {
                    if (i != dr.Count() - 1)
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += "{Guid:'" + dr[i]["departGuid"] + "',departName1:'" + dr[i]["departName1"] + "',DepartAvgScore:'" + dr[i]["DepartAvgScore"] + "'}";
                    }
                }
                json += "]},";
            }

            //其他部门
            DataSet QtDs = AssessmentDetails.QTDepartAvg(where);
            if (QtDs.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < QtDs.Tables[0].Rows.Count; i++)
                {
                    if (i != QtDs.Tables[0].Rows.Count - 1)
                    {
                        json += "{Guid:'" + QtDs.Tables[0].Rows[i]["departGuid"] + "',departName1:'" + QtDs.Tables[0].Rows[i]["departName1"] + "',DepartAvgScore:'" + QtDs.Tables[0].Rows[i]["DepartAvgScore"] + "'},";
                    }
                    else
                    {
                        json += ",{Guid:'" + QtDs.Tables[0].Rows[i]["departGuid"] + "',departName1:'" + QtDs.Tables[0].Rows[i]["departName1"] + "',DepartAvgScore:'" + QtDs.Tables[0].Rows[i]["DepartAvgScore"] + "'}";

                    }

                }
            }

            return "{\"total\":0,\"rows\":[" + json + "]}"; ;
        }



        /// <summary>
        /// 部门json
        /// </summary>
        /// <param name="where"></param>     
        /// <param name="Condition"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetJsonDeparts(string where,  string Condition, string type)
        {
            StringBuilder jsonSb = new StringBuilder();
            StringBuilder strExcel = new StringBuilder();
            DataSet topDs = AssessmentDetails.GetTopDepartment(where);//上级节点
            if (topDs.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < topDs.Tables[0].Rows.Count; i++)
                {
                    if (topDs.Tables[0].Rows[i]["DepartGuid"].ToString() == "-1")
                    {
                        //获取市直部门
                        DataSet DeptDs = AssessmentDetails.GetDepartmentsAvgScore(topDs.Tables[0].Rows[i]["Guid"].ToString(),  Condition);
                        if (DeptDs.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder childJsonSb = new StringBuilder();
                            StringBuilder childExcel = new StringBuilder();
                            double Score = 0;//分数

                            childJsonSb.Append(",\"children\":[");
                            for (int j = 0; j < DeptDs.Tables[0].Rows.Count; j++)
                            {
                                if (j != DeptDs.Tables[0].Rows.Count - 1)
                                {
                                    childJsonSb.Append("{\"Guid\":\"" + DeptDs.Tables[0].Rows[j]["DepartGuid"] + "\",\"DepartName\":\"" + DeptDs.Tables[0].Rows[j]["DepartName"] + "\",\"DepartAvgScore\":\"" + Convert.ToDouble(DeptDs.Tables[0].Rows[j]["AvgScore"]).ToString("N2") + "\"},");

                                    childExcel.Append("<tr><td style='text-align:left;' colspan='5'>&nbsp;&nbsp;&nbsp;&nbsp;" + DeptDs.Tables[0].Rows[j]["DepartName"] + "</td><td colspan='2'>" + Convert.ToDouble(DeptDs.Tables[0].Rows[j]["AvgScore"]).ToString("N2") + "</td></tr>");
                                }
                                else
                                {
                                    childJsonSb.Append("{\"Guid\":\"" + DeptDs.Tables[0].Rows[j]["DepartGuid"] + "\",\"DepartName\":\"" + DeptDs.Tables[0].Rows[j]["DepartName"] + "\",\"DepartAvgScore\":\"" + Convert.ToDouble(DeptDs.Tables[0].Rows[j]["AvgScore"]).ToString("N2") + "\"}");
                                    childExcel.Append("<tr><td style='text-align:left;' colspan='5'>&nbsp;&nbsp;&nbsp;&nbsp;" + DeptDs.Tables[0].Rows[j]["DepartName"] + "</td><td colspan='2'>" + Convert.ToDouble(DeptDs.Tables[0].Rows[j]["AvgScore"]).ToString("N2") + "</td></tr>");
                                }
                                Score += Convert.ToDouble(DeptDs.Tables[0].Rows[j]["AvgScore"].ToString());
                            }
                            //上级部门
                            jsonSb.Append("{\"DepartName\":\"" + topDs.Tables[0].Rows[i]["DepartName"] + "\",\"DepartAvgScore\":\"" + (Score / DeptDs.Tables[0].Rows.Count).ToString("N2") + "\"");

                            strExcel.Append("<tr><td style='text-align:left;font-weight:bolder' colspan='5'>" + topDs.Tables[0].Rows[i]["DepartName"] + "</td><td colspan='2'>" + (Score / DeptDs.Tables[0].Rows.Count).ToString("N2") + "</td></tr>");

                            if (i != topDs.Tables[0].Rows.Count - 1)
                            {
                                jsonSb.Append(childJsonSb.ToString() + "]},");
                                strExcel.Append(childExcel.ToString());
                            }
                            else
                            {
                                jsonSb.Append(childJsonSb.ToString() + "]}");
                                strExcel.Append(childExcel.ToString());
                            }
                        }
                    }
                    else
                    {
                        //单独部门
                        DataSet SingleDeptDs = AssessmentDetails.GetSingletDepartmentsAvgScore(topDs.Tables[0].Rows[i]["DepartGuid"].ToString(),  Condition);
                        if (SingleDeptDs.Tables[0].Rows.Count > 0)
                        {
                            if (i != topDs.Tables[0].Rows.Count - 1)
                            {
                                jsonSb.Append("{\"Guid\":\"" + topDs.Tables[0].Rows[i]["DepartGuid"] + "\",\"DepartName\":\"" + topDs.Tables[0].Rows[i]["DepartName"] + "\",\"DepartAvgScore\":\"" + Convert.ToDouble(SingleDeptDs.Tables[0].Rows[0]["AvgScore"]).ToString("N2") + "\"},");

                                strExcel.Append("<tr><td colspan='5'>" + topDs.Tables[0].Rows[i]["DepartName"] + "</td><td colspan='2'>" + Convert.ToDouble(SingleDeptDs.Tables[0].Rows[0]["AvgScore"]).ToString("N2") + "</td></tr>");
                            }
                            else
                            {
                                jsonSb.Append("{\"Guid\":\"" + topDs.Tables[0].Rows[i]["DepartGuid"] + "\",\"DepartName\":\"" + topDs.Tables[0].Rows[i]["DepartName"] + "\",\"DepartAvgScore\":\"" + Convert.ToDouble(SingleDeptDs.Tables[0].Rows[0]["AvgScore"]).ToString("N2") + "\"}");

                                strExcel.Append("<tr><td colspan='5'>" + topDs.Tables[0].Rows[i]["DepartName"] + "</td><td colspan='2'>" + Convert.ToDouble(SingleDeptDs.Tables[0].Rows[0]["AvgScore"]).ToString("N2") + "</td></tr>");
                            }
                        }
                    }
                }
            }
            string json = "";
            if (type == "")
            {
                json = "{\"total\":0,\"rows\":[" + jsonSb.ToString() + "]}";
            }
            else
            {
                json = strExcel.ToString();
            }
            return json;
        }

        /// <summary>
        /// 获取主管部门名称
        /// </summary>
        /// <param name="departGuid"></param>
        /// <returns></returns>
        public static string GetDepartNameByDepartGuid(string departGuid)
        {
            return AssessmentDetails.GetDepartNameByDepartGuid(departGuid);
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
            return AssessmentDetails.GetProjDeparts(DepartGuid, Year, Month);
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
            return AssessmentDetails.GetProjActualScoreByDepartments(DepartGuid, Year, Month);
        }      
    }
}
                