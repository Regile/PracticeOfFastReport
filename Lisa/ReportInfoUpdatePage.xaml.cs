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
    /// Логика взаимодействия для ReportInfoUpdatePage.xaml
    /// </summary>
    public partial class ReportInfoUpdatePage : Window
    {
        report_info report;
        int id;
        List<string> data;
        public ReportInfoUpdatePage(report_info report,int id, List<string> data)
        {
            InitializeComponent();
            this.report = report;
            this.id = id;
            this.data = data;
            textBox1.Text = report.name;
            textBox2.Text = report.date_of_creating.ToString();
            textBox3.Text = report.path;
            textBox4.Text = report.description;
        }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var date = DateTime.Parse(textBox2.Text);
            report_info report_Info = new report_info(textBox1.Text, date, textBox4.Text,textBox3.Text);
            var rec= ReportBaseEntities1.GetContext().report_info.Where(x=>x.name==report.name).FirstOrDefault();
            rec.name = report_Info.name;
            rec.path = report_Info.path;
            rec.description = report_Info.description;
            rec.date_of_creating = report_Info.date_of_creating;
            ReportBaseEntities1.GetContext().SaveChanges();
            UpdateList();
            this.Close();
        }
    }
}
