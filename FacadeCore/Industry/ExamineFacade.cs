using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.IndustryManagerCore;

namespace Yawei.FacadeCore.Industry
{
    /// <summary>
    /// 考核
    /// </summary>
    public class ExamineFacade
    {
        Examine em = new Examine();

        /// <summary>
        /// 获取所有行业监管考核信息
        /// </summary>
        /// <returns></returns>
        public string GetAllExamine()
        {
            string HtmlValue="";
            DataSet EmDoc=em.GetDataSet("select * from Idty_Examine");//所有考核信息
            DataSet UnitDoc = em.GetDataSet("select * from Busi_IndustrySupervisionDepartment");//所有行业监管部门
            DataRow[] Rows;
            string UnitGuid="";
            string UnitName="";
            double ResponsibilityScore = 0;
            double LryScore=0;
            double TrainingScore = 0;
            string Href = "";
            if (UnitDoc != null && UnitDoc.Tables[0].Rows.Count > 0)
            {
                if (EmDoc != null && EmDoc.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < UnitDoc.Tables[0].Rows.Count; i++)
                    {
                        ResponsibilityScore = 0;
                        LryScore = 0;
                        TrainingScore = 0;
                        UnitGuid=UnitDoc.Tables[0].Rows[i]["UnitGuid"].ToString();
                        UnitName=UnitDoc.Tables[0].Rows[i]["DepartmentName"].ToString();
                        HtmlValue += "<tr>";
                        HtmlValue += "<td class=\"TdContent\" style=\"height: 22px; width: 31%;\">" + UnitName + "</td>";
                        Rows = EmDoc.Tables[0].Select("UnitGuid='" + UnitGuid + "' and [Type]='Responsibility'");//责任体系
                        if (Rows != null && Rows.Length > 0)
                            ResponsibilityScore =double.Parse(Rows[0]["Score"].ToString());
                        if (ResponsibilityScore > 30)
                            ResponsibilityScore = 30.0;
                        if (ResponsibilityScore > 0)
                            Href = "<a style='cursor:pointer;color:red;text-decoration:none' title='" + ResponsibilityScore + "' href='ResponsibilityList.aspx?UnitGuid=" + UnitGuid + "'>" + ResponsibilityScore.ToString("0.0") + "</a>";
                        else
                            Href = ResponsibilityScore.ToString("0.0");
                        HtmlValue += "<td class=\"TdContent\" style=\"height: 22px; width: 18%;\">" + Href + "</td>";//责任体系扣分
                        ResponsibilityScore=30-ResponsibilityScore;
                        
                        LryScore += GetScore(EmDoc, "UnitGuid='" + UnitGuid + "' and [Type] in ('DateLry','NoAttachLry','NoFileLry')");//证照批复
                        if (LryScore > 60)
                            LryScore = 60.0;
                        if (LryScore > 0)
                            Href = "<a style='cursor:pointer;color:red;text-decoration:none' title='" + LryScore + "' href='FileUpList.aspx?UnitGuid=" + UnitGuid + "'>" + LryScore.ToString("0.0") + "</a>";
                        else
                            Href = LryScore.ToString("0.0");
                        HtmlValue += "<td class=\"TdContent\" style=\"height: 22px; width: 18%;\">" + Href + "</td>";//证照批复扣分
                        LryScore=60 - LryScore;

                        TrainingScore += GetScore(EmDoc, "UnitGuid='" + UnitGuid + "' and [Type] in ('NoTraining','PartialTraining')");//培训登记
                        if (TrainingScore > 10)
                            TrainingScore = 10.0;
                        HtmlValue += "<td class=\"TdContent\" style=\"height: 22px; width: 18%;\">" + TrainingScore.ToString("0.0") + "</td>";//培训登记扣分
                        TrainingScore=10 - TrainingScore;
                        HtmlValue += "<td class=\"TdContent\" style=\"height: 22px; width: 15%;\">" + (ResponsibilityScore + LryScore + TrainingScore).ToString("0.0") + "</td>";
                        HtmlValue += "</tr>";
                    }
                }
            }
            return HtmlValue;
        }

        /// <summary>
        /// 根据得到分数
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="OtherSql"></param>
        /// <returns></returns>
        double GetScore(DataSet doc, string OtherSql)
        {
            DataRow[] Rows;
            double score = 0;
            Rows = doc.Tables[0].Select(OtherSql);
            if (Rows != null && Rows.Length > 0)
            {
                for (int j = 0; j < Rows.Length; j++)
                    score += double.Parse(Rows[j]["Score"].ToString());
            }
            return score;
        }

    }
}
