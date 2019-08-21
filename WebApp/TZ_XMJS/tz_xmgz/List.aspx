<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.TZ_XMJS.tz_xmgz.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
        <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>
     <script src="../../js/layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   	<div style="width:100%; text-align:center">
	<div class="FormMenu">
		<div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：项目跟踪 </div>
		<div class="MenuRight">
			
             <%if (roleCheck.isAdmin()||roleCheck.isBm()){ %>
			<a style="cursor:pointer" href="create.aspx?xmguid=<%=strProjGuid %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/add.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;新建</a>

             <a style="cursor: pointer" id="commit" href="javascript:commitChange();">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cup_go.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;提交</a>

			&nbsp;<a style="cursor:pointer" onclick="deleteRow()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/delete_ico.gif" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>
			<%} %>
			
			     
		</div>
	</div>
	<div id="grid" style="margin:9px 0px 0px 12px"></div>
	</div>

    </form>

        <div id="file-container">
            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">附件列表</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>
           </div>
    <div id="hisfile-container">
            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">附件列表</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>
           </div>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">

    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "tz_xmgz";
    cfg.sortName = "CreateDate";
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
        if (key === "status") {
            if (value === "0") {
                value="待提交"
            }
            else if (value === "1") {
                value="待审核"
            }
            else if (value === "2") {
                value="退回"
            }
            else if (value === "3") {
                value="通过"
            }
        }
        else if (key === "bgnr") {
            if (rowsData.bglx === "建设方案" || rowsData.bglx === "项目申报表" || rowsData.bglx === "云资源使用申请表") {
                return "<a style='cursor:pointer;' title='" + value + "' onclick=\"showNewFile('" + rowsData.bglx + "','" + rowsData.bgnr + "')\">" + "查看" + "</a>";
            }
        }
        else if (key === "last_val") {
            if (rowsData.bglx === "建设方案" || rowsData.bglx === "项目申报表" || rowsData.bglx === "云资源使用申请表") {
                return "<a style='cursor:pointer;' title='" + value + "' onclick=\"showOldFile('" + rowsData.bglx + "','" + rowsData.last_val + "')\">" + "查看" + "</a>";
            }
        }
        return "<a style='cursor:pointer;' title='" + value + "' href='View.aspx?xmguid=<%=strProjGuid%>&Guid=" + rowsData.guid + "'>" + value + "</a>";

    }



    cfg.columns = [
		{ field: "guid", width: "10%", checkbox: true },
        { field: "bglx", name: "变更类型", width: "10%", align: "center", render: cfg.render, drag: true },
        { field: "last_val", name: "变更前", width: "25%", align: "center", render: cfg.render, drag: true },
        { field: "bgnr", name: "变更后", width: "25%", align: "center", render: cfg.render, drag: true },
      
		{ field: "bgsj", name: "变更日期", width: "10%", align: "center", render: cfg.render, drag: true },
        { field: "bgyy", name: "变更原因", width: "10%", align: "center", render: cfg.render, order: true, drag: true },
        { field: "status", name: "状态", width: "10%", align: "center", render: cfg.render, order: true, drag: true },
    ];

    var showNewFile = function (bglx, refguid) {
        $('#filecontent0').empty()
        var filesign = "";
        if (bglx === "建设方案") {
            filesign = "tz_Project_Fa";
        }
        else if (bglx == "项目申报表") {
            filesign = "tz_Project_Sbb";
        }
        else if (bglx == "云资源使用申请表") {
            filesign = "tz_Project_Yzy";
        }
        var cfg0 = [];
        cfg0.refGuid = refguid;
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = filesign;
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

        layer.open({
            type: 1,
            title: '查看替换版本',
            skin: 'layui-layer-demo', //样式类名
            area: ['900px', '250px'],
            closeBtn: 1, //不显示关闭按钮
            anim: 2,
            shadeClose: true, //开启遮罩关闭
            content: $('#file-container')
        });
    }
    
    var showOldFile = function (bglx, refguid) {

        $("#filecontent1").empty();

        var filesign = "";
        if (bglx === "建设方案") {
            filesign = "tz_Project_Fa";
        }
        else if (bglx == "项目申报表") {
            filesign = "tz_Project_Sbb";
        }
        else if (bglx == "云资源使用申请表") {
            filesign = "tz_Project_Yzy";
        }

        var cfg1 = [];
        cfg1.refGuid = refguid;
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = filesign;
        cfg1.view = true;
        $("#filecontent1").fileviewload(cfg1);

        layer.open({
            type: 1,
            title: '查看历史版本',
            skin: 'layui-layer-demo', //样式类名
            area: ['900px', '250px'],
            closeBtn: 1, //不显示关闭按钮
            anim: 2,
            shadeClose: true, //开启遮罩关闭
            content: $('#hisfile-container')
        });
    }


    //提交
    var commitChange = function () {

        var userguid = '<%=CurrentUser.UserGuid%>';
        var bmguid = '<%=CurrentUser.UserGroup.Guid%>';

        var selectRows = $("#grid").GetSelection("total");
        var ids = "";
        var rowCount = 0;
        rowCount = selectRows.length;

        if (rowCount == 0) {
            alert("请选择要提交的项目变更");
            return;
        }

        $(selectRows).each(function () {
            ids += "'" + $(this)[0].guid + "',";
        });

        $.ajax({
            type: "post",
            url: "../XMJSHandler.ashx",
            data: { action: "commitchange", changeids: ids, userguid: userguid, bmguid: bmguid },
            cache: false,
            async: false,
            dataType: "json",
            success: function (obj) {
                $("#grid").Grid(cfg);
                alert("成功提交" + obj.Data + "条项目申请");
            }
        });
    }



    $(function () {
        $("#grid").Grid(cfg);
    });

    function deleteRow() {


        //获取选中的内容，删除
        var selectRows = $("#grid").GetSelection("total");
        var ids = "";
        var rowCount = 0;
        rowCount = selectRows.length;
        if (rowCount == 0) {
            alert("请选择要删除的项目！");
            return;
        }
        //删除数据
        if (confirm("确认要删除么？") == false) {
            return;
        }
        $(selectRows).each(function () {
            ids += "'" + $(this)[0].guid + "',";
        });

        $.ajax({
            type: 'POST',
            url: "../XMJSHandler.ashx",
            data: { action: "deleteitem", ids: ids, tablename: "tz_xmgz" },
            success: function (e) {
                //console.log("sucess");
                if (e != rowCount) {
                    alert("选中" + rowCount + "条，成功删除" + e + "条");
                } else {
                    alert("删除成功！");
                }
                //刷新当前页面
                $("#grid").Grid(cfg);
            }
        });
    }

    var config = new Config();



</script>

