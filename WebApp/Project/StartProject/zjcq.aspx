<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjcq.aspx.cs" Inherits="WebApp.Project.StartProject.zjcq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>项目申报-专家抽取</title>
    <link href="../../Content/Form.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
<%--    <script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/config.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
            <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                  当前位置：专家抽取
                </div>
                <div class="MenuRight">
                    <a style="cursor: pointer" id="SaveS">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'>项目名称</td>
                    <td class='TdContent' colspan="3">
                        <%=document["ProName"] %>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>项目简介</td>
                    <td class='TdContent' colspan="3">
                        <%=document["ProSummary"] %>
                    </td>
                   
                </tr>
               <tr>
                    <td class='TdLabel'>投资额度</td>
                    <td class='TdContent'>
                        <%=document["Quota"] %>
                    </td>
                    <td class='TdLabel'>资金来源</td>
                    <td class='TdContent' >
                         <%=document["MoneySource"] %>
                    </td>
                </tr>
            </table>
                <div style="padding:0;margin:0; " >
                    <div style="margin:5px;">评定评审专家数量：<input id="maxnum"/> &nbsp;&nbsp;&nbsp;<input type="button"  onclick="cqzj()"
                         value="抽取"/></div>
                    <div style="margin:5px;">抽取的序号为：<span id="xhs"></span></div>
                </div>
                <div>
                    <table class="table" id="wdzj" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="TdLabel" style="width: 20%; text-align: center">序号</td>
                                <td class="TdLabel" style="width: 20%; text-align: center">姓名</td>
                                <td class="TdLabel" style="width: 20%; text-align: center">联系人</td>
                                <td class="TdLabel" style="width: 20%; text-align: center">工作单位</td>
                                <td class="TdLabel" style="width: 20%; text-align: center">历史评审</td>
                            </tr>
                            <asp:Repeater runat="server" id="list_experts">
                                <ItemTemplate>
                                    <tr class='data'>
                                        <td class="TdContent" style="width: 20%; text-align: center"><%#Container.ItemIndex + 1 %><input type='hidden' class='zjguid' value='<%#Eval("Guid") %>' /></td>
                                        <td class="TdContent" style="width: 20%; text-align: center"><%#Eval("zjmc") %></td>
                                        <td class="TdContent" style="width: 20%; text-align: center"><%#Eval("lxdh") %></td>
                                        <td class="TdContent" style="width: 20%; text-align: center"><%#Eval("gzdw") %></td>
                                        <td class="TdContent" style="width: 20%; text-align: center"><a href='javascript:void(0);' onclick="showpsyj(this)">查看</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
            </div>
        </div>
        <div class="dialog" id="dialogreturn" style="height: 380px; width: 590px; display: none; position: fixed;">
            <img src="../../Images/cross.png" style="position: absolute; right: 3px; top: 1px; cursor: pointer" onclick="$('#dialogreturn').slideUp();" />
            <div class="head"><span class="title" style="color: red; padding-left: 20px; font-size: 18px">历史评审记录</span></div>
            <div id="grid" style="margin: 2px 0px 0px 2px;"></div>
        </div>
    </form>
</body>
    <script type="text/javascript">

        var cfg = [];
        cfg.connectionName = "";
        cfg.connectionString = "";
        cfg.providerName = "";
        cfg.tableName = "v_zjpsjl";
        cfg.sortName = "pssj";
        cfg.order = "desc";
        cfg.pageCount = 14;
        cfg.pageSelect = [5, 15, 20, 50];
        cfg.where = " and sysstatus=0 ";
        cfg.condition = " ";
        cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
        cfg.width = "98%";
        cfg.height = 330;
        cfg.request = "ajax";

        cfg.render = function (value, rowsData, key) {
            return value;
        }
        cfg.columns = [
            { field: "ProName", name: "评审项目", width: "30%", align: "left", render: cfg.render, order: true, drag: true },
            { field: "psyj", name: "评审意见", align: "left", render: cfg.render, drag: true }
        ];

        $(function () {
          

            $("#SaveS").click(function () {
                var guids = "";
                $("#wdzj").find("tbody tr.data input.zjguid").each(function (index, item) {
                    guids = guids + $(item).val() + "$";
                });
                var proguid = "<%=strGuid%>";
                $.ajax({
                    type: "post",
                    url: "WebFunction.ashx",
                    data: { action: "savezjs", guids: guids, proguid: proguid },
                    cache: false,
                    async: false,
                    dataType: "json",
                    success: function (obj) {
                        if (obj.IsSucess == 1) {
                            alert("保存成功！");
                            location.reload();
                        }
                    }
                });
            });
        });
        var cqzj = function () {
            var maxNum = $("#maxnum").val();
            $.ajax({
                type: "post",
                url: "WebFunction.ashx?action=cqzj&maxnum=" + maxNum ,
                data: { },
                cache: false,
                async: false,
                dataType: "json",
                success: function (obj) {
                    if (obj.IsSucess) {
                        var rows=JSON.parse(obj.Data);
                        //加载到table中
                        //清楚data行
                        $("#wdzj").find("tbody tr.data").remove();
                        var tbb = $("#wdzj").find("tbody");
                        var xhs = "";
                        $(rows).each(function (index, item) {
                            var rr = "<tr class='data'>";
                            rr += "<td class=\"TdContent\" style=\"width: 20%; text-align: center\">" + (index+1) + "<input type='hidden' class='zjguid' value='"+item.Guid+"' /></td>";
                            rr += "<td class=\"TdContent\" style=\"width: 20%; text-align: center\">" + item.zjmc + "</td>";
                            rr += "<td class=\"TdContent\" style=\"width: 20%; text-align: center\">" + item.lxdh + "</td>";
                            rr += "<td class=\"TdContent\" style=\"width: 20%; text-align: center\">" + item.gzdw + "</td>";
                            rr += "<td class=\"TdContent\" style=\"width: 20%; text-align: center\">" + "<a href='javascript:;' onclick='showpsyj(this)'>查看</a>" + "</td>";
                            rr+="</tr>";
                            $(tbb).append(rr);
                            xhs +=item.rkxh+",";
                        });
                        xhs = xhs.substring(0, xhs.length - 1);
                        $("#xhs").html(xhs);
                    }
                    else {
                        alert(obj.ErrorInfo);
                    }
                }
            });
        }
        var showpsyj = function (obj) {
            //获取选中那一行的zjguid
            var zjguid = $(obj).parent().parent().find("input.zjguid").val();
            cfg.where = " and sysstatus=0  and ExpertGuid='" + zjguid + "' ";
            $("#grid").Grid(cfg);
            showDialog();
        };
        function showDialog() {
            $('.dialog').css({ left: ($(window).width() - $(".dialog").width()) / 2, top: 20 }).slideDown();
        };
    </script>
</html>
