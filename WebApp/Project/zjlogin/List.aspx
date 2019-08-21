<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.Project.zjlogin.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目列表</title>
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/jquery-easyui/jquery.easyui.min.js"></script>
    <link href="../../Plugins/jquery-easyui/themes/default/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-tabs">
            <div title="项目信息">
                <div style="width: 100%; text-align: center">
                    <div class="FormMenu">
                        <div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：项目申报列表 </div>
                        <div class="MenuRight">
                            <a style="cursor: pointer" id="search">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查询</a>
                        </div>
                    </div>
                    <div id="grid" style="margin: 9px 0px 0px 12px"></div>
                </div>
                <div id="win" class="SearchWin">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="TdLabel">项目名称</td>
                            <td class="TdContent">
                                <input type="text" action="like" datatype="varchar" id="ProNameS" />
                            </td>
                            <td class="TdLabel">申报部门</td>
                            <td class="TdContent">
                                <input type="text" action="like" datatype="varchar" id="StartDeptNameS" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TdLabel">申报时间</td>
                            <td class="TdContent">
                                <input type="text" action="like" datatype="varchar" id="StartDate1" style="width: 40%;" readonly="readonly" class="Wdate" onfocus="WdatePicker()" />
                                ~
                        <input type="text" action="like" datatype="varchar" id="StartDate2" style="width: 40%;" readonly="readonly" class="Wdate" onfocus="WdatePicker()" />
                            </td>
                            <td class="TdLabel">投资额度区间</td>
                            <td class="TdContent">
                                <input type="text" action="like" datatype="float" id="Quota1" style="width: 40%;" />~
                        <input type="text" action="like" datatype="float" id="Quota2" style="width: 40%;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TdLabel">是否新开工</td>
                            <td class="TdContent" colspan="3">
                                <table id="IsNewStart" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input id="IsNewStart0" type="radio" name="IsNewStart" value="1" /><label for="IsNewStart0">是</label></td>
                                            <td>
                                                <input id="IsNewStart1" type="radio" name="IsNewStart" value="0" checked="checked" /><label for="IsNewStart1">否</label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TdContent" style="text-align: center" colspan="4">
                                <input style="width: 45px;" type="button" class="searchbtn" id="btn_s123" onclick="searchData()" value="查询" /><input class="searchreset" type="button" style="width: 45px" id="btn_r" value="重置" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="dialog" id="dialogreturn" style="height: 240px; width: 380px; display: none; position: fixed;">
            <img src="../../Images/cross.png" style="position:absolute;right:3px;top:1px;cursor:pointer" onclick="$('#dialogreturn').slideUp();" />
            <div class="head"><span class="title" style="color: red; padding-left: 20px; font-size: 18px">请填写退回原因</span></div>
            <textarea id="returninfo" rows="6" style="width:370px; height:160px;"></textarea>
            <div class="FormMenu">
                <div class="MenuRight">
                    <a style="cursor: pointer" href="javascript:returnPro();">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/recovery.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;退回申请</a>
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
    cfg.tableName = "tz_Project";
    cfg.sortName = "StateDate";
    cfg.order = "desc";
    cfg.pageCount = 14;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = " and sysstatus=0 and exists(select 1 from tz_ProjectAndExpert where tz_ProjectAndExpert.proguid=tz_Project.proguid and tz_ProjectAndExpert.ExpertGuid='<%=zjguid%>') ";
    cfg.condition = " ";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h - 33;
    cfg.request = "ajax";

    cfg.render = function (value, rowsData, key) {
        if (key == "IsNewStart") {
            if (value == 0) {
                value = "否";
            }
            else if (value == 1) {
                value = "是";
            }
        }
        return "<a onclick='ttabs(\"" + rowsData.ProGuid + "\",\"" + rowsData.ProName + "\")' style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }
    cfg.columns = [
        { field: "ProGuid", width: "40px", checkbox: true },
        { field: "ProName", name: "项目名称", width: "30%", align: "left", render: cfg.render, order: true, drag: true },
        { field: "StartDeptName", name: "申报部门", align: "left", render: cfg.render, drag: true },
        { field: "ContactName", name: "联系人", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "Quota", name: "投资额度", width: "15%", align: "left", render: cfg.render, drag: true },
        { field: "IsNewStart", name: "是否新开工", width: "15%", align: "left", render: cfg.render, drag: true },
        { field: "ProState", name: "状态", width: "10%", align: "center", render: cfg.render, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);

        //初始化查询的开始时间和结束时间
        var date = new Date();
        $("#StartDate1").val(date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
        $("#StartDate2").val(date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
    });

    function showDialog() {
        $('.dialog').css({ left: ($(window).width() - $(".dialog").width()) / 2, top: 50 }).slideDown();
    }

    var searchData = function () {
        var sqlwhere = " ";
        if ($("#ProNameS").val().length > 0) {
            sqlwhere += " and ProName like '%" + $("#ProNameS").val() + "%' ";
        }
        if ($("#StartDeptNameS").val().length > 0) {
            sqlwhere += " and StartDeptName like '%" + $("#StartDeptNameS").val() + "%' ";
        }
        if ($("#StartDate1").val().length > 0 && $("#StartDate2").val().length > 0) {
            sqlwhere += " and StartDate between '" + $("#StartDate1").val() + "' and '" + $("#StartDate2").val() + "' ";
        }
        if ($("#Quota1").val().length > 0 && $("#Quota2").val().length > 0) {
            sqlwhere += " and Quota between " + $("#Quota1").val() + " and " + $("#Quota2").val() + " ";
        }
        if ($("#IsNewStart0").get(0).checked == true) {
            sqlwhere += " and IsNewStart =1";
        }
        if ($("#IsNewStart1").get(0).checked == true) {
            sqlwhere += " and IsNewStart =0";
        }
        cfg.where = sqlwhere;
        $("#grid").Grid(cfg);
        $('#win').slideUp();
    }

    var sucessPro = function () {
        //获取当前proguid、userguid、username
        var proguid = $("#grid").GetSelection("single");
        var userguid = '<%=CurrentUser.UserGuid%>';
        var username = '<%=CurrentUser.UserCN%>';
        $.ajax({
            type: "post",
            url: "WebFunction.ashx?action=submit&proguid=" + proguid + "&userguid=" + userguid + "&username=" + username,
            cache: false,
            async: false,
            dataType: "json",
            success: function (obj) {
                if (obj.IsSucess) {

                }
                else {
                    alert(obj.ErrorInfo);
                }
                $("#grid").Grid(cfg);
            }
        });
    }
    var returnForm = function () {
        showDialog();
        return;
    }
    var returnPro = function () {
        //获取当前proguid、userguid、username
        var proguid = $("#grid").GetSelection("single");
        var userguid = '<%=CurrentUser.UserGuid%>';
        var username = '<%=CurrentUser.UserCN%>';
        var returninfo = encodeURI($("#returninfo").val());
        $.ajax({
            type: "post",
            url: "WebFunction.ashx?action=return&proguid=" + proguid + "&userguid=" + userguid + "&username=" + username,
            data: { returninfo: returninfo },
            cache: false,
            async: false,
            dataType: "json",
            success: function (obj) {
                $('#dialogreturn').slideUp();
                if (obj.IsSucess) {

                }
                else {
                    alert(obj.ErrorInfo);
                }
                $("#grid").Grid(cfg);
            }
        });
    }

    function ttabs(pg, titile) {
        if ($('.easyui-tabs').tabs('exists', titile)) {
            $('.easyui-tabs').tabs('select', titile);
        } else {

            $('.easyui-tabs').tabs('add', {
                title: titile,
                content: '<iframe src="../zjlogin/View.aspx?xmguid=' + pg + '&zjguid=<%=zjguid%>" frameborder="0" width="100%"  height="' + (h + 10) + '"></iframe>',
                closable: true
            });
        }
    }

    function deleteRow() {
        $("#grid").DetelteRow("<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx");
    }

    var config = new Config();
    config.search();
</script>
