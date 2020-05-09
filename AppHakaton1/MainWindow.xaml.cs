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

namespace AppHakaton1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*Создаем объект подключения к бд objSQL. В конструктор передаем параметры подключения */
            DB_MySQL objSQL = new DB_MySQL("sql7.freesqldatabase.com", 3306, "sql7338923", "sql7338923", "bc9vSYmu5u");

            /*Открываем сессию передачи данных в бд */
            objSQL.openSessionMySQL();

            /*Проверяем доступность к бд */
            MessageBox.Show("Доступ есть? " + objSQL.statusOpenSession().ToString());

            
        }
    }
}
