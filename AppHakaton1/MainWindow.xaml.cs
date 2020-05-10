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
using System.Data;
using System.Threading;
using System.Globalization;

namespace AppHakaton1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog openfile;
        Thread threadExl;
        getExcelData objExl;
        DB_MySQL objSQL;
        public MainWindow()
        {
            InitializeComponent();
            objSQL = new DB_MySQL("sql7.freesqldatabase.com", 3306, "sql7338923", "sql7338923", "bc9vSYmu5u");

            objSQL.openSessionMySQL();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                objExl = new getExcelData(openfile.FileName.ToString());
                objExl.pushDB();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            objSQL.clearTableDB("Timetable");
            MessageBox.Show("Таблица очищена");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            /*Начиная от сюда  */
            int[] arr = new int[2];
            openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                objExl = new getExcelData(openfile.FileName.ToString());
                arr = objExl.setRange();
            }

            /*До сюда я просто получаю кол-во строк в файле чтоб для них рассчитать значения*/

            /*Создал матрицу в которой буду хранить некоторые параметры*/
            string[] buf = new string[10];
            string[,] matrix = new string[10, arr[0]];
            int i, j;
            for ( i = 0; i < arr[0]; i++)
            {
                buf = objSQL.getTimeTimetable(i);
                for ( j = 0; j < 4; j++)
                {
                    matrix[j, i] = buf[j];

                }
                matrix[4, i] = objSQL.getKolodki(matrix[2, i]);
                Console.WriteLine(matrix[4, i]);
            }

            /*string[] freeTime = new string[arr[0]];
            var vychet = Convert.ToDateTime("1:00:00");
            for (int k = 1; k < arr[0]; k++)
            {
                var time = Convert.ToDateTime(matrix[1, i]);
                string buf = 
                if(freeTime[k] != .ToString())
                {

                }
            }*/
            var result = Convert.ToDateTime(matrix[1, 2]);
            var result2 = Convert.ToDateTime(matrix[1, 3]);

            Console.WriteLine(result2-Convert.ToDateTime("1:00:00"));

            //string test = result.ToString("hh:mm:ss tt", CultureInfo.CurrentCulture);

        }
    }
}
