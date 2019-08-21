<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjInviteSelected.aspx.cs" Inherits="Yawei.App.Shared.ProjInviteSelected" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Plugins/jquery.grid/jquery.treegrid.js" type="text/javascript"></script>
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        table, tr, td, tbody {
            border-collapse: collapse;
            border-spacing: 2px;
            border-color: gray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="FormMenu">
            <div class="MenuLeft">
                &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                &nbsp;&nbsp;当前位置：<div style="display: inline">
                    <input type="hidden" id="ProjGuid" value="<%=Request["ProjGuid"] %>" /></div>
                选择招标事项
            </div>
            <div class="MenuRight">
                <input type="button" onclick="checkThis()" class="fileBtn" value="确定" style="margin-right: 25px" />

            </div>
        </div>

        <div id="treegrid">
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    
    var cfg = [];
    cfg.width = "98%";
    cfg.height = 400;
    cfg.rowsData = <%=Json%>;
    // cfg.ajaxUrl = "...";
    cfg.Pagination = true;
    cfg.render = function (value, rowsData, key) {
        return "<a style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }

    cfg.columns = [
        { field: "Guid", width: "40px", radiobox: true },
        { field: "InviteName", name: "招标名称", align: "left", width: "15%", render: cfg.render, drag: true },
        { field: "InviteCode", name: "招标编号", align: "left", width: "15%", render: cfg.render, order: true, drag: true },
        { field: "NoticeCode", name: "公告号", align: "center", render: cfg.render, order: true, drag: true },
        { field: "InviteControlCost", name: "招标控制价", width: "15%", align: "center", render: cfg.render, order: true, drag: true }
    ];

    $("#treegrid").TreeGrid(cfg);

    function checkThis() {
        var json = $("#treegrid").GetSelection("total");
        window.parent.selectInvite(json[0].Guid,json[0].InviteName);
    }
</script>
