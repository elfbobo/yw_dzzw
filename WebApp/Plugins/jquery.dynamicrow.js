//田飞飞  2011
(function ($) {
    $.fn.extend({
        dynamicRow: function (options) {
            var defaults = {
                onRowCreated: function () { },
                onRowDeletedBefore: function () { },
                onRowDeletedCompleted: function () { },
                type: "",
                applicationPath: "",
                key: "Guid",
                kv: ""
            };
            var options = $.extend(defaults, options);
            return this.each(function () {
                var o = options;
                var obj = $(this);
                var newRow = obj.clone(true);

                obj.parent().append(newRow);
                if (newRow.find(":radio,:checkbox").length > 0) {
                    newRow.children("td").each(function () {
                        var name = Math.random();
                        $(this).find(":radio,:checkbox").each(function () {
                            $(this).attr("name", name);
                        });
                    });
                }
                var files = newRow.find("div[file='true']");
                if (files.length > 0 && options.type !== "view") {

                    var dialog = ' <div id="dialogFile" class="dialog" style="min-height:100px;min-width:300px;position:absolute;display:none;max-width:700px">';
                    dialog += '<div style="text-align:right;"> <img src="' + options.applicationPath + '/images/delete_ico.gif" style="height:13px;width:13px;margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="$(\'#dialogFile\').hide(400);" /></div>';
                    dialog += '<div id="dialogfilecontent"></div> </div>';
                    if ($("#dialogFile").length <= 0)
                        $("body").append(dialog);

                    if ($("#filecontent").length <= 0)
                        $("body").append("<div style='display:none' id='filecontent'></div>");
                    var guids = '', rowGuid = '';
                    if (options.kv == "") {
                        if (obj.parent().data("guids")) {
                            guids = obj.parent().data("guids");
                        }
                        else {
                            $.ajax({
                                url: options.applicationPath + "/Handlers/DynamicRowHandler.ashx",
                                type: "post",
                                data: { type: "getguid" },
                                async: false,
                                success: function (data) {
                                    guids = data;
                                    obj.parent().data("guids", guids);
                                },
                                error: function ()
                                { alert("获取Guid失败,动态行附件上传失效"); }
                            });
                        }
                        rowGuid = guids.split(",")[0];

                        guids = guids.replace(rowGuid, "");
                        if (guids.charAt(0) == ",") {
                            guids = guids.substr(1, guids.length - 1);
                            obj.parent().data("guids", guids);
                        }
                    }
                    else { rowGuid = options.kv; }
                    newRow.find("td").first().before("<td style='display:none' node='" + options.key + "'><input value='" + rowGuid + "' /></td>")
                    files.each(function (i) {
                        var rn = Math.round(Math.random() * 10000000);
                        var $div = $("<div />").css({ "width": $(this).width(), "float": "left" });
                        $(this).wrap($div);
                        // $(this).parent().after("<a style='float:right;width:" + $(this).width() + "px;height:" + $(this).height() + "px;line-height:20px' class='fileBtn'>编辑</a>");
                        var $a = $("<input />", { type: "button" }).css({ "width": $(this).width() + 10, "height": $(this).height() + 2, "line-height": ($(this).height() - 5) + "px", "margin": "0px 0 0 20px" });
                        $a.addClass("fileBtn").val("编辑");
                        $(this).parent().after($a);

                        $(this).attr("id", "id_" + rn);

                        var cfg = [];
                        cfg.content = "dialogfilecontent";
                        cfg.refGuid = rowGuid;
                        cfg.type = "database";
                        cfg.applicationPath = options.applicationPath;
                        cfg.fileSign = i;
                        cfg.progress = false;
                        cfg.title = "附件";

                        cfg.deleted = true;
                        $a.click(function () {
                            var offset = $(this).offset();

                            $("#dialogfilecontent").children().remove();
                            cfg.success = function () { };
                            $("#dialogfilecontent").fileviewload(cfg);
                            if (offset.top > $("body").height() / 2)
                                $("#dialogFile").css({ left: offset.left + $(this).width() - $("#dialogFile").width(), top: offset.top - $("#dialogFile").height() - 5 }).show(400);
                            else
                                $("#dialogFile").css({ left: offset.left + $(this).width() - $("#dialogFile").width(), top: offset.top + 22 }).show(400);
                        });
                        cfg.success = function () {
                            alert("上传成功");
                        };
                        $(this).uploadfile(cfg);


                    });

                }

                newRow.bind("onRowCreated", options.onRowCreated);
                newRow.fadeIn("slow");
                if (options.type != "view")
                    newRow.find("td:last").children().eq(0).bind("onRowDeletedBefore", options.onRowDeletedBefore)
                        .data("onRowDeletedCompleted", options.onRowDeletedCompleted).bind("click", function () {
                            files.each(function () {
                                $(this).uploadify('destroy');
                            });
                            deleteRow($(this), newRow);
                        });
                newRow.trigger("onRowCreated", newRow);

            });

            function deleteRow(rowObj, newRow) {
                if (confirm("确定要删除此行吗")) {
                    $(rowObj).trigger("onRowDeletedBefore", [newRow, rowObj]);
                    options.onRowDeletedBefore(event, rowObj);
                    $(rowObj).parent().parent().fadeOut("fast", function () {
                        var clone = $(rowObj).data("onRowDeletedCompleted");
                        $(rowObj).parent().parent().remove();
                        clone();
                    });

                }

            };
        },
        getRowData: function (options) {
            var defaults = {
                hideInputId: "",
                self: false,
                type: "xml",
                xmlFormat: "",
                enCode: false
            };
            var options = $.extend(defaults, options);
            return this.each(function () {
                var o = options;
                var obj = $(this);
                var data = "";
                if (o.type == "xml") {
                    data = getRowDataXML(obj, options);
                }
                if (o.enCode) {
                    data = escape(data);
                }
                if (o.hideInputId != "") {
                    $("form").append("<input type='hidden' name='" + o.hideInputId + "' id='" + o.hideInputId + "' />");
                    $('#' + o.hideInputId).val(data);
                }
                else {
                    $("form").append("<input type='hidden' name='rowdata' id='rowdata' />");

                    $("#rowdata").val(data);
                }

            });
            function getRowDataXML(rowObj, options) {
                var xml = "", format = "<";
                if (options.xmlFormat != "") {
                    format = options.xmlFormat;
                }
                var nextRow;
                if (options.self) {
                    nextRow = rowObj.parent().children("tr").not(rowObj.prevAll());
                }
                else { nextRow = rowObj.nextAll() }
                $.each(nextRow, function (i, obj) {
                    if ($(obj).find("input").length > 0)
                        {
                    var dataRow = $(obj).children("td:not(:last)");
                    xml += format + "RowData>";
                    $.each(dataRow, function (j, td) {
                        if ($(td).find("table").length <= 0) {
                            if ($(td).attr("node")) {
                                xml += format + $(td).attr("node") + ">";
                            }
                            else {
                                xml += format + "v" + j + ">";
                            }
                            var el = $(td).find(":input,label,span");
                            if (el.length > 0)
                                var split = "^";
                            el.each(function (i) {

                                if (i > 0)
                                    xml += split;
                                if ($(this).attr("type") == "checkbox") {

                                    if ($(this).attr("checked") == "checked")
                                        xml += format + "checkbox>" + $(this).val() + format + "/checkbox>";
                                }
                                else if ($(this).attr("type") == "radio") {
                                    if ($(this).attr("checked") == "checked")
                                        xml += format + "radio>" + $(this).val() + format + "/radio>";
                                }
                                else if ($(this).is("textarea")) {
                                    xml += format + "textarea>" + $(this).val() + format + "/textarea>";
                                }
                                else if ($(this).is("label")) {
                                    xml += $(this).text();
                                }
                                else
                                    xml += $(this).val();

                            });
                            if ($(td).attr("node")) {
                                xml += format + "/" + $(td).attr("node") + ">";
                            }
                            else {
                                xml += format + "/" + "v" + j + ">";
                            }
                        }
                        else {
                            var trs = $(td).find("tr");
                            $.each(trs, function (m, tr) {
                                var tds = $(tr).find("td[node]");
                                $.each(tds, function (x, td) {
                                    if ($(td).attr("node")) {
                                        xml += format + $(td).attr("node") + ">";
                                    }
                                    else {
                                        xml += format + "v" + j + ">";
                                    }
                                    var el = $(td).find(":input,label,span");
                                    if (el.length > 0)
                                        var split = "^";
                                    el.each(function (i) {

                                        if (i > 0)
                                            xml += split;
                                        if ($(this).attr("type") == "checkbox") {

                                            if ($(this).attr("checked") == "checked")
                                                xml += format + "checkbox>" + $(this).val() + format + "/checkbox>";
                                        }
                                        else if ($(this).attr("type") == "radio") {
                                            if ($(this).attr("checked") == "checked")
                                                xml += format + "radio>" + $(this).val() + format + "/radio>";
                                        }
                                        else if ($(this).is("textarea")) {
                                            xml += format + "textarea>" + $(this).val() + format + "/textarea>";
                                        }
                                        else if ($(this).is("label")) {
                                            xml += $(this).text();
                                        }
                                        else
                                            xml += $(this).val();

                                    });
                                    if ($(td).attr("node")) {
                                        xml += format + "/" + $(td).attr("node") + ">";
                                    }
                                    else {
                                        xml += format + "/" + "v" + j + ">";
                                    }
                                });
                            });
                        }
                    });
                    xml += format + "/RowData>";
                }
                });
 
                return format + "xml>" + xml + format + "/xml>";
            }

        },
        SetRowData: function (options) {
            var defaults = {
                self: false,
                type: "Create",
                data: "<xml />",
                Key: "Guid",
                applicationPath: "",
                onRowCreated: function () { },
                onRowDeleted: function () { }
            };
            var options = $.extend(defaults, options);
            return this.each(function () {
                var objRow = $(this);
                if (options.data != "") {
                    var xmldom = parseXML(options.data);
                    $(xmldom).find("RowData").each(function () {
                        var kv = $(this).children(options.key).eq(0).text();
                        if (!options.self) {
                            options.kv = kv;
                            objRow.dynamicRow(options);
                        }
                        options.self = false;
                        var row;
                        if (objRow.find("table").length <= 0)
                            row = objRow.parent().find("tr").last();
                        else
                            row = objRow.parent().find("table").last();



                        var files = row.find("div[file='true']");
                        if (files.length > 0) {

                            var dialog = ' <div id="dialogFile" class="dialog" style="min-height:100px;min-width:300px;position:absolute;display:none;max-width:700px">';
                            dialog += '<div style="text-align:right;"> <img src="' + options.applicationPath + '/images/delete_ico.gif" style="height:13px;width:13px;margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="$(\'.dialog\').hide(400);" /></div>';
                            dialog += '<div id="dialogfilecontent"></div> </div>';
                            if ($("#dialogFile").length <= 0)
                                $("body").append(dialog);

                            files.each(function (i) {
                                var $a = $("<a />").css({ "width": $(this).width(), "height": $(this).height(), "line-height": $(this).height() + "px" });
                                $a.addClass("fileBtn").text("下载");
                                $(this).parent().append($a);
                                $(this).remove();

                                $a.click(function () {

                                    var dat = [];
                                    $.ajax({
                                        url: options.applicationPath + "/Handlers/UploadFilesHandler.ashx",
                                        type: "post",
                                        data: { type: 'get', 'RefGuid': kv, "FileSign": i },
                                        async: false,
                                        success: function (data) {
                                            dat = eval(data);
                                        },
                                        error: function (msg)
                                        { alert("获取附件失败" + msg.status); }
                                    });
                                    if (dat.length > 0) {
                                        if (dat.length == 1) {
                                            window.location = options.applicationPath + "/Handlers/UploadFilesHandler.ashx?type=download&guid=" + dat[0].Guid;
                                        }
                                        else {
                                            var cfg = [];

                                            cfg.refGuid = kv;
                                            cfg.type = "database";
                                            cfg.applicationPath = options.applicationPath;
                                            cfg.fileSign = i;

                                            var offset = $(this).offset();

                                            $("#dialogfilecontent").children().remove();
                                            $("#dialogfilecontent").fileviewload(cfg);
                                            if (offset.top > $("body").height() / 2)
                                                $("#dialogFile").css({ left: offset.left + $(this).width() - $("#dialogFile").width(), top: offset.top - $("#dialogFile").height() - 5 }).show(400);
                                            else
                                                $("#dialogFile").css({ left: offset.left + $(this).width() - $("#dialogFile").width(), top: offset.top + 22 }).show(400);
                                        }
                                    }
                                });

                            });

                        }


                        $(this).children().each(function (j) {
                            var td = $(row).find("td[node='" + $(this).context.nodeName + "']");
                            if (td) {

                                if ($(this).children().length <= 0) {
                                    if (options.type.toLowerCase() == "create") {

                                        $(td).children().val($(this).text().replace("0:00:00", ""));

                                    }
                                    if (options.type.toLowerCase() == "view")
                                        $(td).children().text($(this).text().replace("0:00:00", ""));
                                }
                                else {
                                    var val = "";
                                    $(this).children().each(function () {
                                        if ($(this).context.nodeName == "textarea") {
                                            if (options.type.toLowerCase() == "create")
                                                $(td).children().text($(this).text().replace("0:00:00", ""));
                                            if (options.type.toLowerCase() == "view")
                                                val += $(this).text().replace("0:00:00", "");
                                        }
                                        if ($(this).context.nodeName == "radio" || $(this).context.nodeName == "checkbox") {
                                            if (options.type.toLowerCase() == "create")
                                                $(td).find("[value='" + $(this).text().replace("0:00:00", "") + "']").eq(0).attr("checked", "checked");
                                            if (options.type.toLowerCase() == "view")
                                                val += $(this).text().replace("0:00:00", "") + "    ";
                                        }
                                        else {
                                            if (options.type.toLowerCase() == "create")
                                                $(td).children().attr("value", $(this).text().replace("0:00:00", ""));
                                            if (options.type.toLowerCase() == "view")
                                                val += $(this).text().replace("0:00:00", "");
                                        }
                                        if (options.type.toLowerCase() == "view")
                                            $(td).children().text(val);
                                    });
                                }
                            }
                        });
                    });
                }
            });
            function parseXML(xml) {
                var xmldom = null
                if (navigator.userAgent.toLowerCase().indexOf("msie") != -1) {
                    xmldom = new ActiveXObject("Microsoft.XMLDOM");
                    xmldom.loadXML(xml);
                }
                else
                    xmldom = new DOMParser().parseFromString(xml, "text/xml");
                return xmldom;
            }
        }


    });
})(jQuery);
