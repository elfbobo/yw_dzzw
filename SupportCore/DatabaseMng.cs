using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yawei.DataAccess;

namespace Yawei.SupportCore
{
    public static class DatabaseMng
    {
        static string treeJson = string.Empty;
        static Database database = DatabaseFactory.CreateDatabase();
        static Database db = DatabaseFactory.CreateDatabase();

        public static string GetTreeJson()
        {
            treeJson = "[";
            DataSet DbData = database.ExecuteDataSet("select * from Sys_DataModel ORDER BY Tstamp asc");
            DataSet TableData = database.ExecuteDataSet("select * from Sys_DataTable ORDER BY Tstamp asc");
            DataSet ColumnData = database.ExecuteDataSet("select * from Sys_DataColumn ORDER BY Tstamp asc");
            if (DbData != null && DbData.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < DbData.Tables[0].Rows.Count; k++)
                {
                    string ModelGuid = DbData.Tables[0].Rows[k]["Guid"].ToString();
                    string DBName = DbData.Tables[0].Rows[k]["DBName"].ToString();
                    string DBConnection = DbData.Tables[0].Rows[k]["DBConnection"].ToString();
                    if (k > 0)
                        treeJson += ",";
                    treeJson += "{id:\"" + ModelGuid + "\",name:\"" + DBName + "\",dbConn:\"" + DBConnection + "\",icon:\"../Images/database.png\",opt:\"update\",open:\"true\",oldname:\"" + DBName + "\"";

                    if (TableData != null && TableData.Tables[0].Rows.Count > 0)
                    {
                        treeJson += ",children:[";
                        DataRow[] Rows = TableData.Tables[0].Select("ModelGuid='" + ModelGuid + "'");
                        if (Rows != null && Rows.Length > 0)
                        {
                            for (int i = 0; i < Rows.Length; i++)
                            {
                                if (i > 0)
                                    treeJson += ",";
                                string TableName = Rows[i]["TableName"].ToString();
                                string Attr = GetProperty(Rows[i]["ExtendProperty"].ToString());
                                treeJson += "{id:\"" + (ModelGuid + "^" + TableName) + "\"";
                                treeJson += ",ext:\"" + CheckTableBuilded(DBConnection, TableName) + "\"";
                                treeJson += ",extendproperty:\"" + Attr + "\"";
                                treeJson += ",display:\"" + Rows[i]["Display"].ToString() + "\"";
                                treeJson += ",icon:\"../Images/table.png\"";
                                treeJson += ",pId:\"" + ModelGuid + "\",oldname:\"" + TableName + "\",opt:\"updatetable\",name:\"" + TableName + "\"";
                                if (ColumnData != null && ColumnData.Tables[0].Rows.Count > 0)
                                {
                                    treeJson += ",children:[";
                                    DataRow[] rows = ColumnData.Tables[0].Select("TableName='" + TableName + "' and ModelGuid='" + ModelGuid + "'");
                                    if (rows != null && rows.Length > 0)
                                    {
                                        for (int j = 0; j < rows.Length; j++)
                                        {
                                            if (j > 0)
                                                treeJson += ",";
                                            string Icon = "../Images/column.png";
                                            string IsPrimaryKey = rows[j]["IsPrimaryKey"].ToString();
                                            if (IsPrimaryKey == "0")
                                                Icon = "../Images/key.png";
                                            string Property = GetProperty(rows[j]["ExtendProperty"].ToString());
                                            treeJson += "{id:\"" + (ModelGuid + "^" + TableName + "^" + rows[j]["Name"].ToString()) + "\"";
                                            treeJson += ",type:\"" + rows[j]["Type"].ToString() + "\"";
                                            treeJson += ",description:\"" + rows[j]["Description"].ToString() + "\"";
                                            treeJson += ",lengh:\"" + rows[j]["Lengh"].ToString() + "\"";
                                            treeJson += ",precision:\"" + rows[j]["Precision"].ToString() + "\"";
                                            treeJson += ",isprimarykey:\"" + rows[j]["IsPrimaryKey"].ToString() + "\"";
                                            treeJson += ",isnull:\"" + rows[j]["IsNull"].ToString() + "\"";
                                            treeJson += ",defaultvalue:\"" + rows[j]["DefaultValue"].ToString() + "\"";
                                            treeJson += ",extendproperty:\"" + Property + "\"";
                                            treeJson += ",icon:\"" + Icon + "\",oldname:\"" + rows[j]["Name"].ToString() + "\"";
                                            treeJson += ",opt:\"updatecolumn\""; treeJson += ",pId:\"" + (ModelGuid + "^" + TableName) + "\"";
                                            treeJson += ",name:\"" + rows[j]["Name"].ToString() + "\"}";
                                        }
                                    }
                                    treeJson += "]";

                                }
                                treeJson += "}";
                            }
                        }
                        treeJson += "]";
                    }
                    treeJson += "}";
                }
            }
            treeJson += "]";
            return treeJson;
        }

        //public static string GetTreeJson(string ProgramGuid)
        //{
        //    treeJson = "[";
        //    DataSet DbData = database.ExecuteDataSet("select * from Sys_DataModel where ProgramGuid='" + ProgramGuid + "' ORDER BY Tstamp asc");
        //    DataSet TableData = database.ExecuteDataSet("select * from Sys_DataTable ORDER BY Tstamp asc");
        //    DataSet ColumnData = database.ExecuteDataSet("select * from Sys_DataColumn ORDER BY Tstamp asc");
        //    if (DbData != null && DbData.Tables[0].Rows.Count > 0)
        //    {
        //        for (int k = 0; k < DbData.Tables[0].Rows.Count; k++)
        //        {
        //            string ModelGuid = DbData.Tables[0].Rows[k]["Guid"].ToString();
        //            string DBName = DbData.Tables[0].Rows[k]["DBName"].ToString();
        //            string DBConnection = DbData.Tables[0].Rows[k]["DBConnection"].ToString();
        //            if (k > 0)
        //                treeJson += ",";
        //            treeJson += "{id:\"" + ModelGuid + "\",name:\"" + DBName + "\",dbConn:\"" + DBConnection + "\",icon:\"../../../Images/database.png\",opt:\"update\",open:\"true\",oldname:\"" + DBName + "\"";

        //            if (TableData != null && TableData.Tables[0].Rows.Count > 0)
        //            {
        //                treeJson += ",children:[";
        //                DataRow[] Rows = TableData.Tables[0].Select("ModelGuid='" + ModelGuid + "'");
        //                if (Rows != null && Rows.Length > 0)
        //                {
        //                    for (int i = 0; i < Rows.Length; i++)
        //                    {
        //                        if (i > 0)
        //                            treeJson += ",";
        //                        string TableName = Rows[i]["TableName"].ToString();
        //                        string Attr = GetProperty(Rows[i]["ExtendProperty"].ToString());
        //                        treeJson += "{id:\"" + (ModelGuid + "^" + TableName) + "\"";
        //                        treeJson += ",ext:\"" + CheckTableBuilded(DBConnection, TableName) + "\"";
        //                        treeJson += ",extendproperty:\"" + Attr + "\"";
        //                        treeJson += ",display:\"" + Rows[i]["Display"].ToString() + "\"";
        //                        treeJson += ",icon:\"../../../Images/table.png\"";
        //                        treeJson += ",pId:\"" + ModelGuid + "\",oldname:\"" + TableName + "\",opt:\"updatetable\",name:\"" + TableName + "\"";
        //                        if (ColumnData != null && ColumnData.Tables[0].Rows.Count > 0)
        //                        {
        //                            treeJson += ",children:[";
        //                            DataRow[] rows = ColumnData.Tables[0].Select("TableName='" + TableName + "' and ModelGuid='" + ModelGuid + "'");
        //                            if (rows != null && rows.Length > 0)
        //                            {
        //                                for (int j = 0; j < rows.Length; j++)
        //                                {
        //                                    if (j > 0)
        //                                        treeJson += ",";
        //                                    string Icon = "../../../Images/column.png";
        //                                    string IsPrimaryKey = rows[j]["IsPrimaryKey"].ToString();
        //                                    if (IsPrimaryKey == "0")
        //                                        Icon = "../../../Images/key.png";
        //                                    string Property = GetProperty(rows[j]["ExtendProperty"].ToString());
        //                                    treeJson += "{id:\"" + (ModelGuid + "^" + TableName + "^" + rows[j]["Name"].ToString()) + "\"";
        //                                    treeJson += ",type:\"" + rows[j]["Type"].ToString() + "\"";
        //                                    treeJson += ",description:\"" + rows[j]["Description"].ToString() + "\"";
        //                                    treeJson += ",lengh:\"" + rows[j]["Lengh"].ToString() + "\"";
        //                                    treeJson += ",precision:\"" + rows[j]["Precision"].ToString() + "\"";
        //                                    treeJson += ",isprimarykey:\"" + rows[j]["IsPrimaryKey"].ToString() + "\"";
        //                                    treeJson += ",isnull:\"" + rows[j]["IsNull"].ToString() + "\"";
        //                                    treeJson += ",defaultvalue:\"" + rows[j]["DefaultValue"].ToString() + "\"";
        //                                    treeJson += ",extendproperty:\"" + Property + "\"";
        //                                    treeJson += ",icon:\"" + Icon + "\",oldname:\"" + rows[j]["Name"].ToString() + "\"";
        //                                    treeJson += ",opt:\"updatecolumn\""; treeJson += ",pId:\"" + (ModelGuid + "^" + TableName) + "\"";
        //                                    treeJson += ",name:\"" + rows[j]["Name"].ToString() + "\"}";
        //                                }
        //                            }
        //                            treeJson += "]";

        //                        }
        //                        treeJson += "}";
        //                    }
        //                }
        //                treeJson += "]";
        //            }
        //            treeJson += "}";
        //        }
        //    }
        //    treeJson += "]";
        //    return treeJson;
        //}

        /// <summary>
        /// 检查该表是否已经生成，t代表已生成，f代表未生成
        /// </summary>
        /// <param name="ModelGuid"></param>
        /// <param name="Table"></param>
        /// <returns></returns>
        public static string CheckTableBuilded(string DBConnection, string Table)
        {
            db = DatabaseFactory.CreateDatabase(DBConnection, "Yawei.DataAccess.SqlClient.SqlDatabase");
            object obj = db.ExecuteScalar("select COUNT(*) from sys.tables WHERE name='" + Table + "'");
            if (int.Parse(obj.ToString()) > 0)
                return "t";
            else
                return "f";
        }

        static string GetProperty(string ExtendProperty)
        {
            string Attr = "";
            if (ExtendProperty != "<xml />" && ExtendProperty != "")
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ExtendProperty);
                XmlNode docNode = doc.DocumentElement;

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
            return Attr;
        }
    }
}
