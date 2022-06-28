using Lisa.Models;
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
using System.Windows.Shapes;

namespace Lisa
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private void UpdateList()
        {
            var data = ReportBaseEntities1.GetContext().report_info.ToList();
            var names = new List<string>();
            foreach (var item in data)
            {
                names.Add(item.name + ", " + item.date_of_creating.ToString());
            }
            (Owner as MainWindow).list.ItemsSource = names;
        }
       
        public AddWindow(string name, DateTime date, string path)
        {
            InitializeComponent();
            this.name = name;
            this.path= path;
            this.date = date; 
            label1.Content = name;       
        }
        string name;
        string path;
        DateTime date;
        private void Add(object sender, RoutedEventArgs e)
        {
            string description = textBox1.Text;
            ReportBaseEntities1.GetContext().report_info.Add(new report_info(name, date, description, path));
            ReportBaseEntities1.GetContext().SaveChanges();
            UpdateList();
            this.Close();
        }
    }
}
