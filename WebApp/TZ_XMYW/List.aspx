<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.TZ_XMYW.List" %>

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
    <link href="../js/layui/css/layui.css" rel="stylesheet" />
    <script src="../js/layer/layer.js" type="text/javascript"></script>
     <script src="../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <style type="text/css">
        .tree-title {
            display: inline;
        }


    </style>
</head>
<body style="overflow: hidden">
    <form id="form1" runat="server">
        <div class="easyui-tabs">
            <div title="运维信息">
                <div style="width: 100%; text-align: center">
                    <div class="FormMenu">
                        <div class="MenuLeft">
                            &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：项目运维<label id="sqlwhere" style="display: none" hidden="hidden"></label>
                            <span>&emsp;&emsp;&emsp;按&nbsp;</span>
                            <span class="layui-breadcrumb">
                                <a ><img src="images/003.jpg" /></a>
                                <a href="XMList.aspx"><img src="images/002.jpg" /></a>
                            </span>
                            <span>&nbsp;查看</span>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            请选择：
                            <select onchange="yearSelect()" id="selectYear">
                                <%for (int i = StartYear; i <= CurYear;i++ ){
                                      if(i==CurYear){ %>
                                    <option selected value="<%=i %>"><%=i %></option>
                                <%}else{ %>
                                <option value="<%=i %>"><%=i %></option>
                                
                                <%} }%>
                            </select>
                        </div>
                        <div class="MenuRight">
                            <a style="cursor: pointer" href="create.aspx">
                                <img src="<%=Yawei.Common.AppSupport.AppPath%>/images/add.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;新建</a>
                            <%--       <a style="cursor: pointer" id="search">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查询</a>--%>
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

    <div id="ywfilesdiv">
        <div id="filecontent0" style="margin-top: 10px">

        </div>
    </div>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "View_tz_xmyw";
    cfg.sortName = "Year";
    cfg.order = "desc";
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

    function yearSelect() {
        cfg.where = "<%=sqlWhere%>" + " and Year=" + $("#selectYear").val();
        $("#grid").TreeGrid(cfg);
    }

    cfg.render = function (value, rowsData, key) {
        if (key == "psfiles") {
            value = "<a onclick=\"showReports('" + value + "')\" style='cursor:pointer'>查看</a>";
            return value;
        }
        return "<a onclick='ttabs(\"" + rowsData.Guid + "\",\"" + rowsData.name + "\",\"" + rowsData.Guid + "\")' style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }

    function showReports(psfiles) {
        $("#filecontent0").empty();
        var cfgPsView = [];
        cfgPsView.refGuid = psfiles;
        cfgPsView.type = "database";
        cfgPsView.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfgPsView.fileSign = "tz_xmyw";
       

        cfgPsView.view = true;
        cfgPsView.title = "运维报告"
        $("#filecontent0").fileviewload(cfgPsView);

        //自定页
        layer.open({
            type: 1,
            title: '运维报告',
            skin: 'layui-layer-demo', //样式类名
            closeBtn: 1, //不显示关闭按钮
            anim: 2,
            shadeClose: true, //开启遮罩关闭
            content: $('#ywfilesdiv')
        });
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
        { field: "Guid", width: "40px", checkbox: true },
		{ field: "name", name: "项目名称", align: "left", render: cfg.render },
        
        { field: "deptname", name: "申报部门", align: "left", render: cfg.render },
        { field: "StartDate", name: "申报时间", align: "left", render: cfg.render },
        { field: "Year", name: "运维年度", width: "20%", align: "left", render: cfg.render },
        { field: "psfiles", name: "运维报告", width: "20%", align: "left", render: cfg.render }

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
        cfg.where = "<%=sqlWhere%>" + " and Year=" + $("#selectYear").val();
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

            url = 'View.aspx?guid=' + pg;

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

<script src="../js/layui/layui.js" type="text/javascript"></script>
<script>
    layui.use('element', function () {
        var element = layui.element; //导航的hover效果、二级菜单等功能，需要依赖element模块
    });
</script>
