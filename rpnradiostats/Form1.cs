using LiveCharts;
using LiveCharts.Wpf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rpnradiostats
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
                    label1.Text += excelWorksheet.Cells[row, col].Value;
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
