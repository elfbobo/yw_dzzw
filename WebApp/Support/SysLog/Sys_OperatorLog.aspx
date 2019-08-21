<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_OperatorLog.aspx.cs" Inherits="Yawei.App.Support.SysLog.Sys_OperatorLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../../Content/Form.css" rel="stylesheet" type="text/css" />
    <link href="../../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../../Content/css.css" rel="stylesheet" />
    <script src="../../../Plugins/datepicker/WdatePicker.js"></script>
    <link href="../../../Plugins/datepicker/skin/WdatePicker.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="FormMenu">
            <div class="MenuLeft">
                &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;操作日志
            </div>
            <div class="MenuRight">
                <a style="cursor: pointer" id="search">
                    <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查询</a>
            </div>
        </div>
        <div>
            <div id="grid" style="margin: 9px 0px 0px 12px">
            </div>
        </div>
        <div id="win" class="SearchWin">
            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="TdLabel">项目名称</td>
                    <td class="TdContent">
                        <input name="" id="ProjName" type="text" style="height: 25px; border: 1px solid #cccccc;" />
                    </td>
                    <td class="TdLabel"></td>
                    <td class="TdContent"></td>
                </tr>
                <tr>
                    <td class="TdLabel">操作人</td>
                    <td class="TdContent">
                        <input name="" id="UserCN" type="text" style="height: 25px; border: 1px solid #cccccc;" />
                    </td>
                    <td class="TdLabel">功能操作</td>
                    <td class="TdContent">
                        <input name="" id="OperatorType" type="text" style="height: 25px; border: 1px solid #cccccc;" />
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>操作时间起</td>
                    <td class='TdContent'>
                        <asp:TextBox runat="server" ID="DateBegin" Class="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                    <td class='TdLabel'>操作时间止</td>
                    <td class='TdContent'>
                        <asp:TextBox runat="server" ID="DateEnd" Class="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="TdContent" style="text-align: center" colspan="4">
                        <input style="width: 45px" type="button" class="searchbtn" id="btn_s" value="查询" onclick="_searchData()" /><input class="searchreset" type="button" style="width: 45px" id="btn_r" value="重置" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">

    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "Sys_OperatorLog";
    cfg.sortName = "OperatorTime";
    cfg.order = "desc";
    cfg.pageCount = 14;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = " and  userCN<>'李锋'  ";
    cfg.condition = "";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h;
    cfg.parentfield = "";
    cfg.childfield = "";
    cfg.request = "ajax";
    cfg.Pagination = true;

    cfg.render = function (value, rowsData, key) {
        if (key == "ProjGuid") {
            var cfgProjName = "";
            $.ajax({
                type: "post",
                url: "../../Project/BudgetPlan/getProjName.ashx",
                data: "ProjGuid=" + value,
                async: false,
                success: function (html) {
                    cfgProjName = html;
                }
            });
            return cfgProjName;
        }
    };


    cfg.columns = [
		{ field: "Guid", width: "40px", checkbox: true },
        { field: "ProjGuid", name: "项目名称", align: "left", render: cfg.render },
		{ field: "UserCN", name: "操作人", align: "left", width: "20%" },
        { field: "OperatorType", name: "功能操作", align: "left", width: "20%" },
        { field: "OperatorTime", name: "操作时间", width: "20%", align: "left" }
    ];

    $(function () {
        $("#search").bind("click", function () {
            $("#win").css("top", "30px");
            $('#win').show(400);
        });
        $("#btn_r").bind("click", function () {
            $('#win').find(':input[type!="button"]').val('');
        });
        $("#grid").Grid(cfg);
    });

    function _searchData() {
        var ProjName = $("#ProjName").val();
        if (ProjName != "") {
            cfg.where += "and ProjGuid in (select Guid from Busi_ProjRegister where projName like '%" + ProjName + "%' )";
        }
        var UserCN = $("#UserCN").val();
        if (UserCN != "") {
            cfg.where += "and UserCN like '%" + UserCN + "%'";
        }
        var OperatorType = $("#OperatorType").val();
        if (OperatorType != "") {
            cfg.where += "and OperatorType like '%" + OperatorType + "%'";
        }

        var DateBegin = $("#DateBegin").val();
        var DateEnd = $("#DateEnd").val();
        if (DateBegin != "") {
            cfg.where += "and OperatorTime>='" + DateBegin + "'";
        }
        if (DateEnd != "") {
            cfg.where += "and OperatorTime<='" + DateEnd + " 23:59:59 " + " ' ";
        }

        $("#grid").Grid(cfg);
        $('#win').hide();
        cfg.where = "";
    }

</script>
