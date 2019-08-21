﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_ErrorLog.aspx.cs" Inherits="Yawei.App.Support.SysLog.Sys_ErrorLog" %>

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
                &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;错误日志
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
                    <td class="TdLabel">操作人</td>
                    <td class="TdContent">
                        <input name="" id="username" type="text" style="height: 25px; border: 1px solid #cccccc;" />
                    </td>
                    <td class="TdLabel">报错页面</td>
                    <td class="TdContent">
                        <input name="" id="ErrorPage" type="text" style="height: 25px; border: 1px solid #cccccc;" />
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>错误时间起</td>
                    <td class='TdContent'>
                        <asp:TextBox runat="server" ID="DateBegin" Class="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                    <td class='TdLabel'>错误时间止</td>
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
    cfg.tableName = "Sys_ErrorLog";
    cfg.sortName = "ErrorTime";
    cfg.order = "desc";
    cfg.pageCount = 14;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "";
    cfg.condition = "";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h;
    cfg.parentfield = "";
    cfg.childfield = "";
    cfg.request = "ajax";
    cfg.Pagination = true;

    cfg.columns = [
		{ field: "Guid", width: "40px", checkbox: true },
		{ field: "username", name: "操作人", align: "left", width: "25%" },
        { field: "ErrorPage", name: "报错页面", align: "left" },
        { field: "Errortime", name: "错误时间", width: "25%", align: "left" }
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
        var username = $("#username").val();
        if (username != "") {
            cfg.where += "and username like '%" + username + "%'";
        }
        var ErrorPage = $("#ErrorPage").val();
        if (ErrorPage != "") {
            cfg.where += "and ErrorPage like '%" + ErrorPage + "%'";
        }

        var DateBegin = $("#DateBegin").val();
        var DateEnd = $("#DateEnd").val();
        if (DateBegin != "") {
            cfg.where += "and ErrorTime>='" + DateBegin + "'";
        }
        if (DateEnd != "") {
            cfg.where += "and ErrorTime<='" + DateEnd +" 23:59:59 " + " '";
        }

        $("#grid").Grid(cfg);
        $('#win').hide();
        cfg.where = "";
    }

</script>

