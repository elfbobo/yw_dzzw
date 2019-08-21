using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Project;
using Yawei.Common;

namespace Yawei.App.Shared
{
    public partial class ProjInviteSelected : SharedPage
    {
        protected string Json = "";//treeGrid数据
        protected string projGuid = "";//项目编号
        protected void Page_Load(object sender, EventArgs e)
        {
            projGuid = Request["projGuid"] != null ? Request["projGuid"] : "";

            Json = SignedContractFacade.GetJsonByProjGuid(projGuid);
        }
    }
}