<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectProjUnitAndPerson.aspx.cs" Inherits="Yawei.App.Shared.SelectProjUnitAndPerson" %>

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
                <div style="display: table-cell; width: 55%">
                    <div id="grid"></div>
                </div>
                <div id="targetTable" style="display: table-cell; width: 40%; border: 1px solid lightgray; height: 380px; vertical-align: top">
                    <div id="grid1"></div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">

    var pguid = "<%=projGuid%>";


    var cfg1 = [];
    cfg1.connectionName = "";
    cfg1.connectionString = "";
    cfg1.providerName = "";
    cfg1.tableName = "";
    cfg1.sortName = "";
    cfg1.order = "";
    cfg1.pageCount = 10;
    cfg1.where = "";
    cfg1.condition = " and sysstatus<>-1";
    cfg1.width = "99%";
    cfg1.height = 380;
    cfg1.request = "ajax";

    cfg1.SelectedRows = function (rowsData) {
        $("#unitPerson").children().remove();
        $("#unitPerson").append("");
    }

    cfg1.render = function (value, rowsData, key) {
        return value;
    }

    cfg1.columns = [
		{ field: "perGuid", width: "30px", radiobox: true },
		{ field: "Name", name: "姓名", align: "left", width: "55%", render: cfg1.render, order: true },
        { field: "Duty", name: "职务", align: "left", render: cfg1.render }
    ];



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
    cfg.condition = " and sysstatus<>-1";
    cfg.ajaxUrl = "SelectProjUnitAndPerson.aspx?type=getUnit&pGuids=" + pguid;
    cfg.width = "99%";
    cfg.height = 380;
    cfg.request = "ajax";

    cfg.SelectedRows = function (rowsData) {
        cfg1.ajaxUrl = "../Handlers/GetPersonByUnit.ashx?ProjGuid=" + pguid + "&UnitGuid=" + rowsData.Guid;
        $("#grid1").Grid(cfg1);
        $("#grid1").find("input[type='radio']").each(function () {
            $(this).attr("name", "name1");
        });
    }

    cfg.render = function (value, rowsData, key) {
        return value;
    }

    cfg.columns = [
		{ field: "Guid", width: "30px", radiobox: true },
		{ field: "UnitsName", name: "单位名称", align: "left", width: "55%", render: cfg.render, order: true },
        { field: "OrganizationCode", name: "机构代码", align: "left", render: cfg.render }
    ];

    $(function () {
        $("#grid").Grid(cfg);
        cfg1.ajaxUrl = "../Handlers/GetPersonByUnit.ashx?ProjGuid=&UnitGuid=";
        $("#grid1").Grid(cfg1);
    });

    function search() {
        cfg.ajaxUrl = "SelectProjUnitAndPerson.aspx?type=getUnit&uName=" + escape($('#UnitsName').val()) + "&pGuids=" + pguid;
        cfg1.ajaxUrl = "../Handlers/GetPersonByUnit.ashx?ProjGuid=&UnitGuid=";
        $("#grid1").Grid(cfg1);
        $("#grid").Grid(cfg);
    }

    function _confirm() {
        var units = $("#grid").GetSelection("total");
        var persons = $("#grid1").GetSelection("total");
        if (units.length > 0 && persons.length > 0) {
            window.parent.selunitspers(units, persons);
            $(window.parent.document).find(".dialog").slideUp();
        }
        else {
            alert("至少选择一个单位及人员");
        }
    }



</script>
