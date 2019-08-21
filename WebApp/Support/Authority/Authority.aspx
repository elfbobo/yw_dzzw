<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Support/Shared/Support.Master" AutoEventWireup="true" CodeBehind="Authority.aspx.cs" Inherits="Yawei.App.Support.Authority.Authority" %>
<asp:Content ID="Content1"  ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="../../Plugins/jquery-ztree/ztree-all.min.js"></script>
    <link href="../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
    <script src="Scripts/Page.Authority.TreeAction.js"></script>
<%--    <script src="Scripts/Page.Authority.LicensesDataGride.js"></script>--%>
    <script src="Scripts/newguid.js"></script>
    <style type="text/css">
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
   
       <%-- <div >
           
            
              
                    <div style="float:right;width:200px">
               
                        </div>
        </div>--%>
           
    <ul class="Content" >
       
         <li >
            <div id="Role" >
                <div class="blocktop"><span>角色</span>
                    <div style="float:right;padding-right:15px;line-height:25px">
                        <input  /> <input value="查询" onclick="searchTree(this,'role')" type="button" />
                       
                    </div>
                </div>
                <div>
                    <ul id="RoleTree" class="ztree" style="height:470px;overflow:auto" >
                    </ul>
                </div>
            </div>
           </li>
        <li >
            <div id="Menu" >
               <div class="blocktop"><span>菜单</span>
                   <div style="float:right;padding-right:15px;line-height:25px">
                        <input  /> <input value="查询" onclick="searchTree(this,'menu')" type="button" />
                         <input type="button" id="permit"  style="margin-left: 40px" value="授权" />
                        <input type="button" id="del" style="margin-left: 10px" value="删除" />
                        <input type="button" id="refuse"  style="margin-left: 10px; " value="拒绝" />
                    </div>
               </div>
                    <div >
                        <ul id="MenuTree" class="ztree" style="height:470px;overflow:auto" >
                        </ul>
                    </div>
                
            </div>
           
         </li>
   </ul>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript">
        var muenJosn=<%=menuJson %>,roleJson=<%=roleJson %>;
       // var moudleJson=<%=moudleJson%>;
</script>
<script src="Scripts/Page.Authority.js"></script>
<script src="Scripts/Page.Authority.LicensesTree.js"></script>
<script type="text/javascript">

//

  
</script>
</asp:Content>
