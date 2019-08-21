function filter(node, name) {
    if (name == "")
        return;
    name = $.trim(name).toLowerCase();
    return (node.name.toLowerCase().indexOf(name) > -1);
}

function resetTree(tree) {
    var nodes = tree.getNodesByParam("highlight", true);

    $.each(nodes, function (i, node) {
        node.highlight = false;
        tree.updateNode(node);
    });

}

function searchNodes(tree, name) {
    // tree.expandAll(false);
    resetTree(tree);
    var nodes = tree.getNodesByFilter(filter, false, null, name);

    $.each(nodes, function (i, node) {
        if (node) {

            node.highlight = true;
            tree.updateNode(node);

            node = node.getParentNode();

            if (node) {
                tree.expandNode(node, true, false, true);
            }
        }
    });
    expandTree(tree);
}

function getFontCss(treeId, treeNode) {
    return (!!treeNode.highlight) ? { color: "#A60000", "font-weight": "bold" } : { color: "#333", "font-weight": "normal" };
}


function expandTree(tree) {
    var nodes = tree.getNodesByParam("open", true);
    $.each(nodes, function (i, node) {
        //alert(tree.getNodesByParam("highlight", true, node).length)
        if (tree.getNodesByParam("highlight", true, node).length <= 0) {
            tree.expandNode(node, false, false, true);
        }

    });
}



function getSelectNodes(tree) {
    var nodeXML = [];
    var nodes = tree.getCheckedNodes(true);
    if (nodes.length > 0) {
        nodeXML.push('<xml>');
        $.each(nodes, function (i, node) {
            nodeXML.push('<role guid="' + node.id + '" name="' + node.name + '" />');
        });

        nodeXML.push('</xml>');
    }
    return nodeXML.join('');
}


function getSelectNodesTop(tree) { //
    var nodeXML = [];
    var nodes = tree.getCheckedNodes(true);
    if (nodes.length > 0) {

        nodeXML.push('<xml>');
        $.each(nodes, function (i, node) {
            var b = true;
            if (node.isParent) {
                var childNode = node.children;
                $.each(childNode, function (i, cn) {
                    if (cn.checked == false) {
                        b = false;

                    }
                });
            }
            if (b)
                nodeXML.push('<role guid="' + node.id + '" name="' + node.name + '" />');
        });

        nodeXML.push('</xml>');
    }
    return nodeXML.join('');
}