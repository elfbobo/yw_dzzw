using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 培训登记
    /// </summary>
    public static class Training
    {
        static Database db = DatabaseFactory.CreateDatabase();

        /// <summary>
        /// 根据部门名称获取分管处室
        /// </summary>
        /// <param name="UserTitle"></param>
        /// <returns></returns>
        public static DataSet GetSupOffice(string UserTitle)
        {
            string sql = "select Guid,Office from Busi_IndustrySupervisionOffice where DepartGuid in (select guid from Busi_IndustrySupervisionDepartment where unitguid in (select unitguid from Sys_LocalUser where UserTitle=@UserTitle))";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@UserTitle", UserTitle));
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据主键获取分管处室
        /// </summary>
        /// <param name="OfficeGuid"></param>
        /// <returns></returns>
        public static object GetSupOfficeByGuid(string OfficeGuid)
        {
            string sql = "select office from Busi_IndustrySupervisionOffice where Guid=@OfficeGuid";
            DbCommand cmd = db.CreateCommand(CommandType.Text, sql);
            cmd.Parameters.Add(new SqlParameter("@OfficeGuid", OfficeGuid));
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 通过项目名称集合获取项目主键，名字集合
        /// </summary>
        /// <param name="ProjName">项目名称集合</param>
        /// <returns>主键，名字DataTable</returns>
        public static DataTable GetDataTableByProjGuids(string ProjName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = new DataTable();
            if (ProjName != "" && ProjName != null)
            {
                dt = db.ExecuteDataSet(string.Format("select Guid,ProjName from Busi_ProjRegister where ProjName like '%{0}%'", ProjName)).Tables[0];
            }
            else
            {
                dt = db.ExecuteDataSet("select Guid,ProjName from Busi_ProjRegister").Tables[0];
            }
            return dt;
        }
    }
}
