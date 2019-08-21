using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Project;
using Yawei.FacadeCore.Support;

namespace Yawei.App.Shared
{
    public partial class EstimateSelect : System.Web.UI.Page
    {
        public String Json = "";//treeGrid数据
        public String EarlyJson = "";
        public String EstOutJson = "";
        public String projGuid = "";//项目编号
        EstimateFacade ef = new EstimateFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            projGuid = Request["ProjGuid"] ?? "";
            CommonForm form = new CommonForm();
            //费用类型
            form.SetDropDownListValue(CostType, "BBD8C56C-87E1-4C34-9B01-D5A79FA794F9", true);
            Json = ef.getData(projGuid, true);
            EarlyJson = ef.getEarInvestData(projGuid, true);
            EstOutJson = ef.getOutsideData(projGuid);
        }
    }
}