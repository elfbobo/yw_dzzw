<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMList.aspx.cs" Inherits="WebApp.TZ_JXKH.XMList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目申报列表</title>
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Plugins/jquery.grid/jquery.progrid.js" type="text/javascript"></script>
    <script src="../Plugins/datepicker/WdatePicker.js"></script>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../Plugins/jquery-easyui/jquery.easyui.min.js"></script>
    <link href="../Plugins/jquery-easyui/themes/default/easyui.css" rel="stylesheet" />
    <script src="../js/layer/layer.js" type="text/javascript"></script>
    <link href="../js/layui/css/layui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-tabs">
            <div title="项目信息">
                <div style="width: 100%; text-align: center">
                    <div class="FormMenu">
                        <div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：绩效考核 


                        </div>
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
                            <td class="TdLabel">批复金额区间</td>
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
                                                <input id="IsNewStart0" type="radio" name="IsNewStart" value="1" checked="checked" /><label for="IsNewStart0">是</label></td>
                                            <td>
                                                <input id="IsNewStart1" type="radio" name="IsNewStart" value="0" /><label for="IsNewStart1">否</label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TdContent" style="text-align: center" colspan="4">
                                <input style="width: 45px;" type="button" class="searchbtn" id="btn_s123" onclick="searchData()" value="查询" /><input onclick="    searchReset()" type="button" style="width: 45px" value="重置" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
       
    </form>
     <div id="dialogreturn" style="display: none;">

            <table>
                <tr>
                    <td class="TdLabel">退回意见</td>
                    <td class="TdContent"><textarea></textarea></td>
                </tr>
                <tr>
                    <td colspan="2">

                    </td>
                </tr>
            </table>

            <textarea id="returninfo" rows="6" style="width: 98%; height: 160px;"></textarea>
            <div class="FormMenu" style="width: 100%">
                <div class="MenuRight">
                    <a style="cursor: pointer" href="javascript:returnPro();">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/recovery.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;退回申请</a>
                </div>
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
    cfg.tableName = "tz_project";
    cfg.sortName = "StartDate,createdate";
    cfg.order = "desc,desc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "<%=sqlWhere%>";
    cfg.condition = " ";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h - 33;
    cfg.request = "ajax";

    cfg.render = function (value, rowsData, key) {
        if (key == "ywcount") {
            if (value == "") {
                return "0";
            }
        }
        //return "<a href='View.aspx?xmguid=" + rowsData.ProGuid + "' onclick='ttabs(\"" + rowsData.ProGuid + "\",\"" + rowsData.ProName + "\")' style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
        return "<a href='List.aspx?xmguid=" + rowsData.ProGuid + "'  style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }
    cfg.columns = [
        { field: "ProGuid", width: "40px", checkbox: true },
        { field: "ProName", name: "项目名称", width: "20%", align: "left", render: cfg.render, order: true, drag: true },
        { field: "StartDeptName", name: "申报部门", align: "left", render: cfg.render, drag: true },
        { field: "StartDate", name: "申报时间", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "ProProperty", name: "项目属性", width: "15%", align: "left", render: cfg.render, drag: true }    
    ];

    $(function () {
        $("#grid").Grid(cfg);

        //初始化查询的开始时间和结束时间
        var date = new Date();
        $("#StartDate1").val(date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
        $("#StartDate2").val(date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
    });


    var searchData = function () {
        var sqlwhere = "<%=sqlWhere%>";

        //判断起始日期前后
        var startdate = $("#StartDate1").val();
        var enddate = $("#StartDate2").val();

        var starttime = new Date(startdate.replace(/\-/g, "\/"));
        var endtime = new Date(enddate.replace(/\-/g, "\/"));

        if (endtime < starttime) {
            alert("请注意日期查询前后顺序！");
            return false;
        }

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
        //alert(sqlwhere);
        $("#grid").Grid(cfg);
        $('#win').slideUp();
    }

    function searchReset() {
        $("#ProNameS").val('');
        $("#StartDeptNameS").val('');
        $("#StartDate1").val('');
        $("#StartDate2").val('');
        $("#Quota1").val('');
        $("#Quota2").val('');
        $("#IsNewStart0").attr("checked", true);
    }  

    var config = new Config();
    config.search();
</script>
<script src="../js/layui/layui.js" type="text/javascript"></script>
<script>
    layui.use('element', function () {
        var element = layui.element; //导航的hover效果、二级菜单等功能，需要依赖element模块
    });
</script>
