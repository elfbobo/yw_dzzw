<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundManage.aspx.cs" Inherits="WebApp.FundManage.FundManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="../js/layui/layui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../js/layui/css/layui.css" />


    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../js/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/UpLoadFile.js" type="text/javascript"></script>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../Content/Form.css" rel="stylesheet" />

    <script src="../../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>

    <script src="../../Scripts/FormCore.js" type="text/javascript"></script>

    <script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
</head>
<body>

    <div style="width: 100%;">
        <div class="FormMenu">
            <div class="MenuLeft">
            </div>

            <div class="MenuRight">
                <a style="cursor: pointer" href="FundList.aspx">
                    <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>
            </div>
        </div>

        <table class="table" cellpadding="0" cellspacing="0">
            <tr>
                <td class='TdLabel'>项目名称</td>
                <td class='TdContent' colspan="3">
                    <%=dsProj.Tables[0].Rows[0]["ProName"].ToString() %>
                </td>
            </tr>
            <tr>
                <td class='TdLabel'>项目简介</td>
                <td class='TdContent' colspan="3">
                    <%=dsProj.Tables[0].Rows[0]["ProSummary"].ToString() %>
                </td>

            </tr>
            <tr>
                <td class='TdLabel'>预估金额(万元)</td>
                <td class='TdContent'>
                    <%=dsProj.Tables[0].Rows[0]["Quota"].ToString() %>
                </td>
                <td class='TdLabel'>资金来源</td>
                <td class='TdContent'>
                    <%=dsProj.Tables[0].Rows[0]["MoneySource"].ToString() %>
                    <%if(dsProj.Tables[0].Rows[0]["MoneySource"].ToString()=="其他"){ %>
                    ：<%=dsProj.Tables[0].Rows[0]["MoneySourceDesc"].ToString() %>
                    <%} %>

                </td>

            </tr>

            <tr>
                <td class='TdLabel'>项目属性</td>
                <td class='TdContent'>
                    <%=dsProj.Tables[0].Rows[0]["MoneySource"].ToString() %>
                </td>
                <td class='TdLabel'>项目类型</td>
                <td class='TdContent'>
                    <%=dsProj.Tables[0].Rows[0]["ProType"].ToString() %>
                </td>

            </tr>

            <tr>
                <td class='TdLabel'>申报部门</td>
                <td class='TdContent'>
                    <%=dsProj.Tables[0].Rows[0]["StartDeptName"].ToString() %>
                </td>
                <td class='TdLabel'>申报时间</td>
                <td class='TdContent'>
                    <%=dsProj.Tables[0].Rows[0]["StartDate"].ToString() %>
                </td>
            </tr>


        </table>
    </div>

    <div class="layui-container" style="width: 98%">
        <div class="layui-tab" style="width: 100%">
            <ul class="layui-tab-title">

                
                
                <%if(type=="zf"){ %>
                    <li>资金支付计划</li>
                    <li class="layui-this">资金支付进度</li>
                <%} else{%>
                    <li class="layui-this">资金支付计划</li>
                    <li>资金支付进度</li>
                <%} %>
               
                
                <li>资金支付概况</li>
            </ul>
            <div class="layui-tab-content">

                <%if(type=="zf"){ %>
                <div class="layui-tab-item" >
                <%}else{ %>
                <div class="layui-tab-item layui-show" >
                <%} %>
                 
                    <div style="width: 100%; text-align: right">
                        <button class="layui-btn" onclick="showzfjhdiv()"><i class="layui-icon layui-icon-add-1"></i>新建支付计划</button>
                    </div>

                    <table class="layui-table">
                        <colgroup>
                            <col width="150">
                            <col width="150">
                            <col width="200">
                            <col>
                        </colgroup>
                        <thead>
                            <tr>
                                <th>序号</th>
                                <th>支付金额（万元）</th>
                                <th>计划年度</th>
                                <th>备注</th>
                                <%if(roleCheck.isAdmin()||roleCheck.isBm()){ %>
                                <th>编辑</th>
                                <th>删除</th>
                                <%}else{ %>
                                <th>收款单位</th>
                                <%} %>
                            </tr>
                        </thead>
                        <tbody>
                            <%if (dsZfjh == null || dsZfjh.Tables[0].Rows.Count == 0)
                              { %>
                            <td colspan="7" style="text-align: center">暂无支付计划信息</td>
                            <%}
                              else
                              { %>
                            <%for (int i = 0; i < dsZfjh.Tables[0].Rows.Count; i++)
                              { %>
                            <tr>
                                <td><%=i+1 %></td>
                                <td><%=dsZfjh.Tables[0].Rows[i]["zfje"].ToString() %></td>
                                <td><%=dsZfjh.Tables[0].Rows[i]["nd"].ToString() %></td>
                                <td><%=dsZfjh.Tables[0].Rows[i]["beizhu"].ToString() %></td>
                                <%if(roleCheck.isAdmin()||roleCheck.isBm()){ %>
                                <td><a style="cursor: pointer; color: #1E9FFF" onclick="zjjhedit('<%=dsZfjh.Tables[0].Rows[i]["guid"].ToString() %>')">编辑</a></td>
                                <td><a style="cursor: pointer; color: #1E9FFF" onclick="zjjhdel('<%=dsZfjh.Tables[0].Rows[i]["guid"].ToString() %>')">删除</a></td>
                                <%}else{ %>
                                <td><%=dsZfjh.Tables[0].Rows[i]["skdw"].ToString() %></td>
                                <%} %>
                            </tr>
                            <%}
                              } %>
                        </tbody>
                    </table>
                </div>

                <%if(type=="zf"){ %>
                <div class="layui-tab-item layui-show" >
                <%}else{ %>
                <div class="layui-tab-item " >
                <%} %>
                
                    <div style="width: 100%; text-align: right">
                        <button class="layui-btn" onclick="showzjzfdiv()"><i class="layui-icon layui-icon-add-1"></i>新建支付信息</button>
                    </div>
                    <table class="layui-table">
                        <colgroup>
                            <col width="150">
                            <col width="150">
                            <col width="200">
                            <col>
                        </colgroup>
                        <thead>
                            <tr>
                                <th>序号</th>
                                <th>支付时间</th>
                                <th>支付金额（万元）</th>
                                <th>收款单位</th>
                                <th>支付凭证</th>
                                <%if(roleCheck.isAdmin()||roleCheck.isBm()){ %>
                                <th>编辑</th>
                                <th>删除</th>
                                <%} %>
                            </tr>
                        </thead>
                        <tbody>
                            <%if (dsZfqk == null || dsZfqk.Tables[0].Rows.Count == 0)
                              { %>
                            <td colspan="7" style="text-align: center">暂无支付信息</td>
                            <%}
                              else
                              { %>
                            <%for (int i = 0; i < dsZfqk.Tables[0].Rows.Count; i++)
                              { %>
                            <tr>
                                <td><%=i+1 %></td>
                                <td><%=dsZfqk.Tables[0].Rows[i]["zfsj"].ToString().Substring(0,dsZfqk.Tables[0].Rows[i]["zfsj"].ToString().Length-7) %></td>
                                <td><%=dsZfqk.Tables[0].Rows[i]["zfje"].ToString() %></td>
                                <td><%=dsZfqk.Tables[0].Rows[i]["skdw"].ToString() %></td>
                                <td><a style="cursor: pointer; color: #1E9FFF" onclick="ckzfps('<%=dsZfqk.Tables[0].Rows[i]["guid"].ToString() %>')">查看</a></td>
                                <%if(roleCheck.isAdmin()||roleCheck.isBm()){ %>
                                <td><a style="cursor: pointer; color: #1E9FFF" onclick="zjzfedit('<%=dsZfqk.Tables[0].Rows[i]["guid"].ToString() %>')">编辑</a></td>
                                <td><a style="cursor: pointer; color: #1E9FFF" onclick="zjzfdel('<%=dsZfqk.Tables[0].Rows[i]["guid"].ToString() %>')">删除</a></td>
                                <%} %>
                            </tr>
                            <%}
                              } %>
                        </tbody>
                    </table>
                </div>
               
               

                <div class="layui-tab-item">
                    <div style="width: 100%">
                        <div style="width: 500px; height: 400px; float: left" id="zfbldiv"></div>
                        <div style="width: 500px; height:400px;float: left" id="zfdbdiv"></div>
                    </div>
                </div>

            </div>

        </div>
        <script src="js/echarts.min.js" type="text/javascript"></script>

        <script>
            $(function () {

                $.ajax({
                    type: 'post',
                    url: 'FundHandler.ashx',
                    dataType:"json",
                    data: { action: 'zftj', proguid: '<%=strProjGuid%>' },
                    success: function (e) {

                        var myChart = echarts.init(document.getElementById('zfbldiv'));
                        var myChart2 = echarts.init(document.getElementById('zfdbdiv'));


                        // 指定图表的配置项和数据
                        var option = {
                            title: {
                                text: '资金支付概况',
                                subtext: '资金支付概况',
                                x: 'center'
                            },
                            tooltip: {
                                trigger: 'item',
                                formatter: "{a} <br/>{b} : {c} ({d}%)"
                            },
                            legend: {
                                orient: 'vertical',
                                left: 'left',
                                data: ['已支付', '待支付']
                            },
                            series: [
                                {
                                    name: '资金',
                                    type: 'pie',
                                    radius: '55%',
                                    center: ['50%', '60%'],
                                    data: [
                                        { value: e[2].yzf, name: '已支付' },
                                        { value: e[2].dzf, name: '待支付' }
                                    ],
                                    itemStyle: {
                                        emphasis: {
                                            shadowBlur: 10,
                                            shadowOffsetX: 0,
                                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                                        }
                                    }
                                }
                            ]
                        };



                        // 使用刚指定的配置项和数据显示图表。
                        myChart.setOption(option);

                        var option2 = {
                            title: {
                                text: '实际支付'
                            },
                            tooltip: {
                                trigger: 'axis'
                            },
                            legend: {
                                data: ['支付计划金额', '实际支付金额']
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
                                //boundaryGap: false,
                                data: ['<%=curYear-2%>', '<%=curYear-1%>', '<%=curYear%>', '<%=curYear+1%>']
                            },
                            yAxis: {
                                type: 'value'
                            },
                            series: [
                                {
                                    name: '支付计划金额',
                                    type: 'line',
                                    //stack: '总量',
                                    data: [e[1].data0, e[1].data1, e[1].data2, e[1].data3]
                                },
                                {
                                    name: '实际支付金额',
                                    type: 'line',
                                    //stack: '总量',
                                    data: [e[0].data0, e[0].data1, e[0].data2, e[0].data3]
                                }
                            ]
                        };
                        myChart2.setOption(option2);
                    }
                });

                
                $('input[double="true"]').keyup(function () {
                    FloatOnly($(this).get(0));
                });

                zjzfguid = uuid();
                var cfg0 = [];
                cfg0.content = "filecontent0";
                cfg0.refGuid = zjzfguid;
                cfg0.type = "database";
                cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
                cfg0.fileSign = "tz_zjzf_zfpz";
                cfg0.title = "支付凭证";

                $("#file0").uploadfile(cfg0);


                var cfg1 = [];
                cfg1.content = "filecontent1";
                cfg1.refGuid = zjzfguid;
                cfg1.type = "database";
                cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
                cfg1.fileSign = "tz_zjzf_fj";
                cfg1.title = "附件";

                $("#file1").uploadfile(cfg1);

                $("#createzjzfdiv").hide();
            });

            function zjzfdel(guid) {
                $.ajax({
                    type: 'post',
                    url: 'FundHandler.ashx',
                    data: { action: 'zjzfdel', guid: guid },
                    success: function (e) {
                        if (e == 1) {
                            alert("删除成功");
                            window.location.href = '?xmguid=<%=strProjGuid%>&type=zf';
                        }
                        else {
                            alert("删除失败");
                        }
                    }
                });
            }

            function zjzfedit(guid) {

            }

            function zjjhdel(guid) {
                $.ajax({
                    type: 'post',
                    url: 'FundHandler.ashx',
                    data: { action: 'zfjhdel', guid: guid },
                    success: function (e) {
                        if (e == 1) {
                            alert("删除成功");
                            window.location.href = '?xmguid=<%=strProjGuid%>';
                        }
                        else {
                            alert("删除失败");
                        }
                    }
                });
            }

            function zjjhedit(guid) {
                return;
                layui.use('layer', function () {
                    var layer = layui.layer;
                    //自定页
                    layui.layer.open({
                        type: 1,
                        title: '编辑支付计划',
                        skin: 'layui-layer-demo', //样式类名
                        area: ['900px', '250px'],
                        closeBtn: 1, //不显示关闭按钮
                        anim: 2,
                        shadeClose: true, //开启遮罩关闭
                        content: $('#createzfjhdiv')
                    });

                });
            }



            layui.use('element', function () {
                var $ = layui.jquery
                , element = layui.element; //Tab的切换功能，切换事件监听等，需要依赖element模块

            });

            function ckzfps(psfiles) {
                $("#filecontentPsView").empty();
                var cfgPsView = [];
                cfgPsView.refGuid = psfiles;
                cfgPsView.type = "database";
                cfgPsView.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";

                cfgPsView.fileSign = "tz_zjzf_zfpz";

                cfgPsView.view = true;
                cfgPsView.title = "评审意见"
                $("#filecontentPsView").fileviewload(cfgPsView);

                layui.use('layer', function () {
                    var layer = layui.layer;

                    //自定页
                    layui.layer.open({
                        type: 1,
                        title: '支付凭证',
                        skin: 'layui-layer-demo', //样式类名
                        closeBtn: 1, //不显示关闭按钮
                        anim: 2,
                        shadeClose: true, //开启遮罩关闭
                        content: $('#psFilesDiv')
                    });
                });

            }


            var zjzfguid = "";
            function showzjzfdiv() {
                zjzfguid = uuid();
                $("#filecontent0").children().remove();
                $("#filecontent1").children().remove();
                //每次打开新建窗口时生成guid，并初始化附件

                $("#zfje").val("");
                $("#zfsj").val("");
                $("#skdw").val("");
                var cfg0 = [];
                cfg0.content = "filecontent0";
                cfg0.refGuid = zjzfguid;
                cfg0.type = "database";
                cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
                cfg0.fileSign = "tz_zjzf_zfpz";
                cfg0.title = "支付凭证";

                $("#file0").uploadfile(cfg0);


                var cfg1 = [];
                cfg1.content = "filecontent1";
                cfg1.refGuid = zjzfguid;
                cfg1.type = "database";
                cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
                cfg1.fileSign = "tz_zjzf_fj";
                cfg1.title = "附件";

                $("#file1").uploadfile(cfg1);
                layui.use('layer', function () {
                    var layer = layui.layer;

                    //自定页
                    layui.layer.open({
                        type: 1,
                        title: '新建支付信息',
                        skin: 'layui-layer-demo', //样式类名
                        area: ['900px', '400px'],
                        closeBtn: 1, //不显示关闭按钮
                        anim: 2,
                        shadeClose: true, //开启遮罩关闭
                        content: $('#createzjzfdiv')
                    });
                    
                });

            }

            function showzfjhdiv() {
                layui.use('layer', function () {
                    var layer = layui.layer;

                    //自定页
                    layui.layer.open({
                        type: 1,
                        title: '新建支付计划信息',
                        skin: 'layui-layer-demo', //样式类名
                        area: ['900px', '250px'],
                        closeBtn: 1, //不显示关闭按钮
                        anim: 2,
                        shadeClose: true, //开启遮罩关闭
                        content: $('#createzfjhdiv')
                    });
                });
            }

            function closeLayer() {
                layui.use('layer', function () {
                    var layer = layui.layer;

                    //自定页
                    layer.closeAll();
                });
            }

            //保存资金支付信息
            function savezjzf() {
                if ($("#zfje").val() == "") {
                    alert("请输入支付金额");
                    return;
                }
                if ($("#zfsj").val() == "") {
                    alert("请选择支付时间");
                    return;
                }
                $.ajax({
                    type: 'POST',
                    url: "FundHandler.ashx",
                    data: { action: "zjzfsubmit", xmguid: '<%=strProjGuid%>', guid: zjzfguid, zfje: $("#zfje").val(), zfsj: $("#zfsj").val(), skdw: $("#skdw").val(), beizhu: $("#beizhu").val() },
                    success: function (e) {
                        //console.log("sucess");
                        if (e == 1) {
                            alert("保存成功");
                            location.reload()
                        } else {
                            alert("保存失败！");
                        }
                    }
                });
            }

            function savezfjh() {
                if ($("#jhzfje").val() == "") {
                    alert("请输入支付金额");
                    return;
                }

                $.ajax({
                    type: 'POST',
                    url: "FundHandler.ashx",
                    data: { action: "zfjhsubmit", xmguid: '<%=strProjGuid%>', guid: zjzfguid, zfje: $("#jhzfje").val(), nd: $("#jhzfnd").val(), skdw: $("#jhskdw").val(), beizhu: $("#jhbeizhu").val() },
                    success: function (e) {
                        //console.log("sucess");
                        if (e == 1) {
                            alert("保存成功");
                            location.reload()
                        } else {
                            alert("保存失败！");
                        }
                    }
                });
            }

            function uuid() {
                var s = [];
                var hexDigits = "0123456789abcdef";
                for (var i = 0; i < 36; i++) {
                    s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
                }
                s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
                s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
                s[8] = s[13] = s[18] = s[23] = "-";

                var uuid = s.join("");
                return uuid;
            }



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
        </script>
    </div>
    <div id="psFilesDiv" style="display: none">
        <div id="filecontentPsView" style="margin-top: 10px"></div>
    </div>


    <div id="createzjzfdiv">
        <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
            <tr>
                <td class="TdLabel" style="width: 15%"><font style="color: red">*</font>支付金额(万元)</td>
                <td class='TdContent' style="width: 35%">
                    <input type="text" id="zfje" double="true" />

                </td>
                <td class="TdLabel" style="width: 15%"><font style="color: red">*</font>支付时间</td>
                <td class="TdContent">
                    <input class="Wdate" id="zfsj" onfocus="WdatePicker()" readonly="true" />
                </td>
            </tr>
            <tr>
                <td class="TdLabel" style="width: 15%">收款单位</td>
                <td class='TdContent' style="width: 35%" colspan="3">
                    <input type="text" id="skdw" />
                </td>
            </tr>
            <tr>
                <td class="TdLabel" style="width: 15%">备注</td>
                <td class='TdContent' style="width: 35%" colspan="3">
                    <input type="text" id="beizhu" />
                </td>
            </tr>

        </table>

        <div style="margin: 13px">
            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 150px; font-size: 13px; font-weight: bold">支付凭证</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend><span id="file1" style="width: 150px; font-size: 13px; font-weight: bold">附件</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>
        </div>
        <div style="width: 100%; text-align: center; margin-top: 15px">
            <button class="layui-btn layui-btn-normal" style="margin-left: -30px" onclick="savezjzf()">保存</button>
            <button class="layui-btn layui-btn-primary" style="margin-left: 30px" onclick="closeLayer()">取消</button>
        </div>
    </div>

    <div id="createzfjhdiv" style="display: none">

        <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
            <tr>
                <td class="TdLabel" style="width: 15%"><font style="color: red">*</font>计划支付金额(万元)</td>
                <td class='TdContent' style="width: 35%">
                    <input type="text" id="jhzfje" double="true" />

                </td>
                <td class="TdLabel" style="width: 15%"><font style="color: red">*</font>计划年度</td>
                <td class="TdContent">
                    <select id="jhzfnd">
                        <option value="<%=projSbYear-5 %>"><%=projSbYear-5 %></option>
                        <option value="<%=projSbYear-4 %>"><%=projSbYear-4 %></option>
                        <option value="<%=projSbYear-3 %>"><%=projSbYear-3 %></option>
                        <option value="<%=projSbYear-2 %>"><%=projSbYear-2 %></option>
                        <option value="<%=projSbYear-1 %>"><%=projSbYear-1 %></option>
                        <option value="<%=projSbYear %>"><%=projSbYear %></option>
                        <option value="<%=projSbYear+1 %>"><%=projSbYear+1 %></option>
                        <option value="<%=projSbYear+2 %>"><%=projSbYear+2 %></option>
                        <option value="<%=projSbYear+3 %>"><%=projSbYear+3 %></option>
                        <option value="<%=projSbYear+4 %>"><%=projSbYear+4 %></option>
                        <option value="<%=projSbYear+5 %>"><%=projSbYear+5 %></option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="TdLabel" style="width: 15%">收款单位</td>
                <td class='TdContent' style="width: 35%" colspan="3">
                    <input type="text" id="jhskdw" />
                </td>
            </tr>
            <tr>
                <td class="TdLabel" style="width: 15%">备注</td>
                <td class='TdContent' style="width: 35%" colspan="3">
                    <input type="text" id="jhbeizhu" />
                </td>
            </tr>

        </table>

        <div style="width: 100%; text-align: center; margin-top: 15px">
            <button class="layui-btn layui-btn-normal" style="margin-left: -30px" onclick="savezfjh()">保存</button>
            <button class="layui-btn layui-btn-primary" style="margin-left: 30px" onclick="closeLayer()">取消</button>
        </div>
    </div>
</body>
</html>
