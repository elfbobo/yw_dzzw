using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.TZ_XMGL.tz_gkzb
{
    public partial class Create : SharedPage
    {
        CommonForm form = new CommonForm();
        protected string strGuid = string.Empty;
        protected string proGuid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strGuid = Request["Guid"] != null ? Request["Guid"] : "";
            proGuid = Request["ProjGuid"] != null ? Request["ProjGuid"] : "";

            if (strGuid == "")
            {
                ///处理有附件的页面 ViewState["Guid"] 无附件去掉以下代码
                if (ViewState["Guid"] == null)
                    ViewState["Guid"] = Guid.NewGuid().ToString();
                else
                    form.StateGuid = ViewState["Guid"].ToString();
            }
            else
            {
                ViewState["Guid"] = strGuid;
            }

            form.TableName = "tz_gkzb"; //表名
            form.Key = "guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            #region 初始化信息

            if (!IsPostBack)
            {
                form.SetControlValue(this.Page);

            }

            #endregion

            System.GC.Collect();
        }

        protected void Page_SaveData(object sender, EventArgs e)
        {
            string[,] arr = { { "xmguid", proGuid } };
            form.SaveData(this.Request, arr); //保存数据
            Response.Redirect("View.aspx?guid=" + form.KeyValue + "&ProjGuid=" + proGuid);
        }
    }
}