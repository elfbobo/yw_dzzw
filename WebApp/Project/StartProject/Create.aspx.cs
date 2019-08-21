using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.Project.StartProject
{
    public partial class Create : SharedPage
    {
        CommonForm form = new CommonForm();
        protected string strGuid = string.Empty;
        protected string strTitle = string.Empty;
        protected string strProjGuid = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            #region 接受参数

            strGuid = Request["Guid"] != null ? Request["Guid"] : "";

            #endregion

            //this.Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (strGuid == "")
            {
                this.CreateDate.Value = DateTime.Now.ToString();
                ///处理有附件的页面 ViewState["Guid"] 无附件去掉以下代码
                if (ViewState["Guid"] == null)
                    ViewState["Guid"] = Guid.NewGuid().ToString();
                else
                    form.StateGuid = ViewState["Guid"].ToString();


                this.StartDeptGuid.Text = CurrentUser.UserGroup.Guid;
                this.StartDeptName.Text = CurrentUser.UserGroup.Name;
                this.StartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StartUserGuid.Text = CurrentUser.UserGuid;
            }
            else
            {
                ViewState["Guid"] = strGuid;
            }
            //*********************************************

            form.TableName = "tz_Project"; //表名
            form.Key = "ProGuid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            #region 初始化信息


            if (!IsPostBack)
            {
                form.SetControlValue(this.Page);
                if (this.MoneySource.SelectedValue == "其他")
                {
                    this.MoneySourceDesc.Visible = true;
                }
                if (this.Quota.Text != "")
                {
                    this.Quota.Text = Convert.ToDouble(this.Quota.Text).ToString();
                }
                

            }

            if (strGuid != "")
                this.EditDate.Value = DateTime.Now.ToString();

            #endregion

            System.GC.Collect();

        }
        protected void Page_SaveData(object sender, EventArgs e)
        {

            //if(this.Request[""].ToString())
            form.SaveData(this.Request, null); //保存数据
            Response.Redirect("View.aspx?xmguid=" + form.KeyValue);
        }

        protected void MoneySource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MoneySource.SelectedValue == "其他")
            {
                this.MoneySourceDesc.Visible = true;
            }
            else
            {
                this.MoneySourceDesc.Visible = false;
            }
        }

    }
}