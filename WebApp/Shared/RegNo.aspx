<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegNo.aspx.cs" Inherits="Yawei.App.Shared.RegNo" %>

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
               <td class="TdLabel" style="width:13%">部门名称</td>
               <td class="TdContent" style="width:20%"><input type="text" style="width:85%" id="QYMC" /></td>
               <td class="TdLabel" style="width:13%">机构代码</td>
               <td class="TdContent" style="width:54%"><input type="text" style="width:35%" id="GSZCH" /> 
                   <input type="button" onclick="search()" class="fileBtn" value="查询" style="margin-left:10px" />
                   <input type="button" onclick="_confirm()" class="fileBtn" value="确定" style="margin-right:5px" />
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
    cfg.connectionName = "qiyebd";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "GS_JCXX";
    cfg.idField = "Guid",
    cfg.sortName = "GSZCH";
    cfg.order = "asc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "";
    cfg.condition = "";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = 380;
    cfg.request = "ajax";


    cfg.render = function (value, rowsData, key) {
        return value;
    }

    cfg.columns = [
        { field: "NBXH", width: "30px", radiobox: true },
		{ field: "QYMC", name: "企业名称", align: "left", render: cfg.render, order: true, drag: true },
        { field: "GSZCH", name: " 工商注册号 ", align: "center", width: "25%", render: cfg.render, order: true, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);
    });

    function search() {
        var sql = "";
        if ($('#QYMC').val() != "")
            sql += " and QYMC like '%" + $('#QYMC').val() + "%'";
        if ($('#GSZCH').val() != "")
            sql += " and GSZCH like '%" + $('#GSZCH').val() + "%'";
        cfg.where = sql;

        $("#grid").Grid(cfg);
    }
    function _confirm() {
        var rowsData = $("#grid").GetSelection("total");
        var id = '<%=Request["ID"]%>';
        if (id != '') {
            $(window.parent.document).find('#' + id).val(rowsData[0].GSZCH).focus();
        }

        $(window.parent.document).find(".dialog").slideUp();
    }
</script>