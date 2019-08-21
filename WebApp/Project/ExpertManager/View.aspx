<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.Project.ExpertManager.View" %>

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
                &nbsp;&nbsp;当前位置：<a href="List.aspx">列表</a>>>专家信息</div>
		    <div class="MenuRight">
			         <a style="cursor:pointer" href="Create.aspx?guid=<%=document["Guid"] %>&ProjGuid=<%=strProjGuid %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;编辑</a>
			         <a style="cursor:pointer" id="del"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>
                 </div>
        	</div>

            <table class="table" cellpadding="0" cellspacing="0">
            <tr>
                <td class='TdLabel' style="width:15%">姓名</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["zjmc"]%>
				</td>
				<td class='TdLabel' style="width:15%"></td>
				<td    class='TdContent' style="width:35%">	
                  
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">联系电话</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["lxdh"]%>
				</td>
				<td class='TdLabel' style="width:15%">工作单位</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["gzdw"]%>
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">职称</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["zjzc"]%>
				</td>
				<td class='TdLabel' style="width:15%"></td>
				<td    class='TdContent' style="width:35%">	
				</td>
			</tr>
		</table>

     </div>
	<asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton"  style="display:none" />
	
	</form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';



    var config = new Config();
    config.deleteData();


</script>



