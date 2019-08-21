<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMView.aspx.cs" Inherits="WebApp.TZ_XMYW.XMView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目申报-项目申报</title>
    <link href="../Content/Form.css" rel="stylesheet" />
    <script src="../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/UpLoadFile.js" type="text/javascript"></script>
    <script src="../Scripts/config.js" type="text/javascript"></script>
    <script src="../js/layer/layer.js" type="text/javascript"></script>
    <link href="../js/layui/css/layui.css" rel="stylesheet" />
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
                     <a style="cursor: pointer" href="create.aspx?xmguid=<%=strProGuid %>&proname=<%=document["ProName"]%>&type=1">
                                <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/add.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;新建</a>
                    <a style="cursor: pointer" href="XMList.aspx">
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
            </table>

          

        </div>

    </form>
    <fieldset class="layui-elem-field layui-field-title">
  <legend style="text-align:center">运维记录</legend>
</fieldset>
    <table class="layui-table" id="pstable">
        <colgroup>
            <col width="150">
            <col width="400">
            <col width="200">
            <col width="200">
        </colgroup>
        <thead>
            <tr>
                <th>序号</th>
                <th>运维年度</th>
                <th>备注</th>
                <th>运维报告</th>
            </tr>
        </thead>
        <tbody>
            <%if (dsYw==null||dsYw.Tables[0].Rows.Count==0)
              { %>
            <td colspan="5" style="text-align: center">当前项目暂无评审记录</td>
            <%}
              else
              { %>
            <%for (int i = 0; i < dsYw.Tables[0].Rows.Count; i++)
              { %>
            <tr>
                <td><%=i+1 %></td>
                <td><%=dsYw.Tables[0].Rows[i]["Year"].ToString() %></td>
                <td><%=dsYw.Tables[0].Rows[i]["beizhu"].ToString() %></td>
                <td><a style="cursor:pointer" onclick="showYwFiles('<%=dsYw.Tables[0].Rows[i]["guid"].ToString() %>')">查看</a></td>
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
    
    function showYwFiles(psfiles) {
        $("#filecontentPsView").empty();
        var cfgPsView = [];
        cfgPsView.refGuid = psfiles;
        cfgPsView.type = "database";
        cfgPsView.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfgPsView.fileSign = "tz_xmyw";
        

        cfgPsView.view = true;
        cfgPsView.title = "运维报告"
        $("#filecontentPsView").fileviewload(cfgPsView);

        //自定页
        layer.open({
            type: 1,
            title: '运维报告',
            skin: 'layui-layer-demo', //样式类名
            closeBtn: 1, //不显示关闭按钮
            anim: 2,
            shadeClose: true, //开启遮罩关闭
            content: $('#psFilesDiv')
        });
    }
</script>
