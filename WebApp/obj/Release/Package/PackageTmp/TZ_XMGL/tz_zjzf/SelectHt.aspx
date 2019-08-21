<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectHt.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_zjzf.SelectHt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
     <style type="text/css">
        .selectht {
            border-bottom: 1px dashed lightgray;
            line-height: 25px;
            font-size: 14px;
            padding-left: 10px;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            text-align: left;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    
                    <td class="TdContent">
                        <input type="button" onclick="_confirm()" class="fileBtn" value="确定" style="margin-right: 5px;float:right" />
                    </td>
                </tr>
            </table>
            <div style="display: table">
                <div id="grid" style="margin: 9px 0px 0px 4px; float: left;"></div>
                <div id="targetTable" style="width: 23%; border: 1px solid #95B8E7; border-left: none; height: 320px; float: left; margin: 9px 1px 0px 0px">
                    <div class="gridHeader" style="vertical-align: middle">
                        <span style="margin-top: 13px">选择的合同</span>
                    </div>
                    <div id="ht">
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">

    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "tz_xmht";
    cfg.sortName = "htkssj";
    cfg.order = "desc";
    cfg.pageCount = 5;
    cfg.pageSelect = [5, 10, 20, 30];
    cfg.where = "";
    cfg.condition = "  <%=strOtherSql%>";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "75%";
    cfg.height = 320;
    cfg.request = "ajax";

    cfg.SelectedRows = function (rowsData) {
        
        $("#ht").children().remove();
        $("#ht").append("<div id='chooseht' onclick='Removeht(this)' guid='" + rowsData.guid + "' ht='" + JSON.stringify(rowsData) + "' class='selectht'>" + rowsData.htmc + "</div>");
    }
    cfg.CannelRows = function (rowsData) {
        $("div[guid='" + rowsData.guid + "']").remove();
    }

    cfg.render = function (value, rowsData, key) {
        return value;
    }

    cfg.columns = [
        { field: "htmc", name: "合同名称", align: "left", render: cfg.render, drag: true },
        { field: "jf", name: "甲方", width: "20%", align: "center", render: cfg.render, drag: true },
		{ field: "yf", name: "乙方", width: "20%", align: "center", render: cfg.render, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);
        
    });

    function Removeht(contorl) {
        $(contorl).remove();
    }

    

    function _confirm() {
        var guid = '', name = '', ht = '', htmlOpinion = '';
        var ht = JSON.parse($("#ht").find("div").attr("ht"));;
       

        $(window.parent.document).find('#htmc').val(ht.htmc);

        $(window.parent.document).find('#htguid').val(ht.guid);
        $(window.parent.document).find(".dialog").slideUp();
        $(window.parent.document).find("#show").hide();
    }
</script>
