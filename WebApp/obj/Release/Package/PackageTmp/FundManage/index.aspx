<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApp.FundManage.index" %>

<!DOCTYPE html>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,Chrome=1" />
<html style="position: relative" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>资金使用情况</title>
    <script src="../Plugins/jquery-1.8.3.min.js" type="text/javascript"></script>
    <link href="../Content/Form.css" rel="stylesheet" type="text/css" />
       <link href="../Content/Layout.css" rel="stylesheet" />
    <script src="../Plugins/datepicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../Plugins/datepicker/skin/WdatePicker.css" rel="stylesheet" />
    <script src="../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
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

        /*左导航开始 */
.leftsidebar1 {
    float:left;
    width: 192px;
    border-right: 1px solid #cccccc;
    border-bottom: 1px solid #cccccc;
    height: 100%;
    background-color: #f2f2f2;
}
/*右边主体开始*/
.right-content1 {
    width: auto;
}
    </style>
</head>

<body>

    <div class="content1"  style="min-height:548px;width: 100%;display: table;height: 548px;">
        <div class="leftsidebar1" style="vertical-align: top; height:100%">
            <div style="POSITION: relative; LINE-HEIGHT: 30px; COLOR: #2772ac; FONT-SIZE: 14px; FONT-WEIGHT: bold" class="leftsidebar-top">
                <img style="WIDTH: 25px; HEIGHT: 20px; MARGIN-LEFT: 15px; VERTICAL-ALIGN: text-bottom" src="../images/shared/xtcd.png">
                系统菜单 <span style="POSITION: absolute; DISPLAY: inline-block; FONT-SIZE: 10px; TOP: 7px; CURSOR: pointer; RIGHT: 5px" onclick="menuClosed()"></span></div>
            <div id="leftmenu" class="leftsidebar-bottom" name="资金管理" sizset="true" >
                <div class="leftsidebar-content1" sizset="true" >
                    <div class="leftsidebar-content-img1">
                        <img alt="" src="../images/shared/tzgl.png" width="25" height="20"></div>
                    <div style="CURSOR: pointer" class="leftsidebar-content-size1" onclick="acc(this)" url="/FundManage/FundList.aspx">资金使用情况</div>
                    <div class="leftsidebar-content-d1">
                        <img alt="" src="../images/shared/img-26.gif"></div>
                </div>
                <div class="leftsidebar-content" sizset="false" >
                    <div class="leftsidebar-content-img">
                        <img alt="" src="../images/shared/tzgl.png" width="25" height="20"></div>
                    <div style="CURSOR: pointer" class="leftsidebar-content-size" onclick="acc(this)" url="/FundManage/NoPayList.aspx">未完成支付项目</div>
                    <div class="leftsidebar-content-d">
                        <img alt="" src="../images/shared/img-19.jpg"></div>
                </div>
            </div>
        </div>



        <div class="right-content1" style="width:auto;height:100%;float:left" >
            <iframe id="iframeContent" name="Content" style="width: 100%; height: 100%;  overflow-x: hidden;" frameborder="0" src="/FundManage/FundList.aspx"></iframe>
        </div>
    </div>

    <script type="text/javascript">

        var $winh = $(window).height();
        $(function () {
            SetHeadMenu();
            $(".right-content1,.content1").height($winh - 50 - 72);
            $("#iframeContent").height($(".right-content1").height());
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

        function zTreeonClick(event, treeId, treeNode) {
            if (treeNode.name == '系统设置') {
                window.open(treeNode.href);
            }
            else
                document.getElementById("iframeContent").src = treeNode.href;

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
    </script>
</body>
</html>
