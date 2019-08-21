<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.TZ_XMYS.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Plugins/jquery.grid/jquery.treegrid.js" type="text/javascript"></script>
    <%--<script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>--%>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../Plugins/jquery-easyui/jquery.easyui.min.js"></script>
    <link href="../Plugins/jquery-easyui/themes/default/easyui.css" rel="stylesheet" />
    <script src="../Plugins/highcharts/highcharts.js" type="text/javascript"></script>
    <style type="text/css">
        .tree-title {
            display: inline;
        }
    </style>
</head>
<body style="overflow: hidden">
    <form id="form1" runat="server">
        <div class="easyui-tabs">
            <div title="项目信息">
                <div style="width: 100%; text-align: center">
                    <div class="FormMenu">
                        <div class="MenuLeft">
                            &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：项目建设<label id="sqlwhere" style="display: none" hidden="hidden"></label>
                        </div>
                        <div class="MenuRight">

                            <a style="cursor: pointer" id="search">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查询</a>

                                                <a style="cursor: pointer" href="Index.aspx">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>

                        </div>
                    </div>
                    <div id="grid" style="margin: 9px 0px 0px 12px"></div>
                </div>

                <div id="win" class="SearchWin">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="TdLabel">项目名称</td>
                            <td class="TdContent">
                                <input type="text" action="like" datatype="varchar" id="ProName" />
                            </td>
                            <td class="TdLabel">申报部门</td>
                            <td class="TdContent">
                                <input type="text" action="like" datatype="varchar" id="StartDeptGuid" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TdLabel">联系人</td>
                            <td class="TdContent">
                                <input type="text" action="like" datatype="varchar" id="ContactName" />
                            </td>
                            <td class="TdLabel">投资额度</td>
                            <td class="TdContent">
                                <input type="text" action="like" datatype="varchar" id="Quota" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TdLabel">是否新开工</td>
                            <td class="TdContent" colspan="3">
                                <input type="text" action="like" datatype="varchar" id="IsNewStart" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TdContent" style="text-align: center" colspan="4">
                                <input style="width: 45px" type="button" class="searchbtn" id="btn_s" onclick="_searchData()" value="查询" /><input class="searchreset" type="button" style="width: 45px;" id="btn_r" value="重置" /></td>
                        </tr>
                    </table>
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
    cfg.tableName = "View_Ys_tz_Project";
    cfg.sortName = "StartDate,createdate";
    cfg.order = "desc,desc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "<%=sqlWhere%>";
    cfg.condition = "";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h - 50;
    cfg.parentfield = "";
    cfg.childfield = "";
    cfg.request = "ajax";
    cfg.Pagination = true;
    //是否关联查询
    cfg.relevanSearch = "true";

    cfg.render = function (value, rowsData, key) {
        if (key == "YSGuid") {
            if (value == "") {
                value = "否";
            }
            else  {
                value = "是";
            }
        }
        return "<a onclick='ttabs(\"" + rowsData.ProGuid + "\",\"" + rowsData.ProName + "\",\"" + rowsData.YSGuid + "\")' style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }

    //弹出查询框
    $(function () {
        $("#search").bind("click", function () {
            $("#win").css("top", "30px");
            $('#win').show(400);
        });
        $("#btn_r").bind("click", function () {
            $('#win').find(':input[type!="button"][type!="checkbox"]').val('');
        });
    })



    function jumpPage(eve, projGuid) {
        window.open("../JumpPage.aspx?ProjGuid=" + projGuid + "&type=" + eve);
    }

    cfg.columns = [
        { field: "ProGuid", width: "40px", checkbox: true },
		{ field: "ProName", name: "项目名称", align: "left", render: cfg.render },
        { field: "StartDeptName", name: "申报部门", width: "20%", align: "left", render: cfg.render },
        { field: "ContactName", name: "联系人", width: "20%", align: "left", render: cfg.render },
        { field: "Quota", name: "批复金额(万元)", width: "15%", align: "left", render: cfg.render },
        { field: "YSGuid", name: "是否验收", width: "20%", align: "left", render: cfg.render }
    ];

    function onBodyMouseDowns(event) {
        if (!(event.target.id == "win" || $(event.target).parents("#win").length > 0)) {
            $("#win").slideUp();
        }
        if (!(event.target.id == "ShowPad" || $(event.target).parents("#ShowPad").length > 0)) {
            $("#ShowPad").animate({
                width: '0px'
            });
            $("#ShowPad").hide();
        }
    }

    $(function () {
        $("#grid").TreeGrid(cfg);
        $("body").bind("mousedown", onBodyMouseDowns);
    });

    var config = new Config();
    //config.search();

    //查询
    function _searchData() {
        var sql = "";
        //项目名称
        if ($("#ProName").val() != "") {
            sql += " and ProName like '%" + $("#ProName").val() + "%'";
        }
        //申报部门
        if ($("#StartDeptGuid").val() != "") {
            sql += " and StartDeptGuid like '%" + $("#StartDeptGuid").val() + "%'";
        }
        //联系人
        if ($("#ContactName").val() != "") {
            sql += " and ContactName='" + $("#ContactName").val() + "'";
        }
        //投资额度
        if ($("#Quota").val() != "") {
            sql += " and Quota='" + $("#Quota").val() + "'";
        }
        //是否新开工
        if ($("#IsNewStart").val() != "") {
            sql += " and IsNewStart='" + $("#IsNewStart").val() + "'";
        }
        cfg.where = sql;
        $("#grid").TreeGrid(cfg);
        $('#win').hide();
        $("#sqlwhere").text(sql);
        //-------------------------------------------------------------------------------------------------------------------------------------
        //$.post("List.aspx", { type: "post", condtion: sql }, function (data) {
        //    $("#ShowPad").find("table").remove();
        //    $("#ShowPad").append(data.split('@')[1]);
        //    //getHightCharts(data.split('@')[0]);
        //})
    }

    function ttabs(pg, titile, YSGUID) {
       
        if ($('.easyui-tabs').tabs('exists', titile)) {
            $('.easyui-tabs').tabs('select', titile);
        } else {
            var url = '';
            if (YSGUID == "") {
                url = 'Create.aspx?xmguid=' + pg;
            }
            else {
                url = 'View.aspx?guid=' + YSGUID;
            }
            $('.easyui-tabs').tabs('add', {
                title: titile,
                content: '<iframe name="MyTabForm" src="' + url + '" frameborder="0" width="100%"  height="' + (h + 100) + '"></iframe>',
                closable: true
            });
        }
    }

    function tabsClose() {
        var tab = $('.easyui-tabs').tabs('getSelected');//获取当前选中tabs
        var index = $('.easyui-tabs').tabs('getTabIndex', tab);//获取当前选中tabs的index
        $('.easyui-tabs').tabs('close', index);//关闭对应index的tabs
    }



</script>