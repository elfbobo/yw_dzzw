<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.TZ_XMJS.tz_xmgz.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Content/Form.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../../Scripts/UpLoadFile.js" type="text/javascript"></script>
    <script src="../../js/layer/layer.js" type="text/javascript"></script>
    <link href="../../js/layui/css/layui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;当前位置：项目变更信息
                </div>
                <div class="MenuRight">
                    <%if(roleCheck.isAdmin()&&document["status"]=="1"){ %>
                    <a style="cursor: pointer" href="javascript:showSucessChange();">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/ico-48.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;通过</a>
                    <a style="cursor: pointer" href="javascript:showReturnChange();">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_333.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;退回</a>
                    <%} %>

                     <%if (roleCheck.isAdmin()){ %>
                    <a style="cursor: pointer;display:none" href="Create.aspx?guid=<%=document["guid"] %>&xmguid=<%=document["xmguid"] %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;编辑</a>
                    <a style="cursor: pointer" id="del">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;删除</a>
                    <%}
                       else if (roleCheck.isBm())
                       { if(document["status"]=="0"||document["status"]=="2"){%>
                     <a style="cursor: pointer" id="del">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;删除</a>
                    <%} }%>
                    <a style="cursor: pointer" href="List.aspx?xmguid=<%=document["xmguid"] %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/back.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;返回</a>



                </div>
            </div>

            <table class="table" cellpadding="0" cellspacing="0">
                <tr style="display:none">
                   

                    <td class='TdLabel' style="width: 15%">项目名称</td>
                    <td class='TdContent' style="width: 35%" colspan="3">
                        <%=document["dqjd"]%>
                    </td>
                </tr>
                <tr style="display:none">
                   

                    <td class='TdLabel' style="width: 15%">当前进度</td>
                    <td class='TdContent' style="width: 35%" colspan="3">
                        <%=document["dqjd"]%>
                    </td>
                </tr>
                <tr>
                    <td class='TdLabel' style="width: 15%">变更类型</td>
                    <td class='TdContent' style="width: 35%">
                        <%=document["bglx"]%>
                    </td>
                    <td class='TdLabel' style="width: 15%">变更时间</td>
                    <td class='TdContent' style="width: 35%">
                        <%=document["bgsj"]%>
                    </td>
                </tr>
                <tr>
                   

                    <td class='TdLabel' style="width: 15%">变更内容</td>
                    <td class='TdContent' style="width: 35%" colspan="3">
                       
                    
                    <%if (document["bglx"] == "建设方案" || document["bglx"] == "项目申报表" || document["bglx"] == "云资源使用申请表")
                      { %>
                    <span>历史版本：<a href="#" onclick="showfile()">点击查看</a></span>&nbsp&nbsp
                    <span>变更为：<a href="#" onclick="showhisfile()">点击查看</a></span>
                    <%}else{ %>
                     <%=document["bgnr"]%>
                    <%} %>
                        </td>
                </tr>
                <tr>
                   

                    <td class='TdLabel' style="width: 15%">变更原因</td>
                    <td class='TdContent' style="width: 35%" colspan="3">
                        <%=document["bgyy"]%>
                    </td>
                </tr>
                <tr>
                   

                    <td class='TdLabel' style="width: 15%">备注</td>
                    <td class='TdContent' style="width: 35%" colspan="3">
                        <%=document["beizhu"]%>
                    </td>
                </tr>

            </table>


            
        </div>
        <asp:Button runat="server" OnClick="Page_DeleteData" ID="DelButton" Style="display: none" />
           

    </form>
    <div id="file-container">
            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">附件列表</span></legend>
                <div id="filecontent0" style="margin-top: 10px"></div>
            </fieldset>
           </div>
    <div id="hisfile-container">
            <fieldset style="margin-top: 5px">
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">附件列表</span></legend>
                <div id="filecontent1" style="margin-top: 10px"></div>
            </fieldset>
           </div>



    <div id="dialogreturn" style="display: none;">
        <div style="margin-left:-10px">

            <div class="layui-form-item layui-form-text" style="margin-top: 10px;">
                <label class="layui-form-label">评审意见</label>
                <div class="layui-input-block">
                    <textarea placeholder="请输入内容" id="returninfo" class="layui-textarea"></textarea>
                </div>
            </div>

            <div style="direction: rtl">
                <button class="layui-btn layui-btn-primary" style="margin-right: 10px" onclick="layer.closeAll();">取消</button>
                <button class="layui-btn layui-btn-normal" style="margin-right: 10px" onclick="returnChange()">退回</button>
            </div>
        </div>
    </div>

     <div id="dialogsuccess" style="display: none;">
        <div style="margin-left:-10px">

            <div class="layui-form-item layui-form-text" style="margin-top: 10px;">
                <label class="layui-form-label">评审意见</label>
                <div class="layui-input-block">
                    <textarea placeholder="请输入内容" id="successinfo" class="layui-textarea"></textarea>
                </div>
            </div>

            <div style="direction: rtl">
                <button class="layui-btn layui-btn-primary" style="margin-right: 10px" onclick="layer.closeAll();">取消</button>
                <button class="layui-btn layui-btn-normal" style="margin-right: 10px" onclick="successChange()">通过</button>
            </div>
        </div>
    </div>


 

</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>
<script type="text/javascript">
    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';

    $(function () {

        var cfg0 = [];
        cfg0.refGuid = "<%=document["last_val"]%>";
        cfg0.type = "database";
        cfg0.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg0.fileSign = "<%=filesign%>";
        cfg0.view = true;
        $("#filecontent0").fileviewload(cfg0);

        var cfg1 = [];
        cfg1.refGuid = "<%=document["bgnr"]%>";
        cfg1.type = "database";
        cfg1.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg1.fileSign = "<%=filesign%>";
        cfg1.view = true;
        $("#filecontent1").fileviewload(cfg1);

        $("#file-container").hide();
        $("#hisfile-container").hide();

    });


    var showSucessChange = function () {
        layer.open({
            type: 1,
            title: '请填写审核依据', //不显示标题
            area: ['600px', '220px'],
            content: $('#dialogsuccess')//捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响

        });
    }

    var showReturnChange = function () {
        layer.open({
            type: 1,
            title: '请填写退回依据', //不显示标题
            area: ['600px', '220px'],
            content: $('#dialogreturn')//捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响

        });

    }

    var returnChange = function () {
        $.ajax({
            type: 'post',
            url: '../XMJSHandler.ashx',
            data: { action: 'changereturn', changeguid: '<%=strGuid%>', info: $("#returninfo").val(), userguid: '<%=userGuid%>', depguid: '<%=depGuid%>' },
            cache: false,
            async: false,
            dataType: "json",
            success: function (e) {

                if (e.Data == "1") {
                    alert("审核成功！");
                    window.location.reload();
                }
                else {
                    alert("审核失败！");
                }
            }
        });
    }

    var successChange = function () {
        $.ajax({
            type: 'post',
            url: '../XMJSHandler.ashx',
            data: { action: 'changesuccess', changeguid: '<%=strGuid%>', info: $("#successinfo").val(), userguid: '<%=userGuid%>', depguid: '<%=depGuid%>' },
            cache: false,
            async: false,
            dataType: "json",
            success: function (e) {
                if (e.Data == "1") {
                    alert("审核成功！");
                    window.location.reload();
                }
                else {
                    alert("审核失败！");
                }
            }
        });
    }


    function showfile() {
        layer.open({
            type: 1,
            title: '查看替换版本',
            skin: 'layui-layer-demo', //样式类名
            area: ['900px', '250px'],
            closeBtn: 1, //不显示关闭按钮
            anim: 2,
            shadeClose: true, //开启遮罩关闭
            content: $('#file-container')
        });

    }

    function showhisfile() {
        layer.open({
            type: 1,
            title: '查看历史版本',
            skin: 'layui-layer-demo', //样式类名
            area: ['900px', '250px'],
            closeBtn: 1, //不显示关闭按钮
            anim: 2,
            shadeClose: true, //开启遮罩关闭
            content: $('#hisfile-container')
        });
    }


    var config = new Config();
    config.deleteData();


</script>

