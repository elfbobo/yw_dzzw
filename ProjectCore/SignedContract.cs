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
    /// 签订合同
    /// </summary>
    public static class SignedContract
    {
        /// <summary>
        /// 通过合同主键返回合同关联概算主键
        /// </summary>
        /// <param name="strGuid">合同主键</param>
        /// <returns>概算主键集合</returns>
        public static string GetGuidsByStrGuid(string strGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select FundGuid from Busi_ProjContractFund where ContractGuid='{0}'", strGuid)).Tables[0];
            string guids = string.Empty;
            foreach (DataRow dr in dt.Select())
            {
                guids += dr["FundGuid"] + "&";
            }
            return guids;
        }

        /// <summary>
        /// 通过项目Guid取招标事项Json串
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns>招标事项Json数据集</returns>
        public static string GetJsonByProjGuid(string ProjGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select * from Busi_ProjInviteItem where ProjGuid='{0}' and SysStatus<>-1 and IType is not null", ProjGuid)).Tables[0];
            string json = string.Empty;
            foreach (DataRow dr in dt.Select())
            {
                json += "{ Guid:'" + dr["Guid"] + "', InviteCode:'" + dr["InviteCode"] + "',InviteName:'" + dr["InviteName"] + "',NoticeCode:'" + dr["NoticeCode"] + "',InviteControlCost:'" + dr["InviteControlCost"] + "'},";
            }
            return "{\"total\":0,\"rows\":[" + json.TrimEnd(',') + "]}";
        }

        /// <summary>
        /// 通过招标主键获取招标名称
        /// </summary>
        /// <param name="InviteGuid">招标主键</param>
        /// <returns>招标名称</returns>
        public static string GetInviteNameByGuid(string InviteGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select InviteName from Busi_ProjInviteItem where Guid='{0}'", InviteGuid)).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["InviteName"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 通过招标主键获取招标名称集合
        /// </summary>
        /// <param name="InviteGuids">招标主键集合</param>
        /// <returns>招标名称集合</returns>
        public static string GetInviteJsonByGuid(string InviteGuids)
        {
            InviteGuids = "'" + InviteGuids.Replace("&", "','") + "'";
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from Busi_ProjInviteItem where Guid in ({0})", InviteGuids));
            var Json = "";
            foreach (DataRow dt in ds.Tables[0].Rows)
            {
                Json += "{Guid:'" + dt["Guid"] + "',Name:'" + dt["InviteName"] + "'},";
            }
            return "[" + Json.TrimEnd(',') + "]";
        }

        /// <summary>
        /// 通过合同主键返回合同名称和合同编号
        /// </summary>
        /// <param name="ContractGuid">合同主键</param>
        /// <returns>合同名称和编号</returns>
        public static string GetContractInfoByGuid(string ContractGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select ContractName,ContractCode from Busi_ProjSignedContract where Guid='{0}'", ContractGuid)).Tables[0];
            string datas = "";
            if (dt.Rows.Count > 0)
            {
                datas = dt.Rows[0]["ContractName"] + "," + dt.Rows[0]["ContractCode"];
            }
            return datas;
        }
    }
}
