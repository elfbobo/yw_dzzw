using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yawei.DataAccess;

namespace Yawei.SupportCore
{
    public class AuthorityCore
    {
        #region 树操作

        /// <summary>
        /// 获取菜单Json
        /// </summary>
        /// <returns></returns>
        public string GetMenu()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string strSql = "select * from Sys_Menus order by sortnum";
            DataSet ds = database.ExecuteDataSet(strSql);
            DataRow[] dr = ds.Tables[0].Select("TopGuid='-1'");
            string json = "";
            for (int i = 0; i < dr.Length; i++)
            {
                if (i > 0)
                    json += ",";
                json += "{id:'" + dr[i]["guid"].ToString() + "',name:'" + dr[i]["name"].ToString() + "' ";
                json += GetNodeJson("guid", "topGuid", ds, dr[i]["guid"].ToString(), "Name");
            }
            return "[" + json + "]";
        }

        /// <summary>
        /// 获取角色json
        /// </summary>
        /// <returns></returns>
        public string GetRole()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string strSql = "select * from sys_Roles";
            DataSet ds = database.ExecuteDataSet(strSql);
            DataRow[] dr = ds.Tables[0].Select("TopGuid='-1'");
            string json = "";
            for (int i = 0; i < dr.Length; i++)
            {

                json += ",";
                json += "{id:'" + dr[i]["guid"].ToString() + "',name:'" + dr[i]["name"].ToString() + "' ";
                json += GetNodeJson("guid", "topGuid", ds, dr[i]["guid"].ToString(), "Name");

            }
            return "[{id:'xxxxx',name:'EVERYONE'}" + json + "]";
        }

        /// <summary>
        /// 获取模块json
        /// </summary>
        /// <returns></returns>
        public string GetModules()
        {
            Database database = DatabaseFactory.CreateDatabase();
            string strSql = "select * from Sys_Modules order by [Identity] asc";
            DataSet ds = database.ExecuteDataSet(strSql);
            DataRow[] dr = ds.Tables[0].Select("TopGuid='-1'");
            string json = "";
            for (int i = 0; i < dr.Length; i++)
            {
                if (i > 0)
                    json += ",";
                json += "{id:'" + dr[i]["guid"].ToString() + "',name:'" + dr[i]["name"].ToString() + "' ";
                json += GetNodeJson("guid", "topGuid", ds, dr[i]["guid"].ToString(), "Name");

            }
            return "[" + json + "]";
        }

        /// <summary>
        /// 递归树  json
        /// </summary>
        /// <param name="idName">节点字段名称</param>
        /// <param name="TopName">父节点字段名称</param>
        /// <param name="data">节点数据</param>
        /// <param name="id">查找节点数据</param>
        /// <param name="title">树显示内容</param>
        /// <returns></returns>
        string GetNodeJson(string idName, string TopName, DataSet data, string id, string title)
        {
            string json = "";
            DataRow[] dr = data.Tables[0].Select(TopName + "='" + id + "'");
            if (dr.Length > 0)
            {
                json += " ,children:[";
                for (int i = 0; i < dr.Length; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{id:'" + dr[i][idName].ToString() + "',name:'" + dr[i][title].ToString() + "'";
                    json += GetNodeJson(idName, TopName, data, dr[i][idName].ToString(), title);
                }
                json += "]}";
            }
            else
                json += "}";
            return json;
        }

        #endregion

        #region 菜单授权
        /// <summary>
        /// 菜单授权
        /// </summary>
        /// <param name="roleXML">角色xml</param>
        /// <param name="menuXML">菜单xml</param>
        /// <param name="action">授权</param>
        public DataSet MenuLicenses(string roleXML, string menuXML, string action)
        {
            Database db = DatabaseFactory.CreateDatabase();
            XmlDocument roleDocument = new XmlDocument();
            XmlDocument menuDocument = new XmlDocument();
            // DataSet dsLicenses = db.ExecuteDataSet("select * from Proj_MenuLicenses where 1=1");

            roleDocument.LoadXml(roleXML);
            menuDocument.LoadXml(menuXML);

            XmlNodeList roleList = roleDocument.FirstChild.ChildNodes;
            XmlNodeList menuList = menuDocument.FirstChild.ChildNodes;

            DataSet dsLicenses = db.ExecuteDataSet("select * from Sys_MenuLicenses where MenuGuid in('" + SplitXml(menuList, "guid", "','") + "') and RoleGuid in ('" + SplitXml(roleList, "guid", "','") + "')");

            DataRow dr = null; DataRow[] drArr = null;
            for (int i = 0; i < menuList.Count; i++)
            {

                for (int j = 0; j < roleList.Count; j++)
                {
                    drArr = dsLicenses.Tables[0].Select("MenuGuid='" + menuList[i].Attributes["guid"].Value + "' and RoleGuid='" + roleList[j].Attributes["guid"].Value + "'");
                    if (drArr.Length > 0)
                    {
                        drArr[0]["Type"] = action;
                        continue;
                    }
                    dr = dsLicenses.Tables[0].NewRow();
                    dr["MenuGuid"] = menuList[i].Attributes["guid"].Value;
                    dr["RoleGuid"] = roleList[j].Attributes["guid"].Value;
                    dr["Type"] = action;
                    dsLicenses.Tables[0].Rows.Add(dr);
                }
            }
            dsLicenses.Tables[0].TableName = "Sys_MenuLicenses";
            return dsLicenses;
        }



        public int DeleteMenuLicenses(string roleXML, string menuXML, string action)
        {
            Database db = DatabaseFactory.CreateDatabase();
            XmlDocument roleDocument = new XmlDocument();
            XmlDocument menuDocument = new XmlDocument();
            // DataSet dsLicenses = db.ExecuteDataSet("select * from Proj_MenuLicenses where 1=1");

            roleDocument.LoadXml(roleXML);
            menuDocument.LoadXml(menuXML);

            XmlNodeList roleList = roleDocument.FirstChild.ChildNodes;
            XmlNodeList menuList = menuDocument.FirstChild.ChildNodes;

            return db.ExecuteNonQuery("delete from Sys_MenuLicenses where MenuGuid in('" + SplitXml(menuList, "guid", "','") + "') and RoleGuid in ('" + SplitXml(roleList, "guid", "','") + "')");

        }


        string SplitXml(XmlNodeList xmlList, string attribute, string spilt)
        {
            string temp = "";
            for (int i = 0; i < xmlList.Count; i++)
            {
                if (i > 0)
                    temp += spilt;
                temp += xmlList[i].Attributes[attribute].Value;
            }

            return temp;
        }
        #endregion

        #region 模块


        /// <summary>
        /// 模块授权
        /// </summary>
        /// <param name="roleXML">角色xml</param>
        /// <param name="menuXML">模块xml</param>
        /// <param name="action">授权</param>
        public DataSet ModelLicenses(string roleXML, string modelXML, string action)
        {
            Database db = DatabaseFactory.CreateDatabase();
            XmlDocument roleDocument = new XmlDocument();
            XmlDocument modelDocument = new XmlDocument();
            // DataSet dsLicenses = db.ExecuteDataSet("select * from Proj_MenuLicenses where 1=1");

            roleDocument.LoadXml(roleXML);
            modelDocument.LoadXml(modelXML);

            XmlNodeList roleList = roleDocument.FirstChild.ChildNodes;
            XmlNodeList modelList = modelDocument.FirstChild.ChildNodes;

            DataSet dsLicenses = db.ExecuteDataSet("select * from Sys_ModelLicenses where modelGuid in('" + SplitXml(modelList, "guid", "','") + "') and RoleGuid in ('" + SplitXml(roleList, "guid", "','") + "')");

            DataRow dr = null; DataRow[] drArr = null;
            for (int i = 0; i < modelList.Count; i++)
            {

                for (int j = 0; j < roleList.Count; j++)
                {
                    drArr = dsLicenses.Tables[0].Select("modelGuid='" + modelList[i].Attributes["guid"].Value + "' and RoleGuid='" + roleList[j].Attributes["guid"].Value + "'");
                    if (drArr.Length > 0)
                    {
                        drArr[0]["Type"] = action;
                        continue;
                    }
                    dr = dsLicenses.Tables[0].NewRow();
                    dr["modelGuid"] = modelList[i].Attributes["guid"].Value;
                    dr["RoleGuid"] = roleList[j].Attributes["guid"].Value;
                    dr["Type"] = action;
                    dsLicenses.Tables[0].Rows.Add(dr);
                }
            }
            dsLicenses.Tables[0].TableName = "Sys_ModelLicenses";
            return dsLicenses;
        }


        public int DeleteModelLicenses(string roleXML, string modelXML, string action)
        {
            Database db = DatabaseFactory.CreateDatabase();
            XmlDocument roleDocument = new XmlDocument();
            XmlDocument modelDocument = new XmlDocument();
            // DataSet dsLicenses = db.ExecuteDataSet("select * from Proj_MenuLicenses where 1=1");

            roleDocument.LoadXml(roleXML);
            modelDocument.LoadXml(modelXML);

            XmlNodeList roleList = roleDocument.FirstChild.ChildNodes;
            XmlNodeList modelList = modelDocument.FirstChild.ChildNodes;

            return db.ExecuteNonQuery("delete from Sys_ModelLicenses where ModelGuid in('" + SplitXml(modelList, "guid", "','") + "') and RoleGuid in ('" + SplitXml(roleList, "guid", "','") + "')");

        }

        #endregion

        #region 授权
        public void Licenses(string roleNodeXml, string menuNodeXml, string modulXml, string type)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(menuNodeXml))
            {
                ds.Merge(MenuLicenses(roleNodeXml, menuNodeXml, type));
            }
            if (!string.IsNullOrEmpty(modulXml))
            {
                ds.Merge(ModelLicenses(roleNodeXml, modulXml, type));
            }
            db.UpdateDataSet(ds);
        }
        #endregion

        public string GetUnionMenu(string roleXml)
        {
            string menuGuid = "";
            XmlDocument roleDocument = new XmlDocument();
            roleDocument.LoadXml(roleXml);
            if (roleDocument.HasChildNodes && roleDocument.FirstChild.HasChildNodes)
            {
                XmlNodeList roleList = roleDocument.FirstChild.ChildNodes;
                string str = "";
                if (roleList.Count > 1)
                {
                    str = " SELECT [MenuGuid] FROM [Sys_MenuLicenses] where type=1 and roleguid in('" + SplitXml(roleList, "guid", "','") + "') group by [MenuGuid] having count(1)>" + (roleList.Count - 1);
                }
                else
                {
                    str = "SELECT [MenuGuid] FROM [Sys_MenuLicenses] where type=1 and roleguid='" + roleList[0].Attributes["guid"].Value + "' ";
                }
                Database db = DatabaseFactory.CreateDatabase();
                DataSet ds = db.ExecuteDataSet(str);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i > 0)
                        menuGuid += "^";
                    menuGuid += ds.Tables[0].Rows[i][0].ToString();
                }
            }
            return menuGuid;
        }


        public string GetUnionModel(string roleXml)
        {
            string modelGuid = "";
            XmlDocument roleDocument = new XmlDocument();
            roleDocument.LoadXml(roleXml);
            if (roleDocument.HasChildNodes && roleDocument.FirstChild.HasChildNodes)
            {
                XmlNodeList roleList = roleDocument.FirstChild.ChildNodes;
                string str = "";
                if (roleList.Count > 1)
                {
                    str = " SELECT [ModelGuid] FROM [Sys_ModelLicenses] where type=1 and roleguid in('" + SplitXml(roleList, "guid", "','") + "') group by [ModelGuid] having count(1)>" + (roleList.Count - 1);
                }
                else
                {
                    str = "SELECT [ModelGuid] FROM [Sys_ModelLicenses] where type=1 and roleguid='" + roleList[0].Attributes["guid"].Value + "' ";
                }
                Database db = DatabaseFactory.CreateDatabase();
                DataSet ds = db.ExecuteDataSet(str);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i > 0)
                        modelGuid += "^";
                    modelGuid += ds.Tables[0].Rows[i][0].ToString();
                }
            }
            return modelGuid;
        }
    }
}
