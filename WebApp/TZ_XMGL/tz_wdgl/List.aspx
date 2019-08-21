<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_wdgl.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.newtreegrid.js"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   	<div style="width:100%; text-align:center">
	<div class="FormMenu">
		<div class="MenuLeft">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;当前位置：文档管理 </div>
		<div class="MenuRight">
			
			<a style="cursor:pointer" id="search" ><img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_282.png" style="vertical-align:text-bottom;height:13px;width:13px" />&nbsp;查询</a>
			
			
			     
		</div>
	</div>
	<div id="grid" style="margin:9px 0px 0px 12px"></div>
	</div>
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("#search").bind("click", function () {
            $("#win").css("top", "30px");
            $('#win').show(400);
        });
        $("#btn_r").bind("click", function () {
            $('#win').find(':input[type!="button"]').val('');
        });
    })

    var cfg = [];
    cfg.connectionName = "";
    cfg.connectionString = "";
    cfg.providerName = "";
    cfg.tableName = "View_wdgl";
    cfg.sortName = "uploaddate";
    cfg.order = "desc";
    cfg.pageCount = 14;
    cfg.pageSelect = [5, 15, 20, 50];
    cfg.where = " and 1=1 ";
    cfg.condition = " and 1=1";
    cfg.ajaxUrl = "<%=Yawei.Common.AppSupport.AppPath %>/Handlers/GridDataHandler.ashx";
    cfg.width = "98%";
    cfg.height = h + 30;
    cfg.request = "ajax";
    cfg.parentfield = "RefGuid";
    cfg.childfield = "Guid";
    cfg.Pagination = true;
    cfg.childrenisload = true;
    cfg.relevanSearch = "true";


    cfg.render = function (value, rowsData, key) {
        var v = "";

        //return "<a style='cursor:pointer;' title='" + value + "' href='View.aspx?Guid=" + rowsData.guid + "'>" + value + "</a>";
        if (key == "filesign") {
            switch (value) {
                case "tz_Project":
                    v = "项目申请附件";
                    break;
                case "tz_ProjectPs":
                    v = "专家评审";
                    break;
                case "tz_lx_fj":
                    v = "项目立项附件";
                    break;
                case "tz_lx_pfwj":
                    v = "项目立项批复文件";
                    break;
                case "tz_gkzb_zhaob":
                    v = "公开招标-招标文件";
                    break;
                case "tz_gkzb_zhongb":
                    v = "公开招标-中标文件";
                    break;
                case "tz_zb_xj_xjd":
                    v = "询价-询价单";
                    break;
                case "tz_zb_xj_fj":
                    v = "询价-中标文件";
                    break;
                case "tz_zb_dyly":
                    v = "单一来源-投标书";
                    break;
                case "tz_zb_zjcg":
                    v = "直接采购-采购文件";
                    break;
                case "tz_zb_jzxcs_zhaob":
                    v = "竞争性磋商-招标文件";
                    break;
                case "tz_zb_jzxcs_zhongb":
                    v = "竞争性磋商-中标文件";
                    break;
                case "tz_xmht":
                    v = "项目合同信息";
                    break;
                case "tz_zjdw":
                    v = "合同金额-附件";
                    break;
                case "tz_zjzf_zfpz":
                    v = "资金支付-支付凭证";
                    break;
                case "tz_zjzf_fj":
                    v = "资金支付-附件";
                    break;
                case "rjwdxmxqfx":
                    v = "项目需求分析";
                    break;
                case "rjwdxmsj":
                    v = "项目涉及";
                    break;
                case "rjwdsjksm":
                    v = "数据库说明";
                    break;
                case "rjwdbswd":
                    v = "部署文档";
                    break;
                case "rjwdcswd":
                    v = "测试文档";
                    break;
                case "rjwdyxqk":
                    v = "运行情况";
                    break;
                case "yjwdcpcswd":
                    v = "产品参数文档";
                    break;
                case "yjwdxczp":
                    v = "现场照片";
                    break;
                case "yjwdpzsm":
                    v = "配置说明";
                    break;
                case "xmysysbg":
                    v = "项目验收-验收报告";
                    break;
                default:
                    
                    break;             
            }
            return v;

        } else if (key == "cz") {
            if (value != "") {
                return "<a style='cursor:pointer;' href='xz.ashx?type=download&Guid=" + rowsData.Guid + "'>下载</a>";
            } else {
                return value;
            }
        }
        else {
            return value;
        }
        

    }

    //dylytbs = '单一来源投标书'
    //zhaobiaowj = '公开招标文件'
    //zhongbiaowj = '公开中标文件'
    //jzxcszhaobiaowj = '竞争性磋商招标文件'
    //jzxcszhongbiaowj = '竞争性磋商中标文件'
    //rjwdxmxqfx = '项目需求分析'
    //rjwdxmsj = '项目设计'
    //rjwdsjksm = '数据库说明'
    //rjwdbswd = '部署文档'
    //rjwdcswd = '测试文档'
    //rjwdyxqk = '运行情况'
    //xjzhongbiaowj = '项目询价中标文件'
    //tz_xmht = '项目合同信息'
    //tz_xmlx = '项目立项批复文件'
    //xmysysbg = '项目验收报告'
    //zjcgcgwj = '直接采购文件'
    //zjdwfj = '资金到位附件'
    //zjzfzfpz = '资金支付凭证'
    //zjzffj='资金支付附件'


    cfg.columns = [
    { field: "Guid", width: "40px", checkbox: true },
    { field: "Name", name: "项目名称/文档名称", align: "left", render: cfg.render, order: true, drag: true, collapsible: true },
    { field: "filesign", name: "文档类别", width: "40%", align: "left", render: cfg.render, order: true, drag: true },
    { field: "cz", name: "下载", width: "10%", align: "center", render: cfg.render, drag: true }

    ];





    $(function () {
        $("#grid").TreeGrid(cfg);
    });



    var config = new Config();
    //config.search();



</script>
