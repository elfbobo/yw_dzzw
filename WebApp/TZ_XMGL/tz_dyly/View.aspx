<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_dyly.View" %>


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
                &nbsp;&nbsp;当前位置：项目单一来源信息</div>
		    <div class="MenuRight">
			         <a style="cursor:pointer" href="Create.aspx?guid=<%=document["guid"] %>&ProjGuid=<%=document["xmguid"] %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;编辑</a>
			         <a style="cursor:pointer" id="del"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>

                
                 </div>
        	</div>

            <table class="table" cellpadding="0" cellspacing="0">
			<tr>
				<td class='TdLabel'style="width:15%">单一来源理由</td>
				<td    class='TdContent'colspan="3">
                    <%=document["dylyly"]%>
				</td>
            </tr>
            <tr>
                <td class='TdLabel' style="width:15%">招标日期</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["zbrq"]%>
				</td>
				<td class='TdLabel' style="width:15%">中标单位信息</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["zbdwxx"]%>
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">中标金额(万元)</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["zbje"]%>
				</td>
				<td class='TdLabel' style="width:15%">专家意见</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["zjyj"]%>
				</td>
			</tr>
                
		</table>

             <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">投标书</span></legend>
                      <div id="filecontent0" style="margin-top:10px"></div>
            </fieldset>
     </div>
	<asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton"  style="display:none" />
	
	</form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';

    $(function () {

        var cfg0 = [];
        cfg0.refGuid = "<%=document["guid"]%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_zb_dyly";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

    });


    var config = new Config();
    config.deleteData();


</script>

