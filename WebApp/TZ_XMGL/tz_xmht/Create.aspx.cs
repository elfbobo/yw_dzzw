using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;


namespace WebApp.TZ_XMGL.tz_xmht
{
    public partial class Create : SharedPage
    {
        CommonForm form = new CommonForm();
        protected string strGuid = string.Empty;
        protected string proGuid = string.Empty;

        protected string strIssueRect = string.Empty;//问题整改动态行信息
        private Yawei.DataAccess.Database db = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
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

            form.TableName = "tz_xmht"; //表名
            form.Key = "guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;

            strIssueRect = form.GetRowDataXml("tz_xmht_zffs", "htguid", form.KeyValue, null, " order by updatesj asc ");

           // DataSet ds = db.ExecuteDataSet(" select t2.* from tz_ProjectAndExpert t1 inner join  tz_ProjectExpert t2 on t1.ExpertGuid=t2.guid and t1.proguid='" + xmguid + "' ");
           // this.ProGuid.DataSource = ds.Tables[0];
           // this.ProGuid.DataTextField = "zjmc";
           // this.ProGuid.DataValueField = "Guid";
           // this.ProGuid.DataBind();

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

            //保存问题整改动态行
            DataSet ds = form.RowData(Request["IssueRowData"], "tz_xmht_zffs", "htguid", form.KeyValue);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["updatesj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string sqlinsert = String.Format(@"insert into tz_xmht_zffs(Guid,ProGuid,htguid,zftj,zfje,sysstatus,Status,updatesj)
values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", dr["Guid"], proGuid, dr["htguid"], dr["zftj"], dr["zfje"], 0, 0, dr["updatesj"]);
                db.ExecuteNonQuery(sqlinsert);
            }

            Response.Redirect("View.aspx?guid=" + form.KeyValue + "&ProjGuid=" + proGuid);
        }
    }
}