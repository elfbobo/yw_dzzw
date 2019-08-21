<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.XM_ZFTZ.Project.StartProject.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目申报列表</title>
    <script src="../../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../../Plugins/jquery.grid/jquery.progrid.js" type="text/javascript"></script>
    <script src="../../../Plugins/datepicker/WdatePicker.js"></script>
    <link href="../../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../../Plugins/jquery-easyui/jquery.easyui.min.js"></script>
    <link href="../../../Plugins/jquery-easyui/themes/default/easyui.css" rel="stylesheet" />
    <script src="../../../js/layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-tabs">
            <div title="项目信息">
                <div style="width: 100%; text-align: center">
                    <div class="FormMenu">
                        <div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                            &nbsp;&nbsp;当前位置：项目申报列表 
                            &nbsp;&nbsp;&nbsp;&nbsp;按状态查看：
                            <select onchange="changestatus(this)">
                                <option value="全部" selected>全部</option>
                                <option value="未提交">未提交</option>
                                <option value="提交">待审核</option>
                                <option value="退回">退回</option>
                                <option value="暂缓">暂缓</option>
                                <option value="整合">整合</option>
                                <option value="申报">通过</option>
                            </select>
                        </div>
                        <div class="MenuRight">
                            <a style="cursor: pointer" id="search">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查询</a>
                            <%--政府投资项目 只有管理员和政府办和发改委管理员才有通过或者退回的权限--%>
                            <%if (roleCheck.isAdmin() || roleCheck.isZfb()||roleCheck.isFgwAdmin())
                              { %>
                           <%-- <a style="cursor: pointer" href="javascript:sucessPro();">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_333.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;通过</a>
                            <a style="cursor: pointer" href="javascript:returnForm();">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/recovery.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;退回申请</a>--%>
                            <%} %>

                            <%--只有部门用户可以新建和提交 --%>
                            <%if(roleCheck.isBm()){ %>
                            <a style="cursor: pointer" href="create.aspx">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/add.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;新建</a>
                            <a style="cursor: pointer" id="commit" href="javascript:commitPro();">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cup_go.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;提交</a>
                            <% }%>

                            <%--管理员和部门有删除权限，部门只能删除自己未提交或者被退回的项目 --%>
                            <%if(roleCheck.isAdmin()||roleCheck.isBm()||roleCheck.isFgwAdmin()){%>
                            &nbsp;<a style="cursor: pointer" onclick="deleteRow()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/delete_ico.gif" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;删除</a>
                            <%} %>
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
<%--                        <tr>
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
                        </tr>--%>
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
    cfg.tableName = "tz_zftz_Project";
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
        //if (key == "IsNewStart") {
        //    if (value == 0) {
        //        value = "否";
        //    }
        //    else if (value == 1) {
        //        value = "是";
        //    }
        //}
        if (key == "ProState") {
            if (value == null || value == "") {
                value = "未提交";
            }
            else if (value == "提交") {
                value = "待审核";
            }
            else if (value == "申报") {
                value = "通过";
            }
            
        }
        //return "<a href='View.aspx?xmguid=" + rowsData.ProGuid + "' onclick='ttabs(\"" + rowsData.ProGuid + "\",\"" + rowsData.ProName + "\")' style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
        return "<a href='View.aspx?xmguid="+rowsData.ProGuid+"'  style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }
    cfg.columns = [
        { field: "ProGuid", width: "40px", checkbox: true },
        { field: "ProName", name: "项目名称", width: "20%", align: "left", render: cfg.render, order: true, drag: true },
        { field: "StartDeptName", name: "申报部门", align: "left", render: cfg.render, drag: true },
        { field: "MoneySource", name: "资金来源", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "Quota", name: "预估金额(万元)", width: "15%", align: "left", render: cfg.render, drag: true },
        { field: "ProProperty", name: "项目属性", width: "15%", align: "left", render: cfg.render, drag: true },
        { field: "ProState", name: "状态", width: "10%", align: "center", render: cfg.render, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);

        //初始化查询的开始时间和结束时间
        //var date = new Date();
        //$("#StartDate1").val(date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
        //$("#StartDate2").val(date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());
    });


    function changestatus(obj) {
        var status=$(obj).val();
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
        

        if (status == "全部") {

        }
        else if (status == "未提交") {
            sqlwhere += " and ProState is NULL";
        }
        else  {
            sqlwhere += " and ProState='" + status + "'";
        }
        cfg.where = sqlwhere;

        $("#grid").Grid(cfg);
        $('#win').slideUp();
    }

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
        cfg.where = sqlwhere;

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


    //提交
    var commitPro = function () {

        var userguid = '<%=CurrentUser.UserGuid%>';
        var bmguid = '<%=CurrentUser.UserGroup.Guid%>';

        var selectRows = $("#grid").GetSelection("total");
        var ids = "";
        var rowCount = 0;
        rowCount = selectRows.length;

        if (rowCount == 0) {
            alert("请选择要提交的项目");
            return;
        }

        $(selectRows).each(function () {
            ids += "'" + $(this)[0].ProGuid + "',";
        });

        $.ajax({
            type: "post",
            url: "ZftzWebFunction.ashx",
            data: { action: "commit", proguid: ids, userguid: userguid, bmguid: bmguid },
            cache: false,
            async: false,
            dataType: "json",
            success: function (obj) {
                $("#grid").Grid(cfg);
                alert("成功提交" + obj.Data + "条项目申请");
            }
        });
    }



    function deleteRow() {
        var userguid = '<%=CurrentUser.UserGuid%>';
        var username = '<%=CurrentUser.UserCN%>';

        //获取选中的内容，删除
        var selectRows = $("#grid").GetSelection("total");
        var ids = "";
        var rowCount = 0;
        rowCount = selectRows.length;
        if (rowCount == 0) {
            alert("请选择要删除的项目！");
            return;
        }
        $(selectRows).each(function () {
            <%--管理员可以删除已提交或者审核通过的项目或者退回的项目--%>
            <%if(roleCheck.isAdmin()||roleCheck.isFgwAdmin()){%>
            if ($(this)[0].ProState == "提交" || $(this)[0].ProState == "退回" || $(this)[0].ProState=="申报") {
                    ids += "'" + $(this)[0].ProGuid + "',";
            }
            <%}else if(roleCheck.isBm()){%>
            //只有未提交的和退回的部门才可以删除
            if ($(this)[0].ProState == "" || $(this)[0].ProState == "退回") {
                ids += "'" + $(this)[0].ProGuid + "',";
            }
            <%}%>
        });

        if (ids == "") {
            alert("没有可以删除的项目");
            return;
        }

        $.ajax({
            type: "post",

            url: "ZftzWebFunction.ashx",
            data: { action: "delete", proguid: ids, userguid: userguid, username: username },
            cache: false,
            async: false,
            dataType: "json",
            success: function (obj) {

                alert("共删除" + obj.Data + "条项目申请");
                $("#grid").Grid(cfg);
            }
        });



        // $("#grid").DetelteRow("<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx");
    }

    var config = new Config();
    config.search();
</script>
