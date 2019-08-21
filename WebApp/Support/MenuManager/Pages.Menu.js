var zTreeObj;
var setting = {
    check: {
        enable: true,
        chkStyle: "checkbox",
        chkboxType: { "Y": "ps", "N": "p" }
    },
    edit: {
        enable: true,
        showRenameBtn: false,
        showRemoveBtn: false
    },
    async: {
        enable: true,
        url: "Index.aspx",
        autoParam: ["id"], //双击父节点传递的参数
        otherParam: { "Opt": "gettree"},
        dataFilter: filter
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    view: {
        fontCss: getFontCss,
        selectedMulti: false
    },
    callback: {
        onClick: clickNode,
        beforeDrop: beforeDrop,
        onDrop: dropNode,
        onRightClick: showMenu
    }
};

$(function () {

    var root = [{ id: "-1", pId: "0", name: "菜单管理", isParent: true }];
    zTreeObj = $.fn.zTree.init($("#tree"), setting, root);
    zTreeObj.expandNode(zTreeObj.getNodeByParam("id", "-1"), true, false);
    setTimeout('zTreeObj.expandAll(true)', 500);



    $(window.parent.document).find("iframe").height(window.document.body.scrollHeight + 20);//重新计算高度
});
function dataFormat(value) {
    if (value == "0")
        return "启用";
    return "禁用";
}

function filter(treeId, parentNode, childNodes) {//加载子节点
    if (!childNodes) return null;
    return childNodes;
}

//拖动菜单
function beforeDrop(treeId, treeNodes, targetNode, moveType) {
    if (targetNode == null || moveType == null) {
        return false;
    }
    if (moveType != null) {
        if (moveType == "inner") {
           // Index.DragNode(treeNodes[0].id, '0', targetNode.id);
            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: { Opt: "DragNode", guid: treeNodes[0].id, sort: '0', pid: targetNode.id },
                async: false,
                success: function (data) {
                  
                },
                error: function () { alert('保存失败'); }

            });


        } else {
           // Index.DragNode(treeNodes[0].id, '0', targetNode.pId);
            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: { Opt: "DragNode", guid: treeNodes[0].id, sort: '0', pid: targetNode.pId },
                async: false,
                success: function (data) {

                },
                error: function () { alert('保存失败'); }

            });

        }
        setting.callback.onClick(event, treeId, treeNodes[0]);
        //$('#table').propertygrid('reload');
      //var a=  [{group:'基本',child:[{name:'菜单名称',value:'',filed:'Name'},{name:'图标样式',value:'',filed:'IconCls'},{name:'图片链接',value:'',filed:'ImgUrl'},{name:'菜单标志',value:'',filed:'Sign'},{name:'菜单状态',value:'',filed:'Status'}]},{group:'操作',child:[{name:'链接地址',value:'',filed:'Href'},{name:'链接目标',value:'',filed:'Target'},{name:'脚本方法',value:'',filed:'JSEvent'}]}];
    }
    return true;
}

function dropNode(event, treeId, treeNodes, targetNode, moveType) {
    var tree = $.fn.zTree.getZTreeObj('tree');
    var nodes = tree.getNodesByParam('pId', treeNodes[0].pId, null);
    var guids = sort = "";
    for (var i = 0; i < nodes.length; i++) {
        guids += nodes[i].id + ",";
        sort += tree.getNodeIndex(nodes[i]) + ",";
    }
    //Index.DragNode(guids, sort, '');
    $.ajax({
        url: 'Index.aspx',
        type: 'Post',
        data: { Opt: "DragNode", guid: guids, sort: sort, pid: '' },
        async: false,
        success: function (data) {

        },
        error: function () { alert('保存失败'); }

    });

}
//突出显示节点
function getFontCss(treeId, treeNode) {
    return (!!treeNode.highlight) ? { color: "#A60000", "font-weight": "bold" } : { color: "#333", "font-weight": "normal" };

}
//右击显示菜单
function showMenu(event, treeId, treeNode) {
    event.preventDefault();
    $.fn.zTree.getZTreeObj('tree').selectNode(treeNode);
    var x = $(event.target).offset().left + $(event.target).width();
    $('#rmenu').css({ left: x, top: event.pageY-5 }).show();
}
//列表树点击事件
function clickNode(e, nid, node) {
   // var json = Index.GetMenujson(node.id).value;

    $.ajax({
        url: 'Index.aspx',
        type: 'Post',
        data: { Opt: "GetMenujson", guid: node.id },
        success: function (data) {
            CreateNodeInfo(eval(data), node.id);
        },
        error: function () { alert('保存失败'); }

    });



   

}
//保存菜单_
function propSave(index, data, changes) {
    $('#table').propertygrid('endEdit', index);
    var tree = $.fn.zTree.getZTreeObj("tree");
    var node = tree.getSelectedNodes()[0];
    editByAjax(node.id, index, data.value, 'saveprop');
    if (index == 1) {
        node.name = data.value;
        tree.updateNode(node);
    }
}

//搜索框重置
function Reset() {
    $("#Name").val("");
}
//搜索树节点，模糊查找
function Search() {
    var highlight = true;
    var treeObj = $.fn.zTree.getZTreeObj("tree");
    var nodelist = treeObj.getNodesByParam("highlight", true, null);
    for (var j = 0; j < nodelist.length; j++) {
        nodelist[j].highlight = false;
        treeObj.updateNode(nodelist[j]);
    }
    var value = $('#Name').val().toLowerCase();
    if (value != '') {
        var nodes = treeObj.getNodesByParamFuzzy('name', value, null);
        for (var i = 0; i < nodes.length; i++) {
            treeObj.expandNode(nodes[i].getParentNode(), true, false); //展开子节点
            nodes[i].highlight = highlight;
            treeObj.updateNode(nodes[i]);
        }
        var opennodes = treeObj.getNodesByParam("open", true);
        $.each(opennodes, function (i, node) {
            if (treeObj.getNodesByParam("highlight", true, node).length <= 0 && node.id != "-1") {
                treeObj.expandNode(node, false, false, true);
            }
        });
    }

}
//增加菜单
function Add() {
    var tree = $.fn.zTree.getZTreeObj('tree');
    var nodes = tree.getSelectedNodes();
    if (nodes.length > 0) {
        var guid ='';
        $.ajax({
            url: 'Index.aspx',
            type: 'Post',
            data: { Opt: "GetGuid" },
            async: false,
            success: function (data) {
                guid = data;
            },
            error: function () { alert('保存失败'); }

        });
        var node = tree.addNodes(nodes[0], { id: guid, pId: nodes[0].id, name: '未命名' });
        editByAjax(guid, nodes[0].id, tree.getNodeIndex(node[0]), 'addnode');
        tree.selectNode(node[0]);

        CreateNodeInfo(staticJson(), guid);
        
    } else {
        alert("提示消息", "请选择父菜单！");
    }
    $("#rmenu").hide();
}
//撤销修改_
function Cancel() {
    if ($('#table').propertygrid('getChanges').length == 0) {
        alert("提示消息", "菜单内容未修改！");
    } else {
       if(confirm('确认消息', '确认撤销操作？')) {
             $('#table').propertygrid('reload');
       }
    }
}
//删除菜单节点
function Remove() {
    var tree = $.fn.zTree.getZTreeObj('tree');
    var nodes = tree.getCheckedNodes();
    if (nodes.length > 0) {
        if(confirm("确认消息", "确认删除菜单吗？")) {
            
            var array = new Array();
            for (var i = 0; i < nodes.length; i++) {
                if ((nodes[i].id != -1) && (nodes[i].check_Child_State != 1)) {

                    if (!nodes[i].isParent) {
                        array.push("'" + nodes[i].id + "'");
                        tree.removeNode(nodes[i]);
                    }

                }
            }
            if (array.length > 0)
                editByAjax(array.toString(), null, null, 'delete');
           
        }
    } else { alert("提示消息", "未选中要删除的菜单！"); }
    $("#rmenu").hide();
}
function editByAjax() {
    $.ajax({
        url: 'Index.aspx',
        type: 'Post',
        data: { guid: arguments[0], pId: arguments[1], sort: arguments[2], Opt: arguments[3] },
        dataType: 'text',
        success: function (data) {
        },
        error: function () { alert('保存失败'); }

    });
}

function staticJson() {
    var ref;
    ret = "[{group:'基本',child:[{name:'菜单名称',value:'未命名',filed:'Name'}";
    ret += ",{name:'图标样式',value:'icon-menuItem',filed:'IconCls'}";
    ret += ",{name:'图片链接',value:'',filed:'ImgUrl'}";
    ret += ",{name:'菜单标志',value:'',filed:'Sign'}";
    ret += ",{name:'菜单状态',value:'',filed:'Status',select:[{name:'启用',value:0},{name:'禁用',value:1}]}]}";
    ret += ",{group:'操作',child:[{name:'链接地址',value:'',filed:'Href'}";
    ret += ",{name:'链接目标',value:'',filed:'Target',select:[{name:'_self',value:'_self'},{name:'_blank',value:'_blank'},{name:'_top',value:'_top'},{name:'_parent',value:'_parent'}]}";
    ret += ",{name:'脚本方法',value:'',filed:'JSEvent'}";
    ret += "]}]";
    return eval(ret);
}

function CreateNodeInfo(json, guid) {
    $("#nodeTable").children().remove();
    $.each(json, function (i, dat) {
        var div = $("<div />", { "class": "title" });
        var img = $("<img />").css({ "margin-left": "3px", "margin-right": "6px", "cursor": "pointer" });
        img.attr("src", "../Images/collapse.gif");
        img.click(function () {
            $(this).parent().next().toggle();
            if ($(this).attr("src").indexOf("collapse.gif") > -1) {
                $(this).attr("src", "../Images/expand.gif");
            } else {
                $(this).attr("src", "../Images/collapse.gif");
            }
        });
        div.append(img);

        div.append("<span>" + dat.group + "</span>");
        $("#nodeTable").append(div);
        var table = $("<table />", { "class": "table" });
        table.attr({ "cellspacing": 0, "cellpadding": "0" }).width("100%");
        table.data("guid", guid);
        $.each(dat.child, function (j, data) {
            var tr = $("<tr />");
            tr.dblclick(function () { trDbClick($(this)); });
            var td = $("<td />", { "class": "td" });
            td.text(data.name);
            td.width("40%");
            tr.append(td);
            var td2 = $("<td />", { "class": "td" });
            td2.text(data.value);
            if (data.select)
                td2.data("s", data.select);
            td2.data("filed", data.filed);
            tr.append(td2);
            table.append(tr);
        });

        $("#nodeTable").append(table);
    });

}

function trDbClick(tr) {
    var td = tr.find("td:last");
    if (td.find(":input").length <= 0) {
        var text = td.text();
        if (td.data("s")) {
            var sle = $("<select />").css("width", "100%");
            sle.append("<option></option>");
            $.each(td.data("s"), function (i, dat) {
                sle.append("<option value='" + dat.value + "'>" + dat.name + "</option>");
            });
            sle.find("option:contains('" + text + "')").attr("selected", "selected");
            sle.blur(function () {
                $(this).parent().text($(this).find("option:selected").text());
                var guid = td.parent().parent().parent().data("guid");
                var filed = td.data("filed");

                $.ajax({
                    url: 'Index.aspx',
                    type: 'Post',
                    data: { Opt: "ModifyMenus", index: filed, value: $(this).val(), guid: guid },
                    async: false,
                    success: function (data) {
                        
                    },
                    error: function () { alert('保存失败'); }

                });

            });
            td.html(sle);
            sle.focus();
        }
        else {
            var input = $("<input />").css("width", "100%");
            input.val(text);
            input.blur(function () {
                var guid = td.parent().parent().parent().data("guid");
                var filed = td.data("filed");

                $.ajax({
                    url: 'Index.aspx',
                    type: 'Post',
                    data: { Opt: "ModifyMenus", index: filed, value: $(this).val(), guid: guid },
                    async: false,
                    success: function (data) {
                       
                    },
                    error: function () { alert('保存失败'); }

                });

                 $(this).parent().text($(this).val());
                 if (filed == "Name")
                 {
                     var node = zTreeObj.getNodeByParam("id",guid,null);
                     node.name = $(this).val();
                     zTreeObj.updateNode(node);

                 }
            });

            td.html(input);
            input.focus();
        }
    }
}