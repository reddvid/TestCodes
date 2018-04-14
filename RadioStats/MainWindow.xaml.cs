using LiveCharts;
using LiveCharts.Wpf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RadioStats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void menuListView_Loaded(object sender, RoutedEventArgs e)
        {
            if (menuListView.Items.Count != 0)
            {
                menuListView.SelectedIndex = 0;
            }
        }

        private void menuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem listViewItem = menuListView.SelectedItem as ListViewItem;

            switch (listViewItem.Tag)
            {
                case "summary":
                    LoadSummaryValues();
                    break;
            }
        }

        private void LoadSummaryValues()
        {
            FileInfo existingFile = new FileInfo(@"D:\OneDrive\Work (CNN PH)\0.Others\Batac2017.xlsx");
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                // Initialize chart
                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Month",
                    Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                });

                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Efficiency",
                    LabelFormatter = value => value.ToString("")
                });

                cartesianChart1.LegendLocation = LegendLocation.Right;

                // Getting the first sheet
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[1]; // NOT 0 Index hmmm
                int col = 6;
                ChartValues<double> values = new ChartValues<double>();
                values.Clear();

                for (int row = 8; row < 20; row++)
                {
                    // label1.Text += excelWorksheet.Cells[row, col].Value;
                    values.Add((double)excelWorksheet.Cells[row, col].Value * 100);
                }

                cartesianChart1.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Batac",
                        Values = values
                    }
                };
            }
        }
    }
}
