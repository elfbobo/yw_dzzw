using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using System.Xml;
using Yawei.FacadeCore.Project;
using System.Web.Configuration;
using Yawei.FacadeCore;
using System.Data;

namespace WebApp.TZ_XMGL.tz_wdgl
{
    public partial class Index : SharedPage
    {
        string[] xmguids;
        public DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlstr = "select * from tz_Project where StartDeptGuid='" + CurrentUser.UserGroup.Guid + "'";
            FinanceDBFacade sql = new FinanceDBFacade();
            DataSet ds = sql.GetDataSetBySql(sqlstr);
            dt = ds.Tables[0];
            string xmguid = "";
            //if (dt != null & dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        xmguid = dr["ProGuid"].ToString();
            //    }
            //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
            //    {
            //        xmguids[i] = dt.Rows[i]["ProGuid"].ToString();
            //    }
            //}
        }
    }
}