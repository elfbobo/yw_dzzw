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
                    <a href="List.aspx">&nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    </a>

                </div>
                <div class="MenuRight">
                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>
                    <a style="cursor: pointer" href="List.aspx">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>
                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'>项目名称</td>
                    <td class='TdContent' colspan="3">
                        <asp:TextBox runat="server" ID="ProName" empty="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>项目简介</td>
                    <td class='TdContent' colspan="3">
                        <asp:TextBox runat="server" ID="ProSummary" empty="true"></asp:TextBox>
                    </td>

                </tr>

                <tr>
                    <td class='TdLabel'>预估金额(万元)</td>
                    <td class='TdContent'>
                        <asp:TextBox runat="server" ID="Quota" empty="true" double='true'></asp:TextBox>
                    </td>
                    <td class='TdLabel'>资金来源</td>
                    <td class='TdContent'>
                        <%-- <asp:TextBox runat="server" ID="MoneySource" empty="true"></asp:TextBox>--%>
                        <asp:DropDownList AutoPostBack="true" ID="MoneySource" runat="server" OnSelectedIndexChanged="MoneySource_SelectedIndexChanged">
                            <asp:ListItem Value="本级财政性资金" Text="本级财政性资金" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="上级财政性资金" Text="上级财政性资金"></asp:ListItem>
                            <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:TextBox runat="server" ID="MoneySourceDesc" Visible="False" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'><font color="red">*</font>项目属性</td>
                    <td class="TdContent">
                        <asp:RadioButtonList runat="server" ID="ProProperty" RepeatDirection="Horizontal" CellSpacing="30">
                            <asp:ListItem Value="新建" Text="新建" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="续建" Text="续建"></asp:ListItem>
                            <asp:ListItem Value="改建" Text="改建"></asp:ListItem>
                            <asp:ListItem Value="运维" Text="运维"></asp:ListItem>
                            <asp:ListItem Value="购买服务" Text="购买服务"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class='TdLabel'><font color="red">*</font>项目类型</td>
                    <td class='TdContent'>
                        <%--<asp:RadioButtonList runat="server" ID="ProType" RepeatDirection="Horizontal" CellSpacing="20"  >
                                <asp:ListItem Value="软件平台" Text="软件平台" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="硬件平台" Text="硬件平台"></asp:ListItem>
                                 <asp:ListItem Value="服务运维" Text="服务运维"></asp:ListItem>
                            </asp:RadioButtonList>--%>
                        <asp:CheckBoxList runat="server" ID="ProType" RepeatDirection="Horizontal" CellSpacing="20">
                            <asp:ListItem Value="软件平台" Text="软件平台" Selected="true"></asp:ListItem>
                            <asp:ListItem Value="硬件平台" Text="硬件平台"></asp:ListItem>
                            <asp:ListItem Value="服务运维" Text="服务运维"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>

                <%--                <tr>

                    <td class='TdLabel'>是否新开工项目</td>
                    <td class='TdContent' >
                        <div>
                            <asp:RadioButtonList runat="server" ID="IsNewStart" RepeatDirection="Horizontal" CellSpacing="20" >
                                <asp:ListItem Value="1" Text="是" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>--%>

                <tr>
                    <td class='TdLabel'>申报部门</td>
                    <td class='TdContent'>
                        <asp:TextBox empty="true" ID="StartDeptName" ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:TextBox ID="StartDeptGuid" ReadOnly="true" runat="server" type="hidden"></asp:TextBox>
                    </td>
                    <td class='TdLabel'>申报时间</td>
                    <td class='TdContent'>
                        <asp:TextBox empty="true" runat="server" ID="StartDate" ReadOnly="true" class="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>联系人</td>
                    <td class='TdContent'>
                        <asp:TextBox empty="true" ID="ContactName" runat="server"></asp:TextBox>
                    </td>
                    <td class='TdLabel'>联系人电话</td>
                    <td class='TdContent'>
                        <asp:TextBox ID="ContactTel" empty="true" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>

                    <td class='TdLabel'><font color="red">*</font>是否部署云平台</td>
                    <td class='TdContent' colspan="3">
                        <asp:RadioButtonList runat="server" ID="IsInCloudPlat" RepeatDirection="Horizontal" CellSpacing="20">
                            <asp:ListItem Value="1" Text="是" Selected="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="否"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>

            <fieldset style="margin-top: 5px">
                <legend><span id="file1" style="width: 60px; height: 20px; display: block">建设方案</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>

            <fieldset style="margin-top: 5px">
                <legend>
                    <span id="file2" style="width: 60px; height: 20px; display: block">项目申报表</span>
         
                </legend>
                <div id="filecontent2" style="margin-top: 10px"></div>
            </fieldset>



            <fieldset style="margin-top: 5px;display:none" >
                <legend><span id="file0" style="width: 60px; height: 20px; display: block"></span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>


            <div style="width:100%;text-align:left">
                <span style="color:red;width:100%;font-size:12px">备注：请各部门上传加盖公章的《泰州市电子政务建设项目申报表》。</span>
            </div>

            <fieldset style="margin-top: 5px">
                <legend>
                    <span id="file3" style="width: 60px; height: 20px; display: block">云资源使用申请表</span>
         
                </legend>
                <div id="filecontent3" style="margin-top: 10px"></div>
            </fieldset>

            
        </div>
        <asp:TextBox ID="StartUserGuid" ReadOnly="true" runat="server" type="hidden"></asp:TextBox>
        <asp:HiddenField ID="CreateDate" runat="server" />
        <asp:HiddenField ID="EditDate" runat="server" />
        <%--<asp:HiddenField ID="InfoType" runat="server" Value="Polices_Laws" />--%>
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
        //alert(cfg0.applicationPath);
        cfg0.fileSign = "tz_Project_Fj";
        cfg0.title = "附件";


        $("#file0").uploadfile(cfg0);

        var cfg1 = [];
        cfg1.content = "filecontent1";
        cfg1.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "tz_Project_Fa";
        cfg1.title = "项目方案";


        $("#file1").uploadfile(cfg1);

        var cfg2 = [];
        cfg2.content = "filecontent2";
        cfg2.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg2.type = "database";
        cfg2.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg2.fileSign = "tz_Project_Sbb";
        cfg2.title = "项目申报表";


        $("#file2").uploadfile(cfg2);

        var cfg3 = [];
        cfg3.content = "filecontent3";
        cfg3.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg3.type = "database";
        cfg3.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg3.fileSign = "tz_Project_Yzy";
        cfg3.title = "云资源使用申请表";


        $("#file3").uploadfile(cfg3);

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
        if (confirm("确定你要保存吗")) {
            if ($('#filecontent1').children().length <= 1) {
                alert("请上传项目方案附件");
                return;
            }

            if ($('#filecontent2').children().length <= 1) {
                alert("请上传项目申报表附件");
                return;
            }

            //表单元素校验成功
            if (formCore.FormVildateSubmit()) {


                var pfjestr = $("#Quota").val();
                if (pfjestr.length > 10 || parseFloat(pfjestr) == 0) {
                    alert("请输入十位以下且大于0的纯数字！");
                    return false;
                }


                //校验是否有冲突项目名
                $.ajax({
                    type: 'post',
                    url: 'WebFunction.ashx',
                    data: { action: 'namevalid', proname: $("#ProName").val(), proguid: "<%=ViewState["Guid"].ToString()%>" },
                    success: function (data) {
                        //校验返回1 表示已有相同项目名称
                        if (data == "1") {
                            alert("已存在相同名称项目！")
                        }
                        else if (data == "0") {
                            $("#SaveButton").click();
                        }

                    }
                });
            }
        }




    });

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

<script src="<%=Yawei.Common.AppSupport.AppPath %>/js/filefix.js" type="text/javascript"></script>
