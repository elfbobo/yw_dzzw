<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectForm.aspx.cs" Inherits="WebApp.Project.ProjectForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <link href="../Content/Layout.css" rel="stylesheet" />
    <script src="../Plugins/jquery-ztree/ztree-all.min.js" type="text/javascript"></script>
    <link href="../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
    <link href="../Content/Form.css" rel="stylesheet" />
    <link href="../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <script src="../Plugins/jquery.messager.js"></script>
    <style type="text/css">
        input[type='button'] {
            margin-left: 20px;
            height: 23px;
            min-width: 60px;
            padding: 2px 5px 2px 5px;
            border: 1px solid #a3cdf3;
            border-radius: 3px;
            background-color: #ffffff;
            color: #1e5f85;
            font-size: 12px;
            font-weight: bold;
            cursor: pointer;
        }

        .menuDiv {
            border-bottom: 1px solid #c6bfbf;
            margin: 2px 0 0 2px;
            cursor: pointer;
            padding-bottom: 2px;
        }

        .menuDivSed {
            border-bottom: 1px solid #c6bfbf;
            margin: 2px 0 0 2px;
            cursor: pointer;
            padding-bottom: 4px;
            background-color: white;
        }

        .titleSpan {
            display: block;
            font-size: 12px;
        }

        .a:hover {
            text-decoration: underline;
        }

        .a:link {
            text-decoration: none;
        }

        .a:link {
            text-decoration: none;
        }
    </style>
</head>
<body style="min-width: 1010px; overflow: hidden">
    <form id="form1" runat="server">
        <div class="content">
            <div id="since" class="leftsidebar" style="vertical-align: top; width: 70px; background-color: #eaf2ff; box-shadow: 0 0 10px black; text-align: center">
            </div>
            <div class="leftsidebar" id="all" style="vertical-align: top; width: 250px; display: none; position: relative">
                <div class="leftsidebar-top" style="line-height: 30px; color: #2772ac; font-size: 14px; font-weight: bold; position: relative; width: 100%">
                    <img src="../images/shared/xtcd.png" style="height: 20px; width: 25px; vertical-align: text-bottom; margin-left: 15px" />
                    系统菜单
            <span style="cursor: pointer; display: inline-block; font-size: 10px; right: 5px; top: 7px; position: absolute">
                <img src="../images/application_double.png" onclick="classtab(2)" style="position: absolute; right: 5px; top: 0px; cursor: pointer; width: 16px" />
            </span>
                </div>
                <ul class="ztree" id="ztreeall" style="width: 230px; overflow: auto"></ul>

            </div>

            <div id="center" style="width: 8px; display: table-cell; border-right: 1px solid lightgray;">

                <div id="smenu" style="display: none; position: absolute; width: 200px; background-color: white; border: 1px solid lightgray; box-shadow: 2px 2px 8px #aaaaaa; border-left: 0px">
                    <div style="height: 18px; background-color: #eaf2ff"></div>
                    <div id="cmenu" style="width: 200px; overflow-x: hidden; overflow-y: auto">
                        <ul class="ztree" id="ztree"></ul>
                    </div>
                    <img id="x" src="../images/cross.png" style="position: absolute; right: 0px; top: 0px; cursor: pointer;" onclick="$('#smenu').hide()" />
                    <span class="tree-folder-open" onclick="center(this)" t="2" style="position: absolute; right: 20px; top: -1px; cursor: pointer;"></span>
                    <img src="../images/application_view_list.png" onclick="classtab(1)" style="position: absolute; right: 40px; top: -0px; cursor: pointer; width: 16px" />
                </div>

            </div>
            <div class="right-content" style="vertical-align: top">
                <iframe id="ifmContent" name="Content" style="width: 100%; height: 100%; display: block; overflow-x: hidden" frameborder="0" src="StartProject/View.aspx?xmguid=<%=Request["Guid"]??"" %>"></iframe>
            </div>
        </div>

    </form>
</body>
</html>
<script type="text/javascript">
    var menuJson = [<%=menuJson%>]
    var projGuid = '<%=Request["Guid"]%>';
    var projJson=<%=projJson%>;
    var setting = {
        callback: {
            onClick: zTreeOnClick
        }

    };
    $(function () {
        //  zTreeObj = $.fn.zTree.init($(".ztree"), setting, menuJson);
        $.fn.zTree.init($("#ztreeall"), setting, menuJson).expandAll(true);
        CreateFirstMenu();
        $(".content").height($(window.parent.document).find("#iframeContent").height());
        $("#ifmContent").height($(".leftsidebar").height()-70);
        $("#projName").text(projJson[0].ProjName);
        $("#smenu").height($(".leftsidebar").height()).css({left:$(".leftsidebar").width()-2,top:0});
        $("#ztreeall").height($(".leftsidebar").height()-15);
        $("#smenu").hover(function(){},function(){
            if($("span[t]").attr("t")=="2")
                $("#smenu").hide();
        })

    });

    function zTreeOnClick(event, treeId, treeNode)
    {
        var url = treeNode.href;
        if(url=="")
            return;
        if (url.indexOf('?') > -1)
            url = url + "&xmguid=" + projGuid;
        else
            url = url + "?xmguid=" + projGuid;
        if (url.indexOf("ProjRegister/View") > -1)
            url = url.replace('ProjGuid', 'Guid');
        $("#ifmContent").attr("src", url);
        if($("span[t]").attr("t")=="2")
            $("#smenu").hide();
    }

    function reBack()
    {
        $(window.parent.document).find("iframe").attr("src","XMList.aspx");
    }

    function projectClosed(pGuid,projName,ProjCode)
    {
        //projGuid=pGuid;
        //$("#projName").text(projName);
        //$("#ifmContent").attr("src","ProjRegister/View.aspx?guid="+projGuid);
        $(window.parent.document).find("iframe").attr("src","/ProjectForm.aspx?guid="+pGuid);
    }

    function showDialog()
    {
        $('.dialog').css({left:($(window).width()-$(".dialog").width())/2,top:50}).slideDown();
    }

    function CreateFirstMenu()
    {
        //leftsidebar
        var htmlMenu='';
        $.each(menuJson,function(i,dat)
        {
            htmlMenu+='<div onclick="accfirst(this)"  class="menuDiv"  title="'+dat.name+'" url="'+dat.href+'">';
            htmlMenu+='<img style="width:16px;height:16px" src="'+dat.imgurl+'"    />';
            htmlMenu+=' <span class="titleSpan">'+dat.name+'</span> </div>';
        });
   
        $("#since").append(htmlMenu);     
             
    }

    function divonclick(obj)
    {
        $("#smenu").show();
        var title=$(obj).attr("title");
        $(".menuDivSed").removeClass("menuDivSed").addClass("menuDiv");
        $(obj).addClass("menuDivSed").removeClass("menuDiv");
        $.each(menuJson,function(i,dat){
            if(dat.name==title)
            {
                //if(title=="财务管理"||title=="投资管理"||title=='质量安全'||title=='验收封存')
                //{ 
                //    $("#cmenu").children().remove();
                //    $("#cmenu").append('<ul class="ztree" id="ztree" style="overflow:auto" ></ul>');
                //    zTreeObj = $.fn.zTree.init($("#ztree"), setting, dat.children);
                //    zTreeObj.expandAll(true);
                //}
                //else
                //    {
                var html="",crurl='';$("#cmenu").children().remove();
                $.each(dat.children, function (j, dt) {

                    html += '<div class="leftsidebar-content"> <div class="leftsidebar-content-img"><img src="../images/book_open.png"  alt="" /></div>';
                    html += '<div  url="'+dt.href+'"  onclick="acc(this)" style="cursor:pointer"  class="leftsidebar-content-size">' + dt.name + '</div><div class="leftsidebar-content-d"></div></div>';

                    if (dt.children)
                    {
                        html += "<div le='2' >";
                        $.each(dt.children, function (m, d) {
                      
                            html += '<div class="leftsidebar-content2"> <div class="leftsidebar-content-img2"><img src="../images/shared/img-23.gif" width="7" height="8px" alt="" /></div>';
                            html += '<div class="leftsidebar-content-size2"><a onclick="acc(this)" url="'+d.href+'" target="Content">' + d.name + '</a></div></div>';
                        });
                        html += "</div>";
                    }
                });
                $("#cmenu").append(html);
                
                //}
                return false;
            }
        })
      
    }
    
    function center(obj)
    {
        if($(obj).attr("t")==2)
        {
            $("#smenu").css({"position":"relative",left:0});
            $("#center").width(207);
            $(obj).attr("t",1);$("#x").hide();
            $(obj).addClass("tree-folder").removeClass("tree-folder-open");
        }
        else if($(obj).attr("t")==1)
        {
            $("#smenu").css({"position":"absolute",left:$(".leftsidebar").width()-2});
            $("#center").width(8);
            $(obj).attr("t",2);$("#x").show();
            $(obj).addClass("tree-folder-open").removeClass("tree-folder");
        }
    }

    function classtab(t)
    {
        if(t==1)
        {
            $("#since").hide();
            $("#all").show();
            $("#center").width(8);
            $("#smenu").hide();
        }
        else
        {
            $("#since").show();
            $("#all").hide();
            var obj=$("span[t]").attr("t",1);
            center(obj);
        }
    }

    function classtabxmxx()
    {
        $("#since").hide();
        //$("#all").show();
        $("#center").width(8);
        $("#smenu").hide();
    }

    function acc(obj)
    {
        if ($(obj).attr("url")!="")
        {
            var url=$(obj).attr("url");
            if (url.indexOf('?') > -1)
                url = url + "&xmguid=" + projGuid;
            else
                url = url + "?xmguid=" + projGuid;
            $("#ifmContent").attr("src", url);
        }
    }

    function accfirst(obj)
    {
        if ($(obj).attr("url")!="")
        {
            var url=$(obj).attr("url");
            if (url.indexOf('?') > -1)
                url = url + "&xmguid=" + projGuid;
            else
                url = url + "?xmguid=" + projGuid;
            $("#ifmContent").attr("src", url);
        }
    }





</script>
