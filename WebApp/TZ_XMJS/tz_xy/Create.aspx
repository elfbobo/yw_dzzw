<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMJS.tz_xy.Create" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                    &nbsp;&nbsp;当前位置：<a href="#">信息资源共享开放协议</a> >> 编辑信息资源共享开放协议
                </div>
                <div class="MenuRight">
                   
                    <%--<a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>--%>

                </div>
            </div>
            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 150px; font-size: 13px; font-weight: bold">信息资源共享开放协议</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>


        </div>
        
      
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>


<script type="text/javascript">

    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var pguid = "<%=proGuid%>";

    $(function () {

        <%if(roleCheck.isAdmin()||roleCheck.isBm()){%>
        var cfg0 = [];
        cfg0.content = "filecontent0";
        cfg0.refGuid = "<%=proGuid%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_Project_xy";
        cfg0.title = "信息资源共享开放协议";

        $("#file0").uploadfile(cfg0);


     

        <%}else{%>
        var cfg0 = [];
        cfg0.refGuid = "<%=proGuid%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_Project_xy";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

     
        <%}%>

    });


</script>

<script src="<%=Yawei.Common.AppSupport.AppPath %>/js/filefix.js" type="text/javascript"></script>