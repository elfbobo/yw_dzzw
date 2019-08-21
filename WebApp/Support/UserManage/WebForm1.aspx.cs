using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore;

namespace Yawei.App.Support.UserManage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // Ajax.Utility.RegisterTypeForAjax(typeof(WebForm1));

            string type = Request["type"] == null ? "" : Request["type"];
            string id = Request["id"] == null ? "" : Request["id"];
            string iid = Request["icon"] == null ? "" : Request["icon"] == "undefined" ? "" : Request["icon"];
            string propertygridGuid = Request["propertygridGuid"] == null ? "" : Request["propertygridGuid"];//通过Guid加载用户属性信息
            string propertynodePid = Request["propertynodePid"] == null ? "" : Request["propertynodePid"];//判断要获取属性的组父节点是不是根节点
            if (propertygridGuid != "")
            {
                if (type == "user")
                {

                    propertygridGuid = UserManageCore.GetUserPropertyByGuid(propertygridGuid);
                }
                else if (type == "group")
                {
                    propertygridGuid = UserManageCore.GetGroupPropertyByGroupGuid(propertygridGuid, propertynodePid);
                }
                Response.Clear();
                Response.Write(propertygridGuid);
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
                string guid = Request["guid"],
                       parentguid = Request["parentguid"],
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
                Response.Clear();
                Response.Write(guid);
                Response.Flush();
                Response.End();
            }
        }
        #region 方法
       // [Ajax.AjaxMethod]
        public bool SaveAddUsers(string users, string addusersAndGroupsql, string groupid, string userid, bool isusercaninmoregroup)
        {
            return UserManageCore.CreateProjUser(users, addusersAndGroupsql, groupid, userid, isusercaninmoregroup); ;
        }
        #endregion
    }
}