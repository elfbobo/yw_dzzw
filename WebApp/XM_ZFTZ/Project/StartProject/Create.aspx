<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.XM_ZFTZ.Project.StartProject.Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <title></title>
    <link href="../../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../../Plugins/datepicker/WdatePicker.js"></script>

    <script type="text/javascript" charset="utf-8" src="../../../Plugins/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../../Plugins/ueditor/ueditor.all.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../../Plugins/ueditor/lang/zh-cn/zh-cn.js"></script>
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
                    <td class='TdLabel'>项目类别</td>
                    <td class='TdContent' colspan="3">
                        <asp:DropDownList AutoPostBack="true" ID="ProCategory" runat="server" OnSelectedIndexChanged="ProCategory_SelectedIndexChanged" style="min-width:150px;width:98%">
                            <asp:ListItem Value="交通" Text="交通" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="水利" Text="水利"></asp:ListItem>
                            <asp:ListItem Value="市政设施" Text="市政设施"></asp:ListItem>
                            <asp:ListItem Value="园林绿化" Text="园林绿化"></asp:ListItem>
                            <asp:ListItem Value="社会事业" Text="社会事业"></asp:ListItem>
                            <asp:ListItem Value="业务用房" Text="业务用房"></asp:ListItem>
                            <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        </asp:DropDownList>

                         <asp:TextBox runat="server" ID="ProCategoryDesc" style="margin-left:5px"  Visible="false" Width="200px"></asp:TextBox>
                    </td>

                </tr>

                <tr>
                    <td class='TdLabel'>建设规模及内容</td>
                    <td class='TdContent' colspan="3">
                        <asp:TextBox runat="server" ID="ProContent" empty="true" ></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class='TdLabel'>总投资匡算(万元)</td>
                    <td class='TdContent'>
                        <asp:TextBox runat="server" ID="Quota" empty="true" double='true'></asp:TextBox>
                    </td>
                    <td class='TdLabel'>资金来源</td>
                    <td class='TdContent'>
                        <asp:DropDownList AutoPostBack="true" ID="MoneySource" runat="server" OnSelectedIndexChanged="MoneySource_SelectedIndexChanged" style="min-width:150px;width:50%">
                            <asp:ListItem Value="财政预算内资金" Text="财政预算内资金" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="自筹资金" Text="自筹资金"></asp:ListItem>
                            <asp:ListItem Value="上争资金" Text="上争资金"></asp:ListItem>
                            <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:TextBox runat="server" ID="MoneySourceDesc" Visible="False" Width="48%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>建设性质</td>
                    <td class="TdContent">
                       <asp:DropDownList AutoPostBack="true" ID="ProProperty" runat="server" style="min-width:150px;width:98%" OnSelectedIndexChanged="ProProperty_SelectedIndexChanged">
                            <asp:ListItem Value="新建" Text="新建" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="改建" Text="改建"></asp:ListItem>
                            <asp:ListItem Value="并建" Text="并建"></asp:ListItem>
                            <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="ProPropertyDesc" Visible="False" Width="200px"></asp:TextBox>
                    </td>
                    <td class='TdLabel'>拟建地点</td>
                    <td class="TdContent">
                       <asp:TextBox runat="server" ID="ProLocation" empty="true"></asp:TextBox>
                    </td>
                </tr>


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
                    <td class='TdLabel'>建设工期（年）</td>
                    <td class='TdContent'>
                        <asp:TextBox empty="true" ID="ProDuration" double="true" runat="server"></asp:TextBox>
                    </td>
                    <td class='TdLabel'>责任单位</td>
                    <td class='TdContent'>
                        <asp:TextBox empty="true" ID="MainDeptName" runat="server"></asp:TextBox>
                    </td>
                   </tr>
                <tr>
                    <td class="TdLabel">备注</td>
                    <td colspan="3" class='TdContent'>
                        <asp:TextBox ID="Remarks" runat="server"></asp:TextBox>
                    </td>
                </tr>

            </table>

            <fieldset style="margin-top: 5px">
                <legend><span id="file1" style="width: 60px; height: 20px; display: block">单位负责人意见</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>
            
            <fieldset style="margin-top: 5px">
                <legend>
                    <span id="file2" style="width: 60px; height: 20px; display: block">市政府分管领导负责人意见</span>
                </legend>
                <div id="filecontent2" style="margin-top: 10px"></div>
            </fieldset>
            
            <fieldset style="margin-top: 5px">
                <legend>
                    <span id="file3" style="width: 60px; height: 20px; display: block">规划和计划要求</span>
         
                </legend>
                <div id="filecontent3" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend>
                    <span id="file4" style="width: 60px; height: 20px; display: block">上级政策要求</span>
         
                </legend>
                <div id="filecontent4" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend>
                    <span id="file5" style="width: 60px; height: 20px; display: block">市委、市政府决策文件</span>
         
                </legend>
                <div id="filecontent5" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend>
                    <span id="file6" style="width: 60px; height: 20px; display: block">人大代表、政协委员意见建议</span>
         
                </legend>
                <div id="filecontent6" style="margin-top: 10px"></div>
            </fieldset>
            <fieldset style="margin-top: 5px">
                <legend>
                    <span id="file7" style="width: 60px; height: 20px; display: block">社会公众意见</span>
         
                </legend>
                <div id="filecontent7" style="margin-top: 10px"></div>
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

        var cfg1 = [];
        cfg1.content = "filecontent1";
        cfg1.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        //alert(cfg0.applicationPath);
        cfg1.fileSign = "tz_TzProject_Dwfze";
        cfg1.title = "单位负责人意见";
        $("#file1").uploadfile(cfg1);


        var cfg2 = [];
        cfg2.content = "filecontent2";
        cfg2.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg2.type = "database";
        cfg2.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg2.fileSign = "tz_TzProject_Zffzr";
        cfg2.title = "市政府分管负责人意见";
        $("#file2").uploadfile(cfg2);

        var cfg3 = [];
        cfg3.content = "filecontent3";
        cfg3.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg3.type = "database";
        cfg3.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg3.fileSign = "tz_TzProject_Ghyq";
        cfg3.title = "规划和计划要求";
        $("#file3").uploadfile(cfg3);

        var cfg4 = [];
        cfg4.content = "filecontent4";
        cfg4.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg4.type = "database";
        cfg4.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg4.fileSign = "tz_TzProject_Sjzc";
        cfg4.title = "上级政策要求";
        $("#file4").uploadfile(cfg4);

        var cfg5 = [];
        cfg5.content = "filecontent5";
        cfg5.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg5.type = "database";
        cfg5.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg5.fileSign = "tz_TzProject_Jcwj";
        cfg5.title = "市委、市政府决策文件";
        $("#file5").uploadfile(cfg5);

        var cfg6 = [];
        cfg6.content = "filecontent6";
        cfg6.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg6.type = "database";
        cfg6.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg6.fileSign = "tz_TzProject_Yjjy";
        cfg6.title = "人大代表、政协委员意见建议";
        $("#file6").uploadfile(cfg6);

        var cfg7 = [];
        cfg7.content = "filecontent7";
        cfg7.refGuid = "<%=ViewState["Guid"].ToString()%>";
        cfg7.type = "database";
        cfg7.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg7.fileSign = "tz_TzProject_Gzyj";
        cfg7.title = "社会公众意见";
        $("#file7").uploadfile(cfg7);
    });

    var formCore = new FormCore();
    formCore.FormVildateLoad();
    var config = new Config();
    $('#save').click(function () {
        if (confirm("确定你要保存吗")) {
            if ($('#filecontent1').children().length <= 1) {
                alert("请上传单位负责人意见");
                return;
            }

            if ($('#filecontent2').children().length <= 1) {
                alert("请上传市政府分管负责人意见");
                return;
            }

            //表单元素校验成功
            if (formCore.FormVildateSubmit()) {
                //校验是否有冲突项目名
                $.ajax({
                    type: 'post',
                    url: 'ZftzWebFunction.ashx',
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

</script>

<script src="<%=Yawei.Common.AppSupport.AppPath %>/js/filefix.js" type="text/javascript"></script>
