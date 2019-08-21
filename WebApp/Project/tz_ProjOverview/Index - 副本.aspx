<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp.Project.tz_ProjOverview.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0, user-scalable=0" />
    <title>项目概况</title>
    <link href="../../Content/reset.css" rel="stylesheet" />
    <link href="../../Content/thems.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js"></script>
    <link href="../../Content/Form.css" rel="stylesheet" />
    <script src="../../Scripts/main.js"></script>
    <link href="../../Content/table.css" rel="stylesheet" />
    <script src="../../js/highcharts.js" type="text/javascript"></script>


    <!--[if lt IE 9]>
<script src="../../js/oldie.js"></script>
<![endif]-->

        <script src="../../Plugins/echart/esl.js"></script>
    <script src="../../Plugins/echart/echarts.js"></script>
    <style>
        #login_click {
            margin-top: 32px;
            height: 40px;
        }

            #login_click a {
                text-decoration: none;
                background: #2f435e;
                color: #f2f2f2;
                padding: 10px 30px 10px 30px;
                font-size: 16px;
                font-family: 微软雅黑,宋体,Arial,Helvetica,Verdana,sans-serif;
                font-weight: bold;
                border-radius: 3px;
                -webkit-transition: all linear 0.30s;
                -moz-transition: all linear 0.30s;
                transition: all linear 0.30s;
            }

                #login_click a:hover {
                    background: #385f9e;
                }

        .tab-header {
            display: inline-block;
            height: 27px;
            margin-bottom: 30px;
        }

            .tab-header .active {
                border-bottom: 2px solid #03A4DB;
                position: relative;
                bottom: -2px;
            }

                .tab-header .active a {
                    color: #03A4DB;
                }

            .tab-header li {
                float: left;
                width: 140px;
                height: 47px;
                line-height: 47px;
                text-align: center;
                font-size: 16px;
                position: relative;
                bottom: -2px;
                margin: 0 20px;
            }

            .tab-header a {
                color: #404040;
            }

                .tab-header a:hover {
                    color: #03A4DB;
                    display: block;
                    border-bottom: 3px solid #03A4DB;
                    position: relative;
                    bottom: 0px;
                }

            .tab-header span {
                padding-right: 5px;
            }

        .tongji {
            width: 784px;
            margin-right: 22px;
            float: left;
        }

        .bmjk-table {
            width: 378px;
            float: right;
        }

            .bmjk-table h3 {
                height: 40px;
                line-height: 40px;
                font-size: 18px;
                color: #404040;
                margin-bottom: 10px;
            }

        .table-wk {
            width: 380px;
            border: 1px solid #ced3ce;
        }

        .tablelist tbody tr.odd {
            background: #f7f6f6;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="scd clearfix">
            <div class="scd_r  scd_r_002">
                <div class="title">
                    <span>近五年项目概况</span><span></span>
                </div>
                <div class="tjt" style="height: 280px; padding: 10px 10px">
                    <table id="table-1" style="width: 100%;" cellpadding="8" cellspacing="1">
                        <thead>
                            <tr style="background: #9dbff5">
                                <th>年度</th>
                                <th>项目申报</th>
                                <th>项目立项</th>
                                <th>项目验收</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%=yearSb.ToString() %>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="scd_l  scd_l_002">
                <div class="title">
                    <span>资金范围统计</span>
                </div>
                <div class="tjt" id="fanwei" style="height: 280px;">
                </div>
            </div>
            <div class="scd_r  scd_r_002" style="margin-top: 20px">
                <div class="title">
                    <span>采购方式</span><span></span>
                </div>
                <div class="tjt" id="zhaobiao" style="height: 300px;">
                </div>
            </div>
            <div class="scd_l  scd_l_002" style="margin-top: 20px">
                <div class="title">
                    <span>资金使用情况</span>
                </div>
                <div class="tjt" id="zjsy" style="height: 280px;">
                </div>
            </div>


            <!--部门分布情况-->
            <div style="width: 100%; height: auto; margin-top: 30px; clear: both;">
                <div style="width: 100%; font-size: 22px; color: #404040; height: auto; text-align: center;">
                    政务信息资源分布情况
                </div>
                <div style="text-align: center; background: #F8F8F8; margin-top: 10px">
                    <ul class="tab-header">
                        <li id="libmtjheader" class="active" onclick="switchtobmtj()"><a><span>
                            <img src="../../images/d07.png"></span>部门信息资源</a></li>
                        <li id="liqstjheader" onclick="switchtoqstj()"><a><span>
                            <img src="../../images/d08.png"></span>区市信息资源</a></li>
                    </ul>
                </div>
                <div class="tongji clearfix" style="height: 600px">
                    <div></div>
                    <div id="divchart" style="width: 100%; height: 500px"></div>
                </div>
                <div class="bmjk-table clearfix">
                    <h3>部门目录情况</h3>
                    <div class="table-wk clearfix" style="height: 500px; padding: -2px,0px,-2px,0px; width: 380px; overflow-y: auto; OVERFLOW-X: hidden;">
                        <table class="tablelist clearfix">
                            <thead>
                                <tr>
                                    <th style="width: 49%">部门</th>
                                    <th style="width: 49%">数量（个）</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>2</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>


                </div>
            </div>


            <div class="scd_x clearfix" style="margin-top: 20px">
                <div class="title">
                    <span><%=tableTitle %></span>
                </div>
                <div class="tjt" style="min-height: 100px; max-height: 600px; overflow: auto">
                    <table id="table-2" style="width: 100%; overflow: auto" cellpadding="8" cellspacing="1">
                        <thead>
                            <tr style="background: #9dbff5">
                                <%if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
                                  { %>
                                <th>部门名称</th>
                                <th>项目数</th>
                                <th>已立项</th>
                                <th>已验收</th>
                                <th>预算总额（万元）</th>
                                <th>合同总额（万元）</th>
                                <th>支付总额（万元）</th>
                                <%}
                                  else
                                  { %>
                                <th>序号</th>
                                <th>项目名称</th>
                                <th>申报日期</th>
                                <th>预算金额（万元）</th>
                                <th>合同金额（万元）</th>
                                <th>支付金额（万元）</th>
                                <th>项目阶段</th>
                                <%} %>
                            </tr>
                        </thead>
                        <tbody>
                            <%=bmSb.ToString() %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>

    <div class="dialog" id="dialogreturn" style="height: 250px; width: 590px; display: none; position: fixed;">
        <img src="../../Images/cross.png" style="position: absolute; right: 3px; top: 1px; cursor: pointer" onclick="$('#dialogreturn').slideUp();" />
        <div class="head"><span class="title" style="padding-left: 20px; font-size: 18px">修改密码</span></div>
        <span style="font-size: 10px; color: red; margin-top: 25px; text-align: left; width: 100%">首次登录，建议您修改默认登录密码！</span>
        <table style="width: 90%; margin: auto; margin-top: 25px" class="table" cellpadding="0" cellspacing="0">
            <tr>

                <td class="TdLabel" style="width: 20%">新密码：</td>
                <td class="TdContent" style="width: 80%">
                    <input type="password" id="password" style="width: 90%" />

                </td>
            </tr>
            <tr>
                <td class="TdLabel" style="width: 20%">重复密码</td>
                <td class='TdContent' style="width: 80%">
                    <input type="password" id="repassword" style="width: 90%" />

                </td>
            </tr>
        </table>
        <div style="width: 100%; text-align: center">
            <div id="login_click" onclick="pswsave()">
                <a id="btlogin" href="#">保 存</a>
            </div>
        </div>
    </div>

</body>
</html>


<%--<script src="js/echarts.min.js" type="text/javascript"></script>--%>
<script type="text/javascript">

    function bmxmtj()
    {
        require(
     [
         'echarts',
         'echarts/chart/bar'
     ],
     function (ec) {
         var myChart = ec.init(document.getElementById('divchart'));
    
         var option = {
             grid: { y2: 140 },
             xAxis: {
                 type: 'category',
                 data: [<%for (int i = 0; i < dszftj.Tables[0].Rows.Count; i++)
                     {%>
                    '<%=dszftj.Tables[0].Rows[i]["startdeptname"].ToString()%>'
                    <%if (i != dszftj.Tables[0].Rows.Count - 1)
                      {%>
                    ,<%}
                         }%>],
            axisLabel: {
                interval: 0,//横轴信息全部显示  
                rotate: -30//-30度角倾斜显示  
            }
        },
        yAxis: {
            type: 'value'
        },
        dataZoom: [
    {
        type: 'slider',//图表下方的伸缩条
        show: false,  //是否显示
        realtime: true,  //
        start: 0,  //伸缩条开始位置（1-100），可以随时更改
        end: 1500 / <%=dszftj.Tables[0].Rows.Count%>  //伸缩条结束位置（1-100），可以随时更改
        },
    {
        type: 'inside',  //鼠标滚轮
        realtime: true
        //还有很多属性可以设置，详见文档
    },
        ],
        tooltip: {},
        series: [{
            data: [<%for (int i = 0; i < dszftj.Tables[0].Rows.Count; i++)
                     {%>
                    <%=Convert.ToInt32(dszftj.Tables[0].Rows[i]["xmcount"].ToString())%>
                    <%if (i != dszftj.Tables[0].Rows.Count - 1)
                      {%>
                    ,<%}
                         }%>],
            type: 'bar',
            barWidth: 25,
            itemStyle: {
                normal: {
                    
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: '#7D68FE'
                    }, {
                        offset: 1,
                        color: '#146BFB'
                    }])
                }
            }
        },

        ]
    };

         // 为echarts对象加载数据 #0C6C97
         myChart.setOption(option);
     });
    //首页加载部门统计图
    // 基于准备好的dom，初始化echarts图表
        

    }

   
   

</script>

<script>

    function switchtobmtj() {
        $("#libmtjheader").attr("class", "active");
        $("#liqstjheader").removeClass("active");
    }

    function switchtoqstj() {
        $("#liqstjheader").attr("class", "active");
        $("#libmtjheader").removeClass("active");
    }

    require.config({
        paths: {
            echarts: '../../Plugins/echart'
        }
    });

    window.onload = function () {
        <%if (isFirstView)
          {%>
        $('.dialog').css({ left: ($(window).width() - $(".dialog").width()) / 2, top: 20 }).slideDown();
        <%}%>
        reload();


        zjsy(<%=sySb.ToString()%>);
        fanwei(<%=fwSb.ToString()%>);

        zhaobiao(<%=zbSb.ToString()%>);
        <% if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
            {%>
        bmxmtj();
        <%}%>
       
    }

    window.onresize = function () {
        reload();
    }

    //资金使用情况
    function zjsy(dat) {
        require(
      [
          'echarts',
          'echarts/chart/bar'
      ],
      function (ec) {
          //--- 折柱 ---
          var myChart = ec.init(document.getElementById('zjsy'));

          myChart.setOption({
              grid: {
                  x: 50,
                  y: 20,
                  x2: 20,
                  y2: 40,
                  show: true,  //表示开启
                  borderColor: "#e4e4e4",//折线图的边宽颜色
                  shadowBlur: 50,
                  containLabel: 50
              },
              calculable: true,
              xAxis: [
                  {
                      type: 'category',
                      data: ['已支付完成', '未支付完成']
                  }
              ],
              yAxis: [

                  {
                      type: 'value',
                      axisLabel: {
                          interval: 0,
                          rotate: 40
                      }
                  }
              ],
              grid: {
                  top: 15
              },
              series: [
                  {
                      name: '项目数',
                      type: 'bar',
                      data: dat,
                      barWidth: 60,
                      itemStyle:
                          {
                              normal:
                                {
                                    label: { show: true, position: 'top' },
                                    color: function (params) {
                                        var colorList = ['#16A085', '#4A235A', '#C39BD3 ', '#F9E79F', '#BA4A00', '#ECF0F1', '#616A6B', '#EAF2F8', '#4A235A', '#3498DB'];
                                        return colorList[params.dataIndex];
                                    }
                                }
                          }
                  }
              ]
          });
      });
    }

    //招标方式
    function zhaobiao(dat) {
        if (dat[0].value == 0 && dat[1].value == 0 && dat[2].value == 0 && dat[3].value == 0 && dat[4].value == 0 && dat[5].value == 0 && dat[6].value == 0) {
            require(
     [
         'echarts',
         'echarts/chart/pie'
     ],
     function (ec) {
         //--- 折柱 ---
         var myChart = ec.init(document.getElementById('zhaobiao'));

         myChart.setOption({
             grid: {
                 x: 50,
                 y: 20,
                 x2: 20,
                 y2: 40,
                 show: true,  //表示开启
                 borderColor: "#e4e4e4",//折线图的边宽颜色
                 shadowBlur: 50,
                 containLabel: 50
             },
             calculable: true,
             xAxis: [
                 {
                     type: 'category',
                     data: ['', '']
                 }
             ],
             yAxis: [

                 {
                     type: 'value',
                     axisLabel: {
                         interval: 0,
                         rotate: 40
                     }
                 }
             ],
             grid: {
                 top: 15
             },
             series: [
                 {
                     name: '',
                     type: 'bar',
                     data: [],
                     barWidth: 60,
                     itemStyle:
                         {
                             normal:
                               {
                                   label: { show: true, position: 'top' },
                                   color: function (params) {
                                       var colorList = ['#16A085', '#4A235A', '#C39BD3 ', '#F9E79F', '#BA4A00', '#ECF0F1', '#616A6B', '#EAF2F8', '#4A235A', '#3498DB'];
                                       return colorList[params.dataIndex];
                                   }
                               }
                         }
                 }
             ]
         });
     });
            return;
        }

        Highcharts.chart('zhaobiao', {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: ''
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    }
                }
            },
            series: [{
                name: 'Brands',
                colorByPoint: true,
                data: [{
                    name: '公开招标',
                    y: dat[0].value
                }, {
                    name: '询价',
                    y: dat[1].value
                }, {
                    name: '单一来源',
                    y: dat[2].value
                }, {
                    name: '自行采购',
                    y: dat[3].value
                }, {
                    name: '竞争性磋商',
                    y: dat[4].value
                }, {
                    name: '邀请招标',
                    y: dat[5].value
                }, {
                    name: '竞争性谈判',
                    y: dat[6].value
                }]
            }]
        });

        $(".highcharts-credits").hide();
       
    }

    //资金范围统计
    function fanwei(dat) {
        require(
       [
           'echarts',
           'echarts/chart/bar'
       ],
       function (ec) {
           //--- 折柱 ---
           var myChart = ec.init(document.getElementById('fanwei'));

           myChart.setOption({

               grid: {
                   x: 30,
                   y: 20,
                   x2: 20,
                   y2: 40,
                   show: true,  //表示开启
                   borderColor: "#e4e4e4",//折线图的边宽颜色
                   shadowBlur: 50,
                   containLabel: 50
               },
               grid: {
                   top: 15
               },
               calculable: true,
               xAxis: [
                   {
                       type: 'category',
                       data: ['100万以内', '  100万到200万  ', '  200万到500万  ', '  500万以上  '],
                       axisLabel: {
                           interval: 0,
                           rotate: 0
                       }
                   }
               ],

               yAxis: [
                   {
                       type: 'value'
                   }
               ],
               series: [
                   {
                       name: '项目数',
                       type: 'bar',
                       data: dat,
                       barWidth: 60,
                       itemStyle:
                           {
                               normal:
                                 {
                                     label: { show: true, position: 'top' },
                                     color: function (params) {
                                         var colorList = ['#C33531', '#EFE42A', '#64BD3D', '#EE9201', '#29AAE3', '#B74AE5', '#0AAF9F', '#E89589', '#16A085', '#4A235A', '#C39BD3 ', '#F9E79F', '#BA4A00', '#ECF0F1', '#616A6B', '#EAF2F8', '#4A235A', '#3498DB'];
                                         return colorList[params.dataIndex];
                                     }
                                 }
                           }
                   }
               ]
           });
       });
    }

    //保存密码
    function pswsave() {
        var password = $("#password").val();
        var repassword = $("#repassword").val();
        if (password != repassword) {
            alert("两次输入密码不一致！")
        }
        else {
            $.ajax({
                type: 'POST',
                url: "../ProjHandler.ashx",
                data: { action: "changepswd", password: password, userguid: "<%=CurrentUser.UserGuid%>" },
                success: function (e) {
                    //console.log("sucess");
                    alert("密码修改成功！");
                    $('#dialogreturn').slideUp();
                }
            });
        }
    }
</script>
