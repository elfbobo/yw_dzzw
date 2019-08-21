<%@ Page Title="" Language="C#" MasterPageFile="~/Support/Shared/Support.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Yawei.App.Support.UserManage.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="../Scripts/newguid.js"></script>
    <script src="../../Plugins/jquery-ztree/ztree-all.min.js"></script>
    <link href="../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.grid/jquery.property.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_gray.css" rel="stylesheet" />
 <style type="text/css">
div #rMenu {position:absolute;background-color: #555;text-align: left;padding: 2px;}
     .panel-header,.layout-expand,.window-header
     {
         background-image:url(../../images/2.gif);         
         color:white;        
         font-size:16px;
         border:solid 1px #F8F8FF;
     }
     .panel-body
     {
 border:solid 1px #F8F8FF;
     }
     .panel-icon
     {
         top:0px;
         height:100%
     }
     .layout-panel-west .icon-search
     {
         background-image:url(Images/user.gif);
     }
     .propertygrid .datagrid-view1 .datagrid-body, .propertygrid .datagrid-group,
     .panel-body
     {
         background-color:#F8F8FF;
         
     }
     .layout-panel-center
     {
         border-left:solid 1px rgb(167, 16, 22);
     }
     .window
     {
         top:100px;
     }
     input[type='button'] {
        
        padding:2px;
        border:1px solid #a3cdf3 ;
        border-radius:3px;
        background-color:#ffffff;
        cursor:pointer;
     }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">


    <ul class="Content" >
        <%--左侧开始--%>
        <li style="width:33%">
            <div class="blocktop"><span><img src="../../Images/jt.gif" />用户管理</span>
                  <div id="search" style=" float:right;margin-top:5px;  width:300px">          
                    组:<input type="text" id="groupname" style="width:80px" />
                    用户:<input type="text" id="username" style="width:80px"/>
                    <input type="button" value="查询" id="treesearch"/>
                    <input type="button" value="重置" id="reset"/>
                </div>

            </div>
            <div style="height:455px;""><ul id="tree" class="ztree" style="width:97%; height:100%; overflow:auto;"></ul></div>
        </li>
        <%--左侧结束--%>
        <%--中间--%>
        <li style="width:5px;border:0px solid"></li>
        <%--右侧开始--%>
        <li style="width:auto">
            <div class="blocktop"><span><img src="../../Images/jt.gif" />属性管理</span>
               
            </div>
            <div id='pg'></div>
        </li>
        <%--右侧结束--%>
    </ul>


<div id="rMenu" style="width:120px; display:none;position:absolute;background-color:white;z-index:99999" class="DivCotent">
        <div class="DivGrayTop" style="height:20px;line-height:20px"><span>用户管理</span>
            <img src="../../images/cross.png" style="color:blue;cursor:pointer;float:right;height:10px;width:10px" onclick="$('#rMenu').hide();" /></div>
		<div id="addgroup" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer" >添加分组</div>		
		<div id="addADUser" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer" >添加AD用户</div>
        <div id="addDBUser" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer" >添加DB用户</div>
        <div id="addLocalhostUser" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer" >添加本地用户</div>
		<div id="renamenode" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer"  >重命名</div>		
		<div id="nodedetele" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer" >删除</div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="Scripts/Users.js"></script>
<script src="Scripts/chooseUsers.js"></script>
<script type="text/javascript">
    $(function () {
        window.parent.$("iframe").height(500);
    })
    function addPropertyName() {
        $('#addview').window('open');
    }

    function ClosePropertyName() {
        $('#addview').window('close');
    }
</script>
<script src="Scripts/iframeCtrl.js"></script>
</asp:Content>
