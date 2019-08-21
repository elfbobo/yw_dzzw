<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_wdgl.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Content/Form.css" rel="stylesheet" />
	<script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
	<script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
	<script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery-uploadify/uploadify.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%if (dt != null & dt.Rows.Count > 0) {%>
        <%for (int i=0;i<dt.Rows.Count-1;i++) {%>
    <div style="width: 99%; position: relative; border: 1px solid lightgray; margin-top: 30px; padding: 5px">
                    <div class="divpanel" style="cursor: default">项目名称：<%=dt.Rows[i]["ProName"].ToString() %></div>
                    <div>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">单一来源投标书</span></legend>
                      <div id="filecontent1<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                         <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">公开招标文件</span></legend>
                      <div id="filecontent2<%=i %>" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">公开中标文件</span></legend>
                      <div id="filecontent3<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">竞争性磋商招标文件</span></legend>
                      <div id="filecontent4<%=i %>" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">竞争性磋商中标文件</span></legend>
                      <div id="filecontent5<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目需求分析</span></legend>
                      <div id="filecontent6<%=i %>" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目设计</span></legend>
                      <div id="filecontent7<%=i %>" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">数据库说明</span></legend>
                      <div id="filecontent8<%=i %>" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">部署文档</span></legend>
                      <div id="filecontent9<%=i %>" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">测试文档</span></legend>
                      <div id="filecontent10<%=i %>" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">运行情况</span></legend>
                      <div id="filecontent11<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目询价中标文件</span></legend>
                      <div id="filecontent12<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目合同信息</span></legend>
                      <div id="filecontent13<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目立项批复文件</span></legend>
                      <div id="filecontent14<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                         <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目验收报告</span></legend>
                      <div id="filecontent15<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">直接采购文件</span></legend>
                      <div id="filecontent16<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">资金到位附件</span></legend>
                      <div id="filecontent17<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">资金支付凭证</span></legend>
                      <div id="filecontent18<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                        <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">资金支付附件</span></legend>
                      <div id="filecontent19<%=i %>" style="margin-top:10px"></div>
            </fieldset>
                    </div>
        </div>
        <%} %>
        <%} %>
    </div>
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';

    $(function () {
        <%for (int j = 0; j <= dt.Rows.Count - 1; j++){%>

        var cfg1 = [];
        cfg1.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "dylytbs";
        cfg1.view = true;
        $("#filecontent1<%=j %>").fileviewload(cfg1);

        var cfg2 = [];
        cfg2.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg2.type = "database";
        cfg2.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg2.fileSign = "zhaobiaowj";
        cfg2.view = true;
        $("#filecontent2<%=j %>" ).fileviewload(cfg2);

        var cfg3 = [];
        cfg3.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
         cfg3.type = "database";
         cfg3.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg3.fileSign = "zhaobiaowj";
        cfg3.view = true;
        $("#filecontent3<%=j %>").fileviewload(cfg3);

        var cfg4 = [];
        cfg4.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg4.type = "database";
        cfg4.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg4.fileSign = "jzxcszhaobiaowj";
        cfg4.view = true;
        $("#filecontent4<%=j %>").fileviewload(cfg4);

        var cfg5 = [];
        cfg5.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg5.type = "database";
        cfg5.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
         cfg5.fileSign = "jzxcszhaobiaowj";
         cfg5.view = true;
         $("#filecontent5<%=j %>").fileviewload(cfg5);

        var cfg6 = [];
        cfg6.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg6.type = "database";
        cfg6.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg6.fileSign = "rjwdxmxqfx";
        cfg6.view = true;
        $("#filecontent6<%=j %>").fileviewload(cfg6);

        var cfg7 = [];
        cfg7.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg7.type = "database";
        cfg7.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg7.fileSign = "rjwdxmsj";
        cfg7.view = true;
        $("#filecontent7<%=j %>").fileviewload(cfg7);

        var cfg8 = [];
        cfg8.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg8.type = "database";
        cfg8.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg8.fileSign = "rjwdsjksm";
        cfg8.view = true;
        $("#filecontent8<%=j %>").fileviewload(cfg8);

        var cfg9 = [];
        cfg9.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg9.type = "database";
        cfg9.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg9.fileSign = "rjwdbswd";
        cfg9.view = true;
        $("#filecontent9<%=j %>").fileviewload(cfg9);

        var cfg10 = [];
        cfg10.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg10.type = "database";
        cfg10.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg10.fileSign = "rjwdcswd";
        cfg10.view = true;
        $("#filecontent10<%=j %>").fileviewload(cfg10);

        var cfg11 = [];
        cfg11.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg11.type = "database";
        cfg11.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg11.fileSign = "rjwdyxqk";
        cfg11.view = true;
        $("#filecontent11<%=j %>").fileviewload(cfg11);

        var cfg12 = [];
        cfg12.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg12.type = "database";
        cfg12.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg12.fileSign = "xjzhongbiaowj";
        cfg12.view = true;
        $("#filecontent12<%=j %>").fileviewload(cfg12);

        var cfg13 = [];
        cfg13.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
         cfg13.type = "database";
         cfg13.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg13.fileSign = "tz_xmht";
        cfg13.view = true;
        $("#filecontent13<%=j %>").fileviewload(cfg13);

        var cfg14 = [];
        cfg14.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg14.type = "database";
        cfg14.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg14.fileSign = "tz_xmlx";
         cfg14.view = true;
         $("#filecontent14<%=j %>").fileviewload(cfg14);

        var cfg15 = [];
        cfg15.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg15.type = "database";
        cfg15.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg15.fileSign = "xmysysbg";
        cfg15.view = true;
        $("#filecontent15<%=j %>").fileviewload(cfg15);

        var cfg16 = [];
        cfg16.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
         cfg16.type = "database";
         cfg16.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg16.fileSign = "zjcgcgwj";
        cfg16.view = true;
        $("#filecontent16<%=j %>").fileviewload(cfg16);

        var cfg17 = [];
        cfg17.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg17.type = "database";
        cfg17.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg17.fileSign = "zjdwfj";
         cfg17.view = true;
         $("#filecontent17<%=j %>").fileviewload(cfg17);

        var cfg18 = [];
        cfg18.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
         cfg18.type = "database";
         cfg18.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg18.fileSign = "zjzfzfpz";
         cfg18.view = true;
         $("#filecontent18<%=j %>").fileviewload(cfg18);

        var cfg19 = [];
        cfg19.refGuid = "<%=dt.Rows[j]["ProGuid"]%>";
        cfg19.type = "database";
        cfg19.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg19.fileSign = "zjzffj";
        cfg19.view = true;
        $("#filecontent19<%=j %>").fileviewload(cfg19);
        
        <%}%>
    });


    var config = new Config();
    config.deleteData();


</script>
