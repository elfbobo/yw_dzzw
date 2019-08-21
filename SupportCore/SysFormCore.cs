//2013-10
//田飞飞

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Yawei.DataAccess;
using System.Web.UI.WebControls;

using System.Xml;
using System.Data.SqlClient;
using Yawei.SupportCore.SupportApi.Entity;
using Yawei.Common;


namespace Yawei.SupportCore
{
    public class SysFormCore
    {
        public string TableName { set; get; }
        public string Key { set; get; }
        public string KeyValue { set; get; }
        public string ConName { set; get; }
        public CurrentUser CurrentUser { set; get; }

        /// <summary>
        /// RefTable={{'table','主键'}}
        /// </summary>
        public string[,] RefTable;
        public bool MasterPage { set; get; }
        public string StateGuid { set; get; }

        private List<DataSet> childTable = null;

        private string sql = string.Empty;

        private static Page cPage = null;

        public bool ConvertHtml { set; get; }

        public SysFormCore()
        {
            MasterPage = false;
            ConvertHtml = true;
        }

        #region 页面数据保存 初始化 删除
        /// <summary>
        /// 初始化页面控件数据
        /// </summary>
        /// <param name="page">当前页面对象</param>
        public void SetControlValue(Page page)
        {
            DataSet doc = GetData();
            SetControlValue(page, doc);
            if (RefTable != null && RefTable.GetLength(0) > 0)
            {
                for (int i = 0; i < RefTable.GetLength(0); i++)
                {
                    doc = GetData(RefTable[i, 0], RefTable[i, 1], KeyValue);
                    SetControlValue(page, doc);
                }
            }
        }

        /// <summary>
        /// 初始化页面控件数据
        /// </summary>
        /// <param name="page">当前页面对象</param>
        public void SetControlValue(Page page,out DataSet datas)
        {
            DataSet doc = GetData();
            datas = doc;
            SetControlValue(page, doc);
            if (RefTable != null && RefTable.GetLength(0) > 0)
            {
                for (int i = 0; i < RefTable.GetLength(0); i++)
                {
                    doc = GetData(RefTable[i, 0], RefTable[i, 1], KeyValue);
                    SetControlValue(page, doc);
                }
            }
        }

        public void SetControlValue(Page page, DataSet doc)
        {
            cPage = page;
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Columns.Count; i++)
                {
                    string colName = doc.Tables[0].Columns[i].ColumnName;

                    Control control = null;
                    if (MasterPage)
                    {
                        control = page.Master.Master.FindControl("body").FindControl("rightbody").FindControl(colName);
                    }
                    else
                        control = page.FindControl(colName);

                    if (control != null)
                    {
                        Type type = control.GetType();
                        switch (type.ToString())
                        {
                            case "System.Web.UI.WebControls.TextBox":
                                TextBox txt = (TextBox)control;
                                if (doc.Tables[0].Columns[i].DataType.Name == "DateTime")
                                {
                                    txt.Text = doc.Tables[0].Rows[0][colName].ToString().Replace("/", "-").Replace("0:00:00", "");
                                }
                                else
                                    txt.Text = doc.Tables[0].Rows[0][colName].ToString();
                                break;
                            case "System.Web.UI.WebControls.DropDownList":
                                DropDownList drp = (DropDownList)control;
                                drp.SelectedValue = doc.Tables[0].Rows[0][colName].ToString();
                                break;
                            case "System.Web.UI.WebControls.HiddenField":
                                HiddenField hid = (HiddenField)control;
                                hid.Value = doc.Tables[0].Rows[0][colName].ToString();
                                break;
                            case "System.Web.UI.WebControls.CheckBoxList":
                                CheckBoxList checkbox = (CheckBoxList)control;
                                var checkItem = checkbox.Items.Cast<ListItem>().Where(it => doc.Tables[0].Rows[0][colName].ToString().Contains(it.Value));
                                foreach (var chk in checkItem)
                                    chk.Selected = true;
                                break;
                            case "System.Web.UI.WebControls.RadioButtonList":
                                RadioButtonList radio = (RadioButtonList)control;
                                var radioCheckItem = radio.Items.Cast<ListItem>().Where(it => doc.Tables[0].Rows[0][colName].ToString().Contains(it.Value));
                                foreach (var chk in radioCheckItem)
                                    chk.Selected = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 页面数据保存 Insert  Update
        /// </summary>
        /// <param name="Request">当前Requst</param>
        public void SaveData(System.Web.HttpRequest Request, string[,] filedValue)
        {
            Database database = null;
            if (!string.IsNullOrEmpty(ConName))
                database = DatabaseFactory.CreateDatabase(ConName);
            else
                database = DatabaseFactory.CreateDatabase();
            if (KeyValue == "")
            {
                if (string.IsNullOrWhiteSpace(StateGuid))
                    KeyValue = Guid.NewGuid().ToString();
                else
                    KeyValue = StateGuid;
                DataSet doc = AddData(Request, filedValue, database, TableName, Key);
                if (RefTable != null && RefTable.GetLength(0) > 0)
                {
                    for (int i = 0; i < RefTable.GetLength(0); i++)
                    {
                        doc.Merge(AddData(Request, filedValue, database, RefTable[i, 0], RefTable[i, 1]));
                    }
                }
                SysLogCore log = new SysLogCore();
                string pg = "";
                if (!string.IsNullOrEmpty(Request["projguid"]))
                    pg = Request["Projguid"];
                doc.Merge(log.OperatorLog(TableName, KeyValue, KeyValue, "添加", CurrentUser.UserGuid, CurrentUser.UserDN, CurrentUser.UserCN, pg));
                if (childTable != null)
                {
                    foreach (DataSet ds in childTable)
                        doc.Merge(ds);
                }
                //*********************添加主管部门审核信息**************************
                if (string.IsNullOrWhiteSpace(Request["projGuid"]) && (TableName.ToLower().StartsWith("busi") || TableName.ToLower().StartsWith("idty")))
                {
                    DataSet confirmDoc = GetData("Busi_AdminConfirm", "Guid", KeyValue);
                    DataRow d = confirmDoc.Tables[0].NewRow();
                    d["Guid"] = Guid.NewGuid().ToString();
                    d["RefGuid"] = KeyValue;
                    d["projGuid"] = Request["projGuid"];
                    d["_table"] = TableName;
                    string currentUrl = System.Web.HttpContext.Current.Request.Url.ToString();
                    if (currentUrl.ToLower().Contains("/project/"))
                    {
                        currentUrl = "/project/" + currentUrl.ToLower().Replace("/project/", "@").Split('@')[1];
                    }
                    if (currentUrl.ToLower().Contains("/industrymanager/"))
                    {
                        currentUrl = "/industrymanager/" + currentUrl.ToLower().Replace("/industrymanager/", "@").Split('@')[1];
                    }
                    d["url"] = currentUrl;
                    confirmDoc.Tables[0].Rows.Add(d);
                    doc.Merge(confirmDoc);
                }
                //***********************************************
                database.UpdateDataSet(doc);
            }
            else
            {
                DataSet doc = GetData();
                doc.Tables[0].TableName = TableName;

                UpdateData(Request, doc);

                if (RefTable != null && RefTable.GetLength(0) > 0)
                {
                    for (int i = 0; i < RefTable.GetLength(0); i++)
                    {
                        DataSet ds = GetData(RefTable[i, 0], RefTable[i, 1], KeyValue);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            UpdateData(Request, ds);
                        }
                        else
                        {
                            doc.Merge(AddData(Request, filedValue, database, RefTable[i, 0], RefTable[i, 1]));
                        }
                        doc.Merge(ds);
                    }
                }

                SysLogCore log = new SysLogCore();
                string pg = "";
                if (!string.IsNullOrEmpty(Request["projguid"]))
                    pg = Request["Projguid"];
                doc.Merge(log.OperatorLog(TableName, KeyValue, KeyValue, "修改", CurrentUser.UserGuid, CurrentUser.UserDN, CurrentUser.UserCN,pg));

                if (childTable != null)
                {
                    foreach (DataSet ds in childTable)
                        doc.Merge(ds);
                }
                //****************删除项目退回记录*****************
                if (TableName.ToLower().StartsWith("busi") || TableName.ToLower().StartsWith("idty"))
                {
                    DataSet reBackdoc = GetData("Busi_RebackInfo", "refGuid", KeyValue);
                    if (reBackdoc != null && reBackdoc.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow d in reBackdoc.Tables[0].Rows)
                        {
                            d.Delete();
                        }
                        doc.Merge(reBackdoc);
                    }
                    //***********************添加主管部门审核信息*******************
                    if (string.IsNullOrWhiteSpace(Request["projGuid"]))
                    {
                        DataSet confirmDoc = GetData("Busi_AdminConfirm", "refGuid", KeyValue);
                        if (confirmDoc.Tables[0].Rows.Count <= 0)
                        {
                            DataRow d = confirmDoc.Tables[0].NewRow();
                            d["Guid"] = Guid.NewGuid().ToString();
                            d["RefGuid"] = KeyValue;
                            d["projGuid"] = Request["projGuid"];
                            d["_table"] = TableName;
                            string currentUrl = System.Web.HttpContext.Current.Request.Url.ToString();
                            if (currentUrl.ToLower().Contains("/project/"))
                            {
                                currentUrl = "/project/" + currentUrl.ToLower().Replace("/project/", "@").Split('@')[1];
                            }
                            if (currentUrl.ToLower().Contains("/industrymanager/"))
                            {
                                currentUrl = "/industrymanager/" + currentUrl.ToLower().Replace("/industrymanager/", "@").Split('@')[1];
                            }
                            d["url"] = currentUrl;
                            confirmDoc.Tables[0].Rows.Add(d);
                            doc.Merge(confirmDoc);
                        }
                    }
                }
                //*********************************
                database.UpdateDataSet(doc);
                cPage = null;
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="filedValue"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public DataSet AddData(System.Web.HttpRequest Request, string[,] filedValue, Database database, string tableName, string tableKey) 
        {
            DataSet doc = database.ExecuteDataSet("select * from " + tableName + " where 1=2");
            try
            {
                doc.Tables[0].TableName = tableName;
                DataRow dr = doc.Tables[0].NewRow();

                dr[tableKey] = KeyValue;
                for (int i = 0; i < doc.Tables[0].Columns.Count; i++)
                {
                    string colName = doc.Tables[0].Columns[i].ColumnName;

                    if (colName.ToLower() == "status" || colName.ToLower() == "sysstatus")
                    {
                        dr[colName] = "0";
                        continue;
                    }

                    if (Request[colName] != null && Request[colName] != "")
                    {
                        dr[colName] = FormatData(Request[colName]);
                    }
                    else
                    {
                        var keys = Request.Params.AllKeys.Where(k => k.Contains(colName) && k.Contains("$")).ToArray();
                        if (keys.Length > 0)
                        {
                            string tm = "";
                            for (int j = 0; j < keys.Length; j++)
                            {
                                if (j > 0)
                                    tm += ",";
                                tm += Request[keys[j]];
                            }
                            dr[colName] = tm;
                        }

                    }
                }

                if (filedValue != null && filedValue.GetLength(0) > 0)
                {
                    for (int i = 0; i < filedValue.GetLength(0); i++)
                    {
                        dr[filedValue[i, 0]] = filedValue[i, 1];
                    }
                }

                doc.Tables[0].Rows.Add(dr);
                doc.Tables[0].TableName = tableName;
                return doc;
            }
            catch (Exception e)
            {
                return doc;
            }
            
        }


        public void UpdateData(System.Web.HttpRequest Request, DataSet doc)
        {
            if (doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Columns.Count; i++)
                {
                    string colName = doc.Tables[0].Columns[i].ColumnName;
                    if (colName.ToLower() == "status" || colName.ToLower() == "sysstatus")
                    {
                        doc.Tables[0].Rows[0][colName] = "0";
                        continue;
                    }
                    if (Request[colName] != null)
                    {
                        doc.Tables[0].Rows[0][colName] = FormatData(Request[colName]);
                    }
                    else
                    {
                        var keys = Request.Params.AllKeys.Where(k => k.Contains(colName) && k.Contains("$")).ToArray();

                        if (keys.Length > 0)
                        {
                            string tm = "";
                            for (int j = 0; j < keys.Length; j++)
                            {
                                if (j > 0)
                                    tm += ",";
                                tm += Request[keys[j]];
                            }
                            doc.Tables[0].Rows[0][colName] = tm;
                        }
                        else
                        {
                            if (cPage != null)
                            {
                                Control control = cPage.FindControl(colName);
                                if (control != null && control.GetType().ToString() == "System.Web.UI.WebControls.CheckBoxList")
                                {
                                    doc.Tables[0].Rows[0][colName] = DBNull.Value;
                                }
                            }
                        }
                    }
                }
            }



        }

        /// <summary>
        /// 删除表
        /// </summary>
        public void DeleteData()
        {
            Database database = null;
            if (!string.IsNullOrEmpty(ConName))
                database = DatabaseFactory.CreateDatabase(ConName);
            else
                database = DatabaseFactory.CreateDatabase();
            database.ExecuteDataSet("update " + TableName + " set sysstatus=-1 where " + Key + " ='" + KeyValue + "' " + sql);
            SysLogCore log = new SysLogCore();
            DataSet ds = log.OperatorLog(TableName, KeyValue, KeyValue, "删除", CurrentUser.UserGuid, CurrentUser.UserDN, CurrentUser.UserCN);
            if (sql != "")
                ds.Merge(log.OperatorLog(TableName, KeyValue, sql, "删除子表", CurrentUser.UserGuid, CurrentUser.UserDN, CurrentUser.UserCN));
            database.UpdateDataSet(ds);
        }

        /// <summary>
        /// 删除子表
        /// </summary>
        /// <param name="sql"></param>
        public void DeleteChild(string sql)
        {
            sql += " " + sql + " ";
        }

        DataSet GetData()
        {
            if (!string.IsNullOrWhiteSpace(KeyValue))
            {
                Database database = null;
                if (!string.IsNullOrEmpty(ConName))
                    database = DatabaseFactory.CreateDatabase(ConName);
                else
                    database = DatabaseFactory.CreateDatabase();
                System.Data.Common.DbCommand cmd = database.CreateCommand(CommandType.Text, "select * from " + TableName + " where " + Key + "=@KeyValue");
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@KeyValue", KeyValue));
                DataSet doc = database.ExecuteDataSet(cmd);
                return doc;
            }
            else
                return null;
        }


        DataSet GetData(string tableName, string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(KeyValue))
            {
                Database database = null;
                if (!string.IsNullOrEmpty(ConName))
                    database = DatabaseFactory.CreateDatabase(ConName);
                else
                    database = DatabaseFactory.CreateDatabase();
                System.Data.Common.DbCommand cmd = database.CreateCommand(CommandType.Text, "select * from " + tableName + " where " + key + "=@KeyValue");
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@KeyValue", value));
                DataSet doc = database.ExecuteDataSet(cmd);
                doc.Tables[0].TableName = tableName;
                return doc;
            }
            else
                return null;
        }

        private object FormatData(string value)
        {
            if (value == "")
                return DBNull.Value;
            else
                return value;
        }


        /// <summary>
        /// View页面初始化数据
        /// </summary>
        /// <param name="convert"></param>
        /// <returns></returns>
        public Dictionary<string, string> SetViewData(string[,] convert)
        {
            DataSet doc = GetData();
            doc.Tables[0].TableName = TableName;
            Dictionary<string, string> document = new Dictionary<string, string>();
            SetViewData(convert, doc, document);
            if (RefTable != null && RefTable.GetLength(0) > 0)
            {
                for (int i = 0; i < RefTable.GetLength(0); i++)
                {
                    doc = GetData(RefTable[i, 0], RefTable[i, 1], KeyValue);
                    doc.Tables[0].TableName = RefTable[i, 0];
                    SetViewData(convert, doc, document);
                }
            }
            return document;
        }

        public void SetViewData(string[,] convert, DataSet doc, Dictionary<string, string> document)
        {
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Columns.Count; i++)
                {
                    string colName = doc.Tables[0].Columns[i].ColumnName;
                    if (document.Keys.Where(k => k == colName).Count() > 0)
                    {
                        if (doc.Tables[0].Columns[i].DataType.Name == "DateTime")
                        {
                            document[doc.Tables[0].TableName + "_" + colName] = FormatDate(doc.Tables[0].Rows[0][colName].ToString());
                        }
                        else
                        {
                            if (ConvertHtml)
                                document[doc.Tables[0].TableName + "_" + colName] = doc.Tables[0].Rows[0][colName].ToString();//.Replace("\r\n", "<br>").Replace(" ", "&nbsp;");
                            else
                                document[doc.Tables[0].TableName + "_" + colName] = doc.Tables[0].Rows[0][colName].ToString();
                        }
                    }
                    else
                    {
                        if (doc.Tables[0].Columns[i].DataType.Name == "DateTime")
                        {
                            document[colName] = FormatDate(doc.Tables[0].Rows[0][colName].ToString());
                        }
                        else
                        {
                            if (ConvertHtml)
                                document[colName] = doc.Tables[0].Rows[0][colName].ToString();//.Replace("\r\n", "<br>").Replace(" ", "&nbsp;");
                            else
                                document[colName] = doc.Tables[0].Rows[0][colName].ToString();
                        }
                    }
                }

                if (convert != null && convert.GetLength(0) > 0)
                {
                    for (int i = 0; i < convert.GetLength(0); i++)
                    {
                        //if (convert[i, 2].ToLower() == "code" && document[convert[i, 0]] != "")
                        //    if (document[convert[i, 0]].Contains(","))
                        //        document[convert[i, 0]] = GetDicNameByGroupCode(document[convert[i, 0]], convert[i, 1]);
                        //    else
                        //        document[convert[i, 0]] = GetDicNameByGroupCode(Convert.ToInt32(document[convert[i, 0]]), convert[i, 1]);
                        //else
                        //    document[convert[i, 0]] = GetDicNameByGroupValue(document[convert[i, 0]], convert[i, 1]);
                        document[convert[i, 0]] = GetMappingNameByGuid(convert[i, 1]);

                    }
                }


            }
            else
            {
                for (int i = 0; i < doc.Tables[0].Columns.Count; i++)
                {
                    string colName = doc.Tables[0].Columns[i].ColumnName;
                    if (document.Keys.Where(k => k == colName).Count() > 0)
                    {
                        document[doc.Tables[0].TableName + "_" + colName] = "暂无";
                    }
                    else
                        document[colName] = "暂无";
                }
            }

        }

        private string FormatDate(string val)
        {
            if (val == "" || val == "1900-1-1 0:00:00")
                return "";
            else
                return val.Split(' ')[0];
        }

        #endregion

        #region  CheckboxList
        /// <summary>
        /// 绑定CheckBoxList列表 Option Value 是 字典表 Value字段
        /// </summary>
        /// <param name="list">下拉控件</param>
        /// <param name="group">字典组名</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetCheckBoxList(CheckBoxList list, string guid)
        {
            var dic = Mapping.GetMappingByGuid(guid);

            var st = (from litem in dic select new ListItem() { Text = litem.Name, Value = litem.Guid }).ToArray();

            list.Items.AddRange(st);
        }

        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Code字段
        /// </summary>
        /// <param name="list">下拉控件</param>
        /// <param name="group">字典组名</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        //public void SetCheckBoxListCode(CheckBoxList list, string guid)
        //{
        //    var dic = Mapping.GetMappingByGuid(guid);

        //    var st = (from litem in dic select new ListItem() { Text = litem.Name, Value = litem.Guid }).ToArray();

        //    list.Items.AddRange(st);
        //}

        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Name字段
        /// </summary>
        /// <param name="list">下拉控件</param>
        /// <param name="group">字典组名</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetCheckBoxListName(CheckBoxList list, string guid)
        {
            var dic = Mapping.GetMappingByGuid(guid);

            var st = (from litem in dic select new ListItem() { Text = litem.Name, Value = litem.Name }).ToArray();

            list.Items.AddRange(st);
        }


        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Name字段
        /// </summary>
        /// <param name="list">控件</param>
        /// <param name="tableName">表明</param>
        /// <param name="filedName">绑定Name的字段</param>
        /// <param name="filedValue">绑定Value的字段</param>
        /// <param name="where">sqlwhere</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetCheckBoxListTable(CheckBoxList list, string tableName, string filedName, string filedValue, string where, bool firstEmpty)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet ds = database.ExecuteDataSet("select " + filedName + "," + filedValue + " from " + tableName + " where  1=1 " + where);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (firstEmpty)
                {
                    ListItem item = new ListItem("", "");
                    list.Items.Add(item);
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListItem item = new ListItem(ds.Tables[0].Rows[i][filedName].ToString(), ds.Tables[0].Rows[i][filedValue].ToString());
                    list.Items.Add(item);
                }
            }
        }


        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Name字段
        /// </summary>
        /// <param name="list">控件</param>
        /// <param name="tableName">表明</param>

        /// <param name="filedValue">绑定Value的字段</param>
        /// <param name="where">sqlwhere</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetCheckBoxListTable(CheckBoxList list, string tableName, string filedValue, string where, bool firstEmpty)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet ds = database.ExecuteDataSet("select " + filedValue + "," + filedValue + " from " + tableName + " where  1=1 " + where);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (firstEmpty)
                {
                    ListItem item = new ListItem("", "");
                    list.Items.Add(item);
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListItem item = new ListItem(ds.Tables[0].Rows[i][filedValue].ToString(), ds.Tables[0].Rows[i][filedValue].ToString());
                    list.Items.Add(item);
                }
            }
        }
        #endregion


        #region DropDownList

        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Value字段
        /// </summary>
        /// <param name="list">下拉控件</param>
        /// <param name="group">字典组名</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetDropDownListValue(DropDownList list, string guid, bool firstEmpty)
        {
            var dic = Mapping.GetMappingByGuid(guid);

            if (firstEmpty)
            {
                ListItem item = new ListItem("", "");
                list.Items.Add(item);
            }
            var st = (from litem in dic select new ListItem() { Text = litem.Name, Value = litem.Guid }).ToArray();

            list.Items.AddRange(st);
        }

        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Code字段
        /// </summary>
        /// <param name="list">下拉控件</param>
        /// <param name="group">字典组名</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetDropDownListCode(DropDownList list, string guid, bool firstEmpty)
        {
            var dic = Mapping.GetMappingByGuid(guid);

            if (firstEmpty)
            {
                ListItem item = new ListItem("", "");
                list.Items.Add(item);
            }
            var st = (from litem in dic select new ListItem() { Text = litem.Name, Value = litem.Guid }).ToArray();

            list.Items.AddRange(st);
        }

        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Name字段
        /// </summary>
        /// <param name="list">下拉控件</param>
        /// <param name="group">字典组名</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetDropDownListName(DropDownList list, string guid, bool firstEmpty)
        {
            var dic = Mapping.GetMappingByGuid(guid);

            if (firstEmpty)
            {
                ListItem item = new ListItem("", "");
                list.Items.Add(item);
            }
            var st = (from litem in dic select new ListItem() { Text = litem.Name, Value = litem.Name }).ToArray();

            list.Items.AddRange(st);
        }


        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Name字段
        /// </summary>
        /// <param name="list">控件</param>
        /// <param name="tableName">表明</param>
        /// <param name="filedName">绑定Name的字段</param>
        /// <param name="filedValue">绑定Value的字段</param>
        /// <param name="where">sqlwhere</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetDropdownListTable(DropDownList list, string tableName, string filedName, string filedValue, string where, bool firstEmpty)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet ds = database.ExecuteDataSet("select " + filedName + "," + filedValue + " from " + tableName + " where  1=1 " + where);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (firstEmpty)
                {
                    ListItem item = new ListItem("", "");
                    list.Items.Add(item);
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListItem item = new ListItem(ds.Tables[0].Rows[i][filedName].ToString(), ds.Tables[0].Rows[i][filedValue].ToString());
                    list.Items.Add(item);
                }
            }
        }


        /// <summary>
        /// 绑定下拉列表 Option Value 是 字典表 Name字段
        /// </summary>
        /// <param name="list">控件</param>
        /// <param name="tableName">表明</param>
        /// <param name="filedName">绑定Name的字段</param>
        /// <param name="filedValue">绑定Value的字段</param>
        /// <param name="where">sqlwhere</param>
        /// <param name="firstEmpty">是否第一个为空 true 为空</param>
        public void SetDropdownListTable(DropDownList list, string tableName, string filedValue, string where, bool firstEmpty)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DataSet ds = database.ExecuteDataSet("select " + filedValue + "," + filedValue + " from " + tableName + " where  1=1 " + where);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (firstEmpty)
                {
                    ListItem item = new ListItem("", "");
                    list.Items.Add(item);
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListItem item = new ListItem(ds.Tables[0].Rows[i][filedValue].ToString(), ds.Tables[0].Rows[i][filedValue].ToString());
                    list.Items.Add(item);
                }
            }
        }

        public string GetDropdownCheckBoxListTable(string tableName, string filedName, string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            object obj = database.ExecuteScalar("select " + filedName + " from " + tableName + " where 1=1 " + where);
            if (obj != null)
                return obj.ToString();
            return "";
        }

        public void SetDropdownListYear(DropDownList list, bool firstEmpty, int length)
        {
            if (firstEmpty)
            {
                ListItem item = new ListItem("", "");
                list.Items.Add(item);
            }
            for (int year = DateTime.Now.Year; year > DateTime.Now.Year - length; year--)
            {
                ListItem item = new ListItem(year.ToString(), year.ToString());
                list.Items.Add(item);
            }
        }

        public void SetDropdownListMonth(DropDownList list, bool firstEmpty)
        {
            if (firstEmpty)
            {
                ListItem item = new ListItem("", "");
                list.Items.Add(item);
            }
            for (int month = 12; month > 0; month--)
            {
                ListItem item = new ListItem(month.ToString(), month.ToString());
                list.Items.Add(item);
            }
        }

        #endregion

        #region 字典表
        /// <summary>
        /// 根据Code值获取字典表Name
        /// </summary>
        /// <param name="code"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        //public string GetDicNameByGroupCode(int code, string group)
        //{
        //    return Mapping.GetDicNameByCode(code, group);
        //}

        /// <summary>
        /// 根据Code值获取字典表Name
        /// </summary>
        /// <param name="code"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        //public string GetDicNameByGroupCode(string code, string group)
        //{
        //    return Mapping.GetDicNameByCode(code, group);
        //}

        /// <summary>
        /// 根据Value值获取字典表Name
        /// </summary>
        /// <param name="code"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public string GetMappingNameByGuid(string guid)
        {
            return Mapping.GetNameByGuid(guid);
        }

        /// <summary>
        /// 根据group获取字典
        /// </summary>
        /// <param name="code"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<Mapping> GetMappingByGuid(string guid)
        {
            return Mapping.GetMappingByGuid(guid);
        }


        #endregion


        #region  页面动态行

        /// <summary>
        /// 保存动态行取数据
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="tableName"></param>
        /// <param name="refName"></param>
        /// <param name="refValue"></param>
        /// <returns></returns>
        public DataSet RowData(string xml, string tableName, string refName, string refValue)
        {
            if (string.IsNullOrWhiteSpace(refValue))
            {
                if (string.IsNullOrWhiteSpace(StateGuid))
                    StateGuid = Guid.NewGuid().ToString();
                refValue = StateGuid;

            }
            if (!string.IsNullOrWhiteSpace(xml))
            {
                xml = System.Web.HttpContext.Current.Server.UrlDecode(xml);
                Database database = null;
                if (!string.IsNullOrEmpty(ConName))
                    database = DatabaseFactory.CreateDatabase(ConName);
                else
                    database = DatabaseFactory.CreateDatabase();
                DataSet ds = database.ExecuteDataSet("select * from " + tableName + " where 1=2");
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);

                if (document.FirstChild.HasChildNodes)
                {

                    XmlNodeList nodeList = document.FirstChild.ChildNodes;
                    for (int i = 0; i < nodeList.Count; i++)
                    {

                        if (nodeList[i].HasChildNodes)
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            XmlNodeList list = nodeList[i].ChildNodes;
                            dr["guid"] = Guid.NewGuid().ToString();
                            dr["SysStatus"] = 0;
                            dr["Status"] = 0;
                            if (refName != "")
                                dr[refName] = refValue;
                            for (int x = 0; x < list.Count; x++)
                            {

                                if (ds.Tables[0].Columns.Contains(list[x].Name))
                                {
                                    if (list[x].HasChildNodes)
                                    {
                                        int j = 0; string temp = "";
                                        foreach (XmlNode node in list[x].ChildNodes)
                                        {

                                            if (node.HasChildNodes)
                                            {
                                                temp += node.FirstChild.Value; temp += "^";
                                            }
                                            else
                                                temp += node.Value;
                                            j++;
                                        }
                                        dr[list[x].Name] = temp;
                                    }

                                }
                            }
                            ds.Tables[0].Rows.Add(dr);
                        }

                    }
                }
                ds.Tables[0].TableName = tableName;

                database.ExecuteNonQuery("delete from   " + tableName + " where " + refName + "='" + refValue + "'");

                if (childTable == null)
                    childTable = new List<DataSet>();
                childTable.Add(ds);

                return ds;
            }
            return null;
        }
        /// <summary>
        /// 保存动态行
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet RowData(string xml, string tableName)
        {
            if (!string.IsNullOrWhiteSpace(xml))
            {
                xml = System.Web.HttpContext.Current.Server.UrlDecode(xml);
                Database database = null;
                if (!string.IsNullOrEmpty(ConName))
                    database = DatabaseFactory.CreateDatabase(ConName);
                else
                    database = DatabaseFactory.CreateDatabase();
                DataSet ds = database.ExecuteDataSet("select * from " + tableName + " where 1=2");
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                if (document.FirstChild.HasChildNodes)
                {
                    XmlNodeList nodeList = document.FirstChild.ChildNodes;
                    for (int i = 0; i < nodeList.Count; i++)
                    {

                        if (nodeList[i].HasChildNodes)
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["SysStatus"] = 0;
                            dr["Status"] = 0;
                            XmlNodeList list = nodeList[i].ChildNodes;
                            for (int x = 0; x < list.Count; x++)
                            {
                                if (ds.Tables[0].Columns.Contains(list[x].Name))
                                {
                                    if (list[x].HasChildNodes)
                                    {
                                        int j = 0; string temp = "";
                                        foreach (XmlNode node in list[x].ChildNodes)
                                        {

                                            if (node.HasChildNodes)
                                            {
                                                temp += node.FirstChild.Value; temp += "^";
                                            }
                                            else
                                                temp += node.Value;
                                            j++;
                                        }
                                        dr[list[x].Name] = temp;
                                    }

                                }
                            }
                            ds.Tables[0].Rows.Add(dr);
                        }

                    }
                }
                ds.Tables[0].TableName = tableName;
                return ds;
            }
            return null;
        }

        /// <summary>
        /// 获取动态行数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public string GetRowDataXml(string tableName, string key, string keyValue, string[] rowsArr)
        {
            if (keyValue != "")
            {
                Database database = null;
                if (!string.IsNullOrEmpty(ConName))
                    database = DatabaseFactory.CreateDatabase(ConName);
                else
                    database = DatabaseFactory.CreateDatabase();
                DataSet ds = database.ExecuteDataSet("select * from " + tableName + " where " + key + "='" + keyValue + "'");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    return GetRowDataXml(ds, rowsArr);
            }
            return "";
        }

        /// <summary>
        /// 获取动态行数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public string GetRowDataXml(string tableName, string key, string keyValue, string[] rowsArr, string Order)
        {
            if (keyValue != "")
            {
                Database database = null;
                if (!string.IsNullOrEmpty(ConName))
                    database = DatabaseFactory.CreateDatabase(ConName);
                else
                    database = DatabaseFactory.CreateDatabase();
                DataSet ds = database.ExecuteDataSet("select * from " + tableName + " where " + key + "='" + keyValue + "' " + Order + "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    return GetRowDataXml(ds, rowsArr);
            }
            return "";
        }



        /// <summary>
        /// 获取动态行数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public string GetRowDataXml(DataSet ds, string[] rowsArr)
        {
            string xml = "<xml>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["sysstatus"].ToString() != "-1")
                {
                    xml += "<RowData>";
                    DataRow dr = ds.Tables[0].Rows[i];
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {

                        if (dr[ds.Tables[0].Columns[j].ColumnName].ToString().Contains("^"))
                        {
                            xml += "<" + ds.Tables[0].Columns[j].ColumnName + ">";
                            string[] tm = dr[ds.Tables[0].Columns[j].ColumnName].ToString().Split('^');
                            foreach (string str in tm)
                            {
                                xml += "<checkbox>" + str + "</checkbox>";
                            }
                            xml += "</" + ds.Tables[0].Columns[j].ColumnName + ">";
                        }
                        else
                        {

                            var b = true;
                            if (rowsArr != null)
                            {
                                for (int x = 0; x < rowsArr.Length; x++)
                                {
                                    if (rowsArr[x] == ds.Tables[0].Columns[j].ColumnName)
                                    {
                                        xml += "<" + ds.Tables[0].Columns[j].ColumnName + ">" + Mapping.GetNameByGuid(dr[ds.Tables[0].Columns[j].ColumnName].ToString());
                                        xml += "</" + ds.Tables[0].Columns[j].ColumnName + ">";
                                        b = false;
                                        break;
                                    }
                                }
                            }
                            if (b)
                            {
                                xml += "<" + ds.Tables[0].Columns[j].ColumnName + ">" + dr[ds.Tables[0].Columns[j].ColumnName].ToString().Replace("\r\n","");
                                xml += "</" + ds.Tables[0].Columns[j].ColumnName + ">";
                            }
                        }

                    }
                    xml += "</RowData>";
                }
            }
            xml += "</xml>";
            return xml;
        }
        #endregion


        #region  表状态管理

        public int SetSate(string filed, string value)
        {
            Database database = null;
            if (!string.IsNullOrEmpty(ConName))
                database = DatabaseFactory.CreateDatabase(ConName);
            else
                database = DatabaseFactory.CreateDatabase();

            string sql = "update " + TableName + " set ";
            string[] fileds = filed.Split(',');              //支持更新多个字段和单个字段
            for (int i = 0; i < fileds.Length; i++)
            {
                sql += i > 0 ? "," : "";
                sql += fileds[i] + "=" + value.Split(',')[i];
            }
            sql += " where " + Key + "='" + KeyValue + "'";
            return database.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="value"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public int SetSate(string filed, string value, string guid)
        {
            Database database = null;
            if (!string.IsNullOrEmpty(ConName))
                database = DatabaseFactory.CreateDatabase(ConName);
            else
                database = DatabaseFactory.CreateDatabase();


            string sql = "update " + TableName + " set ";
            string[] fileds = filed.Split(',');              //支持更新多个字段和单个字段
            for (int i = 0; i < fileds.Length; i++)
            {
                sql += i > 0 ? "," : "";
                sql += fileds[i] + "=" + value.Split(',')[i];
            }
            sql += "   where " + Key + " in (" + guid + ")";

            return database.ExecuteNonQuery(sql);
        }

        #endregion




    }
}
