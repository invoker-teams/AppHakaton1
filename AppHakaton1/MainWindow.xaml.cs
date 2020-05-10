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
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            objSQL.getTimeTimetable(2);
        }
    }
}
