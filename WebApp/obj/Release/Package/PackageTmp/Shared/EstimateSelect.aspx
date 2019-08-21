<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EstimateSelect.aspx.cs" Inherits="Yawei.App.Shared.EstimateSelect" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <script src="../Plugins/jquery.min.js"></script>
    <script src="../Plugins/jquery.grid/jquery.treegrid.js"></script>
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <link href="../Content/tab.css" rel="stylesheet" />
    <style type="text/css">
        table, tr, td, tbody {
            border-collapse: collapse;
            border-spacing: 2px;
            border-color: gray;
        }

        .buttonClass {
            padding: 5px 20px 5px 20px;
            border: solid 1px #a0edc3;
            cursor: pointer;
            background-color: #6fb3e0;
            color: white !important;
            border-radius: 5px;
        }

            .buttonClass:hover {
                background-color: #de6d00;
                border: solid 1px #6fb3e0;
            }

        .buttonClass1 {
            padding: 5px 20px 5px 20px;
            cursor: pointer;
            color: white !important;
            border-radius: 5px;
            background-color: #de6d00;
            border: solid 1px #6fb3e0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="FormMenu">
            <div class="MenuLeft">
                <div class="tabs" id="tabs">
                    <ul id="tags" style="background-color: inherit">
                        <li><a onclick="changeTable('0',this)">前期投资事项</a></li>
                        <li class="tabs_active"><a onclick="changeTable('2',this)">概算明细</a></li>
                        <li><a onclick="changeTable('1',this)">概算外事项</a></li>

                    </ul>

                </div>
                <%--  <span class="buttonClass" flag="estType" onclick="changeTable('2',this)">概算明细</span>

                <div class="tabs" id="tabs">
                    <ul id="tags" style="background-color: inherit">
                        <li><a onclick="changeTable('0',this)">前期投资事项</a></li>
                        <li class="tabs_active"><a onclick="changeTable('2',this)">概算明细</a></li>
                        <li><a onclick="changeTable('1',this)">概算外事项</a></li>

                    </ul>

                </div>
                <%--  <span class="buttonClass" flag="estType" onclick="changeTable('2',this)">概算明细</span>

                <span class="buttonClass" flag="estType"  onclick="changeTable('1',this)">概算外事项</span>
                <span class="buttonClass" flag="estType" onclick="changeTable('0',this)">前期投资事项</span>--%>
            </div>
            <div class="MenuRight">
                <input type="button" onclick="checkThis()" class="fileBtn" value="确定" style="margin-right: 25px; margin-top: 5px" />
            </div>
        </div>
        <asp:DropDownList ID="CostType" runat="server" Style="display: none"></asp:DropDownList>
        <div id="treegrid">
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    //概算3 概算外1  前期2
    var sy=3;
    // EstimatesSource
    function changeTable(type,obj){
        $(".tabs_active").removeClass("tabs_active");
        $(obj).parent().addClass("tabs_active");
        cfg.columns = [
          { field: "Guid", width: "30px", radiobox: true },
        { field: "ProjOrCostName", name: "工程或费用名称", width: "45%", align: "left", render: cfg.render, collapsible: true },
        { field: "InvestAccount", name: "投资额", width: "25%", align: "center", render: cfg.render, drag: true },
           { field: "CostType", name: "费用类型", width: "25%", align: "left", render: cfg.DicRender1, order: true, drag: true }
        ];
        switch(type){
            case "0":cfg.rowsData=<%=EarlyJson%>;sy=2;break;
            case"1":cfg.rowsData=<%=EstOutJson%>; sy=1;
                cfg.columns = [
                  { field: "Guid", width: "30px", radiobox: true },
                { field: "ProjOrCostName", name: "工程或费用名称", width: "65%", align: "left", render: cfg.render, collapsible: true },
                { field: "InvestAccount", name: "投资额", width: "35%", align: "center", render: cfg.render, drag: true }
                ];
                break;
            case"2":cfg.rowsData=<%=Json%>; sy=3;  break;
        }
        $("#treegrid").TreeGrid(cfg);
    }
    $(function(){
        //$.map($("#treegrid tr:not(:only-child) span[class='tree-file']"),function(n){
        //    $(n).parent().prev().children(1).attr("sign","no");
        //})
           
        //$.map( $("#treegrid input:checkbox[sign!='no']"),function(n){
        //    if ($(n).val()=="on") {
        //        $(n).attr("disabled","disabled");
        //    }

        //    $(n).on("click",function(){
        //        alert("此概算不可选择");
        //        $(this).removeAttr("checked");
        //    })
        //})  
    })

    var cfg = [];
    cfg.width = "99%";
    cfg.height = 400;
    cfg.rowsData = <%=Json%>;
    // cfg.ajaxUrl = "...";
    cfg.Pagination = false;
    //cfg.Cascade=true;
    cfg.render = function (value, rowsData, key) {
        return "<a style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }
    //翻译项目类型
    cfg.DicRender1 = function (value, rowsData, key) {
        var showName = "";
        showName = $("#CostType").find("option[value='" + rowsData.CostType + "']").html();
        return "<a style='cursor:pointer;' title='" + showName + "' >" + showName + "</a>";
    }
    cfg.columns = [
      { field: "Guid", width: "30px", checkbox: true },
    { field: "ProjOrCostName", name: "工程或费用名称", width: "45%", align: "left", render: cfg.render, collapsible: true },
    { field: "InvestAccount", name: "投资额", width: "25%", align: "center", render: cfg.render, drag: true },
       { field: "CostType", name: "费用类型", width: "25%", align: "left", render: cfg.DicRender1, order: true, drag: true }
    ];
    $(function(){
        $("#treegrid").TreeGrid(cfg);

        $.map($("#treegrid tr:not(:only-child) span[class='tree-file']"),function(n){
            $(n).parent().prev().children(1).attr("sign","no");
        })
           
        $.map( $("#treegrid input:checkbox[sign!='no']"),function(n){
            if ($(n).val()=="on") {
                $(n).attr("disabled","disabled");
            }
            $(n).on("click",function(){
                alert("此概算不可选择");
                $(this).removeAttr("checked");
            })
        })  
    });
   
    function checkThis() {
        var json = $("#treegrid").GetSelection("total");
        var jsonGuid="";
        var jsonName="";
        for (var i = 0; i < json.length; i++) {
            jsonGuid+=json[i].Guid+",";
            jsonName+=json[i].ProjOrCostName+",";
        }
        jsonGuid=jsonGuid.substring(0,jsonGuid.length -1);
        jsonName=jsonName.substring(0,jsonName.length -1);
        window.parent.selectEstimate(jsonGuid,jsonName,sy);
    }
</script>
