using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.Project.StartProject
{
    public partial class View : SharedPage
    {
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected string strGuid = string.Empty;
        protected CommonForm form = new CommonForm();

        //是否可以编辑，除管理员外  已提交或者审核通过的普通用户不可编辑
        protected bool isEditable = false;
        protected bool isAdmin = true;

        Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();

        //是否存在退回历史
        protected bool hasReturnHistory = false;
        protected DataSet dsReturnHistory;

        protected void Page_Load(object sender, EventArgs e)
        {

            #region 接受参数

            strGuid = Request["xmguid"] != null ? Request["xmguid"] : "";

            checkReturnHistory();
            #endregion

            #region 初始化信息

            form.TableName = "tz_Project"; //表名
            form.Key = "ProGuid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;
            document = form.SetViewData(null);

            string prostate = "";
            if (!UserCheck.checkIsAdmin(CurrentUser.user.UserLoginName))
            {
                isAdmin = false;
                DataSet ds = db.ExecuteDataSet(" select * from tz_Project where ProGuid='" + strGuid + "'");
                prostate = ds.Tables[0].Rows[0]["ProState"].ToString();
                //未提交或者退回的项目申请提供编辑功能
                if (prostate == "" || prostate == "退回")
                {
                    isEditable = true;
                }

            }
            else
            {
                isAdmin = true;
                isEditable = true;
            }

            #endregion

            System.GC.Collect();

        }

        private void checkReturnHistory()
        {
            string querysql = "select * from tz_projecthistory where proguid='" + strGuid + "' and proaction='退回' order by createdate";
            dsReturnHistory = db.ExecuteDataSet(querysql);
            if (dsReturnHistory.Tables[0].Rows.Count > 0)
            {
                hasReturnHistory = true;
            }
            
        }

        protected void Page_DeleteData(object sender, EventArgs e)
        {
            form.DeleteData();
            Response.Redirect("List.aspx");
        }

    }
}