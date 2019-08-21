<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultZJ.aspx.cs" Inherits="WebApp.Support.Login.DefaultZJ" %>

  <!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="content">
<table width="1024px" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td height="252px">&nbsp;</td>
  </tr>
  <tr>
    <td height="29px"><table width="0" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="680px" height="28px">&nbsp;</td>
        <td>
          <label>
            <input type="text" name="username" style="width:160px;background-color:#f2f9ff; border:1px solid #f2f9ff;"/>
            </label>
        
        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="23px">&nbsp;</td>
  </tr>
  <tr>
    <td height="29px"><table width="0" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="680px" height="28px">&nbsp;</td>
        <td>
          <label>
            <input type="password" name="password" style="width:160px;background-color:#f2f9ff; border:1px solid #f2f9ff;"/>
            </label>
        
        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="36px">&nbsp;</td>
  </tr>
  <tr>
    <td><table width="0" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="683px"></td>
        <td><table width="0" border="0" cellpadding="0" cellspacing="0">
          <tr>
          
            <td><a style="cursor:pointer" onclick="$('form').submit()"><img src="images/login.jpg" width="72" height="28px"  alt="" /></a></td>
            <td width="12px">&nbsp;</td>
            <td><a style="cursor:pointer" onclick="reset()"><img src="images/resit.jpg" width="72" height="28px"  alt="" /></a></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="180px"></td>
  </tr>
  <tr>
    <td><div align="center"></div></td>
  </tr>
</table>
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
</script>