using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using Yawei.DataAccess;

namespace Yawei.App.Support.Handlers
{
    /// <summary>
    /// DbGetProperty 的摘要说明
    /// </summary>
    public class DbGetProperty : IHttpHandler
    {
        Database database = DatabaseFactory.CreateDatabase();
        string ModelGuid = string.Empty;
        string TableName = string.Empty;
        string Type = string.Empty;
        string Name = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            Type = context.Request["Type"] == null ? "" : context.Request["Type"];
            ModelGuid = context.Request["ModelGuid"] == null ? "" : context.Request["ModelGuid"];
            TableName = context.Request["TableName"] == null ? "" : context.Request["TableName"];
            Name = context.Request["Name"] == null ? "" : context.Request["Name"];
            string json = string.Empty;
            switch (Type)
            {
                case "table":
                    json = GetTableProperty();
                    break;
                case "column":
                    json = GetColumnProperty();
                    break;
            }
            context.Response.Clear();
            context.Response.Write(json);
            context.Response.End();
        }

        string GetColumnProperty()
        {
            string json = "";
            DataSet doc = database.ExecuteDataSet("select * from Sys_DataColumn where ModelGuid='" + ModelGuid + "' and TableName='" + TableName + "' and Name='" + Name + "'");
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                string ExtendProperty = doc.Tables[0].Rows[0]["ExtendProperty"].ToString();
                string Attr = "";
                if (ExtendProperty != "<xml />" && !string.IsNullOrWhiteSpace(ExtendProperty))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(ExtendProperty);
                    XmlNode docNode = xmlDoc.DocumentElement;
                    XmlNodeList nodes = docNode.SelectNodes("Property");
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            XmlAttributeCollection attrCol = node.Attributes;
                            foreach (XmlAttribute xa in attrCol)
                                Attr += xa.Name + "^" + xa.Value + "Ω";
                        }
                        Attr = Attr.TrimEnd('Ω');
                    }
                }
                json = "[";
                json += "{Type:\"" + doc.Tables[0].Rows[0]["Type"].ToString() + "\",";
                json += "Description:\"" + doc.Tables[0].Rows[0]["Description"].ToString() + "\",";
                json += "Lengh:\"" + doc.Tables[0].Rows[0]["Lengh"].ToString() + "\",";
                json += "Precision:\"" + doc.Tables[0].Rows[0]["Precision"].ToString() + "\",";
                json += "IsPrimaryKey:\"" + doc.Tables[0].Rows[0]["IsPrimaryKey"].ToString() + "\",";
                json += "IsNull:\"" + doc.Tables[0].Rows[0]["IsNull"].ToString() + "\",";
                json += "DefaultValue:\"" + doc.Tables[0].Rows[0]["DefaultValue"].ToString() + "\",";
                json += "ExtendProperty:\"" + Attr + "\"}";
                json += "]";
            }

            return json;
        }

        string GetTableProperty()
        {
            string json = "";
            DataSet doc = database.ExecuteDataSet("select * from Sys_DataTable where ModelGuid='" + ModelGuid + "' and TableName='" + TableName + "'");
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                string ExtendProperty = doc.Tables[0].Rows[0]["ExtendProperty"].ToString();
                string Attr = "";
                if (ExtendProperty != "<xml />" && !string.IsNullOrWhiteSpace(ExtendProperty))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(ExtendProperty);
                    XmlNode docNode = xmlDoc.DocumentElement;
                    XmlNodeList nodes = docNode.SelectNodes("Property");
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            XmlAttributeCollection attrCol = node.Attributes;
                            foreach (XmlAttribute xa in attrCol)
                                Attr += xa.Name + "^" + xa.Value + "Ω";
                        }
                        Attr = Attr.TrimEnd('Ω');
                    }
                }
                json = "[";
                json += "{Display:\"" + doc.Tables[0].Rows[0]["Display"].ToString() + "\",ExtendProperty:\"" + Attr + "\"}";
                json += "]";
            }
            return json;
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