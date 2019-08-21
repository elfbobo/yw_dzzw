using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.DataAccess;
using Yawei.Domain;
using Yawei.Domain.Organization;
using System.DirectoryServices;

namespace Yawei.App.Support.UserManage
{
    public partial class ChooseUsers : System.Web.UI.Page
    {
        protected string model = "AD";
        protected string ou = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            model = "DB";

            #region 接受参数

            model = Request["Model"] != null ? Request["Model"] : model;

            #endregion

            #region 初始化页面


            #endregion

            #region 查询数据

            if (Request["Opt"] != null && Request["Opt"] == "GetUsers")
            {
                string group = Request["Group"];
                string name = Request["Name"];

                switch (model.ToUpper())
                {
                    case "AD":
                        Response.Clear();
                        Response.Write(GetADUser(group, name));
                        Response.Flush();
                        Response.End();
                        break;
                    case "DB":
                        Response.Clear();
                        Response.Write(GetDBUser(group, name));
                        Response.Flush();
                        Response.End();
                        break;
                    case "LOCALHOST":
                        Response.Clear();
                        Response.Write(GetLocalhostUser(name));
                        Response.Flush();
                        Response.End();
                        break;
                    default:
                        Response.Clear();
                        Response.Write("[]");
                        Response.Flush();
                        Response.End();
                        break;
                }
            }
            else
            {
                SetPageData();
            }

            #endregion
        }

        #region 初始化页面

        private void SetPageData()
        {
            switch (model)
            {
                case "AD":
                    Yawei.Domain.Domain domain = new Yawei.Domain.Domain();
                    DomainOU domainOU = domain.GetOU("OU=市直机关,DC=qd,DC=gov,DC=cn");
                    foreach (SearchResult searchResult in Yawei.Domain.Domain.ExecuteSearchResult(domainOU,"(objectClass=organizationalUnit)", SearchScope.OneLevel))
                    {
                        ou += "<option value=\"" + searchResult.GetDirectoryEntry().Guid + "\">" + searchResult.GetDirectoryEntry().Name.Replace("OU=", "") + "</option>";
                    }

                    break;
                case "DB":
                    Database database = DatabaseFactory.CreateDatabase("SystemPerson");
                    DataSet doc = database.ExecuteDataSet("select Guid,title from Groups where ouPath='OU=青岛市政府投资项目科技防腐平台,DC=组'");
                    foreach (DataRow dr in doc.Tables[0].Rows)
                    {
                        ou += "<option value=\"" + dr["Guid"].ToString() + "\">" + dr["title"].ToString() + "</option>";
                    }
                    break;
            }
        }

        #endregion

        #region 查询AD用户

        private string GetADUser(string gourp, string name)
        {
            string json = string.Empty;
            Yawei.Domain.Domain domain = new Yawei.Domain.Domain();

            if (name.Trim() != "")
            {
                SearchResultCollection searchResultCollection = Yawei.Domain.Domain.ExecuteSearchResult(domain.GetDirectoryEntry(domain.DistinguishedName), "(&(objectCategory=person)(objectClass=user)(CN=*" + name + "*))", SearchScope.Subtree);
                json += "{";
                json += "\"total\":" + searchResultCollection.Count.ToString() + ",\"rows\":[";
                for (int i = 0; i < searchResultCollection.Count; i++)
                {
                    DomainUser user = domain.GetUser(searchResultCollection[i].GetDirectoryEntry().Guid.ToString());
                    json += "{\"UserGuid\":\"" + user.Guid.ToString() + "\",\"UserDN\":\"" + user.DN + "\",\"UserCN\":\"" + user.CN + "\",\"UserLoginName\":\"" + user.LoginName + "\",\"UserType\":\"AD\"}";
                    if (i + 1 < searchResultCollection.Count)
                        json += ",";
                }
                json += "]}";
            }
            else
            {
               using (DomainOU domainOU = domain.GetOU(gourp))
               {
                   SearchResultCollection searchResultCollection = Yawei.Domain.Domain.ExecuteSearchResult(domainOU, "(&(objectCategory=person)(objectClass=user))", SearchScope.OneLevel); ;//Yawei.Domain.Domain.ExecuteSearchResult(domainOU, "(&(objectCategory=person)(objectClass=user))", SearchScope.OneLevel);
                    json += "{";
                    json += "\"total\":" + searchResultCollection.Count.ToString() + ",\"rows\":[";
                    for (int i = 0; i < searchResultCollection.Count; i++)
                    {
                        DomainUser user = domain.GetUser(searchResultCollection[i].GetDirectoryEntry().Guid.ToString());
                        json += "{\"UserGuid\":\"" + user.Guid.ToString()+ "\",\"UserDN\":\"" + user.DN + "\",\"UserCN\":\"" + user.CN + "\",\"UserLoginName\":\"" + user.LoginName + "\",\"UserType\":\"AD\"}";
                        if (i + 1 < searchResultCollection.Count)
                            json += ",";
                    }
                    json += "]}";
               }
            }
            return json;
        }

        #endregion

        #region 查询DB用户

        private string GetDBUser(string gourp, string name)
        {
            string json = string.Empty;
            Database database = DatabaseFactory.CreateDatabase("SystemPerson");

            if (name.Trim() != "")
            {
                DataSet doc = database.ExecuteDataSet("select Guid as UserGuid,UserPath as UserDN,title as UserCN,mailAddress as UserLoginName from Users where title like '%" + name + "%'");
                json += "{";
                json += "\"total\":" + doc.Tables[0].Rows.Count.ToString() + ",\"rows\":[";
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = doc.Tables[0].Rows[i];
                    json += "{\"UserGuid\":\"" + dr["UserGuid"].ToString() + "\",\"UserDN\":\"" + dr["UserDN"].ToString() + "\",\"UserCN\":\"" + dr["UserCN"].ToString() + "\",\"UserLoginName\":\"" + dr["UserLoginName"].ToString() + "\",\"UserType\":\"DB\"}";
                    if (i + 1 < doc.Tables[0].Rows.Count)
                        json += ",";
                }
                json += "]}";
            }
            else
            {
                DataSet doc = database.ExecuteDataSet("select UserGuid,UserPath as UserDN,title as UserCN,logonUser as UserLoginName from GroupUserRelationInfo where GroupGuid='" + gourp + "'");
                json += "{";
                json += "\"total\":" + doc.Tables[0].Rows.Count.ToString() + ",\"rows\":[";
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = doc.Tables[0].Rows[i];
                    json += "{\"UserGuid\":\"" + dr["UserGuid"].ToString() + "\",\"UserDN\":\"" + dr["UserDN"].ToString() + "\",\"UserCN\":\"" + dr["UserCN"].ToString() + "\",\"UserLoginName\":\"" + dr["UserLoginName"].ToString() + "\",\"UserType\":\"DB\"}";
                    if (i + 1 < doc.Tables[0].Rows.Count)
                        json += ",";
                }
                json += "]}";
            }
            return json;
        }

        #endregion


        #region 查询本地用户

        private string GetLocalhostUser(string name)
        {
            string json = string.Empty;
            Database database = DatabaseFactory.CreateDatabase();

            if (name.Trim() != "")
            {
                DataSet doc = database.ExecuteDataSet("select Guid as UserGuid, UserDN='', Usertitle as UserCN,UserName as UserLoginName from Sys_LocalUser where UserCN like '%" + name + "%' ");
                json += "{";
                json += "\"total\":" + doc.Tables[0].Rows.Count.ToString() + ",\"rows\":[";
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = doc.Tables[0].Rows[i];
                    json += "{\"UserGuid\":\"" + dr["UserGuid"].ToString() + "\",\"UserDN\":\"" + dr["UserDN"].ToString() + "\",\"UserCN\":\"" + dr["UserCN"].ToString() + "\",\"UserLoginName\":\"" + dr["UserLoginName"].ToString() + "\",\"UserType\":\"LcalHost\"}";
                    if (i + 1 < doc.Tables[0].Rows.Count)
                        json += ",";
                }
                json += "]}";
            }
            else
            {
                DataSet doc = database.ExecuteDataSet("select Guid as UserGuid, UserDN='', Usertitle as UserCN,UserName as UserLoginName from Sys_LocalUser  ");
                json += "{";
                json += "\"total\":" + doc.Tables[0].Rows.Count.ToString() + ",\"rows\":[";
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = doc.Tables[0].Rows[i];
                    json += "{\"UserGuid\":\"" + dr["UserGuid"].ToString() + "\",\"UserDN\":\"" + dr["UserDN"].ToString() + "\",\"UserCN\":\"" + dr["UserCN"].ToString() + "\",\"UserLoginName\":\"" + dr["UserLoginName"].ToString() + "\",\"UserType\":\"LcalHost\"}";
                    if (i + 1 < doc.Tables[0].Rows.Count)
                        json += ",";
                }
                json += "]}";
            }
            return json;
        }

        #endregion
    }
}