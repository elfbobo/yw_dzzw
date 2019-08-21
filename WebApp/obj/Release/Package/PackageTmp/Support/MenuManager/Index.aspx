<%@ Page Title="" Language="C#" MasterPageFile="~/Support/Shared/Support.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Yawei.App.Support.MenuManager.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script src="../Scripts/newguid.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery-ztree/ztree-all.min.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
          <style type="text/css">
        .td
        {
      
            border-bottom:#ccc 1px dotted; 
            border-right:#ccc 1px dotted; 
            width:50%;
            text-align:left;
            font-size:12px;
            letter-spacing:2px;
            padding-left:5px;
            height:23px;
          }
          .title {
          
          background-color:#EFEFEF;text-align:left;color:blue; font-size:12px; font-weight:bold; height:20px; line-height:20px;
          }
          .table {
            width:100%;border-top:#ccc 1px dotted;border-left:#ccc 1px dotted;
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
       

     <div class="blocktop"><span>导航管理</span>
          <div id="search" style="margin-top:5px;float:right;margin-right:15px">
                    菜单名：<input type="text" id="Name" />
                    <input onclick="Search()" type="button" value="搜索"/>
                    <input onclick="Reset()" type="button" value="重置" />
                </div>
     </div>


   <table style="width:100%">       
           <tr>
           <td style="width:35%" >
            <div  style="height:500px; border:#99bbe8 1px solid" >
                
          
                <div style="height:470px">
                    <ul id="tree" class="ztree" style="width:99%; height:100%; overflow:auto;"></ul>
                </div>
              
                 </div>
             </td>
             <td>
                 <div id="nodeTable"  style="height:500px; border:#99bbe8 1px solid" >
                     <%--<div class="title"><img src="../../Images/Pages.Menus/collapse.gif" style="margin-left:3px;margin-right:6px" />基本</div>
                     <table cellspacing="0" cellpadding="0" >
                         <tr class="datagrid-row"><td class="datagrid-body" ></td><td class="datagrid-body" ></td></tr>
                     </table>--%>
                 </div>
                </td>               
            </tr>
    </table> 
                 <div id="rmenu" style="width:120px; display:none;position:absolute;background-color:white;z-index:99999" class="DivCotent">
                     <div class="DivGrayTop" style="height:20px;line-height:20px"><span>菜单管理</span>
                         <img src="../../images/cross.png" style="color:blue;cursor:pointer;float:right;height:10px;width:10px" onclick="$('#rmenu').hide();" /></div>
                    <div onclick="Add()" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer" >添加菜单</div>
                    <div onclick="Remove()" style="font-size:12px;border-bottom:1px dashed lightblue;line-height:25px;padding-left:10px;cursor:pointer" >删除菜单</div>
                 </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="Pages.Menu.js" type="text/javascript"></script>
</asp:Content>
