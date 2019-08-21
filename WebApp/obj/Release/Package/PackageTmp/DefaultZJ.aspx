<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultZJ.aspx.cs" Inherits="WebApp.DefaultZJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
  
</head>
<body>
    <form id="form1" runat="server">
        <div id="treegrid">
            
        </div>
        <%--<input type="button" onclick="gets()" value="选择" />
        <input type="button" onclick="CannelSelection()" value="取消选择" />--%>
    </form>
</body>
</html>
<script type="text/javascript">
    var zjmc="<%=zjmc%>";
    var zjguid = "<%=zjguid%>";
    window.location.href = "Shared/SharedFormZj.aspx?zjmc=" + zjmc + "&zjguid=" + zjguid;
    //项目基本信息  办理计划  项目估算 项目预算 项目概算 项目决算 

</script>
 