<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.Project.StartProject.Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <title></title>
    <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>

    <script type="text/javascript" charset="utf-8" src="../../Plugins/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../Plugins/ueditor/ueditor.all.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../Plugins/ueditor/lang/zh-cn/zh-cn.js"></script>
    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                </div>
                <div class="MenuRight">
<%--                    <%if (CurrentUser.HasSave())
                      {%>--%>
                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
<%--                    <%} %>--%>
                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'>项目名称</td>
                    <td class='TdContent' colspan="3">
                        <asp:TextBox runat="server" ID="PsFiles" type="hidden"></asp:TextBox>
                        <asp:TextBox runat="server" ID="ProName" empty="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>项目简介</td>
                    <td class='TdContent'>
                        <asp:TextBox runat="server" ID="ProSummary"  empty="true"></asp:TextBox>
                    </td>
                    <td class='TdLabel'>批复金额(万元)</td>
                    <td class='TdContent'>
                        <asp:TextBox runat="server" ID="Quota"  empty="true"  double='true' ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'><font color="red">*</font>资金来源</td>
                    <td class='TdContent'>
                        <div>
                            <asp:TextBox ID="MoneySource" runat="server"  empty="true"></asp:TextBox>
                        </div>
                    </td>
                    <td class='TdLabel'>是否新开工项目</td>
                    <td class='TdContent' >
                        <div>
                            <asp:RadioButtonList runat="server" ID="IsNewStart" RepeatDirection="Horizontal" CellSpacing="20" >
                                <asp:ListItem Value="1" Text="是" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>申报部门</td>
                    <td class='TdContent'>
                        <div>
                            <asp:TextBox ID="StartDeptName" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox ID="StartDeptGuid" ReadOnly="true" runat="server" type="hidden"></asp:TextBox>
                        </div>
                    </td>
                      <td class='TdLabel'>项目类型</td>
                    <td class='TdContent'>
                        <div>
                             <%--<asp:RadioButtonList runat="server" ID="ProType" RepeatDirection="Horizontal" CellSpacing="20"  >
                                <asp:ListItem Value="软件平台" Text="软件平台" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="硬件平台" Text="硬件平台"></asp:ListItem>
                                 <asp:ListItem Value="服务运维" Text="服务运维"></asp:ListItem>
                            </asp:RadioButtonList>--%>
                            <asp:CheckBoxList runat="server" ID="ProType" RepeatDirection="Horizontal" CellSpacing="20"  >
                                <asp:ListItem Value="软件平台" Text="软件平台" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="硬件平台" Text="硬件平台"></asp:ListItem>
                                 <asp:ListItem Value="服务运维" Text="服务运维"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>联系人</td>
                    <td class='TdContent'>
                        <div>
                            <asp:TextBox ID="ContactName" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td class='TdLabel'>联系人电话</td>
                    <td class='TdContent'>
                        <div>
                            <asp:TextBox ID="ContactTel" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                 <tr>
                    <td class='TdLabel'>申报时间</td>
                    <td class='TdContent'>
                        <div>
                            <asp:TextBox runat="server" ID="StartDate" ReadOnly="true" class="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                        </div>
                    </td>
                    <td class='TdLabel'>是否部署云平台</td>
                    <td class='TdContent'>
                        <div>
                            <asp:RadioButtonList runat="server" ID="IsInCloudPlat" RepeatDirection="Horizontal" CellSpacing="20">
                                <asp:ListItem Value="1" Text="是" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
            </table>
            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 60px; height: 20px; display: block"></span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>
        </div>

        <asp:HiddenField ID="CreateDate" runat="server" />
        <asp:HiddenField ID="EditDate" runat="server" />
        <asp:HiddenField ID="InfoType" runat="server" Value="Polices_Laws" />
        <asp:Button runat="server" ID="SaveButton" OnClick="Page_SaveData" Style="display: none" />
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    

    $(function () {

        var cfg0 = [];
        cfg0.content = "filecontent0";
        cfg0.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_Project";
        cfg0.title = "附件";

        $("#file0").uploadfile(cfg0);


        //$("#ProName").focusout(function () {
            //alert("in");
        //    if ("<--%=strGuid%>" == "")
        //        IsExistProject('Polices_Laws', $('#ProjGuid').val());
        //});
        

    });

    var formCore = new FormCore();
    formCore.FormVildateLoad();
    var config = new Config();
    $('#save').click(function () {

        //校验是否有冲突项目名
        $.ajax({
            type: 'post',
            url: 'WebFunction.ashx',
            data: { action: 'namevalid', proname: $("#ProName").val() },
            success: function (data) {
                //校验返回1 表示已有相同项目名称
                if (data == "1") {
                    alert("已存在相同名称项目！")
                }
                else if (data == "0") {
                    if (confirm("确定你要保存吗")) {
                        var pfjestr = $("#Quota").val();
                        if (pfjestr.length > 10 || parseFloat(pfjestr) == 0) {
                            alert("请输入十位以下且大于0的纯数字！");
                            return false;
                        }
                        if (formCore.FormVildateSubmit()) {
                            //var b = true;
                            //if (callback)
                            //    b = callback();

                            //if (b || typeof (b) == "undefined")
                                $("#SaveButton").click();
                        }
                    }
                }

            }
        });


    });


    var numvalida = numvalifun();
    function numvalifun() {
        var pfjestr = $("#Quota").val();
        if (pfjestr.length > 10 || parseInt(pfjestr) == 0) {
            alert("请输入十位以下且大于0的纯数字！");
            return false;
        }
        //var pfje = parseFloat($("#Quota").val());

        //alert("?????");
        return true;
    }

    //config.saveData();
    //$(function () {
    //    //文本编辑器
    //    //UE.getEditor('Content');

    //    $("#save").on("click", function () {
    //        if (isNaN($("#Quota").val())) {
    //            alert("请在金额位置输入纯数字！");
    //            return;
    //        }
    //        //alert($("#Quota").val());
    //        //$("#SaveButton").click();
           
    //    })
    //})

</script>

