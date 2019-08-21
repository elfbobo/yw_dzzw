<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.Project.zjps.Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <title></title>
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>

    <script type="text/javascript" charset="utf-8" src="../../Plugins/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../Plugins/ueditor/ueditor.all.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../Plugins/ueditor/lang/zh-cn/zh-cn.js"></script>
    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;<a href="list.aspx?xmguid=<%=xmguid %>">评审列表</a> 专家评审-新增
                </div>
                <div class="MenuRight">
                    <%if (CurrentUser.HasSave())
                      {%>
                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
                    <%} %>
                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'>评审意见</td>
                    <td class='TdContent'  colspan="3">
                        <div>
                            <asp:TextBox ID="psyj" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="ExpertGuid" runat="server" />
                            <asp:HiddenField ID="pssj" runat="server" />
                            <asp:HiddenField ID="ProGuid" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>

       
        <asp:HiddenField ID="CreateDate" runat="server" />
        <asp:HiddenField ID="EditDate" runat="server" />
        <asp:HiddenField ID="InfoType" runat="server" Value="Polices_Laws" />
        <asp:Button runat="server" ID="SaveButton" OnClick="Page_SaveData" Style="display: none" />
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    $(function () {

    });

    var formCore = new FormCore();
    formCore.FormVildateLoad();
    var config = new Config();
    config.saveData();
    $(function () {
        //文本编辑器
        //UE.getEditor('Content');

        //$("#save").on("click", function () {
        //    $("#SaveButton").click();
        //})
    })

</script>

