using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.SupportCore.SupportApi.Entity;
using System.Text;
using Yawei.DataAccess;
using System.Data;

namespace Yawei.App.Shared
{
    public partial class FGWUnitSelect : SharedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["type"] == "getUnit")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" select [Guid],[Name] from [Sys_UserGroups] ");
                sb.Append(" where Guid in ( ");
                sb.Append(" SELECT distinct [UserGroupGuid] ");
                sb.Append(" FROM [Sys_UserAndGroup] where UserGuid in ( ");
                sb.Append(" SELECT [UserGuid] FROM [Sys_RoleUsers] ");
                sb.Append(" where RoleGuid='5e022811-e156-02b0-7971-52d55727c60f' ");
                sb.Append(" )) ");
                Database db = DatabaseFactory.CreateDatabase();
                DataSet ds = db.ExecuteDataSet(sb.ToString());
                string json = string.Empty;
                json += "{";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    json += "\"total\":" + ds.Tables[0].Rows.Count + ",\"rows\":[";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (i > 0)
                            json += ",";
                        json += "{\"Guid\":\"" + ds.Tables[0].Rows[i]["Guid"].ToString() + "\",\"DeptName\":\"" + ds.Tables[0].Rows[i]["Name"].ToString() + "\"}";
                    }
                }
                json += "]}";
                Response.Write(json);
                Response.End();
            }

            //List<Group> groupLit = Group.GetGroup();
            //if (!string.IsNullOrEmpty(Request["unitName"]))
            //{
            //    groupLit = groupLit.Where(g => g.Name.Contains(Request["unitName"])).ToList();
            //}
            //string json = string.Empty;
            //json += "{";
            //json += "\"total\":" + groupLit.Count() + ",\"rows\":[";
            //for (int i = 0; i < groupLit.Count; i++)
            //{
            //    if (i > 0)
            //        json += ",";
            //    json += "{\"Guid\":\"" + groupLit[i].Guid + "\",\"DeptName\":\"" + groupLit[i].Name + "\"}";
            //}
            //json += "]}";
            //Response.Write(json);
            //Response.End();
        }
    }
}
