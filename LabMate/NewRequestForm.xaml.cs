using System;
using System.Collections.Generic;
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
    public partial class NewRequestForm : UserControl
    {
        public NewRequestForm()
        {
            InitializeComponent();
        }

        // --- EVENT LISTENERS FOR UC --- //
        private void NewRequestAcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show($"Create new Request {labRefNoTxtbox.Text}?", "Confirmation", MessageBoxButton.YesNo);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                Request newRequest = GetNewRequest();
                if (Form.NewRequestToDb(newRequest) == true)
                {
                    ExitForm();
                }
            }

        }

        private void NewRequestCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want to cancel and exit? (Any form entries will be lost)", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                ExitForm();
            }
        }

        // -- COLLECT INPUTS FROM NEW REQUEST FORM -- //
        private Request GetNewRequest()
        {

            Request newRequest = new Request
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

            return newRequest;
        }

        private void ExitForm()
        {
            DependencyObject form = this.Parent;
            (form as ContentControl).Content = null;
        }
    }

}
