using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.Project.zjps
{
    public partial class View : Yawei.Common.SharedPage
    {
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected string strGuid = string.Empty;
        protected string xmguid = string.Empty;
        protected CommonForm form = new CommonForm();
        protected void Page_Load(object sender, EventArgs e)
        {

            #region 接受参数

            strGuid = Request["Guid"] != null ? Request["Guid"] : "";
            xmguid = Request["xmguid"] != null ? Request["xmguid"] : "";

            #endregion



            #region 初始化信息

            form.TableName = "tz_ProjectExpertPS"; //表名
            form.Key = "Guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;
            document = form.SetViewData(null);

            #endregion

            System.GC.Collect();

        }
        protected void Page_DeleteData(object sender, EventArgs e)
        {
            form.DeleteData();
            Response.Redirect("List.aspx?xmguid="+xmguid);
        }

    }
}