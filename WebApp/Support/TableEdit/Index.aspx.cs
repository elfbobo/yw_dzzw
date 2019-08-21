using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore;
using System.Data;

namespace Yawei.App.Support.TableEdit
{
    public partial class Index : System.Web.UI.Page
    {
        public string tablejson = "[]";
        public string mappingJson = "[]";

        public string modelJson = "[]";
        public string jquery = string.Empty;
        public string tabelName = "";
        public string tableTitle = "";
        public string treeJson = "[]";
        public string listSetting = "[]";
        public string editSetting = "[]";
        protected void Page_Load(object sender, EventArgs e)
        {
            modelJson = TableEditCore.GetDataStruct();
            if (IsPostBack)
            {
                tabelName = Request["tablename"] ?? "PageDeal";
                tableTitle = Request["tabletitle"] ?? "";
                tablejson = TableEditCore.LoadTableInfo(tabelName);
                jquery = "$('#slecttable').hide();";
                mappingJson = TableEditCore.GetMappingDirectory();
                treeJson = TableEditCore.GetDataStructTree();
                DataSet ds = TableEditCore.GetEditTableSetting(tabelName);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    listSetting = ds.Tables[0].Rows[0]["List"].ToString();
                    editSetting = "[" + ds.Tables[0].Rows[0]["Edit"].ToString() + "]";
                }

            }
        }


        protected void Create_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request["listaspx"]))
            {
                string path = Request["pathPage"].Replace("$", "\\");
                //  path = "c://pages/";
                TableEditCore.CreatePage(path + "/", "List.aspx", Server.UrlDecode(Request["listaspx"]).Replace("^", "+").TrimEnd(','));
                TableEditCore.CreatePage(path + "/", "List.aspx.cs", Server.UrlDecode(Request["listcs"]).TrimEnd(','));
                TableEditCore.CreatePage(path + "/", "List.aspx.designer.cs", Server.UrlDecode(Request["listdesigner"]).TrimEnd(','));
                TableEditCore.CreatePage(path + "/", "View.aspx.designer.cs", Server.UrlDecode(Request["viewdesigner"]).TrimEnd(','));
                TableEditCore.CreatePage(path + "/", "View.aspx", Server.UrlDecode(Request["viewaspx"]).TrimEnd(','));
                TableEditCore.CreatePage(path + "/", "View.aspx.cs", Server.UrlDecode(Request["viewcs"]).Replace("^", "+").TrimEnd(','));
                TableEditCore.CreatePage(path + "/", "Create.aspx.cs", Server.UrlDecode(Request["createcs"]).Replace("^", "+").TrimEnd(','));
                TableEditCore.CreatePage(path + "/", "Create.aspx.designer.cs", Server.UrlDecode(Request["createdesigner"]).TrimEnd(','));
                TableEditCore.CreatePage(path + "/", "Create.aspx", Server.UrlDecode(Request["createaspx"]).TrimEnd(','));
            }
            Response.Redirect("INDEX.ASPX");
        }
    }
}