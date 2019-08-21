using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportLib
{
    public class ExportHelper
    {
        static int startindex = 3;
        static string[] tilesCol = { "StartDeptName", "ProName", "htje", "yzf", "wzf", "beizhu" };
        static string mergedCells = "StartDeptName" ;
        static string sheetname = "Sheet1";
        static int cellstart = 0;
        public static int ExportToExcel(DataSet ds,string year)
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                FileStream file = new FileStream("E:\\dzzwcode\\WebApp\\files\\template.xls", FileMode.Open, FileAccess.Read);
                
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                int mergedIndexStart = 0;
                int mergerIndexEnd = 0;
                string currdeptGuid = "";
                HSSFSheet mainSheet = (HSSFSheet)hssfworkbook.GetSheet(sheetname);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HSSFRow row = (HSSFRow)mainSheet.CreateRow(startindex + i);

                    HSSFCell cellbm = (HSSFCell)row.CreateCell(cellstart);
                    HSSFCell cellproname = (HSSFCell)row.CreateCell(cellstart+1);
                    HSSFCell cellhtje = (HSSFCell)row.CreateCell(cellstart+2);
                    HSSFCell cellzfje = (HSSFCell)row.CreateCell(cellstart+3);

                    HSSFCell cellndzfje = (HSSFCell)row.CreateCell(cellstart + 4);

                    HSSFCell celljhzfje = (HSSFCell)row.CreateCell(cellstart+5);
                    HSSFCell cellzfqk = (HSSFCell)row.CreateCell(cellstart+6);
                    HSSFCell cellbeizhu = (HSSFCell)row.CreateCell(cellstart + 7);
                    //initStyle(cellbm);
                    //initStyle(cellproname);
                    //initStyle(cellhtje);
                    //initStyle(cellzfje);
                    //initStyle(celljhzfje);
                    //initStyle(cellbeizhu);

                    cellproname.SetCellValue(ds.Tables[0].Rows[i]["ProName"].ToString());
                    cellhtje.SetCellValue(ds.Tables[0].Rows[i]["htje"].ToString());
                    
                    cellzfje.SetCellValue(ds.Tables[0].Rows[i]["zfje"].ToString());

                    cellndzfje.SetCellValue(ds.Tables[0].Rows[i]["ndzfje"].ToString());

                    celljhzfje.SetCellValue(ds.Tables[0].Rows[i]["jhzfje"].ToString());

                    if (ds.Tables[0].Rows[i]["htje"].ToString() == "0")
                    {
                        cellzfqk.SetCellValue("合同金额待定");
                    }
                    else
                    {
                        if (Convert.ToDouble(ds.Tables[0].Rows[i]["htje"].ToString()) > Convert.ToDouble(ds.Tables[0].Rows[i]["zfje"].ToString()))
                        {
                            cellzfqk.SetCellValue("未完成支付");
                        }
                        else
                        {
                            cellzfqk.SetCellValue("已完成支付");
                        }
                    }
                    if (ds.Tables[0].Rows[i]["beizhu"].ToString() == "," || ds.Tables[0].Rows[i]["beizhu"].ToString() == "，")
                    {
                        cellbeizhu.SetCellValue("");
                    }
                    else
                    {
                        cellbeizhu.SetCellValue(ds.Tables[0].Rows[i]["beizhu"].ToString());
                    }
                    
                    //只有一行
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        cellbm.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                        break;
                    }
                    if (i == 0)
                    {
                        cellbm.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                        currdeptGuid = ds.Tables[0].Rows[i]["StartDeptGuid"].ToString();
                        mergedIndexStart = 0 + startindex;
                    }
                    else
                    {
                        //最后一行
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            //最后一行部门不同
                            if (ds.Tables[0].Rows[i]["StartDeptGuid"].ToString() != currdeptGuid)
                            {
                                cellbm.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                                mainSheet.AddMergedRegion(new Region(mergedIndexStart, cellstart, ds.Tables[0].Rows.Count - 2 + startindex, cellstart));
                            }
                            //最后一行部门相同,直接合并
                            else
                            {
                                mainSheet.AddMergedRegion(new Region(mergedIndexStart, cellstart, ds.Tables[0].Rows.Count - 1 + startindex, cellstart));
                            }
                            break;
                        }
                        else
                        {
                            //这一行部门不同
                            if (ds.Tables[0].Rows[i]["StartDeptGuid"].ToString() != currdeptGuid)
                            {
                                currdeptGuid = ds.Tables[0].Rows[i]["StartDeptGuid"].ToString();
                                cellbm.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                                //结束，并合并部门单元格
                                mergerIndexEnd = i - 1+startindex;
                                mainSheet.AddMergedRegion(new Region(mergedIndexStart, cellstart, mergerIndexEnd, cellstart));
                                //重新开始下一轮部门项目统计
                                mergedIndexStart = i+startindex;
                            }
                            else
                            {
                                //相等不在列头添加部门数据
                            }
                        }
                       
                    }
                    
                }
              

                HSSFRow rowtitle = (HSSFRow)hssfworkbook.GetSheet("Sheet1").GetRow(0);
                HSSFCell celltitle = (HSSFCell)rowtitle.GetCell(0);

                celltitle.SetCellValue(celltitle.RichStringCellValue.String.Replace("year", year));

                HSSFRow row2 = (HSSFRow)hssfworkbook.GetSheet("Sheet1").GetRow(2);
                HSSFCell cell2 = (HSSFCell)row2.GetCell(4);
                cell2.SetCellValue(cell2.RichStringCellValue.String.Replace("year", year));

                HSSFCell cell3 = (HSSFCell)row2.GetCell(5);
                cell3.SetCellValue(cell3.RichStringCellValue.String.Replace("year", year));

                FileStream file2 = new FileStream("E:\\dzzwcode\\WebApp\\files\\年度资金报告.xls", FileMode.Create);
                hssfworkbook.Write(file2);

                file.Close();
                file2.Close();
            }
            return 0;
        }

        public static int ExportToExcel2(DataSet ds, string year,string path)
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                FileStream file = new FileStream(path + "\\template.xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                int mergedIndexStart = 0;
                int mergerIndexEnd = 0;
                string currdeptGuid = "";
                HSSFSheet mainSheet = (HSSFSheet)hssfworkbook.GetSheet(sheetname);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HSSFRow row = (HSSFRow)mainSheet.CreateRow(startindex + i);

                    HSSFCell cellbm = (HSSFCell)row.CreateCell(cellstart);
                    HSSFCell cellproname = (HSSFCell)row.CreateCell(cellstart + 1);
                    HSSFCell cellhtje = (HSSFCell)row.CreateCell(cellstart + 2);
                    HSSFCell cellzfje = (HSSFCell)row.CreateCell(cellstart + 3);

                    HSSFCell cellndzfje = (HSSFCell)row.CreateCell(cellstart + 4);

                    HSSFCell celljhzfje = (HSSFCell)row.CreateCell(cellstart + 5);
                    HSSFCell cellzfqk = (HSSFCell)row.CreateCell(cellstart + 6);
                    HSSFCell cellbeizhu = (HSSFCell)row.CreateCell(cellstart + 7);
                    //initStyle(cellbm);
                    //initStyle(cellproname);
                    //initStyle(cellhtje);
                    //initStyle(cellzfje);
                    //initStyle(celljhzfje);
                    //initStyle(cellbeizhu);

                    cellproname.SetCellValue(ds.Tables[0].Rows[i]["ProName"].ToString());
                    cellhtje.SetCellValue(ds.Tables[0].Rows[i]["htje"].ToString());

                    cellzfje.SetCellValue(ds.Tables[0].Rows[i]["zfje"].ToString());

                    cellndzfje.SetCellValue(ds.Tables[0].Rows[i]["ndzfje"].ToString());

                    celljhzfje.SetCellValue(ds.Tables[0].Rows[i]["jhzfje"].ToString());

                    if (ds.Tables[0].Rows[i]["htje"].ToString() == "0")
                    {
                        cellzfqk.SetCellValue("合同金额待定");
                    }
                    else
                    {
                        if (Convert.ToDouble(ds.Tables[0].Rows[i]["htje"].ToString()) > Convert.ToDouble(ds.Tables[0].Rows[i]["zfje"].ToString()))
                        {
                            cellzfqk.SetCellValue("未完成支付");
                        }
                        else
                        {
                            cellzfqk.SetCellValue("已完成支付");
                        }
                    }
                    if (ds.Tables[0].Rows[i]["beizhu"].ToString() == "," || ds.Tables[0].Rows[i]["beizhu"].ToString() == "，")
                    {
                        cellbeizhu.SetCellValue("");
                    }
                    else
                    {
                        cellbeizhu.SetCellValue(ds.Tables[0].Rows[i]["beizhu"].ToString());
                    }

                    //只有一行
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        cellbm.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                        break;
                    }
                    if (i == 0)
                    {
                        cellbm.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                        currdeptGuid = ds.Tables[0].Rows[i]["StartDeptGuid"].ToString();
                        mergedIndexStart = 0 + startindex;
                    }
                    else
                    {
                        //最后一行
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            //最后一行部门不同
                            if (ds.Tables[0].Rows[i]["StartDeptGuid"].ToString() != currdeptGuid)
                            {
                                cellbm.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                                mainSheet.AddMergedRegion(new Region(mergedIndexStart, cellstart, ds.Tables[0].Rows.Count - 2 + startindex, cellstart));
                            }
                            //最后一行部门相同,直接合并
                            else
                            {
                                mainSheet.AddMergedRegion(new Region(mergedIndexStart, cellstart, ds.Tables[0].Rows.Count - 1 + startindex, cellstart));
                            }
                            break;
                        }
                        else
                        {
                            //这一行部门不同
                            if (ds.Tables[0].Rows[i]["StartDeptGuid"].ToString() != currdeptGuid)
                            {
                                currdeptGuid = ds.Tables[0].Rows[i]["StartDeptGuid"].ToString();
                                cellbm.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                                //结束，并合并部门单元格
                                mergerIndexEnd = i - 1 + startindex;
                                mainSheet.AddMergedRegion(new Region(mergedIndexStart, cellstart, mergerIndexEnd, cellstart));
                                //重新开始下一轮部门项目统计
                                mergedIndexStart = i + startindex;
                            }
                            else
                            {
                                //相等不在列头添加部门数据
                            }
                        }

                    }

                }


                HSSFRow rowtitle = (HSSFRow)hssfworkbook.GetSheet("Sheet1").GetRow(0);
                HSSFCell celltitle = (HSSFCell)rowtitle.GetCell(0);

                celltitle.SetCellValue(celltitle.RichStringCellValue.String.Replace("year", year));

                HSSFRow row2 = (HSSFRow)hssfworkbook.GetSheet("Sheet1").GetRow(2);
                HSSFCell cell2 = (HSSFCell)row2.GetCell(4);
                cell2.SetCellValue(cell2.RichStringCellValue.String.Replace("year", year));

                HSSFCell cell3 = (HSSFCell)row2.GetCell(5);
                cell3.SetCellValue(cell3.RichStringCellValue.String.Replace("year", year));

                FileStream file2 = new FileStream(path+"\\年度资金报告.xls", FileMode.Create);
                hssfworkbook.Write(file2);

                file.Close();
                file2.Close();
            }
            return 0;
        }


        private static string[] zjArr = { "平台资金", "财政资金", "上争资金", "自筹资金\n（说明来源）", "其他" };
        private static string[] yearArr = { "2018", "2019", "2020", "2021", "..." };
        private static string[] zjColArr = { "ptzj", "cazj", "szzj", "zczj", "qtzj" };
        /// <summary>
        /// 导出部门需要补充信息的excel模板
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="year"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int ExportToExcelTemplate(DataSet ds, string path,string bmname,DataSet dsZj,DataSet dsZjjh)
        {
            int cStartRow = 3;
            int cStartCell = 0;
            string curBmName = "";
            int mergeStartIndex = 0;
            int mergeEndIndex = 0;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                FileStream file = new FileStream(path + "\\template2.xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
               
                string currdeptGuid = "";
                HSSFSheet mainSheet = (HSSFSheet)hssfworkbook.GetSheet(sheetname);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    string proGuid = ds.Tables[0].Rows[i]["proguid"].ToString();
                    DataRow[] zjRowArr = dsZj.Tables[0].Select("xmguid='" + proGuid + "'");




                    //新建一行
                    HSSFRow row1 = (HSSFRow)mainSheet.CreateRow(cStartRow + i * 5 );
                     ICellStyle commonStyle = hssfworkbook.CreateCellStyle();
                     commonStyle.WrapText = true;//设置换行这个要先设置

                     commonStyle.Alignment = HorizontalAlignment.Center;//水平对齐
                     mainSheet.SetDefaultColumnStyle(0, commonStyle);
                     mainSheet.SetDefaultColumnStyle(1, commonStyle);
                     mainSheet.SetDefaultColumnStyle(2, commonStyle);
                     mainSheet.SetDefaultColumnStyle(3, commonStyle);
                     mainSheet.SetDefaultColumnStyle(4, commonStyle);
                     mainSheet.SetDefaultColumnStyle(5, commonStyle);
                     mainSheet.SetDefaultColumnStyle(6, commonStyle);
                     mainSheet.SetDefaultColumnStyle(7, commonStyle);
                     mainSheet.SetDefaultColumnStyle(8, commonStyle);
                    //只有一行
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        HSSFCell celldeptname = (HSSFCell)row1.CreateCell(cStartCell + 1);
                        celldeptname.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());
                        mainSheet.AddMergedRegion(new Region(cStartRow, 1, cStartRow + 4, 1));
                    }
                    else
                    {
                        if (i == 0)
                        {
                            curBmName = ds.Tables[0].Rows[i]["StartDeptName"].ToString();
                            //记录该部门开始位置
                            mergeStartIndex = cStartRow;

                            //创建部门列
                            HSSFCell celldeptname = (HSSFCell)row1.CreateCell(cStartCell + 1);
                            celldeptname.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());

                        }
                        else
                        {
                            //部门已更换
                            if (curBmName != ds.Tables[0].Rows[i]["StartDeptName"].ToString())
                            {
                                //重新设置当前项目所属部门
                                curBmName = ds.Tables[0].Rows[i]["StartDeptName"].ToString();
                                //合并上面部门单元格
                                mergeEndIndex = cStartRow + i * 5 - 1;
                                mainSheet.AddMergedRegion(new Region(mergeStartIndex, 1, mergeEndIndex, 1));

                                //保存下一个部门开始行数
                                mergeStartIndex = mergeEndIndex + 1;
                                //新建部门单元格
                                HSSFCell celldeptname = (HSSFCell)row1.CreateCell(cStartCell + 1);
                                celldeptname.SetCellValue(ds.Tables[0].Rows[i]["StartDeptName"].ToString());

                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    //合并部门单元格
                                    mainSheet.AddMergedRegion(new Region(mergeStartIndex, 1, cStartRow + (i + 1) * 5 - 1, 1));
                                }

                            }
                            //部门未更换
                            else
                            {
                                //最后一行
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    //合并部门单元格
                                    mainSheet.AddMergedRegion(new Region(mergeStartIndex, 1, cStartRow + (i + 1) * 5 - 1, 1));
                                }
                                else
                                {
                                }
                            }
                            
                        }


                    }

                    //dsZj,DataSet dsZjjh
                    DataRow[] zjRows=dsZj.Tables[0].Select("xmguid='"+ds.Tables[0].Rows[i]["proguid"].ToString()+"'");
                    

                    //每个项目占五行
                    for (int k = 0; k < 5; k++)
                    {
                        HSSFRow row ;

                        if (k == 0)
                        {
                            row = (HSSFRow)mainSheet.GetRow(cStartRow + i * 5);
                            HSSFCell cellxh = (HSSFCell)row.CreateCell(cStartCell);
                            HSSFCell cellproname = (HSSFCell)row.CreateCell(cStartCell + 2);
                            HSSFCell cellhtje = (HSSFCell)row.CreateCell(cStartCell + 3);
                            HSSFCell cellyzfje = (HSSFCell)row.CreateCell(cStartCell + 4);

                            HSSFCell cellproguid = (HSSFCell)row.CreateCell(cStartCell + 9);

                            initTemplateStyle(hssfworkbook, cellxh);
                            initTemplateStyle(hssfworkbook, cellproname);
                            initTemplateStyle(hssfworkbook, cellhtje);


                            cellxh.SetCellValue(i + 1);
                            cellproname.SetCellValue(ds.Tables[0].Rows[i]["ProName"].ToString());
                            cellhtje.SetCellValue(ds.Tables[0].Rows[i]["htje"].ToString());
                            cellyzfje.SetCellValue(ds.Tables[0].Rows[i]["zfje"].ToString());
                            cellproguid.SetCellValue(ds.Tables[0].Rows[i]["proguid"].ToString());
                        }
                        else
                        {
                            row = (HSSFRow)mainSheet.CreateRow(cStartRow + i * 5 + k);
                        }


                        HSSFCell cellzjzc = (HSSFCell)row.CreateCell(cStartCell + 5);

                        ICellStyle notesStyle = hssfworkbook.CreateCellStyle();
                        notesStyle.WrapText = true;//设置换行这个要先设置
                        
                        notesStyle.Alignment = HorizontalAlignment.Center;//水平对齐

                        cellzjzc.CellStyle = notesStyle;//设置换行

                        cellzjzc.SetCellValue(zjArr[k]);
                        initTemplateStyle(hssfworkbook, cellzjzc);

                        //资金组成额度
                        HSSFCell cellzje = (HSSFCell)row.CreateCell(cStartCell + 6);
                        initTemplateStyle(hssfworkbook, cellzje);
                        if (zjRowArr.Count() != 0)
                        {
                            if (k == 3)
                            {
                                if (zjRowArr[0]["zczjly"].ToString() != "")
                                {
                                    cellzje.SetCellValue(zjRowArr[0][zjColArr[k]].ToString() + "\n(" + zjRowArr[0]["zczjly"] + ")");
                                }
                                else
                                {
                                    cellzje.SetCellValue(zjRowArr[0][zjColArr[k]].ToString() );
                                }
                               
                            }
                            else
                            {
                                cellzje.SetCellValue(zjRowArr[0][zjColArr[k]].ToString());
                            }
                            
                        }

                        HSSFCell celljhzfnd = (HSSFCell)row.CreateCell(cStartCell + 7);
                        initTemplateStyle(hssfworkbook, celljhzfnd);


                        //年度计划支付额
                        HSSFCell celljhzfe = (HSSFCell)row.CreateCell(cStartCell + 8);

                        initTemplateStyle(hssfworkbook, celljhzfe);

                        DataRow[] rowsJh=null;
                        if (k == 4)
                        {
                            rowsJh = dsZjjh.Tables[0].Select("xmguid='" + proGuid + "' and nd='2022'");
                            if (dsZjjh.Tables[0].Select("xmguid='" + proGuid + "' and nd='2022'").Count() > 0)
                            {
                                celljhzfnd.SetCellValue("2022");
                                celljhzfe.SetCellValue(rowsJh[0]["je"].ToString());
                            }
                            else
                            {
                                celljhzfnd.SetCellValue(yearArr[k]);
                            }
                        }
                        else
                        {
                            rowsJh = dsZjjh.Tables[0].Select("xmguid='" + proGuid + "' and nd='" + yearArr[k] + "'");

                            if (rowsJh.Count() > 0)
                            {
                                celljhzfe.SetCellValue(rowsJh[0]["je"].ToString());
                            }

                            celljhzfnd.SetCellValue(yearArr[k]);
                        }
                       
                        
                       
                     
                     

                        if (k == 4)
                        {
                            mainSheet.AddMergedRegion(new Region(cStartRow + 5 * i , 0, cStartRow + 5 * (i + 1) - 1, 0));
                            mainSheet.AddMergedRegion(new Region(cStartRow + 5 * i , 2, cStartRow + 5 * (i + 1) - 1, 2));
                            mainSheet.AddMergedRegion(new Region(cStartRow + 5 * i , 3, cStartRow + 5 * (i + 1) - 1, 3));
                            mainSheet.AddMergedRegion(new Region(cStartRow + 5 * i, 4, cStartRow + 5 * (i + 1) - 1, 4));
                        }

                    }

                }
                   

                  

                FileStream file2 = new FileStream(path + "\\" + bmname + "_2016—2018年泰州市电子政务项目资金安排清单.xls", FileMode.Create);
                hssfworkbook.Write(file2);

                file.Close();
                file2.Close();
            }
            return 0;
        }


        public static void initTemplateStyle(HSSFWorkbook hssfworkbook,HSSFCell cell)
        {
            //HSSFCellStyle style = (HSSFCellStyle)hssfworkbook.CreateCellStyle();
            //style.BorderBottom = BorderStyle.Thin;
            //style.BorderLeft = BorderStyle.Thin;
            //style.BorderRight = BorderStyle.Thin;
            //style.BorderTop = BorderStyle.Thin;

            //HSSFFont font = (HSSFFont)hssfworkbook.CreateFont();
            //font.FontHeightInPoints = 14;
            //style.SetFont(font);


            //cell.CellStyle = style;

        }

        public static void initStyle(HSSFCell cell)
        {
            cell.CellStyle.BorderBottom = BorderStyle.Thin;
            //cell.CellStyle.BorderLeft = BorderStyle.Thin;
            cell.CellStyle.BorderRight = BorderStyle.Thin;
            //cell.CellStyle.BorderTop = BorderStyle.Thin;
        }

        //public static void WriteLogs(string content)
        //{
        //    string path = "C:";

        //    path = "C:\\1.txt";

        //    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
        //    sw.WriteLine(content);
        //    //  sw.WriteLine("----------------------------------------");
        //    sw.Close();
        //}

        public static string  ExcelToDataTable(string fileName)
        {

            string sql = "";

            int startRow = 3;
            IWorkbook workbook = null;
            ISheet sheet = null;
            DataTable data = new DataTable();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
             if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
             else if (fileName.IndexOf(".xls") > 0) // 2003版本
             {
                 try
                 {
                     workbook = new HSSFWorkbook(fs);
                 }
                 catch (Exception e)
                 {

                     workbook = new XSSFWorkbook(fs);
                 }
                    
             }


             sheet = workbook.GetSheet("Sheet1");

             int rowCount = sheet.LastRowNum;

            int index=startRow;
            while (sheet.GetRow(index).GetCell(0)!=null)
            {
                IRow ptzjRow = sheet.GetRow(index);
                IRow czzjRow = sheet.GetRow(index + 1);
                IRow szzjRow = sheet.GetRow(index + 2);
                IRow zczjRow = sheet.GetRow(index + 3);
                IRow qtRow = sheet.GetRow(index + 4);

                ICell ptzjCell = ptzjRow.GetCell(6);
                ICell czzjCell = czzjRow.GetCell(6);
                ICell szzjCell = szzjRow.GetCell(6);
                ICell zczjCell = zczjRow.GetCell(6);
                ICell qtzjCell = qtRow.GetCell(6);

                //2018 2019 2020 2021 ...
                ICell ndCell0 = ptzjRow.GetCell(7);
                ICell ndCell1 = czzjRow.GetCell(7);
                ICell ndCell2 = szzjRow.GetCell(7);
                ICell ndCell3 = zczjRow.GetCell(7);
                ICell ndCell4 = qtRow.GetCell(7);

            
                string xmguid=getCellValue(sheet.GetRow(index).GetCell(9));

                string[] zczjArr = getZczjCellValue(zczjCell);
                //删除原先上传信息，插入新的信息
                sql += "delete from tz_zjzc where xmguid='" + xmguid + "';insert into tz_zjzc values(newid(),'" + xmguid + "','" + getCellValue(ptzjCell) + "','" + getCellValue(czzjCell) + "','" + getCellValue(szzjCell) + "','" + zczjArr[0] + "','" + getCellValue(qtzjCell) + "','" + zczjArr[1] + "');";


                sql += "delete from tz_zfjhb where xmguid='" + getCellValue(sheet.GetRow(index).GetCell(9)) + "';";
                sql += getJhzjSql(xmguid, ndCell0, ptzjRow.GetCell(8));
                sql += getJhzjSql(xmguid, ndCell1, czzjRow.GetCell(8));
                sql += getJhzjSql(xmguid, ndCell2, szzjRow.GetCell(8));
                sql += getJhzjSql(xmguid, ndCell3, zczjRow.GetCell(8));
                sql += getJhzjSql(xmguid, ndCell4, qtRow.GetCell(8));


                index += 5;
            }
            return sql;
        }

        public static string[] getZczjCellValue(ICell cell)
        {
            string[] zczjArr = new string[2];
            if (cell == null)
            {
                zczjArr[0] = "0";
                zczjArr[1] = "";
            }
            else
            {
                cell.SetCellType(CellType.String);

                string cellValue = cell.StringCellValue;
                if (cellValue.IndexOf("(") >= 0 || cellValue.IndexOf("（") >= 0)
                {
                    int index = cellValue.IndexOf("(") >= 0 ? cellValue.IndexOf("(") : cellValue.IndexOf("（");
                    int endIndex = cellValue.IndexOf(")") >= 0 ? cellValue.IndexOf(")") : cellValue.IndexOf("）");
                    zczjArr[0] = cellValue.Substring(0, index - 1);
                    zczjArr[1] = cellValue.Substring(index + 1, endIndex - index-1);
                }
                else
                {
                    zczjArr[0] = cell.StringCellValue;
                    zczjArr[1] = "";
                }

                
            }
            return zczjArr;
        }

        public static string getCellValue(ICell cell)
        {
            if (cell == null)
            {
                return "0";
            }
            else
            {
                cell.SetCellType(CellType.String);
                return cell.StringCellValue;
            }
        }

        public static string getJhzjSql(string xmguid,ICell ndCell,ICell jeCell)
        {
            if (ndCell == null)
            {
                return "";
            }
            else
            {
                if (jeCell == null)
                {
                    return "";
                }
                else
                {
                    ndCell.SetCellType(CellType.String);
                    if (ndCell.StringCellValue == "..."||ndCell.StringCellValue=="")
                    {
                        return "";
                    }
                    jeCell.SetCellType(CellType.String);
                    if (jeCell.StringCellValue == "")
                    {
                        return "";
                    }
                    else
                    return "insert into tz_zfjhb values(newid(),'" + xmguid + "','" + ndCell.StringCellValue + "','" + jeCell.StringCellValue + "');";
                }
                

            }
            
        }
    }
}
