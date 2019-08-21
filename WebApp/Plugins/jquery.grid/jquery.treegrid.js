
(function ($) {
    $.fn.extend({
        TreeGrid: function (options) {
            var defaults = {
                sortName: "",
                order: "",
                ajaxUrl: "",
                rowsData: [],
                connectionName: "",
                connectionString: "",
                providerName: "",
                tableName: "",
                condition: "",
                pageCount: 0,
                columns: [],
                rowsData: [],
                width: "99%",
                height: 100,
                idField: "",
                page: 1,
                where: "",
                parentfield: "",
                childfield: "",
                childWhere: "",
                //是否关联查询
                relevanSearch: false,
                Pagination: false,
                pageSelect: [],
                Cascade: false,
                SelectedRows: function () { },
                CannelRows: function () { },
                BindBefore: function (data) { return data; }
            }
            var options = $.extend(defaults, options);
            return this.each(function () {
                var opt = options;
                var $gridcontent = $("<div />");//grid容器
                var $gridhead = $("<div />");//grid列头
                var $headtable = $("<table />");//列头表格
                var $datacontent = $("<div />");//grid数据容器
                var $datatable = $("<table />");

                $gridcontent = $(this);
                $gridcontent.children().remove();
                $gridcontent.width(opt.width).height(opt.height).addClass("gridcontent");
                $headtable.width("100%").attr({ "border": "0", "cellpadding": "0", "cellspacing": "0" });
                $gridhead.append($headtable.append(SetGridHeader(opt.columns)));
                $gridcontent.append($gridhead).append($datacontent);
                $datatable.width("100%").addClass("datagrid").attr({ "border": "0", "cellpadding": "0", "cellspacing": "0" });

                $datacontent.height($gridcontent.height() - 35 - 28).addClass("gridtablecontent").css({ "overflow-y": "auto" });
                $datacontent.append($datatable);
                var total = 0;
                if (opt.ajaxUrl == "") {
                    total = opt.rowsData.total;
                    SetGridData(opt.rowsData.rows, opt.columns);
                    //  $headtable.width($datatable.width());
                    if (opt.Pagination) {
                        pageSize();
                    }
                }
                else {
                    GetData(opt.idField, opt.ajaxUrl, opt.sortName, opt.order, opt.connectionName, opt.connectionString, opt.providerName, opt.tableName, opt.condition, opt.pageCount, opt.where, opt.columns, opt.page, opt.parentfield, opt.childfield, opt.childWhere, opt.relevanSearch);
                    //    $headtable.width($datatable.width());
                    if (opt.Pagination) {
                        pageSize();
                    }
                }

                if ($gridcontent.is(':hidden')) {

                    var init = setInterval('Window.SetWidth()', 1000);
                    $gridcontent.data("init", init);
                    Window.SetWidth = function () {
                        $(".gridcontent").each(function () {
                            if ($(this).data("init")) {
                                if ($(this).is(":visible")) {
                                    $(this).find("table").eq(0).width($(this).find("table").eq(1).width());
                                    clearInterval($(this).data("init"));
                                }
                            }
                        });
                    }
                }

                //ajax请求数据
                function GetData(idField, ajaxUrl, sort, order, connectionName, connectionString, providerName, tableName, condition, pageCount, where, columns, page, parentfield, childfield, childWhere, relevanSearch) {
                    $.ajax({
                        type: "POST",
                        url: ajaxUrl,
                        dataType: "json",
                        async: false,

                        data: { idField: idField, sort: sort, order: order, connectionName: connectionName, connectionString: connectionString, providerName: providerName, tableName: tableName, condition: encMe(condition, 'asdefzgereagjlg'), rows: pageCount, page: page, where: encMe(where, 'asdefzgereagjlg'), parentfield: parentfield, childfield: childfield, childWhere: encMe(childWhere, 'asdefzgereagjlg'), relevanSearch: relevanSearch },
                        success: function (data) {
                            $datatable.find("tr").remove();
                            var dataRow;
                            total = data.total;
                            dataRow = data.rows;

                            var newJson = opt.BindBefore(dataRow);
                            if (newJson) {
                                dataRow = newJson;
                            }

                            SetGridData(dataRow, columns);
                            //table.append(str);
                        },
                        error: function (data) {
                            alert("数据请求错误，错误类型：" + data.status);
                        }

                    });
                }


                //设置表头
                function SetGridHeader(columns) {
                    var tr = $("<tr />");
                    $.each(columns, function (i, obj) {
                        if (typeof (obj) != "undefined") {
                            var td = $("<td />");
                            td.addClass("gridHeader")
                            if (typeof (obj.width) != "undefined") {
                                td.css("width", obj.width);
                            }
                            if (obj.checkbox) {
                                $("<input />", { id: "checkall", type: "checkbox", name: "name" + i }).bind("click", function () { checkAll(this); }).appendTo(td);
                            }
                            else {

                                td.html(obj.name);
                            }
                            tr.append(td);
                        }
                    });
                    return tr;
                }



                function SetGridData(dataRow, columns) {

                    var level = 0;
                    $.each(dataRow, function (i, rowDat) {
                        var tr = $("<tr  />");

                        tr.hover(function () { $(this).addClass("griddatahover"); }, function () {
                            if ($(this).find(":checkbox,:radio").attr("checked") != "checked")
                                $(this).removeClass("griddatahover");

                        });
                        tr.data("data", rowDat);
                        $.each(columns, function (i, colDat) {
                            var td = $("<td />", { "class": "gridlabel" });
                            td.width(colDat.width);
                            if (colDat.field) {
                                for (key in rowDat) {

                                    if (colDat.field.toLowerCase() == key.toLowerCase()) {
                                        if (colDat.align) {
                                            td.css({ "text-align": colDat.align });
                                        }
                                        if (colDat.checkbox) {
                                            var input = $("<input />", { "type": "checkbox", "name": "name" + i }).val(rowDat[key]);
                                            input.click(function () {
                                                if ($(this).prop("checked")) {
                                                    opt.SelectedRows(rowDat);
                                                    tr.addClass("griddatahover");
                                                    if (opt.Cascade) {
                                                        if (tr.next().find("div").length > 0) {
                                                            tr.next().find("div").find(":checkbox").each(function () {
                                                                $(this).prop("checked", true);
                                                                $(this).parent().parent().addClass("griddatahover");
                                                            });
                                                        }
                                                    }
                                                }
                                                else {
                                                    opt.CannelRows(rowDat);
                                                    if (opt.Cascade) {
                                                        if (tr.next().find("div").length > 0) {
                                                            tr.next().find("div").find(":checkbox").each(function () {
                                                                $(this).prop("checked", false);
                                                                $(this).parent().parent().removeClass("griddatahover");
                                                            });
                                                        }


                                                        var ptable = $(this).parent().parent().parent();
                                                        while (true) {
                                                            var trs = ptable.find("tr");

                                                            if (ptable.is("tbody")) {
                                                                var istd = ptable.parent().parent().parent();
                                                                if (istd.is("td")) {
                                                                    ptable = istd.parent().parent();
                                                                    if (!istd.parent().prev().find(":checkbox").eq(0).prop("checked", false))
                                                                        break;
                                                                    istd.parent().prev().find(":checkbox").eq(0).prop("checked", false);
                                                                }
                                                                else
                                                                    break;
                                                            }

                                                            if (ptable.is("table")) {
                                                                var istd = ptable.parent().parent();
                                                                if (istd.is("td")) {
                                                                    ptable = istd.parent().parent();
                                                                    if (!istd.parent().prev().find(":checkbox").eq(0).prop("checked", false))
                                                                        break;
                                                                    istd.parent().find(":checkbox").eq(0).prop("checked", false);
                                                                }
                                                                else
                                                                    break;
                                                            }

                                                        }




                                                    }
                                                }
                                            });
                                            td.append(input);
                                        }
                                        else if (colDat.radiobox) {
                                            var input = $("<input />", { "type": "radio", "name": "name" + i }).val(rowDat[key]);
                                            input.click(function () {
                                                if ($(this).attr("checked") == "checked") {
                                                    opt.SelectedRows(rowDat);
                                                    $datatable.find(".griddatahover").removeClass("griddatahover");
                                                    tr.addClass("griddatahover");
                                                }
                                            });
                                            td.append(input);
                                        }
                                        else {
                                            if (colDat.collapsible) {
                                                if (rowDat.children) {
                                                    var span = $("<span />", { "class": "tree-expanded" });
                                                    span.css({ "margin-left": 18 * level, "cursor": "pointer" });
                                                    var folder = $("<span />", { "class": "tree-folder" });
                                                    td.append(span).append(folder);

                                                    span.click(function () {

                                                        if ($(this).attr("class") == "tree-collapsed") {
                                                            $(this).addClass("tree-expanded").removeClass("tree-collapsed");
                                                            $(this).next().addClass("tree-folder-open").removeClass("tree-folder");
                                                            tr.next().find("div").eq(0).hide();
                                                            tr.next().show();
                                                            tr.next().find("div").eq(0).slideDown("slow", function () {
                                                                $headtable.width($datatable.width());
                                                            });
                                                        }
                                                        else if ($(this).attr("class") == "tree-expanded") {
                                                            $(this).addClass("tree-collapsed").removeClass("tree-expanded");
                                                            $(this).next().addClass("tree-folder").removeClass("tree-folder-open");

                                                            tr.next().find("div").eq(0).slideUp("slow", function () {
                                                                tr.next().hide();
                                                                $headtable.width($datatable.width());
                                                            });

                                                        }


                                                    });
                                                }
                                                else {
                                                    var span = $("<span />", { "class": "tree-indent" });
                                                    span.css({ "margin-left": 18 * level, "cursor": "pointer" });
                                                    var folder = $("<span />", { "class": "tree-file" });
                                                    td.append(span).append(folder);
                                                }
                                                if (colDat.render && typeof (colDat.render) == "function") {
                                                    trStr = colDat.render(rowDat[key].replace('0:00:00', ''), rowDat, key);
                                                    td.append("<span class='tree-title'>" + trStr + "</span>");
                                                }
                                                else {
                                                    td.append("<span class='tree-title'>" + rowDat[key].replace('0:00:00', '') + "</span>");
                                                }
                                                // td.css("text-overflow", "clip");
                                            }
                                            else {
                                                if (colDat.render && typeof (colDat.render) == "function") {
                                                    trStr = colDat.render(rowDat[key].replace('0:00:00', ''), rowDat, key);
                                                    td.html(td.html() + trStr);
                                                }
                                                else {
                                                    td.html(rowDat[key].replace('0:00:00', ''));
                                                }
                                            }
                                            td.click(function (event) {

                                                if ($(event.target).is("td")) {
                                                    var checkbox = $(this).parent().find(":checkbox[name*='name']").eq(0);
                                                    if (checkbox.attr("checked") == "checked") {
                                                        checkbox.attr("checked", false);
                                                        opt.CannelRows(rowDat);
                                                    }
                                                    else {
                                                        checkbox.attr("checked", "checked");
                                                        opt.SelectedRows(rowDat);
                                                    }
                                                    checkbox = $(this).parent().find(":radio[name*='name']").eq(0);
                                                    if (checkbox.attr("checked") == "checked") {
                                                        checkbox.attr("checked", false);
                                                    }
                                                    else {
                                                        $datatable.find(".tablehover").removeClass("griddatahover");
                                                        checkbox.attr("checked", "checked");
                                                        tr.addClass("griddatahover");
                                                    }
                                                }
                                            });
                                        } tr.append(td);
                                        break;
                                    }

                                }
                            }
                            else
                                tr.append(td);
                        });
                        $datatable.append(tr);
                        if (rowDat.children) {
                            $datatable.append(childrenTableData(rowDat, opt.columns, level + 1));
                        }
                    });
                    if ($datatable.find("tr").length <= 0) {
                        $datatable.append("<tr><td class='gridlabel' colspan='" + columns.length + "'>暂无数据</td></tr>");
                    }
                }

                function childrenTableData(currentRowData, columns, level) {
                    var $tr = $("<tr />");//.css("display", "none");

                    var $td = $("<td />").attr("colspan", columns.length).css({ "border": "0", "margin": "0", "padding": "0" });
                    var $div = $("<div />").css("display", "block ");
                    $tr.append($td);
                    var table = $("<table />").css({ "width": "100%" }).addClass("datagrid").attr({ "border": "0", "cellpadding": "0", "cellspacing": "0" });
                    $div.append(table);
                    $.each(currentRowData.children, function (i, rowDat) {

                        var tr = $("<tr  />").css({ "border-collapse": "separate" });
                        tr.hover(function () { $(this).addClass("griddatahover"); }, function () {
                            if ($(this).find(":checkbox,:radio").attr("checked") != "checked")
                                $(this).removeClass("griddatahover");

                        });
                        tr.data("data", rowDat);
                        $.each(columns, function (i, colDat) {
                            var td = $("<td />", { "class": "gridlabel" }).css({ "border-collapse": "separate" });
                            td.width(colDat.width);
                            if (colDat.field) {
                                for (key in rowDat) {

                                    if (colDat.field.toLowerCase() == key.toLowerCase()) {
                                        if (colDat.align) {
                                            td.css({ "text-align": colDat.align });
                                        }
                                        if (colDat.checkbox) {
                                            var input = $("<input />", { "type": "checkbox", "name": "name" + i }).val(rowDat[key]);
                                            input.click(function () {

                                                if ($(this).attr("checked") == "checked") {
                                                    opt.SelectedRows(rowDat);
                                                    tr.addClass("griddatahover");
                                                    if (opt.Cascade) {
                                                        if (tr.next().find("div").length > 0) {
                                                            tr.next().find("div").find(":checkbox").each(function () {
                                                                $(this).prop("checked", true);
                                                                $(this).parent().parent().addClass("griddatahover");
                                                            });
                                                        }
                                                        var ptable = $(this).parent().parent().parent();
                                                        var b = true;
                                                        while (true) {
                                                            var trs = ptable.find("tr");
                                                            trs.each(function (i) {
                                                                if ($(this).find("div").length <= 0) {
                                                                    if (!$(this).find(":checkbox").eq(0).prop("checked")) {
                                                                        b = false;
                                                                        return false;
                                                                    }
                                                                }

                                                            });
                                                            if (!b)
                                                                break;
                                                            if (ptable.is("tbody")) {
                                                                var istd = ptable.parent().parent().parent();
                                                                if (istd.is("td")) {
                                                                    ptable = istd.parent().parent();
                                                                    istd.parent().prev().find(":checkbox").eq(0).prop("checked", true);
                                                                }
                                                                else
                                                                    break;
                                                            }


                                                            if (ptable.is("table")) {
                                                                var istd = ptable.parent().parent();
                                                                if (istd.is("td")) {
                                                                    ptable = istd.parent().parent();
                                                                    istd.parent().find(":checkbox").eq(0).prop("checked", true);
                                                                }
                                                                else
                                                                    break;
                                                            }

                                                        }
                                                    }
                                                }
                                                else {
                                                    opt.CannelRows(rowDat);
                                                    if (opt.Cascade) {
                                                        if (tr.next().find("div").length > 0) {
                                                            tr.next().find("div").find(":checkbox").each(function () {
                                                                $(this).prop("checked", false);
                                                                $(this).parent().parent().removeClass("griddatahover");
                                                            });
                                                        }




                                                        var ptable = $(this).parent().parent().parent();
                                                        while (true) {
                                                            var trs = ptable.find("tr");

                                                            if (ptable.is("tbody")) {
                                                                var istd = ptable.parent().parent().parent();
                                                                if (istd.is("td")) {
                                                                    ptable = istd.parent().parent();
                                                                    if (!istd.parent().prev().find(":checkbox").eq(0).prop("checked"))
                                                                        break;
                                                                    istd.parent().prev().find(":checkbox").eq(0).prop("checked", false);
                                                                }
                                                                else
                                                                    break;
                                                            }

                                                            if (ptable.is("table")) {
                                                                var istd = ptable.parent().parent();
                                                                if (istd.is("td")) {
                                                                    ptable = istd.parent().parent();
                                                                    if (!istd.parent().find(":checkbox").eq(0).prop("checked", false))
                                                                        break;
                                                                    istd.parent().find(":checkbox").eq(0).prop("checked", false);
                                                                }
                                                                else
                                                                    break;
                                                            }

                                                        }
                                                    }
                                                }
                                            });
                                            td.append(input);
                                        }
                                        else if (colDat.radiobox) {
                                            var input = $("<input />", { "type": "radio", "name": "name" + i }).val(rowDat[key]);
                                            input.click(function () {
                                                if ($(this).attr("checked") == "checked") {
                                                    $datatable.find(".griddatahover").removeClass("griddatahover");
                                                    tr.addClass("griddatahover");
                                                }
                                            });
                                            td.append(input);
                                        }
                                        else {
                                            if (colDat.collapsible) {
                                                if (rowDat.children) {
                                                    var span = $("<span />", { "class": "tree-expanded" });
                                                    span.css({ "margin-left": 18 * level, "cursor": "pointer" });
                                                    var folder = $("<span />", { "class": "tree-folder" });
                                                    td.append(span).append(folder);

                                                    span.click(function () {
                                                        if ($(this).attr("class") == "tree-collapsed") {
                                                            $(this).addClass("tree-expanded").removeClass("tree-collapsed");
                                                            $(this).next().addClass("tree-folder-open").removeClass("tree-folder");
                                                            tr.next().find("div").eq(0).hide();
                                                            tr.next().show();
                                                            tr.next().find("div").eq(0).slideDown("slow", function () {
                                                                $headtable.width($datatable.width());
                                                            });
                                                        }
                                                        else if ($(this).attr("class") == "tree-expanded") {
                                                            $(this).addClass("tree-collapsed").removeClass("tree-expanded");
                                                            $(this).next().addClass("tree-folder").removeClass("tree-folder-open");

                                                            tr.next().find("div").eq(0).slideUp("slow", function () {
                                                                tr.next().hide();
                                                                $headtable.width($datatable.width());
                                                            });

                                                        }
                                                    });
                                                }
                                                else {
                                                    var span = $("<span />", { "class": "tree-indent" });
                                                    span.css({ "margin-left": 18 * level, "cursor": "pointer" });
                                                    var folder = $("<span />", { "class": "tree-file" });
                                                    td.append(span).append(folder);
                                                }
                                                if (colDat.render && typeof (colDat.render) == "function") {
                                                    trStr = colDat.render(rowDat[key].replace('0:00:00', ''), rowDat, key);
                                                    td.append("<span class='tree-title'>" + trStr + "</span>");
                                                }
                                                else {
                                                    td.append("<span class='tree-title'>" + rowDat[key].replace('0:00:00', '') + "</span>");
                                                }
                                            }
                                            else {
                                                if (colDat.render && typeof (colDat.render) == "function") {
                                                    trStr = colDat.render(rowDat[key].replace('0:00:00', ''), rowDat, key);
                                                    td.html(td.html() + trStr);
                                                }
                                                else {
                                                    td.html(rowDat[key].replace('0:00:00', ''));
                                                }
                                            }
                                            td.click(function (event) {

                                                if ($(event.target).is("td")) {
                                                    var checkbox = $(this).parent().find(":checkbox[name*='name']").eq(0);
                                                    if (checkbox.attr("checked") == "checked") {
                                                        checkbox.attr("checked", false);
                                                        opt.CannelRows(rowDat);
                                                    }
                                                    else {
                                                        checkbox.attr("checked", "checked");
                                                        opt.SelectedRows(rowDat);
                                                    }
                                                    checkbox = $(this).parent().find(":radio[name*='name']").eq(0);
                                                    if (checkbox.attr("checked") == "checked") {
                                                        checkbox.attr("checked", false);
                                                    }
                                                    else {
                                                        table.find(".tablehover").removeClass("griddatahover");
                                                        checkbox.attr("checked", "checked");
                                                        tr.addClass("griddatahover");
                                                    }
                                                }
                                            });
                                        } tr.append(td);
                                        break;
                                    }

                                }
                            }
                            else
                                tr.append(td);
                        });
                        table.append(tr);
                        if (rowDat.children) {
                            tr.after(childrenTableData(rowDat, opt.columns, level + 1));
                        }
                    });
                    $td.append($div);
                    $tr.append($td);
                    return $tr;
                }

                function checkAll(obj) {
                    if ($(obj).attr("checked")) {
                        $("#" + $gridcontent.attr("id") + " [name='" + $(obj).attr("name") + "']").attr("checked", true).parent().parent().addClass("griddatahover");

                    }
                    else {
                        $("#" + $gridcontent.attr("id") + " [name='" + $(obj).attr("name") + "']").attr("checked", false).parent().parent().removeClass("griddatahover");

                    }
                }


                //设置排序以及页码
                function pageSize() {
                    var div = $("<div />", { "class": "gridfooter" });
                    $datatable.parent().next().remove();
                    if (opt.pageSelect.length > 0) {
                        var select = $("<select />");
                        select.append("<option>" + opt.pageCount + "</option>");
                        $.each(opt.pageSelect, function (i, dat) {
                            if (opt.pageCount != dat) {
                                select.append("<option>" + dat + "</option>");
                            }
                        });
                        select.change(function () {
                            opt.pageCount = $(this).val();
                            gridContentData(opt.order, opt.sortName);
                            div.find("div").remove();
                            div.append(SetPageGo());
                            $(this).val(opt.pageCount);
                            // SetTaleEquel(headerTable, table);
                        });
                        var sp = $("<span />").css({ "line-height": "37px", "float": "left" });
                        sp.html(select);
                        select.before("每页&nbsp;");
                        select.after("&nbsp;条目数");
                        div.append(sp);
                    }
                    //div.append(spn);
                    div.append(SetPageGo());
                    $gridcontent.append(div);
                    // SetTaleEquel(headerTable, table);
                    $headtable.width($datatable.width());
                }
                //排序↑↓
                function clumOrder(orderType, sortName, text) {
                    var spn = $("<span />").css("cursor", "pointer");
                    spn.html(text);
                    spn.attr("order", orderType);
                    spn.bind("click", function () {
                        spn.text(text);
                        var spanText = $(".orderF").text();
                        if (spanText.indexOf("↑") > -1 || spanText.indexOf("↓") > -1) {
                            spanText = spanText.replace("↑", "").replace("↓", "");
                            $(".orderF").text(spanText);
                            $(".orderF").removeClass("orderF");
                        }
                        if ($(this).attr("order") == "asc") {
                            $(this).attr("order", "desc");
                            spn.html(spn.html() + "&nbsp;↓");
                        }
                        else {
                            $(this).attr("order", "asc");
                            spn.html(spn.html() + "&nbsp;↑");
                        }
                        gridContent($(this).attr("order"), sortName);
                        spn.addClass("orderF");
                        SetTaleEquel(headerTable, table);
                    });

                    return spn;
                }

                //跳页
                function SetPageGo() {
                    var div = $('<div />').css({ "float": "right", "width": "440px", "line-height": "37px" });
                    div.html("共有记录" + total + "条&nbsp;" + opt.page + "/" + Math.ceil(parseInt(total) / opt.pageCount));
                    $("<span />", { "class": "fPage" }).text("首页").bind("click", function () { firstPage(); }).appendTo(div);
                    $("<span />", { "class": "prevPage" }).text("上一页").bind("click", function () { prevPage(); }).appendTo(div);
                    $("<span />", { "class": "nextPage" }).text("下一页").bind("click", function () { nextPage(); }).appendTo(div);
                    $("<span />", { "class": "ePage" }).text("末页").bind("click", function () { endPage(); }).appendTo(div);
                    $("<input />", { "class": "pageText" }).css({ width: 35, height: 15 }).appendTo(div);
                    $("<span />", { "class": "pageGo" }).text("跳转").bind("click", function () { pageGo(this); }).appendTo(div);

                    return div;

                }

                //首页
                function firstPage() {
                    if (opt.page != 1) {
                        opt.page = 1;
                        GetData(opt.idField, opt.ajaxUrl, opt.sortName, opt.order, opt.connectionName, opt.connectionString, opt.providerName, opt.tableName, opt.condition, opt.pageCount, opt.where, opt.columns, opt.page, opt.parentfield, opt.childfield, opt.childWhere, opt.relevanSearch);

                        pageSize();
                    }
                }
                //末页
                function endPage() {
                    if (opt.page < Math.ceil(parseInt(total) / opt.pageCount)) {
                        opt.page = Math.ceil(parseInt(total) / opt.pageCount);
                        GetData(opt.idField, opt.ajaxUrl, opt.sortName, opt.order, opt.connectionName, opt.connectionString, opt.providerName, opt.tableName, opt.condition, opt.pageCount, opt.where, opt.columns, opt.page, opt.parentfield, opt.childfield, opt.childWhere, opt.relevanSearch);

                        pageSize();
                    }
                }

                //上一页
                function prevPage() {
                    if (opt.page > 1) {
                        opt.page -= 1;
                        GetData(opt.idField, opt.ajaxUrl, opt.sortName, opt.order, opt.connectionName, opt.connectionString, opt.providerName, opt.tableName, opt.condition, opt.pageCount, opt.where, opt.columns, opt.page, opt.parentfield, opt.childfield, opt.childWhere, opt.relevanSearch);

                        pageSize();
                    }
                }

                //下一页
                function nextPage() {

                    if (opt.page < Math.ceil(parseInt(total) / opt.pageCount)) {
                        opt.page += 1;
                        GetData(opt.idField, opt.ajaxUrl, opt.sortName, opt.order, opt.connectionName, opt.connectionString, opt.providerName, opt.tableName, opt.condition, opt.pageCount, opt.where, opt.columns, opt.page, opt.parentfield, opt.childfield, opt.childWhere, opt.relevanSearch);

                        pageSize();
                    }
                }
                //跳页
                function pageGo(obj) {

                    if (!isNaN($(obj).prev(".pageText").val())) {
                        var paged = parseInt($(obj).prev(".pageText").val());
                        if (paged >= 1 && paged <= Math.ceil(parseInt(total) / opt.pageCount) && paged != opt.page) {
                            opt.page = paged;
                            GetData(opt.idField, opt.ajaxUrl, opt.sortName, opt.order, opt.connectionName, opt.connectionString, opt.providerName, opt.tableName, opt.condition, opt.pageCount, opt.where, opt.columns, opt.page, opt.parentfield, opt.childfield, opt.childWhere, opt.relevanSearch);

                            pageSize();
                        }
                    }
                }  //设置表格内容  
                function gridContent() {
                    GetData(opt.idField, opt.ajaxUrl, arguments[1], arguments[0], opt.connectionName, opt.connectionString, opt.providerName, opt.tableName, opt.condition, opt.pageCount, opt.where, opt.columns, opt.page, opt.parentfield, opt.childfield, opt.childWhere, opt.relevanSearch);
                    // pageSize("top")
                    pageSize("b");
                }
                function gridContentData() {
                    GetData(opt.idField, opt.ajaxUrl, arguments[1], arguments[0], opt.connectionName, opt.connectionString, opt.providerName, opt.tableName, opt.condition, opt.pageCount, opt.where, opt.columns, opt.page, opt.parentfield, opt.childfield, opt.childWhere, opt.relevanSearch);

                }

            });
        }, GetSelection: function (type) {

            var arry = new Array();
            var table = $(this).find("table").eq(1);

            if (type.toLowerCase() == "single") {

                table.find("[id!='checkall'][name*='name']:checked").each(function () {
                    if ($(this).attr("value"))
                        arry[arry.length] = $(this).val();
                });
            }
            if (type.toLowerCase() == "total") {
                table.find("[id!='checkall'][name*='name']:checked").each(function () {
                    if ($(this).attr("value"))
                        arry[arry.length] = $(this).parent().parent().data("data");
                });
            }
            return arry;
        }, CannelSelection: function () {
            var table = $(this).find("table").eq(1);
            table.find("input:checked").each(function () {
                $(this).prop("checked", false);
                $(this).parent().parent().removeClass("griddatahover");
            });
        }
    });
})(jQuery);


/// 加解密主调方法
/// beinetkey         密钥
/// message     加密时为待加密的字符串，解密时为待解密的字符串
/// encrypt     1：加密；0：解密
/// mode        true：CBC mode；false：非CBC mode
/// iv          初始化向量
function des(beinetkey, message, encrypt, mode, iv) {
    //declaring this locally speeds things up a bit
    var spfunction1 = new Array(0x1010400, 0, 0x10000, 0x1010404, 0x1010004, 0x10404, 0x4, 0x10000, 0x400, 0x1010400, 0x1010404, 0x400, 0x1000404, 0x1010004, 0x1000000, 0x4, 0x404, 0x1000400, 0x1000400, 0x10400, 0x10400, 0x1010000, 0x1010000, 0x1000404, 0x10004, 0x1000004, 0x1000004, 0x10004, 0, 0x404, 0x10404, 0x1000000, 0x10000, 0x1010404, 0x4, 0x1010000, 0x1010400, 0x1000000, 0x1000000, 0x400, 0x1010004, 0x10000, 0x10400, 0x1000004, 0x400, 0x4, 0x1000404, 0x10404, 0x1010404, 0x10004, 0x1010000, 0x1000404, 0x1000004, 0x404, 0x10404, 0x1010400, 0x404, 0x1000400, 0x1000400, 0, 0x10004, 0x10400, 0, 0x1010004);
    var spfunction2 = new Array(-0x7fef7fe0, -0x7fff8000, 0x8000, 0x108020, 0x100000, 0x20, -0x7fefffe0, -0x7fff7fe0, -0x7fffffe0, -0x7fef7fe0, -0x7fef8000, -0x80000000, -0x7fff8000, 0x100000, 0x20, -0x7fefffe0, 0x108000, 0x100020, -0x7fff7fe0, 0, -0x80000000, 0x8000, 0x108020, -0x7ff00000, 0x100020, -0x7fffffe0, 0, 0x108000, 0x8020, -0x7fef8000, -0x7ff00000, 0x8020, 0, 0x108020, -0x7fefffe0, 0x100000, -0x7fff7fe0, -0x7ff00000, -0x7fef8000, 0x8000, -0x7ff00000, -0x7fff8000, 0x20, -0x7fef7fe0, 0x108020, 0x20, 0x8000, -0x80000000, 0x8020, -0x7fef8000, 0x100000, -0x7fffffe0, 0x100020, -0x7fff7fe0, -0x7fffffe0, 0x100020, 0x108000, 0, -0x7fff8000, 0x8020, -0x80000000, -0x7fefffe0, -0x7fef7fe0, 0x108000);
    var spfunction3 = new Array(0x208, 0x8020200, 0, 0x8020008, 0x8000200, 0, 0x20208, 0x8000200, 0x20008, 0x8000008, 0x8000008, 0x20000, 0x8020208, 0x20008, 0x8020000, 0x208, 0x8000000, 0x8, 0x8020200, 0x200, 0x20200, 0x8020000, 0x8020008, 0x20208, 0x8000208, 0x20200, 0x20000, 0x8000208, 0x8, 0x8020208, 0x200, 0x8000000, 0x8020200, 0x8000000, 0x20008, 0x208, 0x20000, 0x8020200, 0x8000200, 0, 0x200, 0x20008, 0x8020208, 0x8000200, 0x8000008, 0x200, 0, 0x8020008, 0x8000208, 0x20000, 0x8000000, 0x8020208, 0x8, 0x20208, 0x20200, 0x8000008, 0x8020000, 0x8000208, 0x208, 0x8020000, 0x20208, 0x8, 0x8020008, 0x20200);
    var spfunction4 = new Array(0x802001, 0x2081, 0x2081, 0x80, 0x802080, 0x800081, 0x800001, 0x2001, 0, 0x802000, 0x802000, 0x802081, 0x81, 0, 0x800080, 0x800001, 0x1, 0x2000, 0x800000, 0x802001, 0x80, 0x800000, 0x2001, 0x2080, 0x800081, 0x1, 0x2080, 0x800080, 0x2000, 0x802080, 0x802081, 0x81, 0x800080, 0x800001, 0x802000, 0x802081, 0x81, 0, 0, 0x802000, 0x2080, 0x800080, 0x800081, 0x1, 0x802001, 0x2081, 0x2081, 0x80, 0x802081, 0x81, 0x1, 0x2000, 0x800001, 0x2001, 0x802080, 0x800081, 0x2001, 0x2080, 0x800000, 0x802001, 0x80, 0x800000, 0x2000, 0x802080);
    var spfunction5 = new Array(0x100, 0x2080100, 0x2080000, 0x42000100, 0x80000, 0x100, 0x40000000, 0x2080000, 0x40080100, 0x80000, 0x2000100, 0x40080100, 0x42000100, 0x42080000, 0x80100, 0x40000000, 0x2000000, 0x40080000, 0x40080000, 0, 0x40000100, 0x42080100, 0x42080100, 0x2000100, 0x42080000, 0x40000100, 0, 0x42000000, 0x2080100, 0x2000000, 0x42000000, 0x80100, 0x80000, 0x42000100, 0x100, 0x2000000, 0x40000000, 0x2080000, 0x42000100, 0x40080100, 0x2000100, 0x40000000, 0x42080000, 0x2080100, 0x40080100, 0x100, 0x2000000, 0x42080000, 0x42080100, 0x80100, 0x42000000, 0x42080100, 0x2080000, 0, 0x40080000, 0x42000000, 0x80100, 0x2000100, 0x40000100, 0x80000, 0, 0x40080000, 0x2080100, 0x40000100);
    var spfunction6 = new Array(0x20000010, 0x20400000, 0x4000, 0x20404010, 0x20400000, 0x10, 0x20404010, 0x400000, 0x20004000, 0x404010, 0x400000, 0x20000010, 0x400010, 0x20004000, 0x20000000, 0x4010, 0, 0x400010, 0x20004010, 0x4000, 0x404000, 0x20004010, 0x10, 0x20400010, 0x20400010, 0, 0x404010, 0x20404000, 0x4010, 0x404000, 0x20404000, 0x20000000, 0x20004000, 0x10, 0x20400010, 0x404000, 0x20404010, 0x400000, 0x4010, 0x20000010, 0x400000, 0x20004000, 0x20000000, 0x4010, 0x20000010, 0x20404010, 0x404000, 0x20400000, 0x404010, 0x20404000, 0, 0x20400010, 0x10, 0x4000, 0x20400000, 0x404010, 0x4000, 0x400010, 0x20004010, 0, 0x20404000, 0x20000000, 0x400010, 0x20004010);
    var spfunction7 = new Array(0x200000, 0x4200002, 0x4000802, 0, 0x800, 0x4000802, 0x200802, 0x4200800, 0x4200802, 0x200000, 0, 0x4000002, 0x2, 0x4000000, 0x4200002, 0x802, 0x4000800, 0x200802, 0x200002, 0x4000800, 0x4000002, 0x4200000, 0x4200800, 0x200002, 0x4200000, 0x800, 0x802, 0x4200802, 0x200800, 0x2, 0x4000000, 0x200800, 0x4000000, 0x200800, 0x200000, 0x4000802, 0x4000802, 0x4200002, 0x4200002, 0x2, 0x200002, 0x4000000, 0x4000800, 0x200000, 0x4200800, 0x802, 0x200802, 0x4200800, 0x802, 0x4000002, 0x4200802, 0x4200000, 0x200800, 0, 0x2, 0x4200802, 0, 0x200802, 0x4200000, 0x800, 0x4000002, 0x4000800, 0x800, 0x200002);
    var spfunction8 = new Array(0x10001040, 0x1000, 0x40000, 0x10041040, 0x10000000, 0x10001040, 0x40, 0x10000000, 0x40040, 0x10040000, 0x10041040, 0x41000, 0x10041000, 0x41040, 0x1000, 0x40, 0x10040000, 0x10000040, 0x10001000, 0x1040, 0x41000, 0x40040, 0x10040040, 0x10041000, 0x1040, 0, 0, 0x10040040, 0x10000040, 0x10001000, 0x41040, 0x40000, 0x41040, 0x40000, 0x10041000, 0x1000, 0x40, 0x10040040, 0x1000, 0x41040, 0x10001000, 0x40, 0x10000040, 0x10040000, 0x10040040, 0x10000000, 0x40000, 0x10001040, 0, 0x10041040, 0x40040, 0x10000040, 0x10040000, 0x10001000, 0x10001040, 0, 0x10041040, 0x41000, 0x41000, 0x1040, 0x1040, 0x40040, 0x10000000, 0x10041000);

    //create the 16 or 48 subkeys we will need
    var keys = des_createKeys(beinetkey);
    var m = 0, i, j, temp, temp2, right1, right2, left, right, looping;
    var cbcleft, cbcleft2, cbcright, cbcright2
    var endloop, loopinc;
    var len = message.length;
    var chunk = 0;
    //set up the loops for single and triple des
    var iterations = keys.length == 32 ? 3 : 9; //single or triple des
    if (iterations == 3) { looping = encrypt ? new Array(0, 32, 2) : new Array(30, -2, -2); }
    else { looping = encrypt ? new Array(0, 32, 2, 62, 30, -2, 64, 96, 2) : new Array(94, 62, -2, 32, 64, 2, 30, -2, -2); }

    message += '\0\0\0\0\0\0\0\0'; //pad the message out with null bytes
    //store the result here
    result = '';
    tempresult = '';

    if (mode == 1) {//CBC mode
        cbcleft = (iv.charCodeAt(m++) << 24) | (iv.charCodeAt(m++) << 16) | (iv.charCodeAt(m++) << 8) | iv.charCodeAt(m++);
        cbcright = (iv.charCodeAt(m++) << 24) | (iv.charCodeAt(m++) << 16) | (iv.charCodeAt(m++) << 8) | iv.charCodeAt(m++);
        m = 0;
    }

    //loop through each 64 bit chunk of the message
    while (m < len) {
        if (encrypt) {//加密时按双字节操作
            left = (message.charCodeAt(m++) << 16) | message.charCodeAt(m++);
            right = (message.charCodeAt(m++) << 16) | message.charCodeAt(m++);
        } else {
            left = (message.charCodeAt(m++) << 24) | (message.charCodeAt(m++) << 16) | (message.charCodeAt(m++) << 8) | message.charCodeAt(m++);
            right = (message.charCodeAt(m++) << 24) | (message.charCodeAt(m++) << 16) | (message.charCodeAt(m++) << 8) | message.charCodeAt(m++);
        }
        //for Cipher Block Chaining mode,xor the message with the previous result
        if (mode == 1) { if (encrypt) { left ^= cbcleft; right ^= cbcright; } else { cbcleft2 = cbcleft; cbcright2 = cbcright; cbcleft = left; cbcright = right; } }

        //first each 64 but chunk of the message must be permuted according to IP
        temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
        temp = ((left >>> 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
        temp = ((right >>> 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
        temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
        temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

        left = ((left << 1) | (left >>> 31));
        right = ((right << 1) | (right >>> 31));

        //do this either 1 or 3 times for each chunk of the message
        for (j = 0; j < iterations; j += 3) {
            endloop = looping[j + 1];
            loopinc = looping[j + 2];
            //now go through and perform the encryption or decryption 
            for (i = looping[j]; i != endloop; i += loopinc) {//for efficiency
                right1 = right ^ keys[i];
                right2 = ((right >>> 4) | (right << 28)) ^ keys[i + 1];
                //the result is attained by passing these bytes through the S selection functions
                temp = left;
                left = right;
                right = temp ^ (spfunction2[(right1 >>> 24) & 0x3f] | spfunction4[(right1 >>> 16) & 0x3f] | spfunction6[(right1 >>> 8) & 0x3f] | spfunction8[right1 & 0x3f] | spfunction1[(right2 >>> 24) & 0x3f] | spfunction3[(right2 >>> 16) & 0x3f] | spfunction5[(right2 >>> 8) & 0x3f] | spfunction7[right2 & 0x3f]);
            }
            temp = left; left = right; right = temp; //unreverse left and right
        } //for either 1 or 3 iterations

        //move then each one bit to the right
        left = ((left >>> 1) | (left << 31));
        right = ((right >>> 1) | (right << 31));

        //now perform IP-1,which is IP in the opposite direction
        temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
        temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
        temp = ((right >>> 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
        temp = ((left >>> 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
        temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);

        //for Cipher Block Chaining mode,xor the message with the previous result
        if (mode == 1) { if (encrypt) { cbcleft = left; cbcright = right; } else { left ^= cbcleft2; right ^= cbcright2; } }
        if (encrypt) {
            tempresult += String.fromCharCode((left >>> 24), ((left >>> 16) & 0xff), ((left >>> 8) & 0xff), (left & 0xff), (right >>> 24), ((right >>> 16) & 0xff), ((right >>> 8) & 0xff), (right & 0xff));
        }
        else {
            tempresult += String.fromCharCode(((left >>> 16) & 0xffff), (left & 0xffff), ((right >>> 16) & 0xffff), (right & 0xffff));
        } //解密时输出双字节
        encrypt ? chunk += 16 : chunk += 8;
        if (chunk == 512) { result += tempresult; tempresult = ''; chunk = 0; }
    } //for every 8 characters,or 64 bits in the message

    //return the result as an array
    return result + tempresult;
} //end of des

//des_createKeys
//this takes as input a 64 bit beinetkey(even though only 56 bits are used)
//as an array of 2 integers,and returns 16 48 bit keys
function des_createKeys(beinetkey) {
    //declaring this locally speeds things up a bit
    pc2bytes0 = new Array(0, 0x4, 0x20000000, 0x20000004, 0x10000, 0x10004, 0x20010000, 0x20010004, 0x200, 0x204, 0x20000200, 0x20000204, 0x10200, 0x10204, 0x20010200, 0x20010204);
    pc2bytes1 = new Array(0, 0x1, 0x100000, 0x100001, 0x4000000, 0x4000001, 0x4100000, 0x4100001, 0x100, 0x101, 0x100100, 0x100101, 0x4000100, 0x4000101, 0x4100100, 0x4100101);
    pc2bytes2 = new Array(0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808, 0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808);
    pc2bytes3 = new Array(0, 0x200000, 0x8000000, 0x8200000, 0x2000, 0x202000, 0x8002000, 0x8202000, 0x20000, 0x220000, 0x8020000, 0x8220000, 0x22000, 0x222000, 0x8022000, 0x8222000);
    pc2bytes4 = new Array(0, 0x40000, 0x10, 0x40010, 0, 0x40000, 0x10, 0x40010, 0x1000, 0x41000, 0x1010, 0x41010, 0x1000, 0x41000, 0x1010, 0x41010);
    pc2bytes5 = new Array(0, 0x400, 0x20, 0x420, 0, 0x400, 0x20, 0x420, 0x2000000, 0x2000400, 0x2000020, 0x2000420, 0x2000000, 0x2000400, 0x2000020, 0x2000420);
    pc2bytes6 = new Array(0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002, 0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002);
    pc2bytes7 = new Array(0, 0x10000, 0x800, 0x10800, 0x20000000, 0x20010000, 0x20000800, 0x20010800, 0x20000, 0x30000, 0x20800, 0x30800, 0x20020000, 0x20030000, 0x20020800, 0x20030800);
    pc2bytes8 = new Array(0, 0x40000, 0, 0x40000, 0x2, 0x40002, 0x2, 0x40002, 0x2000000, 0x2040000, 0x2000000, 0x2040000, 0x2000002, 0x2040002, 0x2000002, 0x2040002);
    pc2bytes9 = new Array(0, 0x10000000, 0x8, 0x10000008, 0, 0x10000000, 0x8, 0x10000008, 0x400, 0x10000400, 0x408, 0x10000408, 0x400, 0x10000400, 0x408, 0x10000408);
    pc2bytes10 = new Array(0, 0x20, 0, 0x20, 0x100000, 0x100020, 0x100000, 0x100020, 0x2000, 0x2020, 0x2000, 0x2020, 0x102000, 0x102020, 0x102000, 0x102020);
    pc2bytes11 = new Array(0, 0x1000000, 0x200, 0x1000200, 0x200000, 0x1200000, 0x200200, 0x1200200, 0x4000000, 0x5000000, 0x4000200, 0x5000200, 0x4200000, 0x5200000, 0x4200200, 0x5200200);
    pc2bytes12 = new Array(0, 0x1000, 0x8000000, 0x8001000, 0x80000, 0x81000, 0x8080000, 0x8081000, 0x10, 0x1010, 0x8000010, 0x8001010, 0x80010, 0x81010, 0x8080010, 0x8081010);
    pc2bytes13 = new Array(0, 0x4, 0x100, 0x104, 0, 0x4, 0x100, 0x104, 0x1, 0x5, 0x101, 0x105, 0x1, 0x5, 0x101, 0x105);

    //how many iterations(1 for des,3 for triple des)
    var iterations = beinetkey.length >= 24 ? 3 : 1;
    //stores the return keys
    var keys = new Array(32 * iterations);
    //now define the left shifts which need to be done
    var shifts = new Array(0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0);
    //other variables
    var lefttemp, righttemp, m = 0, n = 0, temp;

    for (var j = 0; j < iterations; j++) {//either 1 or 3 iterations
        left = (beinetkey.charCodeAt(m++) << 24) | (beinetkey.charCodeAt(m++) << 16) | (beinetkey.charCodeAt(m++) << 8) | beinetkey.charCodeAt(m++);
        right = (beinetkey.charCodeAt(m++) << 24) | (beinetkey.charCodeAt(m++) << 16) | (beinetkey.charCodeAt(m++) << 8) | beinetkey.charCodeAt(m++);

        temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
        temp = ((right >>> -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
        temp = ((left >>> 2) ^ right) & 0x33333333; right ^= temp; left ^= (temp << 2);
        temp = ((right >>> -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
        temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
        temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
        temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

        //the right side needs to be shifted and to get the last four bits of the left side
        temp = (left << 8) | ((right >>> 20) & 0x000000f0);
        //left needs to be put upside down
        left = (right << 24) | ((right << 8) & 0xff0000) | ((right >>> 8) & 0xff00) | ((right >>> 24) & 0xf0);
        right = temp;

        //now go through and perform these shifts on the left and right keys
        for (i = 0; i < shifts.length; i++) {
            //shift the keys either one or two bits to the left
            if (shifts[i]) { left = (left << 2) | (left >>> 26); right = (right << 2) | (right >>> 26); }
            else { left = (left << 1) | (left >>> 27); right = (right << 1) | (right >>> 27); }
            left &= -0xf; right &= -0xf;

            //now apply PC-2,in such a way that E is easier when encrypting or decrypting
            //this conversion will look like PC-2 except only the last 6 bits of each byte are used
            //rather than 48 consecutive bits and the order of lines will be according to 
            //how the S selection functions will be applied:S2,S4,S6,S8,S1,S3,S5,S7
            lefttemp = pc2bytes0[left >>> 28] | pc2bytes1[(left >>> 24) & 0xf]
| pc2bytes2[(left >>> 20) & 0xf] | pc2bytes3[(left >>> 16) & 0xf]
| pc2bytes4[(left >>> 12) & 0xf] | pc2bytes5[(left >>> 8) & 0xf]
| pc2bytes6[(left >>> 4) & 0xf];
            righttemp = pc2bytes7[right >>> 28] | pc2bytes8[(right >>> 24) & 0xf]
| pc2bytes9[(right >>> 20) & 0xf] | pc2bytes10[(right >>> 16) & 0xf]
| pc2bytes11[(right >>> 12) & 0xf] | pc2bytes12[(right >>> 8) & 0xf]
| pc2bytes13[(right >>> 4) & 0xf];
            temp = ((righttemp >>> 16) ^ lefttemp) & 0x0000ffff;
            keys[n++] = lefttemp ^ temp; keys[n++] = righttemp ^ (temp << 16);
        }
    } //for each iterations
    //return the keys we've created
    return keys;
} //end of des_createKeys


///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////

// 把字符串转换为16进制字符串
// 如：a变成61（即10进制的97）；abc变成616263
function stringToHex(s) {
    var r = '';
    var hexes = new Array('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f');
    for (var i = 0; i < (s.length) ; i++) { r += hexes[s.charCodeAt(i) >> 4] + hexes[s.charCodeAt(i) & 0xf]; }
    return r;
}
// 16进制字符串转换为字符串
// 如：61（即10进制的97）变成a；616263变成abc
function HexTostring(s) {
    var r = '';
    for (var i = 0; i < s.length; i += 2) {
        var sxx = parseInt(s.substring(i, i + 2), 16);
        r += String.fromCharCode(sxx);
    }
    return r;
}

/// 加密测试函数
/// s     待加密的字符串
/// k     密钥
function encMe(s, k) {
    return stringToHex(des(k, s, 1, 0));
}

/// 解密测试函数
/// s     待解密的字符串
/// k     密钥
function uncMe(s, k) {
    return des(k, HexTostring(s), 0, 0);
}