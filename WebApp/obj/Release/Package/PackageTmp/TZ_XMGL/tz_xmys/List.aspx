<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_xmys.List" %>

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
		<div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：项目验收信息 </div>
		<div class="MenuRight">
			
			<a style="cursor:pointer" id="search" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;查询</a>
			<a style="cursor:pointer" href="create.aspx?ProjGuid=<%=strProjGuid %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/add.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;新建</a>
			&nbsp;<a style="cursor:pointer" onclick="deleteRow()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/delete_ico.gif" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>
			
			
			     
		</div>
	</div>
	<div id="grid" style="margin:9px 0px 0px 12px"></div>
	</div>

	<%--<div id="win" class="SearchWin" >
		<table class="table" cellpadding="0" cellspacing="0">
            <tr>
                <td class="TdLabel" style="width:15%">进场时间</td>
				<td class="TdContent" style="width:35%">
                    <input  type="text" class="Wdate" onfocus="WdatePicker()" readonly="readonly" id="ApplyingDateBg" style="width:43%"/>
                    &nbsp;&nbsp;至&nbsp;&nbsp;<input  type="text"  class="Wdate" onfocus="WdatePicker()" readonly="readonly" id="ApplyingDateEnd" style="width:43%"/>
				</td>
            </tr>
            <tr>
                <td class="TdLabel" style="width:15%">材料名称</td>
                <td class="TdContent" style="width:35%"> 
                   <asp:TextBox runat="server" ID="MaterialName" ></asp:TextBox>
                </td>
				<td class="TdLabel" style="width:15%">信息状态</td>
				<td class="TdContent" style="width:35%">
                    <asp:DropDownList ID="Status" runat="server" style="width:99%">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="2">已退回</asp:ListItem>
                    </asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="TdContent" style="text-align:center" colspan="4"><input style="width:45px" type="button" class="searchbtn"  onclick="_searchData()" id="btn_s" value="查询" /><input class="searchreset" type="button" style="width:45px"  id="btn_r" value="重置" /></td>
			</tr>
		</table>

	</div>--%>
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
    cfg.tableName = "tz_xmys";
    cfg.sortName = "yssj";
    cfg.order = "desc";
    cfg.pageCount = 14;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = " and sysstatus!=-1";
    cfg.condition = " <%=strOtherSql%>";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h;
    cfg.request = "ajax";


    cfg.render = function (value, rowsData, key) {
        var v = "";

        return "<a style='cursor:pointer;' title='" + value + "' href='View.aspx?ProjGuid=<%=strProjGuid%>&Guid=" + rowsData.guid + "'>" + value + "</a>";

    }



    cfg.columns = [
		{ field: "guid", width: "40px", checkbox: true },
        { field: "yssj", name: "验收时间", align: "left", render: cfg.render, drag: true },
        { field: "ysnr", name: "验收内容", width: "10%", align: "center", render: cfg.render, drag: true },
		{ field: "yszj", name: "验收专家", width: "10%", align: "center", render: cfg.render, drag: true },
        { field: "ysjg", name: "验收结果", width: "10%", align: "left", render: cfg.render, drag: true }
    ];




    $(function () {
        $("#grid").Grid(cfg);
    });

    function deleteRow() {
        if (confirm("确认要删除么？") == false) {
            return;
        }
        //获取选中的内容，删除
        var selectRows = $("#grid").GetSelection("total");
        var ids = "";
        var rowCount = 0;
        rowCount = selectRows.length;
        $(selectRows).each(function () {
            ids += "'" + $(this)[0].guid + "',";
        });

        $.ajax({
            type: 'POST',
            url: "../XMGLHandler.ashx",
            data: { action: "deleteitem", ids: ids, tablename: "tz_xmys" },
            success: function (e) {
                //console.log("sucess");
                if (e != rowCount) {
                    alert("选中" + rowCount + "条，成功删除" + e);
                } else {
                    alert("删除成功！");
                }
                //刷新当前页面
                $("#grid").Grid(cfg);
            }
        });


       // $("#grid").DetelteRow("<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx");
    }

    var config = new Config();
    //config.search();



</script>

