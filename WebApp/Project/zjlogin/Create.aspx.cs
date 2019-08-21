using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;
using System.Data;

namespace WebApp.Project.zjlogin
{
    public partial class Create : Yawei.Common.SharedPage
    {
        CommonForm form = new CommonForm();
        protected string strGuid = string.Empty;
        protected string xmguid = string.Empty;
        protected string zjguid = string.Empty;
        protected string strTitle = string.Empty;
        protected string strProjGuid = string.Empty;
        public Dictionary<string, string> document = new Dictionary<string, string>();
        private Yawei.DataAccess.Database db = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            #region 接受参数

            strGuid = Request["Guid"] != null ? Request["Guid"] : "";
            this.ProGuid.Value = Request["xmguid"] != null ? Request["xmguid"] : "";
            this.xmguid = Request["xmguid"] != null ? Request["xmguid"] : "";
            this.zjguid = Request["zjguid"] != null ? Request["zjguid"] : "";
            this.ExpertGuid.Value = zjguid;
            this.pssj.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            #endregion

            //加载项目信息
            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            System.Data.DataSet dsProject= db.ExecuteDataSet(" select * from tz_project where proguid='"+xmguid+"' ");
            foreach (DataColumn col in dsProject.Tables[0].Columns)
            {
                if (dsProject.Tables[0].Rows.Count > 0)
                {
                    document.Add(col.ColumnName, dsProject.Tables[0].Rows[0][col].ToString());
                }
            }

            //this.Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
            //*********************************************

            form.TableName = "tz_ProjectExpertPS"; //表名
            form.Key = "Guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            #region 初始化信息


            if (!IsPostBack)
            {
                form.SetControlValue(this.Page);

            }

            if (strGuid != "")
                this.EditDate.Value = DateTime.Now.ToString();

            #endregion

            System.GC.Collect();

        }
        protected void Page_SaveData(object sender, EventArgs e)
        {
            form.SaveData(this.Request, null); //保存数据
            Response.Redirect("View.aspx?xmguid=" + xmguid + "&guid=" + form.KeyValue+"&zjguid="+zjguid);
        }
    }
}