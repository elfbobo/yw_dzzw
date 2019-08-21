<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BudgetDetailSelect.aspx.cs" Inherits="Yawei.App.Shared.BudgetDetailSelect" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <script src="../Plugins/jquery.min.js"></script>
    <script src="../Plugins/jquery.grid/jquery.treegrid.js"></script>
     <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        table, tr, td,tbody {
        border-collapse:collapse;
        border-spacing: 2px;
border-color: gray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="FormMenu">
		<div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
            &nbsp;&nbsp;当前位置：<div style="display:inline"><input type="hidden" id="ProjGuid"  value="<%=Request["ProjGuid"] %>"/></div>概算明细</div>
	<div class="MenuRight">
			<input type="button" onclick="checkThis()" class="fileBtn" value="确定" style="margin-right:25px "/>
		
            
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
     { field: "Guid", width: "30px", radiobox: true },
     { field: "code", name: "编号",width: "15%", align: "left", render: cfg.render, collapsible: true },
    { field: "ProjOrCostName", name: "工程或费用名称", width: "20%", align: "left", render: cfg.render },
    { field: "SubmitAccount", name: "报送金额（万）", width: "15%", align: "center", render: cfg.render, drag: true },
    { field: "AuditingAccount", name: "审核金额（万）", width: "15%", align: "left", render: cfg.render, order: true, drag: true },
    { field: "AuthorizeAccount", name: "审定金额（万）",width: "15%", align: "left", render: cfg.render, order: true, drag: true },
    { field: "TrialReductionAccount", name: "审减金额（万）", width: "15%", align: "left", render: cfg.render, order: true, drag: true }
       
    ];

    $("#treegrid").TreeGrid(cfg);

    function checkThis() {
        var json = $("#treegrid").GetSelection("total");
       window.parent.selectBudget(json[0].Guid,json[0].ProjOrCostName);
    }
</script>
