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
    /// 资金来源明细对象
    /// </summary>
    public class InvestFromDetails
    {
        /// <summary>
        /// 获取数据库实力
        /// </summary>
        Database db = DatabaseFactory.CreateDatabase();
        /// <summary>
        /// 根据月报guid获取资金来源明细guid
        /// </summary>
        /// <param name="refGuid"></param>
        /// <returns></returns>
        public DataSet GetGuidByRefGuid(string refGuid)
        {

            DataSet ds = db.ExecuteDataSet("select * from Busi_InvestFromDetails where RefGuid ='" + refGuid + "'");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            return null;
        }

        /// <summary>
        /// 根据概算主键返回概算单项名称
        /// </summary>
        /// <param name="Guids">主键集合</param>
        /// <returns>名称集合</returns>
        public string GetFundNameByGuid(string Guids)
        {
            Guids = "'" + Guids.Replace("&", "','") + "'";
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from View_AllFundSelect where Guid in({0})", Guids));
            var Json = "";
            foreach (DataRow dt in ds.Tables[0].Rows)
            {
                Json += "{Guid:'" + dt["Guid"] + "',Name:'" + dt["ProjOrCostName"] + "'},";
            }
            return "[" + Json.TrimEnd(',') + "]";
        }

        /// <summary>
        /// 根据概算主键返回概算单项名称
        /// </summary>
        /// <param name="Guids">主键集合</param>
        /// <param name="Datas">名称集合</param>
        public void GetFundNameByGuid(string Guids, out string Datas)
        {
            Guids = "'" + Guids.Replace("&", "','") + "'";
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from View_AllFundSelect where Guid in({0})", Guids));
            Datas = string.Empty;
            foreach (DataRow dt in ds.Tables[0].Rows)
            {
                Datas += dt["Guid"] + "," + dt["ProjOrCostName"] + "&";
            }
            Datas = Datas.TrimEnd('&');
        }

        /// <summary>
        /// 11-18
        /// 支付条件已填，但没有合同，删除支付条件
        /// </summary>
        /// <param name="Guid"></param>
        /// <param name="fieldName">字段</param>
        /// <param name="tableName"></param>
        public int DeleteContractPay(string Guid, string fieldName, string tableName)
        {
            string sql = "select * from " + tableName + " where " + fieldName + "='" + Guid + "' and SysStatus<>-1";
            Database db = DatabaseFactory.CreateDatabase();
            int i = db.ExecuteNonQuery(sql);

            return i;
        }
    }
}
