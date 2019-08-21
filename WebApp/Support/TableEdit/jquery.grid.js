

(function ($) {
    $.fn.extend({
        Grid: function (options) {
            var defaults = {
                columns: [],
                rowsData: [],
                request: "data",
           
                width: "100%",
                height: 0,
                page: 1,
                pageSelect: [5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,30,35,40,45,50],
                BindBefore: function (data) { return data; }
            }
            var options = $.extend(defaults, options);
            return this.each(function () {
                var opt = options;

                var table, headerTable;
                var tableContent = $(this);
                tableContent.children().remove();
                if (tableContent.find("table").length > 1) {
                    table = tableContent.find("table").eq(1);
                    headerTable = tableContent.find("table").eq(0);
                    headerTable.find(":checkbox,:radio").attr("checked", false);
                }
                else {
                    table = $("<table />");
                    headerTable = $("<table />");
                    headerTable.append(SetListHeader(opt.columns));
                    headerTable.attr({ "border": "0", "cellpadding": "0", "cellspacing": "0" }).css({ "width": "100%" });;
                    tableContent.append(headerTable);
                }

                table.attr("border", "0");
                table.attr("cellpadding", "0");
                table.attr("cellspacing", "0");
                table.css({ "width": "100%" });
                table.addClass("tableCls");
                tableContent.addClass("zt");
                tableContent.width(opt.width);
                tableContent.height(opt.height);


                if (table.find("tr").length <= 0) {
                    var divContent = $("<div />");
                    divContent.css({ "overflow-x": "hidden" });
                    divContent.append(table);
                    if (opt.height > 0)
                        divContent.height(tableContent.height() - 35 - 28);

                    tableContent.append(divContent);
                }
                if (opt.request == "data") {
                    setListContent(opt.rowsData, opt.columns);
                    pageSize();
                    
                }



              //  SetTaleEquel(headerTable, table);

                function SetTaleEquel(tableF, eqTable) {
                    tableF.width(Math.round((eqTable.width() / eqTable.parent().width()) * 100) + "%");
                    var tr = tableF.find("tr");
                    var td = table.find("tr").eq(0).find("td");
                    alert(tr.width());
                    tr.find("td").each(function (i) {
                        alert($(this).width()+$(this).text());
                        td.eq(i).width($(this).width());
                    });

                }

                //设置表头
                function SetListHeader(columns) {
                    var tr = $("<tr />");
                    $.each(columns, function (i, obj) {
                        if (typeof (obj) != "undefined") {
                            var td = $("<td />");
                            td.addClass("tableHeader")
                            if (typeof (obj.width) != "undefined") {
                                td.css("width", obj.width);
                            }

                            if (obj.checkbox) {
                                $("<input />", { id: "checkall", type: "checkbox", name: "name" + i }).bind("click", function () { checkAll(this); }).appendTo(td);
                            }
                            else {
                                if (obj.drag) {
                                    if (i < columns.length - 1) {
                                        var span = $("<label />", { "class": "resizeDragClass" });
                                        var img = $('<img />');
                                        span.html("&nbsp;");
                                        drag(span);
                                        td.html(span);
                                    }
                                    td.append("<div  style='padding-top:5px;height:18px;overflow:hidden;width:97%;flaot:left'>" + obj.name + "&nbsp;&nbsp;<img f='" + obj.field + "' style='vertical-align:text-top;cursor:pointer' onclick='setList(this)' src='img/folder_edit.png' /></div>");
                                    if (listSetting.length > 0) {
                                        var column = listSetting[0].List[0].Column;
                                        for (var j = 0; j < column.length; j++) {
                                            if (column[j].field == obj.field) {
                                                td.attr("data", "{\"aglin\":\"" + column[j].aglin + "\",\"order\":\"" + column[j].order + "\",\"drag\":\"" + column[j].drag + "\",\"field\":\"" + obj.field + "\",\"name\":\"" + obj.name + "\"}");
                                                break;
                                            }
                                        }
                                    }
                                    
                                }
                                else {
                                    td.html(obj.name);
                                }
                            }
                            tr.append(td);
                        }
                    });
                    return tr;
                }



                //设置排序以及页码
                function pageSize() {
                    var div = $("<div />", { "class": "zttitle" });
                    table.parent().next().remove();
                    if (opt.pageSelect.length > 0) {
                        var select = $("<select />");
                     
                        $.each(opt.pageSelect, function (i, dat) {
                            if (opt.oldCount != dat) {
                                select.append("<option>" + dat + "</option>");
                            }
                        });
                        
                        var sp = $("<span />").css({ "line-height": "37px", "float": "left" });
                        sp.html(select);
                        select.before("每页&nbsp;");
                        select.after("&nbsp;条目数");
                        div.append(sp);
                    }
                    //div.append(spn);
                    div.append(SetPageGo());
                    tableContent.append(div);
                   
                }


                //跳页
                function SetPageGo() {
                    var div = $('<div />').css({ "float": "right", "width": "400px", "line-height": "37px" });
                    div.html("共有记录10条&nbsp;1/1");
                    $("<span />", { "class": "fPage" }).text("首页").appendTo(div);
                    $("<span />", { "class": "prevPage" }).text("上一页").appendTo(div);
                    $("<span />", { "class": "nextPage" }).text("下一页").appendTo(div);
                    $("<span />", { "class": "ePage" }).text("末页").appendTo(div);
                    $("<input />", { "class": "pageText" }).css({ width: 35, height: 15 }).appendTo(div);
                    $("<span />", { "class": "pageGo" }).text("跳转").appendTo(div);

                    return div;

                }
                //全选
                function checkAll(obj) {
                   
                }






                //设置列表内容
                function setListContent(dataRow, columns) {

                    var trStr = "";
                    table.find("tr").remove();
                    $.each(dataRow, function (j, rowDat) {
                        var tr = $("<tr  />");
                        if (j % 2 != 0)
                            tr.addClass("tabletr");
                        tr.hover(function () { $(this).addClass("tablehover"); }, function () {
                            if ($(this).find(":checkbox,:radio").attr("checked") != "checked")
                                $(this).removeClass("tablehover");

                        });

                        tr.data("data", rowDat);
                        $.each(columns, function (i, colDat) {
                            if (typeof (colDat) != "undefined") {
                                var td = $("<td />", { "class": "tablecontent" });
                               
                                if (typeof (colDat.width) != "undefined") {
                                    td.css("width", colDat.width); 
                                }
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

                                                        tr.addClass("tablehover");
                                                    }
                                                });
                                                td.append(input);
                                            }
                                            else if (colDat.radiobox) {
                                                var input = $("<input />", { "type": "radio", "name": "name" + i }).val(rowDat[key]);
                                                input.click(function () {
                                                    if ($(this).attr("checked") == "checked") {
                                                        table.find(".tablehover").removeClass("tablehover");
                                                        tr.addClass("tablehover");
                                                    }
                                                });
                                                td.append(input);
                                            }
                                            else {
                                                if (colDat.render && typeof (colDat.render) == "function") {
                                                    trStr = colDat.render(rowDat[key].replace('0:00:00', ''), rowDat, key);
                                                    td.html(td.html() + trStr);
                                                }
                                                else {
                                                    td.html(rowDat[key]);
                                                }
                                                td.click(function (event) {

                                                    if ($(event.target).is("td")) {
                                                        var checkbox = $(this).parent().find(":checkbox[name*='name']").eq(0);
                                                        if (checkbox.attr("checked") == "checked") {
                                                            checkbox.attr("checked", false);
                                                        }
                                                        else {
                                                            checkbox.attr("checked", "checked");
                                                        }
                                                        checkbox = $(this).parent().find(":radio[name*='name']").eq(0);
                                                        if (checkbox.attr("checked") == "checked") {
                                                            checkbox.attr("checked", false);
                                                        }
                                                        else {
                                                            table.find(".tablehover").removeClass("tablehover");
                                                            checkbox.attr("checked", "checked");
                                                            tr.addClass("tablehover");
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
                            }
                        });
                        table.append(tr);
                    });

                    if (table.find("tr").length <= 0) {
                        table.append("<tr><td class='tablecontent' colspan='" + columns.length + "'>暂无数据</td></tr>");
                    }

                }

                function drag(obj) {
                    var dragEle = $(obj);
                    getEleWidth = function (ele) {
                        return ele.innerWidth();
                    }

                    setEleWidth = function (ele, width) {
                        ele.width(width);
                        var index = ele.index();
                        var table = ele.parent().parent().parent().next();
                        var tr = table.find("tr").eq(0);
                        tr.find("td").eq(index).width(width);
                    }
                    dragEle.mousedown(function () {

                        var d = document; var evt = window.event;
                        var lastX = evt.clientX;
                        var td = dragEle.get(0);
                        var wd = getEleWidth(dragEle.parent()) + getEleWidth(dragEle.parent().next());
                        if (td.setCapture)
                            td.setCapture();
                        else if (window.captureEvents)
                            window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
                        document.onmousemove = function () {
                            var evt = window.event;
                            var t = evt.clientX - lastX;
                            if (getEleWidth(dragEle.parent()) + getEleWidth(dragEle.parent().next()) > wd) {
                                setEleWidth(dragEle.parent().next(), wd - getEleWidth(dragEle.parent()));
                                return;
                            }
                            if (t > 0) {
                                if (dragEle.parent().next().width() < 30)
                                    return;
                                setEleWidth(dragEle.parent(), getEleWidth(dragEle.parent()) + t);
                                setEleWidth(dragEle.parent().next(), getEleWidth(dragEle.parent().next()) - t);
                            } else {//left 
                                if (dragEle.parent().width() < 30)
                                    return;
                                setEleWidth(dragEle.parent(), getEleWidth(dragEle.parent()) + t);
                                setEleWidth(dragEle.parent().next(), getEleWidth(dragEle.parent().next()) - t);
                            }

                            lastX = evt.clientX;
                        };
                        document.onmouseup = function () {
                            var td = dragEle.get(0);
                            if (td.releaseCapture)
                                td.releaseCapture();
                            else if (window.captureEvents)
                                window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
                            document.onmousemove = null;
                            document.onmouseup = null;
                        };
                    });
                }
            });
        },
        GetSelection: function (type) {

            var arry = new Array();
            var table = $(this).find("table");

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
        },
        DetelteRow: function (url) {
            var tableContent = $(this);
            var table = $(this).find("table").eq(1);
            if (confirm("确定要删除吗")) {
                var guids = $(this).GetSelection("single");
                if (guids.length <= 0) {
                    alert("请选择要删除的行"); return;
                }
                $.ajax({
                    url: url,
                    type: "post",
                    data: { tableName: cfg.tableName, where: " and guid in('" + guids.join("','") + "')" },
                    dataType: "text",

                    success: function (data) {
                        if (data != "False") {
                            alert("删除成功");
                            var options = table.data("opt");
                            tableContent.Grid(options);
                        }
                    },
                    error: function ()
                    { alert("请求数据失败"); }
                });
            }
        },
        Refresh: function () {
            var tableContent = $(this);
            $(this).find("table").eq(0).find(":checkbox,:radio").attr("checked", false);
            var table = $(this).find("table").eq(1);
            var options = table.data("opt");
            tableContent.Grid(options);
        }
    });
})(jQuery);

