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
    /// 
    /// </summary>
    public class CityExamineFacade
    {
        CityExamine cityExamine = new CityExamine();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int StrToDataSet(string str, string type, string where)
        {
            int num = 0;
            DataSet ds = GetCityExamineDataSet(str);
            switch (type.ToLower())
            {
                case "add":
                    num = cityExamine.StrToDataSet(ds);
                    break;
                case "update":
                    num = cityExamine.StrToDataSet(ds, where);
                    break;
                default:
                    break;
            }
            return num;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataSet GetCityExamineDataSet(string str)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Busi_ProjCityExamine");
            dt.Columns.Add(new DataColumn("Guid", typeof(string)));
            dt.Columns.Add(new DataColumn("ProjGuid", typeof(string)));
            dt.Columns.Add(new DataColumn("MapGuid", typeof(string)));
            dt.Columns.Add(new DataColumn("ContentText", typeof(string)));
            dt.Columns.Add(new DataColumn("DateTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("CreateDate", typeof(string)));
            dt.Columns.Add(new DataColumn("EditDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(int)));
            dt.Columns.Add(new DataColumn("SysStatus", typeof(int)));
            dt.Columns.Add(new DataColumn("ExamineYear", typeof(int)));
            dt.Columns.Add(new DataColumn("ExamineMonth", typeof(int)));
            List<string> listRows = new List<string>();
            List<string> listUnits = new List<string>();
            listRows = str.Split(new string[] { "num丨" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            DataRow dr = null;
            for (int i = 0; i < listRows.Count; i++)
            {
                listUnits = listRows[i].Split(new string[] { "丨" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                dr = dt.NewRow();
                dr["Guid"] = Guid.NewGuid();
                dr["ProjGuid"] = listUnits[5].ToString();
                dr["MapGuid"] = listUnits[0].ToString();
                dr["Status"] = listUnits[1].ToString();
                if (listUnits[2].ToString() == "null")
                {
                    dr["ContentText"] = DBNull.Value;
                }
                else
                {
                    dr["ContentText"] = listUnits[2].ToString();
                }
                dr["CreateDate"] = DateTime.Now;
                dr["EditDate"] = DateTime.Now;
                dr["SysStatus"] = 0;
                dr["ExamineYear"] = listUnits[3].ToString();
                dr["ExamineMonth"] = listUnits[4].ToString();
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataTable GetCityExamineView(string projGuid, string year, string month)
        {
            return cityExamine.GetCityExamineView(projGuid, year, month).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<string[]> GetCityExamineYearAndMonth(string where)
        {
            List<string[]> listArray = new List<string[]>();
            DataSet ds = cityExamine.GetCityExamineYearAndMonth(where);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listArray.Add(new string[2] { ds.Tables[0].Rows[i]["ExamineYear"].ToString(), ds.Tables[0].Rows[i]["ExamineMonth"].ToString() });
            }
            return listArray;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int DeleteCityExamine(string where)
        {
            return cityExamine.DeleteCityExamine(where);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public bool ShowCityExamine(string projGuid)
        {
            List<string> list = cityExamine.ShowCityExamine(projGuid);
            if ((list[0] == "" && list[1] == "782B4E4D-BFAE-4034-96A4-BD18F95A3C61") || (list[0] != "" && list[2] == "782B4E4D-BFAE-4034-96A4-BD18F95A3C61"))
            {
                return true;
            }
            return false;
        }
    }
}
