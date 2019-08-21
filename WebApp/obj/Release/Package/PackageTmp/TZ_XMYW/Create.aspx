<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMYW.Create" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../Plugins/datepicker/WdatePicker.js"></script>
    <script src="../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
        <script src="../Plugins/jquery.grid/jquery.treegrid.js" type="text/javascript"></script>
    <%--<script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>--%>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
        <script src="../js/layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;当前位置：<a href="List.aspx">项目运维</a> >> 项目运维信息
                </div>
                <div class="MenuRight">

                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
                    <a style="cursor: pointer" href="List.aspx">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>

                </div>
            </div>
            <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
                <tr>
                     <td class="TdLabel" style="width: 15%">运维项目
                         <%if (createType == "")
                           { %>
                         <input type="button" class="fileBtn" value="选择" onclick="showdialog()" />
                         <%} %>
                     </td>
                    <td class="TdContent" colspan="3">
                            <asp:TextBox runat="server"  empty="true"  ID="ProName" ReadOnly></asp:TextBox>
                        <asp:HiddenField runat="server"  ID="xmGuid"></asp:HiddenField>
                    </td>
                    <td class="TdLabel" style="width: 15%">运维年度</td>
                    <td class="TdContent" colspan="3">
                        <asp:TextBox runat="server" empty="true" int="true" ID="Year"></asp:TextBox>
                    </td>
                </tr>
            </table>

            
            
            
            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 150px; font-size: 13px; font-weight: bold">运维报告</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>
           
        </div>
        <asp:HiddenField ID="CreateDate" runat="server" />
        <asp:Button runat="server" OnClick="Page_SaveData" ID="SaveButton" Style="display: none" />
    </form>

    <div id="xmdialog"  style="min-width:600px;min-height:400px;display:none">
        <input style="float:right" onclick="isok()"  type="button" value="确定" class="fileBtn" />
                <div style="text-align:left"> </div>
                <div id="grid"></div>
    </div>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>


<script type="text/javascript">

    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var pguid = "<%=proGuid%>";

    $(function () {
        var cfg0 = [];
        cfg0.content = "filecontent0";
        cfg0.refGuid = "<%=ViewState["Guid"]%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_xmyw";
        cfg0.title = "运维报告";

        $("#file0").uploadfile(cfg0);

        $("#ProName").val('<%=strProName%>');
        $('#xmGuid').val('<%=proGuid%>');
       

        setTimeout(function () {
            var fileobjArr = $(".swfupload");
            //alert($(".swfupload").length);
            for (var i = 0; i < fileobjArr.length; i++) {
                var parentdiv = fileobjArr[i].parentElement;
                $(parentdiv.children[0]).attr("width", $(parentdiv).width() + 4);
                $(parentdiv.children[0]).css({
                    position: "absolute", 'top': $(parentdiv).offset().top,
                    'left': $(parentdiv).offset().left - 2, 'z-index': 2
                });
            }

        }, 800);

    });

    function showdialog() {
        //自定页
        layer.open({
            type: 1,
            title: '项目信息',
            skin: 'layui-layer-demo', //样式类名
            closeBtn: 1, //不显示关闭按钮
            area:['800px','450px'],
            anim: 2,
            shadeClose: true, //开启遮罩关闭
            content: $('#xmdialog')
        });
    }


    var formCore = new FormCore();
    formCore.FormVildateLoad();

    var config = new Config();
    config.saveData(function () {

        if ($("#filecontent0").children().length <= 1) {
            alert("请添加项目运维报告附件！");
            return false;
        }
      
        //var pfje = parseFloat($("#Quota").val());

        //alert("?????");



    });



    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "View_Ys_tz_Project";
    cfg.sortName = "StartDate,createdate";
    cfg.order = "desc,desc";
    cfg.pageCount = 10;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = " and YSGuid is not null <%=sqlWhere%>";
    cfg.condition = "";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = 350;
    cfg.parentfield = "";
    cfg.childfield = "";
    cfg.request = "ajax";
    cfg.Pagination = true;
    //是否关联查询
    cfg.relevanSearch = "true";

    cfg.render = function (value, rowsData, key) {
        if (key == "YSGuid") {
            if (value == "") {
                value = "否";
            }
            else {
                value = "是";
            }
        }
        return "<a  style='cursor:pointer;' title='" + value + "' >" + value + "</a>";
    }

    //弹出查询框
    $(function () {
        $("#search").bind("click", function () {
            $("#win").css("top", "30px");
            $('#win').show(400);
        });
        $("#btn_r").bind("click", function () {
            $('#win').find(':input[type!="button"][type!="checkbox"]').val('');
        });
    })



    cfg.columns = [
        { field: "ProGuid", width: "40px", radiobox: true },
		{ field: "ProName", name: "项目名称", align: "left", render: cfg.render },
        { field: "StartDeptName", name: "申报部门", width: "20%", align: "left", render: cfg.render },
 

        { field: "YSGuid", name: "是否验收", width: "20%", align: "left", render: cfg.render }
    ];

   

    $(function () {
        $("#grid").TreeGrid(cfg);
        
    });


    function isok()
    {
        layer.closeAll();
        var a = $("#grid").GetSelection('total');
        if (a.length <= 0)
        {
            alert("请选择项目");
            return;
        }

        $('#xmGuid').val(a[0].ProGuid);
        $('#ProName').val(a[0].ProName);
        //$(".dialog").hide();
    }
</script>
