<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanSelect.aspx.cs" Inherits="Yawei.App.Shared.PlanSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
     	<div style="width:100%; text-align:center">
	   <table class="table" cellpadding="0" cellspacing="0">
           <tr>
               <td class="TdLabel" style="width:11%">计划选择</td>
               <td class="TdContent"><input type="text" style="width:65%" id="TaskName" /> 
                   <input type="button" onclick="search()" class="fileBtn" value="查询" style="margin-left:5px" />
                   <input type="button" onclick="_confirm()" class="fileBtn" value="确定" style="margin-left:5px" />
               </td>
           </tr>
	   </table>
	<div id="grid" style="margin:3px 0px 0px 5px;"></div>
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
    cfg.tableName = "Busi_ConstructionPlan";
    cfg.idField = "Guid",
    cfg.sortName = "ReportDate";
    cfg.order = "desc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "" + ("<%=strProjGuid%>" == "" ? "" : " and ProjGuid='<%=strProjGuid%>'") + "";
    cfg.condition = "";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = 380;
    cfg.request = "ajax";


    cfg.render = function (value, rowsData, key) {
        return value;
    }

    cfg.columns = [
        { field: "Guid", width: "30px", radiobox: true },
        { field: "TaskName", name: "计划名称", align: "left", render: cfg.render, collapsible: true },
        { field: "BeginDate", name: "计划开始时间", width: "15%", align: "center", render: cfg.render, drag: true },
        { field: "EndDate", name: "计划结束时间", width: "15%", align: "left", render: cfg.render, order: true, drag: true },
        { field: "TaskLimit", name: "施工计划工期",width: "15%", align: "left", render: cfg.render, order: true, drag: true },
        { field: "Quantity", name: "计划工程量", width: "15%", align: "left", render: cfg.render, order: true, drag: true }
       
    ];

    $(function () {
        $("#grid").Grid(cfg);
    });

    function search() {
        if ($('#TaskName').val() != "")
            cfg.where = " and TaskName like '%" + $('#TaskName').val() + "%'";
        $("#grid").Grid(cfg);
    }
    function _confirm() {
        var rowsData = $("#grid").GetSelection("total");
        if (rowsData.length == 0) {
            alert("请选择施工计划！"); return;
        }
        window.parent.initialPlan(rowsData[0].Guid, rowsData[0].TaskName, rowsData[0].BeginDate, rowsData[0].EndDate, rowsData[0].TaskLimit, rowsData[0].Quantity);
        $(window.parent.document).find("#dlgPlan").slideUp();
    }
</script>