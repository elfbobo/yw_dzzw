<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoPayList.aspx.cs" Inherits="WebApp.FundManage.NoPayList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>未完成支付项目</title>
    <script src="../Plugins/jquery-1.8.3.min.js"></script>
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
   <div class="FormMenu">
            <div class="MenuLeft">
                &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                当前位置：未完成支付项目       
            </div>
        </div>
         <div id="grid" style="margin: 5px 0px 0px 12px;"></div>
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script>

    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "V_FundSummary";
    cfg.sortName = "createdate";
    cfg.order = "desc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    //合同金额未输入（合同金额表单项还未维护）或者合同金额大于支付金额
    cfg.where = " and (htje!=0 and htje>zfje) <%=sqlWhere%>";
    cfg.condition = " ";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h;
    cfg.request = "ajax";

    cfg.render = function (value, rowsData, key) {
        
        if (key == "wzfje") {
            if (value == "") {
                if (parseFloat(rowsData.htje) == 0 ) {
                    value = 0;
                } else {
                    value = numSub(rowsData.htje ,rowsData.zfje);
                }
            }
        }
        return "<a href='FundManage.aspx?xmguid=" + rowsData.ProGuid + "' style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }
    function numSub(num1, num2) {
        var baseNum, baseNum1, baseNum2;
        try {
            baseNum1 = num1.toString().split(".")[1].length;
        } catch (e) {
            baseNum1 = 0;
        }
        try {
            baseNum2 = num2.toString().split(".")[1].length;
        } catch (e) {
            baseNum2 = 0;
        }
        baseNum = Math.pow(10, Math.max(baseNum1, baseNum2));
        var precision = (baseNum1 >= baseNum2) ? baseNum1 : baseNum2;
        return ((num1 * baseNum - num2 * baseNum) / baseNum).toFixed(precision);
    };


    cfg.columns = [
        { field: "ProName", name: "项目名称", width: "20%", align: "left", render: cfg.render, order: true, drag: true },
        { field: "StartDeptName", name: "申报部门", width: "20%", align: "left", render: cfg.render, drag: true },
        //{ field: "lxje", name: "立项资金", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "Quota", name: "预估金额", width: "15%", align: "left", render: cfg.render, drag: true },
        { field: "htje", name: "合同金额", width: "15%", align: "left", render: cfg.render, drag: true },
        { field: "zfje", name: "支付金额", width: "15%", align: "left", render: cfg.render, drag: true },
       // { field: "zfje", name: "支付金额", width: "10%", align: "left", render: cfg.render, drag: true },
        { field: "wzfje", name: "未支付金额", width: "15%", align: "left", render: cfg.render, drag: true }
    ];

    $(function () {
        $("#grid").Grid(cfg);
    });
</script>