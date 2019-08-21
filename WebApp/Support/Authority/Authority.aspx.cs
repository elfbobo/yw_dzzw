using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore;

namespace Yawei.App.Support.Authority
{
    public partial class Authority : System.Web.UI.Page
    {
        protected string menuJson = "";
        protected string roleJson = "";
        protected string moudleJson = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax.Utility.RegisterTypeForAjax(typeof(Authority));
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            #region 加载Tree

            AuthorityCore licenses = new AuthorityCore();

            //菜单
            menuJson = licenses.GetMenu();


            // 角色
            roleJson = licenses.GetRole();

            //模块
          //  moudleJson = licenses.GetModules();
            #endregion
        }

        //[Ajax.AjaxMethod]//jwj 2014-08-06
        //public void Licenses(string roleNodeXml, string menuNodeXml, string modulXml, string type)
        //{
        //    if (roleNodeXml != "")
        //    {
        //        AuthorityCore licenses = new AuthorityCore();
        //        if (type == "3")
        //        {
        //            if (menuNodeXml != "")
        //            {
        //                licenses.DeleteMenuLicenses(roleNodeXml, menuNodeXml, type);
        //            }
        //            if (modulXml != "")
        //            {
        //                licenses.DeleteModelLicenses(roleNodeXml, modulXml, type);
        //            }
        //        }
        //        else
        //        {
        //            licenses.Licenses(roleNodeXml, menuNodeXml, modulXml, type);
        //        }
        //    }
        //}


        //[Ajax.AjaxMethod]//jwj 2014-08-06
        //public string GetUnionMenuGuid(string roleXML)
        //{

        //    AuthorityCore licenses = new AuthorityCore();


        //    return licenses.GetUnionMenu(roleXML) + "&" + licenses.GetUnionModel(roleXML);

        //}
    }
}