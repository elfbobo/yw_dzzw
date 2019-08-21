
//**************************角色树
var roleTree;

var setting = {
    check: { enable: true },
    view: { fontCss: getFontCss }
};

var settingR = {
    check: { enable: true },
    view: { fontCss: getFontCss },
    callback: {

        onClick: onClickTree,
        onCheck: zTreeOnCheck

    }
};

$(document).ready(function () {
    roleTree = $.fn.zTree.init($("#RoleTree"), settingR, roleJson); //初始化角色菜单树

});

function onClickTree(e, treeId, treeNode) {
    // $('#viewRF').attr("src", "ViewLicenses.aspx?id=" + treeNode.id);
    // $('#viewR').window("open");

}

//jwj 2014-08-06
function getUnionMenuGuid() {
    var value = "";
    $.ajax({
        url: '../Handlers/AuthorityOpt.ashx',
        type: 'Post',
        aysnc:false,
        data: { MenuRoleXML: arguments[0], opt: "Menu" },
        success: function (data) {
            value = data;
        },
        error: function () {
        }
    });

    return value;
}

function zTreeOnCheck(e, treeId, treeNode) {
    resetTree(menuTree);
    var roleXML = getSelectNodesTop(roleTree);

    roleXML = roleXML ? roleXML : "";
    if (roleXML != "") {
        menuTree.checkAllNodes(false);
      //  moduleTree.checkAllNodes(false);
        //var guids = Authority.GetUnionMenuGuid(roleXML).value;//jwj 2014-08-06
        var guids = '';//getUnionMenuGuid(roleXML);
        $.ajax({
            url: '../Handlers/AuthorityOpt.ashx',
            type: 'Post',
            aysnc: false,
            data: { MenuRoleXML: roleXML, opt: "Menu" },
            success: function (data) {
                guids = data;
                var menuGuid = guids.split('&')[0];
                var modelGuid = guids.split('&')[1];
                if (menuGuid != "") {
                    var arrGuid = menuGuid.split('^');
                    for (var i = 0; i < arrGuid.length; i++) {
                        var node = menuTree.getNodeByParam("id", arrGuid[i]);
                        if (node) {
                            node.highlight = true;
                            menuTree.updateNode(node);
                        }
                    }
                }


                if (modelGuid != "" && modelGuid != undefined) {
                    var arrGuid = modelGuid.split('^');
                    for (var i = 0; i < arrGuid.length; i++) {
                        var node = moduleTree.getNodeByParam("id", arrGuid[i]);
                        if (node) {
                            node.checked = true;
                            moduleTree.updateNode(node);
                        }
                    }
                }
            },
            error: function () {
            }
        });
       
    }
    else {
        menuTree.checkAllNodes(false);
        //moduleTree.checkAllNodes(false);
    }
}
//**************************菜单树

var menuTree;

var menuSetting = {
    check: { enable: true },
    view: { fontCss: getFontCss }
};

$(document).ready(function () {
    menuTree = $.fn.zTree.init($("#MenuTree"), menuSetting, muenJosn);  //初始化菜单树

});


//**************************模块Tree




//$(document).ready(function () {
//    $.fn.zTree.init($("#moduleTree"), moduleSetting, moudleJson);  //初始化菜单树

//});


var moduleTree;

var moduleSetting = {
    check: { enable: true },
    view: { fontCss: getFontCss }
};

//$(document).ready(function () {
  //  moduleTree = $.fn.zTree.init($("#moduleTree"), moduleSetting, moudleJson);  //初始化菜单树

//});



