//异步加载
var zTreeObj;
var treenode;
var formerPid; //原来parentid
var isusercaninmoregroup = false;//用户是否可以在多个分组下 默认情况下一个用户只能在一个分组下
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
        url: "List.aspx",
        autoParam: ["id", "icon"], //双击父节点传递的参数       带此参数异步，不带同步
        dataFilter: filter
    },
    callback:
           {
               onClick: zTreeonClick,
               onDblClick: zTreeOnDblClick,   //双击节点触发的函数
               //               beforeRename: zTreeBeforeRename, //修改名称之前触发
               //               beforeRemove: zTreeBeforeonRemove, //删除节点之前触发
               onRename: zTreeOnRename,        //重命名触发函数
               //               onRemove: zTreeOnRemove,
               onDrop: zTreeOnDrop,            //用于捕获节点拖拽操作结束的事件回调函数
               onDrag: zTreeOnDrag,            //用于捕获节点被拖拽的事件回调函数
               beforeDrop: zTreeBeforeDrop,    //用于捕获节点拖拽操作结束之前的事件回调函数，并且根据返回值确定是否允许此拖拽操作
               onRightClick: zTreeOnRightClick
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
//右键触发函数
function zTreeOnRightClick(event, treeId, treeNode) {
    if (treeNode) {
        treenode = treeNode;
        zTreeObj.selectNode(treeNode);
        showRMenu(treeNode.type, event.clientX, event.clientY,event);
    }
};
//为查询设置字体
function getFontCss(treeId, treeNode) {
    return (!!treeNode.highlight) ? { color: "#A60000", "font-weight": "bold" } : { color: "#333", "font-weight": "normal" };

}
function filter(treeId, parentNode, childNodes) {//加载子节点
    if (!childNodes) return null;
    return childNodes;
}
//点击用户节点处理函数
function zTreeonClick(event, treeId, treeNode) {
    
    if (treeNode != null && treeNode.type == "user") {
        
        showpropertygrid(treeNode.id, "user", "");
    }
    else if (treeNode != null && treeNode.type == "group") {
   
        showpropertygrid(treeNode.id, "group", treeNode.pId);
    }
   // $("#pg").parents("div").height($("#tree").height());
}
//双击节点后处理函数
function zTreeOnDblClick(event, treeId, treeNode) {

    //           setEdit();
}
//重命名触发的函数
function zTreeOnRename(event, treeId, treeNode) {

    //           if (treeNode.type == "user") {
    //               alert("用户名不可修改");
    editByAjax(treeNode.id, treeNode.pId, treeNode.name, "update");
    return true;
    //           }
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
//删除触发的函数
function zTreeOnRemove(event, treeId, treeNode) {
    if (treeNode.type == "group")//删除分组
    {
        var treeObj = $.fn.zTree.getZTreeObj("tree");
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
        editByAjax(userguid.join(','), groupguid.join(','), treeNode.name, "delete", treeNode.type);
    }
    else {
        editByAjax(treeNode.id, treeNode.pId, treeNode.name, "delete", treeNode.type);
    }
    return true;
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
//用于捕获节点拖拽操作结束之前的事件回调函数，并且根据返回值确定是否允许此拖拽操作

function zTreeBeforeDrop(treeId, treeNodes, targetNode, moveType) {

    if (targetNode.type == "user" && moveType == "inner") {
        alert("不可拖动到用户下");
        return false;
    }
    for (var i = 0; i < treeNodes.length; i++) {
        if (treeNodes[i].type == "user" && targetNode.id == "-1") {
            alert("用户不可放到根节点");
            return false;
        }
    }
    formerPid = treeNodes[0].pId; //原来parentid
    return !(targetNode == null || (moveType != "inner" && targetNode.id == "-1"));  //禁止成为根节点

}

//用于捕获节点拖拽操作结束的事件回调函数
function zTreeOnDrop(event, treeId, treeNodes, targetNode, moveType) {

    var num = treeNodes.length; //被拖拽节点的个数           
    var userguid = new Array();;
    var groupguid = new Array();

    for (var i = 0; i < num; i++) {
        if (treeNodes[i].type == "user") {
            userguid[userguid.length] = "'" + treeNodes[i].id + "'";
        }
        else {
            groupguid[groupguid.length] = "'" + treeNodes[i].id + "'";
        }

    }
    if (userguid.length > 0) {
        editByAjax(userguid.join(','), targetNode.id, targetNode.pId, "drop", "user", moveType, formerPid);
    }
    if (groupguid.length > 0) {
        editByAjax(groupguid.join(','), targetNode.id, targetNode.pId, "drop", "group", moveType);
    }
}
function zTreeOnDrag(event, treeId, treeNodes) {

}
function setEdit() {                //设置后面带有可编辑删除图标  暂时没用 被edit下方法取代
    var zTree = $.fn.zTree.getZTreeObj("tree");
    //           var nodes = zTree.getNodesByParam("type", "group", null);
    zTree.setting.edit.showRemoveBtn = true;
    zTree.setting.edit.showRenameBtn = true;
    zTree.setting.edit.removeTitle = "删除";
    zTree.setting.edit.renameTitle = "重命名";
}
$(function () {

    var root = [{ id: "-1", pId: "0", name: "用户管理", isParent: true, type: "root" }];
    zTreeObj = $.fn.zTree.init($("#tree"), setting, root);
    zTreeObj.expandNode(zTreeObj.getNodeByParam("id", "-1"), true, false);
    $(window.parent.document).find("iframe").height(window.document.body.scrollHeight + 20);//重新计算高度
    // 重置;
    $('#reset').click(function () { $('#username').val(""); $('#groupname').val(""); });
    //添加工作组处理函数
    $('#addgroup').click(function () {
        var treeObj = $.fn.zTree.getZTreeObj("tree");
        var guid = Guid.NewGuid().ToString();
        var newnode = treeObj.addNodes(treenode, { id: guid, pId: treenode.id, name: "新分组", type: 'group', icon: 'Images/group.png' });
        treeObj.editName(newnode[0]);
        editByAjax(guid, treenode.id, "新分组", "add");
        $('#rMenu').hide();
    }
           );

    //工作组添加AD用户处理函数
    $('#addADUser').click(function () {
        var result = chooseUsers("AD");
        if (result != null) {
            result = result.split('&');
            addusernode(result);
        }
        $('#rMenu').hide();
    });
    //工作组添加DB用户处理函数
    $('#addDBUser').click(function () {
        var result = chooseUsers("DB");
        if (result != null) {
            result = result.split('&');
            addusernode(result);
        }
        $('#rMenu').hide();
    });
    //工作组添加DB用户处理函数
    $('#addLocalhostUser').click(function () {
        var result = chooseUsers("LOCALHOST");
        if (result != null) {
            result = result.split('&');
            addusernode(result);
        }
    });

    //接受参数往组上添加用户
    function addusernode() {
        var result = arguments[0];
        if (result.length > 1) {
            var adduserssql = "";//更新用户表语句
            var addusersAndGroupsql = ""; //更新用户和用户组关联表语句
            var treeObj = $.fn.zTree.getZTreeObj("tree");
            var groupid = treenode.id;//分组主键
            var userid = "(";//用户主键
            for (var i = 0; i < result.length;) {

                if (IsUserChangeGroup(result[i], result[i + 4], isusercaninmoregroup)) {//判断用户是否在其他分组下并且要移植到当前分组下
                    if (adduserssql.length > 0) {
                        adduserssql += "  union all  ";
                        addusersAndGroupsql += "  union all  ";
                        userid += ",";
                    }
                    userid += "'" + result[i] + "'";
                    adduserssql += " '" + result[i] + "','" + result[i + 1] + "','" + result[i + 2] + "','" + result[i + 3] + "','" + result[i + 4] + "'";
                    addusersAndGroupsql += " '" + result[i] + "','" + treenode.id + "'";
                    var addusernode = { id: result[i], pId: treenode.id, name: result[i + 4], type: 'user', icon: 'Images/user.png' };
                    treeObj.addNodes(treenode, addusernode);
                }

                i += 5;

            }
            //            if (adduserssql.length > 0) {
            userid += ")";
            saveAddUsers(adduserssql, addusersAndGroupsql, groupid, userid, isusercaninmoregroup);
            //List.SaveAddUsers(adduserssql, addusersAndGroupsql, groupid, userid,isusercaninmoregroup);//jwj 2014-08-06
            //            }
        }
    }

    function saveAddUsers() {
        $.ajax({
            url: 'DbOpt.ashx',
            type: 'Post',
            data: { users: arguments[0], addusersAndGroupsql: arguments[1], groupid: arguments[2], userid: arguments[3], isusercaninmoregroup: arguments[4] },
            success: function (data) {
            },
            error: function () {
                alert('添加失败');
            }

        });
    }


    //判断用户是不是已经在其他分组并判断是否改变分组
    function IsUserChangeGroup(userguid, username, isusercaninmoregroup) {
        if (isusercaninmoregroup) {//一个用户是否可以在多个分组下
            return true
        }
            //一个用户只可以在一个分组下
        else {
            var userguid = arguments[0];
            var treeObj = $.fn.zTree.getZTreeObj("tree");
            var node = treeObj.getNodesByParam("id", userguid, null);
            if (node.length > 0) {
                if (confirm("用户：" + arguments[1] + "  已经在分组：" + treeObj.getNodesByParam("id", node[0].pId, null)[0].name + "  下,是否要更改到当前分组下？")) {
                    treeObj.removeNode(node[0]);//移除用户
                    return true;
                }
                else {
                    return false;
                }

            }
            else {
                return true;
            }
        }

    }
    //节点重命名

    $('#renamenode').click(function () {

        var treeObj = $.fn.zTree.getZTreeObj("tree");
        treenode = treeObj.editName(treenode);
        $('#rMenu').hide();
    });
    //节点删除
    $('#nodedetele').click(function () {
        var treeObj = $.fn.zTree.getZTreeObj("tree");
        if (treenode.type == "group")//删除分组
        {
            if (confirm("确定要删除分组:" + treenode.name + " 及其下面的用户？")) {
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
                editByAjax(userguid.join(','), groupguid.join(','), treenode.name, "delete", treenode.type);
                treeObj.removeNode(treenode);
            }

        }
        else {
            if (confirm("确定要删除用户:" + treenode.name + "？")) {
                editByAjax(treenode.id, treenode.pId, treenode.name, "delete", treenode.type);
                treeObj.removeNode(treenode);
            }
        }
        $('#rMenu').hide();
        return true;
    });
    $('#treesearch').click(function () {   //查询
        var highlight = true;
        var treeObj = $.fn.zTree.getZTreeObj("tree");
        //查询之前取消所有标明特殊字体的节点
        var nodelist = treeObj.getNodesByParam("highlight", true, null);
        for (var j = 0; j < nodelist.length; j++) {
            nodelist[j].highlight = false;
            treeObj.updateNode(nodelist[j]);
        }
        var username = $('#username').val().toLowerCase();
        var groupname = $('#groupname').val().toLowerCase();
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
        var opennodes = treeObj.getNodesByParam("open", true);
        $.each(opennodes, function (i, node) {
            if (treeObj.getNodesByParam("highlight", true, node).length <= 0 && node.id != "-1") {
                treeObj.expandNode(node, false, false, true);
            }
        });

    });


});
//ajax通用方法
function editByAjax() {

    $.ajax({
        url: 'List.aspx',
        type: 'Post',
        data: { guid: arguments[0], parentguid: arguments[1], name: arguments[2], type: arguments[3], nodetype: arguments[4], moveType: arguments[5], formerPid: arguments[6]},
        dataType: 'text',
        success: function (data) {

        },
        error: function () {
            //alert('保存失败');
        }

    });
}


//显示属性列表
function showpropertygrid(guid,type,pid) {
    //$('#pg').propertygrid('options').url = "List.aspx?propertygridGuid=" + arguments[0] + "&type=" + arguments[1] + "&propertynodePid=" + arguments[2];
    //$('#pg').propertygrid('reload');
    var json;
    $.ajax({
        url: 'List.aspx',
        type: 'Post',
        data: { sguid: guid, pid: pid, type: type },
        async: false,
        success: function (data) {
           json =eval( data);
        }
    });
    var confg = [];
    confg.width = '98%';
    confg.height = 400;
    confg.columns = json;
    confg.EditComplete = function (value, filed, ele) {
        $.ajax({
            url: 'List.aspx',
            type: 'Post',
            data: { guid: guid, value: value, type: "area", filed: filed },
            async: false
        });
    }
 
    $("#pg").Property(confg);
}


//显示右键菜单
function showRMenu(type, x, y,event) {
     x = $(event.target).offset().left + $(event.target).width();
    $('#rMenu').css({ left: x, top: y-5 }).show();
    $('#addgroup').hide();
    $('#addADUser').hide();
    $('#addDBUser').hide();
    $('#renamenode').hide();
    $('#nodedetele').hide();
    if (treenode.type == "root") {
        $('#addgroup').show();
    }
    else if (treenode.type == "group") {
        $('#addgroup').show();
        $('#addADUser').show();
        $('#addDBUser').show();
        $('#renamenode').show();
        $('#nodedetele').show();
    }
    else {
        //        $('#renamenode').show();
        $('#nodedetele').show();
    }
}

