using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yawei.SupportCore;

namespace Yawei.App.Support.Mapping
{
    public partial class Directory : System.Web.UI.Page
    {
        //超级父节点ID：19ce7196-04ef-4f6a-893e-2981127d9bb5

        private string Type = string.Empty;//传值类型
        private string Names = string.Empty;//名称
        private string TreeGuid = string.Empty;//主键
        private string aimId = string.Empty;//目标ID
        private string aimPid = string.Empty;//目标Pid
        private string aimName = string.Empty;//目标名称
        private string Status = string.Empty;//判断是否新建标识符
        private string NewCode = string.Empty;
        private string NewSubstance = string.Empty;

        protected string TableHTML = string.Empty;
        protected string Jsons = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Type = Request["type"] != null ? Request["type"] : "";
            if (Type == "post")
            {
                TreeGuid = Request["treeguid"] != null ? Request["treeguid"] : "";
                string result = MappingCore.GetNameRoute(TreeGuid);
                if (result != "0")
                {
                    if (result == "")
                    {
                        TableHTML = "<div class='DivBlueTop'>&nbsp;&nbsp;&nbsp;&nbsp;映射数据<input style='width:10%;font-size:12px;float:right;margin-top:2px;' type='button' value='保存' onclick='saveNew()' /></div>";
                        TableHTML += "<table style='width:100%;margin-top:10px'>";
                        TableHTML += "<tr><td style='width:8%;text-align:right;padding:5px;border-bottom:1px dashed lightgray;'>名称：</td><td style='width:33%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:98%' type='text' name='Code' /></td><td style='width:8%;text-align:right;padding: 5px;border-bottom:1px dashed lightgray;'>备注：</td><td style='width:33%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:98%' type='text' name='Substance' /></td><td style='width:15%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:100%;height:20%;font-size:12px;' type='button' value='添加' onclick='addTr()' /></td></tr>";
                        TableHTML += "</table>";
                    }
                    else
                    {
                        TableHTML = "<div class='DivBlueTop'>&nbsp;&nbsp;&nbsp;&nbsp;映射数据<input style='width:10%;font-size:12px;float:right;margin-top:2px;' type='button' value='编辑' onclick='edit()' /><input style='width:10%;font-size:12px;float:right;margin-top:2px;' type='button' value='新建' onclick='jumpNew()' /></div>";
                        TableHTML += "<table style='width:100%;margin-top:10px'>";
                        for (int i = 0; i < result.Split(';').Length; i++)
                        {
                            TableHTML += "<tr><td style='width:8%;text-align:right;padding:5px;border-bottom:1px dashed lightgray;'>名称：</td><td style='width:40%;padding:5px;border-bottom:1px dashed lightgray;'>" + result.Split(';')[i].ToString().Split(',')[0] + "</td><td style='width:8%;text-align:right;padding:5px;border-bottom:1px dashed lightgray;'>备注：</td><td style='width:40%;padding:5px;border-bottom:1px dashed lightgray;'>" + result.Split(';')[i].ToString().Split(',')[1] + "</td></tr>";
                        }
                        TableHTML += "</table>";
                    }
                }
                else
                {
                    TableHTML = "0";
                }
                Response.Clear();
                Response.Flush();
                Response.Write(TableHTML);
                Response.End();
            }
            else if (Type == "postsave")
            {
                aimId = Request["aimId"] != null ? Request["aimId"] : "";
                aimPid = Request["aimPid"] != null ? Request["aimPid"] : "";
                aimName = Request["aimName"] != null ? Request["aimName"] : "";
                Status = Request["status"] != null ? Request["status"] : "";
                int result =MappingCore.TreeSave(aimId, aimPid, aimName, Status);
                Response.Clear();
                Response.Flush();
                Response.Write(result.ToString());
                Response.End();
            }
            else if (Type == "postdel")
            {
                aimId = Request["aimId"] != null ? Request["aimId"] : "";
                int result =MappingCore.TreeDel(aimId);
                Response.Clear();
                Response.Flush();
                Response.Write(result.ToString());
                Response.End();
            }
            else if (Type == "newdata")
            {
                aimId = Request["aimId"] != null ? Request["aimId"] : "";
                aimPid = Request["aimPid"] != null ? Request["aimPid"] : "";
                aimName = Request["aimName"] != null ? Request["aimName"] : "";
                int result = 0;
                if (aimId != "" && aimName != "" && aimPid != "")
                {
                    NewCode = Request["newCodes"] != null ? Request["newCodes"] : "";
                    NewSubstance = Request["newSubstances"] != null ? Request["newSubstances"] : "";
                    result =MappingCore.NewData(aimName, aimId, aimPid, NewCode, NewSubstance);
                }
                Response.Clear();
                Response.Flush();
                Response.Write(result.ToString());
                Response.End();
            }
            else if (Type == "edit")
            {
                TreeGuid = Request["treeguid"] != null ? Request["treeguid"] : "";
                string result =MappingCore.GetNameRoute(TreeGuid);
                if (result != "0")
                {
                    TableHTML = "<div class='DivBlueTop'>&nbsp;&nbsp;&nbsp;&nbsp;映射数据<input type='button' style='width:10%;font-size:12px;float:right;margin-top:2px;' value='保存' onclick='saveNew()' /><input type='button' style='width:10%;font-size:12px;float:right;margin-top:2px;' value='新建' onclick='jumpNew()' /></div>";
                    TableHTML += "<table style='width:100%;margin-top:10px'>";
                    for (int i = 0; i < result.Split(';').Length; i++)
                    {
                        TableHTML += "<tr><td style='width:8%;padding:5px;border-bottom:1px dashed lightgray;'>名称：</td><td style='width:33%;padding:5px;border-bottom:1px dashed lightgray;'><input name='Code' style='width:98%' value='" + result.Split(';')[i].ToString().Split(',')[0] + "' type='text' /></td><td style='width:8%;padding:5px;border-bottom:1px dashed lightgray;'>内容：</td><td style='width:33%;padding:5px;border-bottom:1px dashed lightgray;'><input name='Substance' style='width:98%' value='" + result.Split(';')[i].ToString().Split(',')[1] + "' type='text' /></td><td style='width:15%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:100%;height:20%;font-size:12px;' type='button' value='删除' onclick='delEdit(this)' /></td></tr>";
                    }
                    TableHTML += "</table>";
                }
                else
                {
                    TableHTML = "0";
                }
                Response.Clear();
                Response.Flush();
                Response.Write(TableHTML);
                Response.End();
            }

            Jsons =MappingCore.GetTreeJson();
        }
    }
}