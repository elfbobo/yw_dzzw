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
    /// 
    /// </summary>
    public class GetConfirm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="remark"></param>
        /// <param name="sign"></param>
        /// <param name="confirmStatus"></param>
        /// <returns></returns>
        public DataSet GetConfirmInfo(string projGuid, string remark, string sign, string confirmStatus)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = new DataSet();
            string sql = string.Empty;
            switch (sign)
            {
                case "confirm":
                    if (confirmStatus == "1" || confirmStatus == "2")
                    {
                        sql = "update  Busi_ProjConfirmList set Remark='" + remark + "',Status=1 where RefGuid='" + projGuid + "'";
                    }
                    else
                    {
                        sql = " insert into Busi_ProjConfirmList ([Guid],[ProjGuid],[RefGuid],[Remark],[Date],[Status],[_Table]) values(NEWID(),'" + projGuid + "','" + projGuid + "','" + remark + "','" + DateTime.Now + "',1,'" + sign + "') ";
                    }
                    break;
                case "cannelConfirm":
                    sql = "delete  Busi_ProjConfirmList  where RefGuid='" + projGuid + "'";
                    break;
                case "reback":
                    if (confirmStatus == "1" || confirmStatus == "2")
                    {
                        sql = "update  Busi_ProjConfirmList set Remark='" + remark + "',Status=2 where RefGuid='" + projGuid + "'";
                    }
                    else
                    {
                        sql = " insert into Busi_ProjConfirmList ([Guid],[ProjGuid],[RefGuid],[Remark],[Date],[Status],[_Table]) values(NEWID(),'" + projGuid + "','" + projGuid + "','" + remark + "','" + DateTime.Now + "',2,'" + sign + "') ";
                    }
                    break;
                default:
                    return ds = null;
            }
            if (db.ExecuteNonQuery(sql) == 1)
            {
                ds = db.ExecuteDataSet("  select * from Busi_ProjConfirmList   where RefGuid='" + projGuid + "' ");
            }

            return ds;
        }
    }
}
