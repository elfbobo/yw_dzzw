<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApp.Project.StartProject.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目申报-项目申报</title>
    <link href="../../Content/Form.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
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

                    <%--当前用户为管理员或者政府办用户并且提交状态的项目为提交时，显示通过和退回按钮 --%>
                    <%if (document["ProState"] == "提交" && (roleCheck.isAdmin() || roleCheck.isZfb()))
                      {%>
                    <a style="cursor: pointer" href="javascript:showSucessPro();">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/ico-48.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;通过</a>
                    <a style="cursor: pointer" href="javascript:showReturnPro();">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_333.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;退回</a>
                     <a style="cursor: pointer" href="javascript:showHoldPro();">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/stop.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;暂缓</a>

                     <a style="cursor: pointer" href="javascript:showMergePro();">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/application_double.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;整合</a>
                    <%} %>

                    <%if (isEditable)
                      { %>
                    <a style="cursor: pointer" href="Create.aspx?guid=<%=strGuid %>">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/edit.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;编辑</a>
                    <a style="cursor: pointer" id="del">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/cross.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;删除</a>
                    <%} %>

                    <a style="cursor: pointer" id="checkreturn" href="#pstable">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/img_333.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;查看评审记录</a>

                    <a style="cursor: pointer" href="List.aspx">
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
                        <%if(document["MoneySource"]=="其他"){ %>
                        :<%=document["MoneySourceDesc"] %>
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
                <legend><span style="width: 150px; font-size: 13px; font-weight: bold">项目绩效目标</span></legend>
                <div id="filecontent_jx" style="margin-top: 10px"></div>
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
                <td><%=dsReturnHistory.Tables[0].Rows[i]["ProAction"].ToString()=="申报"?"通过":dsReturnHistory.Tables[0].Rows[i]["ProAction"].ToString() %></td>
                <td><%=dsReturnHistory.Tables[0].Rows[i]["InfoData"].ToString() %></td>
                <td><%=dsReturnHistory.Tables[0].Rows[i]["CreateDate"].ToString() %></td>
                <td><a style="cursor:pointer" onclick="showPsFiles('<%=dsReturnHistory.Tables[0].Rows[i]["PsFile"].ToString() %>','<%=dsReturnHistory.Tables[0].Rows[i]["ProAction"].ToString() %>')">查看</a></td>
            </tr>
            <%} %>
            <%} %>
        </tbody>
    </table>


    <div id="dialogreturn" style="display: none;">
        <div style="margin-left:-10px">

            <div class="layui-form-item layui-form-text" style="margin-top: 10px;">
                <label class="layui-form-label">评审内容</label>
                <div class="layui-input-block">
                    <textarea placeholder="请输入内容" id="returninfo" class="layui-textarea"></textarea>
                </div>
            </div>

            <div class="layui-form-item layui-form-text" style="margin-top: 10px; margin-bottom: 5px;min-height:97px">
                <label class="layui-form-label">评审意见</label>
                <div class="layui-input-block">
                    <fieldset style="margin-top: 5px">
                        <legend><span id="fileps" style="width: 60px; height: 20px; display: block">上传</span></legend>
                        <div id="filecontentps" style="margin-top: 10px"></div>
                    </fieldset>
                </div>
            </div>
            <div style="direction: rtl">
                <button class="layui-btn layui-btn-primary" style="margin-right: 10px" onclick="layer.closeAll();">取消</button>
                <button class="layui-btn layui-btn-normal" style="margin-right: 10px" onclick="returnPro()">退回</button>
            </div>
        </div>
    </div>

     <div id="dialoghold" style="display: none;">
        <div style="margin-left:-10px">

            <div class="layui-form-item layui-form-text" style="margin-top: 10px;">
                <label class="layui-form-label">评审内容</label>
                <div class="layui-input-block">
                    <textarea placeholder="请输入内容" id="holdinfo" class="layui-textarea"></textarea>
                </div>
            </div>

            <div class="layui-form-item layui-form-text" style="margin-top: 10px; margin-bottom: 5px;min-height:97px">
                <label class="layui-form-label">评审意见</label>
                <div class="layui-input-block">
                    <fieldset style="margin-top: 5px">
                        <legend><span id="filepszh" style="width: 60px; height: 20px; display: block">上传</span></legend>
                        <div id="filecontentpszh" style="margin-top: 10px"></div>
                    </fieldset>
                </div>
            </div>
            <div style="direction: rtl">
                <button class="layui-btn layui-btn-primary" style="margin-right: 10px" onclick="layer.closeAll();">取消</button>
                <button class="layui-btn layui-btn-normal" style="margin-right: 10px" onclick="holdPro()">暂缓</button>
            </div>
        </div>
    </div>

    <div id="dialogmerge" style="display: none;">
        <div style="margin-left:-10px">

            <div class="layui-form-item layui-form-text" style="margin-top: 10px;">
                <label class="layui-form-label">评审内容</label>
                <div class="layui-input-block">
                    <textarea placeholder="请输入内容" id="mergeinfo" class="layui-textarea"></textarea>
                </div>
            </div>

            <div class="layui-form-item layui-form-text" style="margin-top: 10px; margin-bottom: 5px;min-height:97px">
                <label class="layui-form-label">评审意见</label>
                <div class="layui-input-block">
                    <fieldset style="margin-top: 5px">
                        <legend><span id="filemerge" style="width: 60px; height: 20px; display: block">上传</span></legend>
                        <div id="filecontentmerge" style="margin-top: 10px"></div>
                    </fieldset>
                </div>
            </div>
            <div style="direction: rtl">
                <button class="layui-btn layui-btn-primary" style="margin-right: 10px" onclick="layer.closeAll();">取消</button>
                <button class="layui-btn layui-btn-normal" style="margin-right: 10px" onclick="mergePro()">整合</button>
            </div>
        </div>
    </div>


    <div id="dialogsuccess" style="display: none;">
        <div style="margin-left:-10px">

            <div class="layui-form-item layui-form-text" style="margin-top: 10px;">
                <label class="layui-form-label">评审内容</label>
                <div class="layui-input-block">
                    <textarea placeholder="请输入内容" id="successinfo" class="layui-textarea"></textarea>
                </div>
            </div>

            <div class="layui-form-item">
                <label class="layui-form-label">评审后估算额</label>
                <div class="layui-input-block">
                  <input type="text" id="psgse" name="title" double="true"  placeholder="请输入金额" class="layui-input">
                </div>
              </div>


            <div class="layui-form-item layui-form-text" style="margin-top: 10px; margin-bottom: 5px;min-height:97px">
                <label class="layui-form-label">评审意见</label>
                <div class="layui-input-block">
                    <fieldset style="margin-top: 5px">
                        <legend><span id="filepstg" style="width: 60px; height: 20px; display: block">上传</span></legend>
                        <div id="filecontentpstg" style="margin-top: 10px"></div>
                    </fieldset>
                </div>
            </div>
            <div style="direction: rtl">
                <button class="layui-btn layui-btn-primary" style="margin-right: 10px" onclick="layer.closeAll();">取消</button>
                <button class="layui-btn layui-btn-normal" style="margin-right: 10px" onclick="successPro()">通过</button>
            </div>
        </div>
    </div>


    <div style="display: none" id="divreturn">
        <table class="layui-table" style="margin: 0px">
            <thead>
                <tr>
                    <th></th>
                    <th>退回意见</th>
                    <th>退回日期</th>
                </tr>
            </thead>
            <%for (int i = 0; i < dsReturnHistory.Tables[0].Rows.Count; i++)
              { %>
            <tr>
                <td style="width: 5%"><%=i+1 %></td>
                <td style="width: 65%"><%=System.Web.HttpUtility.UrlDecode(dsReturnHistory.Tables[0].Rows[i]["infoData"].ToString()) %></td>
                <td style="width: 30%"><%=dsReturnHistory.Tables[0].Rows[i]["createdate"].ToString() %></td>
            </tr>
            <%} %>
        </table>
    </div>

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

        var cfg_jx = [];
        cfg_jx.refGuid = "<%=strGuid%>";
        cfg_jx.type = "database";
        cfg_jx.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfg_jx.fileSign = "tz_Project_Jx";
        cfg_jx.view = true;
        $("#filecontent_jx").fileviewload(cfg_jx);

        var cfgps = [];
        cfgps.content = "filecontentps";
        cfgps.refGuid = "<%=returnGuid%>";
        cfgps.type = "database";
        //cfgps.iswj = "true";
        cfgps.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfgps.fileSign = "tz_Project_PsTh";
        cfgps.title = "上传";
       

        $("#fileps").uploadfile(cfgps);

        var cfgpstg = [];
        cfgpstg.content = "filecontentpstg";
        cfgpstg.refGuid = "<%=successGuid%>";
        cfgpstg.type = "database";
        //cfgpstg.iswj = "true";
        cfgpstg.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfgpstg.fileSign = "tz_Project_PsTg";
        cfgpstg.title = "上传";
       

        $("#filepstg").uploadfile(cfgpstg);

        var cfgpszh = [];
        cfgpszh.content = "filecontentpszh";
        cfgpszh.refGuid = "<%=holdGuid%>";
        cfgpszh.type = "database";
        //cfgpstg.iswj = "true";
        cfgpszh.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfgpszh.fileSign = "tz_Project_PsZh";
        cfgpszh.title = "上传";


        $("#filepszh").uploadfile(cfgpszh);


        var cfgpsmerge = [];
        cfgpsmerge.content = "filecontentmerge";
        cfgpsmerge.refGuid = "<%=mergeGuid%>";
        cfgpsmerge.type = "database";
        //cfgpstg.iswj = "true";
        cfgpsmerge.applicationPath = "<%=Yawei.Common.AppSupport.AppPath %>";
        cfgpsmerge.fileSign = "tz_Project_Merge";
        cfgpsmerge.title = "上传";


        $("#filemerge").uploadfile(cfgpsmerge);


        //$(".fileBtn").click(function () {
        //    //alert(this.tagName);
        //    //alert($(this).parent().children()[0].tagName);
        //    $(this).parent().children()[0].click();
        //});

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
            title: '查看退回依据', //不显示标题
            area: ['650px', '300px'],
            content: $('#divreturn')//捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响

        });

    }

    function showHoldPro() {
        
        layer.open({
            type: 1,
            title: '请填写暂缓原因', //不显示标题
            area: ['580px', '310px'],
            content: $('#dialoghold')//捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响

        });
        var fileobjArr = $(".swfupload");

        for (var i = 0; i < fileobjArr.length; i++) {
            var parentdiv = fileobjArr[i].parentElement;
            $(parentdiv.children[0]).attr("width", $(parentdiv).width() + 4);

        }
    }

    function showMergePro() {

        layer.open({
            type: 1,
            title: '请填写整合原因', //不显示标题
            area: ['580px', '310px'],
            content: $('#dialogmerge')//捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响

        });
        var fileobjArr = $(".swfupload");

        for (var i = 0; i < fileobjArr.length; i++) {
            var parentdiv = fileobjArr[i].parentElement;
            $(parentdiv.children[0]).attr("width", $(parentdiv).width() + 4);

        }
    }


    //退回
    var showReturnPro = function () {

        showDialog();
        return;
    }
    var showSucessPro = function () {
        layer.open({
            type: 1,
            title: '请填写审核依据', //不显示标题
            area: ['600px', '385px'],
            content: $('#dialogsuccess')//捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响

        });
        var fileobjArr = $(".swfupload");

        for (var i = 0; i < fileobjArr.length; i++) {
            var parentdiv = fileobjArr[i].parentElement;
            $(parentdiv.children[0]).attr("width", $(parentdiv).width() + 4);

        }
    }
    //弹出退回意见填写窗口
    function showDialog() {

        layer.open({
            type: 1,
            title: '请填写退回原因', //不显示标题
            area: ['580px', '310px'],
            content: $('#dialogreturn')//捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响

        });
        var fileobjArr = $(".swfupload");

        for (var i = 0; i < fileobjArr.length; i++) {
            var parentdiv = fileobjArr[i].parentElement;
            $(parentdiv.children[0]).attr("width", $(parentdiv).width() + 4);

        }
        //$('.dialog').css({ left: ($(window).width() - $(".dialog").width()) / 2, top: 50 }).slideDown();
    }

    function returnPro() {
       // if ($("#filecontentps").children().length <= 1) {
        //    alert("请上传评审意见附件！");
        //    return;
        //}
        $.ajax({
            type: 'post',
            url: 'WebFunction.ashx',
            data: { action: 'return', proguid: '<%=strGuid%>', returninfo: $("#returninfo").val(), returnguid: '<%=returnGuid%>', userguid: '<%=userGuid%>', depguid: '<%=depGuid%>' },
            success: function (e) {
               
                if (e.Data == "1") {
                    alert("退回成功！");
                    window.location.reload();
                }
                else {
                    alert("退回失败！");
                }
            }
        });

    }

    function holdPro() {
        //if ($("#filecontentpszh").children().length <= 1) {
        //    alert("请上传评审意见附件！");
        //    return;
        //}
        $.ajax({
            type: 'post',
            url: 'WebFunction.ashx',
            data: { action: 'hold', proguid: '<%=strGuid%>', returninfo: $("#holdinfo").val(), returnguid: '<%=holdGuid%>', userguid: '<%=userGuid%>', depguid: '<%=depGuid%>' },
            success: function (e) {

                if (e.Data == "1") {
                    alert("暂缓成功！");
                    window.location.reload();
                }
                else {
                    alert("暂缓失败！");
                }
            }
        });
    }

    function mergePro() {
        $.ajax({
            type: 'post',
            url: 'WebFunction.ashx',
            data: { action: 'merge', proguid: '<%=strGuid%>', returninfo: $("#mergeinfo").val(), returnguid: '<%=mergeGuid%>', userguid: '<%=userGuid%>', depguid: '<%=depGuid%>' },
            success: function (e) {

                if (e.Data == "1") {
                    alert("整合成功！");
                    window.location.reload();
                }
                else {
                    alert("整合失败！");
                }
            }
        });
    }

    function successPro() {
        //if ($("#filecontentpstg").children().length <= 1) {
        //    alert("请上传评审意见附件！");
        //    return;
        //}
        $.ajax({
            type: 'post',
            url: 'WebFunction.ashx',
            data: { action: 'success', proguid: '<%=strGuid%>',successgse:$("#psgse").val(), successinfo: $("#successinfo").val(), successguid: '<%=successGuid%>', userguid: '<%=userGuid%>', depguid: '<%=depGuid%>' },
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
        else if (proaction == "暂缓") {
            cfgPsView.fileSign = "tz_Project_PsZh";
        }
        else if (proaction == "整合") {
            cfgPsView.fileSign = "tz_Project_Merge";
        }
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

    $('input[double="true"]').keyup(function () {
        FloatOnly($(this).get(0));
    });
    function FloatOnly(oInput) {
        if ('' != oInput.value.replace(/\d{1,}\.{0,1}\d{0,}/, '')) {

            oInput.value = oInput.value.match(/\d{1,}\.{0,1}\d{0,}/) == null ? '' : oInput.value.match(/\d{1,}\.{0,1}\d{0,}/);

        }

        var strValue = oInput.value;

        if (strValue.toString().indexOf('.') != -1) {

            var index = strValue.toString().indexOf('.');

            oInput.value = strValue.toString().substring(0, index + 5);

        }
        else {

            oInput.value = strValue.toString();

        }
    }

</script>

