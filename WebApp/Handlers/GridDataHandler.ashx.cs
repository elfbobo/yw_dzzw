using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yawei.SupportCore;
using Yawei.FacadeCore;


namespace Yawei.App.Handlers
{
    /// <summary>
    /// GridDataHandler 的摘要说明
    /// </summary>
    public class GridDataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            context.Response.ContentType = "text/plain";
            if (context.Request["page"] != null)
            {
                SysGridCore dataGrid = new SysGridCore();

                dataGrid.ConnectionName = context.Request["connectionName"] != null ? context.Request["connectionName"] : "";
                dataGrid.ConnectionString = context.Request["connectionString"] != null ? context.Request["connectionString"] : "";
                dataGrid.ProviderName = context.Request["providerName"] != null ? context.Request["providerName"] : "";
                dataGrid.TableName = context.Request["tableName"];
                dataGrid.IdField = context.Request["idField"];
                dataGrid.Page = context.Request["page"];
                dataGrid.Rows = context.Request["rows"];
                dataGrid.Sort = context.Request["sort"];
                dataGrid.Order = context.Request["order"];

                dataGrid.ParentField = context.Request["parentfield"] != null ? context.Request["parentfield"] : "";
                dataGrid.ChildField = context.Request["childfield"] != null ? context.Request["childfield"] : "";

                dataGrid.RelevanSearch = context.Request["relevanSearch"] != null ? context.Request["relevanSearch"] : "false";
                try
                {

                    dataGrid.Condition = context.Request["condition"] != null ? EncryptUtils.uncMe(context.Request["condition"], "asdefzgereagjlg") : "";
                    dataGrid.Where = context.Request["where"] != null ? EncryptUtils.uncMe(context.Request["where"], "asdefzgereagjlg") : "";
                    dataGrid.ChildWhere = context.Request["childWhere"] != null ? EncryptUtils.uncMe(context.Request["childWhere"], "asdefzgereagjlg") : "";
                }
                catch (Exception ex)
                {
                    dataGrid.Condition = context.Request["condition"] != null ? context.Request["condition"] : "";
                    dataGrid.Where = context.Request["where"] != null ? context.Request["where"] : "";
                    dataGrid.ChildWhere = context.Request["childWhere"] != null ? context.Request["childWhere"] : "";

                }
                context.Response.Write(dataGrid.CreateDataJson());
            }
            else
            {
                string connectionName = context.Request["connectionName"] == null ? "" : context.Request["connectionName"];
                string tableName = context.Request["tableName"] == null ? "" : context.Request["tableName"];
                string where ="";
                try {
                    where = context.Request["where"] == null ? "" : EncryptUtils.uncMe(context.Request["where"], "asdefzgereagjlg");
                }
                catch (Exception ex)
                {
                    where = context.Request["where"] == null ? "" : context.Request["where"];
                }
                string refWhere = context.Request["refWhere"] == null ? "" : context.Request["refWhere"];
                try
                {
                    SysGridCore.DeleteRow(connectionName, tableName, where, refWhere);
                    context.Response.Clear();
                    context.Response.Write(true);
                }
                catch
                {
                    context.Response.Clear();
                    context.Response.Write(false);
                }
            }

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