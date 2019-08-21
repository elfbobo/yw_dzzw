<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListZjps.aspx.cs" Inherits="WebApp.Project.zjlogin.ListZjps" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>项目申报-专家评审</title>
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
                    当前位置：专家评审
                </div>
                <div class="MenuRight">
                    <a style="cursor: pointer" href="create.aspx?xmguid=<%=xmguid %>&zjguid=<%=zjguid %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/add.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;新建</a>
			&nbsp;<a style="cursor: pointer" onclick="deleteRow()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/delete_ico.gif" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;删除</a>
                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'>项目名称</td>
                    <td class='TdContent'>
                        <%=document["ProName"] %>
                    </td>
                     <td class='TdLabel'>项目附件</td>
                    <td class='TdContent'>
                        <a href="javascript:;" onclick="showDialog()">查看</a>
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
                    <td class='TdContent'>
                        <%=document["MoneySource"] %>
                    </td>
                </tr>
            </table>
            <div id="grid" style="margin: 9px 0px 0px 12px;"></div>
        </div>
        <div class="dialog" id="dialogreturn" style="height: 380px; width: 590px; display: none; position: fixed;">
            <img src="../../Images/cross.png" style="position: absolute; right: 3px; top: 1px; cursor: pointer" onclick="$('#dialogreturn').slideUp();" />
            <div class="head"><span class="title" style="color: red; padding-left: 20px; font-size: 18px">项目附件</span></div>
              <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">附件</span></legend>
                      <div id="filecontent0" style="margin-top:10px"></div>
            </fieldset>
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
        cfg.where = " and sysstatus=0 and expertguid='<%=zjguid%>'";
        cfg.condition = " ";
        cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
        cfg.width = "98%";
        cfg.height = h - 160;
        cfg.request = "ajax";

        cfg.render = function (value, rowsData, key) {
            return "<a style='cursor:pointer;' title='" + value + "' href='View.aspx?Guid=" + rowsData.Guid + "&xmguid=<%=xmguid%>&zjguid=<%=zjguid%>'>" + value + "</a>";
        }
        cfg.columns = [
            { field: "zjmc", name: "评审专家", width: "20%", align: "left", render: cfg.render, drag: true },
            { field: "psyj", name: "评审意见", align: "left", render: cfg.render, drag: true },
            { field: "pssj", name: "评审时间", width: "15%", align: "left", render: cfg.render, drag: true }
        ];

        $(function () {
            $("#grid").Grid(cfg);

            var cfg0 = [];
            cfg0.refGuid = "<%=strGuid%>";
            cfg0.type = "database";
            cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
            cfg0.fileSign = "tz_Project";
            cfg0.view = true;
            $("#filecontent0").fileviewload(cfg0);
        });
        function showDialog() {
            $('.dialog').css({ left: ($(window).width() - $(".dialog").width()) / 2, top: 20 }).slideDown();
        };
    </script>
</html>


