var dropfromtype = "group";//拖动的来源树节点 默认是来源为用户分组

function zTreeBeforeDrag(treeId, treeNodes) {
    for (var i = 0, l = treeNodes.length; i < l; i++) {
        if (treeNodes[i].drag === false) {
            return false;
        }
    }
    return true;
}

//为查询设置字体
function getFontCss(treeId, treeNode) {
    return (!!treeNode.highlight) ? { color: "#A60000", "font-weight": "bold"} : { color: "#333", "font-weight": "normal" };
}

function filter(treeId, parentNode, childNodes) {//加载子节点
    if (!childNodes) return null;
    return childNodes;
}

//重命名触发的函数
function zTreeOnRename(event, treeId, treeNode) {
    editByAjax(treeNode.id, treeId, treeNode.name, "update");
    return true;
}
//删除节点之前函数 
function zTreeBeforeonRemove(treeId, treeNode) {
    if (confirm("您确定要删除本节点？")) {
        return true;
    }
    else {
        return false;
    }
}

//重命名之前函数   暂时没用
function zTreeBeforeRename(treeId, treeNode, newName) {
    return true;
}

//设置所有叶子节点不显示重命名按钮
function setRenameBtn(treeId, treeNode) {
    var isparent = treeNode.type == "user";
    return !isparent;
}

function Reset1() {
    $('#RoleName').val(""); $('#UserName').val("");
}
function Reset2() {
    $('#GroupName').val(""); $('#UserName2').val("");
}

function editByAjax() {
    var re;
    $.ajax({
        url: 'Index.aspx',
        type: 'Post',
        data: { guid: arguments[0], parentguid: arguments[1], name: arguments[2], type: arguments[3], nodetype: arguments[4], moveType: arguments[5] },
        dataType: 'text',
        success: function (msz) { re = msz; }
    });
    //return re;
}

//删除触发的函数
function zTreeOnRemove(event, treeId, treeNode) {
    if (treeNode.type == "group")//删除分组
    {
        var treeObj = $.fn.zTree.getZTreeObj(treeId);
        var groupguid = new Array();
        var userguid = new Array();
        groupguid[0] = "'" + treeNode.id + "'";
        var groupnodes = treeObj.getNodesByParam("type", "group", treeNode);
        for (var i = 0; i < groupnodes.length; i++) {
            groupguid[groupguid.length] = "'" + groupnodes[i].id + "'";
        }
        var usernodes = treeObj.getNodesByParam("type", "user", treeNode);
        for (var i = 0; i < usernodes.length; i++) {
            userguid[userguid.length] = "'" + usernodes[i].id + "'";
        }
        editByAjax(groupguid.join(','), userguid.join(','), treeNode.name, "delete", treeNode.type, treeId);
    }
    else {
        editByAjax(treeNode.id, treeNode.pId, treeNode.name, "delete", treeNode.type, treeId);
    }
    return true;
}

//用于捕获节点拖拽操作结束之前的事件回调函数，并且根据返回值确定是否允许此拖拽操作
function zTreeBeforeDrop(treeId, treeNodes, targetNode, moveType) {
    if (treeId == "groupTree"||moveType!="inner")//只能拖到角色节点下 前面或者后面没有实际意义
        return false; 
    
    if (targetNode.type == "user" && moveType == "inner") {
        alert("不可拖动到用户下");
        return false;
    }
    if (treeNodes[0].tId.indexOf("groupTree") == -1)//拖动来源为角色树
    {
        dropfromtype = "role";
    }
    for (var i = 0; i < treeNodes.length; i++) {
        if (targetNode.id == "-1" && treeNodes[i].type == "user") {
            alert("用户不可放到角色根节点下");
            return false;
        }
    }
        if (!(targetNode == null || (moveType != "inner" && targetNode.id == "-1"))) {
            var num = treeNodes.length; //被拖拽节点的个数           
            var userguid = new Array();
            var userpid = new Array();//从角色拖拽过来的用户的所属角色主键
            var groupguid = new Array();
            var groups = "";//获取拖动过来的分组id
            var treeorg = treeNodes[0].tId;
            for (var i = 0; i < num; i++) {
                if (treeNodes[i].type == "user") {
                    userguid[userguid.length] = "'" + treeNodes[i].id + "'";
                    userpid[userpid.length] = "'" + treeNodes[i].pId + "'";
                }
                else {
                    groups += "'" + treeNodes[i].id + "',";
                    if (treeNodes[i].children != undefined) //拖动的是用户分组而且分组下还有数据
                        groups += GetGroups(treeNodes[i].children);
                    
                }
            }
            groups = "(" + groups.substr(0, groups.length - 1) + ")";

            $.ajax({
                url: 'Index.aspx',
                type: 'Post',
                data: { guid: userguid.join(','), groupsguid: groups, parentguid: targetNode.id, name: treeId, type: "drop", nodetype: "usertorole", moveType: moveType, dropfromtype: dropfromtype, userpids: userpid.join(",") },
                dataType: 'text',
                
                success: function (msz) {
                  
                    loadroletree();
                }
            });

            return true;  //禁止成为根节点
        }
}

//用于捕获节点拖拽操作结束的事件回调函数    treeNodes为复制以后出来的新的节点数据集合
function zTreeOnDrop(event, treeId, treeNodes, targetNode, moveType) {
  

        
  
        
}
//递归获取用户组下的所有分组信息
function GetGroups(treenodes)
{
    var groups = "";
    for (var i = 0; i<treenodes.length; i++)
    {
        if (treenodes[i].type == "group")

             groups += "'" + treenodes[i].id + "',";
            if (treenodes[i].children != undefined) {               
                groups += GetGroups(treenodes[i].children);
            }
           
    }
    return groups;
}
//用于捕获节点被拖拽的事件回调函数
function zTreeOnDrag(event, treeId, treeNodes) {
    if (treeId == "groupTree") {
        return false;
    }
}
function setEdit() {                //设置后面带有可编辑删除图标  暂时没用 被edit下方法取代
    var zTree = $.fn.zTree.getZTreeObj("roletree");
    zTree.setting.edit.showRemoveBtn = true;
    zTree.setting.edit.showRenameBtn = true;
    zTree.setting.edit.removeTitle = "删除";
    zTree.setting.edit.renameTitle = "重命名";
}


function search1() {
    var highlight = true;
    var treeObj = $.fn.zTree.getZTreeObj("roleTree");
    //查询之前折叠所有的根节点
    var rootnode = treeObj.getNodesByParam("pId", "-1", null);
    for (var i = 0; i < rootnode.length; i++) {
        if (rootnode[i].open) {
            treeObj.expandNode(rootnode[i], false, false); //折叠根节点
            rootnode[i].open = "false";
            treeObj.updateNode(rootnode[i]);
        }
    }

    var nodelist = treeObj.getNodesByParam("highlight", true, null);
    for (var j = 0; j < nodelist.length; j++) {
        nodelist[j].highlight = false;
        treeObj.updateNode(nodelist[j]);
    }
    var username = $('#UserName').val();
    var groupname = $('#RoleName').val();

    //通过角色名查询
    if (groupname != "" && username == "") {                                 //组名进行查找
        var nodes = treeObj.getNodesByParam("type", "group", null);
        if (nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].name.indexOf(groupname) > -1) {
                    if (nodes[i].pId != "-1") {
                        var parentnode = nodes[i].getParentNode();
                        while (parentnode.pId != "-1") {
                            treeObj.expandNode(parentnode, true, false); //展开父节点
                            parentnode = parentnode.getParentNode();
                        }
                        treeObj.expandNode(parentnode, true, false); //展开父节点
                    }
                    else {
                    }
                    treeObj.expandNode(nodes[i], true, true); //展开子节点
                    nodes[i].highlight = highlight;
                    treeObj.updateNode(nodes[i]);
                }
            }
        }
    }

    //通过用户名查询
    if (groupname == "" && username != "") {
        var nodes = treeObj.getNodesByParam("type", "user", null);
        if (nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].name.indexOf(username) > -1) {
                    var parentnode = nodes[i].getParentNode();
                    while (parentnode.pId != "-1") {
                        treeObj.expandNode(parentnode, true, false); //展开父节点
                        parentnode = parentnode.getParentNode();
                    }
                    treeObj.expandNode(parentnode, true, false); //展开父节点                               
                    nodes[i].highlight = highlight;
                    treeObj.updateNode(nodes[i]);
                }
            }
        }
    }

    if (groupname != "" && username != "") {
        var groupnodes = treeObj.getNodesByParam("type", "group", null);
        for (var i = 0; i < groupnodes.length; i++) {
            if (groupnodes[i].name.indexOf(groupname) > -1) {
                var usernodes = treeObj.getNodesByParam("type", "user", groupnodes[i]);
                for (var j = 0; j < usernodes.length; j++) {
                    if (usernodes[j].name.indexOf(username) > -1) {
                        var parentnode = usernodes[j].getParentNode();
                        while (parentnode.pId != "-1") {
                            treeObj.expandNode(parentnode, true, false);
                            parentnode = parentnode.getParentNode();
                        }
                        treeObj.expandNode(parentnode, true, false);
                        usernodes[j].highlight = highlight;
                        treeObj.updateNode(usernodes[j]);
                    }
                }
            }
        }
    }

}

function search2() {
    var highlight = true;
    var treeObj = $.fn.zTree.getZTreeObj("groupTree");
    //查询之前折叠所有的根节点
    var rootnode = treeObj.getNodesByParam("pId", "-2", null);
    for (var i = 0; i < rootnode.length; i++) {
        if (rootnode[i].open) {
            treeObj.expandNode(rootnode[i], false, false); //折叠根节点
            rootnode[i].open = "false";
            treeObj.updateNode(rootnode[i]);
        }
    }

    var nodelist = treeObj.getNodesByParam("highlight", true, null);
    for (var j = 0; j < nodelist.length; j++) {
        nodelist[j].highlight = false;
        treeObj.updateNode(nodelist[j]);
    }
    var username = $('#UserName2').val();
    var groupname = $('#GroupName').val();

    //通过分组名查询
    if (groupname != "" && username == "") {                                 //组名进行查找
        var nodes = treeObj.getNodesByParam("type", "group", null);
        if (nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].name.toLowerCase().indexOf(groupname) > -1) {
                    if (nodes[i].pId != "-1") {
                        var parentnode = nodes[i].getParentNode();
                        treeObj.expandNode(parentnode, true, false); //展开父节点
                    }
                    else {
                    }
                    treeObj.expandNode(nodes[i], false, true); //展开子节点
                    nodes[i].highlight = highlight;
                    treeObj.updateNode(nodes[i]);
                }
            }
        }
    }
    //通过用户名查询
    if (groupname == "" && username != "") {
        var nodes = treeObj.getNodesByParam("type", "user", null);
        if (nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].name.toLowerCase().indexOf(username) > -1) {
                    var parentnode = nodes[i].getParentNode();
                    treeObj.expandNode(parentnode, true, false); //展开父节点                               
                    nodes[i].highlight = highlight;
                    treeObj.updateNode(nodes[i]);
                }
            }
        }
    }
    //通过分组名和用户名查询
    if (groupname != "" && username != "") {
        var groupnodes = treeObj.getNodesByParam("type", "group", null);
        for (var i = 0; i < groupnodes.length; i++) {
            if (groupnodes[i].name.toLowerCase().indexOf(groupname) > -1) {
                var usernodes = treeObj.getNodesByParam("type", "user", groupnodes[i]);
                for (var j = 0; j < usernodes.length; j++) {
                    if (usernodes[j].name.toLowerCase().indexOf(username) > -1) {
                        var parentnode = usernodes[j].getParentNode();
                        treeObj.expandNode(parentnode, true, false);
                        usernodes[j].highlight = highlight;
                        treeObj.updateNode(usernodes[j]);
                    }
                }
            }
        }
    }
}

function zTreeOnRightClick(event, treeId, treeNode) {
    if (!treeNode && event.target.tagName.toLowerCase() != "button" && $(event.target).parents("a").length == 0) {
        $('#rMenu').hide();
    }
    else if (treeNode) {
        if (treeId == "roleTree") {
            treenode = treeNode;
            var treeObj = $.fn.zTree.getZTreeObj("roleTree");
            treeObj.selectNode(treeNode);
            showRMenu(treeNode.type, event.clientX, event.clientY,event);
        }
    }
}

function showRMenu(type, x, y,event) {
    x = $(event.target).offset().left + $(event.target).width();
    $('#rMenu').css({ left: x, top: y-5 });
    $('#rMenu div[id]').hide();
    if (type == "group") {//右键角色    显示所有按钮
        $('#rMenu div[id]').show();
    }
    else if (type == "user")//右键用户 只显示删除按钮
    {
        $('#rMenu div[id]:eq(2)').show();
    }
    else//根节点 只显示添加按钮
    {
        $('#rMenu div[id]:eq(0)').show();
    }
    $('#rMenu').show();
}

//添加工作组处理函数
$(function () {
    
    //右键删除
    $('#nodedetele').click(function () {
        $('#rMenu').hide();
        var treeObj = $.fn.zTree.getZTreeObj("roleTree");
        if (treenode.type == "group"&&confirm("确定要删除角色："+treenode.name+" 及其下面的用户？"))//删除分组
        {           
            var groupguid = new Array();
            var userguid = new Array();
            groupguid[0] = "'" + treenode.id + "'";
            var groupnodes = treeObj.getNodesByParam("type", "group", treenode);
            for (var i = 0; i < groupnodes.length; i++) {
                groupguid[groupguid.length] = "'" + groupnodes[i].id + "'";
            }
            var usernodes = treeObj.getNodesByParam("type", "user", treenode);
            for (var i = 0; i < usernodes.length; i++) {
                userguid[userguid.length] = "'" + usernodes[i].id + "'";
            }
            editByAjax(groupguid.join(','), userguid.join(','), treenode.name, "delete", treenode.type, "roleTree");
            treeObj.removeNode(treenode);
        }
        else if(treenode.type=="user"&&confirm("确定要删除用户:"+treenode.name+" ?")) {
            editByAjax(treenode.id, treenode.pId, treenode.name, "delete", treenode.type, "roleTree");
            treeObj.removeNode(treenode);
        }
        
        return true;
    });

    //右键单击重命名角色
    $('#renamenode').click(function () {
        $('#rMenu').hide();
        var treeObj = $.fn.zTree.getZTreeObj("roleTree");
        var nodes = treeObj.getSelectedNodes();
        treeObj.editName(treenode);
    });
    //右键添加角色
    $('#addrole').click(function () {
        $('#rMenu').hide();
        var treeObj = $.fn.zTree.getZTreeObj("roleTree");
        var guid = Guid.NewGuid().ToString();
        var newnode = treeObj.addNodes(treenode, { id: guid, pId: treenode.id, name: "新角色", type: 'group', icon: '../../Images/group_key.png' });
        treeObj.editName(newnode[0]);
        editByAjax(guid, treenode.id, "新角色", "addrole","");
    }
    );
});

$(function () {
    loadroletree();

});
function loadroletree()
{
    var zTreeObj;
    var setting = {
        edit: {
            enable: true,
            showRenameBtn: false,
            showRemoveBtn: false,
            drag: {
                isCopy: false,
                isMove: true
            }
        },
        check: {
            enable: false,
            chkStyle: "checkbox"
        },
        async: {
            enable: true,
            url: "Index.aspx",
            autoParam: ["id", "icon"], //双击父节点传递的参数       带此参数异步，不带同步
           
            dataFilter: filter
        },
        callback:
           {
               //beforeRename: zTreeBeforeRename, //修改名称之前触发
               //beforeRemove: zTreeBeforeonRemove, //删除节点之前触发
               onRename: zTreeOnRename,        //重命名触发函数
               //onRemove: zTreeOnRemove,
               onDrop: zTreeOnDrop,            //用于捕获节点拖拽操作结束的事件回调函数
               onDrag: zTreeOnDrag,            //用于捕获节点被拖拽的事件回调函数
               beforeDrop: zTreeBeforeDrop,  //用于捕获节点拖拽操作结束之前的事件回调函数，并且根据返回值确定是否允许此拖拽操作
               beforeDrag: zTreeBeforeDrag,
               onRightClick: zTreeOnRightClick   // 用于捕获节点被右击的事件回调函数
           },
        view: {
            fontCss: getFontCss
        },
        data: {
            simpleData: {
                enable: true
            }
        }
    };

    var root = [{ id: "-1", pId: "0", name: "角色管理", isParent: true, type: "root", treeType: "roleTree" }];
    zTreeObj = $.fn.zTree.init($("#roleTree"), setting, root);
    zTreeObj.expandNode(zTreeObj.getNodeByParam("id", "-1"), true, false);
    $(window.parent.document).find("iframe").height(window.document.body.scrollHeight + 20);//重新计算高度
}
$(function () {

    var zTreeObj;
    var setting = {
        edit: {
            enable: true,
            showRenameBtn: false,
            showRemoveBtn: false,
            drag: {
                isCopy: true,
                isMove: false
            }
        },
        check: {
            enable: false,
            chkStyle: "checkbox"
        },
        async: {
            enable: true,
            url: "Index.aspx",
            autoParam: ["id", "icon"], //双击父节点传递的参数       带此参数异步，不带同步
            
            dataFilter: filter
        },
        callback:
           {
               onDrop: zTreeOnDrop,            //用于捕获节点拖拽操作结束的事件回调函数
               onDrag: zTreeOnDrag,            //用于捕获节点被拖拽的事件回调函数
               beforeDrop: zTreeBeforeDrop,  //用于捕获节点拖拽操作结束之前的事件回调函数，并且根据返回值确定是否允许此拖拽操作
               beforeDrag: zTreeBeforeDrag
               //onRightClick: zTreeOnRightClick   // 用于捕获节点被右击的事件回调函数
           },
        view: {
            fontCss: getFontCss
        },
        data: {
            simpleData: {
                enable: true
            }
        }
    };

    var root = [{ id: "-2", pId: "0", name: "分组管理", isParent: true, type: "root", treeType: "groupTree"}];
    zTreeObj = $.fn.zTree.init($("#groupTree"), setting, root);
    zTreeObj.expandNode(zTreeObj.getNodeByParam("id", "-2"), true, false);
});
