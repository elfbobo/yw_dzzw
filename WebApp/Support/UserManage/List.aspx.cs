using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore;

namespace Yawei.App.Support.UserManage
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax.Utility.RegisterTypeForAjax(typeof(List));

            string type = Request["type"] == null ? "" : Request["type"];
            string id = Request["id"] == null ? "" : Request["id"];
            string iid = Request["icon"] == null ? "" : Request["icon"] == "undefined" ? "" : Request["icon"];
            string sguid = Request["sguid"] == null ? "" : Request["sguid"];//通过Guid加载用户属性信息
            string pid = Request["pid"] == null ? "" : Request["pid"];//判断要获取属性的组父节点是不是根节点
            if (sguid != "")
            {
                if (type == "user")
                {

                    sguid = UserManageCore.GetUserPropertyByGuid(sguid);
                }
                else if (type == "group")
                {
                    sguid = UserManageCore.GetGroupPropertyByGroupGuid(sguid, pid);
                }
                Response.Clear();
                Response.Write(sguid);
                Response.Flush();
                Response.End();
            }

            if (id != "" && iid == "")
            {
                // id = UsersCore.GetGroupsAndUsers(id);    //异步加载节点
                id = UserManageCore.GetGroupsAndUsersOneTime();//一次加载所有数据
                Response.Clear();
                Response.Write(id);
                Response.Flush();
                Response.End();
            }
            else if (type != "")
            {
               string guid = Request["guid"];
                string parentguid = Request["parentguid"],
                   name = Request["name"],
                   nodetype = Request["nodetype"],
                  moveType = Request["moveType"],
                  formerPid = Request["formerPid"];
                if (type == "add")
                {
                    if (name != "adduser")//添加分组
                    {
                        guid = UserManageCore.CreateGroup(guid, parentguid, name);
                    }
                    else//分组下添加用户
                    {
                        guid = UserManageCore.AddUserToGroup(guid, parentguid);
                    }
                }
                else if (type == "update")
                {
                    guid = UserManageCore.ModifyGroup(guid, name);
                }
                else if (type == "delete")
                {
                    if (nodetype == "group")
                    {
                        guid = UserManageCore.DeleteGroups(parentguid, guid);
                    }
                    else if (nodetype == "user")
                    {
                        guid = UserManageCore.DeleteUsers(parentguid, guid);
                    }
                }
                else if (type == "drop") //拖拽
                {
                    if (moveType == "inner")
                    {
                        guid = UserManageCore.ChangePreGuid(guid, parentguid, nodetype, formerPid);

                    }
                    else if (moveType == "prev")
                    {
                        guid = UserManageCore.ChangePreGuid(guid, name, nodetype, formerPid);
                    }
                    else if (moveType == "next")
                    {
                        guid = UserManageCore.ChangePreGuid(guid, name, nodetype, formerPid);
                    }
                }
                else if (type == "deleteproperty") //删除属性函数
                {
                    if (parentguid == "user")
                    {
                        guid = UserManageCore.DeleteUserProperty(guid, name);
                    }
                    else
                    {
                        guid = UserManageCore.UpdateGroupProperty(guid, name);
                    }
                }
                else if (type == "addproperty")
                {
                    if (parentguid == "user")
                    {
                        guid = UserManageCore.CreateUserProperty(guid, name);
                    }
                    else
                    {
                        guid = UserManageCore.UpdateGroupProperty(guid, name);
                    }
                }
                else if (type == "area")
                {
                    guid = Request["guid"];
                    string value = Request["value"];
                    string filed = Request["filed"];
                    guid = UserManageCore.UpdataUserInfo(guid, filed, value);
                }
                Response.Clear();
                Response.Write(guid);
                Response.Flush();
                Response.End();
            }
        }
        //#region 方法
        //[Ajax.AjaxMethod]
        //public bool SaveAddUsers(string users, string addusersAndGroupsql, string groupid, string userid,bool isusercaninmoregroup)
        //{
        //    return UserManageClass.CreateProjUser(users, addusersAndGroupsql, groupid, userid,isusercaninmoregroup); ;
        //}
        //#endregion
    }
}