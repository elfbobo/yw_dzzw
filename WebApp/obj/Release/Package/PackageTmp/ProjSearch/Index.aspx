<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp.ProjSearch.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>综合查询</title>
    <script src="../js/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="../Content/search/js/ui.tab.js"></script>
    <link href="../Content/appStyle.css" rel="stylesheet" />
    <link href="../Content/search/style/list.css" rel="stylesheet" />
    <link href="../Content/search/search.css" rel="stylesheet" />
    <link href="../Content/search/fycss.css" rel="stylesheet" />
    <link href="../js/zTree/zTreeStyle.css" rel="stylesheet" />
    <script src="../js/zTree/jquery.ztree.all.min.js" type="text/javascript"></script>

    <script src="../js/layui/layui.js" type="text/javascript"></script>
    <link href="../js/layui/css/layui.css" rel="stylesheet" />

    <script src="../js/jquery.form.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#selectList").find(".more").toggle(function () {
                $(this).addClass("more_bg");
                $(".more-none").slideDown()
            }, function () {
                $(this).removeClass("more_bg");
                $(".more-none").slideUp()
            });
        });
    </script>
    <style>
        .sfixed {
            position: fixed;
            top: -10px;
            z-index: 10;
        }

        .adminform {
            float: right;
            width: 80%;
        }

        .titlefont {
            border-bottom: 2px solid #ffa63c;
        }

    </style>

</head>
<body style="background-color: #f6f6f6;">

    <%if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
      { %>
    <div id="divmenu" style="width: 20%; float: left; overflow-y: scroll; min-height: 450px">
        <ul id="treeDemo" class="ztree"></ul>
    </div>
    <%} %>
    <form id="form1" name="form1" runat="server" class="adminform" action="upload.ashx"  method="post" enctype="multipart/form-data">

        <div class="w1000">
            <div class="words12">
                <div class="words12_zzgz_xfgz6">
                    <div class="words12_zzgz_xfgz6_title" style="background: #ffffff">
                        <font class="titlefont" id="title_search">项目信息</font>
                        <font id="title_file">资金安排表</font>
                    </div>

                    <div class="words12_zzgz_xfgz6_content" id="div_file" style="display: none">


                        <%if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
                          { %>
                         <div style="width: 100%; background: #ffffff; margin-top: 15px; padding-top: 15px">
                              <a id="a_file" style="cursor: pointer">点击下载2016—2018年泰州市电子政务项目资金安排汇总表</a>
                         </div>


                        <%}
                          else
                          { %>
                        <div style="width: 100%; background: #ffffff; margin-top: 15px; padding-top: 15px">
                            <ul class="layui-timeline">
                                <li class="layui-timeline-item">
                                    <i class="layui-icon layui-timeline-axis"></i>
                                    <div class="layui-timeline-content layui-text">
                                        <h3 class="layui-timeline-title">第一步：下载并填写2016-2018年泰州市电子政务项目资金安排表</h3>
                                        <a id="a_file" style="cursor: pointer">点击下载2016—2018年泰州市电子政务项目资金安排清单修改模板</a>
                                    </div>
                                </li>
                                <li class="layui-timeline-item">
                                    <i class="layui-icon layui-timeline-axis"></i>
                                    <div class="layui-timeline-content layui-text">
                                        <h3 class="layui-timeline-title">第二步：选择并上传填写完成的资金安排表</h3>
                                        <br />
                                        <div class="layui-upload">
                                            选择文件:
                                            <span id="span_filename"></span>
                                            <br/>

                                          <label type="button"  for="file1" class="layui-btn layui-btn-normal" id="filebtn" >选择文件</label>

                                            <input type="file" name="file1" id="file1"  style="margin-top:-1000px;width:10px"/>
                                        <%--<button class="layui-btn" id="upload">上传</button>--%>
                                        <input type="button" id="upload" class="layui-btn" value="开始上传" />
                                        </div>

                                        
                                    </div>
                                </li>
                                <li class="layui-timeline-item">
                                    <i class="layui-icon layui-timeline-axis"></i>
                                    <div class="layui-timeline-content layui-text">
                                        <h3 class="layui-timeline-title">第三步：预览</h3>

                                    </div>
                                </li>

                            </ul>

                            <div class="layui-form">

                                <div style="text-align: center; width: 100%">
                                    <span style="font-weight: bold; font-size: 16px">2016—2018年泰州市电子政务项目资金安排清单</span>

                                </div>
                                <div style="text-align: right; width: 100%">
                                    <span style="font-size: 14px">单位（万元）</span>

                                </div>
                                <table class="layui-table" lay-size="sm" id="mantable">

                                    <thead>

                                        <tr>
                                            <th>序号</th>
                                            <th>项目名称</th>
                                            <th>合同金额</th>
                                            <th>已支付金额</th>
                                            <th colspan="2">资金组成</th>
                                            <th colspan="2">支付计划表</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>


                        <%} %>
                    </div>

                    <div class="words12_zzgz_xfgz6_content" id="div_search">
                        <div class="box">
                            <div class="cxxontent box-shadow" style="background: #ffffff">
                                <div class="screen-top" style="position: relative;">
                                    <div>
                                        <span>项目名称<input id="ProjName" type="text" style="width: 350px" /></span>
                                        <%if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
                                          { %>
                                        <span>申报部门<input id="DeptName" type="text" style="width: 350px" /></span>
                                        <%} %>
                                        <a href="#" id="submit-btn" class="submit-btn">搜索</a>
                                    </div>
                                </div>
                                <ul id="selectList">
                                    <li style="padding-top: 1em">
                                        <span style="display: inline-block; text-align: right; width: 90px; font-weight: bolder">项目类型</span>
                                        <span class="cxcontentmain" titles='ProType' value='软件平台'>软件平台</span>
                                        <span class="cxcontentmain" titles='ProType' value='硬件平台'>硬件平台</span>
                                        <span class="cxcontentmain" titles='ProType' value='服务运维'>服务运维</span>
                                    </li>
                                    <li>
                                        <span style="display: inline-block; text-align: right; width: 90px; font-weight: bolder">合同资金范围</span>
                                        <span class="cxcontentmain" titles='lxje' valuemin='0' valuemax='100'>100万以内</span>
                                        <span class="cxcontentmain" titles='lxje' valuemin='100' valuemax='200'>100万到200万</span>
                                        <span class="cxcontentmain" titles='lxje' valuemin='200' valuemax='500'>200万到500万</span>
                                        <span class="cxcontentmain" titles='lxje' valuemin='500'>500万以上</span>
                                    </li>
                                    <li>
                                        <span style="display: inline-block; text-align: right; width: 90px; font-weight: bolder">招标方式</span>
                                        <span class="cxcontentmain" titles='zb' value='公开招标'>公开招标</span>
                                        <span class="cxcontentmain" titles='zb' value='邀请招标'>邀请招标</span>
                                        <span class="cxcontentmain" titles='zb' value='询价'>询价</span>
                                        <span class="cxcontentmain" titles='zb' value='单一来源'>单一来源</span>
                                        <span class="cxcontentmain" titles='zb' value='自行采购'>自行采购</span>
                                        <span class="cxcontentmain" titles='zb' value='竞争性谈判'>竞争性谈判</span>
                                        <span class="cxcontentmain" titles='zb' value='竞争性磋商'>竞争性磋商</span>


                                    </li>
                                    <li>
                                        <span style="display: inline-block; text-align: right; width: 90px; font-weight: bolder">项目进度</span>
                                        <span class="cxcontentmain" titles='ProState' value='申报'>申报</span>
                                        <span class="cxcontentmain" titles='ProState' value='立项'>立项</span>
                                        <span class="cxcontentmain" titles='ProState' value='建设'>建设</span>
                                        <span class="cxcontentmain" titles='ProState' value='验收'>验收</span>
                                        <span class="cxcontentmain" titles='ProState' value='运维'>运维</span>
                                    </li>

                                    <li>
                                        <span style="display: inline-block; text-align: right; width: 90px; font-weight: bolder">项目申报年份</span>
                                        <span class="cxcontentmain" titles='sbYear' value='2014'>2014</span>
                                        <span class="cxcontentmain" titles='sbYear' value='2015'>2015</span>
                                        <span class="cxcontentmain" titles='sbYear' value='2016'>2016</span>
                                        <span class="cxcontentmain" titles='sbYear' value='2017'>2017</span>
                                        <span class="cxcontentmain" titles='sbYear' value='2018'>2018</span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="m_div box-shadow" style="margin-top: 10px; padding: 10px; min-height: 70px">
                            <div class="t_div">
                                项目信息
                                <br />
                                <ul style="float: right; list-style: none; font-size: 12px" id="fy">
                                    <li style="width: auto; float: left; border-right: 1px solid #ddd; padding: 0 30px 0 30px; color: #999; text-align: center; font-size: 14px">共有项目
                                        <p style="font-size: 18px; color: #333; font-weight: bold;" id="pc">个</p>
                                    </li>

                                    <li style="width: auto; float: left; border-right: 1px solid #ddd; padding: 0 30px 0 30px; color: #999; text-align: center; font-size: 14px">预估总额
                                        <p style="font-size: 18px; color: #333; font-weight: bold;" id="yg">万元</p>
                                    </li>

                                    <li style="width: auto; float: left; border-right: 1px solid #ddd; padding: 0 30px 0 30px; color: #999; text-align: center; font-size: 14px">立项总额
                                        <p style="font-size: 18px; color: #333; font-weight: bold;" id="lx">万元</p>
                                    </li>
                                    <li style="width: auto; float: left; border-right: 1px solid #ddd; padding: 0 30px 0 30px; color: #999; text-align: center; font-size: 14px">支付总额
                                        <p style="font-size: 18px; color: #333; font-weight: bold;" id="zf">万元</p>
                                    </li>
                                    <li style='width: auto; float: right;' id='page'>
                                        <div class='npbtn' style="padding: 0 0 0 20px">
                                            <div class='prev active'><a href='javascript:void(0)' onclick='upPage()'><span class='cap'></span><span class='arrow'></span><span class='title'>上一页</span></a></div>
                                            <div class='next active'><a href='javascript:void(0)' onclick='nextPage()'><span class='cap'></span><span class='arrow'></span><span class='title'>下一页</span></a></div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="list box-shadow" id="ProjList">
                            <ul id="ProjListUl">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <a id="fileexportlink" href="#" style="display: none"><span>下载</span></a>
</body>
</html>

<script>

    var top1 = $(".words12_zzgz_xfgz6_title").height() + $(".box").height();
    var width = $(".m_div").width();
    $(window).scroll(function () {
        var win_top = $(this).scrollTop();

        var top = $(".m_div").offset().top;

        if (win_top >= top) {

            $(".m_div").addClass("sfixed").css("width", width);

        }
        if (win_top < top1) {
            $(".m_div").removeClass("sfixed");
        }
    })

    var Page = 1;
    var AllPage = 0;
    $(function () {


        <%if (roleCheck.isBm())
          {%>
        GetProjData(Page, "and StartDeptGuid='<%=CurrentUser.UserGroup.Guid%>'");
        <%}
          else
          {%>
        GetProjData(Page, "");
        <%}%>   
        

        $('#file1').change(function(){
            
            var filename= $("#file1").val().substring( $("#file1").val().lastIndexOf('\\')+1);
            $('#span_filename').text(filename);
        });

        $(".active a").each(function () {
            $(this).hover(function () {
                $(this).css("cursor", "pointer");
                $(this).stop();
                $(this).animate({ width: 90 }, 400, function () {
                    $(this).children(".title").css("display", "block");
                })
            }, function () {
                $(this).stop();
                $(this).children(".title").css("display", "none");
                $(this).animate({ width: 20 }, 400);
            })
        });
        <%if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
          {%>
        var viewheight=document.body.clientHeight+50;
        document.getElementById("divmenu").style.height= viewheight +"px";
        initTree();

        initChart();
        <%}
          else
          {%>
        initTable();
        $("#form1").removeClass("adminform");
        <%}%>


        $("#title_search").click(function(){
            $("#title_file").removeClass("titlefont");
            $("#title_search").addClass("titlefont");
            $("#div_file").hide();
            $("#div_search").show();
        });

        $("#title_file").click(function(){
            $("#title_search").removeClass("titlefont");
            $("#title_file").addClass("titlefont");

            $("#div_file").show();
            $("#div_search").hide();
        });

        $("#a_file").click(function(){

            $.ajax({
                type:'post',
                url:'SearchHandler.ashx',
                data:{action:'file',roletype:'<%=roleType%>',bmguid:'<%=CurrentUser.UserGroup.Guid%>',bmname:'<%=CurrentUser.UserGroup.Name%>'},
                success:function(e)
                {
                    
                    $("#fileexportlink").attr("href", "<%=ConfigurationManager.AppSettings["flashurl"]%>/<%=CurrentUser.UserGroup.Name%>"+"_2016—2018年泰州市电子政务项目资金安排清单.xls");
                    
                    $('#fileexportlink span').trigger('click');
                }
            });
        });





        $("#upload").click(function () {
           
            if( $('#span_filename').text()=="")
            {
                alert("请选择要上传的文件！");
                return;
            }

            layui.use('layer', function(){ //独立版的layer无需执行这一句
                var layer = layui.layer; //独立版的layer无需执行这一句
                var index = layer.load(1, {
                    shade: [0.1,'#fff'] //0.1透明度的白色背景
                });
            });

            var option = {
                url : 'upload.ashx',
                type : 'POST',
                dataType : 'json',
                headers : {"ClientCallMode" : "ajax"}, //添加请求头部
                success : function(data) {
                    if (data == "0") {
                        //alert("上传成功！");
                        layui.use('layer', function(){
                            var layer = layui.layer; //独立版的layer无需执行这一句
                            layer.closeAll();
                            alert("上传成功");
                            //layer.msg('上传成功');

                        <%if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
                              {%>
                            
                            <%}
                              else
                              {%>
                        initTable();
                            <%}%>
                    });
                }
            },
                 error: function(data) {
                     //alert(JSON.stringify(data));
                     layer.msg('上传失败');
                 }
             };
            $("#form1").ajaxSubmit(option);
            return false;

            
        });
    });



    function initTable()
    {
        $("#mantable tbody").html("");

        $.ajax({
            type:'post',
            url:'SearchHandler.ashx',
            data:{action:'table',bmguid:'<%=CurrentUser.UserGroup.Guid%>'},
            success:function(e)
            {
                if(e=="")
                {
                    $("#mantable tbody").html(' <tr><td rowspan="5">暂无项目信息</td> ');
                }
                else
                {
                    $("#mantable tbody").html(e);
                }
                
            }
        });
    }

    function initChart()
    {

    }

    var isParent="0";

    function zTreeBeforeClick(treeId, treeNode, clickFlag) {

        isParent=treeNode.isparent;
        //alert(treeNode.isparent);
        bmguid=treeNode.guid;
        GetCondition();
        return true;
    };

    var bmguid="";
            <%if (roleCheck.isAdmin() || roleCheck.isSjj() || roleCheck.isZfb())
              {%>
    function initTree() {
        var zTreeObj;
        // zTree 的参数配置，深入使用请参考 API 文档（setting 配置详解）
        var setting = {
            callback: {
                beforeClick: zTreeBeforeClick
            }
        };
        var zNodes=<%=zNodes%>;

        zTreeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes );
    }
            <%}%>

    function GetProjData(Page, where) {
        
        $.ajax({
            type: "POST",
            url: "../Handlers/GridDataHandler.ashx",
            dataType: "json",
            async: false,
            data: { idField: "ProGuid", sort: "StartDate", order: "desc", tableName: "V_TZ_ProjectOverview", condition: "", rows: 6, page: Page, where: where },
            success: function (data) {
                if (data.rows.length > 0) {
                    var html = "";
                    for (var i = 0; i < data.rows.length; i++) {
                        html += "<li>";
                        html += "<a  href='/Project/ProgressManager/ProjectProgressDetail.aspx?xmguid="+data.rows[i]["ProGuid"]+"' target='_self'>";
                        html += "<div class='proj_t_div'>" + data.rows[i]["ProName"] + "</div>";
                        html += "<span style='width: 175px;' class='span'> <span style='color: #ff6600;'>申报部门：</span><span class='c_span'>" + data.rows[i]["StartDeptName"] + "</span></span>";
                        
                        var xmjd="";
                        if (data.rows[i]["ProState"] == "提交" || data.rows[i]["ProState"] == "退回") {
                            xmjd = "申报";
                        }
                        else if (data.rows[i]["ProState"] == "申报" && data.rows[i]["jscount"] == "0" && data.rows[i]["yscount"] == "0" && data.rows[i]["ywcount"] == "0") {
                            xmjd = "立项";
                        }
                        else if (data.rows[i]["ProState"] == "申报" && data.rows[i]["jscount"] != "0" && data.rows[i]["yscount"] == "0" && data.rows[i]["ywcount"] == "0") {
                            xmjd = "建设";
                        }
                        else if (data.rows[i]["ProState"] == "申报" &&  data.rows[i]["yscount"] != "0" && data.rows[i]["ywcount"] == "0") {
                            xmjd = "验收";
                        }
                        else if (data.rows[i]["ProState"] == "申报" &&  data.rows[i]["ywcount"] != "0") {
                            xmjd = "运维";
                        }
                        html += "<span style='width: 120px;' class='span t_span_right'> <span style='color: #ff6600;'>项目进度：</span><span class='c_span'>" + xmjd + "</span></span>";
                        html += "<span style='width: 200px;' class='span t_span_right'> <span style='color: #ff6600;'>项目类型：</span><span class='c_span'>" + data.rows[i]["ProType"] + "</span></span>";
                        html += "<span style='width: 120px;' class='span t_span_right'> <span style='color: #ff6600;'>申报日期：</span><span class='c_span'>" + data.rows[i]["StartDate"].split(' ')[0] + "</span></span>";
                        html += "<span style='width: 170px;' class='span t_span_right'> <span style='color: #ff6600;'>预估金额：</span><span class='c_span'>" + data.rows[i]["Quota"] + "万元</span></span><br/>";
                        if (data.rows[i]["lxsj"] == "") {
                            html += "<span style='width: 175px;' class='span'> <span style='color: #ff6600;'>立项时间：</span><span class='c_span'>暂未立项</span></span>";
                            //html += "<span style='width: 135px;' class='span t_span_right'> <span style='color: #ff6600;'>立项金额：</span><span class='c_span'>0万元</span></span>";
                        } else {
                            html += "<span style='width: 175px;' class='span'> <span style='color: #ff6600;'>立项时间：</span><span class='c_span'>" + data.rows[i]["lxsj"] + "</span></span>";
                            //html += "<span style='width: 135px;' class='span t_span_right'> <span style='color: #ff6600;'>立项金额：</span><span class='c_span'>" + data.rows[i]["lxje"] + "万元</span></span>";
                        } 
                        html += "<span style='width: 120px;' class='span t_span_right'> <span style='color: #ff6600;'>合同金额：</span><span class='c_span'>" + data.rows[i]["htje"] + "万元</span></span>";
                        var zbtype = "";
                        if (data.rows[i]["gkzbcount"] > 0) {
                            zbtype += "公开招标，";
                        }
                        if (data.rows[i]["yqzbcount"] > 0) {
                            zbtype += "公开招标，";
                        }
                        if (data.rows[i]["xjcount"] > 0) {
                            zbtype += "询价，";
                        }
                        if (data.rows[i]["dylycount"] > 0) {
                            zbtype += "单一来源，";
                        }
                        if (data.rows[i]["zjcgcount"] > 0) {
                            zbtype += "自行采购，";
                        }
                        if (data.rows[i]["jzxtpcount"] > 0) {
                            zbtype += "竞争性谈判，";
                        }
                        if (data.rows[i]["jzxcscount"] > 0) {
                            zbtype += "竞争性磋商，";
                        } 
                        if (zbtype == "") {
                            html += "<span style='width: 344px;' class='span t_span_right'> <span style='color: #ff6600;'>招标方式：</span><span class='c_span'>暂未招标</span></span>";
                        } else {
                            html += "<span style='width: 344px;' class='span t_span_right'> <span style='color: #ff6600;'>招标方式：</span><span class='c_span'>" + zbtype.substring(0, zbtype.length - 1) + "</span></span>";
                        } 
                        if (data.rows[i]["yssj"] == "") {
                            html += "<span style='width: 170px;' class='span t_span_right'> <span style='color: #ff6600;'>验收日期：</span><span class='c_span'>暂未验收</span></span><br/>";
                        } else {
                            html += "<span style='width: 170px;' class='span t_span_right'> <span style='color: #ff6600;'>验收日期：</span><span class='c_span'>" + data.rows[i]["yssj"].split(' ')[0] + "</span></span><br/>";
                        } 
                        html += "</a></li>";
                    } 
                    if (Page == 1) {
                        $("#ProjListUl").html(html);
                    } else {
                        $("#ProjListUl").html(html);
                    }
                    $.ajax({
                        type: "POST",
                        url: "Index.aspx",
                        dataType: "text",
                        async: false,
                        data: { type: "post", con: where },
                        success: function (data) {
                            var list = data.split(',');
                            if (list.length > 1) {
                                $("#pc").html(list[0] + "个");
                                $("#lx").html(list[1] + "万元");
                                $("#zf").html(list[2] + "万元");
                                $("#yg").html(list[3] + "万元");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                        }
                    });
                }
                else {
                    $("#ProjListUl").html("<li style='font-size: 14px;font-weight:bolder;text-align:center'><span>暂无项目信息<span></li>");
                    $("#pc").html("0个");
                    $("#lx").html("0万元");
                    $("#zf").html("0万元");
                    $("#yg").html("0万元");
                }
                AllPage = data.total % 6 == 0 ? data.total / 6 : (parseInt(data.total / 6 + 1));
                if (AllPage > 1) {
                    $("#page").show();
                } else {
                    $("#page").hide();
                }
            }
        });
    }

    //上一页
    function upPage() {
        if (Page > 1) {
            --Page;
            GetCondition()
        }
    }
    //下一页
    function nextPage() {
        if (Page < AllPage) {
            ++Page;
            GetCondition()
        }
    }

    //查新条件选择
    $('.cxcontentmain').live('click', function () {
        $(this).addClass('cxcontentspan').removeClass('cxcontentmain').append('<span class="xSpan"></span>');
        GetCondition();
    });
    $('.cxcontentspan').live('click', function () {
        $(this).addClass('cxcontentmain').removeClass('cxcontentspan').children('span').remove();
        GetCondition();
    });

    function GetCondition() {
        var Condition = '';


        Condition += GetLxzjCondition("lxje", "htje");
        //项目名称
        if ($("#ProjName").val()!= "") {
            Condition += " and ProName like '%" + $("#ProjName").val() + "%'";
        }
        <%if (!roleCheck.isBm())
          {%>
        //申报部门
        if ($("#DeptName").val() != "" ) {
            Condition += " and StartDeptName like '%" + $("#DeptName").val() + "%'";
        }
        <%}%>



        Condition += GetConditionStr("ProType"); 
        Condition += GetConditionStr("ProState");
        Condition += GetConditionStr("zb");
        Condition += GetConditionStr("sbYear");
        
        if(isParent=="0")
        {
            if(bmguid!="-1"&&bmguid!="")
            {
                Condition +=" and StartDeptGuid='"+bmguid+"'";
            }
        }
            //父节点
        else
        {
            if(bmguid!="-1"&&bmguid!="")
            {
                Condition +=" and StartDeptGuid in (select guid from Sys_UserGroups where topguid='"+bmguid+"') ";
            }
        }


        <%if (roleCheck.isBm())
          {%>
        GetProjData(Page, Condition+" and StartDeptGuid='<%=CurrentUser.UserGroup.Guid%>'");
        <%}
          else
          {%>
        GetProjData(Page, Condition);
        <%}%>


        
    }

    $("#submit-btn").click(function () {
        GetCondition();
    })

    function GetConditionStr(ifield) {
        var str = '';
        var tmpArray = $('.cxcontentspan[titles="' + ifield + '"]');
        //判断是否是招标查询
        if (ifield == "zb") {
            if (tmpArray.length == 0)
            {; }
            else {
                str += " and  ("
                for (var i = 0; i < tmpArray.length; i++) {
                    if (tmpArray.eq(i).attr('value') == "公开招标") {
                        str += " gkzbcount>0 ";
                    }
                    if (tmpArray.eq(i).attr('value') == "单一来源") {
                        str += " dylycount>0 ";
                    }
                    if (tmpArray.eq(i).attr('value') == "询价") {
                        str += " xjcount>0 ";
                    }
                    if (tmpArray.eq(i).attr('value') == "自行采购") {
                        str += " zjcgcount>0 ";
                    }
                    if (tmpArray.eq(i).attr('value') == "竞争性磋商") {
                        str += " jzxcscount>0 ";
                    }
                    if (tmpArray.eq(i).attr('value') == "竞争性谈判") {
                        str += " jzxtpcount>0 ";
                    }
                    if (tmpArray.eq(i).attr('value') == "邀请招标") {
                        str += " yqzbcount>0 ";
                    }
                    //str += ifield + "  = '" + tmpArray.eq(i).attr('value') + "'"
                    if (i < tmpArray.length - 1) {
                        str += " or ";
                    }
                }
                str += " )"
            }
        }
        else if (ifield == "ProType") {
            if (tmpArray.length == 0)
            {; }
            else {
                str += " and  ("
                for (var i = 0; i < tmpArray.length; i++) {
                    str += " CHARINDEX('" + tmpArray.eq(i).attr('value') + "',ProType)>0 ";
                    //str += ifield + "  = '" + tmpArray.eq(i).attr('value') + "'"
                    if (i < tmpArray.length - 1) {
                        str += " or ";
                    }
                }
                str += " )"
            }
        }
        else if (ifield == "ProState") {
            if (tmpArray.length == 0)
            {; }
            else {
                str += " and  ("
                for (var i = 0; i < tmpArray.length; i++) {
                    if (tmpArray.eq(i).attr('value') == "申报") {
                        str += " (ProState='提交' or ProState='退回') ";
                    }
                    if (tmpArray.eq(i).attr('value') == "立项") {
                        str+=" (ProState='申报' and jscount<1 and yscount<1 and ywcount<1) "
                    }
                    if (tmpArray.eq(i).attr('value') == "建设") {
                        str+="(ProState='申报' and  jscount>0 and yscount<1 and ywcount<1)"
                    }
                    if (tmpArray.eq(i).attr('value') == "验收") {
                        str += " (ProState='申报' and  yscount>0 and ywcount<1) ";
                    }
                    if (tmpArray.eq(i).attr('value') == "运维") {
                        str += " (ProState='申报' and  ywcount>0) ";
                    }
                    //str += ifield + "  = '" + tmpArray.eq(i).attr('value') + "'"
                    if (i < tmpArray.length - 1) {
                        str += " or ";
                    }
                }
                str += " )"
            }
        }
        else if (ifield == "sbYear") {
            if (tmpArray.length == 0)
            {; }
            else
            {
                str += " and (";
                for (var i = 0; i < tmpArray.length; i++) {
                    str += " year(StartDate)=" + tmpArray.eq(i).attr('value') + " ";
                    if (i < tmpArray.length - 1) {
                        str += " or ";
                    }
                }
                str += " )";
            }

        }
        else {
            if (tmpArray.length == 0)
            {; }
            else {
                str += " and  ("
                for (var i = 0; i < tmpArray.length; i++) {
                    str += ifield + "  = '" + tmpArray.eq(i).attr('value') + "'"
                    if (i < tmpArray.length - 1) {
                        str += " or ";
                    }
                }
                str += " )"
            }
        }
        return str;
    }

    //立项资金范围条件
    function GetLxzjCondition(iField, sField) {
        var str = '';
        var tmpArray = $(".cxcontentspan[titles='" + iField + "']");
        if (tmpArray.length == 0) {
            ;
        }
        else {
            str += " and  ("
            for (var i = 0; i < tmpArray.length; i++) {
                if (tmpArray.eq(i).attr('valueMax') == undefined) {
                    str += "(" + sField + " >= " + tmpArray.eq(i).attr('valueMin') + "" + ")";
                }

                else {
                    str += "(" + sField + " >= " + tmpArray.eq(i).attr('valueMin') + " and " + sField + " <" + tmpArray.eq(i).attr('valueMax') + "" + ")";
                }

                if (i < tmpArray.length - 1) {
                    str += " or ";
                }
            }
            str += " )"
        }
        return str;
    }

</script>
