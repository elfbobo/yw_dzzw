using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Project;
using Yawei.Common;
using System.Data;

namespace Yawei.App.Shared
{
    public partial class ProjectsSelected : SharedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["type"] == "getProj")
            {
                DataTable dt = TrainingFacade.GetDataTableByProjGuids(HttpUtility.UrlDecode(Request["projName"]));
                string json = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    json += "{";
                    json += "\"total\":" + dt.Rows.Count + ",\"rows\":[";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i > 0)
                            json += ",";
                        json += "{\"Guid\":\"" + dt.Rows[i]["Guid"].ToString() + "\",\"ProjName\":\"" + dt.Rows[i]["ProjName"].ToString() + "\"}";
                    }
                    json += "]}";
                }
                else 
                {
                    json += "{\"total\":0,\"rows\":[]}";
                }
                Response.Write(json);
                Response.End();
            }
        }
    }
}