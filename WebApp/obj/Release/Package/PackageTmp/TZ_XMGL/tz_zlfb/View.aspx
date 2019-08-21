<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_zlfb.View" %>

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
                &nbsp;&nbsp;当前位置：年度自评信息</div>
		    <div class="MenuRight">
                <%if(roleCheck.isAdmin()||roleCheck.isBm()||CurrentUser.UserGroup.Guid==document["StartDeptGuid"]){ %>
			         <a style="cursor:pointer" href="Create.aspx?guid=<%=document["guid"] %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;编辑</a>
			         <a style="cursor:pointer" id="del"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>
                <%} %>
                     <a style="cursor: pointer" href="List.aspx">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>
                
                 </div>
        	</div>

            <table class="table" cellpadding="0" cellspacing="0">
			<tr>
				<td class='TdLabel'style="width:15%">标题</td>
				<td    class='TdContent'>
                    <%=document["biaoti"]%>
				</td>
                 <td class='TdLabel' style="width:15%">时间</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["fbsj"]%>
				</td>
            </tr>
<%--            <tr>
               
				<td class='TdLabel' style="width:15%">内容</td>
				<td    class='TdContent' style="width:35%" colspan="3">	
                    <%=document["neirong"]%>
				</td>
			</tr>--%>
                
		</table>

             <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">自评报告</span></legend>
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
        cfg0.fileSign = "zlfbfj";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

       
    });


    function deleteRow() {
        if (confirm("确认要删除么？") == false) {
            return;
        }
        //获取选中的内容，删除
        var selectRows = $("#grid").GetSelection("total");
        var ids = "";
        var rowCount = 0;
        rowCount = selectRows.length;
        $(selectRows).each(function () {
            ids += "'" + $(this)[0].guid + "',";
        });

        $.ajax({
            type: 'POST',
            url: "../XMGLHandler.ashx",
            data: { action: "deleteitem", ids: ids, tablename: "tz_zlfb" },
            success: function (e) {
                //console.log("sucess");
                if (e != rowCount) {
                    alert("选中" + rowCount + "条，成功删除" + e);
                } else {
                    alert("删除成功！");
                }
                //刷新当前页面
                $("#grid").Grid(cfg);
            }
        });

        //$("#grid").DetelteRow("<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx");
     }



    var config = new Config();
    config.deleteData();


</script>
