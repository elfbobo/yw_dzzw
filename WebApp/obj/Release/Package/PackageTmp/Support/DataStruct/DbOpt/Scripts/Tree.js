var zTreeObj;
var setting = {
    check: {
        enable: true,
        chkboxType: { "Y": "ps", "N": "ps" },
        chkStyle: "checkbox"
    },
    async: {
        enable: true,
        url: "DataColumns.aspx",
        otherParam: { "type": "get", "Table": table, "ModelGuid": modelGuid },
        dataFilter: filter
    }, view: {
        fontCss: getFontCss,
        selectedMulti: false
    }
};

$(function () {
    var root = [{ id: "-1", pId: "0", name: "字段名称", isParent: true }];
    zTreeObj = $.fn.zTree.init($("#ztree"), setting, root);
    zTreeObj.expandNode(zTreeObj.getNodeByParam("id", "-1"), true, false);
});

function filter(treeId, parentNode, childNodes) {
    if (!childNodes) return null;
    return childNodes;
}

function getFontCss(treeId, treeNode) {
    return (!!treeNode.highlight) ? { color: "#A60000", "font-weight": "bold" } : { color: "#333", "font-weight": "normal" };

}

function Save() {
    var treeObj = $.fn.zTree.getZTreeObj("ztree");
    var nodes = treeObj.getCheckedNodes(true);
    var arr = [];
    if (nodes.length < 1) {
        alert("请选择字段");
        return;
    }
    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].pId != 0)
            arr.push(nodes[i].value);
    }
    window.returnValue = arr.join(",");
    self.close();
}

function Close() {
    self.close();
}