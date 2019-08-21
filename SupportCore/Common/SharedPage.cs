using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using Yawei.Domain;
using Yawei.Domain.Organization;
using Yawei.SSOLib.Common;
using Yawei.SupportCore.SupportApi.Entity;

namespace Yawei.Common
{
    public class SharedPage : System.Web.UI.Page
    {
        #region 属性

        public string MCode { get; set; }

        public CurrentUser CurrentUser { get; private set; }

        #endregion

        #region 初始化方法
        /// <summary>
        /// 初始化页面属性及操作。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {

            try
            {
                string userType = ConfigurationManager.AppSettings["SystemUser"];

                switch (userType.ToUpper())
                {
                    case "AD":
                        if (Request.Cookies["CurrentUserInfo"] == null)
                        {
                            TicketManager ticketManager = new TicketManager();
                            if (ticketManager.LoadTicket(ConfigurationManager.AppSettings["domain"]))
                            {
                                Yawei.Domain.Domain domain = new Yawei.Domain.Domain();
                                using (DomainUser domainUser = domain.GetUser(ticketManager.ADGuid))
                                {
                                    CurrentUser.SetCookieUserInfo(ticketManager.ADGuid, domainUser.CN, domainUser.DN, ticketManager.Userid);
                                }
                                GetUser(ticketManager.ADGuid);
                                SetOnlinUser();
                            }
                            //else
                            //    Response.Redirect(AppSupport.AppPath + "/SystemOption/lock.html");

                        }
                        else
                        {
                            var guid = Request.Cookies["CurrentUserInfo"].Values["CurrentUserGuid"];
                            GetUser(guid);
                            SetOnlinUser();
                        }
                        break;
                    case "LD":
                    case "DB":
                        if (Request.Cookies["CurrentUserInfo"] == null)
                        {
                            Response.Redirect(AppSupport.AppPath + "/Support/Login/Default.aspx?url=" + Server.UrlEncode(Request.Url.ToString()));
                        }
                        else
                        {
                            var guid = Request.Cookies["CurrentUserInfo"].Values["CurrentUserGuid"];
                            GetUser(guid);
                            SetOnlinUser();
                        }

                        break;
                }


                //if (string.IsNullOrWhiteSpace(CurrentUser.UserGuid) || (CurrentUser.UserRole == null))
                //    Response.Redirect(AppSupport.AppPath + "/SystemOption/lock.html");



                //CurrentUser = new CurrentUser("755dd04f-bb9d-44e0-b2f5-07c3986123fb");

            }
            catch (ThreadAbortException exc_)
            {

            }
            catch (Exception ex)
            {



            }


            base.OnLoad(e);

        }

        #endregion

        #region 其他方法


        void GetUser(string guid)
        {
            if (CurrentUser == null)
            {
                if (Session["CurrentUser"] != null)
                {
                    CurrentUser currentUser = Session["CurrentUser"] as CurrentUser;
                    if (currentUser.UserGuid == guid)
                        CurrentUser = currentUser;
                    else
                    {
                        CurrentUser = new CurrentUser(guid);
                        CurrentUser.LoginDate = DateTime.Now;
                        Session["CurrentUser"] = CurrentUser;
                    }
                }
                else
                {
                    CurrentUser = new CurrentUser(guid);
                    CurrentUser.LoginDate = DateTime.Now;
                    Session["CurrentUser"] = CurrentUser;
                }
            }

        }

        void SetOnlinUser()
        {
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["onlineuser"].ToLower() == "true")
            {
                if (System.Web.HttpContext.Current.Application["CurrentUser"] != null)
                {
                    List<CurrentUser> cuList = System.Web.HttpContext.Current.Application["CurrentUser"] as List<CurrentUser>;
                    if (cuList.Count(l => l.UserGuid == CurrentUser.UserGuid) <= 0)
                    {
                        cuList.Add(CurrentUser);
                        System.Web.HttpContext.Current.Application["CurrentUser"] = cuList;
                    }
                }
                else
                {
                    var cuList = new List<CurrentUser>();
                    cuList.Add(CurrentUser);
                    System.Web.HttpContext.Current.Application["CurrentUser"] = cuList;
                }
            }
        }
        #endregion


        protected string GetProjectByCUser()
        {
            string sql = "";
            string deptGuid = CurrentUser.UserGroup.Guid;
            if (!CurrentUser.IsHasRole(new string[] { Common.AppSupport.adminGuid, Common.AppSupport.spvGuid, Common.AppSupport.dzzwGuid, Common.AppSupport.IdyMageGuid,Common.AppSupport.industryMage }))
                //sql += "  and  guid in(select ProjGuid from Busi_ProjRegisterDeparts where deptGuid='" + deptGuid + "' and (depttype='ConstructionUnitName' or depttype='Department' or depttype='Department') ) ";
                sql += " and ( CHARINDEX('" + deptGuid + "', ConstructionUnitGuid)>0  or  CHARINDEX('" + deptGuid + "', DepartmentGuid)>0 or  CHARINDEX('" + deptGuid + "', AdminNameGuid)>0 or  CHARINDEX('" + deptGuid + "', UsedUnitGuid)>0 or  CHARINDEX('" + deptGuid + "', fgfgcs)>0) ";
                return sql;
        }




    }
}
