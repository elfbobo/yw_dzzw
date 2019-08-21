<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.Project.StartProjectJs.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=10"/>
    <title>项目申报-项目申报</title>
    <link href="../../Content/Form.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>
    <script src="../../Scripts/config.js" type="text/javascript"></script>
    <script src="../../js/layer/layer.js" type="text/javascript"></script>
    <link href="../../js/layui/css/layui.css" rel="stylesheet" />
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

                    <a style="cursor: pointer" id="checkreturn"  onclick="ScrollToPs()">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_333.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查看评审记录</a>

                    <a style="cursor: pointer" onclick="closeThisTab()">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>
                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td class='TdLabel'>项目名称</td>
                    <td class='TdContent' colspan="3">
                        <%=document["ProName"] %>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>项目简介</td>
                    <td class='TdContent' colspan="3">
                        <%=document["ProSummary"] %>
                    </td>

                </tr>
                <tr>
                    <td class='TdLabel'>预估金额(万元)</td>
                    <td class='TdContent'>
                        <%=document["Quota"] %>
                    </td>
                    <td class='TdLabel'>资金来源</td>
                    <td class='TdContent'>
                        <%=document["MoneySource"] %>
                        <%if(document["MoneySource"]=="其他") {%>
                        ：<%=document["MoneySourceDesc"] %>
                        <%} %>
                    </td>

                </tr>

                <tr>
                    <td class='TdLabel'>项目属性</td>
                    <td class='TdContent'>
                        <%=document["ProProperty"] %>
                    </td>
                    <td class='TdLabel'>项目类型</td>
                    <td class='TdContent'>
                        <%=document["ProType"] %>
                    </td>

                </tr>

                <tr>
                    <td class='TdLabel'>申报部门</td>
                    <td class='TdContent'>
                        <%=document["StartDeptName"] %>
                    </td>
                    <td class='TdLabel'>申报时间</td>
                    <td class='TdContent'>
                        <%=document["StartDate"] %>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>联系人</td>
                    <td class='TdContent'>
                        <%=document["ContactName"] %>
                    </td>
                    <td class='TdLabel'>联系人电话</td>
                    <td class='TdContent'>
                        <%=document["ContactTel"] %>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>是否部署云平台</td>
                    <td class='TdContent' colspan="3">
                        <%=document["IsInCloudPlat"].ToString()=="1"?"是":"否"  %>
                    </td>

                </tr>


            </table>

            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">建设方案</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>

            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">项目申报表</span></legend>
                <div id="filecontent2" style="margin-top: 10px"></div>
            </fieldset>

            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">云资源使用申请表</span></legend>
                <div id="filecontent3" style="margin-top: 10px"></div>
            </fieldset>

            <fieldset style="margin-top: 5px;display:none">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">其他附件</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>

        </div>
        <asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton" Style="display: none" />

    </form>
    <fieldset class="layui-elem-field layui-field-title">
  <legend style="text-align:center">评审记录</legend>
</fieldset>
    <table class="layui-table" id="pstable" style="margin-bottom:35px">
        <colgroup>
            <col width="150">
            <col width="400">
            <col width="200">
            <col width="200">
        </colgroup>
        <thead>
            <tr>
                <th>序号</th>
                <th>评审类型</th>
                <th>评审内容</th>
                <th>评审日期</th>
                <th>查看评审意见</th>
            </tr>
        </thead>
        <tbody>
            <%if (!hasReturnHistory)
              { %>
            <td colspan="5" style="text-align: center">当前项目暂无评审记录</td>
            <%}
              else
              { %>
            <%for(int i=0;i<dsReturnHistory.Tables[0].Rows.Count;i++){ %>
            <tr>
                <td><%=i+1 %></td>
                <td><%=dsReturnHistory.Tables[0].Rows[i]["ProAction"].ToString() %></td>
                <td><%=dsReturnHistory.Tables[0].Rows[i]["InfoData"].ToString() %></td>
                <td><%=dsReturnHistory.Tables[0].Rows[i]["CreateDate"].ToString() %></td>
                <td><a style="cursor:pointer" onclick="showPsFiles('<%=dsReturnHistory.Tables[0].Rows[i]["PsFile"].ToString() %>','<%=dsReturnHistory.Tables[0].Rows[i]["ProAction"].ToString() %>')">查看</a></td>
            </tr>
            <%} %>
            <%} %>
        </tbody>
    </table>


    <div id="psFilesDiv" style="display:none">
        <div id="filecontentPsView" style="margin-top: 10px"></div>
    </div>
</body>
</html>

<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var guid = '<%=strGuid%>';
    var table = 'tz_Project';

    $(function () {

        var cfg0 = [];
        cfg0.refGuid = "<%=strGuid%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "tz_Project_Fj";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

        var cfg1 = [];
        cfg1.refGuid = "<%=strGuid%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "tz_Project_Fa";
        cfg1.view = true;
        $("#filecontent1").fileviewload(cfg1);

        var cfg2 = [];
        cfg2.refGuid = "<%=strGuid%>";
        cfg2.type = "database";
        cfg2.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg2.fileSign = "tz_Project_Sbb";
        cfg2.view = true;
        $("#filecontent2").fileviewload(cfg2);

        var cfg3 = [];
        cfg3.refGuid = "<%=strGuid%>";
        cfg3.type = "database";
        cfg3.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg3.fileSign = "tz_Project_Yzy";
        cfg3.view = true;
        $("#filecontent3").fileviewload(cfg3);
    });

    var config = new Config();
    config.deleteData();

    function ScrollToPs() {
        document.body.scrollTop = document.body.scrollHeight;
    }

    function closeThisTab() {
        window.parent.parent.tabsClose();

    }


    function showPsFiles(psfiles,proaction) {
        $("#filecontentPsView").empty();
        var cfgPsView = [];
        cfgPsView.refGuid = psfiles;
        cfgPsView.type = "database";
        cfgPsView.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        if (proaction == "退回") {
            cfgPsView.fileSign = "tz_Project_PsTh";
        }
        //通过
        else {
            cfgPsView.fileSign = "tz_Project_PsTg";
        }
        
        cfgPsView.view = true;
        cfgPsView.title="评审意见"
        $("#filecontentPsView").fileviewload(cfgPsView);

        //自定页
        layer.open({
            type: 1,
            title:'评审意见',
            skin: 'layui-layer-demo', //样式类名
            closeBtn: 1, //不显示关闭按钮
            anim: 2,
            shadeClose: true, //开启遮罩关闭
            content: $('#psFilesDiv')
        });
    }

</script>
