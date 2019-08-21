<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.Project.ExpertManager.List" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   	<div style="width:100%; text-align:center">
	<div class="FormMenu">
		<div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：项目合同信息 </div>
		<div class="MenuRight">
			
			<a style="cursor:pointer" id="search" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;查询</a>
			<a style="cursor:pointer" href="create.aspx?ProjGuid=<%=strProjGuid %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/add.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;新建</a>
			&nbsp;<a style="cursor:pointer" onclick="deleteRow()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/delete_ico.gif" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>
			
			
			     
		</div>
	</div>
	<div id="grid" style="margin:9px 0px 0px 12px"></div>
	</div>

	<div id="win" class="SearchWin" >
		<table class="table" cellpadding="0" cellspacing="0">
            <tr>
                  <td class="TdLabel" style="width:15%">姓名</td>
                <td class="TdContent" style="width:35%"> 
                   <asp:TextBox runat="server" ID="zjmc" ></asp:TextBox>
                </td>
				 <td class="TdLabel" style="width:15%"></td>
                <td class="TdContent" style="width:35%"> 
                   
                </td>
            </tr>
            <tr>
                <td class="TdLabel" style="width:15%">工作单位</td>
                <td class="TdContent" style="width:35%"> 
                   <asp:TextBox runat="server" ID="gzdw" ></asp:TextBox>
                </td>
				 <td class="TdLabel" style="width:15%">联系电话</td>
                <td class="TdContent" style="width:35%"> 
                   <asp:TextBox runat="server" ID="lxdh" ></asp:TextBox>
                </td>
			</tr>
			<tr>
				<td class="TdContent" style="text-align:center" colspan="4"><input style="width:45px" type="button" class="searchbtn"  onclick="_searchData()" id="btn_s" value="查询" /><input class="searchreset" type="button" style="width:45px"  id="btn_r" value="重置" /></td>
			</tr>
		</table>

	</div>
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("#search").bind("click", function () {
            $("#win").css("top", "30px");
            $('#win').show(400);
        });
        $("#btn_r").bind("click", function () {
            $('#win').find(':input[type!="button"]').val('');
        });
    })
    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "tz_ProjectExpert";
    cfg.sortName = "rkxh";
    cfg.order = "desc";
    cfg.pageCount = 14;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "";
    cfg.condition = "";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h;
    cfg.request = "ajax";


    cfg.render = function (value, rowsData, key) {
        var v = "";

        return "<a style='cursor:pointer;' title='" + value + "' href='View.aspx?Guid=" + rowsData.Guid + "'>" + value + "</a>";

    }

    cfg.columns = [
		{ field: "Guid", width: "40px", checkbox: true },
        { field: "zjmc", name: "姓名", align: "center", render: cfg.render, drag: true },
    
		{ field: "lxdh", name: "联系电话", width: "15%", align: "center", render: cfg.render, drag: true },
        { field: "gzdw", name: "工作单位", width: "20%", align: "center", render: cfg.render, order: true, drag: true },
        { field: "zjzc", name: "职称", width: "15%", align: "center", render: cfg.render, order: true, drag: true },
        { field: "rkxh", name: "入库序号", width: "15%", align: "center", render: cfg.render, order: true, drag: true }
    ];




    $(function () {
        $("#grid").Grid(cfg);
    });

    function deleteRow() {
        $("#grid").DetelteRow("<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx");
    }

    var config = new Config();
    config.search();



</script>

