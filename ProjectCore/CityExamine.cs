using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 
    /// </summary>
    public class CityExamine
    {
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }
        /// <summary>
        /// 获取List页面数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int StrToDataSet(DataSet ds)
        {
            GetDatabase().UpdateDataSet(ds);
            return 1;
        }
        /// <summary>
        /// 获取List页面查询数据
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int StrToDataSet(DataSet ds, string where)
        {
            DeleteCityExamine(where);
            GetDatabase().UpdateDataSet(ds);
            return 1;
        }
        /// <summary>
        /// 获取View页面数据
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataSet GetCityExamineView(string projGuid, string year, string month)
        {
            return GetDatabase().ExecuteDataSet("select * from(select *,(select name from Sys_Mapping where MapGuid=Sys_Mapping.Guid) as MapName,(select Mark from Sys_Mapping where MapGuid=Sys_Mapping.Guid) as Mark from Busi_ProjCityExamine where ExamineYear=" + year + " and ExamineMonth=" + month + "  and ProjGuid='" + projGuid + "' ) as Busi_ProjCityExamineView order by Mark");
        }
        /// <summary>
        /// 获取项目的年月
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet GetCityExamineYearAndMonth(string where)
        {
            return GetDatabase().ExecuteDataSet("select Top 12 ExamineYear,ExamineMonth from Busi_ProjCityExamine where 1=1 " + where + " group by ExamineYear,ExamineMonth order by ExamineYear desc,ExamineMonth desc ");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int DeleteCityExamine(string where)
        {
            string sql = " delete Busi_ProjCityExamine where 1=1 " + where;
            return GetDatabase().ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获取项目Guid，类型，主项目类型
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public List<string> ShowCityExamine(string projGuid)
        {
            List<string> list = new List<string>();
            DataSet ds = GetDatabase().ExecuteDataSet("select * from Busi_ProjRegister where SysStatus<>-1 and guid='" + projGuid + "'");
            list.Add(ds.Tables[0].Rows[0]["TopGuid"].ToString());
            list.Add(ds.Tables[0].Rows[0]["ProjAffiliation"].ToString());
            if (list[0] != "")
            {
                ds = GetDatabase().ExecuteDataSet("select * from Busi_ProjRegister where SysStatus<>-1 and guid='" + list[0] + "'");
                list.Add(ds.Tables[0].Rows[0]["ProjAffiliation"].ToString());
            }
            return list;
        }
    }
}
