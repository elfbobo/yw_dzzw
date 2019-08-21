
$(function () {
    if (listSetting.length > 0) {
        LoadList(listSetting);
    }
    if (editSetting.length > 0) {
        loadEdit(editSetting);
    }
});

function LoadList(ListJson) {
    var fun = ListJson[0].fun;
    if (fun.length > 1) {
        var last = $("#gridfun").find("em").last();
        $.each(fun, function (i, dat) {
            if (i > 1) {
                last.before('<em><input title="' + dat.cn + '" style="vertical-align: text-top;" type="checkbox" value="' + dat.en + '" img="' + dat.img + '"><label>' + dat.cn + '</label></em>');
            }
        });
    }
    var list = ListJson[0].List;
    $('#sortSelect').val(list[0].sort);
    var column = list[0].Column;
    var $gridContent = $("#grid");
    var cfg = [];
    cfg.width = "98%";//99%
    cfg.height = 400;
    cfg.request = "data"; //默认ajax 


    cfg.columns = [];
    cfg.columns.push({ field: $("#sortSelect option[pk]").text(), checkbox: true, width: "50px" });
    var griddata = [];
    $.each(column, function (i, dat) {
        $("#" + dat.field).prop("checked", true);
        cfg.columns.push({ field: dat.field, name: dat.name, width: dat.width + '%', align: dat.aglin, drag: true, order: true });

        griddata.push(JSON.parse($("#" + dat.field).val()));
    });
    cfg.rowsData = setRowData2(griddata, 10);

    $gridContent.Grid(cfg);
    $("#checkall").attr("checked", list[0].IsChecked); $(".zttitle select").val(list[0].PageCount);
    $("#grid tableHeader").each(function () {
        var data = JSON.parse($(this).attr(data));
        $.each(column, function (i, dat) {
            cfg.columns.push({ field: dat.FieldName, name: dat.FieldDesc, width: width + '%', align: dat.aglin, drag: true, order: true });
            if (dat.FieldName == data.field) {
                data.aglin = dat.align;
                data.drag = dat.drag;
                data.order = dat.order;
                $(this).attr("data", JSON.stringify(data));
            }
        });
    });

    var search = ListJson[0].Search;
    var tr = '';
    for (var i = 0; i < search.length; i++) {
        tr += "<tr>"
        if (search[i].Empty) {
            tr += "<td class='TdContent'></td></tr>";
        }
        else {
            var data = $('#_' + search[i].FieldName).val();
            var dat = JSON.parse(data);
            tr += "<td class='TdContent' data='" + data + "'><span>" + dat.FieldDesc + "</span><div style='float:right;width:70%;text-align:right'>";
            if (dat.FieldDataType == "int" || dat.FieldDataType == "float" || dat.FieldDataType == "datetime") {
                tr += "<select style='width:30%;' search><option value='='>等于</option><option>不等于</option><option value='<='>至多</option><option value='>='>至少</option></select>";
            }
            else
                tr += "<select style='width:30%;' search><option value='='>等于</option><option value='<>'>不等于</option><option selected='selected' value='like'>包含</option><option value='not like'>不包含</option></select>";
            tr += "<div f style='width:60%;margin-left:15px;float:right'>";
            if (dat.control == "select") {
                tr += "<select><option>选项1</option></select>";
            }
            else if (dat.control == "Checkbox") {
                tr += "<input type='checkbox' />选项1";
            }
            else if (dat.control == "Radio") {
                tr += "<input type='radio' />选项1";
            }
            else
                tr += "<input type='text'   />";
            tr += "</div></div></td>";
        }
        if (i + 1 < search.length) {
            if (search[i + 1].Empty) {
                tr += "<td class='TdContent'></td></tr>";
            }
            else {
                var data = $('#_' + search[i + 1].FieldName).val();
                var dat = JSON.parse(data);
                tr += "<td class='TdContent' data='" + data + "'><span>" + dat.FieldDesc + "</span><div style='float:right;width:70%;text-align:right'>";
                if (dat.FieldDataType == "int" || dat.FieldDataType == "float" || dat.FieldDataType == "datetime") {
                    tr += "<select style='width:30%;' search><option value='='>等于</option><option>不等于</option><option value='<='>至多</option><option value='>='>至少</option></select>";
                }
                else
                    tr += "<select style='width:30%;' search><option value='='>等于</option><option value='<>'>不等于</option><option selected='selected' value='like'>包含</option><option value='not like'>不包含</option></select>";
                tr += "<div f style='width:60%;margin-left:15px;float:right'>";
                if (dat.control == "select") {
                    tr += "<select><option>选项1</option></select>";
                }
                else if (dat.control == "Checkbox") {
                    tr += "<input type='checkbox' />选项1";
                }
                else if (dat.control == "Radio") {
                    tr += "<input type='radio' />选项1";
                }
                else
                    tr += "<input type='text'   />";
                tr += "</div></div></td></tr>";
            }
            i++;
        }
    }
    $('#searchTable').children().remove();
    $('#searchTable').append(tr);
    bindSearchTabel();
    var p = ListJson[0].pamas;
    $.each(p, function (i, dat) {
        $("#pamas").dynamicRow({});
        var contorl = $("#pamas").nextAll().last().find("select,input");
        contorl.eq(0).val(dat.type);
        contorl.eq(1).val(dat.action);
        contorl.eq(2).val(dat.value);
    });
    //$("#pamas").dynamicRow({ });
}


function setRowData2(json, count) {
    var rowData = [];
    for (var c = 0; c < count; c++) {
        var strJson = "{\"" + $("#sortSelect option[pk]").text() + "\":\"" + Math.random() + "\"";
        $.each(json, function (i, dat) {

            strJson += ",";
            if (dat.FieldDataType == "int") {
                strJson += "\"" + dat.FieldName + "\":100";
            }
            else if (dat.FieldDataType == "datetime")
                strJson += "\"" + dat.FieldName + "\":\"" + new Date().toLocaleDateString() + "\"";
            else if (dat.FieldDataType == "float")
                strJson += "\"" + dat.FieldName + "\":10.00";
            else
                strJson += "\"" + dat.FieldName + "\":\"" + dat.FieldDesc + dat.FieldDesc + "\"";
        });
        strJson += "}";
        rowData.push(JSON.parse(strJson));
    }
    return rowData;
}


[
    {
        "tabelName": "PageDeal", "tableTitle": "营业执照办理",
        "fun": [
            { "cn": "保存", "en": "create" },
            { "cn": "删除", "en": "delete" },
            { "cn": "修改", "en": "edit" },
            { "cn": "审核", "en": "Confirm", "img": "1.png", "p": "View" },
            { "cn": "验证", "en": "Confirm", "img": "1.png", "p": "Create" }
        ],
        "layout": [
            { "Empty": false, "colspan": 2, "FieldDesc": "企业名称", "FieldLength": "1000", "FieldName": "Name", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 2, "FieldDesc": "企业英文名称", "FieldLength": "1000", "FieldName": "EnName", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 2, "FieldDesc": "注册地址", "FieldLength": "2000", "FieldName": "Address", "FieldDataType": "varchar", "FieldValueDefault": "1" },
            { "Empty": false, "colspan": 2, "FieldDesc": "执照名称", "FieldLength": "1000", "FieldName": "PageName", "FieldDataType": "varchar", "control": "select", "nullvalidate": "notnull", "Mapping": "111", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "邮政编码", "FieldLength": "50", "FieldName": "ZipCode", "FieldDataType": "varchar", "FieldValueDefault": "1" },
            { "Empty": false, "colspan": 1, "FieldDesc": "联系电话", "FieldLength": "50", "FieldName": "Phone", "FieldDataType": "varchar", "control": "Radio", "nullvalidate": "notnull", "Custom": [{ "value": "1", "text": "f的" }], "FieldValueDefault": "1" },
            { "Empty": false, "colspan": 1, "FieldDesc": "联系电话", "FieldLength": "50", "FieldName": "MPPhone", "FieldDataType": "varchar", "FieldValueDefault": "1" },
            { "Empty": false, "colspan": 1, "FieldDesc": "身份证名称", "FieldLength": "100", "FieldName": "IDName", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "身份证号", "FieldLength": "50", "FieldName": "IDCode", "FieldDataType": "varchar", "FieldValueDefault": "1" },
            { "Empty": true, "colspan": 1 },
            { "Empty": false, "colspan": 2, "FieldDesc": "经营范围", "FieldLength": "5000", "FieldName": "Operate", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "注册类型", "FieldLength": "100", "FieldName": "RegisterType", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "注册类型", "FieldLength": "100", "FieldName": "EntType", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "成立日期", "FieldLength": "23", "FieldName": "StartDate", "FieldDataType": "datetime", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "有效期至", "FieldLength": "23", "FieldName": "EndDate", "FieldDataType": "datetime", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "登记批准机构", "FieldLength": "100", "FieldName": "Dept", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "注册号", "FieldLength": "100", "FieldName": "Code", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "注册资本（金）万元", "FieldLength": "53", "FieldName": "Money", "FieldDataType": "float", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "货币类型", "FieldLength": "10", "FieldName": "MoneyType", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "从业人数", "FieldLength": "10", "FieldName": "Count", "FieldDataType": "int", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "国际行业分类", "FieldLength": "10", "FieldName": "BType", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "经办人", "FieldLength": "100", "FieldName": "DoPerson", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "联系电话", "FieldLength": "10", "FieldName": "DoPPhone", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": false, "colspan": 1, "FieldDesc": "身份证号", "FieldLength": "10", "FieldName": "DoPIdCode", "FieldDataType": "varchar", "FieldValueDefault": "" },
            { "Empty": true, "colspan": 1 },
            { "tableName": "Agency_Enterprise", "file": "是", "colspan": 2, "rowsRef": "Guid", "rowsOrder": "Name", "column": [{ "FieldName": "TradeFirstType", "FieldPK": "否", "FieldDataType": "int", "FieldLength": "10", "FieldDecDigits": "0", "FieldNull": "空", "FieldValueDefault": "", "FieldDesc": "行业大类", "dropdown": "true" }, { "FieldName": "TradeSeType", "FieldPK": "否", "FieldDataType": "int", "FieldLength": "10", "FieldDecDigits": "0", "FieldNull": "空", "FieldValueDefault": "", "FieldDesc": "行业小类", "dropdown": "true" }, { "FieldName": "Region", "FieldPK": "否", "FieldDataType": "varchar", "FieldLength": "-1", "FieldDecDigits": "0", "FieldNull": "空", "FieldValueDefault": "", "FieldDesc": "经营范围" }, { "FieldName": "RegisterAuthority", "FieldPK": "否", "FieldDataType": "int", "FieldLength": "10", "FieldDecDigits": "0", "FieldNull": "空", "FieldValueDefault": "", "FieldDesc": "登记机关" }, { "FieldName": "GSRegion", "FieldPK": "否", "FieldDataType": "int", "FieldLength": "10", "FieldDecDigits": "0", "FieldNull": "空", "FieldValueDefault": "", "FieldDesc": "管片工商所" }] }],
        "file": [
            { "title": "附件", "sign": "F1" }
        ], "pamas": [
            { "type": "Send", "action": "Delete", "value": "Const&Type&1" },
            { "type": "Send", "action": "All", "value": "Receive&ProjGuid" },
            { "type": "Send", "action": "List", "value": "Const&Type&1" }
        ]
    }]



function loadEdit(editSetting) {

    var fun = editSetting[0].fun;
    var last = $("#lf em").last();
    $.each(fun, function (i,f) {//{ "cn": "审核", "en": "Confirm", "img": "1.png", "p": "View" }
        if(i>2)
            last.before('<em><input img="' + f.img + '" p="' + f.p + '" type="checkbox" style="vertical-align:text-top"  value="' + f.en + '" title="' + f.cn + '" /><label>' + f.cn + '</label></em>');
    });
    
    var filerows = $("#filerows");
    var file = editSetting[0].file;
    $.each(file, function (i, fr) {
        filerows.dynamicRow({});
        var input=filerows.nextAll().last().find("input");
        input.eq(0).val(fr.title);
        input.eq(1).val(fr.sign);
    });


    var tableJson = editSetting[0].layout;
    var table = $("#LayoutTable");
    table.children().remove();
    for (var i = 0; i < tableJson.length; i++) {
        var html = "<div style='width:74%;float:left'><span>" + tableJson[i].FieldDesc + "</span><div f='d' style='width:70%;float:right'>";
        
        if (tableJson[i].control == "select") {
            html += "<select><option>选项1</option></select>";
        }
        else if (tableJson[i].control == "Checkbox") {
            html += "<input type='checkbox' />选项1";
        }
        else if (tableJson[i].control == "Radio") {
            html += "<input type='radio' />选项1";
        }
        else
            html += "<input type='text' style='width:100%;' />";
        html += "</div></div>";

        if (tableJson[i].colspan == "2") {
            if (tableJson[i].tableName) {
                
                var rows = '<tr table="' + tableJson[i].tableName + '">';
                rows += "<td class='TdContent' style='cursor: pointer;'  colspan='2' data='" + JSON.stringify(tableJson[i]) + "' >";
                $.each(tableJson[i].column, function (x, row) {
                    if (row.dropdown)
                    {
                        rows += '<span class="selectDorpDown" style="font-size: 12px; margin-right: 10px;" onclick="selectRowsDropdown(this)" f="' + row.FieldName + '" v="' + JSON.stringify(row) + '">' + row.FieldDesc + '</span>';
                    }
                    else
                    {
                        rows += '<span style="font-size: 12px; margin-right: 10px;" onclick="selectRowsDropdown(this)" f="' + row.FieldName + '" v="' + JSON.stringify(row) + '">' + row.FieldDesc + '</span>';
                    }
                 
                });
                rows += '<div style="width: 25%; text-align: right; float: right;"><img onclick="resetAutorows(this)" src="img/report_edit.png"> <img onclick="editLayoutDelete(this)" src="img/cross.png"></div></td></tr>';
                table.append(rows);

            }
            else {
                table.append("<tr><td data='" + JSON.stringify(tableJson[i]) + "' class='TdContent' colspan='2' style='cursor:pointer'>" + html.replace("width:70%", "width:85%") + "<div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)' src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td></tr>");
            }
        }
        else {
            var tr = "<tr>";
            if (tableJson[i].Empty) {
                tr += "<td  class='TdContent' style='cursor:pointer'><div style='width:74%;float:left'><span></span><div f='d' style='width:70%;float:right'></div></div><div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)'  src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td>";
            }
            else {
                var html = "<div style='width:74%;float:left'><span>" + tableJson[i].FieldDesc + "</span><div f='d' style='width:70%;float:right'>";
                if (tableJson[i].control == "select") {
                    html += "<select><option>选项1</option></select>";
                }
                else if (tableJson[i].control == "Checkbox") {
                    html += "<input type='checkbox' />选项1";
                }
                else if (tableJson[i].control == "Radio") {
                    html += "<input type='radio' />选项1";
                }
                else
                    html += "<input type='text' style='width:100%;' />";
                html += "</div></div>";
                tr += "<td data='" + JSON.stringify(tableJson[i]) + "' class='TdContent' style='cursor:pointer'>" + html + "<div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)'  src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td>";
            }
            if (i + 1 < tableJson.length) {
                //tr += "<tr>";
                html = "<div style='width:74%;float:left'><span>" + tableJson[i + 1].FieldDesc + "</span><div f='d' style='width:70%;float:right'>";
                if (tableJson[i+1].control == "select") {
                    html += "<select><option>选项1</option></select>";
                }
                else if (tableJson[i+1].control == "Checkbox") {
                    html += "<input type='checkbox' />选项1";
                }
                else if (tableJson[i+1].control == "Radio") {
                    html += "<input type='radio' />选项1";
                }
                else
                    html += "<input type='text' style='width:100%;' />";
                html += "</div></div>";

                if (tableJson[i + 1].colspan == "2") {
                    tr += "<td  class='TdContent' style='cursor:pointer'><div style='width:74%;float:left'><span></span><div f='d' style='width:70%;float:right'></div></div><div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)'  src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td></tr>";
                }
                else {
                    if (tableJson[i + 1].Empty) {
                        tr += "<td  class='TdContent' style='cursor:pointer'><div style='width:74%;float:left'><span></span><div f='d' style='width:70%;float:right'></div></div><div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)'  src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td></tr>";
                    }
                    else {
                        tr += "<td data='" + JSON.stringify(tableJson[i + 1]) + "' class='TdContent' style='cursor:pointer'>" + html + "<div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)' src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td></tr>";
                    }
                    i++;
                }
            }
            else
                tr += "<td class='TdContent'></td></tr>";
            table.append(tr);
        }
    }

    $("#LayoutTable td").click(function () {
        $(".tdborder").removeClass("tdborder");
        $(this).addClass("tdborder");
    });
    //w119 W87 A65 a97 d100 D68 S83 s115
    $("#LayoutTable td").keypress(function () {

        if (event.keyCode == 119 || event.keyCode == 87) {
            editlayoutprev();
        }

        if (event.keyCode == 83 || event.keyCode == 115) {
            editlayoutnext();
        }
        if (event.keyCode == 65 || event.keyCode == 97) {
            editlayoutleft();
        }
        if (event.keyCode == 100 || event.keyCode == 68) {
            editlayoutright();
        }
    }).dblclick(function () {
        if ($(this).attr("data")) {
            $("#edit").css({ left: ($(window).width() - $("#edit").width()) / 2 }).slideDown();
            $("#edit").find(":text").val('');

            $("#edit").find(":radio,:checkbox").each(function () { $(this).prop("checked", false).next().addClass("ui-corner-left").removeClass("ui-state-active"); });

            current = $(this);

            var data = eval("[" + $(this).attr("data") + "]");
            var dat = data[0];
            $("#title").val(dat.FieldDesc);
            if (dat.control) {
                $('#' + dat.control).click();
                //$('#' + dat.control).next().addClass("ui-corner-left").removeClass("ui-state-active");

            }
            else {
                $('#text').click();

                $('#notnull').click();
            }

        }
    });
}



