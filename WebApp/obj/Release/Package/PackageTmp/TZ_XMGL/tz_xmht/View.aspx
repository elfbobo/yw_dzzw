<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_xmht.View" %>

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
                &nbsp;&nbsp;当前位置：项目合同信息</div>
		    <div class="MenuRight">
			         <a style="cursor:pointer" href="Create.aspx?guid=<%=document["guid"] %>&ProjGuid=<%=document["xmguid"] %>" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;编辑</a>
			         <a style="cursor:pointer" id="del"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>
                 </div>
        	</div>

            <table class="table" cellpadding="0" cellspacing="0">
            <tr>
                <td class='TdLabel' style="width:15%">合同名称</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["htmc"]%>
				</td>
				<td class='TdLabel' style="width:15%">甲方</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["jf"]%>
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">乙方</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["yf"]%>
				</td>
				<td class='TdLabel' style="width:15%">合同内容</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["htnr"]%>
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">签订信息</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["qdxx"]%>
				</td>
				<td class='TdLabel' style="width:15%">合同开始时间</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["htkssj"]%>
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">合同结束时间</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["htjssj"]%>
				</td>
				<td class='TdLabel' style="width:15%">合同金额(万元)</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["htje"]%>
				</td>
			</tr>
                <tr>
                <td class='TdLabel' style="width:15%">免费运维期</td>
				<td    class='TdContent' style="width:35%">
                     <%=document["mfywq"]%>
				</td>
				<td class='TdLabel' style="width:15%">项目进度</td>
				<td    class='TdContent' style="width:35%">	
                    <%=document["xmjd"]%>
				</td>
			</tr>
             <tr>
                  
                    <td class='TdLabel'>按需结算</td>
                    <td class='TdContent'>
                        <%=document["jelx"].ToString()=="是"?"是":"否" %>
                    </td>
                </tr>
               
		</table>

            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">合同付款方式</span></legend>
                <table id="tabledy" style="width: 100%; margin-top: 5px; margin-bottom: 5px" class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class='TdLabel' style="width: 30%; text-align: center">支付条件</td>
                        <td class='TdLabel' style="width: 60%; text-align: center">支付金额</td>
                       
                    </tr>
                    <%for(int i=0;i<dsHtfkfs.Tables[0].Rows.Count;i++){ %>
                    <tr >
                        <td  class='TdContent' style="width: 30%; text-align: center">
                           <%-- <asp:TextBox runat="server" ID="ProGuid" ></asp:TextBox>--%>
                            <%=dsHtfkfs.Tables[0].Rows[i]["zftj"].ToString() %>
                        </td>
                        <td class='TdContent' style="width: 60%; text-align: center">
                            <%=dsHtfkfs.Tables[0].Rows[i]["zfje"].ToString() %>
                        </td>
                    </tr>
                    <%} %>

                </table>
            </fieldset>

            <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">附件</span></legend>
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
        cfg0.fileSign = "tz_xmht";
        cfg0.view = true;

        $("#filecontent0").fileviewload(cfg0);
       
    });

    var config = new Config();
    config.deleteData();


</script>


