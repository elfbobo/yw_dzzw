using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yawei.DataAccess;
using Yawei.SupportCore.SupportApi.Entity;

namespace Yawei.SupportCore
{
    public class UserManageCore
    {
        /// <summary>   获取全部用户组和用户的内容。
        /// 
        /// </summary>
        public static string GetGroupsAndUsers(string Topguid)
        {

            Database database = DatabaseFactory.CreateDatabase();
            #region 获取节点数据

            string NodesData = string.Empty;
            string sql = "select Guid,Name from sys_usergroups where TopGuid='-1'";
            if (Topguid != "-1")
            {
                sql = "select Guid,Name from Sys_usergroups where TopGuid ='" + Topguid + "'";
            }
            DataSet doc = database.ExecuteDataSet(sql);
            List<string> treenode = new List<string>();
            if (doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    string node = string.Format("{{id:'{0}',pId:'{2}',name:'{1}',isParent: " + IsParent(doc.Tables[0].Rows[i][0].ToString()) + ",type:'group',icon:'../../Images/box.png'}}", doc.Tables[0].Rows[i][0].ToString(), doc.Tables[0].Rows[i][1].ToString(), Topguid);
                    treenode.Add(node);
                }
                NodesData = string.Join(",", treenode.ToArray());

                NodesData += getnodeuser(Topguid, ",");

            }
            else
            {
                NodesData += getnodeuser(Topguid, "");
            }



            #endregion
            return "[" + NodesData + "]";

        }
        /// <summary>
        /// 获取分组下用户数据
        /// </summary>
        /// <param name="groupGuid"></param>
        /// <returns></returns>
        public static string getnodeuser(string groupGuid, string ishasgroup)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "SELECT u.UserGuid,u.UserLoginName FROM dbo.Sys_users u,Sys_userandgroup ug WHERE u.UserGuid=ug.UserGuid AND ug.UserGroupGuid='" + groupGuid + "'";
            DataSet doc = database.ExecuteDataSet(sql);
            sql = ishasgroup;
            if (doc.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    string node = string.Format("{{id:'{0}',pId:'{2}',name:'{1}',type:'user',icon:'../../Images/user.png'}},", doc.Tables[0].Rows[i][0].ToString(), doc.Tables[0].Rows[i][1].ToString(), groupGuid);
                    sql += node;
                }
            }
            return sql.TrimEnd(',');
        }

        /// <summary>
        /// 根据用户组名称查询用户组和用户。
        /// </summary>
        public static object QueryGroupsAndUsers()
        {
            return null;
        }

        /// <summary>
        /// 根据工作组主键递归删除工作组和用户。
        /// </summary>
        public static string DeleteGroups(string groupguid, string userguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            if (userguid != "")
            {
                db.ExecuteNonQuery("delete from Sys_userandgroup  where UserGuid in (" + userguid + ") and UserGroupGuid in (" + groupguid + ")");
                db.ExecuteNonQuery("delete from Sys_users  where UserGuid not in (select UserGuid from Sys_userandgroup )");//取消该组下的所有用户
            }
            return db.ExecuteNonQuery("delete from Sys_usergroups where guid in (" + groupguid + ")").ToString() == "0" ? "0" : "1";

        }

        /// <summary>
        /// 根据用户主键递归删除用户。
        /// </summary>
        public static string DeleteUsers(string parentguid, string userguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            db.ExecuteNonQuery("delete from Sys_userandgroup where Userguid='" + userguid + "' and UserGroupGuid='" + parentguid + "' ");
            return db.ExecuteNonQuery("delete from Sys_users where Userguid not in (select UserGuid from Sys_userandgroup)").ToString();
        }

        /// <summary>
        /// 根据用户主键获取用户信息。
        /// </summary>
        public static object GetUserInfo()
        {
            return null;
        }

        /// <summary>    添加一个用户组。 
        /// </summary>
        /// <param name="guid">用户组主键</param>
        /// <param name="Topguid">上级用户组主键</param>
        /// <param name="name">用户组名</param>
        /// <returns></returns>

        public static string CreateGroup(string guid, string Topguid, string name)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //guid = guid == "100" ? Guid.NewGuid().ToString() : guid;
            string sql = "INSERT INTO Sys_usergroups (Guid,Name";
            sql += ",TopGuid,status) VALUES ('" + guid + "','" + name + "','" + Topguid + "',0)";

            return db.ExecuteNonQuery(sql).ToString();
        }

        /// <summary>
        /// 修改一个用户组。
        /// </summary>
        public static string ModifyGroup(string groupguid, string name)
        {
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteNonQuery("update Sys_usergroups set name='" + name + "' where guid='" + groupguid + "'").ToString();
        }

        /// <summary>
        /// 添加用户。
        /// </summary>
        public static bool  CreateProjUser(string addusersql, string addusersAndGroupsql, string groupid, string userid, bool isusercaninmoregroup)
        {
            Database db = DatabaseFactory.CreateDatabase();
            if (userid.Length > 2)
            {
                db.ExecuteNonQuery("delete from Sys_users where userguid in  " + userid);
                if (isusercaninmoregroup)
                {
                    db.ExecuteNonQuery("delete from Sys_userandgroup where UserGroupGuid='" + groupid + "' and userguid in  " + userid);
                }
                else
                {
                    db.ExecuteNonQuery("delete from Sys_userandgroup where userguid in  " + userid);
                }
            }
            int groupexenum = db.ExecuteNonQuery("INSERT INTO Sys_userandgroup (UserGuid,UserGroupGuid) select " + addusersAndGroupsql);
            int userexenum = 0;
            if (addusersql != "")
            {
                userexenum = db.ExecuteNonQuery("INSERT INTO Sys_users (UserGuid,UserLoginName,UserType,UserDN,UserCN) select " + addusersql);
            }
            return groupexenum + userexenum > 0 ? true : false;
            //return db.ExecuteNonQuery("INSERT INTO Proj_users (UserGuid,UserLoginName,UserType,UserDN,UserCN) "+addusersql).ToString()=="0"?false:true;
        }

        /// <summary> 修改一个用户。
        /// 
        /// </summary>
        public static void ModifyUser()
        {

        }

        /// <summary>   增加一个用户属性。
        /// 
        /// </summary>
        public static string CreateUserProperty(string userguid, string property)
        {
            Database db = DatabaseFactory.CreateDatabase();
            property = StringToXMl(property);
            return db.ExecuteNonQuery("update Sys_users set UserProperty='" + property + "' where UserGuid='" + userguid + "' ").ToString() == "0" ? "0" : "1";


        }

        /// <summary> 修改一个用户属性。
        /// 
        /// </summary>
        public static void ModifyUserProperty()
        {

        }

        /// <summary> 删除一个用户属性。
        /// 
        /// </summary>
        public static string DeleteUserProperty(string userguid, string property)
        {

            Database db = DatabaseFactory.CreateDatabase();
            property = StringToXMl(property);
            return db.ExecuteNonQuery("update Sys_users set UserProperty='" + property + "' where UserGuid='" + userguid + "' ").ToString() == "0" ? "0" : "1";

        }
        /// <summary>  判断分组下是否有子节点
        /// 
        /// </summary>
        /// <param name="Guid"></param>
        /// <returns></returns>
        public static string IsParent(string Guid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet doc = db.ExecuteDataSet("select guid from Sys_usergroups where TopGuid='" + Guid + "' union SELECT u.UserGuid FROM dbo.Sys_users u,Sys_userandgroup ug WHERE u.UserGuid=ug.UserGuid AND ug.UserGroupGuid='" + Guid + "'");

            return doc.Tables[0].Rows.Count > 0 ? "true" : "false";
        }
        /// <summary> 拖拽节点改变父节点        
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="parentguid">新的父节点主键</param>
        /// <param name="nodetype"></param>
        /// <param name="formerPid">原来的父节点主键</param>
        public static string ChangePreGuid(string guid, string parentguid, string nodetype, string formerPid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "";
            if (nodetype == "group")
            {

                sql = "update Sys_usergroups set TopGuid='" + parentguid + "' where Guid in (" + guid + ")";

                return db.ExecuteNonQuery(sql).ToString() == "0" ? "0" : "1";
            }
            else
            {

                return db.ExecuteNonQuery(" update Sys_userandgroup set UserGroupGuid='" + parentguid + "' where UserGuid in (" + guid + ") and UserGroupGuid='" + formerPid + "'").ToString() == "0" ? "0" : "1";
            }

        }
        /// <summary>
        /// 一次加载所有节点
        /// </summary>
        /// <returns></returns>
        public static string GetGroupsAndUsersOneTime()
        {

            Database db = DatabaseFactory.CreateDatabase();
            string sql = "select Guid,Name,TopGuid pId,type='group',icon='../../Images/group.png' from Sys_usergroups union all select u.UserGuid,u.UserCN UserName,ug.UserGroupGuid pId,type='user',icon='../../Images/user.png' from Sys_users u, Sys_userandgroup ug,Sys_usergroups g" +
                         " where u.UserGuid = ug.UserGuid AND g.guid=ug.usergroupguid";
           
            DataSet doc = db.ExecuteDataSet(sql);
            sql = "";
            if (doc.Tables[0].Rows.Count > 0)
            {
                #region 非递归实现全部加载
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    string node = "";
                    string pid = doc.Tables[0].Rows[i][2].ToString();
                    if (pid == "")
                        pid = "-1";
                    if (doc.Tables[0].Rows[i][3].ToString() != "group")
                        node = string.Format("{{id:'{0}',pId:'{1}',name:'{2}',type:'{3}',icon:'{4}'}},", doc.Tables[0].Rows[i][0].ToString(), pid, doc.Tables[0].Rows[i][1].ToString(), doc.Tables[0].Rows[i][3].ToString(), doc.Tables[0].Rows[i][4].ToString());
                    else
                        node = string.Format("{{id:'{0}',pId:'{1}',name:'{2}',type:'{3}',icon:'{4}'}},", doc.Tables[0].Rows[i][0].ToString(), pid, doc.Tables[0].Rows[i][1].ToString(), doc.Tables[0].Rows[i][3].ToString(), doc.Tables[0].Rows[i][4].ToString());

                    sql += node;

                }
                sql = sql.TrimEnd(',');
                #endregion
                #region 递归实现全部加载
                //DataRow[] dt = doc.Tables[0].Select("type='group'");
                //for (int i = 0; i < dt.Length; i++)
                //{
                //    if (dt[i][2].ToString().Length > 0)
                //        continue;
                //    string node="";
                //   string childnode = GetNodeOfGroup(doc, dt[i][0].ToString());
                //   if (childnode.Length > 0)
                //   {
                //       node = string.Format("{{id:'{0}',pId:'{1}',name:'{2}',type:'{3}',icon:'{4}',isParent:true}},", dt[i][0], "-1", dt[i][1], dt[i][3], dt[i][4]);
                //       sql += node + childnode;
                //   }
                //   else
                //   {
                //       node = string.Format("{{id:'{0}',pId:'{1}',name:'{2}',type:'{3}',icon:'{4}'}},", dt[i][0], "-1", dt[i][1], dt[i][3], dt[i][4]);
                //       sql += node ;
                //   }                   

                //}
                //sql = sql.TrimEnd(',');
                #endregion
            }

            return "[" + sql + "]";
        }
        /// <summary>
        /// 递归获取组下面的节点信息
        /// </summary>
        /// <returns></returns>
        public static string GetNodeOfGroup(DataSet doc, string groupGuid)
        {
            string json = "";
            DataRow[] dt = doc.Tables[0].Select("pId='" + groupGuid + "'");
            for (int i = 0; i < dt.Length; i++)
            {
                string node = "";
                if (dt[i][3].ToString() == "group")
                {
                    string childrennode = GetNodeOfGroup(doc, dt[i][0].ToString());
                    if (childrennode.Length > 0)
                    {
                        node = string.Format("{{id:'{0}',pId:'{1}',name:'{2}',type:'{3}',icon:'{4}',isParent:true}},", dt[i][0], dt[i][2], dt[i][1], dt[i][3], dt[i][4]);
                        node += childrennode;
                    }
                    else
                    {
                        node = string.Format("{{id:'{0}',pId:'{1}',name:'{2}',type:'{3}',icon:'{4}'}},", dt[i][0], dt[i][2], dt[i][1], dt[i][3], dt[i][4]);

                    }
                }
                else
                {
                    node = string.Format("{{id:'{0}',pId:'{1}',name:'{2}',type:'{3}',icon:'{4}'}},", dt[i][0], dt[i][2], dt[i][1], dt[i][3], dt[i][4]);
                }
                json += node;
            }
            return json;
        }
        /// <summary>
        /// 把用户添加到分组下
        /// </summary>
        /// <param name="userguid"></param>
        /// <param name="groupguid"></param>
        /// <returns></returns>
        public static string AddUserToGroup(string userguid, string groupguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //db.ExecuteNonQuery("insert into Proj_userandgroup (UserGuid,UserGroupGuid) select '"+userguid+"','"+groupguid+"'");
            return db.ExecuteNonQuery("update Sys_users set Usergroupguid='" + groupguid + "' where UserGuid in (" + userguid + ")").ToString() == "0" ? "0" : "1";

        }
        /// <summary>  通过用户Guid获取用户属性信息
        /// 
        /// </summary>
        /// <param name="userguid"></param>
        /// <returns></returns>
        public static string GetUserPropertyByGuid(string userguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string json = "select g.Name,u.UserGuid,UserDN,UserCN,UserLoginName,UserType,UserArea from Sys_users u,Sys_usergroups g,Sys_userandgroup ug where u.Userguid='" + userguid + "' AND ug.userguid=u.userguid and ug.UserGroupGuid=g.guid";
            DataSet doc = db.ExecuteDataSet(json);
            // var ret="";
            //ret = "[{group:'基本',child:[{name:'菜单名称',value:'未命名',filed:'Name'}";
            //ret += ",{name:'图标样式',value:'icon-menuItem',filed:'IconCls',readnoly:true}";
            //ret += ",{name:'图片链接',value:'',filed:'ImgUrl'}";
            //ret += ",{name:'菜单标志',value:'',filed:'Sign'}";
            //ret += ",{name:'菜单状态',value:'',filed:'Status',select:[{name:'启用',value:0},{name:'禁用',value:1}]}]}";
            //ret += ",{group:'操作',child:[{name:'链接地址',value:'',filed:'Href'}";
            //ret += ",{name:'链接目标',value:'',filed:'Target',select:[{name:'_self',value:'_self'},{name:'_blank',value:'_blank'},{name:'_top',value:'_top'},{name:'_parent',value:'_parent'}]}";
            //ret += ",{name:'脚本方法',value:'',filed:'JSEvent'}";
            //ret += "]}]";
            json = "[]";
            if (doc.Tables[0].Rows.Count > 0)
            {
                json = "[{group:'用户信息',child:[";
                json += "{name:'用户主键',value:'" + doc.Tables[0].Rows[0]["UserGuid"].ToString() + "',filed:'UserGuid',readonly:true},";
                json += "{name:'用户CN',value:'" + doc.Tables[0].Rows[0]["UserCN"].ToString() + "',filed:'UserCN',readonly:true},";
                json += "{name:'用户登录名',value:'" + doc.Tables[0].Rows[0]["UserLoginName"].ToString() + "',filed:'UserLoginName',readonly:true},";
                json += "{name:'用户类型',value:'" + doc.Tables[0].Rows[0]["UserType"].ToString() + "',filed:'UserType',readonly:true},";
                json += "{name:'行政区划',value:'" + doc.Tables[0].Rows[0]["UserArea"].ToString() + "',filed:'UserArea',readonly:false,select:[";

                List<Mapping> mp = Mapping.GetMappingByGuid("89c96a5c-e2e4-4438-a44a-31c6905effb2").OrderBy(m => m.Guid).ToList();
                foreach (var m in mp)
                {
                    json += "{name:'" + m.Name + "',value:'" + m.Guid + "'},";
                }
                json = json.TrimEnd(',');
                json += "]}]}]";
            }
            return json;
        }
        /// <summary>
        /// 通过分组Guid获取分组属性信息
        /// </summary>
        /// <param name="groupguid"></param>
        /// <returns></returns>
        public static string GetGroupPropertyByGroupGuid(string groupguid, string propertynodePid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string json = "select '上级分组'=g2.name, g1.Guid,g1.Name,g1.SortNum,g1.Property  from Sys_usergroups g1,Sys_usergroups g2 where g1.guid ='" + groupguid + "' and g1.topguid=g2.guid";
            if (propertynodePid == "-1")
            {
                json = "select '上级分组'= '根节点',Guid,Name,SortNum,Property from Sys_usergroups where Guid='" + groupguid + "'";
            }
            DataSet doc = db.ExecuteDataSet(json);
            json = "[]";
            if (doc.Tables[0].Rows.Count > 0)
            {
                json = "[{group:'用户组信息',child:[";
                json += "{name:'用户组主键',value:'" + doc.Tables[0].Rows[0]["Guid"].ToString() + "',filed:'Guid',readonly:true},";
                json += "{name:'用户组名',value:'" + doc.Tables[0].Rows[0]["Name"].ToString() + "',filed:'Name',readonly:true}";
                json += "]}]";
            }
            return json;
        }
        /// <summary>
        /// 通过分组Guid和拓展属性信息Property更新分组扩展属性信息
        /// </summary>
        /// <returns></returns>
        public static string UpdateGroupProperty(string groupguid, string property)
        {
            Database db = DatabaseFactory.CreateDatabase();
            property = StringToXMl(property);
            return db.ExecuteNonQuery("update Sys_usergroups set Property='" + property + "' where Guid='" + groupguid + "' ").ToString() == "0" ? "0" : "1";


        }
        /// <summary>
        /// 将字符串转换成xml字段
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToXMl(string str)
        {
            string xmlstr = "<xml>";
            if (str.Length > 0)
            {
                string[] xml = str.Split(',');
                for (int i = 0; i < xml.Length; )
                {
                    xmlstr += "<" + xml[i] + ">" + xml[i + 1] + "</" + xml[i] + ">";
                    i = i + 2;
                }
            }
            xmlstr += "</xml>";
            return xmlstr;
        }

        public static string UpdataUserInfo(string guid, string filed, string value)
        {
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteNonQuery("update Sys_Users set " + filed + " = '" + value + "' where guid='" + guid + "'").ToString();
        }
    }
}
