using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.SupportCore
{
    public static class RolesManageCore
    {
        /// <summary>
        /// 根据角色名称或人员名称查询角色和用户。
        /// </summary>
        public static string QueryRolesAndUsers()
        {
            return null;
        }
        /// <summary>  将用户分组中的数据拖拽到角色分组下
        /// 
        /// </summary>
        /// <param name="userguids">拖拽的用户主键集合</param>
        /// <param name="groupguids">拖拽的分组主键集合</param>
        /// <param name="projguid">项目主键</param>
        /// <param name="roleguid">目标角色主键</param>
        public static string DropGroupToRoles(string userguids, string groupguids, string roleguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            if (userguids != "")    //拖拽中有用户
            {
                db.ExecuteNonQuery("delete from Sys_RoleUsers where roleguid='" + roleguid + "' and userguid in (" + userguids + ")"); //删除当前角色下已经存在的拖拽来的用户的记录
                db.ExecuteNonQuery("insert into Sys_RoleUsers(userguid,roleguid) select userguid,'" + roleguid + "' from Sys_users where userguid in (" + userguids + ")");
            }
            if (groupguids.Length > 2)         //拖拽中有用户分组
            {
                db.ExecuteNonQuery("delete from Sys_RoleUsers where roleguid='" + roleguid + "' and userguid in (select userguid from Sys_UserAndGroup where usergroupguid in " + groupguids + ")"); //删除当前角色下已经存在的拖拽来的分组下的用户的记录

                db.ExecuteNonQuery("insert into Sys_RoleUsers(userguid,roleguid) select userguid,'" + roleguid + "' from Sys_UserAndGroup where usergroupguid in " + groupguids + "");
            }
            return "";
        }
        /// <summary>  将角色中的数据拖拽到角色分组下
        /// 
        /// </summary>
        /// <param name="userguids">拖拽的用户主键集合</param>
        /// /// <param name="userpids">拖拽的用户所属角色主键集合</param>
        /// <param name="groupguids">拖拽的角色主键集合</param>
        /// <param name="projguid">项目主键</param>
        /// <param name="roleguid">目标角色主键</param>
        public static string DropRolesToRoles(string userguids, string userpids, string groupguids, string roleguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            if (userguids != "")    //拖拽中有用户
            {
                db.ExecuteNonQuery(" delete from Sys_RoleUsers where roleguid='" + roleguid + "' and userguid in (" + userguids + ")");//删除本角色下与拖拽来的用户重复的数据
                db.ExecuteNonQuery("update Sys_RoleUsers set roleguid='" + roleguid + "' where userguid in (" + userguids + ") and roleguid in (" + userpids + ")");
            }
            if (groupguids.Length > 2)         //拖拽中有用户分组
            {
                db.ExecuteNonQuery("update Sys_Roles set topguid ='" + roleguid + "' where guid in " + groupguids + "");
            }
            return "";
        }
        /// <summary>
        /// 根据角色主键递归删除角色和用户。
        /// </summary>
        public static string DeleteRoles(string roleguid, string userguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            if (userguid != "")
            {
                db.ExecuteNonQuery("delete from Sys_RoleUsers where roleguid in( " + roleguid + ") and UserGuid in (" + userguid + ")");//取消该角色中的所有用户
            }
            //删除角色
            return db.ExecuteNonQuery("delete from Sys_Roles where guid in (" + roleguid + ")").ToString() == "0" ? "0" : "1";
        }




        /// <summary>
        /// 添加一个角色。
        /// </summary>
        public static string CreateRoles(string guid, string topGuid, string name, string code)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //string codecnt = db.ExecuteScalar("select count(*) from Sys_Roles where code='" + code + "' and projguid='" + projguid + "'").ToString();
            string sql = "INSERT INTO Sys_Roles (Guid,Name,TopGuid,Code) VALUES ('" + guid + "','" + name + "','" + topGuid + "','" + code + "')";

            //if (codecnt == "0")
            return db.ExecuteNonQuery(sql).ToString();
            //else
            //    return "false";
        }

        /// <summary>
        /// 修改一个角色。
        /// </summary>
        public static string ModifyRole(string guid, string name)
        {
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteNonQuery("update Sys_Roles set name='" + name + "' where guid='" + guid + "'").ToString();
        }


        /// <summary>
        /// 将人员加入角色。
        /// </summary>
        public static string AddUserToRole(string userguid, string roleguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteNonQuery("update Sys_Roles set RoleGuid='" + roleguid + "' where UserGuid in (" + userguid + ")").ToString() == "0" ? "0" : "1";
        }

        /// <summary>
        /// 将人员从角色移除。
        /// </summary>
        public static string RemoveUserFromRole(string roleguid, string userguid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteNonQuery("delete from Sys_RoleUsers where roleguid ='" + roleguid + "' and UserGuid ='" + userguid + "'").ToString() == "0" ? "0" : "1";
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
        /// 获取分组下用户数据
        /// </summary>
        /// <param name="groupGuid"></param>
        /// <returns></returns>
        public static string getnodeuser(string roleGuid, string ishasgroup)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select UserGuid,LoginName from Sys_RoleUsers where RoleGuid='" + roleGuid + "'";
            DataSet doc = database.ExecuteDataSet(sql);
            sql = ishasgroup;
            if (doc.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    string node = string.Format("{{id:'{0}',pId:'{2}',name:'{1}',type:'user',icon:'../../Images/user.png'}},", doc.Tables[0].Rows[i][0].ToString(), doc.Tables[0].Rows[i][1].ToString(), roleGuid);
                    sql += node;
                }
            }
            return sql.TrimEnd(',');
        }
        /// <summary>
        /// 拖拽节点改变父节点roleTree
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="parentguid"></param>
        /// <param name="nodetype"></param>
        public static string ChangePreGuid(string guid, string parentguid, string nodetype)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "";
            if (nodetype == "group")
            {
                if (parentguid != "-1")
                {
                    sql = "update Sys_Roles set TopGuid='" + parentguid + "' where Guid in (" + guid + ")";
                }
                else
                {
                    sql = "update Sys_Roles set TopGuid ='-1' where Guid in (" + guid + ")";
                }
                return db.ExecuteNonQuery(sql).ToString() == "0" ? "0" : "1";
            }
            else        //
            {
                return db.ExecuteNonQuery("update Sys_RoleUsers set RoleGuid='" + parentguid + "' where UserGuid in(" + guid + ")").ToString() == "0" ? "0" : "1";
            }

        }

        /// <summary>
        /// 拖拽节点授权
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="parentguid"></param>
        /// <param name="nodetype"></param>
        public static string GiveRole(string guid, string parentguid, string nodetype)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string codecnt = db.ExecuteScalar("select count(*) from Sys_RoleUsers where UserGuid=" + guid + " and RoleGuid='" + parentguid + "'").ToString();
            string sql1 = "insert into Sys_RoleUsers (UserGuid,UserDN,UserCN,UserLoginName,RoleGuid) SELECT " + guid + ",UserDN,UserCN,UserLoginName,'" + parentguid + "' FROM dbo.Sys_Users WHERE UserGuid=" + guid;

            if (codecnt == "0")
                return db.ExecuteNonQuery(sql1).ToString() == "0" ? "0" : "1";
            else
                return "false";
        }
        /// <summary>   一次加载所有角色节点
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetRolesAndUsersOneTime()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "select Guid,Name,TopGuid pId,type='group',icon='../../Images/group_key.png' from Sys_Roles  union select r.UserGuid,u.UserCN Name,r.RoleGuid pId,type='user',icon='../../Images/user.png' from Sys_RoleUsers r,Sys_Users u where  r.RoleGuid in (select guid from Sys_roles ) and r.userguid=u.userguid";

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

            }

            return "[" + sql + "]";
        }


    }
}
