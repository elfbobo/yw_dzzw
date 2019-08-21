using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.DataAccess;

namespace WebApp.TZ_XMJS.tz_xmgz
{
    public partial class Edit : SharedPage
    {
        protected string strGuid = string.Empty;
        protected string proGuid = string.Empty;
        protected DataSet dsPro;
        protected DataRow row;

        protected void Page_Load(object sender, EventArgs e)
        {
            string username = CurrentUser.UserLoginName;
            strGuid = Request["Guid"] != null ? Request["Guid"] : "";
            proGuid = Request["xmguid"] != null ? Request["xmguid"] : "";
            Database database = DatabaseFactory.CreateDatabase();

       
            dsPro = database.ExecuteDataSet("select * from tz_xmgz where guid='" + strGuid + "'");
            row = dsPro.Tables[0].Rows[0];


            #region 初始化信息

            if (!IsPostBack)
            {


            }

            #endregion

            System.GC.Collect();
        }
    }
}