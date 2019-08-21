<%@ Page Title="" Language="C#" MasterPageFile="~/Support/Shared/Support.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Yawei.App.Support.TableEdit.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Content/Layout.css" rel="stylesheet" />
    <script src="../../Plugins/jquery-ztree/ztree-all.min.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery-ztree/themes/default/zTreeStyle.css" rel="stylesheet" />
    <script src="../../Plugins/jquery.dynamicrow.js" type="text/javascript"></script>
    <script src="jquery.grid.js" type="text/javascript"></script>
    <link href="grid_gray.css" rel="stylesheet" type="text/css" />
    <script src="../../Plugins/jquery-uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <link href="../../Plugins/jquery-uploadify/uploadify.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Plugins/jquery-ui/jquery-ui.min.js"></script>
    <link type="text/css" href="../../Plugins/jquery-ui/themes/ui-lightness/jquery-ui.min.css" rel="stylesheet" />
    <style type="text/css">
        #content span {
            font-weight: normal;
            font-size: 12px;
        }

        #content .DivBlueTop {
            table-layout: fixed;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
        }

        .dialog {
            border: #999 1px solid;
            border-radius: 6px;
            background-color: white;
            box-shadow: 0px 0px 7px #535658;
            position: absolute;
            left: 100px;
            top: 90px;
        }

            .dialog .head {
                border: #e5e5e5 solid 1px;
                border-top-left-radius: 5px;
                border-top-right-radius: 5px;
                background-attachment: scroll;
                background-origin: padding-box;
                background-clip: border-box;
                background-size: auto;
                height: 30px;
                background-color: rgb(243, 243, 243);
                line-height: 30px;
                text-align: right;
            }

            .dialog .title {
                float: left;
                line-height: 30px;
                font-weight: bold;
            }

        .ui-button-text-only .ui-button-text {
            padding: 2px 6px 2px 2px;
            cursor: pointer;
        }

        label {
            margin: 3px 3px 3px 3px;
            font-size: 12px;
        }

        .ui-state-default {
            background: white;
        }

        dl {
            border-bottom: 1px solid #EEE;
            padding: 6px 0;
            zoom: 1;
            overflow: hidden;
            margin-top: 0px;
        }

        dt {
            float: left;
            width: 54px;
            line-height: 22px;
            text-align: right;
            color: #ff4400;
            font-size: 12px;
            padding: 3px 3px 0 0;
            color: #e4393c;
        }

            dt a {
                color: #ff4400;
                font-size: 12px;
            }

        dd {
            padding: 3px 0 0 15px;
            zoom: 1;
            overflow: hidden;
        }

        em {
            float: left;
            font-size: 12px;
            height: 14px;
            margin: 2px 0;
            padding: 0 4px;
            font-style: normal;
        }

            em input {
                vertical-align: text-top;
            }

        .tdborder {
            border: 1px solid red;
        }

        #LayoutTable img {
            cursor: pointer;
            margin: 0 2px 0 2px;
        }

        .autorowref {
            color: red;
        }

        .selectDorpDown {
            border:1px solid lightblue;
            color:blue;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="border: 1px solid lightgray">
        <div class="DivGrayTop">
            <span>表单编辑器</span><div style="width: 50%; float: right; text-align: right">
                <input style="padding: .1em 0.5em; font-size: 14px" onclick="$('#slecttable').show();" type="button" value="模型选择" />
                <input style="padding: .1em 0.5em; font-size: 14px" onclick="requstShow()" type="button" value="参数设置" />
                <input onclick="$('#ListContent').siblings().slideUp();$('#ListContent').slideDown()" value="列表" style="padding: .1em 0.5em; font-size: 14px" type="button" />
                <input onclick="$('#LayoutContent').siblings().slideUp();$('#LayoutContent').slideDown()" style="padding: .1em 0.5em; font-size: 14px" value="布局" type="button" />

                <%-- <input onclick="$('#content').siblings().slideUp();$('#content').slideDown()" style="padding:.1em 0.5em" value="Setting" type="button" />--%>


                <input id="page" style="padding: .1em 0.5em; font-size: 14px" onclick="Save()" value="发布" type="button" />
            </div>
        </div>
        <div>
            <div id="content" style="padding-left: 2px; display: none">
            </div>
            <div id="ListContent" style="padding-left: 2px;">
                <div id="gridfun" style="display: table; border: 1px solid lightgray; margin: 5px; width: 98%">
                    <dl style="width: 60%; float: left">
                        <dt>列表功能</dt>
                        <dd><em>
                            <input type="checkbox" style="vertical-align: text-top" value="create" title="新建" /><label>新建</label></em>
                            <em>
                                <input value="delete" style="vertical-align: text-top" title="删除" type="checkbox" /><label>删除</label></em>
                            <em>
                                <input class="but_fun" onclick="uploadfun(this)" value="添加功能" style="padding: .1em 0.5em; font-size: 14px" type="button" /></em></dd>
                    </dl>
                    <div style="width: 40%; float: right; text-align: right; line-height: 35px">
                        <label for="sortSelect">设置排序</label><select id="sortSelect" style="padding: .1em 0.5em; font-size: 14px"></select>
                        <input type="button" onclick="$('#search').slideDown()" style="padding: .1em 0.5em; font-size: 14px" value="查询设置" />
                    </div>
                </div>

                <div id="gridhead" style="display: table; border: 1px solid lightgray; margin: 5px; width: 98%">
                    <dl>
                        <dt>表头选择</dt>
                        <dd id="dd"></dd>
                    </dl>

                </div>
                <div>

                    <div style="margin: 5px" id="grid"></div>

                </div>
            </div>
            <div id="LayoutContent" style="padding: 10px; display: none">
                <div id="laytoufun" style="display: table; border: 1px solid lightgray; width: 98%; margin-bottom: 5px">
                    <dl style="width: 50%; float: left">
                        <dt>功能</dt>
                        <dd id="lf"><em>
                            <input type="checkbox" value="create" title="保存" /><label>保存</label></em><em><input value="delete" title="删除" type="checkbox" /><label>删除</label></em><em><input value="edit" title="修改" type="checkbox" /><label>修改</label></em><em><input onclick="uploadfun(this)" class="but_fun" value="添加功能" style="padding: .1em 0.5em; font-size: 14px" type="button" /></em></dd>
                    </dl>

                    <div style="width: 48%; float: right; text-align: right; line-height: 35px">
                        <input onclick="$('#autoRows').show()" style="padding: .1em 0.5em; font-size: 14px" type="button" value="动态行" />
                        <input onclick="AddFile()" style="padding: .1em 0.5em; font-size: 14px" type="button" value="附件" /><%--<input onclick="    PreView()" style="padding:.1em 0.5em;font-size:14px"  type="button" value="预览" /><input onclick="Download()" style="padding:.1em 0.5em;font-size:14px"  type="button" value="下载" /><input onclick="upload()" style="padding:.1em 0.5em;font-size:14px"  type="button" value="上传" />--%>
                    </div>
                </div>
                
                <table id="LayoutTable" class="table" style="width: 98%; border: 1px solid #a3cdf3" cellpadding="0" cellspacing="0">
                </table>
            </div>

        </div>
        <div id="edit" class="dialog" style="width: 540px; height: 400px; position: fixed; display: none; top: 140px; z-index: 9999">
            <div class="head">
                <div style="text-align: right;">
                    <div class="title">设置控件</div>
                    <input type="button" value="确定" style="padding: .1em 0.5em; font-size: 14px" onclick="_close()" />
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="_close()" />
                </div>
            </div>
            <div id="_cont">
                <span style="margin: 8px 10px 5px 10px">控件选择:</span><input checked="checked" type="radio" id="text" name="control" value="text" /><label for="text">Text</label><input onclick="srcCheck(this)" id="select" name="control" value="select" type="radio" /><label for="select">Select</label><input onclick="    srcCheck(this)" id="Radio" name="control" value="Radio" type="radio" /><label for="Radio">Radio</label>
                <input onclick="srcCheck(this)" id="Checkbox" name="control" value="Checkbox" type="radio" /><label for="Checkbox">Checkbox</label><input id="Areatext" name="control" value="Areatext" type="radio" /><label for="Areatext">Areatext</label><input id="Hidden" name="control" value="Hidden" type="radio" /><label for="Hidden">Hidden</label>
            </div>
            <div id="_valut">
                <span style="margin: 5px 10px 5px 10px">验证选择:</span><input type="radio" id="int" name="validate" value="int" /><label for="int">整数</label><input type="radio" id="float" name="validate" value="float" /><label for="float">浮点</label><input type="radio" id="email" name="validate" value="email" /><label for="email">邮箱</label><input type="radio" id="phoene" name="validate" value="phoene" /><label for="phoene">电话</label><input type="radio" id="idcode" name="validate" value="idcode" /><label for="idcode">身份证</label>
                <input type="radio" id="null" name="isnull" value="null" /><label for="null">必填</label><input checked="checked" type="radio" id="notnull" name="isnull" value="notnull" /><label for="notnull">非必填</label>
            </div>
            <div><span style="margin: 5px 10px 5px 10px">默&nbsp;&nbsp;认&nbsp;值:</span><input type="text" id="tags" style="width: 65%; margin: 5px 5px 5px 0px; height: 22px" /></div>
            <div><span style="margin: 5px 10px 5px 10px">显示信息:</span><input type="text" id="title" style="width: 65%; margin: 5px 5px 5px 0px; height: 22px" /></div>
        </div>

        <div id="mapping" class="dialog" style="width: 540px; height: 370px; position: fixed; display: none; top: 150px; left: 400px; z-index: 999999">
            <div style="border-bottom: 1px dashed lightgray">
                <div class="title">设置选项</div>
                <div style="text-align: right;">
                    <input id="tog" onclick="_toggle()" type="button" value="自定义" style="padding: .1em 0.5em; font-size: 14px" />
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="SetMapping()" />
                </div>
            </div>
            <div id="tree" style="overflow-y: auto; height: 340px; width: 100%">
                <ul id="ztree" class="ztree"></ul>
            </div>
            <div id="set" style="display: none; overflow-y: auto; height: 340px; width: 100%">
                <table cellpadding="0" cellspacing="0" class="table">
                    <tr>
                        <td class="TdLabel" style="text-align: center">Value</td>
                        <td class="TdLabel" style="text-align: center">Text</td>
                        <td class="TdLabel">
                            <input type="button" onclick='$("#rows").dynamicRow({ });' value="添加" style="padding: .1em 0.5em; font-size: 14px" /></td>
                    </tr>
                    <tr id="rows" style="display: none;">
                        <td class="TdContent">
                            <input type="text" /></td>
                        <td class="TdContent">
                            <input type="text" /></td>
                        <td class="TdLabel">
                            <input type="button" value="删除" style="padding: .1em 0.5em; font-size: 14px" /></td>
                    </tr>

                </table>
            </div>
        </div>



        <div id="search" class="dialog" style="width: 750px; height: 430px; position: fixed; display: none; top: 170px; left: 400px">
            <div class="head">
                <div class="title">设置查询选项</div>
                <div style="text-align: right;">
                    <input onclick="ResetContorl()" type="button" value="设置控件" style="padding: .1em 0.5em; font-size: 14px" /><input onclick="    $('#search').slideUp()" type="button" value="确定" style="padding: .1em 0.5em; font-size: 14px" />
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="$('#search').slideUp()" />
                </div>
            </div>
            <div id="searchSelect" style="display: table; border: 1px solid lightgray; margin: 5px; width: 98%">
                <dl>
                    <dt>查询选择</dt>
                    <dd id="searchdd"></dd>
                </dl>

            </div>
            <table id="searchTable" style="border: 1px solid #a3cdf3; width: 98%; margin-left: 6px" cellpadding="0" cellspacing="0"></table>

        </div>


        <div id="align" class="dialog" style="width: 340px; height: 180px; position: absolute; display: none;">
            <div style="text-align: right;">
                <span style="float: left; font-size: 12px; margin-left: 15px; line-height: 20px"></span>
                <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="closeListSet()" />
            </div>
            <div style="height: 2px;"></div>
            <dl>
                <dt>文本对其</dt>
                <dd><em>
                    <input type="radio" checked="checked" name="align" id="left" onclick="leftRight(this)" value="left" /><label for="left">左对齐</label></em><em><input type="radio" onclick="leftRight(this)" name="align" id="center" value="center" /><label for="center">居中对齐</label></em><em><input type="radio" onclick="    leftRight(this)" name="align" id="right" value="right" /><label for="right">右对齐</label></em></dd>
            </dl>
            <dl>
                <dt>是否排序</dt>
                <dd><em>
                    <input type="radio" checked="checked" name="order" id="oy" value="y" /><label for="oy">是</label></em><em><input type="radio" name="order" id="on" value="n" /><label for="on">否</label></em></dd>
            </dl>
            <dl>
                <dt>是否拖拽</dt>
                <dd><em>
                    <input type="radio" checked="checked" name="drag" id="dy" value="y" /><label for="dy">是</label></em><em><input type="radio" name="drag" id="dn" value="n" /><label for="dn">否</label></em></dd>
            </dl>
            <dl>
                <dt>列表顺序</dt>
                <dd><em><a style="cursor: pointer" onclick="tdprev()"><<</a></em>  <em><a style="cursor: pointer" onclick="tdnext()">>></a></em></dd>
            </dl>
        </div>


        <div id="addControl" class="dialog" style="width: 580px; height: 400px; position: fixed; display: none; top: 170px">
            <div class="head">
                <div style="text-align: right;">
                    <div class="title">设置控件</div>
                    <input type="button" value="确定" style="padding: .1em 0.5em; font-size: 14px" onclick="addControlColse()" />
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="addControlColse()" />
                </div>
            </div>
            <div>
                <div id="addfiled" style="display: table; border: 1px solid lightgray; margin: 5px; width: 98%">
                    <dl>
                        <dt>表头选择</dt>
                        <dd id="fileddd"></dd>
                    </dl>

                </div>
            </div>
            <div id="addcont">
                <span style="margin: 8px 10px 5px 10px">控件选择:</span><input checked="checked" type="radio" name="Addcontrol" value="text" /><label>Text</label><input onclick="srcCheck2(this)" name="Addcontrol" value="select" type="radio" /><label>Select</label><input onclick="    srcCheck2(this)" name="Addcontrol" value="Radio" type="radio" /><label>Radio</label>
                <input onclick="srcCheck2(this)" name="Addcontrol" value="Checkbox" type="radio" /><label>Checkbox</label><input name="Addcontrol" value="Areatext" type="radio" /><label>Areatext</label><input name="Addcontrol" value="Hidden" type="radio" /><label>Hidden</label>
            </div>
            <div id="addvalut">
                <span style="margin: 5px 10px 5px 10px">验证选择:</span><input type="radio" name="addvalidate" value="int" /><label>整数</label><input type="radio" name="addvalidate" value="float" /><label>浮点</label><input type="radio" name="addvalidate" value="email" /><label>邮箱</label><input type="radio" name="addvalidate" value="phoene" /><label>电话</label><input type="radio" name="addvalidate" value="idcode" /><label>身份证</label>
                <input type="radio" name="addisnull" value="null" /><label>必填</label><input checked="checked" type="radio" name="addisnull" value="notnull" /><label>非必填</label>
            </div>
            <div><span style="margin: 5px 10px 5px 10px">默&nbsp;&nbsp;认&nbsp;值:</span><input id="addtags" style="width: 65%; margin: 5px 5px 5px 0px; height: 22px" /></div>
            <%--<div ><span style="margin:5px 10px 5px 10px">显示信息:</span><input id="title"  style="width:65%;margin:5px 5px 5px 0px;height:22px" /></div>--%>
        </div>

        <div id="fileset" class="dialog" style="width: 540px; height: 370px; position: fixed; display: none; top: 180px; left: 400px; z-index: 2000">
            <div style="border-bottom: 1px dashed lightgray">
                <div class="title">设置附件</div>
                <div style="text-align: right;">
                    <input type="button" value="确定" style="padding: .1em 0.5em; font-size: 14px" onclick="CannleFile()" /><img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="CannleFile()" />
                </div>
            </div>

            <div id="" style="overflow-y: auto; height: 340px; width: 100%">
                <table cellpadding="0" cellspacing="0" class="table">
                    <tr>
                        <td class="TdLabel" style="text-align: center">显示信息</td>
                        <td class="TdLabel" style="text-align: center">附件标识</td>
                        <td class="TdLabel">
                            <input type="button" onclick='$("#filerows").dynamicRow({ });' value="添加" style="padding: .1em 0.5em; font-size: 14px" /></td>
                    </tr>
                    <tr id="filerows" style="display: none;">
                        <td class="TdContent">
                            <input type="text" /></td>
                        <td class="TdContent">
                            <input type="text" /></td>
                        <td class="TdLabel">
                            <input type="button" value="删除" style="padding: .1em 0.5em; font-size: 14px" /></td>
                    </tr>

                </table>
            </div>
        </div>

        <div id="slecttable" class="dialog" style="width: 640px; height: 400px; position: fixed; top: 180px; left: 400px; z-index: 2000">
            <div class="head">
                <div class="title">选择数据模型</div>
                <div style="text-align: right;">
                    <input type="button" value="确定" style="padding: .1em 0.5em; font-size: 14px" />
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="$('#slecttable').slideUp()" />
                </div>
            </div>

        </div>
        <div id="uploadfun" class="dialog" style="width: 640px; height: 400px; position: fixed; top: 180px; left: 400px; z-index: 2000; display: none">
            <div class="head">
                <div class="title">添加功能</div>
                <div style="text-align: right;">
                    <input onclick="_confm()" type="button" value="确定" style="padding: .1em 0.5em; font-size: 14px" />
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="$('#uploadfun').slideUp()" />
                </div>
            </div>
            <div style="border: 1px solid lightgray">
                <table class="table" style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr style="display: none">
                        <td class="TdLabel">页面</td>
                        <td class="TdContent">
                            <select id="pagename">
                                <option>View</option>
                                <option>Create</option>
                            </select></td>
                    </tr>
                    <tr>
                        <td class="TdLabel">中文名</td>
                        <td class="TdContent">
                            <input type="text" id="funcnName" /></td>
                    </tr>
                    <tr>
                        <td class="TdLabel">英文名</td>
                        <td class="TdContent">
                            <input type="text" id="funenName" /></td>
                    </tr>
                    <tr>
                        <td class="TdLabel">图片名</td>
                        <td class="TdContent">
                            <input type="text" id="imgName" value="tongyong.png" /></td>
                    </tr>
                </table>

            </div>

        </div>

        <div id="autoRows" class="dialog" style="width: 750px; height: 400px; position: fixed; top: 180px; left: 400px; z-index: 2000; display: none">
            <div class="head">
                <div class="title">动态行</div>
                <div style="text-align: right;">
                    <input onclick="rowsConfirm()" type="button" value="确定" style="padding: .1em 0.5em; font-size: 14px" />
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="$('#autoRows').slideUp()" />
                </div>
            </div>
            <div style="border: 1px solid lightgray; display: table; width: 100%; min-height: 360px">
                <div style="display: table-cell; width: 32%; border-right: 1px solid lightgray; vertical-align: top">
                    <ul id="rowztree" class="ztree"></ul>
                </div>
                <div style="display: table-cell; width: 70%">
                    <div>
                        <dl>
                            <dt>选择字段</dt>
                            <dd id="rowFiled"></dd>
                        </dl>
                    </div>
                    <div style="margin-bottom:5px">
                        <label style="color:#ff4400;font-size:12px">动态行字段:</label>
                        <div id="rowSelect">

                        </div>
                    </div>
                    
                    <div style="margin-bottom:5px"><label style="color:#ff4400;font-size:12px">关&nbsp;联&nbsp;字&nbsp;段：</label><select id="rowsRef"></select></div>
                    <div style="margin-bottom:5px"><label style="color:#ff4400;font-size:12px">排&nbsp;序&nbsp;字&nbsp;段：</label><select id="rowsOrder"></select></div>
                    <div style="margin-bottom:5px"><label style="color:#ff4400;font-size:12px">附&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件：</label><select id="rowsFile"><option value="否">否</option><option value="是">是</option></select></div>
                </div>

            </div>

        </div>



        <div id="requst" class="dialog" style="width: 640px; height: 400px; position: fixed; top: 180px; left: 400px; z-index: 2000; display: none">
            <div class="head">
                <div class="title">参数设置</div>
                <div style="text-align: right;">
                    <input onclick="$('#requst').slideUp()" type="button" value="确定" style="padding: .1em 0.5em; font-size: 14px" />
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="$('#requst').slideUp()" />
                </div>
            </div>
            <div>
                <table class="table" style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="TdLabel">参数类型</td>
                        <td class="TdLabel">参数方式</td>
                        <td class="TdLabel">参数值</td>
                        <td class="TdLabel">
                            <input type="button" onclick='$("#pamas").dynamicRow({ });' value="添加" style="padding: .1em 0.5em; font-size: 14px" /></td>
                    </tr>
                    <tr id="pamas" style="display: none">
                        <td class="TdContent">
                            <select>
                                <option value="Send">传参</option>
                                <option value="Receive">接参</option>

                            </select>

                        </td>
                        <td class="TdContent">
                            <select>
                                <option></option>
                                <option value="List">列表</option>
                                <option value="Create">新建</option>
                                <option value="Save">保存</option>
                                <option value="Edit">修改</option>
                                <option value="Delete">删除</option>
                                <option value="All">通用</option>
                            </select></td>
                        <td class="TdContent">
                            <input type="text" /></td>
                        <td class="TdLabel">
                            <input type="button" value="删除" style="padding: .1em 0.5em; font-size: 14px;" /></td>
                    </tr>
                </table>
                <div>
                    <br />
                    参数值说明:<br />
                    传参:&nbsp;&nbsp;字段&nbsp;  Field&Name  &nbsp;&nbsp;&nbsp;    接收的参数&nbsp;Receive&ProjGuid  &nbsp;&nbsp;&nbsp;  常量&nbsp;Const&Type&1&nbsp;&nbsp;&nbsp;
                    <br />
                    接参:直接填写接收参数的名称（ProjGuid）
                </div>
            </div>
        </div>
    </div>

     <div id="win" style="width:300px;height:450px;border:lightgray 1px solid;background-color:white;position:absolute;display:none">
             <ul style="width:300px;" class="ztree"  id="pathtree"></ul>
             <img title="关闭" onclick="$('#win').hide();" style="position:absolute;right:3px;top:0px;cursor:pointer" src="img/cross.png" />      
        </div>




    <div id="rowsmapping" class="dialog" style="width: 540px; height: 370px; position: fixed; display: none; top: 150px; left: 400px; z-index: 999999">
            <div style="border-bottom: 1px dashed lightgray">
                <div class="title">设置选项</div>
                <div style="text-align: right;">
                    <img src="img/cross.png" style="height: 13px; width: 13px; margin-right: 5px; margin-top: 3px; cursor: pointer;" onclick="$('#rowsmapping').hide()" />
                </div>
            </div>
            <div  style="overflow-y: auto; height: 340px; width: 100%">
                <ul id="rowsmappingztree" class="ztree"></ul>
            </div>
        </div>


    <asp:Button runat="server" ID="Create" OnClick="Create_Click" Style="display: none" />
    <input type="hidden" id="tablename" name="tablename" value="" />
    <input type="hidden" id="tabletitle" name="tabletitle" value="" />
     <input id="pathPage" name="pathPage" type="hidden" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" src="PageCreate.js"></script>
    <script src="tableedit.js" type="text/javascript"></script>
    <script src="SavePage.js" type="text/javascript"></script>
    <script type="text/javascript">

        //var isChrome = navigator.userAgent.toLowerCase().match(/chrome/) != null
        //if(isChrome)
        //{
        //    $("link[href*='Contents/Site.css']").remove();
        //}

        var listSetting=eval('<%=listSetting%>');
        var editSetting=eval('<%=editSetting%>');

        var ztree,usertree,funtree;
        var current,mapping;
        var tableJson=<%=tablejson%>;
       var mappingJson=<%=mappingJson%>;
      
        var tableedit=new tableEdit();
        var modelJson=<%=modelJson%>;
       var pk;
       var tabelName="<%=tabelName%>";
        var tableTitle="<%=tableTitle%>";
       var treeJson=<%=treeJson%>;
        $(function(){
           <%=jquery%>

         
           if($(window).width()<1100)
           {
               $(".dialog").each(function(){
                   var wd=$(this).width();
                   var ht=$(this).height();
                   $(this).css({left:($(window).width()-wd)/2});
               });
           }
           // tableedit.loadFildDiv(tableJson,$("#content"));
           var availableTags = ["当前时间","当前用户主键","当前用户名","当前用户DN"];
           $("#tags,#addtags").autocomplete({
               source: availableTags
           });
           $("div[id*='_']").buttonset();


           $("#mapping").css({top:($(window).height()-$("#mapping").height())/2,left:($(window).width()-$("#mapping").width()+100)/2});
          
           var cl=[];
           $.each(tableJson,function(i,dat){
               var check=""
               
               if(dat.FieldPK=="是")
               {
                   pk=dat;
                   check="";
                   $("#sortSelect").append("<option pk>"+dat.FieldName+"</option>");
               }
               else
               {
                   if(i<6)
                   {
                       check=" checked='checked' ";
                       if(dat.FieldPK!="是")
                           cl.push(dat);
                   }
                   $("#dd").append("<em><input "+check+" type='checkbox' style='vertical-align:text-top' id='"+dat.FieldName+"' value='"+JSON.stringify(dat)+"' name='c' /><label for='"+dat.FieldName+"'>"+dat.FieldDesc+"</label></em>");
                   $("#searchdd").append("<em><input "+check+" type='checkbox' style='vertical-align:text-top' id='_"+dat.FieldName+"' value='"+JSON.stringify(dat)+"' name='_c' /><label for='_"+dat.FieldName+"'>"+dat.FieldDesc+"</label></em>");
                   $("#fileddd").append("<em><input  type='radio' style='vertical-align:text-top' id='"+dat.FieldName+"_' value='"+JSON.stringify(dat)+"' name='c_' /><label for='"+dat.FieldName+"_'>"+dat.FieldDesc+"</label></em>");
                   $("#ffdd").append("<em><input checked='checked' "+check+" type='checkbox' style='vertical-align:text-top' id='"+dat.FieldName+"' value='"+JSON.stringify(dat)+"' name='ac' /><label for='"+dat.FieldName+"'>"+dat.FieldDesc+"</label></em>");
                   $("#sortSelect").append("<option>"+dat.FieldName+"</option>");
               }
               
           });
           tableedit.SetList(cl,$("#grid"));
           //$("#column").buttonset();
           $("#dd :input[type='checkbox']").click(function(){
               var checed= $("#gridhead :checked");
               if(checed.length<1)
                   return false;
               var newcl=[];
               checed.each(function(i){
                   newcl.push(JSON.parse( $(this).val()));
               });
               tableedit.SetList(newcl,$("#grid"));
           });
          

           $("#searchTable").append(searchTable(cl));
           $("#searchdd :input[type='checkbox']").click(function(){checkSearch();});
           bindSearchTabel();//slecttable
           // $("#sortSelect").selectmenu();
           $.each(modelJson,function(i,dat){
               var htmldd="<dl style='margin-left:10px'>";
               htmldd+="<dt>"+dat.name+"</dt>";
               if(dat.children)
               {
                   htmldd+="<dd>"
                   $.each(dat.children,function(j,child){
                       if(child.f)
                       {
                           htmldd+="<em style=\"cursor:pointer;color:#1c94c4\" onclick='ssbit(\""+child.table+"\",\""+child.name+"\")' title='"+child.table+"'><label>"+child.name+"</lable></em>";
                       }
                       else
                        htmldd+="<em style=\"cursor:pointer\" onclick='ssbit(\""+child.table+"\",\""+child.name+"\")' title='"+child.table+"'><label>"+child.name+"</lable></em>";
                   });
                   htmldd+="</dd>";
               }
               htmldd+="</dl>";
              
               $("#slecttable").append(htmldd);
           });
       });
       function ssbit(tablename,tabletitle)
       {
           $("#tablename").val(tablename);
           $("#tabletitle").val(tabletitle);
           $("form").submit();
       }




        
       function srcCheck(obj)
       {
           var offset= $("#edit").offset();
           $("#mapping").css({left:offset.left,top:offset.top+35});
           mapping=$(obj);
           $("#mapping").show();
           $("#rows").nextAll().remove();
           $("#tog").val('自定义');
           var nodes = ztree.getSelectedNodes();
           if(nodes.length>0)
               ztree.cancelSelectedNode(nodes[0]);
           var data=JSON.parse("["+current.attr("data")+"]");
           var dat=data[0];
           if (dat.control == 'select' || dat.control == 'Radio' || dat.control == 'Checkbox')
           {
               if (dat.Mapping)
               { 
                   $("#tree").show();
                   $("#set").hide();
                   var nd = ztree.getNodeByTId(dat.Mapping);
                   ztree.selectNode(nd);
                   $("#tree").show();
                   $("#set").hide();
               }
               if (dat.Custom)
               {
                   $.each(dat.Custom,function(i,ct){
                       $("#rows").dynamicRow({ });
                       var tr=  $("#rows").parent().parent().parent().find("tr").last();
                       tr.find(":text").eq(0).val(ct.value);
                       tr.find(":text").eq(1).val(ct.text);

                   });
                   $("#tree").hide();
                   $("#set").show();  
               }
           }

         
          
       }

       function srcCheck2(obj)
       {
           var offset= $("#addControl").offset();
           $("#mapping").css({left:offset.left,top:offset.top+35});
           mapping=$(obj);
           $("#mapping").show();
           $("#rows").nextAll().remove();
           $("#tog").val('自定义');
           var nodes = ztree.getSelectedNodes();
           if(nodes.length>0)
               ztree.cancelSelectedNode(nodes[0]);
           $("#tree").show();
           $("#set").hide();
          
       }

       function _toggle()
       {
           if($("#tog").val()=="自定义")
           {
               $("#tree").hide();
               var nodes = ztree.getSelectedNodes();
               if(nodes.length>0)
                   ztree.cancelSelectedNode(nodes[0]);
               $("#set").show();
               $("#tog").val('字典');
           }
           else
           {
               $("#tree").show();
               $("#set").hide();
               $("#rows").nextAll().remove();
               $("#tog").val('自定义');
           }
       }

       
       var setting = {
      
           callback: {

               onClick: onClickTree,

           }
       };
      
       var rowsSetting={
           callback: {
               onClick: onClickTreeRow,
           }
       };
       var rowsmappingSetting={
           callback: {
               onClick: onClickTreeRowMapping,
           }
       };


       var autoRowTree,mappingztree;
       $(function () {
           ztree = $.fn.zTree.init($("#ztree"), setting, mappingJson);
           autoRowTree=$.fn.zTree.init($("#rowztree"), rowsSetting, treeJson);
           tableedit.setEditLayout();
           
           mappingztree = $.fn.zTree.init($("#rowsmappingztree"), rowsmappingSetting, mappingJson);
       })

       function onClickTreeRowMapping(e, treeId, treeNode)
       {
           var json=JSON.parse($(rowsmappingObj).attr("v"));
          
           //$(span).addClass("selectDorpDown");
           json.Mapping=treeNode.id;
           json.dropdown="true";
           $(rowsmappingObj).addClass("selectDorpDown");
           $(rowsmappingObj).attr("v",JSON.stringify(json));
           $('#rowsmapping').hide();
       }

       function onClickTree(e, treeId, treeNode){}

       function onClickTreeRow(e, treeId, treeNode) {
          
           if(!treeNode.isParent)
           {
               $.ajax({
                   url:"Handler.ashx",
                   type:"post",
                   data:{tableName:treeNode.id},
                   success: function (data) {//rowFiled
                       $("#rowFiled").children().remove();
                       $("#rowSelect").children().remove();
                       $("#rowsRef").children().remove();
                       $("#rowsOrder").children().remove();
                       $("#rowsFile").val('否');
                       var filed=eval(data);
                       $.each(filed,function(i,dat){
                           $("#rowFiled").append("<em><input type='checkbox' onclick='selectRows(this)' style='vertical-align:text-bottom' id='"+dat.FieldName+"' value='"+JSON.stringify(dat)+"' name='_c' /><label for='"+dat.FieldName+"'>"+dat.FieldDesc+"</label></em>");
                           $("#rowsRef").append("<option value='"+dat.FieldName+"'>"+dat.FieldName+"--"+dat.FieldDesc+"</option>");
                           $("#rowsOrder").append("<option value='"+dat.FieldName+"'>"+dat.FieldName+"--"+dat.FieldDesc+"</option>");
                       });
                      
                   },
                   error: function ()
                   { alert("请求数据失败"); }
               });
               
              
           }
       }

       function selectRows(obj)
       {
           if($(obj).prop("checked"))
           {
               //alert($(obj).attr("id")+'   '+$(obj).prop("checked")+'  '+$("label[for='"+$(obj).attr("id")+"']").text());
               $("#rowSelect").append("<span onclick='selectRowsDropdown(this)' style='margin-right:10px;font-size:12px' f='"+$(obj).attr("id")+"' v='"+$(obj).val()+"'>"+$("#rowFiled label[for='"+$(obj).attr("id")+"']").text()+"</span>");
           }
           else
           {
               $("#rowSelect span[f='"+$(obj).attr("id")+"']").remove();
           }
       }
       var rowsmappingObj;
       function selectRowsDropdown(span)
       {
           rowsmappingObj=span;
           var json=JSON.parse($(rowsmappingObj).attr("v"));
           if($(span).attr("class")=="selectDorpDown")
           {
               $(span).removeClass("selectDorpDown");
               json.dropdown="false";
               $(rowsmappingObj).attr("v",JSON.stringify(json));
           }
           else
           {   
               if(json.Mapping)
               {
                   var rnd= mappingztree.getNodeByTId(json.Mapping);
                   mappingztree.selectNode(rnd);
               }
               $("#rowsmapping").show();
           }
          
       }


       function rowsConfirm()
       {
           if($("#rowSelect").children().length>0)
           {
               var ns= autoRowTree.getSelectedNodes();
               var rowsRef=$("#rowsRef").val();
               var rowsOrder=$("#rowsOrder").val();
               var json='{tableName:"'+ns[0].id+'",file:"'+$("#rowsFile").val()+'",colspan:2,';
               json+='rowsRef:"'+rowsRef+'",rowsOrder:"'+rowsOrder+'",column:[';
               $("#rowSelect").find("span").each(function(i){
                   if(i>0)
                       json+=",";
                   json+=$(this).attr("v");
               });
               json+="]}";
               //document.write(json);              
               addAutoRows(json,$("#rowSelect").html(),ns[0].id)
               $("#autoRows").hide();
           }
       }

       var tdInx;
       function setList(obj){
           tdInx=$(obj).parent().parent();
           var offset=tdInx.offset();
           $("#align").find("span").text($(obj).parent().text());
           var data=tdInx.attr("data");
           $("input[name='align'][value='"+ data.aglin+"']").prop("checked",true);
           $("input[name='drag'][value='"+ data.drag+"']").prop("checked",true);
           $("input[name='order'][value='"+ data.order+"']").prop("checked",true);
          
           if(offset.left<$(window).width()/2)
           {
               $("#align").css({left:offset.left,top:offset.top+32}).show();
           }
           else
           {
               offset=$(obj).offset();
               $("#align").css({left:offset.left-$("#align").width()+20,top:offset.top+28}).show();
           }
       }

       function leftRight(obj)
       {
           var index=tdInx.index();
           var align=$(obj).val();
         
           $(".tableCls").find("tr").each(function(){
               $(this).find("td").eq(index).css("text-align",align);
           });
       }

       function closeListSet()
       {
           var dat=eval("["+tdInx.attr("data")+"]");
           var data=dat[0];
           data.aglin=$("input[name='align']:checked").val();
           data.drag=$("input[name='drag']:checked").val();
           data.order=$("input[name='order']:checked").val();
           tdInx.attr("data",JSON.stringify(data));
           $('#align').hide();
       }


       function tdprev()
       {
          
           var idx=tdInx.index();
           var cl=[];
           if(idx>1)
           {
               var feild=tdInx.find("img[f]").eq(0).attr("f");
               var head= $(".tableHeader");
               for(var i=0;i<head.length;i++)
               {
                   if(i>0)
                   {
                       if(i+1==idx)
                       {
                           cl.push({feild:$(head[i+1]).find("img[f]").eq(0).attr("f"),width:$(head[i+1]).width()});
                           cl.push({feild:$(head[i]).find("img[f]").eq(0).attr("f"),width:$(head[i]).width()});
                           i++;
                       }
                       else
                       {
                           cl.push({feild:$(head[i]).find("img[f]").eq(0).attr("f"),width:$(head[i]).width()});
                       }
                   }
               }
               var cm=[];
               $.each(cl,function(i,dat){
                   cm.push(JSON.parse( $("input[id='"+dat.feild+"']").val()));
               });
               tableedit.SetList(cm,$("#grid"));
               $("#align").hide();
           }
          
       }


       function tdnext()
       {
           var head= $(".tableHeader");
           var idx=tdInx.index();
           var cl=[];
           if(idx<head.length-1)
           {
               var feild=tdInx.find("img[f]").eq(0).attr("f");
             
               for(var i=0;i<head.length;i++)
               {
                   if(i>0)
                   {
                       if(i==idx)
                       {
                           cl.push({feild:$(head[i+1]).find("img[f]").eq(0).attr("f"),width:$(head[i+1]).width()});
                           cl.push({feild:$(head[i]).find("img[f]").eq(0).attr("f"),width:$(head[i]).width()});
                           i++;
                       }
                       else
                       {
                           cl.push({feild:$(head[i]).find("img[f]").eq(0).attr("f"),width:$(head[i]).width()});
                       }
                   }
               }
               var cm=[];
               $.each(cl,function(i,dat){
                   cm.push(JSON.parse( $("input[id='"+dat.feild+"']").val()));
               });
               tableedit.SetList(cm,$("#grid"));
               $("#align").hide();
           }
          
       }

       function _confm(){
           if($("#funcnName").val()!=""&&$("#funenName").val()!="")
           {
               var ty='';
               if(funtype.parent().attr("id"))
                   ty='p="'+$("#pagename").val()+'"';
               funtype.before('<em><input img="'+$("#imgName").val()+'" '+ty+' type="checkbox" style="vertical-align:text-top"  value="'+$("#funenName").val()+'" title="'+$("#funcnName").val()+'" /><label>'+$("#funcnName").val()+'</label></em>');
           }
           $("#uploadfun").hide();
       }

       function ResetContorl()
       {
           current=$("#searchTable .tdborder");//.find("select,input").not("select[search]").eq(0);
           if(current.length>0)
           {
               $("#title").val(current.find("span").eq(0).text());
               $("#edit").css({ left: ($(window).width() - $("#edit").width()) / 2 }).slideDown();
           }
           else
           {
               alert("没有要编辑的控件");
           }
       }

       function requstShow()
       {
           $("#requst").css({ left: ($(window).width() - $("#requst").width()) / 2 }).slideDown();
       }
    </script>
    <script type="text/javascript" src="LoadPage.js"></script>
</asp:Content>
