using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;
using System.IO;
using Microsoft.Win32;
using System.Data;

namespace AppHakaton1
{
    class getExcelData
    {
        string txtFilePath;
        Microsoft.Office.Interop.Excel.Application excelApp;
        Microsoft.Office.Interop.Excel.Workbook excelBook;
        Microsoft.Office.Interop.Excel.Worksheet excelSheet;
        Microsoft.Office.Interop.Excel.Range excelRange;

        bool x = false;
        public getExcelData(string txtPath)
        {
            txtFilePath = txtPath;
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelBook = excelApp.Workbooks.Open(txtFilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
            excelRange = excelSheet.UsedRange;
        }

        /*
         * Метод для получения размера excel файла
         */
        public int[] setRange()
        {
            int rowCnt, colCnt; //строка, столбец
            int countR=0, countC=0;
            int[] mass = new int[2];
            for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
            {
                countR += countR + 1;
                
            }
            mass[0] = excelRange.Rows.Count;
            for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
            {
                countC = countC + 1;
                
            }
            mass[1] = excelRange.Columns.Count;
            return mass;
        }

        /*
         * Метод для публикации данных в бд
         */
        public void pushDB()
        {
            string strCellData = "";

            double douCellData;
            DB_MySQL objSQL = new DB_MySQL("sql7.freesqldatabase.com", 3306, "sql7338923", "sql7338923", "bc9vSYmu5u");

            objSQL.openSessionMySQL();

            int rowCnt = 0; //строка
            int colCnt = 0; //столбец
            int[] mass = new int[10];
            DataTable dt = new DataTable();
            DateTime FlightDate = new DateTime();
            TimeSpan ScheduledTime = new TimeSpan();
            int AirlineCode=0,FlightNumber=0;
            string CodeA = "", FlagArrivalDeparture = "", TypeAircraft = "", AParking = "", ParkingSector = "", NameAirline = "";
            for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
            {
                string strData = "";
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    switch (colCnt)
                    {
                        case 1:
                            douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            FlightDate = DateTime.FromOADate(douCellData);
                            break;
                        case 2:
                            DateTime dt3 = new DateTime(2000, 2, 3, 10, 20, 30);
                            douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            ScheduledTime = dt3.TimeOfDay;
                            break;
                         case 3:
                            CodeA = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            break;
                        case 4:
                            AirlineCode = (int)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            break;
                        case 5:
                            //FlightNumber = Convert.ToInt16((excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2);
                            break;
                        case 6:
                            FlagArrivalDeparture = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            break;
                        case 7:
                            TypeAircraft = Convert.ToString((excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2);
                            break;
                        case 8:
                            AParking = Convert.ToString((excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2);
                            break;
                        case 9:
                            ParkingSector = Convert.ToString((excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2);
                            break;
                    }
                       
                }
                    objSQL.CreatingNewRowTimetable(FlightDate, ScheduledTime, CodeA, AirlineCode, FlightNumber, FlagArrivalDeparture, TypeAircraft, AParking, ParkingSector, NameAirline);           
            }
            objSQL.closeSessionMySQL();

            x = true;
        }

        /*
         * Метод для закрытия работы с excel
         */
        public void closeExl()
        {
            excelBook.Close(true, null, null);
            excelApp.Quit();
        }

    }
}
