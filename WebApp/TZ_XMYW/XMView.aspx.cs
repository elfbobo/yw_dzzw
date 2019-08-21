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

namespace WebApp.TZ_XMYW
{
    public partial class XMView : SharedPage
    {
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected string strProGuid = string.Empty;
        protected CommonForm form = new CommonForm();

        Yawei.DataAccess.Database db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();


        protected RoleCheck roleCheck;
        protected string returnGuid;
        protected string successGuid;

        protected string userGuid;
        protected string depGuid;

        protected DataSet dsYw;

        protected void Page_Load(object sender, EventArgs e)
        {

            #region 接受参数
            roleCheck = new RoleCheck(CurrentUser);
            strProGuid = Request["xmguid"] != null ? Request["xmguid"] : "";
            userGuid = CurrentUser.UserGuid;
            depGuid = CurrentUser.UserGroup.Guid;

            #endregion

            #region 初始化信息
            #endregion

            form.TableName = "tz_Project"; //表名
            form.Key = "ProGuid";  //主键
            form.KeyValue = strProGuid; //主键的值
            form.CurrentUser = CurrentUser;
            document = form.SetViewData(null);

            initYw();

            System.GC.Collect();

        }

        private void initYw()
        {
            dsYw = db.ExecuteDataSet("select * from tz_xmyw where xmguid='" + strProGuid + "' order by Year desc");
        }

    }
}