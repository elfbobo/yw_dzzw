using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore.SupportApi.Entity;
using Yawei.Common;
using Yawei.FacadeCore.Project;
using System.Data;

namespace Yawei.App.Shared
{
    public partial class BuilderUnitSelected : SharedPage
    {
        protected string projGuid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            projGuid = Request["projGuid"] != null ? Request["projGuid"] : "";

            if (Request["type"] == "getUnit")
            {
                string pGuid = Request["pGuids"] != null ? Request["pGuids"] : "";
                string uName = Request["uName"] != null ? Request["uName"] : "";

                DataTable dt = CooperatedBuildPersonFacade.GetBuilderUnitsByProjGuid(pGuid, uName);
                string json = string.Empty;
                json += "{";
                json += "\"total\":" + dt.Rows.Count + ",\"rows\":[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{\"Guid\":\"" + dt.Rows[i]["Guid"] + "\",\"UnitsName\":\"" + dt.Rows[i]["UnitsName"] + "\",\"UType\":\"" + dt.Rows[i]["UType"] + "\"}";
                }
                json += "]}";
                Response.Write(json);
                Response.End();
            }
        }
    }
}