using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yawei.DataAccess;

namespace Yawei.SupportCore
{
    public static class MenuCore
    {
        /// <summary>
        /// 获取全部菜单内容。
        /// </summary>
        /// <returns></returns>
        public static string GetMenusRecurse(string pguid)
        {
            Database db = DatabaseFactory.CreateDatabase();

            #region 非递归
            DataSet doc = db.ExecuteDataSet("select Guid,Name,TopGuid,IconCls from Sys_Menus where topguid !=''  order by sortnum asc ");
            string ret = "";
            if (doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    ret += string.Format("{{id:'{0}',pId:'{1}',name:'{2}'}},", doc.Tables[0].Rows[i][0].ToString(), doc.Tables[0].Rows[i][2].ToString(), doc.Tables[0].Rows[i][1].ToString());
                }
                ret = ret.TrimEnd(',');
            }

            #endregion

            return "[" + ret + "]";
        }

        /// <summary>
        /// 获取指定菜单节点的子级菜单。
        /// </summary>
        /// <param name="parentMenuGuid"></param>
        /// <returns></returns>
        public static string GetMenus(string parentMenuGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet doc = database.ExecuteDataSet("select * from Sys_Menus where topGuid='" + parentMenuGuid + "' order by SortNum asc");

            List<string> treenode = new List<string>();
            for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
            {
                object obj = database.ExecuteScalar("select count(*) from Sys_Menus where topGuid = '" + doc.Tables[0].Rows[i]["Guid"] + "'");
                string node = string.Format("{{id:'{0}',pId:'{1}',name:'{2}',isParent:{3},icon:'Images/bullet_picture.png'}}", doc.Tables[0].Rows[i]["Guid"].ToString(), parentMenuGuid, doc.Tables[0].Rows[i]["Name"].ToString(), (int.Parse(obj.ToString()) > 0).ToString().ToLower());
                treenode.Add(node);
            }
            string ret = string.Join(",", treenode.ToArray());

            return "[" + ret + "]";
        }

        /// <summary>
        /// 根据菜单名称查询菜单。
        /// </summary>
        /// <param name="parentMenuName"></param>
        /// <returns></returns>
        public static object QueryMenus(string parentMenuName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet doc = database.ExecuteDataSet("select * from Sys_Menus m1,Sys_Menus m2 where m1.topguid=m2.guid and m2.Name like '" + parentMenuName + "' order by m2.name asc");
            string ret = "[";
            if (doc.Tables[0].Rows.Count > 0)
            {
                ret += "{";
                ret += "\"id\":" + doc.Tables[0].Rows[0]["m1.SortNum"].ToString() + ",\"text\":\"" + doc.Tables[0].Rows[0]["m1.Name"].ToString() + "\",\"icon\":\"Images/bullet_picture.png\",\"state\":closed";
                ret += "}";
            }
            ret += "]";
            return ret;
        }

        /// <summary>
        /// 根据菜单主键递归删除菜单。
        /// </summary>
        /// <param name="menuGuid"></param>
        public static void DeleteMenus(string menuGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            database.ExecuteNonQuery("delete from Sys_Menus where guid in(" + menuGuid + ")");

        }
        /// <summary>
        /// 根据菜单主键获取菜单信息。
        /// </summary>
        /// <param name="menuGuid"></param>
        /// <returns></returns>
        public static string GetMenuInfo(string menuGuid)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet doc = database.ExecuteDataSet("select * from Sys_Menus where guid='" + menuGuid + "'");
            string ret = "";
            if (doc.Tables[0].Rows.Count > 0)
            {

                ret = "[{group:'基本',child:[{name:'菜单名称',value:'" + doc.Tables[0].Rows[0]["Name"].ToString() + "',filed:'Name'}";
                ret += ",{name:'图标样式',value:'" + doc.Tables[0].Rows[0]["IconCls"].ToString() + "',filed:'IconCls'}";
                ret += ",{name:'图片链接',value:'" + doc.Tables[0].Rows[0]["ImgUrl"].ToString() + "',filed:'ImgUrl'}";
                ret += ",{name:'菜单标志',value:'" + doc.Tables[0].Rows[0]["Sign"].ToString() + "',filed:'Sign'}";
                ret += ",{name:'菜单状态',value:'" + (doc.Tables[0].Rows[0]["Status"].ToString() == "0" ? "启用" : "禁用") + "',filed:'Status',select:[{name:'启用',value:0},{name:'禁用',value:1}]}]}";
                ret += ",{group:'操作',child:[{name:'链接地址',value:'" + doc.Tables[0].Rows[0]["Href"].ToString() + "',filed:'Href'}";
                ret += ",{name:'链接目标',value:'" + doc.Tables[0].Rows[0]["Target"].ToString() + "',filed:'Target',select:[{name:'_self',value:'_self'},{name:'_blank',value:'_blank'},{name:'_top',value:'_top'},{name:'_parent',value:'_parent'}]}";
                ret += ",{name:'脚本方法',value:'" + doc.Tables[0].Rows[0]["JSEvent"].ToString() + "',filed:'JSEvent'}";
                ret += "]}]";
            }
            else
            {
                ret = "[{group:'基本',child:[{name:'菜单名称',value:'',filed:'Name'}";
                ret += ",{name:'图标样式',value:'',filed:'IconCls'}";
                ret += ",{name:'图片链接',value:'',filed:'ImgUrl'}";
                ret += ",{name:'菜单标志',value:'',filed:'Sign'}";
                ret += ",{name:'菜单状态',value:'',filed:'Status'}]}";
                ret += ",{group:'操作',child:[{name:'链接地址',value:'',filed:'Href'}";
                ret += ",{name:'链接目标',value:'',filed:'Target'}";
                ret += ",{name:'脚本方法',value:'',filed:'JSEvent'}";
                ret += "]}]";
            }
            return ret;
        }

        /// <summary>
        /// 添加一个菜单项。
        /// </summary>
        /// <param name="doc"></param>
        public static void CreateMenu(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            Database database = DatabaseFactory.CreateDatabase();
            DbConnection cnn = database.CreateConnection();
            Database.OpenConnection(cnn);
            DbTransaction tran = Database.BeginTransaction(cnn);
            DbCommand command;
            try
            {
                XmlNode values = xmlDoc.FirstChild;
                command = database.GetSqlStringCommand("insert into Sys_Menus(Guid,TopGuid,Name,IconCls,ImgUrl,Href,Target,JSEvent) values(@Guid,@Pguid,@Name,@Icon,@Img,@Href,@Targ,@JS,@Sort)");
                database.AddInParameter(command, "Guid", DbType.String, Guid.NewGuid().ToString());
                database.AddInParameter(command, "Pguid", DbType.String, values.Attributes["TopGuid"].Value);
                database.AddInParameter(command, "Name", DbType.String, values.Attributes["Name"].Value);
                database.AddInParameter(command, "Icon", DbType.String, values.Attributes["IconCls"].Value);
                database.AddInParameter(command, "Img", DbType.String, values.Attributes["ImgUrl"].Value);
                database.AddInParameter(command, "Href", DbType.String, values.Attributes["Href"].Value);
                database.AddInParameter(command, "Targ", DbType.String, values.Attributes["Target"].Value);
                database.AddInParameter(command, "JS", DbType.String, values.Attributes["JSEvent"].Value);
                database.AddInParameter(command, "Sort", DbType.String, values.Attributes["SortNum"].Value);
                
                database.ExecuteNonQuery(command, tran);
                Database.CommitTransaction(tran);
            }
            catch
            {
                Database.RollbackTransaction(tran);
            }
            finally
            {
                Database.CloseConnection(cnn);
            }
        }

        /// <summary>
        /// 修改一个菜单项。
        /// </summary>
        /// <param name="doc"></param>
        public static void ModifyMenu(string xml)
        {
            XmlDataDocument xmlDoc = new XmlDataDocument();
            xmlDoc.LoadXml(xml);
            Database database = DatabaseFactory.CreateDatabase();
            DbConnection cnn = database.CreateConnection();
            Database.OpenConnection(cnn);
            DbTransaction tran = Database.BeginTransaction(cnn);
            DbCommand command;

            try
            {
                XmlNode values = xmlDoc.FirstChild.FirstChild;
                command = database.GetSqlStringCommand("update Sys_Menus set Name=@Name,IconCls=@Icon,ImgUrl=@Img,Href=@Href,Target=@Targ,JSEvent=@JS where Guid=@Guid");
                database.AddInParameter(command, "Guid", DbType.String, values.Attributes["Guid"].Value);
                database.AddInParameter(command, "Name", DbType.String, values.Attributes["Name"].Value);
                database.AddInParameter(command, "Icon", DbType.String, values.Attributes["IconCls"].Value);
                database.AddInParameter(command, "Img", DbType.String, values.Attributes["ImgUrl"].Value);
                database.AddInParameter(command, "Href", DbType.String, values.Attributes["Href"].Value);
                database.AddInParameter(command, "Targ", DbType.String, values.Attributes["Target"].Value);
                database.AddInParameter(command, "JS", DbType.String, values.Attributes["JSEvent"].Value);
                database.ExecuteNonQuery(command, tran);
                Database.CommitTransaction(tran);
            }
            catch
            {
                Database.RollbackTransaction(tran);
            }
            finally
            {
                Database.CloseConnection(cnn);
            }
        }

        public static void ModifyMenu(string guid, string index, string value)
        {
            Database database = DatabaseFactory.CreateDatabase();

            if (index.ToLower() == "status")
            {
                database.ExecuteNonQuery("update Sys_Menus set Status=" + (value == "启用" ? 0 : 1) + " where guid ='" + guid + "'");
            }
            else
            {
                database.ExecuteNonQuery("update Sys_Menus set " + index + "='" + value + "' where guid ='" + guid + "'");
            }
        }
        ///<summary>
        ///增加菜单节点
        ///</summary>
        ///<param name="doc"></param>
        public static void AddNode(string guid, string topGuid, string sort)
        {
            Database database = DatabaseFactory.CreateDatabase();
            database.ExecuteNonQuery("insert into Sys_Menus(Guid,TopGuid,Name,SortNum,IconCls) values('" + guid + "','" + topGuid + "','未命名'," + sort + ",'icon-menuItem')");
        }

        public static void DragNode(string guid, string sort, string topguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            if (topguid != "")
            {
                db.ExecuteNonQuery("update Sys_Menus set TopGuid='" + topguid + "',sortnum=" + sort + " where guid ='" + guid + "'");
            }
            else
            {
                string[] ids = guid.TrimEnd(',').Split(',');
                string[] num = sort.TrimEnd(',').Split(',');
                string sql = "";
                for (int i = 0; i < ids.Length; i++)
                {
                    sql += "update Sys_Menus set sortnum=" + num[i] + " where guid ='" + ids[i] + "';";
                }
                db.ExecuteNonQuery(sql);
            }
        }

    }
}

