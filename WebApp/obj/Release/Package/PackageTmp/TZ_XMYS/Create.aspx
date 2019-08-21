<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMYS.Create" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../Plugins/datepicker/WdatePicker.js"></script>
    <script src="../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;当前位置：<a href="List.aspx">项目验收</a> >> 项目验收信息
                </div>
                <div class="MenuRight">

                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
                    <a style="cursor: pointer" href="List.aspx">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>

                </div>
            </div>
            <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="TdLabel" style="width: 15%">验收内容</td>
                    <td class="TdContent" colspan="3">
                        <asp:TextBox runat="server" empty="true" ID="YsContent"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="TdLabel" style="width: 15%">验收时间</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" ReadOnly="true" empty="true" ID="YsDate" class="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                    </td>

                    <td class="TdLabel" style="width: 15%">运维时间(年)</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" int="true" empty="true" ID="YwYear"></asp:TextBox>
                    </td>

                </tr>

                <tr>
                    <td class="TdLabel" style="width: 15%">验收结果</td>
                    <td class="TdContent" colspan="3">
                        <asp:TextBox runat="server" empty="true" ID="YsResult"></asp:TextBox>
                    </td>
                </tr>


            </table>
            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 150px; font-size: 13px; font-weight: bold">验收报告</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file1" style="width: 150px; font-size: 13px; font-weight: bold">共建共享情况</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>

<%--            <fieldset style="margin-top: 5px">
                <legend><span id="file2" style="width: 150px; font-size: 13px; font-weight: bold">资金使用说明</span></legend>
                <div id="filecontent2" style="margin-top: 10px"></div>
            </fieldset>--%>

        </div>
        <asp:HiddenField ID="CreateDate" runat="server" />
        <asp:Button runat="server" OnClick="Page_SaveData" ID="SaveButton" Style="display: none" />
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>


<script type="text/javascript">

    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var pguid = "<%=proGuid%>";

    $(function () {
        var cfg0 = [];
        cfg0.content = "filecontent0";
        cfg0.refGuid = "<%=ViewState["Guid"]%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "TZ_XMYS1";
        cfg0.title = "验收报告";

        $("#file0").uploadfile(cfg0);


        var cfg1 = [];
        cfg1.content = "filecontent1";
        cfg1.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "TZ_XMYS2";
        cfg1.title = "共建共享情况";
        $("#file1").uploadfile(cfg1);

        //var cfg2 = [];
        //cfg2.content = "filecontent2";
        //cfg2.refGuid = "";
        //cfg2.type = "database";
        //cfg2.applicationPath = "";
        //cfg2.fileSign = "TZ_XMYS3";
        //cfg2.title = "资金使用说明";
        //$("#file2").uploadfile(cfg2);
    });




    var formCore = new FormCore();
    formCore.FormVildateLoad();

    var config = new Config();
    config.saveData(function () {



        if ($("#filecontent0").children().length <= 1) {
            alert("请添加项目验收报告附件！");
            return false;
        }
        //if ($("#filecontent1").children().length <= 1) {
        //    alert("请添加共建共享情况附件！");
        //    return false;
        //}
        //if ($("#filecontent2").children().length <= 1) {
        //    alert("请添加资金使用说明附件！");
        //    return false;
        //}
        //var pfje = parseFloat($("#Quota").val());

        //alert("?????");
       


    });

</script>


<script src="<%=Yawei.Common.AppSupport.AppPath %>/js/filefix.js" type="text/javascript"></script>