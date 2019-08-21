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
    /// 参建人员
    /// </summary>
    public static class CooperatedBuildPersonnel
    {
        /// <summary>
        /// 根据项目主键返回参建单位
        /// </summary>
        /// <param name="projGuid">项目主键</param>
        /// <param name="uName">参建单位名字查询条件</param>
        /// <returns>参建单位的DataTable</returns>
        public static DataTable GetBuilderUnitsByProjGuid(string projGuid, string uName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = new DataTable();
            if (uName == "")
            {
                dt = db.ExecuteDataSet(string.Format("select *,UType=(  select Name from Sys_Mapping where DirectoryGuid='9E9FC8C8-D961-450D-AA30-B2EF844A2E10' and Guid= Busi_ProjParticipationUnits.UnitsType) from Busi_ProjParticipationUnits where ProjGuid='{0}' and SysStatus<>-1", projGuid)).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(string.Format("select *,UType=(  select Name from Sys_Mapping where DirectoryGuid='9E9FC8C8-D961-450D-AA30-B2EF844A2E10' and Guid= Busi_ProjParticipationUnits.UnitsType) from Busi_ProjParticipationUnits where ProjGuid='{0}' and SysStatus<>-1 and UnitsName like '%{1}%'", projGuid, uName)).Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据参建单位主键查询参建单位名称
        /// </summary>
        /// <param name="Guids">参建单位主键</param>
        /// <returns>参建单位名称</returns>
        public static string GetBuilderUnitsNameByGuid(string Guids)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string json = "";
            string[] list = Guids.Split('&');
            string sql = string.Empty;
            for (int i = 0; i < list.Length; i++)
            {
                sql += string.Format("select Guid,UnitsName from Busi_ProjParticipationUnits where Guid='{0}' and SysStatus<>-1;", list[i]);
            }
            DataSet ds = db.ExecuteDataSet(sql);
            for (int j = 0; j < ds.Tables.Count; j++)
            {
                if (ds.Tables[j].Rows.Count > 0)
                {
                    json += "{Guid:'" + ds.Tables[j].Rows[0]["Guid"] + "',UnitsName:'" + ds.Tables[j].Rows[0]["UnitsName"] + "'},";
                }
            }
            return "[" + json.TrimEnd(',') + "]";
        }

        /// <summary>
        /// 根据参建单位主键查询参建单位名称
        /// </summary>
        /// <param name="Guids">参建单位主键</param>
        /// <param name="Names">名字结果集</param>
        public static void GetBuilderUnitsNameByGuid(string Guids, out string Names)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string[] list = Guids.Split('&');
            string sql = string.Empty;
            Names = string.Empty;
            string Guid = string.Empty;
            foreach (string s in list)
            {
                Guid += "'" + s + "',";
            }
            Guid = Guid.TrimEnd(',');
            sql = string.Format("select Guid,UnitsName from Busi_ProjParticipationUnits where Guid in ({0});", Guid);
            DataTable dt = db.ExecuteDataSet(sql).Tables[0];
            foreach (DataRow dr in dt.Select())
            {
                Names += dr["Guid"] + "," + dr["UnitsName"] + "&";
            }
            Names = Names.TrimEnd('&');
        }

        /// <summary>
        /// 根据工商注册号去企业库中检查是否存在，存在返回中介机构信息的主键
        /// </summary>
        /// <param name="GSZCH">工商注册号</param>
        /// <returns></returns>
        public static string CheckGszchEqual(string GSZCH)
        {
            Database db = DatabaseFactory.CreateDatabase("Agency");
            string Guid = "";
            DataSet doc = db.ExecuteDataSet("select * from Agency_Qiye where Status>-1 and GSZCH='" + GSZCH + "'");
            if (doc != null && doc.Tables[0].Rows.Count > 0)
                Guid = doc.Tables[0].Rows[0]["NBXH"].ToString();
            return Guid;
        }

    }
}
