<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_xmsb.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Content/Form.css" rel="stylesheet" />
	<script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
	<script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    	<div style="width:100%; text-align:center">
	    <div class="FormMenu">
		    <div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                &nbsp;&nbsp;当前位置：项目申报信息</div>
		    <div class="MenuRight">
                 </div>
        	</div>

            <table class="table" cellpadding="0" cellspacing="0">
			<tr>
				<td class='TdLabel'style="width:15%">项目名称</td>
				<td    class='TdContent'colspan="3">
                    <%=document["ProName"]%>
				</td>
            </tr>
            <tr>
                <td class='TdLabel' style="width:15%">项目简介</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["ProSummary"]%>
				</td>
				<td class='TdLabel' style="width:15%">批复金额(万元)</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["Quota"]%>
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">资金来源</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["MoneySource"]%>
				</td>
				<td class='TdLabel' style="width:15%">是否新开工项目</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["IsNewStart"]%>
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">申报部门</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["StartDeptName"]%>
				</td>

                    <td class='TdLabel' style="width:15%">项目类型</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["ProType"]%>
				</td>


				
			</tr>
                <tr>

                    <td class='TdLabel' style="width:15%">项目联系人</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["ContactName"]%>
				</td>

                <td class='TdLabel' style="width:15%">联系人电话</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["ContactTel"]%>
				</td>

			</tr>

                <tr>
                 <td class='TdLabel' style="width:15%">申报时间</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["StartDate"]%>
				</td>

                <td class='TdLabel' style="width:15%">是否部署云平台</td>
				<td    class='TdContent' style="width:35%">	
                    <%=getIsCloud(document["IsInCloudPlat"])%>
				</td>
			</tr>
		</table>

             <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">附件</span></legend>
                      <div id="filecontent0" style="margin-top:10px"></div>
            </fieldset>
     </div>
	<%--<asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton"  style="display:none" />--%>
	
	</form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';

    $(function () {

        var cfg0 = [];
        cfg0.refGuid = "<%=document["ProGuid"]%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_Project";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

    });


    var config = new Config();
    config.deleteData();


</script>

