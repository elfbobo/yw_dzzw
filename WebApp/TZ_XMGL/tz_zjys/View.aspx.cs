﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.TZ_XMGL.tz_zjys
{
    public partial class View : SharedPage
    {
        public Dictionary<string, string> document = new Dictionary<string, string>();
        protected string strProjGuid = string.Empty;
        protected string strGuid = string.Empty;
        protected CommonForm form = new CommonForm();
        protected string strTitle = string.Empty;
        protected string datarow = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // strProjGuid = Request["Guid"] != null ? Request["Guid"] : "";
            strGuid = Request["Guid"] != null ? Request["Guid"] : "";

            form.TableName = "tz_zjys"; //表名
            form.Key = "guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            document = form.SetViewData(null);

            datarow = form.GetRowDataXml("tz_zjys_mx", "refguid", form.KeyValue, null);
            System.GC.Collect();
        }

        protected void Page_DeleteData(object sender, EventArgs e)
        {
            form.DeleteData();
            Response.Redirect("List.aspx?xmguid=" + strProjGuid);
        }
    }
}