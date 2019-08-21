$(document).ready(function () {
   // resize();
    // $('#slectModule,:radio').uniform();


    $('#permit').bind("click", function () {
        if (confirm("此操作将授权选择的【菜单】的权限\n确定要授权吗")) {
            resultAction("1");
        }
    });
    $('#refuse').bind("click", function () {
        if (confirm("此操作将拒绝选择的【菜单】的权限\n确定要执行拒绝操作吗")) {
            resultAction("0");
        }
    });
    $('#del').bind("click", function () {
        if (confirm("此操作将删除选择的【菜单】的权限\n确定要删除权限吗,"))
        { resultAction("3"); }
    });

});



function searchTree(obj, name) {
    var value = $(obj).prev().val();
    if (name == "role") {
        searchNodes(roleTree, value);
        
        //searchNodes(sRole, value);
    }
    if (name == "menu") {
        searchNodes(menuTree, value);
    }
    if (name == "modle") {
        searchNodes(moduleTree, value);
    }

}


function resultAction(action) {

    var roleXML;
    var menuXML;
    var moduleXML;
    if (action == "1") {
        roleXML = getSelectNodes(roleTree);
        menuXML = getSelectNodes(menuTree);
       // moduleXML = getSelectNodes(moduleTree);
    }
    else {
        roleXML = getSelectNodesTop(roleTree);
        menuXML = getSelectNodesTop(menuTree);
       // moduleXML = getSelectNodesTop(moduleTree);
    }
    roleXML = roleXML ? roleXML : "";

    menuXML = menuXML ? menuXML : "";

    moduleXML = moduleXML ? moduleXML : "";

    if (roleXML == "") {
        alert("请选择角色"); return;
    }

    if (menuXML == "" && moduleXML == "") {
        alert("请选择菜单或模块"); return;
    }

    //Authority.Licenses(roleXML, menuXML, moduleXML, action);//jwj 2014-08-06
   
    licenses(encodeURI(roleXML), encodeURI(menuXML), moduleXML, action)
    alert("操作成功");
    document.location.reload();

}

function licenses() {
    $.ajax({
        url: '../Handlers/AuthorityOpt.ashx',
        type: 'Post',
        data: { RoleXML:arguments[0],MenuXML:arguments[1],ModuleXML:arguments[2],Action:arguments[3],opt:"License" },
        success: function (data) {
        },
        error: function (s) {
            
        }
    });
}


//*****************************************
