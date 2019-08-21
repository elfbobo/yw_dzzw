<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialSelected.aspx.cs" Inherits="Yawei.App.Shared.MaterialSelected" %>

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
               <td class="TdLabel" style="width:11%">材料名称</td>
               <td class="TdContent"><input type="text" style="width:65%" id="MaterialName" /> 
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
    cfg.tableName = "Busi_Con_MainMaterialApproach";
    cfg.sortName = "MaterialName";
    cfg.order = "asc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "";
    cfg.condition = " and sysstatus<>-1 and ProjGuid='<%=Request["ProjGuid"]%>'";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = 380;
    cfg.request = "ajax";


    cfg.render = function (value, rowsData, key) {
        return value;
    }

    cfg.columns = [
		{ field: "Guid", width: "30px", radiobox: true },
		{ field: "MaterialName", name: "材料名称", align: "left", render: cfg.render, drag: true },
		{ field: "FactoryUnitName", name: "生产厂家", width: "25%", align: "left", render: cfg.render, drag: true },
        { field: "ApproachTime", name: "进场时间", width: "12%", align: "left", render: cfg.render, order: true, drag: true },
        { field: "Model", name: "规格型号", width: "15%", align: "left", render: cfg.render, drag: true },
        { field: "num", name: "材料数量", width: "10%", align: "left", render: cfg.render, order: true, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);
    });

    function search() {
        cfg.where = " and MaterialName like '%" + $('#MaterialName').val() + "%'";
        $("#grid").Grid(cfg);
    }

    function _confirm() {
        var rowsData = $("#grid").GetSelection("total");
        if (rowsData.length == 0) {
            alert("请选择材料！");
            return;
        }
        window.parent.initMaterial(rowsData[0]);
        $(window.parent.document).find("#<%=Request["divid"]%>").slideUp();
    }
</script>
