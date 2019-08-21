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
    /// 选择单位及单位人员
    /// </summary>
    public static class SelectUnitAndPerson
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
                dt = db.ExecuteDataSet(string.Format("select *,UnitsType as UnitsTypeName,UnitsQuality as UnitsQualityName from Busi_ProjParticipationUnits where ProjGuid='{0}' and SysStatus<>-1", projGuid)).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet(string.Format("select *,UnitsType as UnitsTypeName,UnitsQuality as UnitsQualityName from Busi_ProjParticipationUnits where ProjGuid='{0}' and SysStatus<>-1 and UnitsName like '%{1}%'", projGuid, uName)).Tables[0];
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
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select *,Type as TypeName from Busi_ProjCooperatedBuildPersonnel where ProjGuid='{0}' and UnitGuid='{1}'", projGuid, UnitGuid)).Tables[0];
            return dt;
        }
    }
}
