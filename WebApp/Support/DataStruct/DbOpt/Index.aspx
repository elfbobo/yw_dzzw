<%@ Page Title="" Language="C#" MasterPageFile="~/Support/Shared/Support.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Yawei.App.Support.DataStruct.DbOpt.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../../Plugins/jquery-ztree/ztree-all.min.js"></script>
    <link href="../../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
    <script src="../../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../../Plugins/jquery.grid/themes/grid_gray.css" rel="stylesheet" />
    <link href="Css/DbMain.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <ul class="Content" >
        <li style="width:33%">
             <div class="blocktop">
                 <span><img src="../../../Images/jt.gif" />数据结构</span><input type="button" value="数据建模" onclick="_newDataModel()" style="cursor:pointer;margin-right:2px;margin-top:3px;float:right;padding:.1em 0.5em" />
                </div>
                <div>
                    <ul id="ztree" class="ztree" style="width:98%;height:500px;overflow:auto;"></ul>
                </div>
        </li>
        <li style="width:5px;border:0px solid"></li>
        <li style="width:auto">
                <div class="blocktop"><span><img src="../../../Images/jt.gif" />属性</span>
                <input type="button" value="保存" onclick="_save()" style="cursor:pointer;margin-right:2px;margin-top:3px;float:right;padding:.1em 0.5em" /></div>
                <div id="showplace" style="border-left:1px solid lightgray;height:400px;">
                    <table class="table" id="TableAttr" style="display:none">
                        <tr>
                            <td class="TdContent" style="width:20%;text-align:right">表名称</td>
                            <td class="TdContent" style="width:30%" id="tdtable"></td>
                            <td class="TdContent" style="width:20%;text-align:right"><font style="color:red">*</font>表描述</td>
                            <td class="TdContent" style="width:30%"><input id="TableDesc" type="text" /></td>
                        </tr>
                        <tr><td colspan="4">
                         <table class="table" id="TableSub" style="width:100%">
                            <tr>
                                <td class="TdContent" colspan="3" style="text-align:center">表扩展属性</td>
                            </tr>
                            <tr>
                                <td style="width:45%;text-align:center" class="TdContent">属性名</td>
                                <td style="width:45%;text-align:center" class="TdContent">属性值</td>
                                <td style="width:10%;text-align:center" class="TdContent">
                                    <input type="button" value="添加" id="addRow" style="cursor:pointer;" />
                                </td>
                            </tr>
                        </table>
                       </td></tr>
                    </table>
                    <table class="tableNew" id="ColumnAttr" style="display:none">
                        <tr><td class="TdLabelNew" style="width:17%">字段名</td><td class="TdContentNew" colspan="2" id="tdcolumn" style="width:33%"></td>
                            <td class="TdLabelNew" style="width:17%"><font style="color:red">*</font>字段描述</td>
                            <td class="TdContentNew" colspan="2" style="width:33%"><input id="FieldDesc" type="text" style="width:99%;"/></td>
                        </tr>
                        <tr><td style="width:17%" class="TdLabelNew">数据类型</td><td class="TdContentNew" style="width:16%"><select id="FieldDataType" style="width:99%" onchange="checkDataType()">
                                    <option value="bigint">bigint</option>
                                    <option value="binary">binary</option>
                                    <option value="bit">bit</option>
                                    <option value="char">char</option>
                                    <option value="date">date</option>
                                    <option value="datetime">datetime</option>
                                    <option value="datetime2">datetime2</option>
                                    <option value="datetimeoffset">datetimeoffset</option>
                                    <option value="decimal">decimal</option>
                                    <option value="float">float</option>
                                    <option value="geography">geography</option>
                                    <option value="geometry">geometry</option>
                                    <option value="hierarchyid">hierarchyid</option>
                                    <option value="image">image</option>
                                    <option value="int">int</option>
                                    <option value="money">money</option>
                                    <option value="nchar">nchar</option>
                                    <option value="ntext">ntext</option>
                                    <option value="numeric">numeric</option>
                                    <option value="nvarchar">nvarchar</option>
                                    <option value="real">real</option>
                                    <option value="smalldatetime">smalldatetime</option>
                                    <option value="smallint">smallint</option>
                                    <option value="smallmoney">smallmoney</option>
                                    <option value="sql_variant">sql_variant</option>
                                    <option value="text">text</option>
                                    <option value="time">time</option>
                                    <option value="timestamp">timestamp</option>
                                    <option value="tinyint">tinyint</option>
                                    <option value="uniqueidentifier">uniqueidentifier</option>
                                    <option value="varbinary">varbinary</option>
                                    <option value="varchar" selected="selected">varchar</option>
                                    <option value="xml">xml</option>
                                </select></td>
                            <td class="TdLabelNew" style="width:17%">字段长度</td><td class="TdContentNew" style="width:16%"><input id="FieldLength" type="text" style="width:99%;" onkeyup ='intOnly(this);' /></td>
                            <td class="TdLabelNew" style="width:17%">小数位数</td><td class="TdContentNew" style="width:17%"><input id="FieldDigit" onkeyup ='intOnly(this);' type="text" style="width:99%;"/></td>
                        </tr>
                        <tr><td style="width:17%" class="TdLabelNew">是否主键</td><td class="TdContentNew" style="width:16%"><input id="FieldPk" type="checkbox" style="width:16px;height:16px;border:none" /></td>
                            <td class="TdLabelNew" style="width:17%">允许Null值</td><td class="TdContentNew" style="width:16%"><input id="FieldNull" type="checkbox" style="width:16px;height:16px;border:none" /></td>
                            <td class="TdLabelNew" style="width:17%">默认值</td><td class="TdContentNew" style="width:17%"><input id="FieldValue" type="text" style="width:99%" /></td>
                        </tr>
                        <tr><td colspan="6">
                            <table class="table" id="ColumnSub" style="width:100%">
                            <tr>
                                <td class="TdContent" colspan="3" style="text-align:center">字段扩展属性</td>
                            </tr>
                            <tr>
                                <td style="width:45%;text-align:center" class="TdContent">属性名</td>
                                <td style="width:45%;text-align:center" class="TdContent">属性值</td>
                                <td style="width:10%;text-align:center" class="TdContent">
                                    <input type="button" value="添加" id="addColumnRow" style="cursor:pointer;" />
                                </td>
                            </tr>
                        </table>
                            </td></tr>
                    </table>
                </div>
        </li>
    </ul>

    <%--数据库、表、字段右键菜单(开始)--%>
    <div id="TableOpt" style="display:none;">
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="addColumn()">&nbsp;<img src="../Images/add1.png" style="vertical-align:middle" alt="新建字段" />&nbsp;&nbsp;新建字段</div>
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="selectColumn()">&nbsp;<img src="../Images/search1.png" style="vertical-align:middle" alt="查询数据" />&nbsp;&nbsp;查询数据</div>
       <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="delTable()">&nbsp;<img src="../Images/delete1.png" style="vertical-align:middle" alt="删除表" />&nbsp;&nbsp;删除表</div>
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="existTable()">&nbsp;<img src="../Images/table1.png" style="vertical-align:middle" alt="生成表" />&nbsp;&nbsp;生成表</div>
    </div>
    <div id="DataBaseOpt" style="display:none;">
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="addTable()">&nbsp;<img src="../Images/add1.png" style="vertical-align:middle" alt="新建表" />&nbsp;&nbsp;新建表</div>
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="importTable()" >&nbsp;<img src="../Images/import.png" style="vertical-align:middle" alt="导入表" />&nbsp;&nbsp;导入表</div>
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="delDataBase()">&nbsp;<img src="../Images/delete1.png" style="vertical-align:middle" alt="删除" />&nbsp;&nbsp;删除</div>
    </div>
    <div id="ColumnOpt" style="display:none;">
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="delColumn()">&nbsp;<img src="../Images/delete1.png" style="vertical-align:middle" alt="删除字段" />&nbsp;&nbsp;删除字段</div>
    </div>
    <%--数据库、表、字段右键菜单(结束)--%>

    <%--创建数据库连接串(开始)--%>
    <div id="DivDataModel" class="win" style="height: 320px; width:500px;display:none">
             <div class="titledialog" >
                <img alt="" src="../Images/disk.png" style="vertical-align:middle;cursor:pointer" onclick="_saveDataModel()" />
               <img src="../Images/cancel.png" style="margin-right: 5px; margin-top: 3px; cursor: pointer;vertical-align:text-bottom;" onclick="$('#DivDataModel').hide();$('#DBName,#DBDisplay,#DBConnection').empty();" />
                 </div>
                <div style="height: 280px; width:500px;">
                    <table class="table" cellpadding="0" cellspacing="0" style="width:99%;margin:5px 3px 5px 3px">
                        <tr>
				            <td class='TdLabel' style="width:20%"><font style="color:red">*</font>数据库名称</td>
				            <td class='TdContent' style="width:80%">
                                <asp:TextBox ID="DBName" runat="server" style="width:95%"></asp:TextBox>
				            </td>
                         </tr> 
                        <tr>
                            <td class='TdLabel' style="width:20%"><font style="color:red">*</font>显示名称</td>
				            <td class='TdContent' style="width:80%">
                                <asp:TextBox ID="DBDisplay" runat="server" style="width:95%"></asp:TextBox>
				            </td>
			            </tr>
                         <tr>
                         <td class='TdLabel' style="width:20%"><font style="color:red">*</font>数据库连接</td>
				            <td class='TdContent' style="width:80%">
                                <table style="width:100%">
                                    <tr><td style="width:25%;text-align:right">Data Source=</td><td style="width:75%"><asp:TextBox ID="DbSource" runat="server" style="width:95%" onblur="change()"></asp:TextBox></td></tr>
                                        <tr><td style="width:25%;text-align:right">Initial Catalog=</td><td style="width:75%"><asp:TextBox ID="DbCatalog" runat="server" style="width:95%" onblur="change()"></asp:TextBox></td></tr>
                                    <tr><td style="width:25%;text-align:right">User ID=</td><td style="width:75%"><asp:TextBox ID="DbUserID" runat="server" style="width:95%" onblur="change()"></asp:TextBox></td></tr>
                                        <tr><td style="width:25%;text-align:right">Password=</td><td style="width:75%"><asp:TextBox ID="DbPassword" runat="server" style="width:95%" TextMode="Password" onblur="change()"></asp:TextBox></td></tr>
                                    
                                </table>
				            </td>
                        </tr>
                        <tr><td class='TdContent' colspan="2" style="width:100%;height:25px">
                            <asp:Label ID="lblDBconn" runat="server" Text=""></asp:Label></td></tr>
                    </table>
                </div>
            </div>
    <%--创建数据库连接串(结束)--%>

    <%--查询数据开始--%>
    <div id="divsearchdata" class="win" style="height: 500px; width:850px;display:none">
             <div class="titledialog" >
               <img alt="查询" src="../Images/search.png" onclick="initGrid()" style="margin-top: 3px; cursor: pointer;vertical-align:text-bottom;" />
                 <img alt="选择字段" src="../Images/columns.png" style="margin-left: 5px;margin-right: 5px; margin-top: 3px; cursor: pointer;vertical-align:text-bottom;" onclick="selectSearchColumns()" />
               <img alt="关闭" src="../Images/close.png" style="margin-right: 5px; margin-top: 3px; cursor: pointer;vertical-align:text-bottom;" onclick="$('#divsearchdata').hide();" />
                 </div>
              <table id="talesearchdata" class="table"  style="width:99%" align="center" >
                <tr>
                    <td style="width:40%" class="TdContentNew">字段名</td>
                    <td style="width:12%" class="TdContentNew">操作符</td>
                    <td style="width:40%" class="TdContentNew">默认值</td>
                    <td style="width:8%" class="TdContentNew">
                        <input class="ui-button ui-widget ui-state-default ui-corner-all ui-state-focus" type="button" value="添加"   id="addsearchdata" />
                    </td>
                </tr>
                        </table>
                <table style="width:99%;margin-top:10px" align="center" >
                    <tr><td style="margin-top:10px;margin-top:10px;width:100%;border-bottom-color: lightgray; border-bottom-width: 1px; border-bottom-style: dashed;"></td></tr>
                    <tr><td style="height:10px"></td></tr>
                </table>
              <div style="width:99%" ><div id="grid"></div></div>
            </div>
    <%--查询数据结束--%>

    <%--导入表开始--%>
    <div id="divimporttable" class="win" style="height: 420px; width:850px;display:none">
             <div class="titledialog" >
                 <img alt="导入" src="../Images/import.png" style="margin-right: 5px; margin-top: 3px; cursor: pointer;vertical-align:text-bottom;" onclick="importTableToDb()" />
               <img alt="关闭" src="../Images/close.png" style="margin-right: 5px; margin-top: 3px; cursor: pointer;vertical-align:text-bottom;" onclick="$('#divimporttable').hide();" />
                 </div>
              <div id="importGrid" style="margin-top:10px;width:99%"></div>
    </div>
    <%--导入表结束--%>

    <asp:Button ID="btnDataModel" runat="server" OnClick="btnDataModel_Click" style="display:none" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        var zTreeObj;
        var json=<%=treeJson%>;
        var setting = {
            edit: {
                drag: {
                    isCopy: false,
                    isMove: false
                },
                enable: true,
                showRenameBtn: true,
                showRemoveBtn: false,
                renameTitle:"重命名"
            },
            check: {
                enable: false
            },
            view: {
                fontCss:setFontCss,
                showIcon: true,
                showLine:true,
                expandAll: false,
                selectedMulti: false,
                nameIsHTML: true
            },
            data:{
                simpleData: {
                    enable: true,
                    idKey: "id",
                    pIdKey:"pId"
                }
            },
            callback:{
                onClick:zTreeOnClick,
                onRightClick:zTreeOnRightClick,
                onRename:reName,
                beforeRename: zTreeBeforeRename,
                beforeEditName: zTreeBeforeEditName,//点击编辑时触发，用来判断该节点是否能编辑 
            }
        }

        //已生成的表显示为蓝色
        function setFontCss(treeId, treeNode) {
            if(treeNode.level==1 && treeNode.ext=="t")
                return {color:"blue"};
            else
            {};
        };

        var fieldNameHtml="";
        $(function () {
            zTreeObj = $.fn.zTree.init($("#ztree"), setting, json);
            //checkDataType();//根据数据类型设置小数位数及字段长度
            
            //动态行添加
            $("#addsearchdata").bind("click", function () {
                var rowHtml = "";
                rowHtml += "<tr>";
                rowHtml += "<td class='TdContentNew'>";
                rowHtml += "<select name='FieldName' style='width:60%'>" + fieldNameHtml + "</select></td>";
                rowHtml += "<td class='TdContentNew'>";
                rowHtml += "<select name='FieldOptType'><option value='>'>大于</option><option value='<'>小于</option><option value='='>等于</option>";
                rowHtml += "<option value='<>'>不等于</option><option value='like'>包含</option><option value='not like'>不包含</option></select></td>";
                rowHtml += "<td class='TdContentNew'><input name='FieldDefault' /></td>";
                rowHtml += "<td class='TdContentNew'>";
                rowHtml+="<input class='ui-button ui-widget ui-state-default ui-corner-all ui-state-focus' type='button' value='删除' onclick='deltr(this)' style='cursor:pointer;' />";
                rowHtml += "</td>";
                rowHtml += "</tr>";
                $("#talesearchdata").append(rowHtml);
            });

            $("#addRow").bind("click", function () {
                var rowHtml = "";
                rowHtml += "<tr>";
                rowHtml += "<td class='TdContent'><input type='text' name='TableAttrName' /></td>";
                rowHtml += "<td class='TdContent'><input type='text' name='TableAttrValue' /></td>";
                rowHtml += "<td class='TdContent' style='text-align:center'>";
                rowHtml+="<input class='ui-button ui-widget ui-state-default ui-corner-all ui-state-focus' type='button' value='删除' onclick='deltr(this)' style='cursor:pointer;' />";
                rowHtml += "</td></tr>";
                $("#TableSub").append(rowHtml);
            });

            //动态行添加
            $("#addColumnRow").bind("click", function () {
                var rowHtml = "";
                rowHtml += "<tr>";
                rowHtml += "<td class='TdContent'><input type='text' name='FieldAttrName' /></td>";
                rowHtml += "<td class='TdContent'><input type='text' name='FieldAttrValue' /></td>";
                rowHtml += "<td class='TdContent' style='text-align:center'>";
                rowHtml += "<input class='ui-button ui-widget ui-state-default ui-corner-all ui-state-focus' type='button' value='删除' onclick='deltr(this)' style='cursor:pointer;' />";
                rowHtml += "</td></tr>";
                $("#ColumnSub").append(rowHtml);
            });
        });

        //保存
        function _save(){
            if(confirm("确定要保存吗？")){
                var nodes = zTreeObj.getSelectedNodes();//被选中的节点
                var len=patch(nodes[0].id);//区分是表节点还是字段节点
                if(nodes.length>0){
                    if(len==1){//表节点
                        saveTable(nodes[0]);
                    }
                    if(len==2)
                        saveColumn(nodes[0]);
                }
            }
        }

        function saveColumn(node){
            var FieldDesc=$("#FieldDesc").val();
            var FieldDataType=$("#FieldDataType").val();
            var FieldLength=$("#FieldLength").val();
            var FieldPk="";//0代表是，1代表不是
            if($("#FieldPk:checked").length>0)
                FieldPk="0";
            else
                FieldPk="1";
            var FieldDigit=$("#FieldDigit").val();
            var FieldNull="";//0代表不空,1代表空
            if($("#FieldNull:checked").length>0)
                FieldNull="0";
            else
                FieldNull="1";
            var FieldValue=$("#FieldValue").val();
            var ExtendProperty="";
            if(FieldDesc==""){
                alert("字段描述不能为空");
                $("#FieldDesc").focus();
                return;
            }

            if(FieldPk=="0" && FieldNull=="0")
            {
                alert("字段为主键时，不能为null");
                return;
            }

            if($("#ColumnSub").find("tr:gt(1)").length>0)
            {
                $("#ColumnSub").find("tr:gt(1)").each(function(i){
                    var name= $(this).find("input[name*='Name']").val();
                    var value= $(this).find("input[name*='Value']").val();
                    if(i>0)
                        ExtendProperty+="Ω"
                    ExtendProperty+=name+"^"+value;
                });
            }
            node.description=FieldDesc;node.type=FieldDataType;node.lengh=FieldLength;node.precision=FieldDigit;
            node.isnull=FieldNull;node.defaultvalue=FieldValue;node.isprimarykey=FieldPk;
            node.extendproperty=ExtendProperty;
            addColumnProperty(node.pId.split('^')[0],node.pId.split('^')[1],node.name,node.description,node.type,node.lengh,node.precision
                ,node.isnull,node.defaultvalue,node.isprimarykey,node.extendproperty);
        }

        function saveTable(node){
            var Display=$("#TableDesc").val();
            if(Display=="")
            {
                alert("表描述不能为空");
                $("#TableDesc").focus();
                return;
            }
            var ExtendProperty="";
            if($("#TableSub").find("tr:gt(1)").length>0)
            {
                $("#TableSub").find("tr:gt(1)").each(function(i){
                    var name= $(this).find("input[name*='Name']").val();
                    var value= $(this).find("input[name*='Value']").val();
                    if(i>0)
                        ExtendProperty+="Ω"
                    ExtendProperty+=name+"^"+value;
                });
            }
            node.display=Display;
            node.extendproperty=ExtendProperty;
            addTableProperty(node.pId,node.name,Display,ExtendProperty);
        }

        function addColumnProperty(){
            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: {ModelGuid:arguments[0] ,TableName: arguments[1],Name:arguments[2],Description:arguments[3],Type:arguments[4],Lengh:arguments[5],Precision:arguments[6],IsNull:arguments[7],DefaultValue:arguments[8],IsPrimaryKey:arguments[9],ExtendProperty:arguments[10],Opt:"columnattr"},
                success: function (data) {
                    if(data==-2){
                        alert("字段属性操作失败");
                    }else if(data==-1){
                        alert("表结构已生成，不能修改字段信息！");
                    }else{
                        alert("编辑成功");
                    }
                },
                error: function () { alert('操作失败'); }
            });
        }

        function addTableProperty(){
            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: {ModelGuid:arguments[0] ,TableName: arguments[1],Display:arguments[2],ExtendProperty:arguments[3],Opt:"tableattr"},
                success: function (data) {
                    if(data>0)
                        alert("编辑成功");
                    else
                        alert("编辑失败");
                },
                error: function () { alert('操作失败'); }
            });
        }

        //删除动态行
        function deltr(clickTd) {
            var tr = $(clickTd).parent().parent();
            tr.remove();
        }

        function zTreeBeforeEditName(treeId, treeNode) {
            var level=treeNode.level;
            if(level==1){
                var v=checkIfTableBuilded(treeNode.pId,treeNode.name);
                if(v==-2){
                    alert("操作失败!");return false;
                }
                if(v==-1){
                    alert("错误!");return false;
                }
                if(v>0){
                    alert("表结构已生成，表不能重命名!");
                    return false;
                }
            }

            if(level==2){
                var v=checkIfTableBuilded(treeNode.pId.split('^')[0],treeNode.pId.split('^')[1]);
                if(v>0){
                    v=checkTableIfColumnBuilded(treeNode.pId.split('^')[0],treeNode.pId.split('^')[1],treeNode.oldname);
                    if(v==-2){
                        alert("操作失败!");return false;
                    }
                    if(v==-1){
                        alert("错误!");return false;
                    }
                    if(v>0){
                        alert("表结构已生成，不能重命名该字段!");
                        return false;
                    }
                }
            }
            return true;
        }

        function zTreeBeforeRename(treeId, treeNode, newName, isCancel){
            var level=treeNode.level;
            if(newName=="")
            {
                if(level==1)
                    alert("表名称不能为空！");
                if(level==2)
                    alert("字段名称不能为空！");
                return false;
            }
            return true;
        }

        //节点重命名
        function reName(event, treeId, treeNode, isCancel){
            if(treeNode.name!=""){
                switch(treeNode.opt){
                    case "inserttable":
                        treeNode.id=treeNode.pId+"^"+treeNode.name;
                        treeNode.oldname=treeNode.name;
                        insertTableByAjax(treeNode.pId,treeNode.name,treeNode.opt,treeNode);
                        break;
                    case "insertcolumn":
                        treeNode.id=treeNode.pId.split('^')[0]+"^"+treeNode.pId.split('^')[1]+"^"+treeNode.name;
                        treeNode.oldname=treeNode.name;
                        insertColumnByAjax(treeNode.pId.split('^')[0],treeNode.pId.split('^')[1],treeNode.name,treeNode.opt,treeNode);
                        break;
                    case "updatetable":
                        editTableByAjax(treeNode.pId,treeNode.name,treeNode.opt,treeNode.oldname,treeNode);
                        break;
                    case "updatecolumn":
                        editColumnByAjax(treeNode.pId.split('^')[0],treeNode.pId.split('^')[1],treeNode.name,treeNode.opt,treeNode.oldname,treeNode);
                        break;
                }
            }
        }

        //编辑节点
        function editNode() {
            var nodes = zTreeObj.getSelectedNodes();
            zTreeObj.editName(nodes[0]);
        }

        function refreshTreeJson(){
            $.ajax({
                type: 'Post',
                url: '../../Handlers/DbTreeJson.ashx',
                async:false,
                success: function (data) {
                    zTreeObj = $.fn.zTree.init($("#ztree"), setting,eval(data));
                },
                error: function () {}
            });
        }

        function checkColumnsNum(node){
            var n=0;
            $.ajax({
                type: 'Post',
                url: '../../Handlers/DbColumnsNum.ashx',
                async:false,
                data: {ModelGuid:node.pId,TableName:node.name},
                success: function (data) {
                    n=data;
                },
                error: function () {alert('操作失败');}
            });
            return n;
        }

        function buildtable(node){
            if(checkColumnsNum(node)>0){
                $.ajax({ url: 'Index.aspx',type: 'Post',data: {ModelGuid:node.pId,TableName:node.name,Opt:"buildtable"},success: function (data) {
                    refreshTreeJson();
                },
                    error: function () {alert('操作失败');}
                });
            }
            else{
                alert("生成表结构至少要有一列！");
                return;
            }
        }

        var columnsSetting = "";
        var sortName="";
        //选择查询字段
        function selectSearchColumns(){
            columnsSetting="[";
            $("#talesearchdata tr:gt(0)").remove();
            var nodes = zTreeObj.getSelectedNodes();
            var sFeature = "dialogWidth:450px; dialogHeight:450px;center:yes;help:no;resizable:yes;scroll:no;status:no";
            var rnd = Math.round(Math.random() * 10000); //产生随机数，能自动刷新
            var sPath = "DataColumns.aspx?tempid=" + rnd + "&Table="+nodes[0].name+"&ModelGuid="+nodes[0].pId;
            var result = window.showModalDialog(sPath, "", sFeature);
            if (typeof (result) == "undefined")
                return;
            var arr = result.split(',');
            var w = Math.floor(100 / arr.length) + "%";//每列宽度
            sortName=arr[0].split('^')[1];
            for (var i = 0; i < arr.length; i++) {
                var name=arr[i].split('^')[0];
                var value=arr[i].split('^')[1];
                name=(name=="" ? value:name);
                columnsSetting += "{ field:'" + value + "', name: '" + name + "',render: cfg.render, align: 'left', width: '" + w + "',order: true }";
                if ((i + 1) < arr.length)
                    columnsSetting += ",";
            }
            columnsSetting += "]";
        }
        
        //grid动态查询条件
        function sqlwhere() {
            var where = "";
            $("#talesearchdata").find("tr:gt(0)").each(function (i, n) {
                var FieldName = $(this).find("[name='FieldName']").val();
                var FieldDesc = $(this).find("[name='FieldName'] option:selected").text();
                var FieldOptType = $(this).find("[name='FieldOptType']").val();
                var FieldDefault = $(this).find("[name='FieldDefault']").val();
                var width = Math.floor(100 / $("#subtable").find("tr:gt(0)").length) + "%";
                if (FieldOptType.indexOf("like") >= 0)
                    where += " and [" + FieldName + "] " + FieldOptType + " " + "'%" + FieldDefault + "%'";
                else
                    where += " and [" + FieldName + "] " + FieldOptType + " " + "'" + FieldDefault + "'";
            });
            return where;
        };

        function dbConn(DBConnection,node){
            $("#grid").children().remove();
            var cfg = [];
            cfg.render = function (value, rowsData, key) {
                return "<a style='cursor:pointer; font-size:11pt;' title='" + value + "'>" + value + "</a>";
            }
            cfg.connectionName = "";
            cfg.connectionString =DBConnection;
            cfg.providerName = "Yawei.DataAccess.SqlClient.SqlDatabase";
            cfg.tableName =node.name;
            cfg.sortName =sortName;
            cfg.order = "asc";
            cfg.pageCount = 10;
            cfg.pageSelect = [10, 15, 20, 50];
            cfg.where =sqlwhere();
            cfg.condition = "";
            cfg.ajaxUrl = "../../../Handers/GridDataHandler.ashx";
            cfg.width = '100%';
            cfg.height = 370;
            cfg.request = "ajax"; //默然ajax 
            //表格列头
            cfg.columns =eval("(" + columnsSetting + ")");
            $("#grid").Grid(cfg);
        }

        function initGrid(){
            if(columnsSetting==""){
                alert("请选择查询字段");
                return;
            }
            var nodes = zTreeObj.getSelectedNodes();
            $.ajax({ url: '../../Handlers/DbConn.ashx',type: 'Post',data: {ModelGuid:nodes[0].pId},success: function (data) {
                dbConn(data,nodes[0]);
            },
                error: function () {alert("error")}
            });
        }

        function getTableColumns(node){
            $.ajax({ 
                url: '../../Handlers/DbTableColumns.ashx',
                type: 'Post',
                aysnc:false,
                data: {ModelGuid:node.pId,Table:node.name},
                success: function (data) {
                    fieldNameHtml=data;
                },
                error: function () {alert('操作失败');}
            });
        }

        //查询数据
        function selectColumn(){
            $("#grid").children().remove();
            var nodes = zTreeObj.getSelectedNodes();
            $.ajax({ url: '../../Handlers/DbTableExist.ashx',type: 'Post',data: {ModelGuid:nodes[0].pId,TableName:nodes[0].name},success: function (data) {
                if(data==-1){
                    alert("错误");
                    return;
                }
                if(data==0){
                    alert("未生成表结构，不能查询数据！");
                    $("#TableOpt").hide();
                    return;
                }else{
                    $("#TableOpt").hide();
                    $("#divsearchdata").show();
                    getTableColumns(nodes[0]);
                }
            },
                error: function () {alert('操作失败');}
            });
        }

        //生成表
        function existTable(){
            var nodes = zTreeObj.getSelectedNodes();
            if(confirm("确定要生成该表吗？")){
                $.ajax({ url: '../../Handlers/DbTableExist.ashx',type: 'Post',data: {ModelGuid:nodes[0].pId,TableName:nodes[0].name},success: function (data) {
                    if(data==-1)
                    {
                        alert("错误");
                        return;
                    }
                    if(data==0)
                        buildtable(nodes[0]);
                    else
                    {
                        if(confirm("该表结构已生成，确定要再次生成吗？再次生成将删除表结构与数据")){
                            buildtable(nodes[0]);
                        }
                    }
                },
                    error: function () {alert('操作失败');}
                });
                $("#TableOpt").hide();
            }
        }

        function delDataBase(){
            var nodes = zTreeObj.getSelectedNodes();
            if(confirm("确定要删除该数据库连接串吗？")){
                $("#DataBaseOpt").hide();
                delDb(nodes[0].id);
                zTreeObj.removeNode(nodes[0]);
            }
        }

        //删除数据库连接串
        function delDb(){
            $.ajax({url: '../../Handlers/DbDel.ashx',type: 'Post',data: {Guid:arguments[0],Type:"db"},success: function (data) {},error: function () { alert('操作失败'); }
            });
        }

        function delTable(){
            var nodes = zTreeObj.getSelectedNodes();
            var v=checkIfTableBuilded(nodes[0].pId,nodes[0].name);
            if(v==-2){
                alert("操作失败!");return;
            }
            if(v==-1){
                alert("错误!");return;
            }
            if(v>0){
                if(confirm("表结构已经生成，确定要删除吗？")){
                    dropTable(nodes[0]);
                }else
                    $("#TableOpt").hide();
            }else{
                if(confirm("确定要删除该表吗？"))
                    dropTable(nodes[0]);
                else
                    $("#TableOpt").hide();
            }
        }

        function dropTable(node){
            $("#TableOpt").hide();
            delcolumnopt(node.pId,node.name,"");
            zTreeObj.removeChildNodes(node);zTreeObj.removeNode(node);
            deltableopt(node.pId,node.name);
        }

        function deltableopt(){
            $.ajax({ url: '../../Handlers/DbDel.ashx',type: 'Post',data: {ModelGuid:arguments[0],TableName:arguments[1],Type:"table"},success: function (data) {},
                error: function () { alert('操作失败'); }
            });
        }

        function delcolumnopt(){
            $.ajax({
                url: '../../Handlers/DbDel.ashx',
                type: 'Post',
                data: {ModelGuid:arguments[0],TableName:arguments[1],Name :arguments[2],Type:"column"},
                success: function (data) {
                },
                error: function () { alert('操作失败'); }
            });
        }

        //操作前先检查一下该表结构是否已经生成
        function checkIfTableBuilded(){
            var v=0;
            $.ajax({
                type: 'Post',
                url: '../../Handlers/DbTableExist.ashx',
                async:false,
                data: {ModelGuid:arguments[0],TableName:arguments[1]},
                success: function (data) {
                    v=data;
                },
                error: function () {v=-2;}
            });
            return v;
        }

        //操作前先检查一下该字段是否已经生成
        function checkTableIfColumnBuilded(){
            var v=0;
            $.ajax({
                type: 'Post',
                url: '../../Handlers/DbColumnExist.ashx',
                async:false,
                data: {ModelGuid:arguments[0],TableName:arguments[1],Name:arguments[2]},
                success: function (data) {
                    v=data;
                },
                error: function () {v=-2;}
            });
            return v;
        }

        function delColumn(){
            var nodes = zTreeObj.getSelectedNodes();
            if(confirm("确定要删除该字段吗？")){
                var ModelGuid=nodes[0].pId.split('^')[0];
                var TableName=nodes[0].pId.split('^')[1];
                var Name=nodes[0].name;
                var v=checkIfTableBuilded(ModelGuid,TableName);
                if(v==-2){
                    alert("操作失败!");return;
                }
                if(v==-1){
                    alert("错误!");return;
                }
                if(v>0){
                    alert("表结构已生成，不能删除字段!");$("#ColumnOpt").hide();
                    return;
                }else{
                    $("#ColumnOpt").hide();
                    delcolumnopt(ModelGuid,TableName,Name);
                    zTreeObj.removeNode(nodes[0]);
                }
            }else
                $("#ColumnOpt").hide();
        }

        //添加新列
        function addColumn(){
            var nodes=zTreeObj.getSelectedNodes();
            var level=nodes[0].level;
            if(level==1){
                var newNode={id:nodes[0].id+"^newcolumn",icon:"../Images/column.png",nocheck: false ,pId:nodes[0].id,name:"新建字段",opt:"insertcolumn",oldname:"新建字段"};
                newNode=zTreeObj.addNodes(nodes[0],newNode);
                zTreeObj.selectNode(newNode[0]);
                editNode();
                $("#TableOpt").hide();
            }
        }

        //添加新表
        function addTable(){
            var nodes=zTreeObj.getSelectedNodes();
            var level=nodes[0].level;
            if(level==0){
                var newNode={id:nodes[0].id+"^newtable",icon:"../Images/table.png",pId:nodes[0].id,name:"新建表",opt:"inserttable",oldname:"新建表"};
                newNode=zTreeObj.addNodes(nodes[0],newNode);
                zTreeObj.selectNode(newNode[0]);
                editNode();
                $("#DataBaseOpt").hide();
            }
        }

        function getNode(){
            var nodes=zTreeObj.getSelectedNodes();
            return nodes[0];
        }

        //导入表库的grid列表
        function importTable(){
            $("#DataBaseOpt").hide();
            $("#divimporttable").show();
            $("#importGrid").children().remove();
            var cfg = [];
            cfg.render = function (value, rowsData, key) {
                return "<a style='cursor:pointer; font-size:11pt;' title='" + value + "'>" + value + "</a>";
            }
            cfg.connectionName = "";
            cfg.connectionString =getNode().dbConn;
            cfg.providerName ="Yawei.DataAccess.SqlClient.SqlDatabase";
            cfg.tableName ="TableBaseInfo";
            cfg.sortName = "Name";
            cfg.order = "asc";
            cfg.pageCount = 10;
            cfg.pageSelect = [10, 15, 20, 50];
            cfg.where = "";
            cfg.condition = "";
            cfg.ajaxUrl = "../../../Handers/GridDataHandler.ashx";
            cfg.width = '99%';
            cfg.height = 370;
            cfg.request = "ajax"; //默然ajax 
            
            //表格列头
            cfg.columns = [
            { field: "Name", checkbox: true, width: "50", render: cfg.render },
            { field: "Name", name: "表名称", width: "30%", render: cfg.render, align: "left", drag: true, order: true },//drag 是否拖动
            { field: "Value", name: "表说明",render: cfg.render, align: "left", drag: true, order: true },
            { field: "CrDate", name: "创建日期", width: "20%", render: cfg.FormatDate, align: "left", drag: true, order: true },
            { field: "ModifiedTime", name: "最后修改日期", width: "20%", render: cfg.FormatDate, align: "left", drag: true, order: true }
            ];
            $("#importGrid").Grid(cfg);
        }

        //将选择的表结构导入到本数据库中去
        function importTableToDb(){
            var tables=[];
            var tableNames="";
            tables=$("#importGrid").GetSelection("single");
            for(var i=0;i<tables.length;i++){
                if(i>0)
                    tableNames+=",";
                tableNames+=tables[i];
            }
            if(tableNames!=""){
                if(confirm("确定要导入选中的表结构吗？")){
                    var node=getNode();
                    $.ajax({
                        url: '../../Handlers/DbImportTableToDB.ashx',
                        type: 'Post',
                        aysnc:false,
                        data: {DbConn:node.dbConn,Tables:tableNames,ModelGuid:node.id},
                        success: function (data) {
                            if(data>0){
                                alert("导入成功!");
                                $("#importGrid").children().remove();
                                $("#divimporttable").hide();
                                refreshTreeJson();
                            }
                            else
                                alert("导入失败!");
                        },
                        error: function () { alert('错误'); }
                    });
                }
            }else
                alert("请选择要导入的表");
        }

        function insertColumnByAjax(modelGuid,tableName,name,opt,node) {
            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: {ModelGuid:modelGuid,TableName:tableName,Name :name,Opt:opt},
                success: function (data) {
                    if(data==-1)
                    {
                        alert("已存在该字段，请重新命名");
                        zTreeObj.selectNode(node);
                        editNode();
                    } else if(data==-2){
                        alert("生成的表结构中已经存在该字段，请重新命名");
                        zTreeObj.selectNode(node);
                        editNode();
                    }
                    else
                        node.opt="updatecolumn";
                },
                error: function () { alert('创建失败'); }
            });
        }

        function editColumnByAjax(modelGuid,tableName,name,opt,oldName,node) {
            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: {ModelGuid:modelGuid,TableName:tableName,Name:name,Opt:opt,OldName:oldName},
                success: function (data) {
                    if(data==-1)
                    {
                        alert("已存在该字段");
                        zTreeObj.selectNode(node);
                        editNode();
                    }else
                        refreshTreeJson();
                },
                error: function () { alert('操作失败'); }
            });
        }

        function insertTableByAjax(modelGuid,tableName,opt,node) {
            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: {ModelGuid:modelGuid,TableName:tableName,Display :tableName,Opt:opt},
                success: function (data) {
                    if(data==-1)
                    {
                        alert("已存在该表，请重新命名");
                        zTreeObj.selectNode(node);
                        editNode();
                    }
                    else
                        node.opt="updatetable";
                },
                error: function () { alert('新建失败'); }

            });
        }

        function editTableByAjax(modelGuid,tableName,opt,oldName,node) {
            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: {ModelGuid:modelGuid,TableName:tableName,Opt:opt,OldName:oldName},
                success: function (data) {
                    if(data==-1)
                    {
                        alert("已存在该表");
                        zTreeObj.selectNode(node);
                        editNode();
                    }else
                        refreshTreeJson();
                },
                error: function () { alert('操作失败'); }
            });
        }

        function zTreeOnClick(event,treeId,treeNode){
            $("#TableOpt,#DataBaseOpt,#ColumnOpt").hide();
            if(treeNode!=null){
                var level=treeNode.level;
                if(level==1){
                    getTableAttr(treeNode.pId,treeNode.name);
                    $("#TableSub").find("tr:gt(1)").remove();
                    $("#TableAttr").show();
                    $("#ColumnAttr").hide();$("#tdtable").html(treeNode.name);
                }
                if(level==2){
                    getColumnAttr(treeNode.pId.split('^')[0],treeNode.pId.split('^')[1],treeNode.name);
                    $("#ColumnSub").find("tr:gt(1)").remove();
                    $("#TableAttr").hide();
                    $("#ColumnAttr").show();$("#tdcolumn").html(treeNode.name);
                }
            }
        }

        function getColumnAttr(){
            $.ajax({
                url: '../../Handlers/DbGetProperty.ashx',
                type: 'Post',
                data: {ModelGuid:arguments[0] ,TableName: arguments[1],Name:arguments[2],Type:"column"},
                success: function (data) {
                    if(data!="")
                    {
                        data=eval(data);
                        $("#FieldDesc").val(data[0].Description);
                        $("#FieldDataType").val(data[0].Type);
                        $("#FieldLength").val((data[0].Lengh=="-1" ? "max":data[0].Lengh));
                        if(data[0].IsPrimaryKey=="0"){
                            $("#FieldPk").prop("checked","checked");
                        }
                        else
                            $("#FieldPk").prop("checked",false);

                        if(data[0].IsNull=="0"){
                            $("#FieldNull").prop("checked","checked");
                        }
                        else
                            $("#FieldNull").prop("checked",false);

                        $("#FieldDigit").val(data[0].Precision);
                        $("#FieldValue").val(data[0].DefaultValue);

                        var ExtendProperty=data[0].ExtendProperty;
                        if(ExtendProperty!="")
                        {
                            var Property=ExtendProperty.split('Ω');
                            if(Property.length>0){
                                for(var i=0;i<Property.length;i++){
                                    var rowHtml = "";
                                    rowHtml += "<tr>";
                                    rowHtml += "<td class='TdContent'><input name='FieldAttrName' value='"+(Property[i].split('^')[0]==undefined ? "":Property[i].split('^')[0])+"' /></td>";
                                    rowHtml += "<td class='TdContent'><input name='FieldAttrValue' value='"+(Property[i].split('^')[1]==undefined ? "":Property[i].split('^')[1])+"' /></td>";
                                    rowHtml += "<td class='TdContent'>";
                                    rowHtml+="<input class='ui-button ui-widget ui-state-default ui-corner-all ui-state-focus' type='button' value='删除' onclick='deltr(this)' style='cursor:pointer;' />";
                                    rowHtml += "</td></tr>";
                                    $("#ColumnSub").append(rowHtml);
                                }
                            }
                        }
                    }
                },
                error: function () { alert('错误'); }
            });
        }

        function getTableAttr(){
            $.ajax({
                url: '../../Handlers/DbGetProperty.ashx',
                type: 'Post',
                data: {ModelGuid:arguments[0] ,TableName: arguments[1],Type:"table"},
                success: function (data) {
                    if(data!="")
                    {
                        data=eval(data);
                        $("#TableDesc").val(data[0].Display);
                        var ExtendProperty=data[0].ExtendProperty;
                        if(ExtendProperty!="")
                        {
                            var Property=ExtendProperty.split('Ω');
                            if(Property.length>0){
                                for(var i=0;i<Property.length;i++){
                                    var rowHtml = "";
                                    rowHtml += "<tr>";
                                    rowHtml += "<td class='TdContent'><input name='TableAttrName' value='"+(Property[i].split('^')[0]==undefined ? "":Property[i].split('^')[0])+"' /></td>";
                                    rowHtml += "<td class='TdContent'><input name='TableAttrValue' value='"+(Property[i].split('^')[1]==undefined ? "":Property[i].split('^')[1])+"' /></td>";
                                    rowHtml += "<td class='TdContent'>";
                                    rowHtml+="<input class='ui-button ui-widget ui-state-default ui-corner-all ui-state-focus' type='button' value='删除' onclick='deltr(this)' style='cursor:pointer;' />";
                                    rowHtml += "</tr>";
                                    $("#TableSub").append(rowHtml);
                                }
                            }
                        }
                    }
                },
                error: function () { alert('错误'); }
            });
        }

        function zTreeOnRightClick(event,treeId,treeNode){
            if(treeNode!=null){
                zTreeObj.selectNode(treeNode);
                var level=treeNode.level;
                var off = $(event.target).offset();
                if(level==0){
                    $("#TableOpt,#ColumnOpt").hide();
                    $("#DataBaseOpt").attr("style", "position:absolute;top:" + off.top + "px;left:" + (off.left + $(event.target).width() - 3) + "px;z-index:999;color:black;background-color:white;border:1px solid gray;line-height:20px;width:70px");
                    $("#DataBaseOpt").show();
                }

                if(level==1)
                {
                    $("#DataBaseOpt,#ColumnOpt").hide();
                    $("#TableOpt").attr("style", "position:absolute;top:" + off.top + "px;left:" + (off.left + $(event.target).width()+10) + "px;z-index:999;color:black;background-color:white;border:1px solid gray;line-height:20px;width:80px");
                    $("#TableOpt").show();
                }

                if(level==2){
                    $("#DataBaseOpt,#TableOpt").hide();$("#ColumnOpt").show();
                    $("#ColumnOpt").attr("style", "position:absolute;top:" + off.top + "px;left:" + (off.left + $(event.target).width()+10) + "px;z-index:999;color:black;background-color:white;border:1px solid gray;line-height:20px;width:80px");
                }
            }
        }

        //数据库连接串文本变化
        function change(){
            $("#lblDBconn").text("Data Source="+$("#DbSource").val()+";Initial Catalog="+$("#DbCatalog").val()+";User ID="+$("#DbUserID").val()+";Password="+$("#DbPassword").val());
        }

        function _saveDataModel(){
            if($("#DBName").val()==""){
                alert("数据库名称不能为空");
                return;
            }
            if($("#DBDisplay").val()==""){
                alert("数据库显示不能为空");
                return;
            }
            if($("#DbSource").val()==""){
                alert("数据库连接不能为空");
                return;
            }
            if($("#DbCatalog").val()==""){
                alert("数据库连接不能为空");
                return;
            }
            if($("#DbUserID").val()==""){
                alert("数据库连接不能为空");
                return;
            }
            if($("#DbPassword").val()==""){
                alert("数据库连接不能为空");
                return;
            }
            $("#Content_btnDataModel").click();
        }

        function _newDataModel(){
            $("#DivDataModel").show();
        }


        //根据主键所包含的^的个数，判断是什么类型的结点，无代表是数据库，1个代表是表，2个代表是字段
        function patch(s){
            var len=0;
            var s=s.match((/\^/g));
            if(s!=null && s!=undefined)
                len= s.length;
            return len;
        }

        //数据类型变化时，检查字段是否可从原来数据类型转换成新的数据类型
        function checkDataType() {
            var arrDec = ["decimal", "numeric"];
            var arrChar = ["binary","char", "nchar", "nvarchar", "varbinary", "varchar"];
            var arrOther = ["bigint", "bit", "date", "datetime", "datetime2", "datetimeoffset", "float", "geography", "geometry", "hierarchyid", "image", "int", "money", "ntext", "real", "smalldatetime", "smallint", "smallmoney", "sql_variant", "text", "timestamp", "tinyint", "uniqueidentifier  ", "xml","time"];
          
            SetFocus(arrOther, "NoLength", "NoDigit");//长度、小数位数不可填
            SetFocus(arrDec, "NoLength","Digit");//长度不可填
            SetFocus(arrChar, "Length", "NoDigit");//小数位数不可填
        }

        function dataType(){
            var nodes=zTreeObj.getSelectedNodes();
            if(nodes!=null)
            {
                $.ajax({
                    url: '../../Handlers/DbColumnType.ashx',
                    type: 'Post',
                    data: {DBConn:"<%=DBConnection%>",ModelGuid:nodes[0].pId.split('^')[0] ,TableName:nodes[0].pId.split('^')[1],Name:nodes[0].name,DataType:$("#FieldDataType").val()},
                    success: function (data) {
                        if(data==-1){
                            alert("字段不能从'"+nodes[0].type+"'数据类型转换成'"+$("#FieldDataType").val()+"'数据类型");
                            $("#FieldDataType").val(nodes[0].type);
                        }
                    },
                    error: function () { alert('错误'); }
                });
            }
        }

        function SetFocus(arr,type1,type2) {
            for (var i in arr) {
                if (arr[i] == $("#FieldDataType").val()) {
                    if (type1 =="NoLength") {
                        $("#FieldLength").bind("focus", function () { $(this).blur(); });
                        $("#FieldLength").val('');
                    }
                    else
                        $("#FieldLength").unbind("focus");
                    if (type2 =="NoDigit") {
                        $("#FieldDigit").bind("focus", function () { $(this).blur(); });
                        $("#FieldDigit").val('');
                    }
                    else
                        $("#FieldDigit").unbind("focus");
                    break;
                }
            }
        }

        function intOnly(oInput) {
            if ('' != oInput.value.replace(/\d/g, '')) {
                oInput.value = oInput.value.replace(/\D/g, '');
            }
        }
    </script>
</asp:Content>
