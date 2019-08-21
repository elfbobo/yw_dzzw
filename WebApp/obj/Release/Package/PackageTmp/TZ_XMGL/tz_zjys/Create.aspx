<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebApp.TZ_XMGL.tz_zjys.Create" %>


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
    <script src="../../Plugins/jquery.dynamicrow.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center">
            <div class="FormMenu">
                <div class="MenuLeft">
                    &nbsp;&nbsp;<img src="<%=Yawei.Common.AppSupport.AppPath %>/images/home_ico.gif" width="16px" height="13px" align="absmiddle" />
                    &nbsp;&nbsp;当前位置：<a href="List.aspx?ProjGuid=<%=proGuid %>">资金预算信息</a> >> 编辑资金预算信息
                </div>
                <div class="MenuRight">
                   
                    <a style="cursor: pointer" id="save">
                        <img src="<%=Yawei.Common.AppSupport.AppPath %>/images/disk.png" style="vertical-align: text-bottom; height: 13px; width: 13px" />&nbsp;保存</a>

                </div>
            </div>
            <table style="width: 100%" class="table" cellpadding="0" cellspacing="0">
                <tr>

                    <td class="TdLabel" style="width: 15%">预算额(万元)</td>
                    <td class="TdContent" style="width: 35%">
                        <asp:TextBox runat="server" empty="true" ID="yse" double="true"></asp:TextBox>
                    </td>
                    <td class="TdLabel" style="width: 15%"></td>
                    <td class='TdContent' style="width: 35%">

                    </td>
                </tr>
                
            </table>
            <table style="width: 100%; line-height: 35px;" class="table" id="rowstable">
                <tr>
                        <td style="width: 0px;display:none"></td>
                        <td style="width: 30%; text-align: center" class="TdLabel">工程或费用名称</td>
                        <td style="width: 20%; text-align: center" class="TdLabel">投资额(万元)</td>
                        <td style="width: 40%; text-align: center" class="TdLabel">备注</td>
                       
                        <td style="width: 10%; text-align: center" class="TdLabel"><a id="addrow" class="fileBtn">添加</a></td>
                    </tr>
                    <tr id="rows" style="display: none">
                        <td style="width: 0px;display:none" node="refguid"><input type="text" style="text-align: center"/></td>                        
                        <td style="width: 30%; text-align: center" class="TdContent" node="fymc"><input type="text" style="text-align: center"/></td>
                        <td style="width: 20%; text-align: center" class="TdContent" node="tze"><input type="text" style="text-align: center" double="true"/></td>
                        <td style="width: 40%; text-align: center" class="TdContent" node="bz"><input type="text" style="text-align: center"/></td>
                        <td style="width: 10%; text-align: center" class="TdContent"><a name="but" class="fileBtn">删除</a></td>

                    </tr>
            </table>

        </div>
        
        <asp:Button runat="server" OnClick="Page_SaveData" ID="SaveButton" Style="display: none" />
        <input type="hidden" id="RowData" name="RowData" />
    </form>
</body>
</html>
<script src="<%=Yawei.Common.AppSupport.AppPath %>/Scripts/config.js" type="text/javascript"></script>


<script type="text/javascript">

    var appPath = '<%=Yawei.Common.AppSupport.AppPath%>';
    var pguid = "<%=proGuid%>";
    var rowsData = '<%=datarow%>';

    $(function () {

        $('#addrow').click(function () {//添加
            $('#rows').dynamicRow({});//动态行添加新行
        });

    });

    //动态行赋值
    $("#rows").SetRowData({ data: rowsData, self: false });//修改时为动态行赋值


    var formCore = new FormCore();
    formCore.FormVildateLoad();

    var config = new Config();
    //config.saveData();
    config.saveData(function () {
        $("#rows").getRowData({ hideInputId: 'UnitRowData', enCode: true });//获取动态行数据
    });

</script>
