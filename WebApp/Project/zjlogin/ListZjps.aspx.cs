using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.FacadeCore.Support;

namespace WebApp.Project.zjlogin
{
    public partial class ListZjps : Yawei.Common.SharedPage
    {
        protected string strGuid = "";
        protected string xmguid = "";
        protected string zjguid = "";
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected CommonForm form = new CommonForm();
        private Yawei.DataAccess.Database db = null;
        protected System.Data.DataTable dtExperts = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            strGuid = this.Request.Params["xmguid"] ?? "";
            xmguid = this.Request.Params["xmguid"] ?? "";
            zjguid = this.Request.Params["zjguid"] ?? "";

            form.TableName = "tz_Project"; //表名
            form.Key = "ProGuid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;
            document = form.SetViewData(null);
        }
    }
}