using Lisa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FastReport;
using System.Windows.Controls;

namespace Lisa
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void UpdateList()
        {
            var data = ReportBaseEntities1.GetContext().report_info.ToList();
            var names = new List<string>();
            foreach (var item in data)
            {
                names.Add(item.name + ", " + item.date_of_creating.ToString());
            }
            list.ItemsSource = names;
        }
        public MainWindow()
        {
            InitializeComponent();
            UpdateList();


        }


        public report_info GetInfo()
        {
            return ReportBaseEntities1.GetContext().report_info.ToList()[list.SelectedIndex];
        }
        private void Add_Report(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Filter = "Report file (*.frx)|*.frx| RDL file (*.rdl)|*.rdl| Rich Text file (*.rtf)|*.rtf";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string path = dlg.FileName;
                var date = System.IO.File.GetCreationTime(path);
                string name = System.IO.Path.GetFileNameWithoutExtension(path);
                AddWindow taskWindow = new AddWindow(name, date, path);
                taskWindow.Owner = this;
                taskWindow.ShowDialog();


            }
        }
        private void Update_Report(object sender, RoutedEventArgs e)
        {
            using (Report report = new Report())
            {



                try
                {
                    var data = list.ItemsSource.Cast<string>().ToList();
                    string name = data[list.SelectedIndex].Split(',')[0];
                    var path = ReportBaseEntities1.GetContext().report_info.Where(x => x.name == name).FirstOrDefault().path;


                    report.Load(path);
                    
                    report.Design();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        private void Update_Info(object sender, RoutedEventArgs e)
        {

            try
            {
                var data = list.ItemsSource.Cast<string>().ToList();
                string name = data[list.SelectedIndex].Split(',')[0];
                var report = ReportBaseEntities1.GetContext().report_info.Where(x => x.name == name).FirstOrDefault();
                ReportInfoUpdatePage reportInfoUpdate = new ReportInfoUpdatePage(report, list.SelectedIndex, data);
                reportInfoUpdate.Owner = this;
                reportInfoUpdate.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Delete_Report(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = list.ItemsSource.Cast<string>().ToList();
                string name = data[list.SelectedIndex].Split(',')[0];
                report_info report = ReportBaseEntities1.GetContext().report_info.Where(x => x.name == name).FirstOrDefault();
                if (report != null)
                {
                    ReportBaseEntities1.GetContext().report_info.Remove(report);
                    ReportBaseEntities1.GetContext().SaveChanges();
                }
                UpdateList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void list_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            using (Report report = new Report())
            {



                try
                {
                    var data = list.ItemsSource.Cast<string>().ToList();
                    string name = data[list.SelectedIndex].Split(',')[0];
                    var path = ReportBaseEntities1.GetContext().report_info.Where(x => x.name == name).FirstOrDefault().path;


                    report.Load(path);
                    report.Show();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Table is not connected to the data. Register the data using Report.RegisterData method."))
                    {
                        MessageBox.Show("Не подключена база данных для данного отчёта");
                    }
                }



            }
        }
        private void ListBoxItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                var item = e.OriginalSource as ListBoxItem;

                var _content = item.Content as string;
                string name = _content.Split(',')[0];

                var description = ReportBaseEntities1.GetContext().report_info.Where(x => x.name == name).FirstOrDefault().description;

                TextBlock txt = new TextBlock();
                txt.Text = description;
                ToolTip tt = new System.Windows.Controls.ToolTip();
                tt.Content = txt;

                tt.HasDropShadow = true;

                (e.OriginalSource as ListBoxItem).ToolTip = tt;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
