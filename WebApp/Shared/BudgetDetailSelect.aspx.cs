using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Project;

namespace Yawei.App.Shared
{
    /// <summary>
    /// 选择预算明细
    /// </summary>
    public partial class BudgetDetailSelect : System.Web.UI.Page
    {
        public String Json = "";//treeGrid数据
        public String projGuid = "";//项目编号
        BudgetDetailFacade bd = new BudgetDetailFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            projGuid = Request["ProjGuid"] ?? "";

            Json = bd.getData(projGuid, true);
        }
    }
}