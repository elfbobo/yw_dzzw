using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.Common;
using System.Xml;
using Yawei.FacadeCore.Project;
using System.Web.Configuration;
using Yawei.FacadeCore;
using System.Data;


namespace WebApp.Project
{
    public partial class ProjectForm : SharedPage
    {
        public string menuJson = "";
        public string projJson = "[]";
        Dictionary<string, string> dic = new Dictionary<string, string>();
        string C = "";//市财力
        string P = "";//省级投资
        string N = "";//中央投资
        string S = "";//审核
        string H = "";//核准
        string B = "";//备案
        string ititle1 = "";//投资批复
        string ititle2 = "";//投资信息
        string title3 = "其他财务信息";//其他财务信息
        string title4 = "资金到位月报";//资金到位月报
        string titile5 = "财政支出预算下达";//财政支出预算下达
        protected bool IsChecked = false;
        protected bool isShowCityExamine = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            dic = FormatProjectFacade.GetProjDicByGuid(Request["Guid"]);

            if (dic["ProGuid"] != null)
            {
                projJson = "[{ProGuid:'" + dic["ProGuid"] + "',ProName:'" + dic["ProName"] + "'}]";
            }
            XmlDocument xml = CurrentUser.GetMenuXml("e946a8f9-fcd0-4897-bad4-b0e7f2e680c5");
            menuJson = GetNodeJson(xml.FirstChild, Yawei.Common.AppSupport.AppPath);

            FinanceDBFacade safesup = new FinanceDBFacade();
            DataSet doc;
            doc = safesup.GetDataSetBySql("select * from tz_Project where ProGuid='" + Request["Guid"] + "'");
            //>=DATEADD(MONTH,DATEDIFF(MONTH,0,GETDATE()),0) and Checktime<=DATEADD(SS,-1,DATEADD(MONTH,1+DATEDIFF(MONTH,0,GETDATE()),0))
            if (doc != null && doc.Tables[0].Rows.Count > 0)
                IsChecked = false;
            else
                IsChecked = true;
        }

        public string GetNodeJson(XmlNode node, string path)
        {
            string json = "";
            if (node.HasChildNodes)
            {
                XmlNodeList nodeList = node.ChildNodes;

                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{id:'" + nodeList[i].Attributes["Guid"].Value + "',name:'" + nodeList[i].Attributes["Name"].Value + "',href:'" + path + nodeList[i].Attributes["Href"].Value + "',target:'Content',imgurl:'" + nodeList[i].Attributes["ImgUrl"].Value + "'";
                    json += GetChildMenuJson(nodeList[i], path);

                }
            }
            return json;
        }
        string GetChildMenuJson(XmlNode node, string path)
        {
            string json = "";
            if (node.HasChildNodes)
            {
                json += ",children:[";
                XmlNodeList nodeList = node.ChildNodes;
                for (int i = 0; i < nodeList.Count; i++)
                {


                    //if (!json.EndsWith(",") && i > 0 && json.EndsWith("}"))
                    //    json += ",";

                    //else
                    //{
                    json += "{id:'" + nodeList[i].Attributes["Guid"].Value + "',name:'" + nodeList[i].Attributes["Name"].Value + "',href:'" + path + nodeList[i].Attributes["Href"].Value + "',target:'Content',icon:'" + path + "/images/note.png'},";
                    //json += GetChildMenuJson(nodeList[i], path);
                    //}
                }
                json = json.TrimEnd(',') + "]}";
            }
            else
                json += "}";
            return json;
        }
    }
}