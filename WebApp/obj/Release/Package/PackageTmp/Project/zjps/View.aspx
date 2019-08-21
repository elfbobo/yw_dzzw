<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.Project.zjps.View" %>

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
                     当前位置：<a href="list.aspx?xmguid=<%=xmguid %>">评审列表</a> >>专家评审-查看
                </div>
                <div class="MenuRight">
      <%--              <a style="cursor: pointer" href="Create.aspx?guid=<%=strGuid %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;编辑</a>
                    <a style="cursor: pointer" id="del" onclick="delData()">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;删除</a>--%>
                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'>评审意见</td>
                    <td class='TdContent' colspan="3">
                        <%=document["psyj"] %>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>评审时间</td>
                    <td class='TdContent'>
                        <%=document["pssj"] %>
                    </td>
                </tr>
            </table>
            
        </div>
        <asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton" Style="display: none" />

    </form>
</body>
</html>

<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var guid = '<%=strGuid%>';
    var table = 'tz_ProjectExpertPS';

    $(function () {

     
    });

    var config = new Config();
    config.deleteData();

    function delData() {
        if (confirm("你确定要删除么？")) {
            $('#DelButton').click();
        }
    }
</script>

