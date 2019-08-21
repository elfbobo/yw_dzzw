<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseUsers.aspx.cs" Inherits="Yawei.App.Support.UserManage.ChooseUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../Content/Layout.css" rel="stylesheet" />
    <style type="text/css">
      input[type='button'] {
        
        padding:2px;
        border:1px solid #a3cdf3 ;
        border-radius:3px;
        background-color:#ffffff;
        cursor:pointer;
     }
          input[type='text'], select {
            padding:2px;
            border:1px solid #a3cdf3 ;
          }
        .selectuser {
            border-bottom:1px dashed lightgray;line-height:25px;font-size:14px;padding-left:10px; text-overflow: ellipsis;overflow: hidden;white-space: nowrap;
        }
        </style>
</head>
<body >
    <form id="form1" runat="server">
        <div class="DivGrayTop">
            <span>选择用户</span>
            <div id="search" style="float:right;font-size:14px;margin-right:20px">
            工作组:<select id="Group" style="min-width: 90px;"><%=ou%></select>
            用户名称:<input type="text" id="Name"  />
            <input type="button" value="查询" id="btnSearch" onclick="Search()" />
            <input type="button" value="重置" id="btnReset" onclick="Reset()"/>
            <input type="button" value="保存" id="btnSave" onclick="Save()"/>
        </div>
        </div>

    <div style="display:table;width:100%" >
        <div id="srcTable" style="display:table-cell"></div>
	
        <div id="targetTable" style="display:table-cell;width:20%;border:1px solid lightgray;height:450px;vertical-align:top">
            <div class="DivGrayTop" style="background-color:#E0ECFF">
            <span>已选择的用户</span>
            </div>
            <div id="user">
               
            </div>
        </div>
    </div>
    </form>
</body>
</html>
<script  type="text/javascript">
    model = '<%=model%>';
    var cfg = {};

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
    cfg.ajaxUrl = "ChooseUsers.aspx?Model=<%=model%>&Opt=GetUsers&Group=" + escape($("#Group").val() )+ "&Name=" +escape( $("#Name").val());
	cfg.width = "60%";
	cfg.height = 450;
	cfg.request = "ajax";
	cfg.SelectedRows = function (rowsData) {
	      
	    $("#user").append("<div onclick='RemoveUser(this)' guid='" + rowsData.UserGuid + "' user='" + JSON.stringify(rowsData) + "' class='selectuser'>" + rowsData.UserCN + "</div>");
	}
	cfg.CannelRows = function (rowsData) {
	    $("div[guid='" + rowsData.UserGuid + "']").remove();
	}


	<%--cfg.render = function (value, rowsData, key) {
	    return "<a style='cursor:pointer;' title='" + value + "' href='View.aspx?Guid=" + rowsData.Guid + "&ProjGuid=<%=Request["ProjGuid"]%>&Type=1'>" + value + "</a>";
	}--%>


	cfg.columns = [
		{ field: "UserGuid", width: "50px", checkbox: true },
		{ field: "UserCN", name: "用户简名", width: "20%", align: "left", render: cfg.render, order: true, drag: true },
		{ field: "UserLoginName", name: "用户账号", width: "20%", align: "left", render: cfg.render, drag: true },
		{ field: "UserDN", name: "用户全名", width: "57%", align: "left", render: cfg.render, order: true, drag: true }
    ];


	$(function () {
	    $("#srcTable").Grid(cfg);
	});

	function Search()
	{
	    cfg.ajaxUrl = "ChooseUsers.aspx?Model=<%=model%>&Opt=GetUsers&Group=" +escape( $("#Group").val()) + "&Name=" +escape( $("#Name").val());
	    $("#srcTable").Grid(cfg);
	}

    function RemoveUser(contorl)
    {
        $(contorl).remove();
    }

    function Save() {
        var result = new Array();
        var rowresuly = $("#user").find("div");
        if (rowresuly.length > 0) {
            for (var i = 0; i < rowresuly.length; i++) {
                var user =JSON.parse($( rowresuly[i]).attr("user"));
                result[result.length] = user.UserGuid;
                result[result.length] = user.UserLoginName;
                result[result.length] = user.UserType;
                result[result.length] = user.UserDN;
                result[result.length] = user.UserCN;
            }
        }
        window.returnValue = result.join('&');
        
        window.close();
    };

</script>