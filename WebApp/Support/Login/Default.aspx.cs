using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SSOLib.Cryptography;
using Yawei.DataAccess;

namespace Yawei.App.Support.Login
{
    public partial class Default : System.Web.UI.Page
    {
        public string jquery = "", userguid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                string username = Request["username"] == null ? "" : Request["username"];
                string ps = Request["password"] == null ? "" : Request["password"];
                if (username != "" && ps != "")
                {
                    if (ValidUser(username.Replace("@qingdao.gov.cn", ""), ps))
                    {
                        //if (!string.IsNullOrWhiteSpace(Request["url"]))
                        //    Response.Redirect(Request.QueryString["url"]);
                        //else
                        Response.Redirect("../../default.aspx");
                    }
                    else
                    {
                        jquery = "alert('用户名或密码错误');";
                    }
                }
            }
        }

        public bool ValidUser(string username, string password)
        {
            string userType = ConfigurationManager.AppSettings["SystemUser"];
            bool ret = false;
            if (userType.ToLower() == "db")
            {
                string Key = ConfigurationManager.AppSettings["Key"];
                string IV = ConfigurationManager.AppSettings["IV"];
                string mailAddress = username.Replace("'", "").Replace(",", "").Replace(" ", "") + "@qingdao.gov.cn";

                Encrypter en = new Encrypter(Key, IV);

                string mailPass = en.EncryptString(password);
                string strSQL = "SELECT * FROM Users WHERE mailAddress='" + mailAddress + "' AND mailPass='" + mailPass + "'";

                Database db = DatabaseFactory.CreateDatabase("SystemPerson");
                DataSet ds = db.ExecuteDataSet("select * from Users where mailAddress='" + mailAddress + "' AND mailPass='" + mailPass + "'");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Response.Cookies.Clear();
                    HttpCookie cookie = new HttpCookie("CurrentUserInfo");
                    cookie.HttpOnly = true;
                    cookie.Values.Add("CurrentUserGuid", ds.Tables[0].Rows[0]["guid"].ToString());
                    userguid = ds.Tables[0].Rows[0]["guid"].ToString();
                    cookie.Values.Add("CurrentUserID", ds.Tables[0].Rows[0]["mailAddress"].ToString().Replace("@qingdao.gov.cn", ""));
                    cookie.Values.Add("CurrentUserCN", HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["TITLE"].ToString()));
                    cookie.Values.Add("CurrentUserDN", HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["USERPATH"].ToString()));
                    Response.AppendCookie(cookie);
                    ret = true;


                }
            }
            else if (userType.ToLower() == "ld")
            {
                Database db = DatabaseFactory.CreateDatabase();
                DataSet ds = db.ExecuteDataSet("select * from Sys_LocalUser where UserName='" + username + "' AND Password='" + password + "'");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Response.Cookies.Clear();
                    HttpCookie cookie = new HttpCookie("CurrentUserInfo");
                    cookie.HttpOnly = true;
                    cookie.Values.Add("CurrentUserGuid", ds.Tables[0].Rows[0]["guid"].ToString());
                    cookie.Values.Add("CurrentUserCN", HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["UserTitle"].ToString()));
                    Response.AppendCookie(cookie);
                    ret = true;
                }
            }


            return ret;
        }
    }
}