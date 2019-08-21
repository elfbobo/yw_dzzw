using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore.SupportApi.Entity;
using Yawei.Common;

namespace Yawei.App.Shared
{
    public partial class ProjUnitSelected : SharedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["type"] == "getUnit")
            {
                List<Group> groupLit = Group.GetGroup();
                if (!string.IsNullOrEmpty(Request["unitName"]))
                {
                    groupLit = groupLit.Where(g => g.Name.Contains(Request["unitName"])).ToList();
                }
                string json = string.Empty;
                json += "{";
                json += "\"total\":" + groupLit.Count() + ",\"rows\":[";
                for (int i = 0; i < groupLit.Count; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{\"Guid\":\"" + groupLit[i].Guid + "\",\"DeptName\":\"" + groupLit[i].Name + "\"}";
                }
                json += "]}";
                Response.Write(json);
                Response.End();
            }
        }
    }
}