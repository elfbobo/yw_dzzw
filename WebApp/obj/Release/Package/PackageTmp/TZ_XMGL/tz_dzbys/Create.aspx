<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_dzbys.Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;当前位置：<a href="List.aspx?ProjGuid=<%=proGuid %>">电子政务中心验收信息</a> >> 编辑电子政务中心验收信息
                </div>
                <div class="MenuRight">
                   
                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>

                </div>
            </div>
            <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
                <tr>

                    <td class="TdLabel" style="width: 15%">验收时间</td>
                    <td class="TdContent" style="width: 35%">
                        <asp:TextBox runat="server" ID="yssj" ReadOnly="true" class="Wdate" empty="true" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                    <td class="TdLabel" style="width: 15%">验收内容</td>
                    <td class='TdContent' style="width: 35%">
                        <asp:TextBox runat="server" empty="true" ID="ysnr"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">软硬件验收内容</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="ryjysnr"></asp:TextBox>
                    </td>
                    <td class="TdLabel" style="width: 15%">云资源验收内容</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="yzyysnr"></asp:TextBox>
                    </td>
                </tr>
                
            </table>

        </div>
        
        <asp:Button runat="server" OnClick="Page_SaveData" ID="SaveButton" Style="display: none" />
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>


<script type="text/javascript">

    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var pguid = "<%=proGuid%>";




    var formCore = new FormCore();
    formCore.FormVildateLoad();

    var config = new Config();
    config.saveData();

</script>


