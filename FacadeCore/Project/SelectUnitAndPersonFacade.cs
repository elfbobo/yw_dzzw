using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;
using Yawei.SupportCore.SupportApi;
using Yawei.SupportCore.SupportApi.DBContext;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 选择单位及单位人员
    /// </summary>
    public static class SelectUnitAndPersonFacade
    {
        /// <summary>
        /// 根据项目主键返回参建单位
        /// </summary>
        /// <param name="projGuid">项目主键</param>
        /// <param name="uName">参建单位名字查询条件</param>
        /// <returns>参建单位的DataTable</returns>
        public static DataTable GetBuilderUnitsByProjGuid(string projGuid, string uName)
        {
            API api = new API();
            SysDbContext context = api.CreateDbContext();
            DataTable dt = SelectUnitAndPerson.GetBuilderUnitsByProjGuid(projGuid, uName);
            string guid = "";
            foreach (DataRow dr in dt.Select())
            {
                guid = dr["UnitsTypeName"].ToString();
                var dic1 = context.Mapping.Where(d => d.Guid == guid).ToList();
                if (dic1.Count > 0)
                {
                    dr["UnitsTypeName"] = dic1[0].Name;
                }
                else
                    dr["UnitsTypeName"] = "";

                guid = dr["UnitsQualityName"].ToString();
                var dic2 = context.Mapping.Where(d => d.Guid == guid).ToList();
                if (dic2.Count > 0)
                {
                    dr["UnitsQualityName"] = dic2[0].Name;
                }
                else
                    dr["UnitsQualityName"] = "";
            }
            return dt;
        }

        /// <summary>
        /// 通过参建单位主键获取参建人员
        /// </summary>
        /// <param name="projGuid">项目主键</param>
        /// <param name="UnitGuid">参建单位主键</param>
        /// <returns>DataTable参建人员集合</returns>
        public static DataTable GetPersonsByUnit(string projGuid, string UnitGuid)
        {
            API api = new API();
            SysDbContext context = api.CreateDbContext();
            DataTable dt = SelectUnitAndPerson.GetPersonsByUnit(projGuid, UnitGuid);
            string guid = "";
            foreach (DataRow dr in dt.Select())
            {
                guid = dr["TypeName"].ToString();
                var dic1 = context.Mapping.Where(d => d.Guid == guid).ToList();
                if (dic1.Count > 0)
                {
                    dr["TypeName"] = dic1[0].Name;
                }
                else
                    dr["TypeName"] = "";
            }
            return dt;
        }
    }
}
