<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuilderUnitSelected.aspx.cs" Inherits="Yawei.App.Shared.BuilderUnitSelected" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <link href="../Content/Layout.css" rel="stylesheet" />
    <style type="text/css">
        .selectuser {
            border-bottom: 1px dashed lightgray;
            line-height: 25px;
            font-size: 14px;
            padding-left: 10px;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            text-align: left;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="TdLabel" style="width: 11%">单位名称</td>
                    <td class="TdContent">
                        <input type="text" style="width: 65%" id="UnitsName" />
                        <input type="button" onclick="search()" class="fileBtn" value="查询" style="margin-left: 5px" />
                        <input type="button" onclick="_confirm()" class="fileBtn" value="确定" style="margin-left: 5px" />
                    </td>
                </tr>
            </table>
            <div style="display: table">
                <div style="display: table-cell; width: 59%">
                    <div id="grid"></div>
                </div>
                <div id="targetTable" style="display: table-cell; width: 49%; border: 1px solid lightgray; height: 380px; vertical-align: top">
                    <div class="DivGrayTop" style="background-color: #E0ECFF">
                        <span>已选择的部门</span>
                    </div>
                    <div id="unit">
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">

    var pguid = "<%=projGuid%>";

    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "";
    cfg.sortName = "";
    cfg.order = "";
    cfg.pageCount = 10;
    //cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "";
    cfg.condition = "  and sysstatus<>-1";
    cfg.ajaxUrl = "BuilderUnitSelected.aspx?type=getUnit&pGuids=" + pguid;
    cfg.width = "99%";
    cfg.height = 380;
    cfg.request = "ajax";
    cfg.pagination = false;
    cfg.SelectedRows = function (rowsData) {
        $("div[guid='" + rowsData.Guid + "']").remove();
        $("#unit").children().remove();
        $("#unit").append("<div onclick='RemoveUser(this)' guid='" + rowsData.Guid + "' unit='" + JSON.stringify(rowsData) + "' class='selectuser'>" + rowsData.UnitsName + "</div>");
    }

    cfg.CannelRows = function (rowsData) {
        $("div[guid='" + rowsData.Guid + "']").remove();
    }

    cfg.render = function (value, rowsData, key) {
        return value;
    }

    cfg.columns = [
		{ field: "Guid", width: "30px", radiobox: true },
		{ field: "UnitsName", name: "单位名称", align: "left", render: cfg.render, order: true, drag: true },
        { field: "UType", name: "单位类型", width: "30%", align: "left", render: cfg.render, order: true, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);
        if ('<%=Request["units"]%>' != "") {
            var unit = '<%=Request["units"]%>'.split('$');
            $("#unit").append("<div onclick='RemoveUser(this)' guid='" + unit[0] + "' unit='{\"Guid\":\"" + unit[0] + "\",\"UnitsName\":\"" + unit[1] + "\"}' class='selectuser'>" + unit[1] + "</div>");
        }
    });

    function search() {
        cfg.ajaxUrl = "BuilderUnitSelected.aspx?type=getUnit&uName=" + escape($('#UnitsName').val()) + "&pGuids=" + pguid;
        $("#grid").Grid(cfg);
    }

    function _confirm() {
        var guid = '', name = '';
        var rowresuly = $("#unit").find("div");
        if (rowresuly.length > 0) {
            var unit = JSON.parse($(rowresuly[0]).attr("unit"));
            guid = unit.Guid;
            name = unit.UnitsName;
        }
        var id = '<%=Request["ID"]%>';
        if (id != '') {
            $(window.parent.document).find('#' + id.split('@')[0]).eq(0).val(guid);
            $(window.parent.document).find('#' + id.split('@')[1]).eq(0).val(name);
        }
        $(window.parent.document).find(".dialog").slideUp();
    }

    function RemoveUser(contorl) {
        $(contorl).remove();
    }

    $("radio").bind("click", function () {

    })

</script>
