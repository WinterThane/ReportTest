using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReportTest
{
    public class ReportTestViewModel
    {
        private MainWindow _mainWindow;
        private ReportViewer _reportViewer;

        public ReportTestViewModel(MainWindow window)
        {
            _mainWindow = window;
            _reportViewer = window.reportViewer;
            Initialize();
        }

        private IEnumerable<Department> departments = new List<Department>()
        {
           new Department() {ID = 1, Name = "I. erwas" },
           new Department() {ID = 2, Name = "II. etwas etwas" },
           new Department() {ID = 3, Name = "III. etwas etwas etwas" },
           new Department() {ID = 4, Name = "IV. you get the idea" },
        };

        private List<YearsData> Years = new List<YearsData>()
        {
            new YearsData() {ID = 1, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 1, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 1, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 1, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 2, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 2, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 2, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 3, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 3, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 4, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 4, Year1 = 1, Year2 = 20, Year3 = 300 },
            new YearsData() {ID = 4, Year1 = 1, Year2 = 20, Year3 = 300 }
        };

        private void Initialize()
        {
            _reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource departmentsModels = new ReportDataSource() { Name = "Departments_DS", Value = departments };
            _reportViewer.LocalReport.DataSources.Add(departmentsModels);
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            string MainPage = path + @"\ReportTest\MainReport.rdlc";
            _reportViewer.LocalReport.ReportPath = MainPage;
            _reportViewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            _reportViewer.LocalReport.EnableExternalImages = true;
            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.Refresh();
            _reportViewer.RefreshReport();
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            int ID = Convert.ToInt32(e.Parameters[0].Values[0]);
            List<YearsData> years = Years.FindAll(x => x.ID == ID);
            if (e.ReportPath == "SubReport")
            {
                ReportDataSource yearsDetails = new ReportDataSource() { Name = "YearsData_DS", Value = years };
                e.DataSources.Add(yearsDetails);
            }
        }
    }
}
