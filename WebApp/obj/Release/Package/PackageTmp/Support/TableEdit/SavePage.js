function Save() {
    getPagesPath();
}

function onClickPTree(e, treeId, treeNode) {
    if (treeNode.name != 'WebApp') {

        $('#pathPage').val(treeNode.path);
        var node = treeNode; var space = '';
        if (node.name != 'WebApp')
            space = node.name;
        while (true) {
            node = node.getParentNode();
            if (node.name == 'WebApp')
                break;
            else {
                if (space == '')
                    space = node.name;
                else
                    space = node.name + "." + space;
            }
        }


        var listJosn = SaveList();
        var listPage = new ListPage(listJosn);
        listPage.CreateList();

        var layoutJson = SaveEdit();

        var editPage = new CreatePage(layoutJson);
        editPage.CreateEdit(layoutJson);

        var viewPage = new ViewPage(layoutJson);
        viewPage.CreateView();

        $('[id*="list"],[id*="create"],[id*="view"]').each(function (i) {
            $(this).val(escape($(this).val().replace("@@", "Yawei.App." + space)));
        });


        $.ajax({
            url: '../Handlers/TableEdit.ashx',
            type: "post",
            data: { type: "Save", TableName: listJosn[0].tabelName, List: JSON.stringify(listJosn), Edit: JSON.stringify(layoutJson) },
            dataType: "text",
            success: function (data) {
                $("#Content_Create").click();
            },
            error: function ()
            { alert("请求数据失败"); }
        });
    }
}

function getPagesPath() {
    $.ajax({
        url: "Handler.ashx",
        type: "post",
        data: { tableName: this.tableName, type: "path" },
        dataType: "text",
        async: false,
        success: function (data) {

            var psetting = {
                callback: {
                    onClick: onClickPTree
                }
            };
            $.fn.zTree.init($("#pathtree"), psetting, eval(data));
            var offset = $("#page").offset();
            $('#win').css({ left: offset.left - 280, top: offset.top + 30 }).show(400);
        },
        error: function ()
        { alert("请求数据失败"); }
    });
}


function GetPamas() {
    var nextTr = $("#pamas").nextAll();
    var pamas = [];
    $.each(nextTr, function (i, dat) {
        var c = $(this).find("select,input");
        if (c.eq(0).val() == "Receive") {
            pamas.push({ type: c.eq(0).val(), value: c.eq(2).val() });
        }
        else {
            pamas.push({ type: c.eq(0).val(), action: c.eq(1).val(), value: c.eq(2).val() });
        }
    });
    return pamas;
}

//***********************************加载LIST*****************************
function SaveList() {
    //JSON格式  [{fun:[],Search:[],List:[PageCount:5,IsChecked:true,Column:[]]}]
    var fun = [];
    $("#gridfun :checkbox").each(function (i) {
        if (i < 2)
            fun.push({ cn: $(this).attr("title"), en: $(this).val() });
        else
            fun.push({ cn: $(this).attr("title"), en: $(this).val(), img: $(this).attr("img") });
    });
    var search = [];
    $("#searchTable td").each(function () {
        if ($(this).children().length > 0) {

            var data = JSON.parse($(this).attr("data"));
            if (data.Mapping) {
                search.push({ Empty: false, FieldDesc: data.FieldDesc, FieldName: data.FieldName, Action: $(this).find("select[search]").val(), FieldDataType: data.FieldDataType, control: data.control, valvalidate: data.valvalidate, nullvalidate: data.nullvalidate, Mapping: data.Mapping });
            }
            else if (data.Custom) {
                search.push({ Empty: false, FieldDesc: data.FieldDesc, FieldName: data.FieldName, Action: $(this).find("select[search]").val(), FieldDataType: data.FieldDataType, control: data.control, valvalidate: data.valvalidate, nullvalidate: data.nullvalidate, Custom: data.Custom });
            }
            else
                search.push({ Empty: false, FieldDesc: data.FieldDesc, FieldName: data.FieldName, Action: $(this).find("select[search]").val(), FieldDataType: data.FieldDataType, control: data.control, valvalidate: data.valvalidate, nullvalidate: data.nullvalidate });
        }
        else
            search.push({ Empty: true });

    });
    var list = [];
    var isChecked = false;

    if ($("#checkall").prop("checked")) {
        isChecked = true;
    }

    var column = [];
    var wd = $("#grid").width();

    $("#grid .tableHeader").each(function (i) {
        if (i > 0) {
            var dat = JSON.parse($(this).attr("data"));
            dat.width = ($(this).width() / wd * 100).toString().split('.')[0];
            column.push(dat);
        }
    });
    list.push({ sort: $("#sortSelect").val(), PageCount: $(".zttitle select").val(), IsChecked: isChecked, Column: column });

    var jsonList = [{ tabelName: tabelName, tableTitle: tableTitle, Search: search, List: list, pamas: GetPamas(), fun: fun }];

    var listPage = new ListPage(jsonList);
    listPage.CreateList();
    return jsonList;

}


function SaveEdit() {

    var fun = [];
    $("#laytoufun :checkbox").each(function (i) {
        if (i < 3)
            fun.push({ cn: $(this).attr("title"), en: $(this).val() });
        else
            fun.push({ cn: $(this).attr("title"), en: $(this).val(), img: $(this).attr("img"), p: $(this).attr("p") });
    });

    var layout = [];
    $("#LayoutTable td").each(function () {
        if (!$(this).parent().attr("table")) {
            var colspan = 1;
            if ($(this).attr("colspan"))
                colspan = 2;
            if ($(this).find("div[f]").children().length > 0) {

                var data = JSON.parse($(this).attr("data"));

                if (data.Mapping) {
                    layout.push({ Empty: false, colspan: colspan, FieldDesc: data.FieldDesc, FieldLength: data.FieldLength, FieldName: data.FieldName, Action: $(this).find("select[search]").val(), FieldDataType: data.FieldDataType, control: data.control, valvalidate: data.valvalidate, nullvalidate: data.nullvalidate, Mapping: data.Mapping, FieldValueDefault: data.FieldValueDefault });
                }
                else if (data.Custom) {
                    layout.push({ Empty: false, colspan: colspan, FieldDesc: data.FieldDesc, FieldLength: data.FieldLength, FieldName: data.FieldName, Action: $(this).find("select[search]").val(), FieldDataType: data.FieldDataType, control: data.control, valvalidate: data.valvalidate, nullvalidate: data.nullvalidate, Custom: data.Custom, FieldValueDefault: data.FieldValueDefault });
                }
                else
                    layout.push({ Empty: false, colspan: colspan, FieldDesc: data.FieldDesc, FieldLength: data.FieldLength, FieldName: data.FieldName, Action: $(this).find("select[search]").val(), FieldDataType: data.FieldDataType, control: data.control, valvalidate: data.valvalidate, nullvalidate: data.nullvalidate, FieldValueDefault: data.FieldValueDefault });
            }
            else
                layout.push({ Empty: true, colspan: colspan });
        }
        else {
            var tddata = eval("[" + $(this).attr("data") + "]");
            layout.push(tddata[0])
        }
    });

    var file = [];
    $("#filerows").nextAll().each(function () {
        var input = $(this).find("input");
        file.push({ title: input.eq(0).val(), sign: input.eq(1).val() });
    });


    var layoutJson = { tabelName: tabelName, tableTitle: tableTitle, fun: fun, layout: layout, file: file, pamas: GetPamas() };
    return layoutJson;
}




