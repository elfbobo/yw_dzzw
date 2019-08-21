using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore;
using Yawei.DataAccess;

namespace WebApp
{
    public partial class DefaultZJ : Yawei.Common.SharedPage
    {
        protected string zjmc = "";
        protected string zjguid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            zjmc = this.Request.Params["zjmc"] ?? "";
            zjguid = this.Request.Params["zjguid"] ?? "";
            string type = ConfigurationManager.AppSettings["SystemUser"];

            #region //登录日志  登录人GUID CN DN  登录人IP 登录时间 用户类型[AD/DB]

            SysFormCore core = new SysFormCore();
            HttpCookie cookie = Request.Cookies["CurrentUserInfo"];
            if (type.ToUpper() == "DB")
            {
                cookie.Values["CurrentUserGuid"] = System.Web.HttpUtility.UrlDecode(cookie.Values["CurrentUserGuid"]);
                cookie.Values["CurrentUserCN"] = System.Web.HttpUtility.UrlDecode(cookie.Values["CurrentUserCN"]);
                cookie.Values["CurrentUserDN"] = System.Web.HttpUtility.UrlDecode(cookie.Values["CurrentUserDN"]);

            }
            var dataBase = DatabaseFactory.CreateDatabase();
            dataBase.ExecuteNonQuery("insert into Sys_LoginLog select '" + Guid.NewGuid() + "','" + cookie.Values["CurrentUserGuid"] + "','" + cookie.Values["CurrentUserCN"] + "','" + cookie.Values["CurrentUserDN"] + "','" + GetIp() + "','" + DateTime.Now + "','" + type + "'");
            #endregion
            //switch (type.ToUpper())
            //{
            //    case "AD":
            //        if (HasUserRoles(Yawei.Business.Common.App.maxRoles))
            //        {

            //            Response.Redirect("ProjectGeneral/mainindex.aspx");

            //        }
            //        else
            //        {
            //            Response.Redirect("Business/baseinfo/list.aspx");
            //            //js = "window.open(\"../Notice.htm\");";
            //        }
            //        break;
            //    case "DB":
            //        if (cookie != null)
            //        {
            //            if (HasUserRoles(Yawei.Business.Common.App.maxRoles))
            //            {

            //                Response.Redirect("ProjectGeneral/mainindex.aspx");

            //            }
            //            else
            //            {
            //                Response.Redirect("Business/baseinfo/list.aspx");
            //                //js = "window.open(\"../Notice.htm\");";
            //            }

            //        }
            //        else
            //        {
            //            Response.Redirect("login/login.aspx");
            //        }
            //        break;
            //}

        }
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        protected string GetIp()
        {


            return Page.Request.UserHostAddress;
        }
    }
}