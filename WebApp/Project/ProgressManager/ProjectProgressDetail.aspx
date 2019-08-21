<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectProgressDetail.aspx.cs" Inherits="WebApp.Project.ProgressManager.ProjectProgressDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目进度情况分析</title>
    <link href="../../Content/Form.css" rel="stylesheet" />
    <link href="../../Plugins/step/ystep.css" rel="stylesheet" />
    <link href="../../Content/reset.css" rel="stylesheet" />
    <link href="../../Content/thems.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>
    <script src="../../Plugins/step/setStep.js"></script>
    <script src="../../Scripts/main.js"></script>
    <script src="../../Plugins/highcharts/highcharts.js" type="text/javascript"></script>
    <script src="../../Plugins/echarts/echarts.js"></script>
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu" style="position:fixed; top:0px;z-index:300000;">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;当前位置：项目详情查看
                </div>
                <div class="MenuRight">
                </div>
            </div>
            <div class="scd clearfix" style="overflow-y: hidden; margin-top: 20px;">
                <div class="scd_x clearfix" style="margin-top:20px;">
                   <%-- <div class="title">
                        <span>项目进度</span>
                    </div>--%>
                    <div class="tjt">
                        <div class="stepCont stepCont1">
                            <!-- 菜单导航显示-->
                            <div class='ystep-container ystep-lg ystep-blue'></div>
                            <!-- 分页容器-->
                            <div class="pageCont">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="scd_x clearfix" style="">
                   <%-- <div class="title">
                        <span>项目信息</span>
                    </div>--%>
                    <div class="tjt">
                        <table class="table" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class='TdLabel' style="width:10%">项目名称</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["ProName"] %>
                                </td>
                                 <td class='TdLabel' style="width:10%">项目简介</td>
                                <td class='TdContent' style="width:23%" colspan="4">
                                    <%=document["ProSummary"] %>
                                </td>
                            </tr>
                            <tr>
                                <td class='TdLabel' style="width:10%">预估金额（万元）</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["Quota"] %>
                                </td>
                                  <td class='TdLabel' style="width:10%">资金来源</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["MoneySource"] %>
                                    <%if(document["MoneySource"]=="其他"){ %>
                                    ：<%=document["MoneySourceDesc"] %>
                                    <%} %>
                                </td>
                                <td class='TdLabel' style="width:10%">项目属性</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["ProProperty"] %>
                                </td>
                            </tr>
                            <tr>
                                <td class='TdLabel' style="width:10%">申报部门</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["StartDeptName"] %>
                                </td>
                                <td class='TdLabel' style="width:10%">项目类型</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["ProType"] %>
                                </td>
                                 <td class='TdLabel' style="width:10%">联系人</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["ContactName"] %>
                                </td>
                            </tr>
                            <tr>
                               
                                <td class='TdLabel' style="width:10%">联系人电话</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["ContactTel"] %>
                                </td>
                                 <td class='TdLabel' style="width:10%">申报时间</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["StartDate"] %>
                                </td>
                                <td class='TdLabel' style="width:10%">是否部署云平台</td>
                                <td class='TdContent' style="width:23%">
                                    <%=document["IsInCloudPlat"].ToString()=="1"?"是":"否"  %>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="scd_r  scd_r_002">
                    <div class="title">
                        <span>项目金额</span>
                        <span></span>
                    </div>
                    <div class="tjt" style="height: 280px;">
                        <div id="ProgressMap" style="width: 80%; height: 270px; margin: 25px auto 0px auto;"></div>
                    </div>
                </div>
                <div class="scd_l  scd_l_002">
                    <div class="title">
                        <span>项目资金信息云图</span>
                    </div>
                    <div class="tjt" style="height: 280px;" id="WordcloudContainer">
                    </div>
                </div>
                <div class="scd_x clearfix" style="margin-top: 20px">
                    <div class="title">
                        <span>项目文档</span>
                    </div>
                     <div class="tjt" id="divfile">

                         

                     </div>
                </div>
            </div>
        </div>
        <fieldset id="filetemplate" name="filetemplate" style="margin-top: 5px;display:none">
             <legend><span style="width: 150px; font-size: 13px; font-weight: bold"></span></legend>
             <div name="filecontent" style="margin-top: 10px"></div>
        </fieldset>                                

    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var guid = '<%=""%>';
    var table = '';
    $(function () {

       // var cfgsb = [];
       // cfgsb.refGuid = '<%=xmguid%>';
       // cfgsb.type = "database";
       // cfgsb.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
       // cfgsb.fileSign = "tz_Project_Fa";
       // cfgsb.view = true;
       // $("#filetemplate").fileviewload(cfgsb);            

        $.ajax({
            type:'post',
            url:'../ProjHandler.ashx',
            dataType:"json",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            async:false,
            data:{xmguid:'<%=xmguid%>',action:'filesearch'},
            success:function(e)
            {
                for(var i=0;i<e.data.length;i++)
                {
                    var refguid=e.data[i].refGuid;
                    var filesign=e.data[i].fileSign;
                    var divobject=$("#filetemplate").clone(true).attr("id","file_"+i);
                    divobject.css('display','inline'); 
                    var filediv=  divobject.find("div[name='filecontent']")[0]

                   // alert(filediv.tagName);
                    var cfgsb = [];
                    cfgsb.refGuid = refguid;
                    cfgsb.type = "database";
                    cfgsb.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
                    cfgsb.fileSign = filesign;
                    cfgsb.view = true;
                    $(filediv).fileviewload(cfgsb);
                    $("#divfile").append(divobject);
                }
            }
        });

        bindMap();
    });

    var step1 = new SetStep({
        content: '.stepCont1',
        showBtn: false,
        clickAble: false,
        curStep:<%=step%>
    })

    var config = new Config();
    function showDialog() {
        $('.dialog').css({ left: ($(window).width() - $(".dialog").width()) / 2, top: 20 }).slideDown();
    };

    var bindMap = function () {
        $('#ProgressMap').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: [
                    '合同额',
                    '支付额'
                ],
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: '金额'
                }
            },
            tooltip: {
            },
            plotOptions: {
                column: {
                    borderWidth: 0
                }
            },
            series: [{
                name: '金额',
                data: [parseFloat(<%=document["htje"]%>), parseFloat(<%=document["zfje"]%>)]
                }]
            });
    };
</script>

<script type="text/javascript">
    require.config({
        paths: {
            echarts: '../../Plugins/echarts'
        }
    });
    function createRandomItemStyle() {
        return {
            normal: {
                color: 'rgb(' + [
                    Math.round(Math.random() * 160),
                    Math.round(Math.random() * 160),
                    Math.round(Math.random() * 160)
                ].join(',') + ')'
            }
        };
    }

    // 使用
    require(
        [
            'echarts',
            'echarts/chart/wordCloud' 
        ],
        function (ec) {
            $("#main").height($(window).height());
            $("#main").width($(window).width());

            //生成data
            var jsonData=<%=mapJson%>;
                var tagsData = new Array(jsonData.length);
                for (var i = 0; i < tagsData.length; i++) {
                    tagsData[i] = {};
                    tagsData[i].name=jsonData[i].label;
                    tagsData[i].value=jsonData[i].value;
                    tagsData[i].itemStyle = createRandomItemStyle();
                }



                var option = {
                    title: {
                        text: '',
                        link: '#'
                    },
                    tooltip: {
                        show: false
                    },
                    series: [{
                        name: '文字云',
                        type: 'wordCloud',
                        size: ['80%', '80%'],
                        //textRotation: [0, 45, 90, -45],
                        textPadding: 5,
                        autoSize: {
                            enable: true,
                            minSize: 14
                        },
                        data: <%=mapJson%>
                    }]
                };
                console.log(option);
                var myChart = ec.init(document.getElementById('WordcloudContainer'));
                myChart.setOption(option);
            });
</script>

