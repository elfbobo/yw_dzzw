<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_rjwd.Create" %>

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
                    &nbsp;&nbsp;当前位置：<a href="List.aspx?ProjGuid=<%=proGuid %>">资金支付信息</a> >> 编辑软件文档
                </div>
                <div class="MenuRight">
                   
                    <%--<a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>--%>

                </div>
            </div>
            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 150px; font-size: 13px; font-weight: bold">项目需求分析</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file1" style="width: 150px; font-size: 13px; font-weight: bold">项目设计</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file2" style="width: 150px; font-size: 13px; font-weight: bold">数据库说明</span></legend>
                <div id="filecontent2" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file3" style="width: 150px; font-size: 13px; font-weight: bold">部署文档</span></legend>
                <div id="filecontent3" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file4" style="width: 150px; font-size: 13px; font-weight: bold">测试文档</span></legend>
                <div id="filecontent4" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file5" style="width: 150px; font-size: 13px; font-weight: bold">运行情况</span></legend>
                <div id="filecontent5" style="margin-top: 10px"></div>
            </fieldset>
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
        cfg0.refGuid = "<%=proGuid%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "rjwdxmxqfx";
        cfg0.title = "项目需求分析";

        $("#file0").uploadfile(cfg0);


        var cfg1 = [];
        cfg1.content = "filecontent1";
        cfg1.refGuid = "<%=proGuid%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "rjwdxmsj";
        cfg1.title = "项目设计";

        $("#file1").uploadfile(cfg1);


        var cfg2 = [];
        cfg2.content = "filecontent2";
        cfg2.refGuid = "<%=proGuid%>";
        cfg2.type = "database";
        cfg2.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg2.fileSign = "rjwdsjksm";
        cfg2.title = "数据库说明";

        $("#file2").uploadfile(cfg2);

        var cfg3 = [];
        cfg3.content = "filecontent3";
        cfg3.refGuid = "<%=proGuid%>";
        cfg3.type = "database";
        cfg3.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg3.fileSign = "rjwdbswd";
        cfg3.title = "部署文档";

        $("#file3").uploadfile(cfg3);

        var cfg4 = [];
        cfg4.content = "filecontent4";
        cfg4.refGuid = "<%=proGuid%>";
        cfg4.type = "database";
        cfg4.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg4.fileSign = "rjwdcswd";
        cfg4.title = "测试文档";

        $("#file4").uploadfile(cfg4);

        var cfg5 = [];
        cfg5.content = "filecontent5";
        cfg5.refGuid = "<%=proGuid%>";
        cfg5.type = "database";
        cfg5.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg5.fileSign = "rjwdyxqk";
        cfg5.title = "运行情况";

        $("#file5").uploadfile(cfg5);

        

    });




    var formCore = new FormCore();
    formCore.FormVildateLoad();

    var config = new Config();
    config.saveData();

</script>