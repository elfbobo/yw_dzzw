<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_zjys.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Content/Form.css" rel="stylesheet" />
	<script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
	<script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
	<script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.dynamicrow.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    	<div style="width:100%; text-align:center">
	    <div class="FormMenu">
		    <div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                &nbsp;&nbsp;当前位置：资金预算信息</div>
		    <div class="MenuRight">
			         <a style="cursor:pointer" href="Create.aspx?guid=<%=document["guid"] %>&ProjGuid=<%=document["xmguid"] %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;编辑</a>
			         <a style="cursor:pointer" id="del"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>

                
                 </div>
        	</div>

            <table class="table" cellpadding="0" cellspacing="0">
            <tr>
                <td class='TdLabel' style="width:15%">预算额(万元)</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["yse"]%>
				</td>
				<td class='TdLabel' style="width:15%"></td>
				<td    class='TdContent' style="width:35%">	
				</td>
			</tr>
                
		</table>
             <table style="width: 100%;line-height:35px;" class="table">
             <tr>
               
                <td style="width:30%;text-align:center"  class="TdLabel" >工程或费用名称</td>
              <td  style="width:20%;text-align:center" class="TdLabel">投资额(万元)</td>
              <td  style="width:40%;text-align:center" class="TdLabel">备注</td>  
     
             </tr>
             <tr id="rows" style="display:none" >         
             <td style="width:20%;text-align:center"  class="TdContent" node="fymc"><label></label></td>                
              <td style="width:15%;text-align:center" class="TdContent" node="tze"><label></label></td>
                 <td style="width:15%;text-align:center" class="TdContent" node="bz"><label></label></td>
                    
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

    var datarow = '<%=datarow%>';

    $("#rows").SetRowData({ data: datarow, type: "view" });

    var config = new Config();
    config.deleteData();


</script>
