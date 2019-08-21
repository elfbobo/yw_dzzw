<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataColumns.aspx.cs" Inherits="Yawei.App.Support.DataStruct.DbOpt.DataColumns" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../../Plugins/jquery.min.js"></script>
    <script src="../../../Plugins/jquery-ztree/ztree-all.min.js"></script>
    <link href="../../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
     <style type="text/css">
        .headbutton2
        {
            float:right;
            width:450px;
            text-align:right;
            margin-top:5px;
            margin-bottom:5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="btnSave" type="button" value="保存" style="margin-right:10px;" onclick="Save();" />
            <input id="btnClose" type="button" value="关闭" style="margin-right:10px;" onclick="Close();" />
        </div>
        <div style="height:400px;border:#99bbe8 1px solid;width:440px;overflow:hidden" align="center" >
            <div><ul id="ztree" class="ztree" style="width:98%;height:500px;overflow:auto;"></ul></div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    var zTreeObj;
    var setting = {
        check: {
            enable: true,
            chkboxType: { "Y": "ps", "N": "ps" },
            chkStyle: "checkbox"
        },view: {
            fontCss: getFontCss,
            selectedMulti: false
        }
    };

    $(function () {
        zTreeObj = $.fn.zTree.init($("#ztree"), setting,<%=json%>);
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
</script>