


function ListPage(options) {

    var opt = options[0];
    var listDesigner = '';

    this.CreateList = function () {
        $("form").append("<input type='hidden' id='listaspx' name='listaspx' />");
        $('#listaspx').val(SetListHead() + SetListBody() + SetListScript());
        $("form").append("<input type='hidden' id='listcs'  name='listcs' />");
        $('#listcs').val(SetListCS());
        $("form").append("<input type='hidden' id='listdesigner' name='listdesigner' />");
        $('#listdesigner').val(SetListDesigner());

    }


    function SetListHead() {
        var html = "";
        html += '<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="@@.List" %>';
        html += '\n\n<!DOCTYPE html>\n\n';
        html += '<html xmlns="http://www.w3.org/1999/xhtml">';
        html += '\n<head >';
        html += '\n<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>';
        html += '\n\t<title></title>';
        html += '\n\t<link href="<%=Yawei.Common.AppSupport.AppPath %>/Content/Form.css" rel="stylesheet" />';
        html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery.min.js" type="text/javascript"></script>';
        html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>';
        html += '\n\t<link href="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery.Grid/themes/grid_gray.css" rel="stylesheet" />';
        html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/DatePicker/WdatePicker.js" type="text/javascript"></script> ';
        html += '\n</head>\n';
        return html;
    }


    function SetListBody() {
        var html = '';
        html += "<body>";
        html += '\n\t<form id="form1" runat="server">';
        html += '\n\t<div style="width:100%; text-align:center">';
        html += '\n\t<div class="FormMenu">';
        html += '\n\t\t<div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：' + opt.tableTitle + ' </div>';
        html += '\n\t\t<div class="MenuRight">';
        html += '\n\t\t\t';
        html += '\n\t\t\t<a style="cursor:pointer" id="search" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;查询</a>';
        var fun = opt.fun;
        var createUrl = '';
        var funButton = '';
        $.each(fun, function (i, dat) {
            if (dat.en == "create") {

                var p = opt.pamas;
                for (var l = 0; l < p.length; l++) {
                    if (p[l].type == "Send" && (p[l].action == "Create" || p[l].action == "All")) {
                        var tm = p[l].value.split('&');
                        if (tm[0] == "Receive") {
                            if (createUrl != "")
                                createUrl += '&' + tm[1] + '=<%=Request["' + tm[1] + '"]%>';
                            else
                                createUrl += tm[1] + '=<%=Request["' + tm[1] + '"]%>';
                        }
                        if (tm[0] == "Const") {
                            if (createUrl != "")
                                createUrl += '&' + tm[1] + '=' + tm[2];
                            else
                                createUrl += tm[1] + '=' + tm[2];
                        }

                    }
                }
                if (createUrl == "")
                    createUrl = "create.aspx";
                else
                    createUrl = "create.aspx?" + createUrl;
                html += '\n\t\t\t<%if(CurrentUser.HasSave()) {%>';
                //html += '\n\t\t\t<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/g.gif" width="1px" height="10px" />';
                html += '\n\t\t\t<a style="cursor:pointer" href="' + createUrl + '" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/add.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;新建</a>';
                html += "\n\t\t\t<%} %>";
            }
            else if (dat.en == "delete") {
                html += '\n\t\t\t<%if(CurrentUser.HasDelete()) {%>';
                //html += '\n\t\t\t<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/g.gif" width="1px" height="10px" />';

                html += '\n\t\t\t&nbsp;<a style="cursor:pointer" onclick="deleteRow()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/delete_ico.gif" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>';
                html += "\n\t\t\t<%} %>";
            }
            else {
                html += '\n\t\t\t';
                //html += '\n\t\t\t<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/g.png" width="1px" height="10px" />';
                html += '\n\t\t\t&nbsp;<a style="cursor:pointer" onclick="' + dat.en + '_click()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/' + dat.img + '" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;' + dat.cn + '</a>';
                html += "\n\t\t\t";
                funButton += '\n\t\t<asp:Button runat="server" ID="_' + dat.en + '" OnClick="' + dat.en + '_Click" style="display:none" />';
                listDesigner += "\n\n\t\tprotected global::System.Web.UI.WebControls.Button " + dat.en + ";";
            }
        });


        html += '\n\t\t</div>';
        html += "\n\t</div>";
        html += '\n\t<div id="grid" style="margin:9px 0px 0px 12px"></div>';
        html += "\n\t</div>";
        html += SetListSearchWin();

        html += funButton;
        html += '\n\t</form>';
        html += "\n</body>\n</html>\n";
        return html;
    }



    function SetListSearchWin() {
        var html = '\n\n\t';
        html += '<div id="win" class="SearchWin" >';
        html += '\n\t\t<table class="table" cellpadding="0" cellspacing="0">';
        var dic = opt.Search;
        $.each(dic, function (i, dat) {
            if (i % 2 == 0)
                html += '\n\t\t\t<tr>';
            if (!dat.Empty) {
                var validate = '';
                if (dat.valvalidate == 'int') {
                    validate += ' int="true" ';
                }
                if (dat.valvalidate == 'flaot') {
                    validate += ' flaot="true" ';
                }
                if (dat.valvalidate == 'email') {
                    validate += ' email="true" ';
                }
                if (dat.valvalidate == 'phoene') {
                    validate += ' tell="true" ';
                }
                if (dat.valvalidate == 'idcode') {
                    validate += ' card="true" ';
                }

                if (dat.control == "select") {
                    html += '\n\t\t\t\t<td class="TdLabel">' + dat.FieldDesc + '</td>';
                    html += '\n\t\t\t\t<td class="TdContent"><asp:DropDownList ' + validate + ' runat="server" action="' + dat.Action + '"  datatype="' + dat.FieldDataType + '" ID="' + dat.FieldName + '"></asp:DropDownList></td>';
                    listDesigner += '\n\n\t\tprotected global::System.Web.UI.WebControls.DropDownList ' + dat.FieldName + ';';
                }
                else if (dat.control == "Checkbox") {
                    html += '\n\t\t\t\t<td class="TdLabel">' + dat.FieldDesc + '</td>';
                    html += '\n\t\t\t\t<td class="TdContent"><asp:CheckBoxList ' + validate + ' RepeatDirection="Horizontal" action="' + dat.Action + '" datatype="' + dat.FieldDataType + '" RepeatLayout="Flow"  runat="server" ID="' + dat.FieldName + '"></asp:CheckBoxList></td>';
                    listDesigner += '\n\n\t\tprotected global::System.Web.UI.WebControls.CheckBoxList ' + dat.FieldName + ';';
                }
                else {
                    if (dat.FieldDataType.toLowerCase() == "datetime") {
                        html += '\n\t\t\t\t<td class="TdLabel">' + dat.FieldDesc + '</td>';
                        html += '\n\t\t\t\t<td class="TdContent"><input ' + validate + ' class="wdate" onfocus="WdatePicker()" action="' + dat.Action + '" datatype="' + dat.FieldDataType + '" type="text"  id="' + dat.FieldName + '"/></td>';
                       // listDesigner += '\n\n\t\tprotected global::System.Web.UI.WebControls.TextBox ' + dat.FieldName + ';';
                    }
                    else {
                        html += '\n\t\t\t\t<td class="TdLabel">' + dat.FieldDesc + '</td>';
                        html += '\n\t\t\t\t<td class="TdContent"><input ' + validate + ' type="text" action="' + dat.Action + '" datatype="' + dat.FieldDataType + '"  id="' + dat.FieldName + '"/></td>';
                      //listDesigner += '\n\n\t\tprotected global::System.Web.UI.WebControls.TextBox ' + dat.FieldName + ';';
                    }
                }
            }
            else {
                html += '\n\t\t\t\t<td class="TdLabel">&nbsp;</td>';
                html += '\n\t\t\t\t<td class="TdContent">&nbsp;</td>';
            }

            if ((i - 1) % 2 == 0)
                html += '\n\t\t\t</tr>';
        });
        html += '\n\t\t\t<tr>';
        html += '\n\t\t\t\t<td class="TdContent" style="text-align:center" colspan="4"><input style="width:45px" type="button" class="searchbtn" id="btn_s" value="查询" /><input class="searchreset" type="button" style="width:45px"  id="btn_r" value="重置" /></td>';
        html += '\n\t\t\t</tr>';
        html += '\n\t\t</table>\n\n\t</div>';
        return html;
    }

    function SetListDesigner() {
        var code = '';
        code += 'namespace @@\n{';
        code += '\n\n\tpublic partial class List {';
        code += '\n\n\t\tprotected global::System.Web.UI.HtmlControls.HtmlForm form1;';
        code += listDesigner;
        code += '\n\n\t}\n}';
        return code;
    }

    function SetListCS() {
        var code = '';
        code += 'using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Web;\nusing System.Web.UI;\nusing System.Web.UI.WebControls;\nusing Yawei.SupportCore;';
        code += '\n\nnamespace @@\n{';
        code += '\n\tpublic partial class List : Yawei.Common.SharedPage\n\t{';
        code += '\n\t\tprotected void Page_Load(object sender, EventArgs e)\n\t\t{';
        var dic = opt.Search;
        var b = false;
        $.each(dic, function (i, dat) {
            if (dat.control == "select") {
                if (!b) {
                    code += '\n\t\t\t#region 初始化信息\n\n\t\t\tif (!IsPostBack)\n\t\t\t{';
                    code += '\n\t\t\t\tSysFormCore viewForm = new SysFormCore();';
                    b = true;
                }
                if (dat.Custom) {
                    $.each(dat.Custom, function (j, c) {

                        if (j == 0) {
                            code += '\n\t\t\t\tListItem item = new ListItem("' + c.text + '","' + c.value + '");';
                            code += '\n\t\t\t\tthis.' + dat.FieldName + '.Items.Add(item);';
                        }
                        else {
                            code += '\n\t\t\t\t item = new ListItem("' + c.text + '","' + c.value + '");';
                            code += '\n\t\t\t\tthis.' + dat.FieldName + '.Items.Add(item);';
                        }
                    });
                }
                if (dat.Mapping)
                    code += '\n\t\t\t\tviewForm.SetDropDownListValue(' + dat.FieldName + ', "' + dat.Mapping + '", true); ';
            }
            if (dat.control == "Checkbox") {
                if (!b) {
                    code += '\n\n\t\t\t#region 初始化信息\n\n\t\t\tif (!IsPostBack)\n\t\t\t{';
                    code += '\n\n\t\t\t\tSysFormCore viewForm = new SysFormCore();';
                    b = true;
                }
                if (dat.Custom) {
                    $.each(dat.Custom, function (j, c) {


                        code += '\n\t\t\t\tListItem item = new ListItem("' + c.text + '","' + c.value + '");';
                        code += '\n\t\t\t\tthis.' + dat.FieldName + '.Items.Add(item);';
                    });
                }
                if (dat.Mapping)
                    code += '\n\t\t\tviewForm.SetCheckBoxListCode(' + dat.FieldName + ', "' + dat.Mapping + '"); ';

            }

        });
        if (b)
            code += '\n\t\t\t}\n\n\t\t\t#endregion';
        code += '\n\n\t\t}';
        $.each(opt.fun, function (i, fun) {
            if (i > 1) {
                code += '\n\t\tprotected void ' + fun.en + '_Click(object sender, EventArgs e)\n\t\t{';
                code += '\n\n\t\t}'
            }
        });
        code += '\n\t}\n}';

        return code;
    }

    function SetListScript() {

        var list = opt.List[0];

        var html = '\n<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js"></script>';
        html += '\n<script type="text/javascript">\n';
        html += '\n\n\tvar cfg = [];';
        html += '\n\tcfg.connectionName = "";';
        html += '\n\tcfg.connectionString = "";';
        html += '\n\tcfg.providerName = "";';
        html += '\n\tcfg.tableName = "' + opt.tabelName + '";';
        html += '\n\tcfg.sortName = "' + list.sort + '";';
        html += '\n\tcfg.order = "desc";';
        html += '\n\tcfg.pageCount = ' + list.PageCount + ';';
        html += '\n\tcfg.pageSelect = [5, 15, 20, 50];';
        html += '\n\tcfg.where = "";';
        html += '\n\tcfg.condition = " and sysstatus<>-1";';
        html += '\n\tcfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";';
        html += '\n\tcfg.width = "98%";';
        html += '\n\tcfg.height = h;';
        html += '\n\tcfg.request = "ajax"; ';
        html += '\n\n';
        html += '\n\tcfg.render = function (value, rowsData, key) {';
        var ulr = "";
        var p = opt.pamas;
        for (var l = 0; l < p.length; l++) {
            if (p[l].type == "Send" && (p[l].action == "List" || p[l].action == "All")) {
                var tm = p[l].value.split('&');
                if (tm[0] == "Receive") {
                    ulr += '&' + tm[1] + '=<%=Request["' + tm[1] + '"]%>';
                }
                if (tm[0] == "Const") {

                    ulr += '&' + tm[1] + '=' + tm[2];
                }
                if (tm[0] == "Field") {
                    if (tm[1].toLowerCase() != $("#sortSelect option[pk]").val().toLowerCase()) {
                        ulr += "&" + tm[1] + "=\" ^ rowsData." + tm[1] + " ^ \"";
                    }
                }

            }
        }

        html += '\n\t\treturn "<a style=\'cursor:pointer;\' title=\'" ^ value ^ "\' href=\'View.aspx?' + $("#sortSelect option[pk]").val() + '=" ^ rowsData.' + $("#sortSelect option[pk]").val() + '^"' + ulr + '\'>" ^ value ^ "</a>";';
        html += '\n\t}';
        html += '\n\n';
        html += '\n\tcfg.columns = [';

        html += SetListClumns();
        html += '\n\t];';
        html += '\n\n';
        html += '\n\t$(function () {\n\t\t $("#grid").Grid(cfg);\n\t});';
        html += '\n\n\tfunction deleteRow() {\n\t\t$("#grid").DetelteRow("<%=Yawei.Common.AppSupport.AppPath %>/Handers/GridDelHandler.ashx");';
        html += '\n\t}\n\n\tvar config = new Config();';
        html += '\n\tconfig.search();';
        $.each(opt.fun, function (i, fun) {
            if (i > 1) {
                html += '\n\n\tfunction ' + fun.en + '_click()\n\t{';
                html += '\n\t\t$("#' + fun.en + '_click").click();';
                html += '\n\n\t}';
            }
        });
        html += '\n</script>';
        return html;
    }



    function SetListClumns() {
        var html = "";
        var column = opt.List[0].Column;
        if (opt.List[0].IsChecked) {
            html += '\n\t\t{ field: "' + $("#sortSelect option[pk]").val() + '",width:"5%",checkbox: true }';
        }
        $.each(column, function (i, dat) {
            if (html != "")
                html += ",";
            html += '\n\t\t{ field: "' + dat.field + '", name: "' + dat.name + '",width:"' + dat.width + '%", align:"' + dat.aglin + '", render: cfg.render';
            if (dat.order == "y")
                html += ", order: true ";
            if (dat.drag == "y")
                html += ",drag: true";
            html += "}";
        });
        return html;
    }


}
//****************************生成新建页**********************************
function CreatePage(options) {
    var layout = options;

    var cLoad = '';
    var cSave = '', vDelete = '';
    var cField = '';
    var scrField = '';
    var csrLoad = '', vscrLoad = '';
    var chtml = '', head = '', scrSave = '';


    this.CreateEdit = function () {
        SetRowInfo();
        $("form").append("<input type='hidden' id='createcs'  name='createcs' />");
        $('#createcs').val(SetCreateCS());
        $("form").append("<input type='hidden' id='createaspx' name='createaspx' />");
        $('#createaspx').val(SetCreateHead() + SetCreateBody() + SetCreateScript());
        $("form").append("<input type='hidden' id='createdesigner' name='createdesigner' />");
        $('#createdesigner').val(SetCreateDesigner());
    }


    function SetCreateHead() {
        var html = "";
        html += '<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="@@.Create" %>';
        html += '\n\n<!DOCTYPE html>\n\n';
        html += '<html xmlns="http://www.w3.org/1999/xhtml">';
        html += '\n<head >';
        html += '\n<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>';
        html += '\n\t<title></title>';
        html += '\n\t<link href="<%=Yawei.Common.AppSupport.AppPath %>/Content/Form.css" rel="stylesheet" />';
        html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery.min.js" type="text/javascript"></script>';
        html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/FormCore.js" type="text/javascript"></script>';
        html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/DatePicker/WdatePicker.js" type="text/javascript"></script> ';
        var b = false, f = false;

        $.each(layout.layout, function (i, dat) {
            if (dat.tableName) {
                if (!f) {
                    html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery.dynamicrow.js" type="text/javascript"></script>';
                    f = true;
                }
                if (dat.file == "是" && !b) {
                    html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>';
                    html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/UpLoadFile.js" type="text/javascript"></script>';
                    b = true;
                }
            }
        });

        if (layout.file.length > 0 && !b) {
            html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>';
            html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/UpLoadFile.js" type="text/javascript"></script>';
        }

        html += '\n</head>\n';

        return html;
    }
    var funButton='';
    function SetCreateBody() {
        var html = '';
        html += "<body>";
        html += '\n\t<form id="form1" runat="server">';
        html += '\n\t<div style="width:100%; text-align:center">';
        html += '\n\t<div class="FormMenu">';
        var ulr = "";
        var p = layout.pamas;
        for (var l = 0; l < p.length; l++) {
            if (p[l].type == "Send" && p[l].action == "All") {
                var tm = p[l].value.split('&');
                if (tm[0] == "Receive") {
                    ulr += '&' + tm[1] + '=<%=Request["' + tm[1] + '"]%>';
                }
                if (tm[0] == "Const") {

                    ulr += '&' + tm[1] + '=' + tm[2];
                }


            }
        }
        ulr = ulr.replace("&", "?");
        html += '\n\t\t<div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：<a href="list.aspx' + ulr + '">' + layout.tableTitle + '</a> >> 编辑' + layout.tableTitle + ' </div>';
        html += '\n\t\t<div class="MenuRight">';
        html += '\n\t\t\t<%if(CurrentUser.HasSave()) {%>';

        html += '\n\t\t\t<a style="cursor:pointer" id="save" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;保存</a>';
        html += "\n\t\t\t<%} %>";

        //html += '\n\t\t\t<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/g.gif" width="1px" height="10px" />';
        var fun = layout.fun;
        $.each(fun, function (i, dat) {
            if (dat.en != "create" && dat.en != "delete" && dat.en != "edit" && dat.p == "Create")
            {
                html += '\n\t\t\t&nbsp;<a style="cursor:pointer" onclick="' + dat.en + '_click()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/' + dat.img + '" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;' + dat.cn + '</a>';
                html += "\n\t\t\t";
                funButton += '\n\t<asp:Button runat="server" ID="_' + dat.en + '" OnClick="' + dat.en + '_Click" style="display:none" />';
                designer += "\n\n\t\tprotected global::System.Web.UI.WebControls.Button " + dat.en + ";";
            }
        });
        // html += '\n\t\t\t<a style="cursor:pointer" href="list.aspx" id="del"><img src="<%=Yawei.Application.Common.APP.applicatonPath %>/images/delete_ico.gif" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;返回</a>';

        html += '\n\t\t</div>';
        html += "\n\t</div>";
        html += SetCreateData();
        html += "\n\t</div>";
        html += '\n\t<asp:Button runat="server" OnClick="Page_SaveData" ID="SaveButton"  style="display:none" />';
        html += funButton;
        html += '\n\t</form>';
        html += "\n</body>\n</html>\n";
        return html;
    }


    function SetCreateData() {
        var html = '\n\n\t';

        html += '\n\t\t<table class="table" cellpadding="0" cellspacing="0">';
        var json = layout.layout;
        for (var i = 0; i < json.length; i++) {

            if (json[i].colspan == 2) {

                if (!json[i].Empty) {

                    if (json[i].tableName) {

                        html += "\n\t\t\t<tr>";
                        html += "\n\t\t\t\t<td colspan='4' style='min-height:100px' class='TdContent'>\n\t\t\t\t\t<table cellpadding='0' cellspacing='0' class='rowTable'>" + GetChildRow(json[i], "create") + "</table>\n\t\t\t\t</td>";
                        html += "\n\t\t\t</tr>";
                    }
                    else {
                        html += "\n\t\t\t<tr>";
                        html += "\n\t\t\t\t<td class='TdLabel'>" + json[i].FieldDesc + "</td>";

                        html += InputVidate(json[i], 3);
                        html += "\n\t\t\t</tr>";
                    }
                }
                else {
                    html += "\n\t\t\t<tr>";
                    html += "\n\t\t\t\t<td colspan='4' style='' class='TdContent'>\n\t\t\t\t</td>";
                    html += "\n\t\t\t</tr>";
                }
            }
            else {
                html += "\n\t\t\t<tr>";
                if (!json[i].Empty) {
                    html += "\n\t\t\t\t<td class='TdLabel'>" + json[i].FieldDesc + "</td>";
                    html += InputVidate(json[i]);
                }
                else {
                    html += "\n\t\t\t\t<td class='TdLabel'>&nbsp;</td>\n\t\t\t\t<td class=\'TdContent\'>&nbsp;</td>";
                }
                if (i + 1 < json.length && json[i + 1].colspan < 2 && !json[i + 1].Empty) {
                    html += "\n\t\t\t\t<td class='TdLabel'>" + json[i + 1].FieldDesc + "</td>";
                    html += InputVidate(json[i + 1]);
                    html += "\n\t\t\t</tr>";
                    i++;
                }
                else {
                    if (i + 1 < json.length && json[i + 1].Empty) {
                        html += "\n\t\t\t\t<td class='TdLabel'>&nbsp;</td>";
                        html += "\n\t\t\t\t<td class='TdContent'>&nbsp;</td>";
                        html += "\n\t\t\t</tr>"; i++;
                    }
                }
            }
        }
        if (layout.file.length > 0) {
            $.each(layout.file, function (i, dat) {
                html += "\n\t\t\t<tr>";
                html += '\n\t\t\t\t<td class="TdLabel" style="padding-left:50px"><div  style="width:100px;height:28px;" id="file' + i + '"></div></td>';
                html += '\n\t\t\t\t<td class=\'TdContent\' colspan="3"><div id="filecontent' + i + '"></div></td>';
                html += "\n\t\t\t</tr>";
            });
        }
        html += '\n\t\t</table>';
        return html;
    }
    var designer = '';
    function InputVidate(dat, clospan) {

        var html = '';
        var em = '';
        if (clospan) {
            clospan = " colspan='" + clospan + "' ";
        }
        else
            clospan = "";
        if (dat.nullvalidate == "null")
            em = 'empty="true"';
        if (dat.dataType == "datetime") {
            html += "\n\t\t\t\t<td " + clospan + " class='TdContent'>\n\t\t\t\t\t<asp:TextBox " + em + " ID=\"" + dat.FieldName + "\" class=\"Wdate\" onfocus=\"WdatePicker()\" runat=\"server\"></asp:TextBox>\n\t\t\t\t</td>";
            designer += '\n\n\t\tprotected global::System.Web.UI.WebControls.TextBox ' + dat.FieldName + ';';
        }
        else if (dat.control == "select") {

            html += "\n\t\t\t\t<td " + clospan + "  class='TdContent'>\n\t\t\t\t\t<asp:DropDownList " + em + " runat=\"server\" ID=\"" + dat.FieldName + "\">";
            if (dat.Custom) {
                $.each(dat.Custom, function (i, c) {
                    html += "\n\t\t\t\t\t\t<asp:ListItem Value=\"" + c.value + "\">" + c.text + "</asp:ListItem>";
                });
            }
            html += "\n\t\t\t\t\t</asp:DropDownList>\n\t\t\t\t</td>";
            designer += '\n\n\t\tprotected global::System.Web.UI.WebControls.DropDownList ' + dat.FieldName + ';';
        }
        else if (dat.control == "Checkbox") {
            html += "\n\t\t\t\t<td " + clospan + " class='TdContent'>\n\t\t\t\t\t<asp:CheckBoxList " + em + " RepeatDirection=\"Horizontal\" RepeatLayout=\"Flow\"  runat=\"server\" ID=\"" + dat.FieldName + "\">";
            if (dat.Custom) {
                $.each(dat.Custom, function (i, c) {
                    html += "\n\t\t\t\t\t\t<asp:ListItem Value=\"" + c.value + "\">" + c.text + "</asp:ListItem>";
                });
            }
            html += "\n\t\t\t\t\t</asp:CheckBoxList>\n\t\t\t\t</td>";
            designer += '\n\n\t\tprotected global::System.Web.UI.WebControls.CheckBoxList ' + dat.FieldName + ';';
        }
        else if (dat.control == "Radio") {
            html += "\n\t\t\t\t<td " + clospan + " class='TdContent'>\n\t\t\t\t\t<asp:RadioButtonList " + em + " RepeatDirection=\"Horizontal\" RepeatLayout=\"Flow\"  runat=\"server\" ID=\"" + dat.FieldName + "\">";
            if (dat.Custom) {
                $.each(dat.Custom, function (i, c) {
                    html += "\n\t\t\t\t\t\t<asp:ListItem Value=\"" + c.value + "\">" + c.text + "</asp:ListItem>";
                });
            }
            html += "\n\t\t\t\t\t</asp:RadioButtonList>\n\t\t\t\t</td>";
            designer += '\n\n\t\tprotected global::System.Web.UI.WebControls.RadioButtonList ' + dat.FieldName + ';';
        }
        else {
            var tm = '', cls = '';
            if (dat.control == "Areatext") {
                tm = ' TextMode=\"MultiLine\" ';
            }
            cls = clospan;
            if (dat.valvalidate == "int")
                html += "\n\t\t\t\t<td " + cls + " class='TdContent'>\n\t\t\t\t\t<asp:TextBox " + em + tm + " ID=\"" + dat.FieldName + "\" int='true' runat=\"server\"></asp:TextBox>\n\t\t\t\t</td>";
            else if (dat.valvalidate == "float")
                html += "\n\t\t\t\t<td " + cls + " class='TdContent'>\n\t\t\t\t\t<asp:TextBox " + em + tm + " ID=\"" + dat.FieldName + "\" double='true' runat=\"server\"></asp:TextBox>\n\t\t\t\t</td>";
            else if (dat.valvalidate == "email")
                html += "\n\t\t\t\t<td " + cls + " class='TdContent'>\n\t\t\t\t\t<asp:TextBox " + em + tm + " ID=\"" + dat.FieldName + "\" email='true' runat=\"server\"></asp:TextBox>\n\t\t\t\t</td>";
            else if (dat.valvalidate == "phoene")
                html += "\n\t\t\t\t<td " + cls + " class='TdContent'>\n\t\t\t\t\t<asp:TextBox " + em + tm + " ID=\"" + dat.FieldName + "\" tell='true' runat=\"server\"></asp:TextBox>\n\t\t\t\t</td>";
            else if (dat.valvalidate == "idcode")
                html += "\n\t\t\t\t<td " + cls + " class='TdContent'>\n\t\t\t\t\t<asp:TextBox " + em + tm + " ID=\"" + dat.FieldName + "\" card='true' runat=\"server\"></asp:TextBox>\n\t\t\t\t</td>";
            else
                html += "\n\t\t\t\t<td " + cls + " class='TdContent'>\n\t\t\t\t\t<asp:TextBox " + em + tm + " ID=\"" + dat.FieldName + "\"  runat=\"server\"></asp:TextBox>\n\t\t\t\t</td>";
            designer += '\n\n\t\tprotected global::System.Web.UI.WebControls.TextBox ' + dat.FieldName + ';';
        }
        return html;
    }

    function SetCreateScript() {
        var html = '\n<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>';
        html += '\n<script type="text/javascript">\n';

        if (layout.file.length > 0) {
            html += SetFileScript(false);
        }

        html += scrField + csrLoad;
        html += '\n\n\tvar formCore = new FormCore();';
        html += '\n\tformCore.FormVildateLoad();';
        html += '\n\n\tvar config = new Config();';
        html += '\n\t\config.saveData(function(){' + scrSave + '});';
        var fun = layout.fun;
        $.each(fun, function (i, f) {
            if (f.en != "create" && f.en != "delete" && f.en != "edit" && f.p == "Create") {
                html += '\n\n\tfunction ' + f.en + '_click()\n\t{';
                html += '\n\t\t$("#' + f.en + '_click").click();';
                html += '\n\n\t}';
            }
        });

        html += '\n\n</script>';
        return html;
    }


    function SetCreateCS() {
        var code = '';
        code += 'using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Web;\nusing System.Web.UI;\nusing System.Web.UI.WebControls;\nusing Yawei.SupportCore;';
        code += '\n\nnamespace @@\n{';
        code += '\n\tpublic partial class Create : Yawei.Common.SharedPage\n\t{';
        code += '\n\t\tprotected string strGuid = string.Empty;';
        code += '\n\t\tprotected SysFormCore viewForm = new SysFormCore();';
        code += cField;
        code += '\n\t\t';
        code += '\n\t\tprotected void Page_Load(object sender, EventArgs e)\n\t\t{';
        code += '\n\t\t\t#region 接受参数'
        code += '\n\n\t\t\tstrGuid = Request["Guid"] != null ? Request["Guid"] : "";';
        code += '\n\n\t\t\t#endregion';


        if (layout.file.length > 0) {
            code += '\n\n\t\t\tif (strGuid == "")'
            code += '\n\t\t\t{'
            code += '\n\t\t\t\tif (ViewState["Guid"] == null)';
            code += '\n\t\t\t\t\tViewState["Guid"] = Guid.NewGuid().ToString();';
            code += '\n\t\t\t\telse\n\t\t\t\t\tviewForm.StateGuid = ViewState["Guid"].ToString();';
            code += '\n\t\t\t}\n\t\t\telse';
            code += '\n\t\t\t\tViewState["Guid"] = strGuid;';
            code += '\n';

        }
        code += '\n\n\t\t\tviewForm.TableName = "' + layout.tabelName + '"; //表名';
        code += '\n\t\t\tviewForm.Key = "' + $("#sortSelect option[pk]").text() + '";  //主键';
        code += '\n\t\t\tviewForm.KeyValue = strGuid; //主键的值';
        code += '\n\t\t\tviewForm.CurrentUser = CurrentUser;';
        code += '\n\n\t\t\t#region 初始化信息\n\n\t\t\tif (!IsPostBack)\n\t\t\t{';


        $.each(layout.layout, function (i, dat) {
            if (dat.control == "select"&&dat.Mapping) {
                code += '\n\t\t\t\tviewForm.SetDropDownListValue(' + dat.FieldName + ', "' + dat.Mapping + '", true);';
            }
            if (dat.control == "Checkbox" && dat.Mapping)
                code += '\n\t\t\t\tviewForm.SetCheckBoxListCode(' + dat.FieldName + ', "' + dat.Mapping + '");';
        });
        var dic = setChildRow();

        $.each(dic, function (i, dat) {
            if (dat.control == "drop" && typeof (dat.FieldName) != "undefined") {
                code += '\n\t\t\t\tviewForm.SetDropDownListValue(' + dat.FieldName + ', "' + dat.Mapping + '", true);';
                designer += '\n\n\t\tprotected global::System.Web.UI.WebControls.DropDownList ' + dat.FieldName + ';';
            }

        });
        code += '\n\n\t\t\t\tviewForm.SetControlValue(this.Page);';
        code += cLoad.replace("^^", "\t");
        code += '\n\t\t\t}';
        code += '\n\n\t\t\t#endregion';
        code += '\n\n\t\t\tSystem.GC.Collect();';
        code += '\n\n\t\t}';
        code += '\n\t\tprotected void Page_SaveData(object sender, EventArgs e)\n\t\t{';
        code += cSave;
        code += '\n\t\t\tviewForm.SaveData(this.Request,null); //保存数据';

        var ulr = "";
        var p = layout.pamas;
        for (var l = 0; l < p.length; l++) {
            if (p[l].type == "Send" && (p[l].action == "Save" || p[l].action == "All")) {
                var tm = p[l].value.split('&');
                if (tm[0] == "Receive") {
                    ulr += " ^ \"&" + tm[1] + "=\" ^ Request[\"" + tm[1] + "\"]";
                }
                if (tm[0] == "Const") {

                    ulr += ' ^ "&' + tm[1] + '=' + tm[2] +' "';
                }
                if (tm[0] == "Field") {
                    if (tm[1].toLowerCase() != $("#sortSelect option[pk]").val().toLowerCase()) {
                        ulr += " ^ \"&" + tm[1] + "=\" ^ Request[\"" + tm[1] + "\"]";
                    }
                }

            }
        }


        code += '\n\t\t\tResponse.Redirect("View.aspx?guid=" ^ viewForm.KeyValue '+ulr+');';
        code += '\n\t\t}';
        var fun = layout.fun;
        $.each(fun, function (i, f) {
            if (f.en != "create" && f.en != "delete" && f.en != "edit" && f.p == "Create") {
                code += '\n\t\tprotected void ' + f.en + '_Click(object sender, EventArgs e)\n\t\t{';
                code += '\n\n\t\t}';
            }
        });

        code += '\n\t}\n}';
        return code;
    }

    function SetCreateDesigner() {
        var code = '';
        code += 'namespace @@\n{';
        code += '\n\n\tpublic partial class Create {';
        code += '\n\n\t\tprotected global::System.Web.UI.HtmlControls.HtmlForm form1;';
        code += '\n\n\t\tprotected global::System.Web.UI.WebControls.Button SaveButton;';
        code += designer;
        code += '\n\n\t}\n}';
        return code;
    }

    function GetChildRow(json, type) {
        var chtml = ''; head = '';
        var tb = json.tableName;
        chtml += '\n\t\t\t\t\t\t<tr id="' + tb + 'row" style="display:none">';
        head += '\n\t\t\t\t\t\t<tr >';
        $.each(json.column, function (i, c) {

            chtml += '\n\t\t\t\t\t\t\t<td class="RowContent" node="' + c.FieldName + '">';
            if (type == "create") {
                if (c.FieldDataType.toLowerCase() == "datetime") {
                    chtml += "<input class=\"wdate\" onfocus=\"WdatePicker()\" />";
                }
                else if (c.FieldDataType.toLowerCase() == "int" && !c.dropdown)
                    chtml += "<input int='true'  />";
                else if ((c.FieldDataType.toLowerCase() == "float" || c.FieldDataType.toLowerCase() == "numeric") && !c.dropdown)
                    chtml += "<input double='true'  />";
                else if (c.dropdown)
                    chtml += "<asp:DropDownList runat='server' ID='" + c.FieldName + "'  ></asp:DropDownList>";
                else
                    chtml += "<input  />";
            }
            else { chtml += "<label  />"; }
            chtml += "</td>";


            head += '\n\t\t\t\t\t\t\t<td class="RowHead" >';
            head += c.FieldDesc;
            head += "</td>";

        });
        if (json.file == "是") {
            head += '\n\t\t\t\t\t\t\t<td class="RowHead" >';
            head += "附件";
            head += "</td>";
            chtml += '\n\t\t\t\t\t\t\t<td class="RowContent" width="15%">';
            chtml += '<div file="true" style="width:60px;height:20px" ></div></td>';
        }
        if (type == "create") {
            chtml += "\n\t\t\t\t\t\t\t<td class=\"RowContent\"><input style=\"width:80px;cursor:pointer\" type='button' class='delRowBtn' value='删除'  /></td>";
        }
        chtml += '\n\t\t\t\t\t\t</tr>';
        if (type == "create") {
            head += "\n\t\t\t\t\t\t\t<td class=\"RowHead\" style='width:15%'><input style=\"width:80px;cursor:pointer\" type='button' class='addRowBtn' value='添加' onclick='$(\"#" + tb + "row\").dynamicRow({ applicationPath:\"<%=Yawei.Common.AppSupport.AppPath %>\" });' /></td>";
        }
        head += '\n\t\t\t\t\t\t</tr >';
        return head + chtml;
    }

    function SetRowInfo() {
        var json = layout.layout;
        for (var i = 0; i < json.length; i++) {
            if (json[i].tableName) {

                cLoad += '\n\n\t\t\t^^' + json[i].tableName + 'XML = viewForm.GetRowDataXml("' + json[i].tableName + '", "' + json[i].rowsRef + '", strGuid,null);';
                cSave += '\n\t\t\tviewForm.RowData(Request["' + json[i].tableName + 'rowdata"], "' + json[i].tableName + '", "' + json[i].rowsRef + '", strGuid);';
                cField += '\n\t\tprotected string ' + json[i].tableName + 'XML = string.Empty;';
                vDelete += '\n\t\t\tviewForm.DeleteChild("update ' + json[i].tableName + ' set sysstatus=-1 where ' + json[i].rowsRef + '=\'" ^ strGuid ^ "\'");';
                scrField += '\n\tvar ' + json[i].tableName + 'xml = \'<%=' + json[i].tableName + 'XML%>\';';
                csrLoad += '\n\t$("#' + json[i].tableName + 'row").SetRowData({ data: ' + json[i].tableName + 'xml });';
                vscrLoad += '\n\t$("#' + json[i].tableName + 'row").SetRowData({ data: ' + json[i].tableName + 'xml,type:"view" });';
                scrSave += "$('#" + json[i].tableName + "row').getRowData({ hideInputId:'" + json[i].tableName + "rowdata',enCode: true });";

            }
        }
    }


    function SetFileScript(b) {
        var html = '\n\n\t$(function () {';
        $.each(layout.file, function (i, dat) {

            html += '\n\n\t\tvar cfg' + i + ' = [];';
            html += '\n\t\tcfg' + i + '.content = "filecontent' + i + '";';
            if (b)
            { html += '\n\t\tcfg' + i + '.refGuid = "<%=strGuid%>";'; }
            else
                html += '\n\t\tcfg' + i + '.refGuid = "<%=ViewState["Guid"].ToString()%>";';
            html += '\n\t\tcfg' + i + '.type = "database";';
            html += '\n\t\tcfg' + i + '.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";';
            html += '\n\t\tcfg' + i + '.fileSign = "' + dat.sign + '";';
            html += '\n\t\tcfg' + i + '.title = "' + dat.title + '";';
            if (b)
                html += '\n\t\tcfg' + i + '.view = true;';
            html += '\n\n\t\t$("#file' + i + '").uploadfile(cfg' + i + ');\n\n';

        });
        html += '\n\n\t});\n\n';
        return html;
    }

    function setChildRow() {
        var djson = [];
        var json = layout.layout;
        for (var i = 0; i < json.length; i++) {
            if (json[i].tableName) {
                $.each(json[i].column, function (j, dat) {
                    if (dat.dropdown) {
                        djson.push({ control: "drop", FieldName: dat.FieldName, Mapping: dat.Mapping });
                    }
                })
            }
        }

        return djson;
    }
}


//****************************生成查看页**********************************

function ViewPage(options) {
    var layout = options;

    var cLoad = '';
    var cSave = '', vDelete = '';
    var cField = '';
    var scrField = '';
    var csrLoad = '', vscrLoad = '';
    var chtml = '', head = '', scrSave = '';
    var designer = '';

    this.CreateView = function () {
        SetRowInfo();
        $("form").append("<input type='hidden' id='viewdesigner' name='viewdesigner' />");
        $('#viewdesigner').val(SetViewtDesigner());
        $("form").append("<input type='hidden' id='viewcs'  name='viewcs' />");
        $('#viewcs').val(SetViewCS());
        $("form").append("<input type='hidden' id='viewaspx' name='viewaspx' />");
        $('#viewaspx').val(SetViewHead() + SetViewBody() + SetViewScript());
    }

    function SetRowInfo() {
        var json = layout.layout;
        var rowsArr = '';
        var tload = '';
        for (var i = 0; i < json.length; i++) {
            if (json[i].tableName) {

                tload += '\n\t\t\t' + json[i].tableName + 'XML = viewForm.GetRowDataXml("' + json[i].tableName + '", "' + json[i].rowsRef + '", strGuid,rowsArr);';
                cSave += '\n\t\t\tviewForm.RowData(Request["' + json[i].tableName + 'rowdata"], "' + json[i].tableName + '", "' + json[i].rowsRef + '", strGuid);';
                cField += '\n\t\tprotected string ' + json[i].tableName + 'XML = string.Empty;';
                vDelete += '\n\t\t\tviewForm.DeleteChild("update ' + json[i].tableName + ' set sysstatus=-1 where ' + json[i].rowsRef + '=\'" ^ strGuid ^ "\'");';
                scrField += '\n\tvar ' + json[i].tableName + 'xml = \'<%=' + json[i].tableName + 'XML%>\';';
                csrLoad += '\n\t$("#' + json[i].tableName + 'row").SetRowData({ data: ' + json[i].tableName + 'xml });';
                vscrLoad += '\n\t$("#' + json[i].tableName + 'row").SetRowData({ data: ' + json[i].tableName + 'xml,type:"view" });';
                scrSave += "$('#" + json[i].tableName + "row').getRowData({ hideInputId:'" + json[i].tableName + "rowdata',enCode: true });";

                $.each(json[i].column, function (j, dat) {
                    if (dat.dropdown) {
                       rowsArr += "{\"" + dat.FieldName+ "\",\""+dat.Mapping+"\"},";;
                    }
                })
            }
        }
       
        cLoad = '\n\n\t\t\tvar rowsArr=new string[,]{'+rowsArr.substring(0,rowsArr.length-1)+'};'+tload;
    }


    function SetViewHead() {
        var html = "";
        html += '<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="@@.View" %>';
        html += '\n\n<!DOCTYPE html>\n\n';
        html += '<html xmlns="http://www.w3.org/1999/xhtml">';
        html += '\n<head >';
        html += '\n<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>';
        html += '\n\t<title></title>';
        html += '\n\t<link href="<%=Yawei.Common.AppSupport.AppPath %>/Content/Form.css" rel="stylesheet" />';
        html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery.min.js" type="text/javascript"></script>';
        var b = false, f = false;

        $.each(layout.layout, function (i, dat) {
            if (dat.tableName) {
                if (!f) {
                    html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery.dynamicrow.js" type="text/javascript"></script>';
                    f = true;
                }
                if (dat.file == "是" && !b) {
                    html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>';
                    html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/UpLoadFile.js" type="text/javascript"></script>';
                    b = true;
                }
            }
        });

        if (layout.file.length > 0 && !b) {
            html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>';
            html += '\n\t<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/UpLoadFile.js" type="text/javascript"></script>';
        }
        html += '\n</head>\n';

        return html;
    }

    function SetViewScript() {
        var html = '\n<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>';
        html += '\n<script type="text/javascript">\n';
        if (layout.file.length > 0) {
            html += SetViewFileScript();
        }
        html += '\n\n\tvar config = new Config();';
        html += '\n\t\config.deleteData();';
        html += scrField;
        html += vscrLoad;

        var fun = layout.fun;
        $.each(fun, function (i, f) {
            if (f.en != "create" && f.en != "delete" && f.en != "edit" && f.p == "View") {
                html += '\n\n\tfunction ' + f.en + '_click()\n\t{';
                html += '\n\t\t$("#' + f.en + '_click").click();';
                html += '\n\n\t}';
            }
        });
        html += '\n\n</script>';
        return html;
    }
    var funButton = "";
    function SetViewBody() {
        var html = '';
        html += "<body>";
        html += '\n\t<form id="form1" runat="server">';
        html += '\n\t<div style="width:100%; text-align:center">';
        html += '\n\t<div class="FormMenu">';
        var p = layout.pamas;
        for (var l = 0; l < p.length; l++) {
            if (p[l].type == "Send" && p[l].action == "All") {
                var tm = p[l].value.split('&');
                if (tm[0] == "Receive") {
                    ulr += '&' + tm[1] + '=<%=Request["' + tm[1] + '"]%>';
                }
                if (tm[0] == "Const") {

                    ulr += '&' + tm[1] + '=' + tm[2];
                }
               

            }
        }
        ulr = ulr.replace("&", "?");
        html += '\n\t\t<div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：<a href="list.aspx' + ulr + '">' + layout.tableTitle + '</a> >> 查看' + layout.tableTitle + ' </div>';
        html += '\n\t\t<div class="MenuRight">';
        html += '\n\t\t\t<%if(CurrentUser.HasEdit()) {%>';

        var ulr = "";
        var p = layout.pamas;
        for (var l = 0; l < p.length; l++) {
            if (p[l].type == "Send" && (p[l].action == "Edit" || p[l].action == "All")) {
                var tm = p[l].value.split('&');
                if (tm[0] == "Receive") {
                    ulr += '&' + tm[1] + '=<%=Request["' + tm[1] + '"]%>';
                }
                if (tm[0] == "Const") {

                    ulr += '&' + tm[1] + '=' + tm[2];
                }
                if (tm[0] == "Field") {
                    if (tm[1].toLowerCase() != $("#sortSelect option[pk]").val().toLowerCase()) {
                        ulr += "&" + tm[1] + "=\" ^ document[\"" + tm[1] + "\"]\"";
                    }
                }

            }
        }


        html += '\n\t\t\t<a style="cursor:pointer" href="Create.aspx?guid=<%=strGuid %>' + ulr + '" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;编辑</a>';
        html += "\n\t\t\t<%} %>";
        html += '\n\t\t\t<%if(CurrentUser.HasDelete()) {%>';
        //html += '\n\t\t\t<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/g.gif" width="1px" height="10px" />';

        html += '\n\t\t\t<a style="cursor:pointer" id="del"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;删除</a>';
        html += "\n\t\t\t<%} %>";
        var fun = layout.fun;
        $.each(fun, function (i, dat) {
            if (dat.en != "create" && dat.en != "delete" && dat.en != "edit" && dat.p == "View") {
                html += '\n\t\t\t&nbsp;<a style="cursor:pointer" onclick="' + dat.en + '_click()"><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/' + dat.img + '" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;' + dat.cn + '</a>';
                html += "\n\t\t\t";
                funButton += '\n\t<asp:Button runat="server" ID="_' + dat.en + '" OnClick="' + dat.en + '_Click" style="display:none" />';
                designer += "\n\n\t\tprotected global::System.Web.UI.WebControls.Button " + dat.en + ";";
            }
        });
        html += '\n\t\t</div>';
        html += "\n\t</div>";
        html += SetViewData();
        html += "\n\t</div>";
        html += '\n\t<asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton"  style="display:none" />';
        html += funButton;
        html += '\n\t</form>';
        html += "\n</body>\n</html>\n";
        return html;
    }

    function SetViewData() {
        var html = '\n\n\t';

        html += '\n\t\t<table class="table" cellpadding="0" cellspacing="0">';
        var json = layout.layout;
        for (var i = 0; i < json.length; i++) {

            if (json[i].colspan == 2) {
                if (!json[i].Empty) {

                    if (json[i].tableName) {
                        html += "\n\t\t\t<tr>";
                        html += "\n\t\t\t\t<td colspan='4' style='min-height:100px' class='TdContent'>\n\t\t\t\t\t<table class='rowTable' cellpadding='0' cellspacing='0'>" + GetChildRow(json[i], "view") + "</table>\n\t\t\t\t</td>";
                        html += "\n\t\t\t</tr>";
                    }
                    else {
                        html += "\n\t\t\t<tr>";
                        html += "\n\t\t\t\t<td class='TdLabel'>" + json[i].FieldDesc + "</td>";
                        if (json[i].control != "Areatext") {
                            html += "\n\t\t\t\t<td colspan='3'  class='TdContent'>\n\t\t\t\t\t<%=document[\"" + json[i].FieldName + "\"] %>\n\t\t\t\t</td>";
                        }
                        else
                            html += "\n\t\t\t\t<td colspan='3' style='min-height:100px' class='TdContent'>\n\t\t\t\t\t<%=document[\"" + json[i].FieldName + "\"] %>\n\t\t\t\t</td>";
                        html += "\n\t\t\t</tr>";
                    }

                } else {
                    html += "\n\t\t\t<tr>";
                    html += "\n\t\t\t\t<td colspan='4' style='' class='TdContent'>\n\t\t\t\t</td>";
                    html += "\n\t\t\t</tr>";
                }
            }
            else {
                html += "\n\t\t\t<tr>";
                if (!json[i].Empty) {
                    html += "\n\t\t\t\t<td class='TdLabel'>" + json[i].FieldDesc + "</td>";
                    html += "\n\t\t\t\t<td class='TdContent'>\n\t\t\t\t\t<%=document[\"" + json[i].FieldName + "\"] %>\n\t\t\t\t</td>";
                }
                else {
                    html += "\n\t\t\t\t<td class='TdLabel'>&nbsp;</td>\n\t\t\t\t<td class=\'TdContent\'>&nbsp;</td>";
                }
                if (i + 1 < json.length && json[i + 1].colspan < 2 && !json[i + 1].Empty) {
                    html += "\n\t\t\t\t<td class='TdLabel'>" + json[i + 1].FieldDesc + "</td>";
                    html += "\n\t\t\t\t<td class='TdContent'>\n\t\t\t\t\t<%=document[\"" + json[i + 1].FieldName + "\"] %>\n\t\t\t\t</td>";
                    html += "\n\t\t\t</tr>";
                    i++;
                }
                else {
                    if (i + 1 < json.length && json[i + 1].Empty) {
                        html += "\n\t\t\t\t<td class='TdLabel'>&nbsp;</td>";
                        html += "\n\t\t\t\t<td class='TdContent'>&nbsp;</td>";
                        html += "\n\t\t\t</tr>"; i++;
                    }
                }
            }
        }
        if (layout.file.length > 0) {
            $.each(layout.file, function (i, dat) {
                html += "\n\t\t\t<tr>";
                html += '\n\t\t\t\t<td class="TdLabel" >' + dat.title + '</td>';
                html += '\n\t\t\t\t<td class=\'TdContent\' colspan="3"><div id="filecontent' + i + '"></div></td>';
                html += "\n\t\t\t</tr>";
            });
        }
        html += '\n\t\t</table>';
        return html;
    }



    function SetViewCS() {
        var code = '';
        code += 'using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Web;\nusing System.Web.UI;\nusing System.Web.UI.WebControls;\nusing Yawei.SupportCore;';
        code += '\n\nnamespace @@\n{';
        code += '\n\tpublic partial class View : Yawei.Common.SharedPage\n\t{';
        code += '\n\t\tprotected Dictionary<string, string> document = new Dictionary<string, string>();';
        code += '\n\t\tprotected string strGuid = string.Empty;';
        code += cField;
        code += '\n\t\tprotected SysFormCore viewForm = new SysFormCore();';
        code += '\n\t\t';
        code += '\n\t\tprotected void Page_Load(object sender, EventArgs e)\n\t\t{';
        code += '\n\t\t\t#region 接受参数'
        code += '\n\n\t\t\tstrGuid = Request["Guid"] != null ? Request["Guid"] : "";';

        code += '\n\n\t\t\t#endregion';
 
        code += '\n\n\t\t\t#region 初始化信息';
        code += '\n\n\t\t\tviewForm.TableName ="' + layout.tabelName + '"; //表名';
        code += '\n\t\t\tviewForm.Key = "' + $("#sortSelect option[pk]").text() + '";  //主键';
        code += '\n\t\t\tviewForm.KeyValue = strGuid; //主键的值';
        code += '\n\t\t\tviewForm.CurrentUser = CurrentUser;';
        code += cLoad.replace("^^", "");
        var tm = '';
        $.each(layout.layout, function (i, dat) {
            if ((dat.control == "select" || dat.control == "Checkbox"||dat.control=="Radio")&&dat.Mapping) {
                if (tm != "")
                    tm += ",";
                tm += '{ "' + dat.FieldName + '", "' + dat.Mapping + '"}';
            }

        });
        if (tm != "") {
            code += '\n\n\t\t\tvar arr = new string[,] {' + tm + '};';
            code += '\n\n\t\t\tdocument = viewForm.SetViewData(arr);';
        }
        else
            code += '\n\n\t\t\tdocument = viewForm.SetViewData(null);';

        code += '\n\n\t\t\t#endregion';
        code += '\n\n\t\t\tSystem.GC.Collect();';
        code += '\n\n\t\t}';
        code += '\n\t\tprotected void Page_DeleteData(object sender, EventArgs e)\n\t\t{';
        code += vDelete;
        code += '\n\t\t\tviewForm.DeleteData();';
        var ulr = "";
        var p = layout.pamas;
        for (var l = 0; l < p.length; l++) {
            if (p[l].type == "Send" && (p[l].action == "Delete" || p[l].action == "All")) {
                var tm = p[l].value.split('&');
                if (tm[0] == "Receive") {
                   // ulr += '&' + tm[1] + '=<%=Request["' + tm[1] + '"]%>';
                    ulr += " ^ \"&" + tm[1] + "=\" ^ Request[\"" + tm[1] + "\"]";
                }
                if (tm[0] == "Const") {

                    ulr += ' ^ "&' + tm[1] + '=' + tm[2] +' "';
                }
                if (tm[0] == "Field") {
                    if (tm[1].toLowerCase() != $("#sortSelect option[pk]").val().toLowerCase()) {
                        ulr += " ^ \"&" + tm[1] + "=\" ^ document[\"" + tm[1] + "\"]";
                    }
                }

            }
        }
        ulr = ulr.replace("&", "?");
        code += '\n\t\t\tResponse.Redirect("List.aspx"' + ulr + ');';
        code += '\n\t\t}';

        var fun = layout.fun;
        $.each(fun, function (i, f) {
            if (f.en != "create" && f.en != "delete" && f.en != "edit" && f.p == "View") {
                code += '\n\t\tprotected void ' + f.en + '_Click(object sender, EventArgs e)\n\t\t{';
                code += '\n\n\t\t}';
            }
        });

        code += '\n\t}\n}';
        return code;
    }

    function SetViewtDesigner() {
        var code = '';
        code += 'namespace @@\n{';
        code += '\n\n\tpublic partial class View {';
        code += '\n\n\t\tprotected global::System.Web.UI.HtmlControls.HtmlForm form1;';
        code += '\n\n\t\tprotected global::System.Web.UI.WebControls.Button DelButton;';
        code += designer;
        code += '\n\n\t}\n}';
        return code;
    }

    function SetViewFileScript() {
        var html = '\n\n\t$(function () {';
        $.each(layout.file, function (i, dat) {

            html += '\n\n\t\tvar cfg' + i + ' = [];';
            html += '\n\t\tcfg' + i + '.refGuid = "<%=strGuid%>";';
            html += '\n\t\tcfg' + i + '.type = "database";';
            html += '\n\t\tcfg' + i + '.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";';
            html += '\n\t\tcfg' + i + '.fileSign = "' + dat.sign + '";';
            html += '\n\n\t\t$("#filecontent' + i + '").fileviewload(cfg' + i + ');';
            html += "\t\t";
        });
        html += '\n\n\t});\n\n';
        return html;
    }

    function GetChildRow(json, type) {
        var chtml = ''; head = '';
        var tb = json.tableName;
        chtml += '\n\t\t\t\t\t\t<tr id="' + tb + 'row" style="display:none">';
        head += '\n\t\t\t\t\t\t<tr >';
        $.each(json.column, function (i, c) {

            chtml += '\n\t\t\t\t\t\t\t<td class="RowContent" node="' + c.FieldName + '">';
            if (type == "create") {
                if (c.FieldDataType.toLowerCase() == "datetime") {
                    chtml += "<input class=\"wdate\" onfocus=\"WdatePicker()\" />";
                }
                else if (c.FieldDataType.toLowerCase() == "int")
                    chtml += "<input int='true'  />";
                else if (c.FieldDataType.toLowerCase() == "float" || c.FieldDataType.toLowerCase() == "numeric")
                    chtml += "<input double='true'  />";
                else if (c.dropdown)
                    chtml += "<asp:DropDownList runat='server' ID='" + c.FieldName + "'  ></asp:DropDownList>";
                else
                    chtml += "<input  />";
            }
            else { chtml += "<label  />"; }
            chtml += "</td>";


            head += '\n\t\t\t\t\t\t\t<td class="RowHead" >';
            head += c.FieldDesc;
            head += "</td>";

        });
        if (type == "create") {
            chtml += "\n\t\t\t\t\t\t\t<td class=\"RowContent\"><input style=\"width:80px;cursor:pointer\" type='button' class='delRowBtn' value='删除'  /></td>";
        }
        chtml += '\n\t\t\t\t\t\t</tr>';
        if (type == "create") {
            head += "\n\t\t\t\t\t\t\t<td class=\"RowHead\" style='width:15%'><input style=\"width:80px;cursor:pointer\" type='button' class='addRowBtn' value='添加' onclick='$(\"#" + tb + "row\").dynamicRow({ });' /></td>";
        }
        head += '\n\t\t\t\t\t\t</tr >';
        return head + chtml;
    }

    //function setViewCustem

}