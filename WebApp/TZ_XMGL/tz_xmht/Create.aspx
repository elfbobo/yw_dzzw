<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_xmht.Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="../../Content/Form.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/FormCore.js" type="text/javascript"></script>
    <script src="../../Plugins/datepicker/WdatePicker.js"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.grid/jquery.grid.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery.grid/themes/grid_blue.css" rel="stylesheet" />
    <script src="../../Scripts/config.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery.dynamicrow.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;当前位置：<a href="List.aspx?ProjGuid=<%=proGuid %>">项目合同信息</a> >> 编辑项目合同信息
                </div>
                <div class="MenuRight">
                   
                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>

                </div>
            </div>
            <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
                <tr>

                    <td class="TdLabel" style="width: 15%">合同名称</td>
                    <td class="TdContent" style="width: 35%">
                         <asp:TextBox runat="server" empty="true" ID="htmc"></asp:TextBox>
                    </td>
                    <td class="TdLabel" style="width: 15%">甲方</td>
                    <td class='TdContent' style="width: 35%">
                        <asp:TextBox runat="server" empty="true" ID="jf"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">乙方</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="yf"></asp:TextBox>
                    </td>
                    <td class="TdLabel" style="width: 15%">合同内容</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="htnr"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">签订信息</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server"  ID="qdxx"></asp:TextBox>
                    </td>
                    <td class="TdLabel" style="width: 15%">合同开始时间</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" ID="htkssj" ReadOnly="true" class="Wdate" empty="true" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TdLabel" style="width: 15%">合同结束时间</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" ID="htjssj" ReadOnly="true" class="Wdate" empty="true" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                    <td class="TdLabel" style="width: 15%">合同金额(万元)</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="htje" double="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    
                    <td class="TdLabel" style="width: 15%">免费运维期</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="mfywq"></asp:TextBox>
                    </td>

                    <td class="TdLabel" style="width: 15%">项目进度</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server"  ID="xmjd"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>按需结算</td>
                    <td class='TdContent'>
                        <div>
                             <%--<asp:RadioButtonList runat="server" ID="ProType" RepeatDirection="Horizontal" CellSpacing="20"  >
                                <asp:ListItem Value="软件平台" Text="软件平台" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="硬件平台" Text="硬件平台"></asp:ListItem>
                                 <asp:ListItem Value="服务运维" Text="服务运维"></asp:ListItem>
                            </asp:RadioButtonList>--%>
                            <asp:CheckBoxList runat="server" ID="jelx" RepeatDirection="Horizontal" CellSpacing="20"  >
                                <asp:ListItem Value="是" Text="是"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                
<%--                <tr>
                    <td class="TdLabel" style="width: 15%">合同付款方式</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="htfkfs"></asp:TextBox>
                    </td>
                    
                    <td class="TdLabel" style="width: 15%">支付条件</td>
                    <td class="TdContent">
                        <asp:TextBox runat="server" empty="true" ID="zftj"></asp:TextBox>
                    </td>
                </tr>--%>
                
            </table>

            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">合同付款方式</span></legend>
                <table id="tabledy" style="width: 100%; margin-top: 5px; margin-bottom: 5px" class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class='TdLabel' style="width: 30%; text-align: center">支付条件</td>
                        <td class='TdLabel' style="width: 60%; text-align: center">支付金额</td>
                        <td class='TdLabel' style="width: 10%">
                            <input id="addr" style="width: 50px;" class="fileBtn" value="添加" type="button" />
                        </td>
                    </tr>
                    <tr id="IssueRow" style="display: none">
                        <td node="zftj" class='TdContent' style="width: 30%; text-align: left">
                           <%-- <asp:TextBox runat="server" ID="ProGuid" ></asp:TextBox>--%>
                            <asp:TextBox runat="server" ID="zftj"></asp:TextBox>
                        </td>
                        <td node="zfje" class='TdContent' style="width: 60%; text-align: left">
                            <asp:TextBox  runat="server" ID="zfje" ></asp:TextBox>
                        </td>
                        <td class='TdContent' style="text-align: center; width: 10%">
                            <input style="width: 50px;" class="fileBtn" value="删除" type="button" />
                        </td>
                    </tr>
                </table>
            </fieldset>


            <fieldset style="margin-top: 5px">
                <legend><span id="file0" style="width: 150px; font-size: 13px; font-weight: bold">附件</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>

        </div>
        
        <asp:Button runat="server" OnClick="Page_SaveData" ID="SaveButton" Style="display: none" />
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>


<script type="text/javascript">

    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var pguid = "<%=proGuid%>";

    var rowsData = '<%=strIssueRect%>';

    $("#IssueRow").SetRowData({ data: rowsData, onRowDeletedCompleted: function () { } });

    var config = new Config();
    config.saveData(function () {
        var zbrqdate = $("#htkssj").val();
        var ggrqdate = $("#htjssj").val();

        var starttime = new Date(zbrqdate.replace(/\-/g, "\/"));
        var endtime = new Date(ggrqdate.replace(/\-/g, "\/"));

        if (endtime < starttime) {
            alert("请注意日期前后顺序！");
            return false;
        }

        var zbkzjstr = $("#htje").val();
        if (zbkzjstr.length > 10 || parseInt(zbkzjstr) == 0) {
            alert("请输入十位以下且大于0的纯数字！");
            return false;
        }


        //var zfjearr = $("input[name='zfje']");
        //alert(zfjearr.length);
        //for (var i = 0; i < zfjearr.length; i++) {
            
        //    var zbjestr = zfjearr[i].val();
        //    alert(zfjearr[i].val());
        //    if (zbjestr.length > 10 || parseInt(zbjestr) == 0) {
        //        alert("请输入十位以下且大于0的纯数字！");
        //        return false;
        //    }
        //}
        
        $("#IssueRow").getRowData({ hideInputId: 'IssueRowData', enCode: true });
       
    });

    $(function () {

        var cfg0 = [];
        cfg0.content = "filecontent0";
        cfg0.refGuid = "<%=ViewState["Guid"]%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_xmht";
        cfg0.title = "上传附件";

        $("#file0").uploadfile(cfg0);

        $('#addr').click(function () {
            $('#IssueRow').dynamicRow({
                onRowDeletedCompleted: function () { optdynamicRowNum(); }
            });
            optdynamicRowNum();
        });

     });

    var formCore = new FormCore();
    formCore.FormVildateLoad();



</script>

