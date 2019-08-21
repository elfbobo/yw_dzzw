using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.Common;

namespace WebApp.Project.ProgressManager
{
    public partial class ProjectProgressDetail : Yawei.Common.SharedPage
    {
        //db tool 
        Yawei.DataAccess.Database db = null;
        protected Dictionary<string, string> document = new Dictionary<string, string>();
        protected string xmguid = "";
        protected string[] ss = new String[2];
        protected string wordCloudData = "中标金额：,招标方式：,资金估算：,资金到位：,预算：,";
        protected string mapJson = "[]";
        protected string step = "0";
        protected string proRefGuid = "";
        private List<String> stepTitle = new List<string>(new string[] { "", "申报", "立项", "建设", "验收", "运维"});
        protected DataSet dsOverview;
        protected DataRow drOverview = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = Yawei.DataAccess.DatabaseFactory.CreateDatabase();
            //取回参数
            xmguid = this.Request.Params["xmguid"] ?? "";

            dsOverview = db.ExecuteDataSet("select * from V_TZ_ProjectOverview where ProGuid='" + xmguid + "'");
            DataTable dt = dsOverview.Tables[0];
            int rowcount = dt.Rows.Count;
            foreach (DataColumn col in dt.Columns)
            {
                if (rowcount > 0)
                {
                    document.Add(col.ColumnName, dt.Rows[0][col].ToString());
                }
                else
                {
                    document.Add(col.ColumnName, "");
                }
            }

            //加载项目信息
            //loadProjectInfo(xmguid);

            //项目附件加上评审附件
            proRefGuid += xmguid + "," + document["PsFiles"];


            //评审附件refguid
            initWordJson();
            initMoney();

            if (document["ProState"] == "提交" || document["ProState"] == "退回")
            {
                step = "2";
            }
            else if (document["ProState"] == "申报" && document["jscount"] == "0" && document["yscount"] == "0" && document["ywcount"] == "0")
            {
                step = "3";
            }
            else if (document["ProState"] == "申报" && document["jscount"] != "0" && document["yscount"] == "0" && document["ywcount"] == "0")
            {
                step = "4";
            }
            else if (document["ProState"] == "申报" && document["yscount"] != "0" && document["ywcount"] == "0")
            {
                step = "5";
            }
            else if (document["ProState"] == "申报" && document["ywcount"] != "")
            {
                step = "6";
            }
            //string currentStep = document["ProState"];
            //int setIndex = stepTitle.IndexOf(currentStep);
            //if (setIndex >= 0)
            //{
            //    step = (setIndex + 1).ToString();
            //}
        }


        protected void initWordJson()
        {
            string zbfs = "";
            if (document["gkzbcount"] != "0")
            {
                zbfs += "公开招标，";
            }
            if (document["xjcount"] != "0")
            {
                zbfs+="询价，";
            }
            if (document["dylycount"] != "0")
            {
                zbfs+="单一来源，";
            }
            if (document["zjcgcount"] != "0")
            {
                zbfs += "直接采购，";
            }
            if (document["jzxcscount"] != "0")
            {
                zbfs += "竞争性磋商，";
            }
            if (document["yqzbcount"] != "0")
            {
                zbfs += "邀请招标，";
            }
            if (document["jzxtpcount"] != "0")
            {
                zbfs += "竞争性谈判，";
            }
            if (zbfs == "")
            {
                zbfs = "暂无";
            }
            else
            {
                zbfs = zbfs.Substring(0, zbfs.Length - 1);
            }
            string htje = "";
            if (document["htje"] == "0")
            {
                htje = "：暂无";
            }
            else
            {
                htje = ":" + document["htje"];
            }
            string zjzf = "";
            if (document["zfje"] == "0")
            {
                zjzf = "：暂无";
            }
            else
            {
                zjzf = ":" + document["zfje"];
            }
            //+ document["Quota"].Substring(0, document["Quota"].IndexOf("."))
            //mapJson = "[{\"label\":\"预算金额(万元)：" + document["Quota"] + "\",\"url\":\"#\",\"value\":500,\"target\":\"_top\",\"forecolor\":\"#000\"},{\"label\":\"中标方式：" + zbfs + "\",\"url\":\"#\",\"value\":600,\"target\":\"_top\",\"forecolor\":\"#000\"},{\"label\":\"合同金额(万元)" + htje + "\",\"url\":\"#\",\"value\":700,\"target\":\"_top\",\"forecolor\":\"#000\"},{\"label\":\"资金支付(万元)" + zjzf + "\",\"url\":\"#\",\"value\":800,\"target\":\"_top\",\"forecolor\":\"#000\"},{\"label\":\"项目类型：" + document["ProType"] + "\",\"url\":\"#\",\"value\":900,\"target\":\"_top\",\"forecolor\":\"#000\"}]";
            mapJson = "[{name:\"预算金额(万元)：" + document["Quota"] + "\",value:500,itemStyle: createRandomItemStyle()},{name:\"中标方式：" + zbfs + "\",value:600,itemStyle: createRandomItemStyle()},{name:\"合同金额(万元)" + htje + "\",value:700,itemStyle: createRandomItemStyle()},{name:\"资金支付(万元)" + zjzf + "\",value:800,itemStyle: createRandomItemStyle()},{name:\"项目类型：" + document["ProType"] + "\",value:900,itemStyle: createRandomItemStyle()}]";
        }

        protected void initMoney()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select top 1 htje from tz_xmht where xmguid=@xmguid;");
            sb.Append(" select top 1 zfje from tz_zjzf where xmguid=@xmguid;");

            DbCommand comd = db.CreateCommand(CommandType.Text, sb.ToString());
            db.AddInParameter(comd, "@xmguid", DbType.String, xmguid);
            DataSet ds = db.ExecuteDataSet(comd);

            string[] ss2 = new String[2];
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].Rows.Count > 0)
                {
                    ss2[i] = ds.Tables[i].Rows[0][0].ToString();
                }
                else
                {
                    ss2[i] = "0";
                }
            }
            ss = ss2;
        }
    }
}