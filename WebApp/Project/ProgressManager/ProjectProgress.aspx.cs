using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebApp.AppCode;

namespace WebApp.Project.ProgressManager
{
    public partial class ProjectProgress : Yawei.Common.SharedPage
    {
        private Yawei.DataAccess.Database db = null;
        public string[] ss = new String[5];

        protected RoleCheck roleCheck;
        protected DataSet dsJdtj;
        protected string sqlwhere="";
        protected void Page_Load(object sender, EventArgs e)
        {

            roleCheck = new RoleCheck(CurrentUser);

            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();

            string querysql = "";
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                querysql = "select * from View_tz_xmjd";
            }
            else
            {
                sqlwhere += " and StartDeptGuid='" + CurrentUser.UserGroup.Guid + "'";
                querysql = "select * from View_tz_xmjd where StartDeptGuid='" + CurrentUser.UserGroup.Guid + "'";
            }
            dsJdtj = db.ExecuteDataSet(querysql);
            //项目申报 即提交未通过状态的
            ss[0] = dsJdtj.Tables[0].Select("prostate='提交' or prostate='退回'").Count().ToString();
            //项目立项 即审核通过后续信息未维护的
            ss[1] = dsJdtj.Tables[0].Select("prostate='申报' and jscount=0 and yscount=0 and ywcount=0").Count().ToString();
            //项目建设 即审核通过维护过项目建设信息 但没有验收和运维信息的
            ss[2] = dsJdtj.Tables[0].Select("prostate='申报' and jscount<>0 and yscount=0 and ywcount=0").Count().ToString();
            //项目验收  审核通过 有项目建设信息 也有验收信息的
            ss[3] = dsJdtj.Tables[0].Select("prostate='申报'  and yscount<>0 and ywcount=0").Count().ToString();
            //项目运维 均有
            ss[4] = dsJdtj.Tables[0].Select("prostate='申报'  and ywcount<>0").Count().ToString();

            
        }
    }
}