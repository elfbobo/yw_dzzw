using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore;

namespace Yawei.App.Support.RoleManager
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["type"] == null ? "" : Request["type"];
            string id = Request["id"] == null ? "" : Request["id"];
            string iid = Request["icon"] == null ? "" : Request["icon"] == "undefined" ? "" : Request["icon"];

            if (id == "-1" && iid == "")
            {
                id = RolesManageCore.GetRolesAndUsersOneTime();//一次加载所有数据--角色
                Response.Clear();
                Response.Write(id);
                Response.Flush();
                Response.End();
            }
            else if (id == "-2" && iid == "")
            {
                id = UserManageCore.GetGroupsAndUsersOneTime();//一次加载所有数据--分组
                Response.Clear();
                Response.Write(id);
                Response.Flush();
                Response.End();
            }
            else if (type != "")
            {
                string guid = Request["guid"],
                parentguid = Request["parentguid"],
                name = Request["name"],
                nodetype = Request["nodetype"],
                groupsguid = Request["groupsguid"], //分组guid
                dropfromtype = Request["dropfromtype"],//拖动来源树 
                userpids = Request["userpids"] ?? "",//从角色树拖拽过来的用户的上级角色主键集合
                moveType = Request["moveType"];

                if (type == "addrole")
                {
                    guid = RolesManageCore.CreateRoles(guid, parentguid, name, nodetype);
                }
                else if (type == "update")
                {
                    if (parentguid == "roleTree")
                    {
                        guid = RolesManageCore.ModifyRole(guid, name);
                    }

                }
                else if (type == "delete")    //删除
                {
                    if (moveType == "roleTree")
                    {
                        if (nodetype == "group")
                        {
                            guid = RolesManageCore.DeleteRoles(guid, parentguid);
                        }
                        else if (nodetype == "user")
                        {
                            guid = RolesManageCore.RemoveUserFromRole(parentguid, guid);
                        }
                    }

                }
                else if (type == "drop") //拖拽
                {

                    if (dropfromtype == "group")   //用户树往角色树拖动
                    {
                        guid = RolesManageCore.DropGroupToRoles(guid, groupsguid, parentguid);
                    }
                    else if (dropfromtype == "role") //角色自身拖动
                    {
                        guid = RolesManageCore.DropRolesToRoles(guid, userpids, groupsguid, parentguid);
                    }
                }
                Response.Clear();
                Response.Write(guid);
                Response.Flush();
                Response.End();
            }
        }
    }
}