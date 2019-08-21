<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.Project.zjps.List" %>

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
    <script src="../../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Scripts/config.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.dynamicrow.js"></script>
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
                   <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
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
                    <td class='TdLabel'>批复金额</td>
                    <td class='TdContent'>
                        <%=document["Quota"] %>
                    </td>
                    <td class='TdLabel'>资金来源</td>
                    <td class='TdContent'>
                        <%=document["MoneySource"] %>
                    </td>
                </tr>
              
            </table>
            <fieldset style="margin-top: 5px">
                <legend><span id="file1" style="width: 60px; height: 20px; display: block"></span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">填写评审意见</span></legend>
                <table id="tabledy" style="width: 100%; margin-top: 5px; margin-bottom: 5px" class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class='TdLabel' style="width: 30%; text-align: center">专家</td>
                        <td class='TdLabel' style="width: 60%; text-align: center">评审意见</td>
                        <td class='TdLabel' style="width: 10%">
                            <input id="addr" style="width: 50px;" class="fileBtn" value="添加" type="button" />
                        </td>
                    </tr>
                    <tr id="IssueRow" style="display: none">
                        <td node="ExpertGuid" class='TdContent' style="width: 30%; text-align: left">
                           <%-- <asp:TextBox runat="server" ID="ProGuid" ></asp:TextBox>--%>
                            <asp:DropDownList runat="server" ID="ProGuid"></asp:DropDownList>
                        </td>
                        <td node="psyj" class='TdContent' style="width: 60%; text-align: left">
                            <asp:TextBox runat="server" ID="psyj"></asp:TextBox>
                        </td>
                        <td class='TdContent' style="text-align: center; width: 10%">
                            <input style="width: 50px;" class="fileBtn" value="删除" type="button" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="grid" style="margin: 9px 0px 0px 12px; display:none;" ></div>
        </div>
          <asp:Button runat="server" OnClick="Page_SaveData" ID="SaveButton" Style="display: none" />
        <div class="dialog" id="dialogreturn" style="height: 380px; width: 590px; display: none; position: fixed;">
            <img src="../../Images/cross.png" style="position: absolute; right: 3px; top: 1px; cursor: pointer" onclick="$('#dialogreturn').slideUp();" />
            <div class="head"><span class="title" style="color: red; padding-left: 20px; font-size: 18px">项目附件</span></div>
              <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目附件</span></legend>
                      <div id="filecontent0" style="margin-top:10px"></div>
            </fieldset>
        </div>
    </form>
</body>
    <script type="text/javascript">
        var rowsData = '<%=strIssueRect%>';

        var formCore = new FormCore();
        formCore.FormVildateLoad();

        $("#IssueRow").SetRowData({ data: rowsData, onRowDeletedCompleted: function () { } });

        var config = new Config();
        config.saveData(function () {
            $("#IssueRow").getRowData({ hideInputId: 'IssueRowData', enCode: true });
        });

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
        cfg.height = h-160;
        cfg.request = "ajax";

        cfg.render = function (value, rowsData, key) {
            return "<a style='cursor:pointer;' title='" + value + "' href='View.aspx?Guid=" + rowsData.Guid + "&xmguid=<%=xmguid%>'>" + value + "</a>";
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

            var cfg1 = [];
            cfg1.refGuid = "<%=document["PsFiles"]%>";
            cfg1.content = "filecontent1";
            cfg1.title = "评审附件";
            cfg1.type = "database";
            cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
            cfg1.fileSign = "tz_ProjectPs";
            $("#file1").uploadfile(cfg1);

            $('#addr').click(function () {
                $('#IssueRow').dynamicRow({
                    onRowDeletedCompleted: function () { optdynamicRowNum(); }
                });
                optdynamicRowNum();
            });
        });
        function showDialog() {
            $('.dialog').css({ left: ($(window).width() - $(".dialog").width()) / 2, top: 20 }).slideDown();
        };
    </script>
</html>

