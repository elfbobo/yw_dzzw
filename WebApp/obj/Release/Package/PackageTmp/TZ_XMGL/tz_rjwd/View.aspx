<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_rjwd.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Content/Form.css" rel="stylesheet" />
	<script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
	<script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
	<script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    	<div style="width:100%; text-align:center">
	    <div class="FormMenu">
		    <div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                &nbsp;&nbsp;当前位置：软件文档</div>
		    <div class="MenuRight">
		<%--	         <a style="cursor:pointer" href="Create.aspx?guid=<%=document["guid"] %>&ProjGuid=<%=strProjGuid %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;编辑</a>--%>
			         <%--<a style="cursor:pointer" id="del"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>--%>

                
                 </div>
        	</div>

             <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目需求分析</span></legend>
                      <div id="filecontent0" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">项目设计</span></legend>
                      <div id="filecontent1" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">数据库说明</span></legend>
                      <div id="filecontent2" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">部署文档</span></legend>
                      <div id="filecontent3" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">测试文档</span></legend>
                      <div id="filecontent4" style="margin-top:10px"></div>
            </fieldset>
            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">运行情况</span></legend>
                      <div id="filecontent5" style="margin-top:10px"></div>
            </fieldset>
     </div>
<%--	<asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton"  style="display:none" />--%>
	
	</form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';

    $(function () {

        var cfg0 = [];
        cfg0.refGuid = "<%=strProjGuid%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "rjwdxmxqfx";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

        var cfg1 = [];
        cfg1.refGuid = "<%=strProjGuid%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "rjwdxmsj";
        cfg1.view = true;
        $("#filecontent1").fileviewload(cfg1);

        var cfg2 = [];
        cfg2.refGuid = "<%=strProjGuid%>";
        cfg2.type = "database";
        cfg2.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg2.fileSign = "rjwdsjksm";
        cfg2.view = true;
        $("#filecontent2").fileviewload(cfg2);

        var cfg3 = [];
        cfg3.refGuid = "<%=strProjGuid%>";
        cfg3.type = "database";
        cfg3.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg3.fileSign = "rjwdbswd";
        cfg3.view = true;
        $("#filecontent3").fileviewload(cfg3);

        var cfg4 = [];
        cfg4.refGuid = "<%=strProjGuid%>";
        cfg4.type = "database";
        cfg4.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg4.fileSign = "rjwdcswd";
        cfg4.view = true;
        $("#filecontent4").fileviewload(cfg4);

        var cfg5 = [];
        cfg5.refGuid = "<%=strProjGuid%>";
        cfg5.type = "database";
        cfg5.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg5.fileSign = "rjwdyxqk";
        cfg5.view = true;
        $("#filecontent5").fileviewload(cfg5);


    });


    //var config = new Config();
    //config.deleteData();


</script>
