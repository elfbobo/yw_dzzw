<%@ Page Title="" Language="C#" MasterPageFile="~/Support/Shared/Support.Master" AutoEventWireup="true" CodeBehind="Directory.aspx.cs" Inherits="Yawei.App.Support.Mapping.Directory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Plugins/jquery-ztree/ztree-all.min.js"></script>
    <link href="../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
    <style type="text/css">
        li a span {
            cursor:pointer;
        }
        td {
            font-size: 14px;
            color: #2a2a2a;
            font-weight: normal;
            text-align: left;
            border: none 0px;
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
        <%--<div class="DivGrayTop" style="font-size:20px;font:bold;font-family:微软雅黑">&nbsp;&nbsp;&nbsp;&nbsp;映射管理</div>--%>
    <table style="width:100%;height:500px;border-spacing:0px;padding-top:5px;">
        <tr>
            <td style="width:38%;height:auto;display:table-cell;vertical-align:top;">
                <div class="blocktop"><span>映射目录</span></div>
                <div>
                    <ul id="ztree" class="ztree" style="width:98%;height:100%;overflow:auto;"></ul>
                </div>
            </td>
            <td style="width:60%;display:table-cell;height:auto;border-left:1px solid lightgray;vertical-align:top">
                <div class="blocktop"><span>映射数据</span></div>
                <div id="showplace" style="border-left:1px solid lightgray;height:94%;">
                </div>
            </td>
        </tr>
    </table>
    <div id="RCShow" style="display:none;">
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="addNode()">&nbsp;<img src="../../Images/add.png" style="vertical-align:middle" alt="新增" />&nbsp;&nbsp;新增</div>
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="editName()">&nbsp;<img src="../../Images/edit.png" style="vertical-align:middle" alt="修改" />&nbsp;&nbsp;修改</div>
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="delNode()">&nbsp;<img src="../../Images/delete.png" style="vertical-align:middle" alt="删除" />&nbsp;&nbsp;删除</div>
    </div>
    <div id="HeadRC" style="display:none;">
        <div style="border-bottom:1px dashed lightgray;cursor:pointer;font-size:12px;" onclick="addNode()">&nbsp;<img src="../../Images/add.png" style="vertical-align:middle" alt="新增" />&nbsp;&nbsp;新增</div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">

        var zTreeObj;

        //新增
        function addNode() {
            var nodes = zTreeObj.getSelectedNodes();
            var newNode = { name: "新建映射", nocheck: false, status: "create" };
            var newNode = zTreeObj.addNodes(nodes[0], newNode);
            zTreeObj.selectNode(newNode[0]);
            editName();
        }

        //修改
        function editName() {
            var nodes = zTreeObj.getSelectedNodes();
            zTreeObj.editName(nodes[0]);
        }

        //保存
        function saveName(event, treeId, treeNode) {
            if (treeNode.status == "create") {
                $.post("Directory.aspx", { type: "postsave", aimId: treeNode.id, aimPid: treeNode.getParentNode().id, aimName: treeNode.name, status: treeNode.status }, function (data) {
                    var result = data;
                    if (result == '1') {
                        return true;
                    }
                    else if (result == '2') {
                        zTreeObj.removeNode(treeNode);
                    }
                    else { return false; }
                });
            }
            else {
                $.post("Directory.aspx", { type: "postsave", aimId: treeNode.id, aimPid: treeNode.getParentNode().id, aimName: treeNode.name }, function (data) {
                    var result = data;
                    if (result == '1') {
                        return true;
                    }
                    else if (result == '2') {
                        zTreeObj.removeNode(treeNode);
                    }
                    else { return false; }
                });
            }
        }

        //删除
        function delNode() {
            var nodes = zTreeObj.getSelectedNodes();
            $.post("Directory.aspx", { type: "postdel", aimId: nodes[0].id }, function (data) {
                var result = data;
                if (result != '0') {
                    zTreeObj.removeNode(nodes[0]);
                    zTreeObj.updateNode(nodes[0]);
                    return true;
                }
                else { return false; }
            });
        }

        //右键菜单
        function RightClick(event, treeId, treeNode) {
            var off = $(event.target).offset();
            if (treeNode != null) {
                zTreeObj.selectNode(treeNode);
                if (treeNode.id == "19ce7196-04ef-4f6a-893e-2981127d9bb5") {
                    $("#RCShow").hide();
                    $("#HeadRC").attr("style", "position:absolute;top:" + off.top + "px;left:" + (off.left + $(event.target).width() - 3) + "px;z-index:999;color:black;background-color:white;border:1px solid gray;line-height:20px;width:60px");
                    $("#HeadRC").show();
                }
                else {
                    $("#HeadRC").hide();
                    $("#RCShow").attr("style", "position:absolute;top:" + off.top + "px;left:" + (off.left + $(event.target).width() - 3) + "px;z-index:999;color:black;background-color:white;border:1px solid gray;line-height:20px;width:60px");
                    $("#RCShow").show();
                }
                event.preventDefault();
            }
            else { return false; }
        }

        //单击树事件
        function OnClick(event, treeId, treeNode) {
            if (treeNode.id != "19ce7196-04ef-4f6a-893e-2981127d9bb5") {
                $.post("Directory.aspx", { type: "post", treeguid: treeNode.id }, function (data) {
                    var result = data;
                    if (result != "0") {
                        $("#rightBT").remove();
                        $("#showplace").children().remove();
                        $("#showplace").append(result);
                        $(":input[type=submit],:input[type=button]").button();
                    }
                });
            }
        }

        //动态行
        function addTr() {
            $("#showplace table tr:last").after("<tr><td style='width:8%;text-align:right;padding:5px;border-bottom:1px dashed lightgray;'>编码：</td><td style='width:33%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:98%' type='text' name='Code' /></td><td style='width:8%;text-align:right;padding:5px;border-bottom:1px dashed lightgray;'>内容：</td><td style='width:33%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:98%' type='text' name='Substance' /></td><td style='width:15%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:100%;height:20%;font-size:12px;' type='button' value='删除' onclick='delTr()' /></td></tr>");
            $(":input[type=submit],:input[type=button]").button();
        };

        //删除动态行
        function delTr() {
            $("#showplace table tr:last").remove();
        }

        //跳新建
        function jumpNew() {
            var TableHTML = "<div class='DivBlueTop'>&nbsp;&nbsp;&nbsp;&nbsp;映射数据<input type='button' style='width:10%;font-size:12px;float:right;margin-top:2px;' value='保存' onclick='saveNew()' /></div>";
            TableHTML += "<table style='width:100%;margin-top:10px'>";
            TableHTML += "<tr><td style='width:8%;text-align:right;padding:5px;border-bottom:1px dashed lightgray;'>编码：</td><td style='width:33%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:98%' type='text' name='Code' /></td><td style='width:8%;text-align:right;padding:5px;border-bottom:1px dashed lightgray;'>内容：</td><td style='width:33%;padding:5px;border-bottom:1px dashed lightgray;'><input style='width:98%' type='text' name='Substance' /></td><td style='width:15%;padding:5px;border-bottom:1px dashed lightgray;'><input type='button' style='width:100%;height:20%;font-size:12px;' value='添加' onclick='addTr()' /></td></tr>";
            TableHTML += "</table>";
            $("#rightBT").remove();
            $("#showplace").children().remove();
            $("#showplace").append(TableHTML);
            $(":input[type=submit],:input[type=button]").button();
        }

        //编辑
        function edit() {
            var nodes = zTreeObj.getSelectedNodes();
            $.post("Directory.aspx", { type: "edit", treeguid: nodes[0].id }, function (data) {
                var result = data;
                if (result != "0") {
                    $("#rightBT").remove();
                    $("#showplace").children().remove();
                    $("#showplace").append(result);
                    $(":input[type=submit],:input[type=button]").button();
                }
            });
        }

        //编辑时删除
        function delEdit(btn) {
            if ($("#showplace table tr").length > 0) {
                $(btn).parent().parent().remove();
            }
        }

        //动态行保存
        function saveNew() {
            var newCodes = "";
            var newSubstances = "";
            for (var i = 0; i < $("#showplace table tr").length; i++) {
                if ($("#showplace table tr").eq(i).find("input[name='Code']").val() != "" && $("#showplace table tr").eq(i).find("input[name='Substance']").val() != "") {
                    newCodes += $("#showplace table tr").eq(i).find("input[name='Code']").val() + ',';
                    newSubstances += $("#showplace table tr").eq(i).find("input[name='Substance']").val() + ',';
                }
            }
            var Node = zTreeObj.getSelectedNodes();
            $.post("Directory.aspx", { type: "newdata", aimId: Node[0].id, aimName: Node[0].name, aimPid: Node[0].getParentNode().id, newCodes: newCodes, newSubstances: newSubstances }, function (data) {
                var result = data;
                if (result != "0") {
                    alert("保存成功");
                }
            });
        }

        var setting = {
            edit: {
                drag: {
                    isCopy: false,
                    isMove: false
                },
                enable: true,
                showRenameBtn: false,
                editNameSelectAll: true,
                showRemoveBtn: false
            },
            data: {
                simpleData: {
                    enable: true,
                    idKey: "id",
                    pIdKey: "pid",
                    rootPId: ""
                }
            },
            view: {
                expandAll: false,
                selectedMulti: false,
                nameIsHTML: true
            },
            check: {
                enable: false
            },
            callback: {
                onRightClick: RightClick,
                onClick: OnClick,
                onRename: saveName
            }
        }

        var json = <%=Jsons%>

    $(function () {
        zTreeObj = $.fn.zTree.init($("#ztree"), setting, json);
    });

    $(document).ready(function () {
        $("body").bind("click", function () {
            $("#RCShow").hide();
            $("#HeadRC").hide();
        })
    });

</script>
</asp:Content>
