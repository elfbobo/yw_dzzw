using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.SupportCore;
using System.Threading;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// AuthorityOpt 的摘要说明
    /// </summary>
    public class AuthorityOpt : IHttpHandler
    {
        string strMenuRoleXML = string.Empty;
        string Opt = string.Empty;
        string strLicenseRoleXml = string.Empty;
        string strLicenseMenuXml = string.Empty;
        string strLicenseModuleXml = string.Empty;
        string strLicenseType = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            Opt = context.Request["opt"] == null ? "" : context.Request["opt"];
            try
            {
                switch (Opt)
                {
                    case "Menu":
                        strMenuRoleXML = context.Request["MenuRoleXML"] == null ? "" : context.Request["MenuRoleXML"];
                        AuthorityCore licenses = new AuthorityCore();
                        context.Response.Clear();
                        context.Response.Write(licenses.GetUnionMenu(strMenuRoleXML) + "&" );//AuthorityOpt
                        context.Response.End();
                        break;
                    case "License":
                        strLicenseRoleXml =context.Request["RoleXML"] == null ? "" : context.Server.UrlDecode(  context.Request["RoleXML"]);
                        strLicenseMenuXml = context.Request["MenuXML"] == null ? "" :  context.Server.UrlDecode( context.Request["MenuXML"]);
                        strLicenseModuleXml = context.Request["ModuleXML"] == null ? "" : context.Request["ModuleXML"];
                        strLicenseType = context.Request["Action"] == null ? "" : context.Request["Action"];
                        Licenses(strLicenseRoleXml, strLicenseMenuXml, strLicenseModuleXml, strLicenseType);
                        context.Response.Clear();
                        context.Response.Write(true);
                        context.Response.End();
                        break;
                }
            }
            catch (ThreadAbortException eb)
            {

            }
            catch
            {
                context.Response.Clear();
                context.Response.Write(false);
                context.Response.End();
            }
        }

        void Licenses(string roleNodeXml, string menuNodeXml, string modulXml, string type)
        {
            if (roleNodeXml != "")
            {
                AuthorityCore licenses = new AuthorityCore();
                if (type == "3")
                {
                    if (menuNodeXml != "")
                    {
                        licenses.DeleteMenuLicenses(roleNodeXml, menuNodeXml, type);
                    }
                    if (modulXml != "")
                    {
                        licenses.DeleteModelLicenses(roleNodeXml, modulXml, type);
                    }
                }
                else
                {
                    licenses.Licenses(roleNodeXml, menuNodeXml, modulXml, type);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}