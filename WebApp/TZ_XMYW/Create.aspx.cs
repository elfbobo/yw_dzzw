using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.TZ_XMYW
{

    public partial class Create : SharedPage
    {
        CommonForm form = new CommonForm();
        protected string strGuid = string.Empty;
        protected string proGuid = string.Empty;
        protected RoleCheck roleCheck;
        protected string baseWhere = " and sysstatus!=-1";//显示未删除的项目
        protected string roleWhere = "";
        protected string bmWhere = "";
        protected string strProName = "";

        protected string sqlWhere = "", bmGuid="";
        protected string createType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            roleCheck = new RoleCheck(CurrentUser);

            bmGuid = CurrentUser.UserGroup.Guid;
            strGuid = Request["Guid"] != null ? Request["Guid"] : "";
            proGuid = Request["xmguid"] != null ? Request["xmguid"] : "";
            createType = Request["type"] != null ? Request["type"] : "";
            if (createType != "")
            {
                strProName = Request["proname"] != null ? Request["proname"] : "";
            }

            if (strGuid == "")
            {
                this.CreateDate.Value = DateTime.Now.ToString();
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

            form.TableName = "tz_xmyw"; //表名
            form.Key = "guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            #region 初始化信息

            if (!IsPostBack)
            {
                form.SetControlValue(this.Page);

            }

            #endregion

           string bmWhere = " (StartDeptGuid='" + bmGuid + "' and ProState='申报')";

            //此处查看所有审核通过的项目
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                roleWhere = "ProState='申报'";
            }

            if (roleWhere != "")
            {
                sqlWhere = baseWhere + "and (" + bmWhere + " or " + roleWhere + ")";
            }
            else
            {
                sqlWhere = baseWhere + "and (" + bmWhere + ")";
            }
            System.GC.Collect();
        }

        protected void Page_SaveData(object sender, EventArgs e)
        {
            string[,] arr = { };
            form.SaveData(this.Request, arr); //保存数据
            if (createType != "")
            {
                Response.Redirect("XMView.aspx?xmguid=" + proGuid);
            }
            else
            {
                Response.Redirect("View.aspx?guid=" + form.KeyValue + "&xmguid=" + proGuid);
            }
            
        }
    }
}