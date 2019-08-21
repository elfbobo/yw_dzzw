<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectsSelected.aspx.cs" Inherits="Yawei.App.Shared.ProjectsSelected" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <link href="../Content/Layout.css" rel="stylesheet" />
    <style type="text/css">
           .selectuser {
            border-bottom:1px dashed lightgray;line-height:25px;font-size:14px;padding-left:10px; text-overflow: ellipsis;overflow: hidden;white-space: nowrap;
            text-align:left;cursor:pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     	<div style="width:100%; text-align:center">
	   <table class="table" cellpadding="0" cellspacing="0">
           <tr>
               <td class="TdLabel" style="width:11%">项目名称</td>
               <td class="TdContent"><input type="text" style="width:65%" id="deptName" /> 
                   <input type="button" onclick="search()" class="fileBtn" value="查询" style="margin-left:5px" />
                   <input type="button" onclick="_confirm()" class="fileBtn" value="确定" style="margin-left:5px" />
               </td>
           </tr>
	   </table>
	<div style="display:table">
        <div style="display:table-cell;width:68%">
            <div id="grid" ></div>
        </div>
         <div id="targetTable" style="display:table-cell;width:30%;border:1px solid lightgray;height:380px;vertical-align:top">
            <div class="DivGrayTop" style="background-color:#E0ECFF">
            <span>已选择的项目</span>
            </div>
            <div id="proj">
               
            </div>
        </div>
	</div>
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
    cfg.tableName = "";
    cfg.sortName = "";
    cfg.order = "";
    cfg.pageCount = 10;
    //cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "";
    cfg.condition = " ";
    cfg.ajaxUrl = "ProjectsSelected.aspx?type=getProj";
    cfg.width = "99%";
    cfg.height = 380;
    cfg.request = "ajax";

    cfg.SelectedRows = function (rowsData) {
        $("div[guid='" + rowsData.Guid + "']").remove();
        $("#proj").append("<div onclick='RemoveUser(this)' guid='" + rowsData.Guid + "' proj='" + JSON.stringify(rowsData) + "' class='selectuser'>" + rowsData.ProjName + "</div>");
    }

    cfg.CannelRows = function (rowsData) {
        $("div[guid='" + rowsData.Guid + "']").remove();
    }


    cfg.render = function (value, rowsData, key) {
        return value;
    }

    cfg.columns = [
		{ field: "Guid", width: "30px", checkbox: true },
		{ field: "ProjName", name: "项目名称", align: "left", render: cfg.render, order: true, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);
        if ('<%=Request["projs"]%>' != "") {
            var proj = '<%=Request["projs"]%>'.split('$');
            var pt = proj[0].split(',');
            var pn = proj[1].split(',');
            for (var i = 0; i < pt.length; i++) {
                $("#proj").append("<div onclick='RemoveUser(this)' guid='" + pt[i] + "' proj='{\"Guid\":\"" + pt[i] + "\",\"ProjName\":\"" + pn[i] + "\"}' class='selectuser'>" + pn[i] + "</div>");
            }
        }
        $(".gridfooter").hide();
    });

    function search() {
        cfg.ajaxUrl = "ProjectsSelected.aspx?type=getProj&projName=" + escape($('#deptName').val());
        $("#grid").Grid(cfg);
        $(".gridfooter").hide();
    }

    function _confirm() {
        var guid = '', name = '';
        var rowresuly = $("#proj").find("div");
        if (rowresuly.length > 0) {
            for (var i = 0; i < rowresuly.length; i++) {
                var proj = JSON.parse($(rowresuly[i]).attr("proj"));
                if (i > 0) {
                    guid += ","; name += ",";
                }

                guid += proj.Guid;
                name += proj.ProjName;
            }
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

   </script>