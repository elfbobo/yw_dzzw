<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_zjzf.Create" %>

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
                    &nbsp;&nbsp;当前位置：<a href="List.aspx?ProjGuid=<%=proGuid %>">资金支付信息</a> >> 编辑资金支付信息
                </div>
                <div class="MenuRight">
                   
                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>

                </div>
            </div>
            <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
                <tr>

                    <td class="TdLabel" style="width: 15%">合同名称</td>
                    <td class="TdContent" colspan="3">
                        <asp:TextBox runat="server" empty="true" ID="htmc" style="width:88%" ReadOnly></asp:TextBox>
                        <asp:TextBox runat="server" ID="htguid" Style="display: none;"></asp:TextBox>
                        <input style="width: 10%; margin-left: 5px" onclick="showhtinfo()" class="fileBtn" type="button" value="选择" />
                    </td>

                </tr>
                <tr>

                    <td class="TdLabel" style="width: 15%">支付金额(万元)</td>
                    <td class='TdContent' style="width: 35%">
                        <asp:TextBox runat="server" empty="true" ID="zfje" double="true"></asp:TextBox>

                    </td>
                     <td class="TdLabel" style="width: 15%">支付类型</td>
                    <td class="TdContent" style="width: 35%">
                        <%--<asp:TextBox runat="server" empty="true" ID="zflx"></asp:TextBox>--%>
                        <asp:DropDownList empty="true" runat="server" ID="zflx" style="min-width:250px;min-height:30px">
                            <asp:Listitem value="全款">全款</asp:Listitem>
                            <asp:Listitem value="首款">首款</asp:Listitem>
                            <asp:Listitem value="中间款">中间款</asp:Listitem>
                            <asp:Listitem value="尾款">尾款</asp:Listitem>
                        </asp:DropDownList>

                    </td>


                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">支付时间</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" ID="zfsj" ReadOnly="true" class="Wdate" empty="true" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                     <td class="TdLabel" style="width: 15%">收款单位</td>
                    <td class='TdContent' style="width: 35%">
                        <asp:TextBox runat="server" empty="true" ID="skdw"></asp:TextBox>

                    </td>
                </tr>
                
            </table>
            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 150px; font-size: 13px; font-weight: bold">支付凭证</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file1" style="width: 150px; font-size: 13px; font-weight: bold">附件</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>

        </div>
        <div id="show" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%; opacity: 0.15; z-index: 10000; background: rgb(0, 0, 0); display: none; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=15);"></div>
                    <div class="dialog" id="xzht" style="height: 430px; width: 800px; display: none; position: fixed; z-index: 10001">
                        <div class="head">
                            <span class="title">选择合同</span>
                        </div>
                        <img src="../../Images/cross.png" style="position: absolute; right: 3px; top: 10px; cursor: pointer" onclick="htHide()" />
                        <iframe id="ifmContent" frameborder="0" style="height: 380px; width: 800px"></iframe>
                    </div>
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
        cfg0.refGuid = "<%=strGuid%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_zjzf_zfpz";
        cfg0.title = "支付凭证";

        $("#file0").uploadfile(cfg0);


        var cfg1 = [];
        cfg1.content = "filecontent1";
        cfg1.refGuid = "<%=strGuid%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "tz_zjzf_fj";
        cfg1.title = "附件";

        $("#file1").uploadfile(cfg1);
    });




    var formCore = new FormCore();
    formCore.FormVildateLoad();

    var config = new Config();
    config.saveData();

    function htHide() {
        $("#show").hide();
        $('#xzht').slideUp();
    }

    function showhtinfo() {

        $("#ifmContent").attr("src", "SelectHt.aspx?proGuid=" + pguid);
        $("#show").show();
        $("#xzht").css({ left: ($(window).width() - $(".dialog").width()) / 2, width: 800, top: 30 }).slideDown();
    }

</script>
