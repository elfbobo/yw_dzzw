using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.FacadeCore.Support;

namespace WebApp.Project.zjlogin
{
    public partial class View : Yawei.Common.SharedPage
    {
        public Dictionary<string, string> document = new Dictionary<string, string>();
        public Dictionary<string, string> documentProject = new Dictionary<string, string>();
        protected string strGuid = string.Empty;
        protected string xmguid = string.Empty;
        protected string zjguid = string.Empty;
        protected CommonForm form = new CommonForm();
        private Yawei.DataAccess.Database db = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            #region 接受参数

            strGuid = Request["Guid"] != null ? Request["Guid"] : "";
            xmguid = Request["xmguid"] != null ? Request["xmguid"] : "";
            zjguid = Request["zjguid"] != null ? Request["zjguid"] : "";

            #endregion

            if (strGuid == "" && xmguid.Length > 0 &&zjguid.Length>0)
            {
                System.Data.DataSet dsPs = db.ExecuteDataSet(" select top 1 * from tz_ProjectExpertPS where proguid='" + xmguid + "' and ExpertGuid='"+zjguid+"'");
                if (dsPs.Tables[0].Rows.Count > 0)
                {
                    strGuid = dsPs.Tables[0].Rows[0]["Guid"].ToString();
                }
                else
                {
                    //默认insert一条记录
                    var psguid=Guid.NewGuid().ToString();
                    db.ExecuteNonQuery(String.Format(@" insert into tz_ProjectExpertPS(Guid,ProGuid,ExpertGuid,psyj,pssj,sysstatus,status)
 values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",psguid,xmguid,zjguid,"",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),0,0));
                    this.strGuid = psguid;
                }
            }

            //加载项目信息
            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            System.Data.DataSet dsProject = db.ExecuteDataSet(" select * from tz_project where proguid='" + xmguid + "' ");
            foreach (DataColumn col in dsProject.Tables[0].Columns)
            {
                if (dsProject.Tables[0].Rows.Count > 0)
                {
                    documentProject.Add(col.ColumnName, dsProject.Tables[0].Rows[0][col].ToString());
                }
            }

            #region 初始化信息

            form.TableName = "tz_ProjectExpertPS"; //表名
            form.Key = "Guid";  //主键
            form.KeyValue = strGuid; //主键的值
            form.CurrentUser = CurrentUser;
            document = form.SetViewData(null);

            #endregion

            System.GC.Collect();

        }
        protected void Page_DeleteData(object sender, EventArgs e)
        {
            form.DeleteData();
            Response.Redirect("List.aspx?xmguid=" + xmguid);
        }
    }
}