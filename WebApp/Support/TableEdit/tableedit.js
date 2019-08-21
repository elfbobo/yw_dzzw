function tableEdit() {
    this.loadFildDiv = function (json, obj) {

        $.each(json, function (i, dat) {
            if (dat.FieldPK != "是") {
                var $divContent = $("<div />", { "class": "DivCotent", "style": "height:113px;margin:4px;width:240px;float:left" });
                var $divTop = $("<div />", { "class": "DivBlueTop", "style": "position:relative" });

                $divContent.append($divTop);
                var title = dat.FieldDesc + " | " + dat.FieldName + " | " + getFieldType(dat.FieldDataType, dat.FieldLength);
                var html = "<span>" + dat.FieldDesc + "</span> <span>| " + dat.FieldName + " | " + getFieldType(dat.FieldDataType, dat.FieldLength) + "</span>";
                $divTop.html(html).attr("title", title);
                $divTop.append(getEdit());
                // $divTop.append(getDelete());
                $divContent.append(getControl(dat.FieldName)); $divContent.append(getValidate(dat.FieldName));
                obj.append($divContent);
                $divContent.data("data", dat);

            }
        });

        var $divContent = $("<div />", { "class": "DivCotent", "style": "height:113px;margin:4px;width:240px;float:left" });
        var $divTop = $("<div />", { "class": "DivBlueTop", "style": "position:relative" });
        $divContent.append($divTop).css("line-height", "80px");
        $divContent.append('<input class="ui-button ui-widget ui-state-default ui-corner-all" role="button" aria-disabled="false" style="padding: 0.1em 0.5em;margin-left:30px;cursor:pointer"  type="button" value="下一步">');
        $divContent.append('<input class="ui-button ui-widget ui-state-default ui-corner-all" role="button" aria-disabled="false" style="padding: 0.1em 0.5em;margin-left:15px;cursor:pointer"  type="button" value="自定义">');
        obj.append($divContent);
    }

    //获取数据类型
    function getFieldType(name, length) {
        if (name == "varchar") {
            if (length == "-1")
                return "varchar(max)"
            else
                return "varchar(" + length + ")";
        }
        return name;
    }

    //获取控件操作
    function getControl(field) {
        var $div = $("<div />", { "id": field + "_" + Math.random() }).css("width", "100%");
        $div.html('<input checked="checked" type="radio" id="' + field + '_control1" name="' + field + '_control" value="text" /><label for="' + field + '_control1">Text</label><input onclick="srcCheck(this)" id="' + field + '_control2" name="' + field + '_control" value="select" type="radio" /><label for="' + field + '_control2">Select</label><input onclick="srcCheck(this)" id="' + field + '_control3" name="' + field + '_control" value="Radio" type="radio" /><label for="' + field + '_control3">Radio</label><input onclick="srcCheck(this)" id="' + field + '_control4" name="' + field + '_control" value="Checkbox" type="radio" /><label for="' + field + '_control4">Checkbox</label><input id="' + field + '_control5" name="' + field + '_control" value="Areatext" type="radio" /><label for="' + field + '_control5">Areatext</label>');
        $div.css("margin-left", "5px");
        return $div;
    }
    //获取验证操作
    function getValidate(field) {
        var did = $("<div>").css("width", "100%");
        var $div = $("<div />", { "id": field + "_" + Math.random() }).css("width", "100%");
        $div.html('<input  type="radio" id="' + field + '_validate1" name="' + field + '_validate" value="int" /><label for="' + field + '_validate1">整数</label><input  type="radio" id="' + field + '_validate2" name="' + field + '_validate" value="float" /><label for="' + field + '_validate2">浮点</label><input  type="radio" id="' + field + '_validate3" name="' + field + '_validate" value="email" /><label for="' + field + '_validate3">邮箱</label><input  type="radio" id="' + field + '_validate4" name="' + field + '_validate" value="phoene" /><label for="' + field + '_validate4">电话</label><input  type="radio" id="' + field + '_validate5" name="' + field + '_validate" value="idcode" /><label for="' + field + '_validate5">身份证</label>');
        var $div2 = $("<div />", { "id": field + "_" + Math.random() }).css("width", "100%");
        $div2.html('<input  type="radio" id="' + field + '_isnull1" name="' + field + '_isnull" value="null" /><label for="' + field + '_isnull1">必填</label><input checked="checked" type="radio" id="' + field + '_isnull2" name="' + field + '_isnull" value="notnull" /><label for="' + field + '_isnull2">非必填</label>')
        $div.css("margin-left", "5px"); $div2.css("margin-left", "5px");
        did.append($div); did.append($div2);
        return did;
    }

    //获取删除操作
    function getDelete() {
        var $img = $("<img />", { "style": "position:absolute;top:10px;right:2px;cursor:pointer", "src": "img/cross.png" });
        $img.click(function () {
            $(this).parent().parent().remove();
        });
        return $img;
    }

    //获取编辑操作
    function getEdit() {
        var $img = $("<img />", { "style": "position:absolute;top:10px;right:5px;cursor:pointer", "src": "img/edit.png" });
        $img.click(function () {
            var parent = $(this).parent().parent();
            var offset = parent.offset();
            current = parent;
            $("#edit").css({ "left": offset.left, "top": offset.top }).show(400);
            $("#desc").val(parent.find("span").eq(0).text());
        });
        return $img;
    }
    //*************************************************************************************
    this.SetList = function (json, obj) {
        var $gridContent = obj;

        var cfg = [];

        cfg.width = "98%";//99%
        cfg.height = 400;
        cfg.request = "data"; //默认ajax 
        cfg.rowsData = setRowData(json, 10);

        cfg.columns = [];
        cfg.columns.push({ field: $("#sortSelect option[pk]").text(), checkbox: true, width: "50px" });
        var width = (json.length - 1);
        b = false;
        $.each(json, function (i, dat) {
            //if (i == 0) {
            //    cfg.columns.push({ field: dat.FieldName, checkbox: true, width: "50px" });
            //}
            //else 
            {
                var wd = "";
                if (!b)
                { wd = "auto"; b = true; }
                else
                    wd = Math.ceil((1 / json.length) * 100) + "%";
                if (dat.FieldDataType == "int" || dat.FieldDataType == "datetime" || dat.FieldDataType == "float")
                    if (dat.Width)
                        cfg.columns.push({ field: dat.FieldName, name: dat.FieldDesc, width: dat.Width, align: "center", drag: true, order: true });
                    else
                        cfg.columns.push({ field: dat.FieldName, name: dat.FieldDesc, width: wd, align: "center", drag: true, order: true });
                else
                    if (dat.Width)
                        cfg.columns.push({ field: dat.FieldName, name: dat.FieldDesc, width: dat.Width, align: "left", drag: true, order: true });
                    else
                        cfg.columns.push({ field: dat.FieldName, name: dat.FieldDesc, width: wd, align: "left", drag: true, order: true });
            }
        });
       
        $gridContent.Grid(cfg);

    }

    function setRowData(json, count) {
        var rowData = [];
        for (var c = 0; c < count; c++) {
            var strJson = "{\"" + $("#sortSelect option[pk]").text() + "\":\"" + Math.random() + "\"";
          
            $.each(json, function (i, dat) {
                //if (i > 0)
              
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

    this.setEditLayout = function () {
        var table = $("#LayoutTable");
        for (var i = 0; i < tableJson.length; i++) {
            if (tableJson[i].FieldPK == "是")
                continue;
            var html = "<div style='width:74%;float:left'><span>" + tableJson[i].FieldDesc + "</span><div f='d' style='width:70%;float:right'><input type='text' style='width:100%;' /></div></div>";
            if (tableJson[i].FieldLength == "-1" || parseInt(tableJson[i].FieldLength) > 999 || tableJson[i].FieldDataType == "text") {
                table.append("<tr><td data='" + JSON.stringify(tableJson[i]) + "' class='TdContent' colspan='2' style='cursor:pointer'>" + html.replace("width:70%", "width:85%") + "<div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)' src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td></tr>");
            }
            else {
                var tr = "<tr><td data='" + JSON.stringify(tableJson[i]) + "' class='TdContent' style='cursor:pointer'>" + html + "<div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)'  src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td>";
                if (i + 1 < tableJson.length) {
                    html = "<div style='width:74%;float:left'><span>" + tableJson[i + 1].FieldDesc + "</span><div f='d' style='width:70%;float:right'><input type='text' style='width:100%;' /></div></div>";
                    if ((tableJson[i + 1].FieldLength == "-1" || parseInt(tableJson[i + 1].FieldLength) > 999 || tableJson[i + 1].FieldDataType == "test")) {

                        tr += "<td  class='TdContent' style='cursor:pointer'><div style='width:74%;float:left'><span></span><div f='d' style='width:70%;float:right'></div></div><div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)'  src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td></tr>";

                    }
                    else {
                        tr += "<td data='" + JSON.stringify(tableJson[i + 1]) + "' class='TdContent' style='cursor:pointer'>" + html + "<div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)' src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td></tr>";
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
                   // $('#text').next().addClass("ui-corner-left").removeClass("ui-state-active"); 
                    $('#notnull').click();
                   /// $('#notnull').next().addClass("ui-corner-left").removeClass("ui-state-active"); 
                }
               
            }
        });
    }



}

function editLayoutDelete(obj) {
    if (confirm("确定要删除吗")) {
        var $this = $(obj);
        var count = 0;
        if ($this.parent().parent().attr("colspan") == "2") {
            $this.parent().parent().parent().remove();

        }
        else {
            $this.parent().parent().find("span").text('');
            $this.parent().parent().find("div[f]").children().remove();
        }


    }
}

function oneRow(obj) {
    if (confirm("确定要单行布局吗")) {
        var $this = $(obj);
        if ($this.parent().parent().attr("colspan") == "2") {

        }
        else {
            var td = $this.parent().parent();
            var newTd;
            if (td.index() == 0) {
                newTd = td.next().clone(true);
                td.next().remove();
            }
            else {
                newTd = td.prev().clone(true);
                td.prev().remove();
            }
            td.attr("colspan", 2);
            var tr = $("<tr />");
            tr.append(newTd.attr("colspan", 2));
            td.parent().after(tr);
            tr.find("div[f]").width("85%");
            td.parent().find("div[f]").width("85%");
        }
    }
}

function twoRow(obj) {
    if (confirm("确定要双列布局吗")) {
        var $this = $(obj);
        if ($this.parent().parent().attr("colspan") == "2") {
            var td = $this.parent().parent();
            td.removeAttr("colspan");
            var newTd = $("<td />");
            newTd.click(function () {
                $(".tdborder").removeClass("tdborder");
                $(this).addClass("tdborder");
            });
            newTd.addClass("TdContent");
            newTd.append("<div style='float:right;width:25%;text-align:right'><img onclick='oneRow(this)' src='img/page.png'/><img onclick='twoRow(this)' src='img/page_copy.png'/><img onclick='repalceTd(this)' src='img/arrow_switch.png'/><img onclick='editLayoutDelete(this)' src='img/cross.png'/></div>");
            td.after(newTd);
            newTd.keypress(function () {

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
                    current = $(this);
                }
            });
            td.find("div[f]").width("70%");
            newTd.find("div[f]").width("70%");
        }

    }
}





function editlayoutprev() {
    var $this = $(".tdborder");
    var parent = $this.parent();
    if (parent.find("td").length > 1) {
        var prevtr;
        var pal = parent.prevAll();
        for (var i = 0; i < pal.length; i++) {
            if ($(pal[i]).index() >= 0 && $(pal[i]).find("td[colspan='2']").length <= 0) {
                prevtr = $(pal[i]);
                break;
            }
        }

        if (typeof (prevtr) != "undefined") {
            var ix = $this.index();
            var cuttd = $this.clone(true);
            var pvtd = prevtr.find("td").eq(ix).clone(true);
            $this.after(pvtd);
            $this.remove();
            prevtr.find("td").eq(ix).after(cuttd);
            prevtr.find("td").eq(ix).remove();
            cuttd.focus();
        }
    }
    else {
        if (parent.index() > 0) {
            var cpa = parent.clone(true);
            parent.prev().before(cpa);
            parent.remove();
            cpa.find("td").focus();
        }
    }
}

function editlayoutnext() {
    var $this = $(".tdborder");
    var parent = $this.parent();
    if (parent.find("td").length > 1) {
        var prevtr;
        var pal = parent.nextAll();
        for (var i = 0; i < pal.length; i++) {
            if ($(pal[i]).index() < $("#LayoutTable").find("tr").length && $(pal[i]).find("td[colspan='2']").length <= 0) {
                prevtr = $(pal[i]);
                break;
            }
        }

        if (typeof (prevtr) != "undefined") {
            var ix = $this.index();
            var cuttd = $this.clone(true);
            var pvtd = prevtr.find("td").eq(ix).clone(true);
            $this.after(pvtd);
            $this.remove();
            prevtr.find("td").eq(ix).after(cuttd);
            prevtr.find("td").eq(ix).remove();
            cuttd.focus();
        }
    }
    else {
        if (parent.index() < $("#LayoutTable").find("tr").length - 1) {
            var cpa = parent.clone(true);
            parent.next().after(cpa);
            parent.remove();
            cpa.find("td").focus();
        }
    }
}

function editlayoutleft() {
    var $this = $(".tdborder");
    var parent = $this.parent();

    if (parent.find("td").length > 1) {
        if ($this.index() > 0) {
            var ctd = $this.clone(true);
            $this.prev().before(ctd);
            $this.remove();
            ctd.focus();
        }
    }
}

function editlayoutright() {
    var $this = $(".tdborder");
    var parent = $this.parent();

    if (parent.find("td").length > 1) {
        if ($this.index() < 1) {
            var ctd = $this.clone(true);
            $this.next().after(ctd);
            $this.remove();
            ctd.focus();
        }
    }
}


function repalceTd(obj) {
    var old = $(".tdborder");
    var oldClone = old.clone(true);
    var target = $(obj).parent().parent();
    var targetClone = target.clone(true);
    if (old.attr("colspan") == "2") {
        targetClone.attr("colspan", "2");
        targetClone.find("div[f]").width("85%");
    }

    if (target.attr("colspan") == "2") {
        oldClone.attr("colspan", "2");
        oldClone.find("div[f]").width("85%");
    }

    if (!old.attr("colspan")) {
        targetClone.removeAttr("colspan");
        targetClone.find("div[f]").width("70%");
    }
    if (!target.attr("colspan")) {
        oldClone.removeAttr("colspan");
        oldClone.find("div[f]").width("70%");
    }
    old.after(targetClone);
    target.after(oldClone);
    old.remove();
    target.remove();
    targetClone.focus();
}


function searchTable(json) {
    var tr = ""
    for (var i = 0; i < json.length; i++) {
        tr += "<tr>"
        tr += "<td class='TdContent' data='" + JSON.stringify(json[i]) + "'><span>" + json[i].FieldDesc + "</span><div style='float:right;width:70%;text-align:right'>";
        if (json[i].FieldDataType == "int" || json[i].FieldDataType == "float" || json[i].FieldDataType == "datetime") {
            tr += "<select style='width:30%;' search><option value='='>等于</option><option>不等于</option><option value='<='>至多</option><option value='>='>至少</option></select>";
        }
        else
            tr += "<select style='width:30%;' search><option value='='>等于</option><option value='<>'>不等于</option><option selected='selected' value='like'>包含</option><option value='not like'>不包含</option></select>";
        tr += "<div f style='width:60%;margin-left:15px;float:right'><input type='text'   /></div></div></td>";
        if (i + 1 < json.length) {
            tr += "<td class='TdContent' data='" + JSON.stringify(json[i + 1]) + "'><span>" + json[i + 1].FieldDesc + "</span><div style='float:right;width:70%;text-align:right'>";
            if (json[i + 1].FieldDataType == "int" || json[i + 1].FieldDataType == "float" || json[i + 1].FieldDataType == "datetime") {
                tr += "<select style='width:30%;' search><option value='='>等于</option><option value='<>'>不等于</option><option value='<='>至多</option><option value='>='>至少</option></select>";
            }
            else
                tr += "<select style='width:30%;' search><option value='='>等于</option><option value='<>'>不等于</option><option selected='selected' value='like'>包含</option><option value='not like'>不包含</option></select>";
            tr += "<div f style='width:60%;margin-left:15px;float:right'><input type='text'  /></div></div></td></tr>"; i++;
        }
        else {
            tr += "<td class='TdContent'></td></tr>";
        }


    }
    return tr;
}

function checkSearch() {
    var json = [];
    $("#searchdd :checked").each(function () {
        json.push(JSON.parse($(this).val()));
    });
    $("#searchTable").html(searchTable(json));
    bindSearchTabel();

}

function bindSearchTabel() {
    $("#searchTable td").click(function () {
        $(".tdborder").removeClass("tdborder");
        $(this).addClass("tdborder");
    }).keypress(function () {
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
        var inx = $(this).index();
        if (inx == 0) {
            if ($(this).next().find("input").length > 0) {


                var tabletd = $("#searchTable td");
                var nulltd;
                for (var i = 0; i < tabletd.length; i++) {
                    if ($(tabletd[i]).find("input").length == 0)
                    { nulltd = tabletd[i]; break; }
                }
                if (nulltd) {
                    $(nulltd).after($(this).next().clone(true)); $(nulltd).remove();
                }
                else {
                    var $tr = $("<tr />");
                    $tr.append($(this).next().clone(true));
                    var td = $("<td />", { "class": "TdContent" });
                    $tr.append(td);
                    $(this).parent().after($tr);
                }
                $(this).next().remove();
                $(this).after($(this).clone(true));

            }
            else {
                $(this).next().remove();
                $(this).after($(this).clone(true));
            }
        }
        if (inx == 1) {
            if ($(this).prev().find("input").length > 0) {
                var tabletd = $("#searchTable td");
                var nulltd;
                for (var i = 0; i < tabletd.length; i++) {
                    if ($(tabletd[i]).find("input").length == 0)
                    { nulltd = tabletd[i]; break; }
                }
                if (nulltd) {
                    $(nulltd).after($(this).prev().clone(true)); $(nulltd).remove();
                }
                else {
                    var $tr = $("<tr />");
                    $tr.append($(this).prev().clone(true));
                    var td = $("<td />", { "class": "TdContent" });
                    $tr.append(td);
                    $(this).parent().before($tr);
                }
                $(this).prev().remove();
                $(this).after($(this).clone(true));
            }
            else {
                $(this).prev().remove();
                $(this).after($(this).clone(true));
            }
        }
    });
}

function addControl() {
    $("#addControl").css({ left: ($(window).width() - $("#addControl").width()) / 2 }).slideDown();
}

function addControlColse() {
    var current = $(".tdborder");
    var width = "48%";
    if (current.find("input[type='text'],select").length > 0) {
        var input = current.find("input,select").width("49%");
    }
    else {
        width = "100%";
        current.find("span").text($("#addfiled").find(":checked").next().text());

    }
    var text = $("#addcont :checked").val();
    var div = current.find("div[f]");
    switch (text.toLowerCase()) {
        case "text":
            div.append("<input type='text' style='width:" + width + ";height:23px;margin-left:2px' />");
            break;
        case "select":
            div.append("<select style='width:" + width + ";height:23px;margin-left:2px' ><option>demo1</option><option>demo1</option></select>");
            break;
        case "checkbox":
            div.append("<span><input type='checkbox' />demo1<input type='checkbox' />demo2</span>");
            break;
        case "radio":
            div.append("<span><input type='radio' />demo1<input type='radio' />demo2</span>");
            break;
        default:
            div.append("<input type='text' style='width:" + width + "' />");
            break;

    }
    $("#addControl").slideUp();
}

function CannelControl() {

    var current = $(".tdborder");
    var div = current.find("div[f]");

    if (div.find("span").length > 0) {
        div.find("span").remove();
        div.find("input[type='text'],select").width("100%");
    }
    if (div.find("input[type='text'],select").length == 2) {
        div.children().last().remove();
        div.children().first().width("100%");
    }
}


function AddFile() {
    $("#fileset").slideDown();
}

function CannleFile(path) {
    //var tr = $("#fileset").find("tr");
    //if (tr.length > 1) {

    //    tr.each(function (i) {
    //        if (i > 1) {
    //            var $td = $("#LayoutTable").find("td").eq(0).clone(true).removeAttr("data").attr("file","");
    //            $td.attr("colspan", "2");
    //            var div = $td.find("div").first();
    //            div.children().remove();
    //            //div.
    //            var id = "id_" + Math.random().toString().replace('.', '');
    //            $file = $("<div />").css({ height: "22px", width: "20%", float: "left" });
    //            div.append($file.append("<div style='width:66px;height:22px;' id='" + id + "' >" + $(this).find("input").eq(0).val() + "</div>"));

    //            var filecontent = "con_" + Math.random().toString().replace('.', '');
    //            $filcontent = $("<div />", { id: filecontent }).css({ float: "right", width: "78%" });
    //            div.append($filcontent);

    //            $("#LayoutTable").append($("<tr />").append($td));

    //        }
    //    });
    //    $("#LayoutTable div[id*='id_']").each(function (i) {
    //        var cfg = [];
    //        cfg.content = $(this).parent().next().attr("id");
    //        cfg.refGuid = Math.random();
    //        cfg.type = "database";
    //        cfg.applicationPath = path;
    //        //cfg.fileSign = $(this).find("input").eq(1).val();
    //        cfg.title = $(this).text();

    //        $("#" + $(this).attr("id")).uploadfile(cfg);

    //    });


    //}


    $("#fileset").slideUp();
}

function PreView() {
    var LayoutTable = $("#LayoutContent").clone();
    LayoutTable.find("img").remove();
    var div = LayoutTable.find("#laytoufun").find("div");
    LayoutTable.find("dl").hide();
    div.last().children().remove();
    div.last().append("<input type='button' value='保存' /> <input type='button' value='返回' />");
    LayoutTable.find("div[f]").each(function () {
        $(this).parent().next().remove();
        $(this).parent().width("100%");

    });
    var html = "<html><head><title>layout</title> <link href='style.css' rel='stylesheet' /></head><body>";
    html += LayoutTable.html();
    html += "</body></html>";
    $.ajax({
        type: "post",
        url: "PreView.ashx",
        data: { html: html, type: "preview" },
        success: function () {
            window.open("layout/layout.html?tt=" + Math.random());
        },
        asnyc: false,
        error: function (msg) { alert(msg.status); }
    });
}

function Download() {
    var LayoutTable = $("#LayoutContent").clone();
    LayoutTable.find("img").remove();
    var div = LayoutTable.find("#laytoufun").find("div");
    LayoutTable.find("dl").hide();
    div.last().children().remove();
    div.last().append("<input type='button' value='保存' /> <input type='button' value='返回' />");
    LayoutTable.find("div[f]").each(function () {
        $(this).parent().next().remove();
        $(this).parent().width("100%");

    });
    var html = "<html><head><title>layout</title> <link href='style.css' rel='stylesheet' /></head><body>";
    html += LayoutTable.html();
    html += "</body></html>";
    $.ajax({
        type: "post",
        url: "PreView.ashx",
        data: { html: html, type: "preview" },
        success: function () {
            window.location = 'PreView.ashx?type=download'
        },
        asnyc: false,
        error: function (msg) { alert(msg.status); }
    });
}

function upload() {
    $("#upload").show();
}
var funtype;
function uploadfun() {
    funtype = $(arguments[0]).parent();
    if (funtype.parent().attr("id") == "lf") {
        $("#pagename").parent().parent().show();
    }
    else {
        $("#pagename").parent().parent().hide();
    }
    $("#uploadfun").show();
}





function _close() {

    var data = JSON.parse(current.attr("data"));
    if ($("#title").val() != "") {
        current.find("span").eq(0).text($("#title").val());
        data.FieldDesc = $("#title").val();
        if ($("#tags").val() != "") {
            data.FieldValueDefault = $("#tags").val();
        }
    }
    var control = $("#_cont :checked");
    if (control.val() == "select") {
        if (current.find("select").not("[search]").length <= 0) {
            current.find("div[f]").html("<select style='width:100%'><option>demo1</option><option>demo2</option></select>");
        }
    }
    else if (control.val() == "Radio") {
        if (current.find("radio").length <= 0) {
            current.find("div[f]").html("<input type='radio' />demo1<input type='radio' />demo2");
        }
    }
    else if (control.val() == "Checkbox") {
        if (current.find("checkbox").length <= 0) {
            current.find("div[f]").html("<input type='checkbox' />demo1<input type='checkbox' />demo2");
        }
    }
    else {
        current.find("div[f]").html("<input type='text' />");
    }
    $("#edit").slideUp();
    data.control = control.val();
    data.valvalidate = $("input[name='validate']:checked").val();
    data.nullvalidate = $("input[name='isnull']:checked").val();
    
    current.attr("data", JSON.stringify(data));

}


function SetMapping() {
    var data = JSON.parse(current.attr("data"));
    var nodes = ztree.getSelectedNodes();
    if (nodes.length > 0) {
        data.Mapping = nodes[0].id;
    }
    else {
        var tr = $("#rows").nextAll();
        if (tr.length > 0) {
            var mapping = [];
            tr.each(function () {
                var input = $(this).find("input");
                mapping.push({ value: input.eq(0).val(), text: input.eq(1).val() });
            });
            data.Custom = mapping;
        }
    }
    current.attr("data", JSON.stringify(data));
    $("#mapping").hide();
}


function alignClose() {
    var align = $("#align").find("input[name='align']:checked").val();
    var order = $("#align").find("input[name='order']:checked").val();
    var drag = $("#align").find("input[name='drag']:checked").val();

    tdInx.attr("data", "{aglin:'" + align + "',order:'" + order + "',drag:'" + drag + "'}");
}


//************************************************************************************

function addAutoRows(json,html,table)
{
    $("#LayoutTable").find("tr[table='"+table+"']").remove();
    $("#LayoutTable").append("<tr table='" + table + "'><td data='" + json + "' class='TdContent' colspan='2' style='cursor:pointer'>" + html + "<div style='float:right;width:25%;text-align:right'><img onclick='resetAutorows(this)' src='img/report_edit.png'/> <img onclick='editLayoutDelete(this)' src='img/cross.png'/></div></td></tr>");
   
    $("#LayoutTable td").last().click(function () {
        $(".tdborder").removeClass("tdborder");
        $(this).addClass("tdborder");
    });
    //w119 W87 A65 a97 d100 D68 S83 s115
    $("#LayoutTable td").last().keypress(function () {

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
    });//.dblclick(function () {
    //    if ($(this).attr("data")) {
    //        $("#edit").css({ left: ($(window).width() - $("#edit").width()) / 2 }).slideDown();
    //        current = $(this);
    //        $("#title").val(current.find("span").text());
    //    }
    //});
}

function resetAutorows(obj)
{
    
    //document.write($(obj).parent().parent().attr("data"));
    var dat = eval("["+$(obj).parent().parent().attr("data")+"]");
    var nd = autoRowTree.getNodesByParam("id",dat[0].tableName);

    autoRowTree.selectNode(nd[0]);
    rowsSetting.callback.onClick(event, "rowztree", nd[0]);
   
   
  
    if (confirm("确定要重置吗")) {
        $.each(dat[0].column, function (i, d) {

            $('#' + d.FieldName).prop("checked", true);
            selectRows($('#' + d.FieldName));
            if (d.dropdown)
            {
                $("span[f='" + d.FieldName + "']").addClass("selectDorpDown");
            }
        });
        $("#rowsRef").val(dat[0].rowsRef);
        $("#rowsOrder").val(dat[0].rowsOrder);
        $("#rowsFile").val(dat[0].file);
        $("#autoRows").show();
    }
}
