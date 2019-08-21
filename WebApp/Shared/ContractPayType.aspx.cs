using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yawei.App.Shared
{
    /// <summary>
    /// 选择合同的支付条件 井维杰 2015/3/11
    /// </summary>
    public partial class ContractPayType : System.Web.UI.Page
    {
        protected string strProjGuid = string.Empty;
        protected string strContractGuid = string.Empty;
        protected string OtherSql = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 接收参数
            strProjGuid = Request["ProjGuid"] == null ? "" : Request["ProjGuid"];
            strContractGuid = Request["ContractGuid"] == null ? "" : Request["ContractGuid"];
            #endregion

            if (strProjGuid != "")
                OtherSql += " and ProjGuid='"+strProjGuid+"'";
            if (strContractGuid != "")
                OtherSql += " and ContractGuid='"+strContractGuid+"'";

        }
    }
}