<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMJS.tz_dyly.Create" %>

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
                    &nbsp;&nbsp;当前位置：<a href="List.aspx?ProjGuid=<%=proGuid %>">项目招标</a> >> 项目单一来源信息
                </div>
                <div class="MenuRight">
                   
                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
                     <a style="cursor: pointer" href="List.aspx?xmguid=<%=proGuid %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>
                </div>
            </div>
            <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="TdLabel" style="width: 15%">单一来源理由</td>
                    <td class="TdContent" colspan="3">
                        <asp:TextBox runat="server" empty="true" ID="dylyly"></asp:TextBox>
                    </td>
                </tr>
                <tr>

                    <td class="TdLabel" style="width: 15%">招标日期</td>
                    <td class="TdContent" style="width: 35%">
                        <asp:TextBox runat="server" ID="zbrq" ReadOnly="true" class="Wdate"  onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                    <td class="TdLabel" style="width: 15%">中标单位</td>
                    <td class='TdContent' style="width: 35%">
                        <asp:TextBox runat="server"  ID="zbdwxx"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">合同金额(万元)</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="htje" double="true"></asp:TextBox>
                    </td>
                   <%-- <td class="TdLabel" style="width: 15%">专家意见</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="zjyj"></asp:TextBox>
                    </td>--%>
                </tr>
                
            </table>
             <fieldset style="margin-top: 5px">
                <legend><span id="file1" style="width: 150px; font-size: 13px; font-weight: bold">专家意见</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 150px; font-size: 13px; font-weight: bold">项目合同</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>

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
        cfg0.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_zb_dyly";
        cfg0.title = "项目合同";

        $("#file0").uploadfile(cfg0);

        var cfg1 = [];
        cfg1.content = "filecontent1";
        cfg1.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "tz_zb_dyly_zjyj";
        cfg1.title = "专家意见";

        $("#file1").uploadfile(cfg1);

    });




    var formCore = new FormCore();
    formCore.FormVildateLoad();

    var config = new Config();
    config.saveData(function () {

        if ($("#filecontent0").children().length <= 1) {
            alert("请添加项目合同附件！");
            return false;
        }
        //if ($("#filecontent1").children().length <= 1) {
        //    alert("请添加专家意见附件！");
        //    return false;
        //}

        var htjestr = $("#htje").val();
        if (htjestr.length > 10 || parseFloat(htjestr) == 0) {
            alert("请输入十位以下且大于0的纯数字！");
            return false;
        }
        //var pfje = parseFloat($("#Quota").val());

        //alert("?????");
        return true;


    });

</script>


<script src="<%=Yawei.Common.AppSupport.AppPath %>/js/filefix.js" type="text/javascript"></script>