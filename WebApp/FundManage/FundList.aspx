<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundList.aspx.cs" Inherits="WebApp.FundManage.FundList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>资金使用情况</title>
    <script src="../Plugins/jquery-1.8.3.min.js" type="text/javascript"></script>
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../Plugins/datepicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../Plugins/datepicker/skin/WdatePicker.css" rel="stylesheet" />
    <script src="../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="FormMenu">
            <div class="MenuLeft">
                &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                当前位置：资金使用情况       
            </div>
            <div style="line-height: 32px; margin-left: 40px; float: left; color:#000">
                <span style="font-size: 14px">预估总额：<label id="ygze"></label></span>  <span style="font-size: 14px; padding-left: 20px">合同总额：<label id="htzr"></label></span>  <span style="font-size: 14px; padding-left: 20px">支付总额：<label id="zfzr"></label></span>
            </div>
            <div class="MenuRight">
                <a style="cursor: pointer" id="search">
                    <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查询</a>
                <a style="cursor: pointer" id="export" onclick="exporttoexcel()">
                    <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/excel.jpg" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;导出支出安排表</a>
            </div>
        </div>
         <div id="grid" style="margin: 5px 0px 0px 12px;"></div>
        <div id="win" class="SearchWin">
            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="TdLabel">立项起止时间</td>
                    <td class="TdContent">
                        <input type="text" id="StartDate" style="width:45%" readonly="true" class="Wdate" onfocus="WdatePicker()"/> 至 <input type="text"id="EndDate" style="width:45%"  readonly="true" class="Wdate" onfocus="WdatePicker()"/>
                    </td>
                    <td class="TdLabel">申报部门</td>
                    <td class="TdContent">
                        <input type="text" action="like" datatype="varchar" id="StartDeptNameS" />
                    </td>
                </tr>
                <tr>
                    <td class="TdContent" style="text-align: center" colspan="4">
                        <input style="width: 45px;" type="button" class="searchbtn" id="btn_s123" onclick="searchData()" value="查询" /><input class="searchreset" type="button" style="width: 45px" id="btn_r" value="重置" /></td>
                </tr>
            </table>
        </div>
    </form>
    <a id="fileexportlink" href="#" style="display:none" ><span>下载</span></a>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script>
    $(function () {
        $("#search").bind("click", function () {
            $("#win").css("top", "40px");
            $('#win').slideDown(400);
        });
        $("#btn_r").bind("click", function () {
            $('#win').find(':input[type!="button"]').val('');
        });
        
        Total();//合计
        $("#grid").Grid(cfg);
        
    })

    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "V_FundSummary";
    cfg.sortName = "createdate";
    cfg.order = "desc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = "<%=sqlWhere%>";
    cfg.condition = " ";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h;
    cfg.request = "ajax";

    cfg.render = function (value, rowsData, key) {
        //if (key == "wzfje") {
        //    if (value == "") {
        //        if (rowsData.htje != 0 && rowsData.zfje == 0) {
        //            value = rowsData.htje;
        //        } else {
        //            value = 0;
        //        }           
        //    }
        //}
        if (key == "zfqk") {
            //合同金额未定
            if (rowsData.htje == 0) {
                value = "合同金额待定";
            }
            else {
                if (parseFloat(rowsData.zfje) >= parseFloat(rowsData.htje)) {
                    value = "支付完成";
                }
                else {
                    value = "未支付完成";
                }
            }
        }
        if (key == "Quota") {
            value = parseFloat(rowsData.Quota).toFixed(6);
        }
        if (key == "htje") {
            value = parseFloat(rowsData.htje).toFixed(6);
        }
        if (key == "zfje") {
            value = parseFloat(rowsData.zfje).toFixed(6);
        }
        //alert("in");
        return "<a href='FundManage.aspx?xmguid=" + rowsData.ProGuid + "' style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }

    cfg.columns = [
        { field: "ProName", name: "项目名称", align: "left", render: cfg.render, order: true, drag: true },
        { field: "StartDeptName", name: "申报部门", width: "20%", align: "left", render: cfg.render, drag: true },
        //{ field: "lxje", name: "立项资金", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "Quota", name: "预估金额（万元）", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "htje", name: "合同金额（万元）", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "zfje", name: "支付金额（万元）", width: "10%", align: "left", render: cfg.render, drag: true },
        //{ field: "wzfje_count", name: "未支付金额", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "zfqk", name: "支付情况", width: "10%", align: "left", render: cfg.render, drag: true }
    ];


    function exporttoexcel() {
        $.ajax({
            type: 'post',
            url: 'FundHandler.ashx',
            data: { action: 'export',roletype:"<%=roletype%>",year:<%=DateTime.Now.Year%>,bmguid:"<%=CurrentUser.UserGroup.Guid%>" },
            success: function (e) {
                $("#fileexportlink").attr("href", "<%=ConfigurationManager.AppSettings["excelurl"]%>");
                //$("#fileexportlink").attr("href", "http://50.104.5.122/files/年度资金报告.xls");
                //alert($("#fileexportlink").attr("href"));
                //$("#fileexportlink").attr("href", "http://localhost:8080/年度资金报告.xls");
                $('#fileexportlink span').trigger('click');
            }
        });
    }
 

    //合计计算
    function Total() {
        $.ajax({
            type: "POST",
            url: "FundList.aspx",
            dataType: "text",
            async: false,
            data: { type: "post" },
            success: function (data) {
                var list = data.split(',');
                if (list.length > 1) {
                    $("#ygze").html(list[0] + "万元");
                    $("#htzr").html(list[1] + "万元");
                    $("#zfzr").html(list[2] + "万元");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }
        });
    }

    function searchData() {
        var sql = "";
        if ($("#StartDeptNameS").val() != "") {
            sql += " and StartDeptName like'%" + $("#StartDeptNameS").val() + "%'";
        }
        if ($("#StartDate").val() != "") {
            sql += " and pfsj >='" + $("#StartDate").val() + "'";
        }
        if ($("#EndDate").val() != "") {
            sql += " and pfsj <='" + $("#EndDate").val() + "'";
        }

        cfg.condition = sql;
        Total(sql);
        $("#grid").Grid(cfg);
        $('#win').hide();
    }

</script>