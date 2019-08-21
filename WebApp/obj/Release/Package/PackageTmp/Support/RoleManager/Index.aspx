<%@ Page Title="" Language="C#" MasterPageFile="~/Support/Shared/Support.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Yawei.App.Support.RoleManager.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title></title>

    <script src="../Scripts/newguid.js"></script>
    <script src="../../Plugins/jquery-ztree/ztree-all.min.js"></script>
    <link href="../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
 <style type="text/css">
     div #rMenu {position:absolute;background-color: #555;text-align: left;padding: 2px;}
     .panel-header,.layout-expand
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
         background-image:url(../../images/user.gif);
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
           <li>
            <div class="blocktop"><span>角色管理</span>
            
		    <div style="float:right;line-height:25px;padding-right:15px;font-size:12px">
                角色名称：<input type="text" id="RoleName" style="width:100px;" />
                用户名称：<input type="text" id="UserName" style="width:100px;"/>    
                <input type="button" value="查询" onclick="search1()" />
                <input type="button" value="重置" onclick="Reset1()"/>                  
            </div>
	    
           </div>  
            <div style="height:490px"> 
		        <ul id="roleTree" class="ztree" style="width:97%; height:99%; overflow:auto;"></ul>
            </div>
 
        </li>
           <li>
	
        
        <div class="blocktop"><span>用户信息</span>
             <div style="float:right;line-height:25px;padding-right:15px;font-size:12px">
                分组名称： <input type="text" id="GroupName" style="width:80px" />
                用户名称： <input type="text" id="UserName2" style="width:80px"/>
                <input type="button" value="查询" onclick="search2()" />
                <input type="button" value="重置" onclick="Reset2()"/>    
            </div>
           
	    </div>
    <div style="height:490px"> 
    <ul id="groupTree" class="ztree" style="width:97%; height:99%; overflow:auto;"></ul></div>
</li>


   
</ul>
 


    <div id="rMenu"  style="width:120px; display:none;position:absolute;background-color:white" class="DivCotent">
        <div class="DivGrayTop" style="height:20px;line-height:20px"><span>角色管理</span><img src="../../images/cross.png" style="color:blue;cursor:pointer;float:right;height:10px;width:10px" onclick="$('#rMenu').hide();" /></div>
		<div id="addrole" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer" >添加角色</div>		
		<div id="renamenode"   style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer">重命名</div>		
		<div id="nodedetele"  style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer">删除</div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

<script src="Role.js" type="text/javascript"></script>

</asp:Content>
