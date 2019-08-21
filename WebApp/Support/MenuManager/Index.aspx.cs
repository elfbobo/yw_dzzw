using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore;

namespace Yawei.App.Support.MenuManager
{
    public partial class Index : System.Web.UI.Page
    {
        protected string strOpt = string.Empty;
        protected string strId = string.Empty;
        protected string projGuid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            projGuid = Request["projGuid"] == null ? "" : Request["projGuid"];
            #region 接受参数

            strOpt = Request["Opt"] != null ? Request["Opt"] : "";
            strId = Request["id"] != null ? Request["id"] : "";
            if (strOpt == "gettree")
            {
                if (strId != "")
                {
                    Response.Write(MenuCore.GetMenusRecurse(strId));
                    Response.End();
                }
            }
            else if (strOpt == "addnode")
            {
                string guid = Request["guid"];
                string pid = Request["pId"];
                string sort = Request["sort"];
                MenuCore.AddNode(guid, pid, sort);
            }
            else if (strOpt == "dragnode")
            {
                string guid = Request["guid"];
                string pid = Request["pId"] == null ? "" : Request["pId"];
                string sort = Request["sort"];
                MenuCore.DragNode(guid, sort, pid);
            }
            else if (strOpt == "delete")
            {
                string guid = Request["guid"];
                MenuCore.DeleteMenus(guid);
            }
            else if (strOpt == "modifyprop")
            {
                string guid = Request["guid"];
                MenuCore.ModifyMenu(guid);
            }
            else if (strOpt == "saveprop")
            {
                string guid = Request["guid"];
                string index = Request["pId"] == null ? "" : Request["pId"];
                string value = Request["sort"];
                MenuCore.ModifyMenu(guid, index, value);
            }
            else if(strOpt=="GetMenujson")
            {
                string guid = Request["guid"];
                Response.Write(MenuCore.GetMenuInfo(guid));
                Response.End();
            }
            else if (strOpt == "GetGuid")
            {
                Response.Write(Guid.NewGuid().ToString());
                Response.End();
            }
            else if (strOpt == "ModifyMenus")
            {
                string guid = Request["guid"];
                string index = Request["index"];
                string value = Request["value"];
                MenuCore.ModifyMenu(guid, index, value);
            }
            else if (strOpt == "DragNode")
            {
                string guid = Request["guid"];
                string sort = Request["sort"];
                string pid = Request["pid"];
                MenuCore.DragNode(guid, sort, pid);
            }
            #endregion

        }
       
    }
}