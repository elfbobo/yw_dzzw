<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.Project.zjlogin.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目申报-项目申报</title>
    <link href="../../Content/Form.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>
    <script src="../../Scripts/config.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                     当前位置：<%--<a href="ListZjps.aspx?xmguid=<%=xmguid %>&zjguid=<%=zjguid %>">评审列表</a> >>--%>专家评审-查看
                </div>
                <div class="MenuRight">
                    <a style="cursor: pointer" href="Create.aspx?guid=<%=strGuid %>&xmguid=<% =xmguid %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;编辑</a>
                    <a style="cursor: pointer" id="del" onclick="delData()">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;删除</a>
                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'>项目名称</td>
                    <td class='TdContent'>
                        <%=documentProject["ProName"] %>
                    </td>
                     <td class='TdLabel'>项目附件</td>
                    <td class='TdContent'>
                        <a href="javascript:;" onclick="showDialog()">查看</a>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>项目简介</td>
                    <td class='TdContent' colspan="3">
                        <%=documentProject["ProSummary"] %>
                    </td>

                </tr>
                <tr>
                    <td class='TdLabel'>投资额度</td>
                    <td class='TdContent'>
                        <%=documentProject["Quota"] %>
                    </td>
                    <td class='TdLabel'>资金来源</td>
                    <td class='TdContent'>
                        <%=documentProject["MoneySource"] %>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>评审意见</td>
                    <td class='TdContent' colspan="3">
                        <%=document["psyj"] %>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>评审时间</td>
                    <td class='TdContent' colspan="3">
                        <%=document["pssj"] %>
                    </td>
                </tr>
            </table>
            
        </div>
        <asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton" Style="display: none" />
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
</html>

<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var guid = '<%=strGuid%>';
    var table = 'tz_ProjectExpertPS';

    $(function () {
        var cfg0 = [];
        cfg0.refGuid = "<%=xmguid%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_Project";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);
    });

    var config = new Config();
    config.deleteData();

    function delData() {
        if (confirm("你确定要删除么？")) {
            $('#DelButton').click();
        }
    }
    function showDialog() {
        $('.dialog').css({ left: ($(window).width() - $(".dialog").width()) / 2, top: 20 }).slideDown();
    };
</script>
