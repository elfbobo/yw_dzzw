
var h = 0;
var tm = 70 + 100;
if (h < 400)
    h = 400;
if (window.parent.$winh)
    h = window.parent.$winh - tm;
else if (window.parent.parent.$winh)
    h = window.parent.parent.$winh - tm - 30;
else if (window.parent.parent.parent.$winh)
    h = window.parent.parent.parent.$winh - tm;
else
    h = $(window).height() - tm;
// alert(h);
var url = '';
$(document).ready(function () {
    $("table").attr("cellpadding", "0").attr("cellspacing", "0");
    $("#win").hide();

    GetProjNameByProjGuid();
    InfoConfirm();
    $("body").bind("mousedown", onBodyMouseDown);

    function onBodyMouseDown(event) {
        if (!(event.target.id == "win" || $(event.target).parents("#win").length > 0)) {
            $("#win").slideUp();

        }

    }
});



//根据页面ProjGuid获取项目名称[{ProjGuid:'',ProjName:''}]
function GetProjNameByProjGuid() {
    if ($('#ProjGuid').length > 0 && $('#ProjGuid').val() != "") {
        var projJson
        $.ajax({
            type: "POST",
            url: appPath + "/Handlers/GetProjNameHandler.ashx",
            async: false,
            data: { projGuid: $('#ProjGuid').val() },
            success: function (data) {
                projJson = eval(data);
            },
            error: function (data) {
                alert("数据请求错误，错误类型：" + data.status);
            }
        });

        if ($('#ProjGuid').prevAll().length == 0) {
            $('#ProjGuid').before(projJson[0].ProjName);
        }
        if ($('#ProjGuid').prevAll("#ProjName").length > 0) {
            $("#ProjName").val(projJson[0].ProjName);
        }
    }
}

///View页审核功能
function InfoConfirm() {
    url = window.location.href;//获取当前url并格式化绝对路径
    if (url.toLowerCase().indexOf("/project/") > -1) {
        url = "/project/" + url.toLowerCase().split('/project/')[1];
    }
    if (url.toLowerCase().indexOf("/industrymanager/") > -1) {
        url = "/IndustryManager/" + url.toLowerCase().split('/industrymanager/')[1];
    }
    if ($("#confirm").length > 0 && typeof (infoStatus) != "undefined") {
        GetConfirmInfo();
        ReSetConfirm(infoStatus);
        $("#confirm").click(function () {
            if (confirm("确定审核吗")) {
                var $div = InitConfirm("1", '审核');
                $("body").append($div);
                $div.find("img").attr('src', appPath + '/images/cross.png');
                $div.slideDown();
            }
        });
        $("#reback").click(function () {
            var $div = InitConfirm("2", '退回');
            $("body").append($div);
            $div.find("img").attr('src', appPath + '/images/cross.png');
            $div.slideDown();
        });

        $("#cannelConfirm").click(function () {
            cannelConfirm();
        });

    }
}

//审核或取消审核存储
function InfoAction(status, remark) {
    //remark = $("#confirmText").text();
    if (remark == "" || typeof (remark) == "undefined" || remark == "请输入您的意见") {
        alert("请输入您的意见");
        return false;
    }
    $.ajax({
        type: "POST",
        url: appPath + "/Handlers/CommonHanlder.ashx",
        async: false,
        data: { type: "confirm", table: table, status: status, remark: remark, projguid: $('#ProjGuid').val(), refguid: guid, url: escape(url) },
        success: function (data) {
            alert("信息操作成功");
            ReSetConfirm(status);
            GetConfirmInfo();
        },
        error: function (data) {
            alert("数据请求错误，错误类型：" + data.status);
        }
    });
}

//根据状态重置页面审核功能按钮
function ReSetConfirm(status) {

    if (status == "0") {
        $('#confirm').show();
        $('#reback').show();
        $('#cannelConfirm').hide();
        $('#edit').show();
        $('#del').show();
    }
    else if (status == "1") {
        $('#confirm').hide();
        $('#reback').hide();
        $('#cannelConfirm').show();
        $('#edit').hide();
        $('#del').hide();
    }
    else if (status == "2") {
        $('#confirm').hide();
        $('#reback').hide();
        $('#cannelConfirm').hide();
        $('#edit').show();
        $('#del').show();

    }
}

///初始化页面选择框
function InitConfirm(action, title) {
    var $div = $("<div />");
    var $head = $("<div />");
    var $title = $("<span />");
    var $divContent = $("<div />");
    var $img = $("<img />");
    var $btn = $("<input />").attr("type", "button").addClass("fileBtn");
    var $textarea = $("<input />", { id: "confirmText" }).css({ width: 390, height: 240, overflow: "hidden", color: "gray", "vertical-align": "top" });
    $div.addClass("dialog").width(400).css({ top: 32, left: $(window).width() - 410 });
    $head.addClass("head");
    $title.addClass("title").val('信息操作');
    $div.append($head); $head.append($title);
    $divContent.height(250).css({ "text-align": "center", "margin-top": "2px" });
    $btn.css({ flaot: "right", margin: "3px 30px 0 0" }).val(title).click(function () { InfoAction(action, $textarea.val()); $div.slideUp().remove(); });
    $head.append($btn);
    $div.append($divContent).append($img);
    $img.css({ position: 'absolute', top: 5, right: 5, cursor: 'pointer' });
    $textarea.focusin(
        function () {
            if ($(this).val() == "请输入您的意见")
                $(this).css("color", "black").val('');
        }).focusout(
        function () {
            if ($(this).val() == '') {
                $(this).css("color", "gray").val('请输入您的意见');
            }
        }
        ).val('请输入您的意见');
    $divContent.append($textarea);
    $img.click(function () {
        $div.remove();
    });

    return $div;
}


function cannelConfirm() {
    if (confirm("确定要取消审核吗")) {
        $.ajax({
            type: "POST",
            url: appPath + "/Handlers/CommonHanlder.ashx",
            async: false,
            data: { table: table, refGuid: guid, type: "cannel", url: url, projguid: $('#ProjGuid').val() },
            success: function (data) {
                alert("操作成功");
                ReSetConfirm("0");
            },
            error: function (data) {
                alert("数据请求错误，错误类型：" + data.status);
            }
        });
    }
}


//获取审核信息记录
function GetConfirmInfo() {
    $.ajax({
        type: "POST",
        url: appPath + "/Handlers/CommonHanlder.ashx",
        async: false,
        data: { table: table, refGuid: guid, type: "getinfo", projguid: $('#ProjGuid').val() },
        success: function (data) {
            var json = eval(data);

            if (json.length > 0) {

                var $a = $("<a />").css({ cursor: "pointer" });
                $a.append("<img src='" + appPath + "/images/monitor.png' style=\"vertical-align:text-bottom;height:13px;width:13px\" />&nbsp;查看意见");
                $(".MenuRight").append($a);
                $a.click(function () {
                    var $div = InitConfirm("", "");
                    $div.find("input[type='button']").remove();
                    $div.find("img").attr('src', appPath + '/images/cross.png');
                    var $content = $div.find("#confirmText").parent();
                    $content.css({ "line-height": "25px", "overflow-y": "auto" });
                    $div.find("#confirmText").remove();
                    $.each(json, function (i, dat) {
                        $content.append("<div style='width:97%;text-align:left;padding-left:5px'><span style='font-size:12px' title='" + dat.date + "'>" + dat.date + "</span><span style='font-size:12px;font-weight:bold'>" + (dat.status == "1" ? " 审核" : " 退回") + "意见:</span></div>");
                        $content.append("<div style='width:97%;text-align:left;padding-left:10px;border-bottom:1px dashed gray'>" + dat.remark + "</div>");
                    });
                    $("body").append($div);
                });

            }
        },
        error: function (data) {
            alert("数据请求错误，错误类型：" + data.status);
        }
    });
}


//判断该项目是否存在(提示)
function IsExistProject(table, projGuid) {
    var isProj;
    $.ajax({
        type: "POST",
        url: appPath + "/Handlers/GetIsProjNameHandler.ashx",
        async: false,
        data: { projGuid: projGuid, table: table },
        success: function (data) {
            isProj = data;
        },
        error: function (data) {
            alert("数据请求错误，错误类型：" + data.status);
        }
    });

    if (isProj == "True") {
        $("#ProjName").val("");
        $('#ProjGuid').val("");
        alert("该项目已存在！,请在信息列表查询该项目");
    }
    return isProj;
}

//获取项目的建设单位名称
function GetProjCstrctUnitName(projGuid) {
    var val = "";
    $.ajax({
        type: "POST",
        url: appPath + "/Handlers/GetProjCstrctUnit.ashx",
        async: false,
        data: { projGuid: projGuid },
        success: function (data) {
            val = data;
        },
        error: function (data) {
            alert("数据请求错误，错误类型：" + data.status);
        }
    });
    return val;
}

//设置项目里程碑的在办时间
function InitialMileStoneHandlingDate(guid, column) {
    $.ajax({
        type: "POST",
        url: appPath + "/Handlers/MileStoneHandle.ashx",
        async: false,
        data: { Guid: guid, Column: column },
        success: function (data) {
        },
        error: function (data) {
            alert("数据请求错误，错误类型：" + data.status);
        }
    });
}
//设置项目里程碑的开工竣工时间
function InitialMileStoneHandlingDate(guid, column, worktime) {
    $.ajax({
        type: "POST",
        url: appPath + "/Handlers/MileStoneHandle.ashx",
        async: false,
        data: { Guid: guid, Column: column, WorkTime: worktime },
        success: function (data) {
        },
        error: function (data) {
            alert("数据请求错误，错误类型：" + data.status);
        }
    });
}





//判断该项目是否存在（返回bool值）
function IsProject(table, projGuid) {
    var isProj;
    $.ajax({
        type: "POST",
        url: appPath + "/Handlers/GetIsProjNameHandler.ashx",
        async: false,
        data: { projGuid: projGuid, table: table },
        success: function (data) {
            isProj = data;
        },
        error: function (data) {
            alert("数据请求错误，错误类型：" + data.status);
        }
    });
    return isProj;
}



function Config() {
    this.h = h;



    this.deleteData = function () {
        $('#del').bind("click", function () {
            if (confirm("确定要执行删除吗")) {
                $('#DelButton').click();
            }
        });
    }

    this.saveData = function (callback) {
        $('#save').click(function () {
            if (confirm("确定你要保存吗")) {
                if (formCore.FormVildateSubmit()) {
                    var b = true;
                    if (callback)
                        b = callback();

                    if (b || typeof (b) == "undefined")
                        $("#SaveButton").click();
                }
            }
        });
    }

    this.search = function () {

        $("#search").bind("click", function () {
            $("#win").css("top", "30px");
            $('#win').show(400);
        });
        $("#btn_r").bind("click", function () {

            $('#win').find('[type="checkbox"],[type="radio"]').prop("checked", false);

        });
        $("#btn_s").bind("click", function () {
            var sql = '';
            $('#win').find(':input[type!="button"]').each(function () {
                if ($(this).val() != "") {

                    if ($(this).attr("datatype") == "datetime" || $(this).attr("datatype") == "int" || $(this).attr("datatype") == "float" || $(this).attr("numeric") == "int")
                        sql += " and " + $(this).attr("id") + "='" + $(this).val() + "' ";
                    else
                        sql += " and " + $(this).attr("id") + " like '%" + $(this).val() + "%' ";

                }
            });

            cfg.where = sql;
            $("#grid").Grid(cfg);
            $('#win').slideUp();
        });
    }
}

