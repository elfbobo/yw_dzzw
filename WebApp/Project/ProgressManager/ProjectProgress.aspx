<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectProgress.aspx.cs" Inherits="WebApp.Project.ProgressManager.ProjectProgress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目进度</title>
    <link href="../../Plugins/jquery-ui/themes/ui-lightness/jquery-ui.min.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
    <script src="../../Plugins/highcharts/highcharts.js" type="text/javascript"></script>
    <script src="../../Scripts/config.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    当前位置：项目进度
                </div>
                <div class="MenuRight">
                    <%-- <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>--%>
                </div>
            </div>
            <div style="margin: 9px 0px 0px 12px;">
                <div id="ProgressMap" style="width: 98%; height: 280px;">
                </div>
            </div>
            <div id="grid" style="margin: 9px 0px 0px 12px;"></div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            bindMap();
            $("#grid").Grid(cfg);
        });
        var config = new Config();
        var cfg = [];
        cfg.connectionName = "";
        cfg.connectionString = "";
        cfg.providerName = "";
        cfg.tableName = "View_tz_xmjd";
        cfg.sortName = "StartDate,createdate";
        cfg.order = "desc,desc";
        cfg.pageCount = 10;
        cfg.pageSelect = [5, 15, 20, 50];
        cfg.where = " <%=sqlwhere%> ";
        cfg.condition = " ";
        cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
        cfg.width = "98%";
        cfg.height = h - 100;
        cfg.request = "ajax";

        cfg.render = function (value, rowsData, key) {
            //return "<a style='cursor:pointer;' title='" + value + "' href='View.aspx?Guid=" + rowsData.Guid + "&xmguid=<%=""%>'>" + value + "</a>";
            //<div class="ui-progressbar-value ui-widget-header ui-corner-left" style="width: 50%;"></div>
            if (key == "sysstatus") {
                var progressnum = "0";
                if (rowsData.ProState == "提交" || rowsData.ProState == "退回") {
                    progressnum = "20";
                }
                else if(rowsData.ProState=="申报"&&rowsData.jscount==0&&rowsData.yscount==0&&rowsData.ywcount==0)
                {
                    progressnum = "40";
                }
                else if (rowsData.ProState == "申报" && rowsData.jscount != 0 && rowsData.yscount == 0 && rowsData.ywcount == 0) {
                    progressnum = "60";
                }
                else if (rowsData.ProState == "申报" && rowsData.yscount != 0 && rowsData.ywcount == 0) {
                    progressnum = "80";
                }
                else if (rowsData.ProState == "申报"  && rowsData.ywcount != 0) {
                    progressnum = "100";
                }
                var vv = '<div id="progressbar" style="border:1px solid #63B8FF;  height:18px;   position: relative;" class="ui-progressbar ui-widget ui-widget-content ui-corner-all" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="' + progressnum + '"><div class="progress-label" style="position: absolute;left: 50%;top: 4px;font-weight: bold;">' + progressnum + '%</div><div class="ui-progressbar-value ui-widget-header ui-corner-left" style="width: ' + progressnum + '%;"></div></div>'
                value = vv;// '<div class="ui-progressbar-value ui-widget-header ui-corner-left" style="width: ' + progressnum + '%;height:15px;"></div>';
            }
            else if (key == "ProState") {
                var curstate="";
                if (rowsData.ProState == "提交" || rowsData.ProState == "退回") {
                    curstate = "申报";
                }
                else if (rowsData.ProState == "申报" && rowsData.jscount == 0 && rowsData.yscount == 0 && rowsData.ywcount == 0) {
                    curstate = "立项";
                }
                else if (rowsData.ProState == "申报" && rowsData.jscount != 0 && rowsData.yscount == 0 && rowsData.ywcount == 0) {
                    curstate = "建设";
                }
                else if (rowsData.ProState == "申报" && rowsData.yscount != 0 && rowsData.ywcount == 0) {
                    curstate = "验收";
                }
                else if (rowsData.ProState == "申报" && rowsData.ywcount != 0) {
                    curstate = "运维";
                }
                value = "<a style='cursor:pointer;' title='" + curstate + "' href='ProjectProgressDetail.aspx?xmguid=" + rowsData.ProGuid + "'>" + curstate + "</a>";
            }
            else {
                value = "<a style='cursor:pointer;' title='" + value + "' href='ProjectProgressDetail.aspx?xmguid=" + rowsData.ProGuid + "'>" + value + "</a>";
            }
            return value;
        }
        cfg.columns = [
            { field: "ProName", name: "项目名称", width: "30%", align: "left", render: cfg.render, drag: true },
            <%if(roleCheck.isAdmin()||roleCheck.isSjj()||roleCheck.isZfb()){%>
            { field: "StartDeptName", name: "申报部门", align: "left", render: cfg.render, drag: true },
            <%}else{%>
            { field: "ProSummary", name: "项目描述", align: "left", render: cfg.render, drag: true },
            <%}%>
            { field: "StartDate", name: "申报时间", align: "left", render: cfg.render, drag: true },
            { field: "ProState", name: "环节进度", width: "60", align: "center", render: cfg.render, drag: true },
            { field: "sysstatus", name: "进度展示", width: "150", render: cfg.render, drag: true }
        ];
        var bindMap = function () {
            $('#ProgressMap').highcharts({
                chart: {
                    type: 'column',
                    borderWidth: 1,
                    borderColor: '#95B8E7'
                    //plotBorderWidth: 2,
                    //plotBorderColor: 'yellow'
                },
                title: {
                    text: '项目环节'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: [
                        '申报',
                        '立项',
                        '建设',
                        '验收',
                        '运维'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '项目数 (个)'
                    }
                },
                tooltip: {
                    //headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    //pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    //'<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
                    //footerFormat: '</table>',
                    //shared: true,
                    //useHTML: true
                },
                plotOptions: {
                    column: {
                        borderWidth: 0,
                        pointWidth: 50
                    }
                },
                series: [{
                    name: '项目数',
                    data: [<%=ss[0]%>, <%=ss[1]%>, <%=ss[2]%>, <%=ss[3]%>, <%=ss[4]%>]
                }]
            });
        };
    </script>
</body>
</html>
