﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Yawei.App.Support.Login.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>欢迎登录管理系统</title>
<link href="css/login.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" src="js/jquery.js"></script>
<script src="js/cloud.js" type="text/javascript"></script>
<script src="js/layer/layer.js" type="text/javascript" ></script>

<script language="javascript">
    $(function () {
        $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
        $(window).resize(function () {
            $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
        })
    });
</script> 

</head>

<body style="background-color:#1c77ac; background-image:url(images/light.png); background-repeat:no-repeat; background-position:center top; overflow:hidden;">
    <form id="form1" runat="server">


    <div id="mainBody">
      <div id="cloud1" class="cloud"></div>
      <div id="cloud2" class="cloud"></div>
    </div>  


<div class="logintop">    
    <span>欢迎登录泰州市电子政务项目管理系统</span>    
    <ul>
    <li><a href="#" onclick="showadmininfo()">联系管理员</a></li>
    <li><a href="#" onclick="showhelp()">帮助</a></li>
    <li><a href="#" onclick="showabout()">关于</a></li>
    </ul>    
    </div>
    
    <div class="loginbody">
    
    <span class="systemlogo">泰州市电子政务项目管理平台 </span>
       
    <div class="loginbox">
    
    <ul>
    <li><input name="username" type="text" class="loginuser" value="请输入用户名" onclick="JavaScript: this.value = ''" onblur="if(this.value==''){this.value='请输入用户名'}"/></li>
    <li><input name="password" type="password" class="loginpwd" value="请输入密码" onclick="JavaScript: this.value = ''" onblur="if(this.value==''){this.value='请输入密码'}"/></li>
    <li><input name="" type="button" class="loginbtn" value="登录"  onclick="$('form').submit()" style="margin-right:20px;"  /><input name="" type="button" class="loginbtn" value="重置"  onclick="    javascript: window.location = '#'"  /></li>
    </ul>
    
    
    </div>
    
    </div>
    
    
    
    <div class="loginbm">
        <a href="<%=ConfigurationManager.AppSettings["flashurl"] %>/install_flash_player_ax_cn.exe">IE及360 flash下载</a>
        &nbsp;/&nbsp;
        <a href="<%=ConfigurationManager.AppSettings["flashurl"] %>/install_flash_player_ppapi_cn.exe">Chrome flash下载</a>
        &nbsp;/&nbsp;
        <a href="<%=ConfigurationManager.AppSettings["flashurl"] %>/install_flash_player_cn.exe">Firefox flash下载</a>
        山东亚微软件股份有限公司  2018

    </div>
	
    </form>
</body>

</html>
<script type="text/javascript">
    $(function () {
        <%=jquery%>
        $("input[name='password'],input[name='username']").keydown(function () {
            if (event.keyCode == 13) {
                if ($("input[name='username']").val() == "") {
                    alert('用户名不能为空');
                    return false;
                }
                if ($("input[name='password']").val() == "") {
                    alert('密码不能为空');
                    return false;
                }

                $("form").submit();
            }
        });
    })

    function reset() {
        $("input[name='password'],input[name='username']").val('');
    }

    function showadmininfo() {
    }

    function showhelp() {
    }

    function showabout() {
        //layer.open({
        //    title: '关于'
        //    , content: '<div>名称：泰州市电子政务项目管理系统</div><div>版本：V1.0</div>'
        //});

    }
</script>

