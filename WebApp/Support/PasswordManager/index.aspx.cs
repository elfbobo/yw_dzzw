using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using Yawei.DataAccess;


namespace WebApp.Support.PassWordManager
{
    public partial class Index : SharedPage
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Database database = DatabaseFactory.CreateDatabase();
                DataSet ds = database.ExecuteDataSet("select Password from Sys_LocalUser where guid='" + CurrentUser.UserGuid + "'");

                string pswd = ds.Tables[0].Rows[0]["password"].ToString();

                string oldpswd = Request.Params["Pass1"] ?? "";
                string newpswd = Request.Params["Pass2"] ?? "";
                string newpswdAgain = Request.Params["Pass3"] ?? "";
                if (pswd != oldpswd)
                {
                    Response.Write("<script>alert('旧密码输入有误，请重新输入!')</script>");
                }
                else
                {
                    database.ExecuteNonQuery("update  Sys_LocalUser set Password='" + newpswd + "'  where guid='" + CurrentUser.UserGuid + "'");
                    Response.Write("<script>alert('修改成功!')</script>");
                }
            }
        }
    }

}