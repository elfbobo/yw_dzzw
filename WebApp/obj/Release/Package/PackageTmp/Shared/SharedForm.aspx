<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SharedForm.aspx.cs" Inherits="Yawei.App.Shared.SharedForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10"/>
    <title>泰州市电子政务项目管理系统</title>
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <link href="../Content/Layout.css" rel="stylesheet" />
    <script src="../Plugins/jquery-ztree/ztree-all.min.js" type="text/javascript"></script>
    <link href="../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
    <link href="../Content/Form.css" rel="stylesheet" />
    <style type="text/css">
        .asshow {
            cursor: pointer;
            min-height: 210px;
            min-width: 380px;
            padding-top: 10px;
        }

        .adtitle {
            cursor: pointer;
            display: block;
            width: 84%;
            text-align: center;
            font-size: 18px;
            padding: 0px 8%;
        }

        .adcontent {
            cursor: pointer;
            display: block;
            width: 90%;
            margin-top: 10px;
            font-size: 15px;
            padding: 0px 5%;
        }
    </style>
</head>
<body style="min-width: 1010px; overflow-y: hidden">
    <form id="form1" runat="server">
        <div class="header">
            <div class="header-left">
                <img src="../Images/01.png" alt="" />
            </div>
            <div class="header-right" style="background: #82AF6F">
                <div class="header-right-user" style="width: 240px">
                    <span>欢迎，<%=userCn %>|
                        <a onclick="Exit()" href="#" style="cursor: pointer; text-align: right; color: #ffffff; text-decoration: underline;">注销</a>
                    </span>
                </div>
                <div class="header-right-exit">
                </div>
            </div>
        </div>

        <div class="nav">
            <%--       <div class="nav-left">
                <div class="nav-left-t">
                    <img src="../images/00.png" width="58px" alt="头像" style="border: 1px solid lightgray; border-radius: 5px; box-shadow: 0px 0px 5px #535658;" />
                </div>
                <div class="nav-left-m">
                    <div class="nav-left-m-1"><%=CurrentUser.UserCN %></div>
                    <div class="nav-left-m-2">
                        <img src="../images/shared/tongyong.png" style="vertical-align: text-bottom" alt="" />
                        <%=CurrentUser.UserRole[0].Name %>
                    </div>
                </div>

            </div>--%>
            <div class="nav-right" id="headmenu">
                <div class="nav-left-g"></div>

            </div>
        </div>

        <div class="content">
            <div class="leftsidebar" style="vertical-align: top; display: none">
                <div class="leftsidebar-top" style="line-height: 30px; color: #2772ac; font-size: 14px; font-weight: bold; position: relative">
                    <img src="../images/shared/xtcd.png" style="height: 20px; width: 25px; vertical-align: text-bottom; margin-left: 15px" />
                    系统菜单
            <span style="cursor: pointer; display: inline-block; font-size: 10px; right: 5px; top: 7px; position: absolute" onclick="menuClosed()"></span>
                </div>

                <div class="leftsidebar-bottom" id="leftmenu" name="" >
                </div>
            </div>



            <div class="right-content" >
                <iframe id="iframeContent" name="Content" style="width: 100%; height: 100%; display: block; overflow-x: hidden;" frameborder="0" src="../Project/tz_ProjOverview/Index.aspx"></iframe>
            </div>
        </div>

        <div class="dialog" id="ldialog" style="height: 240px; width: 380px; position: fixed; display: none;">
            <div class="head" style="height: 20px; padding-bottom: 5px;"><span class="title" style="color: red; height: 15px; font-size: 14px;">项目催报</span><span style="margin-right: 30px; cursor: pointer; font-size: 12px;" onclick="indexremind(this)">标记已读</span></div>
            <img src="../Images/cross.png" style="position: absolute; right: 3px; top: 1px; cursor: pointer" onclick="$('#ldialog').slideUp();" />
            <div id="content" class="adshow" onclick="jumppage(this)">
            </div>
        </div>

    </form>
</body>
</html>
<script type="text/javascript">

    var html = '<%=html%>';
    var isShow = "<%=isshow%>";
    var remindGuid = "<%=RemindGuid%>";
    var menuJson = [<%=menuJson%>];
    var $winh = $(window).height();
    $(function () {
        SetHeadMenu();
        $(".right-content,.content").height($winh - 50 - 72);
        $("#iframeContent").height($(".right-content").height());
        // $("#ldialog").slideDown("slow");
        $("#ldialog").offset({ top: $(window).height() - 242, left: $(window).width() - 382 });//$(window).height() - 242
        if (isShow == "False") {
            $("#ldialog").hide();
        }
        else {
            if (remindGuid != "")
                $("#content").attr("guid", remindGuid);
            $("#content").append(html);
        }



    });

    function SetHeadMenu() {
        var html = '';
        $.each(menuJson, function (i, dat) {
            if (i = 0) {
                html += '<div onclick=\'tab("' + dat.name + '",this)\'  class="nav-right-1"><div class="nav-right-1-top"><img src="' + dat.icon + '" width="40" height="34px" alt="" /></div> <div  class="nav-right-1-size">' + dat.name + '</div></div>';
                html += '<div class="nav-right-d"><img src="../images/shared/img-13.jpg" width="1px" height="68px" alt="虚线" /></div>';
            }
            else {
                html += '<div url="' + dat.href + '" onclick=\'tab("' + dat.name + '",this)\' class="nav-right-2"><div class="nav-right-2-top"><img src="' + dat.icon + '" width="40" height="34px" alt="" /></div> <div  class="nav-right-2-size">' + dat.name + '</div></div>';
                html += '<div class="nav-right-d"><img src="../images/shared/img-13.jpg" width="1px" height="68px" alt="虚线" /></div>';
            }
        });
        $("#headmenu").append(html);
    }

    function tab() {

        $(".nav-right-1").removeClass("nav-right-1").addClass("nav-right-2");
        $(".nav-right-1").find(".nav-right-1-top").removeClass("nav-right-1-top").addClass("nav-right-2-top");
        $(".nav-right-1").find(".nav-right-1-size").removeClass("nav-right-1-size").addClass("nav-right-2-size");

        $(arguments[1]).addClass("nav-right-1").removeClass("nav-right-2");
        $(arguments[1]).find(".nav-right-2-top").removeClass("nav-right-2-top").addClass("nav-right-1-top");
        $(arguments[1]).find(".nav-right-2-size").removeClass("nav-right-2-size").addClass("nav-right-1-size");
        var name = $('#leftmenu').attr("name");
        if (arguments[0] == "项目管理" || arguments[0] == "项目申报" || arguments[0] == "专家登录" || arguments[0] == "专家管理" || arguments[0] == "项目概况" || arguments[0] == "首页" || arguments[0] == "文档管理" || arguments[0] == "年度自评" || arguments[0] == "资料发布" || arguments[0] == "项目进度" || arguments[0] == "综合查询" || arguments[0] == "项目建设" || arguments[0] == "项目验收" || arguments[0] == "项目运维" || arguments[0] == "资金管理" || arguments[0] == "用户管理") {

           
            //document.getElementById("iframeContent").src = "http://localhost:6225/FundManage/FundList.aspx";
            //alert(document.getElementById("iframeContent").src);
            //window.frames['Content'].location = $(arguments[1]).attr("url");
            //if (arguments[0] == "资金管理") {
            //    setTimeout(function (value1) {
            //        document.getElementById("iframeContent").src = value1;
            //    }, 2000, $(arguments[1]).attr("url"));

            //}
            //else {
            //    document.getElementById("iframeContent").src = $(arguments[1]).attr("url");
            //}
            
            if (arguments[0] != "资金管理" && arguments[0] != "用户管理") {
                $(".leftsidebar").hide();
            }
            else {
                $(".leftsidebar").show();
                $(".leftsidebar").css("display","table-cell");
                if (name != arguments[0]) {
                    leftmenu(arguments[0]);
                    $('#leftmenu').attr("name", arguments[0]);
                }
            }
            //if (arguments[0] == "资金管理") {
            //    $(".leftsidebar").hide();
            //    document.getElementById("iframeContent").src = "/FundManage/index.aspx";
            //}
            //else {
               document.getElementById("iframeContent").src = $(arguments[1]).attr("url");
            //}
        }
        else {
            $(".leftsidebar").show();
            if (name != arguments[0]) {
                leftmenu(arguments[0]);
                $('#leftmenu').attr("name", arguments[0]);
            }
        }
    }

    var setting = {
        callback: { onClick: zTreeonClick }

    };

    function zTreeonClick(event, treeId, treeNode) {
        if (treeNode.name == '系统设置') {
            window.open(treeNode.href);
        }
        else
            document.getElementById("iframeContent").src = treeNode.href;

    };
    var newurl;
    function leftmenu(name) {
        $("#leftmenu").children().remove();
        $.each(menuJson, function (i, dat) {
            //console.log(name);
            //console.log(dat.name);
            if (dat.name == name) {
                if (dat.name == "项目申报") {
                    //console.log(dat);
                }
                if (name == "行业监管" || name == "综合运维" || name == "统计分析" || name == "行政监督"||name=="用户管理"||name=="用户中心") {
                    $("#leftmenu").append('<ul class="ztree" style="width:97%; height:100%; overflow:auto;"></ul>');
                    zTreeObj = $.fn.zTree.init($(".ztree"), setting, dat.children);
                    var nodes = zTreeObj.transformToArray(zTreeObj.getNodes());
                    for (var j = 0; j < nodes.length; j++) {
                        if (j == 0) {
                            document.getElementById("iframeContent").src = nodes[j].href;
                        }
                        nodes[j].icon = '../images/note.png';

                        zTreeObj.updateNode(nodes[j]);
                    }


                    zTreeObj.expandAll(true);
                }
                else {
                    var html = "", crurl = '';
                    if (!dat.children)
                        return;
                    $.each(dat.children, function (j, dt) {
                        if (dt.href != '' && crurl == '') {
                            crurl = dt.href;
                            if (newurl == '')
                                newurl = dt.href;
                        }

                        html += '<div class="leftsidebar-content"> <div class="leftsidebar-content-img"><img src="' + dt.icon + '" width="25" height="20px" alt="" /></div>';
                        html += '<div  url="' + (dt.href == '' ? '' : dt.href) + '"  onclick="acc(this)" style="cursor:pointer"  class="leftsidebar-content-size">' + dt.name + '</div><div class="leftsidebar-content-d"><img src="../images/shared/img-19.jpg"  alt="" /></div></div>';

                        if (dt.children) {
                            html += "<div le='2'  style='display:none'>";
                            $.each(dt.children, function (m, d) {
                                if (d.url != '' && crurl == '') {
                                    crurl = d.href;
                                }
                                html += '<div class="leftsidebar-content2"> <div class="leftsidebar-content-img2"><img src="../images/shared/img-23.gif" width="7" height="8px" alt="" /></div>';
                                html += '<div class="leftsidebar-content-size2"><a href="' + d.href + '" target="Content">' + d.name + '</a></div></div>';
                            });
                            html += "</div>";
                        }
                    });
                    $("#leftmenu").append(html);

                    $("#leftmenu").find(".leftsidebar-content-size").eq(0).click();
                    // $("#iframeContent").attr("src", crurl);

                    document.getElementById("iframeContent").src = crurl;

                }
                return false;
            }
        });
    }

    function acc(obj) {
        if ($(obj).attr("url") != "") {
            $("#iframeContent").attr("src", $(obj).attr("url"));
        }
        var div = $(obj).parent().next();
        var crdiv = $(obj).parent();
        if (crdiv.attr("class") == "leftsidebar-content1")
            return false;
        $(".leftsidebar-content1").addClass("leftsidebar-content").removeClass("leftsidebar-content1").find("img");
        $(".leftsidebar-content-img1").removeClass("leftsidebar-content-img1").addClass("leftsidebar-content-img");
        $(".leftsidebar-content-size1").removeClass("leftsidebar-content-size1").addClass("leftsidebar-content-size");
        $(".leftsidebar-content-d1").removeClass("leftsidebar-content-d1").addClass("leftsidebar-content-d").find("img").attr("src", "../images/shared/img-19.jpg");

        crdiv.removeClass("leftsidebar-content").addClass("leftsidebar-content1");
        crdiv.find(".leftsidebar-content-img").removeClass("leftsidebar-content-img").addClass("leftsidebar-content-img1");
        crdiv.find(".leftsidebar-content-size").removeClass("leftsidebar-content-size").addClass("leftsidebar-content-size1");
        crdiv.find(".leftsidebar-content-d").removeClass("leftsidebar-content-d").addClass("leftsidebar-content-d1").find("img").attr("src", "../images/shared/img-26.gif");
        if (div.attr("le")) {
            $("div[le]").slideUp("slow");
            div.slideDown("slow");
        }
        else {
            $("div[le]").slideUp("slow");
        }

    }

    function menuClosed() {
        $(".leftsidebar-content1").addClass("leftsidebar-content").removeClass("leftsidebar-content1").find("img");
        $(".leftsidebar-content-img1").removeClass("leftsidebar-content-img1").addClass("leftsidebar-content-img");
        $(".leftsidebar-content-size1").removeClass("leftsidebar-content-size1").addClass("leftsidebar-content-size");
        $(".leftsidebar-content-d1").removeClass("leftsidebar-content-d1").addClass("leftsidebar-content-d").find("img").attr("src", "../images/shared/img-19.jpg");
        $("div[le]").slideUp("slow");
    }

    function indexremind(eve) {
        if ($(eve).parents("#ldialog").find("#content").attr("guid")) {
            var guid = $(eve).parents("#ldialog").find("#content").attr("guid");
            $.post("SharedForm.aspx", { type: "post", guid: guid }, function (data) {
                $('#ldialog').slideUp();
            })
        }
    }

    function jumppage(eve) {
        var guid = $(eve).attr("guid");
        if (guid != "") {
            $.post("SharedForm.aspx", { type: "jumppost", guid: guid }, function (data) {
                var result = data;
                if (result > 0) {
                    $('#ldialog').slideUp();
                    window.open("../Project/WorkRemind/View.aspx?Guid=" + guid);
                }
            })
        }
    }

    var userType = "<%=userType %>"

    function Exit() {
        if (userType == "ad") {
        }
        else {
            window.location = '../Support/Login/Default.aspx';
        }
    }


    <%-- $(function () {
        $("#headmenu").append($('<a target="_blank" id="lssj"><div class="nav-right-2"><div class="nav-right-2-top"><img width="40" height="34" alt="" src="../images/shared/img-32.png"/></div><div class="nav-right-2-size">历史数据</div></div></a><div class="nav-right-d"><img width="1" height="68" alt="虚线" src="../images/shared/img-13.jpg"></div>'));
        switch ('<%=ConfigurationManager.AppSettings["SystemUser"].ToUpper()%>') {
            case "AD": $("#lssj").attr("href", "http://35.1.11.37:8612");
                break;
            case "DB": $("#lssj").attr("href", "http://202.110.193.79/zftzxmLs");
                break;
            default: alert("不可访问");
                break;
        }
    })--%>
</script>





