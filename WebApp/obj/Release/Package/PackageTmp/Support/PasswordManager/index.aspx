<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp.Support.PassWordManager.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../../Content/Form.css" rel="stylesheet" type="text/css" />
    <link href="../../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <link href="../../../Content/css.css" rel="stylesheet" />
    <script src="../../../Plugins/datepicker/WdatePicker.js"></script>
    <link href="../../../Plugins/datepicker/skin/WdatePicker.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="FormMenu">
            <div class="MenuLeft">
                &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />&nbsp;&nbsp;修改密码
            </div>
                <div class="MenuRight">                  
                    <a style="cursor: pointer" id="save" onclick="copyText()">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>                  
                </div>
        </div>
        <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'  >旧密码</td>
                    <td class='TdContent'  colspan="3">
                        <input type="password"  name="Pass1" id="Pass1"/>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>新密码</td>
                    <td class='TdContent'>
                        <input type="password" name="Pass2" id="Pass2"/>
                    </td>
                </tr>
                     <tr>
                    <td class='TdLabel'>重复密码</td>
                    <td class='TdContent'>
                        <input type="password" name="Pass3" id="Pass3"/>
                    </td>
                </tr>
            </table>
    </form>

    <script>
        function copyText() {
         if($("#Pass2").val()!=$("#Pass3").val())
            {
                alert("两次输入密码不一致！");
            }
            else
            {
                $("#form1").submit();
            }
            $("#form1").submit();

            
        }
    </script>
</body>
</html>
