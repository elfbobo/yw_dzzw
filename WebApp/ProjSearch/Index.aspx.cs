using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using Yawei.Common;
using Yawei.DataAccess;

namespace WebApp.ProjSearch
{
    public partial class Index : SharedPage
    {

        protected RoleCheck roleCheck;
        protected string roleWhere = "";
        protected string formStyle = "";
        protected string roleType = "0"; //0是部门用户 1是其他，2是部门管理员
        protected void Page_Load(object sender, EventArgs e)
        {
            roleCheck = new RoleCheck(CurrentUser);
            if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {
                //管理部门能查看所有提交或者通过的项目
                roleWhere = "ProState!='' and ProState is not null";
                formStyle = "float:right;width:80%";
                initTree();
                roleType = "1";

            }
            else
            {
                //部门能查询所有未删除的
                roleWhere = "";
            }
            string type = Request["type"] != null ? Request["type"] : "";
            if (type == "post")
            {
                Database db = DatabaseFactory.CreateDatabase();
                string con = Request["con"] != null ? Request["con"] : "";
                string result = string.Empty;

                DataTable dt = db.ExecuteDataSet("SELECT count(proGuid) AS projCount, sum(Quota) as ygje,sum(htje) AS htje,sum(zfje) AS zfje FROM V_TZ_ProjectOverview WHERE SysStatus<>-1 " + con).Tables[0];
                if (dt.Rows.Count > 0)
                    //String.Format("{0:F}",Convert.ToDouble( dt.Rows[0]["projCount"].ToString()))
                    result = getTwoTail(dt.Rows[0]["projCount"].ToString()) + "," + getTwoTail(dt.Rows[0]["htje"].ToString()) + "," + getTwoTail(dt.Rows[0]["zfje"].ToString()) + "," + getTwoTail(dt.Rows[0]["ygje"].ToString());
                Response.Clear();
                Response.Write(result);
                Response.Flush();
                Response.End();
            }
        }

        private string getTwoTail(string value)
        {
            return Math.Round(Convert.ToDouble(value), 2)+"";
            //return String.Format("{0:F}", Convert.ToDouble(value));
        }

        protected string zNodes;
        private void initTree()
        {
            string zwzxguid = "";
            //zNodes = "[{name: \"电子政务项目目录\",guid:\"-1\", icon:\"../js/zTree/img/diy/1_open.png\",open: true, children: [{ name: \"test1_1\" ,children: [{ name: \"test1_1\" }, { name: \"test1_2\" }]}, { name: \"test1_2\" }]}]";
            zNodes = "[{name: \"电子政务项目目录\", guid:\"-1\",open: true, icon:\"../js/zTree/img/diy/1_open.png\",open: true,children: [";
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet("select name,topguid,guid from Sys_UserGroups  order by cast(sortnum as int)");
            //获取根节点
            DataRow[] dtRootbm = ds.Tables[0].Select("topguid='-1'");
            for (int i = 0; i < dtRootbm.Count(); i++)
            {
                //if (dtRootbm[i]["name"].ToString().IndexOf("管理员") >= 0 || dtRootbm[i]["name"].ToString().IndexOf("监管") >= 0 || dtRootbm[i]["name"].ToString().IndexOf("政府办") >= 0)
                //{
                //    continue;
                //}

                //if (dtRootbm[i]["name"].ToString() == "电子政务中心")
                //{
                //    zwzxguid = dtRootbm[i]["guid"].ToString();
                //    zNodes += "{name:\"" + "政府办" + "\",\"isparent\":\"0\",guid:\"" + zwzxguid + "\"" + ",icon:\"../js/zTree/img/diy/8.png\"}";
                //    if (i != dtRootbm.Count() - 1)
                //    {
                //        zNodes += ",";
                //    }
                //    continue;
                //}


                zNodes += "{name:\"" + dtRootbm[i]["name"].ToString() + "\",guid:\"" + dtRootbm[i]["guid"].ToString() + "\"";
                DataRow[] dtLv2Rows = ds.Tables[0].Select("topguid='" + dtRootbm[i]["guid"].ToString() + "'");

                if (dtLv2Rows.Count()!=0)
                {
                    zNodes += ",\"isparent\":\"1\",icon:\"../js/zTree/img/diy/9.png\",children:[";

                    for (int j = 0; j < dtLv2Rows.Count(); j++)
                    {

                        zNodes += "{name:\"" + dtLv2Rows[j]["name"].ToString() + "\",\"isparent\":\"0\",icon:\"../js/zTree/img/diy/8.png\",guid:\"" + dtLv2Rows[j]["guid"].ToString() + "\"}";
                        if (j != dtLv2Rows.Count() - 1)
                        {
                            zNodes += ",";
                        }
                    }

                    zNodes += "]}";
                }
                else
                {
                    zNodes += ",\"isparent\":\"0\",icon:\"../js/zTree/img/diy/8.png\"}";
                }

                //if (dtRootbm[i]["name"].ToString() == "红十字会")
                //{
                //    zNodes += ",{name:\"" + "政府办" + "\",guid:\"" + zwzxguid + "\"" + ",icon:\"../js/zTree/img/diy/8.png\"}";
                //}

                if (i != dtRootbm.Count() - 1)
                {
                    zNodes += ",";
                }
            }
            zNodes += "]}]";
        }

        private string getChildStr(DataRow row,DataSet ds)
        {
            string childstr = "";
            
            DataRow[] childRows=ds.Tables[0].Select("topguid='" + row["guid"] + "'");
            if (childRows.Count() == 0)
            {
                return "";
            }
            else
            {
                for (int i = 0; i < childRows.Count(); i++)
                {
                    childstr += "{name}";
                }
            }

            return childstr;
        }

        private bool hasChild(DataRow row)
        {
            return false;
        }
    }
}