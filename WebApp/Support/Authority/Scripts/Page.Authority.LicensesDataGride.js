

function setTreeGrid(mid) {
    var ajaxUrl = "ListLicenses.aspx?mid=" + mid;
    $('#function').treegrid({

        width: "auto",
        height: "auto",

        rownumbers: true,
        animate: true,
        collapsible: true,
        singleSelect: false,
        loadMsg: "正在加载数据，请稍后...",
        url: ajaxUrl,
        idField: 'id',
        treeField: 'id',
        frozenColumns: [[
                    {
                        field: 'ck', checkbox: true
                    },
	                {
	                    title: 'Code', field: 'id', width: 150, formatter: function (value, rowData) {
	                        return rowData.Code;
	                    }
	                },
                    { field: 'Name', title: '菜单名称', width: 150 },
                    { field: 'ModuleCode', title: '模块号', width: 300 }
        ]],

        onClickRow: function (row) {

            if (row.Code == row.ModuleCode && row.Code == row.Name) {
                $('#function').treegrid("unselect", row.id);
            }
        },
        onLoadSuccess: function (row, data) {
            var roleXML = getSelectNodes(roleTree);
            roleXML = roleXML ? roleXML : "";
            var funcode = ListLicenses.GetUnionFunId(roleXML, $('#slectModule').attr("mid")).value;

            if (funcode != "" && funcode.indexOf("^") != -1) {
                var arr = funcode.split('^');
                $.each(arr, function (i, o) {
                    try {
                        $('#function').treegrid("select", o);
                    }
                    catch (e) { }
                });
            }
        }

    });

}

function GetFunSelect() {
    var funXml = [""];
    try {
        var rows = $('#function').treegrid("getSelections");
        if (rows.length > 0) {

            funXml.push("<xml>");
            $.each(rows, function (i, row) {
                if (row.Code != row.ModuleCode && row.Code != row.Name)
                    funXml.push("<row code='" + row.Code + "' mid='" + row.ModuleCode + "'/>");
            });
            funXml.push("</xml>");
        }
    } catch (e) { }
    return funXml.join('');
}

//*********************************************

var cfg = {};
cfg.idField = "Code";
cfg.url = "../../Services/ListDataHandler.ashx";
cfg.sortName = "Code";
cfg.sortOrder = "asc";
cfg.connectionName = "";
cfg.connectionString = "";
cfg.providerName = "";
cfg.tableName = "Sys_Modules";
cfg.condition = encodeURI("");
cfg.href = "";
cfg.Formatter = function (value) {
    if (cfg.href != "")
        return "<a href=\"" + cfg.href + "\">" + value + "</a>";
    else
        return value;
};
cfg.frozenColumns = [[
    { field: "Identity", hidden: true },
    { title: "模块号", field: "Code", width: 200, sortable: true, formatter: cfg.Formatter },
    { title: "页面名称", field: "Name", width: 300, sortable: true, formatter: cfg.Formatter, editor: { type: "validatebox", options: { required: true, missingMessage: "内容不能为空" } } }

]];

cfg.toolbar = [{
    text: "增加",
    iconCls: "icon-add",
    handler: function () {
        if (!$('#slectModule').attr("mid")) {
            alert("请选择模块"); return;
        }
        $("#table").datagrid("appendRow", {
            Code: $('#slectModule').attr("mid"),
            Name: ""

        });
        lastIndex = $("#table").datagrid("getRows").length - 1;
        $("#table").datagrid("beginEdit", lastIndex);
    }
}, "-", {
    text: "删除",
    iconCls: "icon-remove",
    handler: function () {
        var row = $("#table").datagrid("getSelections");
        if (row.length > 0) {
            $.messager.confirm('确认消息', '确认要删除选择的内容吗？', function (r) {
                if (r) {
                    var s = [];
                    for (var i = 0; i < row.length; i++) {
                        $("#table").datagrid("deleteRow", $("#table").datagrid("getRowIndex", row[i]));
                    };

                }
            });
        }
        else {
            $.messager.alert("提示消息", "没有选择要删除的失败");
        }
    }

}];

$(function () {

    $("#table").datagrid({
        idField: cfg.idField,
        url: cfg.url,
        fitColumns: true,
        nowrap: true,
        striped: true,
        loadMsg: "正在加载数据，请稍后...",
        width: "auto",
        height: "auto",
        remoteSort: true,
        rownumbers: true,
        frozenColumns: cfg.frozenColumns,
        toolbar: cfg.toolbar,
        onDblClickRow: function (rowIndex) {
            $("#table").datagrid("beginEdit", rowIndex);
            $("#table").datagrid('getEditors', rowIndex)[0].target.attr("readonly", "true").css("background-color", "#CCCCCC");
            lastIndex = rowIndex;
        }
    });
});


function GetPageData() {
    var xml = "";
    try {
        var rows = $("#table").datagrid("getRows");
        for (var i = 0; i < rows.length; i++) {
            $("#table").datagrid("endEdit", i);
        }

        if ($("#table").datagrid("getChanges").length) {

            xml += "<xml>";
            var inserted = $('#table').datagrid('getChanges', "inserted");
            for (var i = 0; i < inserted.length; i++) {
                xml += "<row code=\"" + inserted[i].Code + "\" name=\"" + inserted[i].Name + "\"  />";
            }
            xml += "</xml>";
        }
    } catch (e) { }
    return xml;
}


//*********************
