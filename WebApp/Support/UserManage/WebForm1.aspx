<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Yawei.App.Support.UserManage.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery-easyui/jquery.easyui.min.js"></script>
    <script src="Scripts/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <link href="Scripts/jquery-easyui/themes/default/easyui.css" rel="stylesheet" />
    <link href="Scripts/jquery-easyui/themes/icon.css" rel="stylesheet" />
    <script src="Scripts/newguid.js"></script>
    <script src="../../Plugins/jquery-ztree/ztree-all.min.js"></script>
    <link href="../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
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
</style>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
            <div data-options="region:'north',title: '综合查询',border:false,iconCls:'icon-search'" style="height:60px;padding-left:10px; border-image:url(../../images/1.gif)">
         
          <div id="search" style=" float:left;margin-top:5px;  width:400px">          
            用户组:<input type="text" id="groupname" style="width:80px" />
            用户:<input type="text" id="username" style="width:80px"/>
            <input type="button" value="查询" id="treesearch"/>
            <input type="button" value="重置" id="reset"/>
            </div>
            <div id="propertyact" style=" float:left;margin-top:5px">            
            <input type="button" value="添加属性" id="addproperty" onclick="addPropertyName()"/>
            <input type="button" value="删除属性" id="deleteproperty"/>
          </div>
         
   </div>
    <div region="west" split="false" title="用户管理" data-options="border:true,iconCls:'icon-search'"  style="width:400px;overflow:hidden; border-image:url(../../images/1.gif)">
        <ul id="tree" class="ztree" style="width:97%; height:97%; overflow:auto;"></ul>
	</div>
	<div region="center" title="属性管理" style=" border-image:url(../../images/1.gif)">
        <table id='pg' class="propertygrid" data-options="fit:true,border:false"></table>         
	</div>
    <div id="addview" class="easyui-window" data-options="closed:true,collapsible:false,minimizable:false,maximizable:false,modal:true,title:'增加属性'" style="width:300px;height:200px;">
         <table style=" width:100%; height:80%; background-color:White; text-align:center; table-layout:auto">
         <tr>
         <td>属性名称：</td>
         <td><input  id="propertyname" type="text" style=" width:80%"/></td>
         </tr>
         <tr>
         <td> 属性类型：</td>
         <td><select id="propertytype" style=" width:80%">
              <option value="text">文本</option>
              <option value="datebox">日期</option>
              <option value="numberbox">数值</option>
             </select>
          </td>
         </tr>
         <tr>
         <td><input type="button" value="确定" id="savepropertyname"/></td>
         <td> <input type="button" value="取消" id="cancelpropertyname" onclick="ClosePropertyName()"/></td>
         </tr>
         </table>  
     </div>
     


<div id="rMenu" class="easyui-menu" style="width:120px; display:none">
		<div id="addgroup" data-options="border:true,iconCls:'icon-addgroup'" >添加分组</div>		
		<div  id="addADUser" data-options="border:true,iconCls:'icon-adduser'" >添加AD用户</div>
        <div  id="addDBUser" data-options="border:true,iconCls:'icon-adduser'" >添加DB用户</div>
         <div  id="addLocalhostUser" data-options="border:true,iconCls:'icon-adduser'" >添加本地用户</div>
		<div id="renamenode" data-options="border:true,iconCls:'icon-edit'"  >重命名</div>		
		<div id="nodedetele" data-options="iconCls:'icon-cancel'" >删除</div>
</div>
    </form>
</body>
</html>
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