<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractPayType.aspx.cs" Inherits="Yawei.App.Shared.ContractPayType" %>

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
    <form id="form1" runat="server">
     	<div style="width:100%; text-align:center">
	   <table class="table" cellpadding="0" cellspacing="0">
           <tr>
               <td class="TdLabel" style="width:11%">支付费用名称</td>
               <td class="TdContent"><input type="text" style="width:65%" id="PaymentName" /> 
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
    cfg.tableName = "V_ContractPayType";
    cfg.idField = "Guid",
    cfg.sortName = "PaymentNum";
    cfg.order = "asc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "";
    cfg.condition = "<%=OtherSql%>";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = 380;
    cfg.request = "ajax";


    cfg.render = function (value, rowsData, key) {
        return value;
    }

    cfg.columns = [
        { field: "Guid", width: "30px", checkbox: true },
		{ field: "PaymentNum", name: "支付条件序号", width: "10%", align: "left", render: cfg.render, order: true, drag: true },
        { field: "PaymentName", name: "支付费用名称", align: "left",render: cfg.render, order: true, drag: true },
        { field: "PaymentCondition", name: "支付条件", align: "left", width: "15%", render: cfg.render, order: true, drag: true },
        { field: "PayTimeControl", name: "付款时间要求", align: "left", width: "11%", render: cfg.render, order: true, drag: true },
        { field: "NewPayProperty", name: "支付性质", align: "left", width: "12%", render: cfg.render, order: true, drag: true },
        { field: "PaymentScale", name: "支付比例(%)", align: "left", width: "11%", render: cfg.render, order: true, drag: true },
        { field: "PayementCost", name: "支付金额（万）", align: "left", width: "13%", render: cfg.render, order: true, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);
    });

    function search() {
        var sql = "";
        if ($('#PaymentName').val() != "")
            sql += " and PaymentName like '%" + $('#PaymentName').val() + "%'";
        cfg.where = sql;

        $("#grid").Grid(cfg);
    }
    function _confirm() {
        var rowsData = $("#grid").GetSelection("total");
        if (rowsData.length == 0) {
            alert("请选择支付条件！");
        } else {
            window.parent.initialContractPayType(rowsData);
        }
        $(window.parent.document).find("#dlgContractPayType").slideUp();
    }
</script>
