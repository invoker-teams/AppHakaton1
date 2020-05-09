using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop;
using System.IO;
using Microsoft.Win32;
//using DocumentFormat.OpenXml.Drawing.Charts;
//using Microsoft.Office.Interop.Excel;
//using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 


   
    public partial class MainWindow : Window
    {




        //private Microsoft.Office.Interop.Excel.Application ExcelApp;
        //private Microsoft.Office.Interop.Excel.Workbook WorkBookExcel;
        //private Microsoft.Office.Interop.Excel.Worksheet WorkSheetExcel;
        //private Microsoft.Office.Interop.Excel.Range RangeExcel;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";

            var browsefile = openfile.ShowDialog();

            if(browsefile==true)
            {
                txtFilePath.Text = openfile.FileName;
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(txtFilePath.Text.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                string strCellData = "";
                double douCellData;
                //DateTime dat;
                int rowCnt = 0; //строка
                int colCnt = 0; //столбец

                DataTable dt = new DataTable();
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++) //цикл для создания шапки
                {
                    string strColumn = "";
                    strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2; 
                    dt.Columns.Add(strColumn, typeof(string));
                }
                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                {
                    string strData = "";
                    for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                    {
                        if (colCnt == 1)
                        {
                            douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += DateTime.FromOADate(douCellData).ToShortDateString().ToString() + "|";
                        }
                        else if(colCnt == 2)
                        {
                            douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += DateTime.FromOADate(douCellData).ToLongTimeString().ToString() + "|";
                        }
                        else
                        {
                            try
                            {
                                strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                                strData += strCellData + "|";
                            }
                            catch (Exception ex)
                            {
                                douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                                strData += douCellData.ToString() + "|";
                            }
                        }
                    }
                    strData = strData.Remove(strData.Length - 1, 1);
                    dt.Rows.Add(strData.Split('|'));
                }
                dGrid.ItemsSource = dt.DefaultView;

                excelBook.Close(true, null, null);
                excelApp.Quit();
            }
            //Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            //openDialog.Filter = "Файл Excel|*.xlsx;.xls";
            //var result = openDialog.ShowDialog();
            //if (result == false)
            //{
            //    MessageBox.Show("Файл не выбран");
            //    return;
            //}
            //string fileName = System.IO.Path.GetFileName(openDialog.FileName);

            //ExcelApp = new Microsoft.Office.Interop.Excel.Application();

            //WorkBookExcel = ExcelApp.Workbooks.Open(openDialog.FileName);

            //WorkSheetExcel = (Microsoft.Office.Interop.Excel.Worksheet)WorkBookExcel.Sheets[1];

            //var lastCell = WorkSheetExcel.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);

            //string[,] list = new string[lastCell.Column, lastCell.Row];

            //for (int i = 0; i < (int)lastCell.Column; i++)
            //    for (int j = 0; j < (int)lastCell.Row; j++)
            //        list[i, j] = WorkSheetExcel.Cells[j + 1, i + 1].Text.ToString();

           
            //WorkBookExcel.Close(false, Type.Missing, Type.Missing);
            //ExcelApp.Quit();
            //GC.Collect();
            //dGrid.ItemsSource = list;

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
