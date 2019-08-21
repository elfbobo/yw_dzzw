<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp.TZ_XMYS.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0, user-scalable=0" />
    <title>项目概况</title>
    <link href="../Content/reset.css" rel="stylesheet" />
    <link href="../Content/thems.css" rel="stylesheet" />
    <script src="../Plugins/jquery.min.js"></script>
    <link href="../Content/Form.css" rel="stylesheet" />
    <script src="../Scripts/main.js"></script>
    <link href="../Content/table.css" rel="stylesheet" />
     <style>  
  
#login_click{ margin-top:32px; height:40px;}  
#login_click a   
{  
      
  
    text-decoration:none;  
    background:#2f435e;  
    color:#f2f2f2;  
      
    padding: 10px 30px 10px 30px;  
    font-size:16px;  
    font-family: 微软雅黑,宋体,Arial,Helvetica,Verdana,sans-serif;  
    font-weight:bold;  
    border-radius:3px;  
      
    -webkit-transition:all linear 0.30s;  
    -moz-transition:all linear 0.30s;  
    transition:all linear 0.30s;  
      
    }  
   #login_click a:hover { background:#385f9e; }  
  
</style> 
</head>
<body>
    <form id="form1" runat="server">
        <div class="scd clearfix">
            <div class="scd_r  scd_r_002">
                <div class="title">
                    <span>近五年验收概况</span><span></span>
                </div>
                <div class="tjt" style="height: 280px;padding:10px 10px">
                      <div class="tjt" id="zhaobiao" style="height: 300px;">
                </div>
                </div>
            </div>
            <div class="scd_l  scd_l_002">
                <div class="title">
                    <span>验收统计</span>
                </div>
                <div class="tjt" id="fanwei" style="height: 280px;">
                </div>
            </div>
      
            <div class="scd_x clearfix" style="margin-top: 20px">
                <div class="title">
                    <span>最近项目验收</span>
                    <span style="float:right"><a href="List.aspx">添加项目验收信息</a>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </div>
                <div class="tjt" style="min-height:100px;max-height:600px;overflow:auto">
                    <table id="table-2" style="width: 100%; overflow:auto" cellpadding="8" cellspacing="1">
                        <thead>
                            <tr style="background:#9dbff5">
                              <%if(!roleCheck.isBm()){ %>
                                <th>申报部门</th>
                                <%} %>
                                <th>项目名称</th>
                                <th>项目预算</th>
                                <th>资金来源</th>
                                 <th>项目类型</th>
                                <th>验收时间</th>
                                <th>验收结果</th>
                         
                            </tr>
                            <%=table %>
                        </thead>
                     
                    </table>
                </div>
            </div>
        </div>
    </form>



</body>
</html>
<script src="../../Plugins/echart/esl.js"></script>
<script src="../../Plugins/echart/echarts.js"></script>
<%--<script src="../js/echarts.min.js"></script>--%>
<script>
    require.config({
        paths: {
            echarts: '../../Plugins/echart'
        }
    });

    window.onload = function () {
     
        reload();

        zhaobiao();
        fanwei(<%=YSCount%>);


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
        require(
      [
          'echarts',
          'echarts/chart/line'
      ],
      function (ec) {
          //--- 折柱 ---
          var myChart = ec.init(document.getElementById('zhaobiao'));

          myChart.setOption({
             
              legend: {
                  data: ['项目申报数','项目验收数']
              },
              grid: {
                  left: '3%',
                  right: '4%',
                  bottom: '3%',
                  containLabel: true
              },
              toolbox: {
                  feature: {
                      saveAsImage: {}
                  }
              },
              xAxis: {
                  type: 'category',
                  boundaryGap: false,
                  data: <%=Year%>
              },
              yAxis: {
                  type: 'value'
              },
              series: [
                  {
                      name: '项目申报数',
                      type: 'line',
                      //stack: '总量',
                      data: <%=s%>
                  },
                  {
                      name: '项目验收数',
                      type: 'line',
                      //stack: '总量',
                      data:  <%=es%>
                  }
                  
              ]
          });
      });
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
               calculable: true,
               xAxis: [
                   {
                       type: 'category',
                       data: ['申报项目', '  验收项目  ']
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


</script>