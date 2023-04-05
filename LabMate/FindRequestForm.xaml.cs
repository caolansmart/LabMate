using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace LabMate
{
    public partial class FindRequestForm : UserControl
    {
        public FindRequestForm()
        {
            InitializeComponent();
        }

        // --- EVENT LISTENERS FOR UC --- //
        private void FindRequestSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            Request searchRequest = GetSearchRequest();
            List<Request> resultList = Form.FindRequestsFromDb(searchRequest);
            if (resultList.Count == 0)
            {
                MessageBox.Show("No results");
            }
            else
            {
                ClearSearchForm();
                DisplaySearchResults(resultList);
            }
        }

        private void FindRequestCancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        // -- COLLECT INPUTS FROM SEARCH FORM -- //
        private Request GetSearchRequest()
        {

            Request searchRequest = new Request
            {
                LabRefNo = labRefNoTxtbox.Text,
                HcNo = hcNoTxtbox.Text,
                HospitalNo = hospitalNoTxtbox.Text,
                Surname = surnameTxtbox.Text,
                Forename = forenameTxtbox.Text,
                Sex = sexCombobox.Text,
                Dob = dobDatepicker.Text,
                Address1 = address1Txtbox.Text,
                Address2 = address2Txtbox.Text,
                Address3 = address3Txtbox.Text,
                PostCode = postCodeTxtbox.Text,
                Hospital = hospitalTxtbox.Text,
                ConsGp = consGpTxtbox.Text,
                Ward = wardTxtbox.Text,
                Specialty = specialtyTxtbox.Text,
                SpecimenType = specimenTypeCombobox.Text,
                SpecimenDate = specimenDateDatepicker.Text,
                RecdDate = recDateDatepicker.Text,
                FlagCode = flagCodeCombobox.Text,
                Urgency = urgencyCombobox.Text,
            };

            return searchRequest;
        }

        // -- CLEAR SEARCH FORM -- //
        private void ClearSearchForm()
        {
            labRefNoPanel.IsEnabled = false;
            labRefNoPanel.Visibility = Visibility.Hidden;

            requestFormGrid.IsEnabled = false;
            requestFormGrid.Visibility = Visibility.Hidden;

            findRequestSearchBtn.IsEnabled = false;
            findRequestSearchBtn.Visibility = Visibility.Hidden;
        }

        // -- DISPLAY SEARCH RESULTS IN TABLE -- //
        private void DisplaySearchResults(List<Request> resultList)
        {
            DataTable resultTable = new DataTable();

            foreach(var property in resultList[0].GetType().GetProperties())
            {
                resultTable.Columns.Add(property.Name);
            }

            foreach (Request result in resultList)
            {
                DataRow resultRow = resultTable.NewRow();
                foreach (var property in resultList[0].GetType().GetProperties())
                {
                    resultRow[property.Name] = property.GetValue(result);
                }
                resultTable.Rows.Add(resultRow);
            }
            
            resultsDataGrid.ItemsSource = resultTable.DefaultView; // Map data table to data grid

            AddColumnToDataGrid("Lab Ref No.", "LabRefNo"); // Add necessary columns to data grid
            AddColumnToDataGrid("H+C No.", "HcNo");
            AddColumnToDataGrid("Hospital No.", "HospitalNo");
            AddColumnToDataGrid("Surname", "Surname");
            AddColumnToDataGrid("Forename", "Forename");
            AddColumnToDataGrid("Date of Birth", "Dob");
            AddColumnToDataGrid("Specimen Type", "SpecimenType");
            AddColumnToDataGrid("Received Date", "RecdDate");

            resultsDataGrid.IsEnabled = true; // Show data grid in window
            resultsDataGrid.Visibility = Visibility.Visible;
        }

        private void AddColumnToDataGrid(string displayColumnName, string dataColumnName)
        {
            DataGridTextColumn newColumn = new DataGridTextColumn();
            newColumn.Header = displayColumnName;
            newColumn.Binding = new Binding(dataColumnName);
            resultsDataGrid.Columns.Add(newColumn);
        }

        private void ExitForm()
        {
            DependencyObject form = this.Parent;
            (form as ContentControl).Content = null;
        }
    }

}
