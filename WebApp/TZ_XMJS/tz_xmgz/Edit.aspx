<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebApp.TZ_XMJS.tz_xmgz.Edit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;当前位置：<a href="List.aspx?xmguid=<%=proGuid %>">项目跟踪</a> >> 新建项目跟踪信息
                </div>
                <div class="MenuRight">

                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
                    <a style="cursor: pointer" href="List.aspx?xmguid=<%=proGuid %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>
                </div>
            </div>
            <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="TdLabel" style="width: 15%">项目名称</td>
                    <td class='TdContent' colspan="3">

                        <%=dsPro.Tables[0].Rows[0]["proname"] %>
                    </td>


                </tr>

                <tr style="display:none">
                    <td class="TdLabel" style="width: 15%">项目当前进度</td>
                    <td class='TdContent' colspan="3">
                        <input type="text" id="dqjd" />
                    </td>
                </tr>

                <tr>
                    <td class="TdLabel" style="width: 15%">变更类型</td>
                    <td class="TdContent" colspan="3">
                        <select id="main_sel" style="width: 90%">
                            <option value="项目名称">项目名称</option>
                            <option value="项目简介">项目简介</option>
                            <option value="预估金额">预估金额</option>
                            <option value="项目属性">项目属性</option>
                            <option value="资金来源">资金来源</option>
                            <option value="项目类型">项目类型</option>
                            <option value="申报时间">申报时间</option>
                            <option value="联系方式">联系方式</option>
                            <option value="是否部署云平台">是否部署云平台</option>
                            <option value="建设方案">建设方案</option>
                            <option value="项目申报表">项目申报表</option>
                            <option value="云资源使用申请表">云资源使用申请表</option>
                        </select>

                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">变更为</td>
                    <td class="TdContent" colspan="3">
                        <span id="container_txt">
                            <input type="text" id="bgnr" />
                        </span>
                        <span id="container_lxfs" style="display: none">联系人：<input type="text" id="lxr" style="width: 35%" />
                            联系电话：<input type="text" id="lxdh" style="width: 35%" />
                        </span>
                        <span id="container_zjly" style="display: none">
                            <select style="width: 100px; min-width: 200px" id="zjly_sel">
                                <option value="本级财政性资金" selected>本级财政性资金</option>
                                <option value="上级财政性资金">上级财政性资金</option>
                                <option value="其他">其他</option>
                            </select>
                            <input type="text" id="zjly_txt" style="width: 40%; display: none" />
                        </span>
                        <span id="container_xmsx" style="display: none">
                            <input type="radio" name="xmsx" value="新建" checked />新建
                            <input type="radio" name="xmsx" value="续建" />续建
                            <input type="radio" name="xmsx" value="改建" />改建
                            <input type="radio" name="xmsx" value="运维" />运维
                            <input type="radio" name="xmsx" value="购买服务" />购买服务
                        </span>
                        <span id="container_xmlx" style="display: none">
                            <input type="checkbox" name="xmlx" value="软件平台" />软件平台
                            <input type="checkbox" name="xmlx" value="硬件平台" />硬件平台
                            <input type="checkbox" name="xmlx" value="服务运维" />服务运维
                        </span>

                        <span id="container_iscloud" style="display: none">
                            <input type="radio" checked name="isCloud" value="是" />是
                            <input type="radio" name="isCloud" value="否" />否
                        </span>

                        <fieldset style="margin-top: 5px; display: none" id="container_file1">
                            <legend><span id="file1" style="width: 60px; height: 20px; display: block">建设方案</span></legend>
                            <div id="filecontent1" style="margin-top: 10px"></div>
                        </fieldset>

                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">变更时间</td>
                    <td class="TdContent">
                        <input type="text" id="bgsj" readonly class="Wdate" onfocus="WdatePicker()" />
                    </td>
                    <td class="TdLabel" style="width: 15%">变更人</td>
                    <td class="TdContent">
                        <input type="text" id="bgr" readonly value="<%=CurrentUser.UserCN %>" />
                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">变更原因</td>
                    <td class='TdContent' colspan="3">
                        <select id="bgyy" style="width: 40%">
                            <option value="专家评审">专家评审</option>
                            <option value="需求变更">需求变更</option>
                            <option value="其他">其他</option>
                        </select>

                        <input type="text" style="width: 40%;display:none" id="bgyy_qt"/>
                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">备注</td>
                    <td class='TdContent' colspan="3">
                        <input type="text" id="beizhu" />
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>


<script type="text/javascript">

    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var pguid = "<%=proGuid%>";

    $("#bgyy").change(function () {
        if ($(this).children('option:selected').val() === "其他") {
            $("#bgyy_qt").show();
        }
        else {
            bgyy_qt = "";
            $("#bgyy_qt").hide();
        }
    })

    $("#main_sel").change(function () {
        $("#bgnr").val("");
        $("#container_txt").hide();
        $("#container_lxfs").hide();
        $("#container_zjly").hide();
        $("#container_xmsx").hide();
        $("#container_xmlx").hide();
        $("#container_file1").hide();
        $("#container_file2").hide();
        $("#container_file3").hide();
        $("#container_iscloud").hide();
        $('#bgnr').keyup(function () {
            //return;
            //FloatOnly($(this));
        });
        $('#bgnr').unbind("keyup");
        $('#bgnr').unbind("focus");
        //$('#bgnr').focus(function () {
        //WdatePicker();
        //});
        $("#bgnr").removeClass("Wdate");




        if ($(this).children('option:selected').val() === "联系方式") {
            $("#container_lxfs").show();
        }
        else if ($(this).children('option:selected').val() === "项目类型") {
            $("#container_xmlx").show();
        }
        else if ($(this).children('option:selected').val() === "项目属性") {
            $("#container_xmsx").show();
        }
        else if ($(this).children('option:selected').val() === "资金来源") {
            $("#container_zjly").show();
        }
        else if ($(this).children('option:selected').val() === "是否部署云平台") {
            $("#container_iscloud").show();
        }
        else if ($(this).children('option:selected').val() === "预估金额") {
            $("#container_txt").show();
            $('#bgnr').keyup(function () {
                FloatOnly(this);
            });
        }
        else if ($(this).children('option:selected').val() === "申报时间") {
            $("#container_txt").show();
            //readonly class="Wdate" onfocus="WdatePicker()"
            $('#bgnr').focus(function () {
                WdatePicker();
            });
            $("#bgnr").addClass("Wdate");
        }
        else if ($(this).children('option:selected').val() === "建设方案") {
            $("#container_file1").show();
            $("#filecontent1").empty();
            var cfg1 = [];
            cfg1.content = "filecontent1";
            cfg1.refGuid = "<%=ViewState["Guid"]%>";
            cfg1.type = "database";
            cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
            cfg1.fileSign = "tz_Project_Fa";
            cfg1.title = "项目方案";

            $("#file1").uploadfile(cfg1);
        }
        else if ($(this).children('option:selected').val() === "项目申报表") {
            $("#container_file1").show();
            $("#filecontent1").empty();
            var cfg1 = [];
            cfg1.content = "filecontent1";
            cfg1.refGuid = "<%=ViewState["Guid"]%>";
            cfg1.type = "database";
            cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
            cfg1.fileSign = "tz_Project_Sbb";
            cfg1.title = "项目申报表";
            $("#file1").uploadfile(cfg1);
        }
        else if ($(this).children('option:selected').val() === "云资源使用申请表") {
            $("#container_file1").show();
            $("#filecontent1").empty();
            var cfg1 = [];
            cfg1.content = "filecontent1";
            cfg1.refGuid = "<%=ViewState["Guid"]%>";
            cfg1.type = "database";
            cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
            cfg1.fileSign = "tz_Project_Yzy";
            cfg1.title = "云资源使用申请表";
            $("#file1").uploadfile(cfg1);
        }
        else {
            $("#container_txt").show();
        }
    });

function FloatOnly(oInput) {
    if ('' != oInput.value.replace(/\d{1,}\.{0,1}\d{0,}/, '')) {

        oInput.value = oInput.value.match(/\d{1,}\.{0,1}\d{0,}/) == null ? '' : oInput.value.match(/\d{1,}\.{0,1}\d{0,}/);

    }

    var strValue = oInput.value;

    if (strValue.toString().indexOf('.') != -1) {

        var index = strValue.toString().indexOf('.');

        oInput.value = strValue.toString().substring(0, index + 5);

    }
    else {

        oInput.value = strValue.toString();

    }
}

$("#zjly_sel").change(function () {
    $("#zjly_txt").hide();
    if ($(this).children('option:selected').val() === "其他") {
        $("#zjly_txt").show();
    }
})

var bgnr = "";
var bgnr_desc = "";
var last_val = "";
var change_type = "";
var bgyy = "";
var bgyy_qt = "";
$("#save").click(function () {

    change_type = $("#main_sel").children('option:selected').val();
    bgyy = $("#bgyy").children('option:selected').val();

    if (bgyy === "其他") {
        bgyy_qt = $("#bgyy_qt").val();
    }
    else {
        bgyy_qt = "";
    }

    if (change_type === "项目名称") {
        if ('<%=row["proname"]%>' === $("#bgnr").val()) {
            alert("项目名称无变化");
            return;
        }
        else {
            bgnr = $("#bgnr").val();
            last_val = '<%=row["proname"]%>';
            bgnr_desc = "项目名称由" + '<%=row["proname"]%>' + "变为" + $("#bgnr").val();
        }
    }
    else if (change_type === "项目简介") {

        if ('<%=row["prosummary"]%>' === $("#bgnr").val()) {
            alert("项目简介无变化");
            return;
        }
        else {
            bgnr = $("#bgnr").val();
            last_val = '<%=row["prosummary"]%>';
            bgnr_desc = "项目简介由" + '<%=row["prosummary"]%>' + "变为" + $("#bgnr").val();
        }
    }
    else if (change_type === "预估金额") {

        if (parseFloat('<%=row["quota"]%>').toFixed(4) === parseFloat($("#bgnr").val()).toFixed(4)) {
                alert("预估金额无变化");
                return;
            }
            else {
                bgnr = $("#bgnr").val();
                last_val = '<%=row["quota"]%>';
            bgnr_desc = "预估金额由" + '<%=row["quota"]%>' + "变为" + $("#bgnr").val();
            }
        }
        else if (change_type === "资金来源") {
            var zjlynr = $("#zjly_sel").children('option:selected').val();
            if ('<%=row["moneysource"]%>' === zjlynr) {
                alert("资金来源无变化");
                return;
            }
            else {
                bgnr = zjlynr;
                last_val = '<%=row["moneysource"]%>';
                bgnr_desc = "资金来源由" + '<%=row["moneysource"]%>' + "变为" + zjlynr;
            }
        }
        else if (change_type === "项目属性") {

            if ('<%=row["proproperty"]%>' === $("input[name='xmsx']:checked").val()) {
                alert("项目属性无变化");
                return;
            }
            else {
                bgnr = $("input[name='xmsx']:checked").val();
                last_val = '<%=row["proproperty"]%>';
            bgnr_desc = "项目属性由" + last_val + "变为" + bgnr;
        }
    }
    else if (change_type === "项目类型") {

        var xmlxnr = "";
        $("input:[name='xmlx']:checked").each(function (index, item) {
            xmlxnr += $(this).val() + ",";
        });
        if (xmlxnr != "") {
            xmlxnr = xmlxnr.substring(0, xmlxnr.length - 1);
        }

        if ('<%=row["protype"]%>' === xmlxnr) {
                alert("项目类型无变化");
                return;
            }
            else {
                bgnr = xmlxnr;
                last_val = '<%=row["protype"]%>';
                bgnr_desc = "项目类型由" + last_val + "变为" + bgnr;
            }
        }
        else if (change_type === "申报时间") {
            if ('<%=row["startdate"]%>' === $("#bgnr").val()) {
                alert("申报时间无变化");
                return;
            }
            else {
                bgnr = $("#bgnr").val();
                last_val = '<%=row["startdate"]%>';
            bgnr_desc = "申报时间由" + last_val + "变为" + bgnr;
        }
    }
    else if (change_type === "联系方式") {

        bgnr = $("#lxr").val() + ":" + $("#lxdh").val();
        last_val = '<%=row["contactname"]%>' + ":" + '<%=row["contacttel"]%>';
            bgnr_desc = "联系方式由" + last_val + "变为" + bgnr;
        }
        else if (change_type === "是否部署云平台") {

            if ('<%=row["isincloudplat"]%>' === $("input[name='isCloud']:checked").val()) {
                alert("是否部署云平台值无变化");
                return;
            }
            else {
                bgnr = $("input[name='isCloud']:checked").val();
                last_val = '<%=row["isincloudplat"]%>';
                bgnr_desc = "是否部署云平台值由" + last_val + "变为" + bgnr;
            }
        }
        else {
            bgnr_desc = "已重新上传附件！";
            last_val = '<%=strGuid%>';
            bgnr = '<%=strGuid%>';
        }


    $.ajax({
        type: 'post',
        url: '../XMJSHandler.ashx',
        data: {
            action: 'change', guid: '<%=strGuid%>', xmguid: '<%=proGuid%>', dqjd: $("#dqjd").val(),
            bgsj: $("#bgsj").val(), bglx: change_type, bgnr: bgnr,
            bgyy: bgyy, bgyy_qt: bgyy_qt, beizhu: $("#beizhu").val(),
            last_val: last_val, bgr: $("#bgr").val(), bgrguid: '<%=CurrentUser.UserGuid%>',
            bgbm: '<%=CurrentUser.UserGroup.Name%>', bgsm: bgnr_desc, bgnr: bgnr
        },
        success: function (data) {
            //校验返回1 表示已有相同项目名称
            if (data + "" === "1") {
                alert("添加项目变更信息成功");
                window.location = "View.aspx?guid=<%=strGuid%>";
                }
                else {
                    alert("项目变更信息保存失败!")
                }

            }
    });
})

    $(function () {



    });

</script>

<script src="<%=Yawei.Common.AppSupport.AppPath %>/js/filefix.js" type="text/javascript"></script>
