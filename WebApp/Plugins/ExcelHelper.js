//===============================================================================================================================
//
// Excel 文档控制
//
// 使用提示
//
// 无需创建对象新对象，使用方法列表中的方法既可以控制，脚步中定义了xlObject（ActiveXObject），xlWork（工作簿对象）
// xlSheet（工作表对象），特殊功能可使用这三个对象控制。
//
// 方法列表
//
// InitExcel()											_初始化Excel文档
//
// OpenExcel(FileName)									_打开一个Excel模版，FileName是文件路径
//
// SaveAsExcel(FileName)								_保存一个Excel文档，FileName是保存路径，并作为字符串返回
//
// SetVisible(IsShow)									_设置前台或后台显示
//
// SetPage(Orientation)									_设置页面打印方式，1为纵向，2为横向
//
// SetFormat(AreaStr,FontName,FontSize,FontBold,FontItalic,HorAlignment,VerAlignment,Wrap)
//
//														_设置格式，支持单元格、列、区域
//
// SetMergeCells(AreaStr)								_合并单元格，仅支持区域
//
// SetColumnWidth(AreaStr,ColumnValue)					_设置列宽，支持单元格，列，区域，全部(区域参数为"all")
//
// SetRowHeight(AreaStr,HeightValue)					_设置行高，支持单元格，列，区域，全部(区域参数为"all")
//
// SetValue(AreaStr,AreaValue)							_设置区域的值，支持单元格，区域
//
// SetValueFormatToString(AreaStr)						_转化区域的输出类型为字符串型，支持单元格，列，区域，全部(区域参数为"all")
//
// SetValueFormatToCustom(AreaStr,regix)				_转化区域的输出类型为自定义型，支持单元格，列，区域，全部(区域参数为"all")
//
// SetBorder(AreaStr)									_设置区域的边线，支持单元格，列，区域
//
// ShowGrids(IsShow)									_设置Excel网格线
//
// SetUserControl(IsControl)							_控制权交给用户
//
// ClearExcel()											_清空对象
//
// QuitExcel()											_退出Excel
//
// PrintPreviewExcel()									_打印预览Excel
//
// PrintOutExcel()										_打印Excel
//
//===============================================================================================================================
//
// 定义了Excel操作对象
//-----------------------------------------------




function ExcelHelper(option) {

    var defaults = {
        columns: [],
        applicationPath: "",
        tableName: "",
        conName: "",
        tableWhere: '',
        filed: '',
        key:"",
        staticData: [],
        head: '未导出数据或导出过程中出现错误'
    };

    var options = $.extend(defaults, option);

    var code = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"];


    var xlObject = null;
    var xlWork = null;
    var xlSheet = null;

    this.NewExcel = function () {
        InitExcel();
        SetVisible(true);
        SetPage(1);
        var count = GetColumnsCount(options.columns[0]);
        if (count > 26)
            ResetCode(count);
        SetHead(count);

        $.each(code, function (i, dat) {
            SetColumnWidth(dat, 20);
        });

    }


    function SetHead(count) {
        SetMergeCells("A1:" + code[count - 1] + "1");
        SetValue("A1:" + code[count - 1] + "1", options.head);
        SetFormat("A1:" + code[count - 1] + "1", "", null, 17, true, 3, null, true);
        SetRowHeight("A1:" + code[count - 1] + "1", 20);
        var rows = [];
        $.each(options.columns, function (i, data) {
            var cl = []; var c = 0;
            $.each(data, function (j, dat) {
                if (dat.rowspan) {

                    SetMergeCells(code[j + c] + (i + 2) + ":" + code[j + c] + (i + 2 + parseInt(dat.rowspan) - 1));
                    SetValue(code[j + c] + (i + 2) + ":" + code[j + c] + (i + 2 + parseInt(dat.rowspan) - 1), dat.title);
                    SetFormat(code[j + c] + (i + 2) + ":" + code[j + c] + (i + 2 + parseInt(dat.rowspan) - 1), "", null, false, false, 3, null, true);
                    rows.push({ rows: i, span: dat.rowspan, col: j + c });
                }
                else if (dat.colspan) {

                    $.each(rows, function (m, da) {
                        if (j + c == da.col) {
                            if (i < da.span + da.rows)
                            { c += 1; }
                        }
                    });
                    SetMergeCells(code[j + c] + (i + 2) + ":" + code[j + c + parseInt(dat.colspan) - 1] + (i + 2));
                    SetValue(code[j + c] + (i + 2) + ":" + code[j + c + parseInt(dat.colspan) - 1] + (i + 2), dat.title);
                    SetFormat(code[j + c] + (i + 2) + ":" + code[j + c + parseInt(dat.colspan) - 1] + (i + 2), "", null, false, false, 3, null, true);
                    cl.push({ cls: j + c, count: dat.colspan });
                    c += dat.colspan - 1;
                }
                else {

                    $.each(rows, function (m, da) {
                        if (j + c == da.col) {

                            if (i < da.span + da.rows)
                            { c += 1; }
                        }
                    });

                    SetFormat(code[j + c] + (i + 2) + ":" + code[j + c] + (i + 2), "", null, false, false, 3, null, true);
                    SetValue(code[j + c] + (i + 2) + ":" + code[j + c] + (i + 2), dat.title);
                }
            });
        });
    }
    //[['a','b'],[]]
    this.ExportStaticData = function () {
        var rows = option.columns.length + 1;
        $.each(options.staticData, function (i, dat) {
            $.each(dat, function (j) {
                SetValue(code[j] + (rows + i + 1) + ":" + code[j] + (rows + i + 1), dat[j]);
            });
            
        });
    }
   

    this.ExportData = function () {
        var rows = option.columns.length + 1;
        var url = options.applicationPath + "/Handlers/ExcelHandler.ashx";
        if (options.tableName == "V_ProjAssessment" || options.tableName == "V_ProjDepartAvg") {
            url = options.applicationPath + "Handlers/ExcelHandler.ashx";
        }
        $.ajax({         
            url: url,
            type: "post",
            data: { table: options.tableName, where: options.tableWhere, conName: options.conName, type: "getdata",key:option.key },
            async: false,
            success: function (data) {
         
                var filedArr = option.filed.split(',');
                $.each(eval(data), function (j, dat) {
                    for (var i = 0; i < filedArr.length; i++)
                    {
                        for (key in dat)
                        {
                            if (key.toLowerCase() == filedArr[i].toLowerCase())
                            {
                                SetValue(code[i] + (rows + j + 1) + ":" + code[i] + (rows + j + 1), dat[key]);
                            }
                        }
                    }
                     
                });
                
            },
            error: function (msg) { alert("导出数据失败，失败状态"+msg.status); }
        });
    }


    this.LoadImprotData=function (id,calback) {
        var cfg = [];
        cfg.content = "excelcontent";
        cfg.type = "server";
        cfg.applicationPath = options.applicationPath;
        cfg.fileSign = "Execl导入";
        cfg.title = "导入";
        cfg.fileTypeExts = "*.xls;*.xlsx";
        //cfg.view = true;
        //上传后执行的方法 file.name 附件名称 file.guid 上传成功后附件主键 file.size 附件的大小 file.type 附件的扩展名 file.FileNewName  附件新名字(一般是以Guid命名的)
        cfg.success = function (file) {
            $.ajax({
                url: options.applicationPath+"/Handlers/ExcelHandler.ashx",
                type: "post",
                async: false,
                data: { type: "inport", fileName: file.FileNewName, Guid: file.guid, filed: option.filed, rows: option.columns.length, table: option.tableName, conName: option.conName,key:option.key },
                success: function (data) {
                    if (data == "1")
                        alert("导入成功");
                    if (data == "0")
                        alert("导入失败");
                    if (calback)
                        calback();
                },
                error: function (msg) { alert("导入数据失败，失败原因:" + msg.status); }
            });
        }

        cfg.progress = false;
        $("#"+id).uploadfile(cfg);
    }

    function GetColumnsCount(data) {
        var count = 0;
        $.each(data, function (i, dat) {
            if (dat.colspan)
                count += parseInt(dat.colspan);
            else
                count += 1;
        });
        return count;
    }

    function ResetCode(count) {
        count = count - 26;
        for (var i = 1; i < count; i++) {
            code[code.length] = code[0] + code[i - 1];
        }
    }


    //===============================================================================================================================
    //
    // 用于检测范围字符串的类型
    // Cells:表示单元格;Columns:表示列;Range:表示区域
    //-----------------------------------------------

    function Check(AreaStr) {
        var CellsPatrn = /^\d+[,]{1}\d+$/;
        var ColPatrn = /^([a-zA-Z]+[:]{1}[a-zA-Z]+)|([a-zA-Z]+)$/;
        var RangePatrn = /^[a-zA-Z]+\d+[:]{1}[a-zA-Z]+\d+$/;
        var AreaRet = "";
        if (CellsPatrn.exec(AreaStr)) {
            AreaRet = "Cells";
        }
        if (ColPatrn.exec(AreaStr)) {
            AreaRet = "Columns";
        }
        if (RangePatrn.exec(AreaStr)) {
            AreaRet = "Range";
        }
        if (AreaStr == "all") {
            AreaRet = "all";
        }

        return AreaRet;
    }

    //===============================================================================================================================
    //
    // 用于转换表示单元格的字符串
    // Cells:表示单元格;Columns:表示列;Range:表示区域
    //-----------------------------------------------
    function CellsFormat(CellsStr) {
        var st = CellsStr.split(',');

        if (st.length == 2) {
            st[0] = parseInt(st[0]);
            st[1] = parseInt(st[1]);
        }

        return st;
    }


    //===============================================================================================================================
    //
    // 初始化一个Excel对象
    //-----------------------------------------------

    function InitExcel() {
        try {
            xlObject = new ActiveXObject("Excel.Application");
        }
        catch (ex) {
            alert("Excel对象无法创建，请开启浏览器的ActiveX控键限制，并安装Excel2003！");
        }

        xlWork = xlObject.Workbooks.Add();
        xlSheet = xlWork.Worksheets(1);
    }

    //===============================================================================================================================
    //
    // 打开一个Excel模版
    // FileName：标示文件路径
    //-----------------------------------------------

    function OpenExcel(FileName) {
        try {
            if (FileName != "") {
                xlObject = new ActiveXObject("Excel.Application");
                xlWork = xlObject.Workbooks.Open(FileName);
                xlSheet = xlWork.Worksheets(1);
            }
            else {
                alert("Excel文件路径错误");
            }
        }
        catch (ex) {
            alert("Excel对象无法创建，请开启浏览器的ActiveX控键限制，并安装Excel2003！");
        }
    }

    //===============================================================================================================================
    //
    // 保存一个Excel模版
    // FileName：标示文件路径
    //-----------------------------------------------

    function SaveAsExcel(FileName) {
        var filename = "";
        try {
            if (FileName != "") {
                xlWork.SaveAs(FileName);
                xlWork.Close(savechanges = false);
                xlObject.Visible = false;
                xlObject.Quit;
                filename = FileName;
            }
            else {
                alert("保存路径发生错误");
                filename = ""
            }
        }
        catch (ex) {
            alert("Excel文件路径错误");
        }
        return filename;
    }

    //===============================================================================================================================
    //
    // 设置Excel前台运行和后台运行
    //-----------------------------------------------

    function SetVisible(IsShow) {
        if (IsShow) {
            xlObject.visible = true;
        }
        else {
            xlObject.visible = false;
        }
    }

    //===============================================================================================================================
    //
    // 设置页面边距,Orientation：1表示纵向，2表示横向
    //-----------------------------------------------

    function SetPage(Orientation) {
        if (Orientation == 2) {
            with (xlSheet.PageSetup) {
                Orientation = 2;																	//横向
                LeftMargin = xlObject.InchesToPoints(0.748031496062992);
                RightMargin = xlObject.InchesToPoints(0.748031496062992);
                TopMargin = xlObject.InchesToPoints(0.984251968503937);
                BottomMargin = xlObject.InchesToPoints(0.984251968503937);
                HeaderMargin = xlObject.InchesToPoints(0.511811023622047);
                FooterMargin = xlObject.InchesToPoints(0.511811023622047);
            }
        }
        else if (Orientation == 1) {
            with (xlSheet.PageSetup) {
                Orientation = 1;																	//纵向
                LeftMargin = xlObject.InchesToPoints(0.590551181102362);
                RightMargin = xlObject.InchesToPoints(0.590551181102362);
                TopMargin = xlObject.InchesToPoints(0.393700787401575);
                BottomMargin = xlObject.InchesToPoints(0.393700787401575);
                HeaderMargin = xlObject.InchesToPoints(0.511811023622047);
                FooterMargin = xlObject.InchesToPoints(0.393700787401575);
            }
        }
    }

    //===============================================================================================================================
    //
    // 单元格设置字体
    // AreaStr：表示范围的字符串
    // FontName：字符串型，字体名称
    // FontSize：整型，字体大小
    // FontBold：Bool型，True表示加粗
    // FontItalic：Bool型，True表示斜体
    // HorAlignment：整型，1是常规，2 是左，3 中，4 右
    // VerAlignment：整型，1 上，2 中，3 下
    // Wrap：Bool型，True表示自动换行
    //-----------------------------------------------

    function SetFormat(AreaStr, FontName, FontSize, FontBold, FontItalic, HorAlignment, VerAlignment, Wrap) {
        var CheckStr = Check(AreaStr);

        if (CheckStr != "") {
            switch (CheckStr) {
                case "Cells":
                    var Cell = CellsFormat(AreaStr);

                    if (FontName != "") {
                        xlSheet.Cells(Cell[0], Cell[1]).Font.Name = FontName;
                    }
                    if (FontSize != null) {
                        xlSheet.Cells(Cell[0], Cell[1]).Font.Size = FontSize;
                    }
                    if (FontBold != false) {
                        xlSheet.Cells(Cell[0], Cell[1]).Font.Bold = true;
                    }
                    if (FontItalic != false) {
                        xlSheet.Cells(Cell[0], Cell[1]).Font.Italic = true;
                    }
                    if (HorAlignment != null) {
                        xlSheet.Cells(Cell[0], Cell[1]).HorizontalAlignment = HorAlignment;
                    }
                    if (VerAlignment != null) {
                        xlSheet.Cells(Cell[0], Cell[1]).VerticalAlignment = VerAlignment;
                    }
                    if (Wrap != false) {
                        xlSheet.Cells(Cell[0], Cell[1]).WrapText = true;
                    }
                    break;


                case "Columns":
                    if (FontName != "") {
                        xlSheet.Columns(AreaStr).Font.Name = FontName;
                    }
                    if (FontSize != null) {
                        xlSheet.Columns(AreaStr).Font.Size = FontSize;
                    }
                    if (FontBold != false) {
                        xlSheet.Columns(AreaStr).Font.Bold = true;
                    }
                    if (FontItalic != false) {
                        xlSheet.Columns(AreaStr).Font.Italic = true;
                    }
                    if (HorAlignment != null) {
                        xlSheet.Columns(AreaStr).HorizontalAlignment = HorAlignment;
                    }
                    if (VerAlignment != null) {
                        xlSheet.Columns(AreaStr).VerticalAlignment = VerAlignment;
                    }
                    if (Wrap != false) {
                        xlSheet.Columns(AreaStr).WrapText = true;
                    }
                    break;


                case "Range":
                    if (FontName != "") {
                        xlSheet.Range(AreaStr).Font.Name = FontName;
                    }
                    if (FontSize != null) {
                        xlSheet.Range(AreaStr).Font.Size = FontSize;
                    }
                    if (FontBold != false) {
                        xlSheet.Range(AreaStr).Font.Bold = true;
                    }
                    if (FontItalic != false) {
                        xlSheet.Range(AreaStr).Font.Italic = true;
                    }
                    if (HorAlignment != null) {
                        xlSheet.Range(AreaStr).HorizontalAlignment = HorAlignment;
                    }
                    if (VerAlignment != null) {
                        xlSheet.Range(AreaStr).VerticalAlignment = VerAlignment;
                    }
                    if (Wrap != false) {
                        xlSheet.Range(AreaStr).WrapText = true;
                    }
                    break;
            }
        }
    }

    //===============================================================================================================================
    //
    // 合并单元格
    // AreaStr：表示范围的字符串
    //-----------------------------------------------

    function SetMergeCells(AreaStr) {
        var CheckStr = Check(AreaStr);

        if (CheckStr != "") {
            if (CheckStr == "Range") {
                xlSheet.range(AreaStr).MergeCells = true;
            }
        }
    }

    //===============================================================================================================================
    //
    // 设置列宽
    // AreaStr：设置范围，如："A:D"
    // ColumnValue：列宽值
    //-----------------------------------------------

    function SetColumnWidth(AreaStr, ColumnValue) {
        var CheckStr = Check(AreaStr);

        if (CheckStr != "") {
            switch (CheckStr) {
                case "Cells":
                    var Cell = CellsFormat(AreaStr);

                    xlSheet.Cells(Cell[0], Cell[1]).ColumnWidth = ColumnValue;
                    break;


                case "Columns":
                    xlSheet.Columns(AreaStr).ColumnWidth = ColumnValue;
                    break;


                case "Range":
                    xlSheet.range(AreaStr).ColumnWidth = ColumnValue;
                    break;

                case "all":
                    xlSheet.Cells.ColumnWidth = ColumnValue;
                    break;
            }
        }
    }

    //===============================================================================================================================
    //
    // 设置行高
    // AreaStr：范围标示
    // HeightValue：行高值
    //-----------------------------------------------

    function SetRowHeight(AreaStr, HeightValue) {
        var CheckStr = Check(AreaStr);

        if (CheckStr != "") {
            switch (CheckStr) {
                case "Cells":
                    var Cell = CellsFormat(AreaStr);
                    xlSheet.Cells(Cell[0], Cell[1]).RowHeight = HeightValue;
                    break;


                case "Columns":
                    xlSheet.Columns(AreaStr).RowHeight = HeightValue;
                    break;


                case "Range":
                    xlSheet.range(AreaStr).RowHeight = HeightValue;
                    break;

                case "all":
                    xlSheet.Cells.RowHeight = HeightValue;
                    break;
            }
        }
    }

    //===============================================================================================================================
    //
    // 设置单元格内容
    // AreaStr：范围标示
    // AreaValue：内容
    //-----------------------------------------------

    function SetValue(AreaStr, AreaValue) {
        var CheckStr = Check(AreaStr);

        if (CheckStr != "" && CheckStr != "Columns") {
            switch (CheckStr) {
                case "Cells":
                    var Cell = CellsFormat(AreaStr);
                    xlSheet.Cells(Cell[0], Cell[1]).value = AreaValue;
                    break;


                case "Range":
                    xlSheet.range(AreaStr).value = AreaValue;
                    break;

            }
        }
    }

    //===============================================================================================================================
    //
    // 设置单元格内容的格式 - 字符串型
    // AreaStr：范围标示
    //-----------------------------------------------

    function SetValueFormatToString(AreaStr) {
        var CheckStr = Check(AreaStr);

        if (CheckStr != "") {
            switch (CheckStr) {
                case "Cells":
                    var Cell = CellsFormat(AreaStr);
                    xlSheet.Cells(Cell[0], Cell[1]).NumberFormatLocal = "@";
                    break;


                case "Columns":
                    xlSheet.Columns(AreaStr).NumberFormatLocal = "@";
                    break;



                case "Range":
                    xlSheet.range(AreaStr).NumberFormatLocal = "@";
                    break;


                case "all":
                    xlSheet.Cells.NumberFormatLocal = "@";
                    break;

            }
        }
    }

    //===============================================================================================================================
    //
    // 设置单元格内容的格式 - 自定义型
    // AreaStr：范围标示
    // regix：格式表达式
    //-----------------------------------------------

    function SetValueFormatToCustom(AreaStr, regix) {
        var CheckStr = Check(AreaStr);

        if (CheckStr != "") {
            switch (CheckStr) {
                case "Cells":
                    var Cell = CellsFormat(AreaStr);
                    xlSheet.Cells(Cell[0], Cell[1]).NumberFormatLocal = regix;
                    break;


                case "Columns":
                    xlSheet.Columns(AreaStr).NumberFormatLocal = regix;
                    break;


                case "Range":
                    xlSheet.range(AreaStr).NumberFormatLocal = regix;
                    break;
            }
        }
    }

    //===============================================================================================================================
    //
    // 设置范围的边框
    // AreaStr：范围标示
    //-----------------------------------------------

    function SetBorder(AreaStr) {
        var CheckStr = Check(AreaStr);

        if (CheckStr != "") {
            switch (CheckStr) {
                case "Cells":
                    var Cell = CellsFormat(AreaStr);
                    for (var i = 1; i < 5; i++) {
                        xlSheet.Cells(Cell[0], Cell[1]).Borders(i).LineStyle = 1;
                        xlSheet.Cells(Cell[0], Cell[1]).Borders(i).Weight = 2;
                    }
                    break;


                case "Columns":
                    for (var i = 7; i < 13; i++) {
                        xlSheet.Columns(AreaStr).Borders(i).LineStyle = 1;
                        xlSheet.Columns(AreaStr).Borders(i).Weight = 2;
                    }
                    break;


                case "Range":
                    for (var i = 7; i < 13; i++) {
                        xlSheet.range(AreaStr).Borders(i).LineStyle = 1;
                        xlSheet.range(AreaStr).Borders(i).Weight = 2;
                    }
                    break;
            }
        }
    }

    //===============================================================================================================================
    //
    // 显示Excel表格线对象
    //---------------------------------------------
    function ShowGrids(IsShow) {
        if (IsShow) {
            xlObject.ActiveWindow.DisplayGridlines = true;
        }
        else {
            xlObject.ActiveWindow.DisplayGridlines = false;
        }
    }


    //===============================================================================================================================
    //
    // 控制权交给用户
    //-----------------------------------------------
    function SetUserControl(IsControl) {
        if (IsControl == true) {
            xlObject.UserControl = true
        }
    }

    //===============================================================================================================================
    //
    // 清空Excel对象
    //-----------------------------------------------
    function ClearExcel() {
        try {
            xlObject = null;
            xlWork = null;
            xlSheet = null;
        }
        catch (ex) {
            throw ex;
        }
    }

    //===============================================================================================================================
    //
    // 关闭Excel对象
    //---------------------------------------------
    function QuitExcel() {
        try {
            xlWork.Close(savechanges = false);
            xlObject.Quit();
            xlObject = null;
            xlWork = null;
            xlSheet = null;
        }
        catch (ex) {
            alert("发生错误，Excel无法退出");
        }
    }

    //===============================================================================================================================
    //
    // 打印预览Excel对象
    //---------------------------------------------
    function PrintPreviewExcel() {
        try {
            xlSheet.PrintPreview();
        }
        catch (ex) {
            alert("发生错误，Excel无法打印预览");
        }
    }

    //===============================================================================================================================
    //
    // 打印Excel对象
    //---------------------------------------------
    function PrintOutExcel() {
        try {
            xlSheet.PrintOut();
        }
        catch (ex) {
            alert("打印机未安装或发生错误，无法打印文档");
        }
    }
}