<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.Project.StartProject.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                </div>
                
                <div class="MenuRight">
                    <%if (isEditable){ %>
                    <a style="cursor: pointer" href="Create.aspx?guid=<%=strGuid %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;编辑</a>
                    <a style="cursor: pointer" id="del" >
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;删除</a>
                    <%} %>
                    <%if (hasReturnHistory){ %>
                    <a style="cursor: pointer" id="checkreturn" onclick="viewReturnHistory()">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_333.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查看退回记录</a>
                    <%} %>
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
                    <td class='TdContent'>
                        <%=document["ProSummary"] %>
                    </td>
                    <td class='TdLabel'>批复金额(万元)</td>
                    <td class='TdContent'>
                        <%=document["Quota"] %>
                    </td>
                </tr>
               <tr>
                    <td class='TdLabel'>资金来源</td>
                    <td class='TdContent' >
                         <%=document["MoneySource"] %>
                    </td>
                    <td class='TdLabel'>是否新开工项目</td>
                    <td class='TdContent' >
                         <%=document["IsNewStart"].ToString()=="1"?"是":"否" %>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel'>申报部门</td>
                    <td class='TdContent'>
                        <%=document["StartDeptName"] %>
                    </td>
                    <td class='TdLabel'>项目类型</td>
                    <td class='TdContent'>
                        <%=document["ProType"] %>
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
                    <td class='TdLabel'>申报时间</td>
                    <td class='TdContent'>
                        <%=document["StartDate"] %>
                    </td>
                    <td class='TdLabel'>是否部署云平台</td>
                    <td class='TdContent'>
                        <%=document["IsInCloudPlat"].ToString()=="1"?"是":"否"  %>
                    </td>
                </tr>

          
            </table>
             <fieldset style="margin-top:5px">
                    <legend><span style="width:150px;font-size:13px; font-weight:bold">附件</span></legend>
                      <div id="filecontent0" style="margin-top:10px"></div>
            </fieldset>

        </div>
        <asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton" Style="display: none" />

    </form>
   
    <div style="display:none" id="divreturn">
        <table class="layui-table" style="margin:0px" >
            <thead>
                 <tr>
                    <th></th>
                    <th>退回意见</th>
                    <th>退回日期</th>
                </tr>
            </thead>
            <%for(int i=0;i<dsReturnHistory.Tables[0].Rows.Count;i++){ %>
            <tr>
                <td style="width:5%"><%=i+1 %></td>
                <td style="width:65%"><%=System.Web.HttpUtility.UrlDecode(dsReturnHistory.Tables[0].Rows[i]["infoData"].ToString()) %></td>
                <td style="width:30%"><%=dsReturnHistory.Tables[0].Rows[i]["createdate"].ToString() %></td>
            </tr>
            <%} %>
        </table>
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
        cfg0.fileSign = "tz_Project";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

    });

    var config = new Config();
    config.deleteData();

    //function delData() {
    //    if (confirm("你确定要删除么？")) {
    //        $('#DelButton').click();
    //    }
    //}

    function viewReturnHistory() {
        layer.open({
            type: 1,
            title: '查看退回意见', //不显示标题
            area: ['650px', '300px'],
            content: $('#divreturn'), //捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响

        });
    }
</script>
