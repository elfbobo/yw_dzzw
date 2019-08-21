using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yawei.SupportCore.SupportApi.Entity;


namespace Yawei.SupportCore.SupportApi.EntityHelper
{
    public static class Action
    {
        #region XML操作

        public static XmlDocument ForeachMenuList(List<Menu> data, string topGuid)
        {
            var menuList = data;
            var menuFrist = menuList.Where(ml => ml.TopGuid == topGuid).OrderBy(m => m.SortNum).ToList();

            XmlDocument xml = new XmlDocument();
            XmlElement firstNoe = xml.CreateElement("Menus");
            if (menuFrist.Count > 0)
            {
                for (int i = 0; i < menuFrist.Count; i++)
                {
                    XmlElement newEle = xml.CreateElement("Menu");
                    string[] arrStr = new string[] { "Guid", "Name", "IconCls", "ImgUrl", "Href", "Target", "JSEvent", "Sign", "ImgUrl" };
                    Action.SetXMLAttribute(newEle, menuFrist[i], arrStr);
                    Action.SetXMLChildNode(menuList, newEle, xml, menuFrist[i].Guid, arrStr, "Menu");
                    firstNoe.AppendChild(newEle);
                }
            }
            xml.AppendChild(firstNoe);
            return xml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element">节点</param>
        /// <param name="dr">节点属性数据</param>
        /// <param name="strArr">节点属性名称</param>
        public static void SetXMLAttribute(XmlElement element, object obj, params string[] strArr)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();
            for (int i = 0; i < strArr.Length; i++)
            {
                var menu = properties.Where(p => p.Name == strArr[i]);
                string value = "";
                if (menu.Count() > 0)
                {
                    value = menu.First().GetValue(obj) == null ? "" : menu.First().GetValue(obj).ToString();
                }
                menu = null;
                element.SetAttribute(strArr[i], value);
            }
        }

        /// <summary>
        /// 递归方法
        /// </summary>
        /// <param name="data">递归的数据</param>
        /// <param name="ele">父节点</param>
        /// <param name="xml">XMLDocument</param>
        /// <param name="topName">父节点列名</param>
        /// <param name="idName">节点列名</param>
        /// <param name="value">当前节点值</param>
        /// <param name="arrStr">节点的属性名称</param>
        public static void SetXMLChildNode(List<Menu> data, XmlElement ele, XmlDocument xml, string value, string[] arrStr, string nodeName)
        {
            var child = data.Where(m => m.TopGuid == value).OrderBy(m => m.SortNum).ToList();
            if (child.Count > 0)
            {
                for (int i = 0; i < child.Count; i++)
                {
                    XmlElement newEle = xml.CreateElement(nodeName);
                    SetXMLAttribute(newEle, child[i], arrStr);
                    ele.AppendChild(newEle);
                    SetXMLChildNode(data, newEle, xml, child[i].Guid, arrStr, nodeName);
                }
            }
        }



        #endregion

        #region Json 操作
        public static string GetNodeJson(XmlNode node, string path)
        {
            string json = "";
            if (node.HasChildNodes)
            {
                XmlNodeList nodeList = node.ChildNodes;

                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{id:'" + nodeList[i].Attributes["Guid"].Value + "',name:'" + nodeList[i].Attributes["Name"].Value + "',href:'" + path + nodeList[i].Attributes["Href"].Value + "',target:'Content',icon:'" + path + nodeList[i].Attributes["ImgUrl"].Value + "',open:true";
                    json += GetChildMenuJson(nodeList[i], path);

                }
            }
            return json;
        }


        public static string GetNodeJson(XmlNode node)
        {
            string json = "";
            if (node.HasChildNodes)
            {
                XmlNodeList nodeList = node.ChildNodes;

                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{id:'" + nodeList[i].Attributes["Guid"].Value + "',name:'" + nodeList[i].Attributes["Name"].Value + "',type:'group'";
                    json += GetChildJson(nodeList[i]);

                }
            }
            return json;
        }

        static string GetChildMenuJson(XmlNode node, string path)
        {
            string json = "";
            if (node.HasChildNodes)
            {
                json += ",children:[";
                XmlNodeList nodeList = node.ChildNodes;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (i > 0)
                        json += ",";
                    json += "{id:'" + nodeList[i].Attributes["Guid"].Value + "',name:'" + nodeList[i].Attributes["Name"].Value + "',href:'" + path + nodeList[i].Attributes["Href"].Value + "',target:'Content',icon:'" + path + nodeList[i].Attributes["ImgUrl"].Value + "'";
                    json += GetChildMenuJson(nodeList[i], path);
                }
                json += "]}";
            }
            else
                json += "}";
            return json;
        }

        static string GetChildJson(XmlNode node)
        {
            string json = "";
            if (node.HasChildNodes)
            {
                json += ",children:[";
                XmlNodeList nodeList = node.ChildNodes;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (i > 0)
                        json += ",";
                    if (nodeList[i].Name == "Group")
                    {
                        json += "{id:'" + nodeList[i].Attributes["Guid"].Value + "',name:'" + nodeList[i].Attributes["Name"].Value + "',type:'group'";
                        json += GetChildJson(nodeList[i]);
                    }
                    else
                    {
                        json += "{id:'" + nodeList[i].Attributes["Guid"].Value + "',name:'" + nodeList[i].Attributes["UserCN"].Value + "',type:'user'";
                        json += GetChildJson(nodeList[i]);
                    }
                }
                json += "]}";
            }
            else
                json += "}";
            return json;
        }

        #endregion

        public static int[] ConverToIntArr(string[] strArr)
        {
            var intArr = new int[strArr.Length];
            for (int i = 0; i < intArr.Length; i++)
            {
                intArr[i] = Convert.ToInt32(strArr[i]);
            }
            return intArr;
        }
    }
}
