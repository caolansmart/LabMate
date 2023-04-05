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
    public partial class AmendRequestForm : UserControl
    {
        public AmendRequestForm()
        {
            InitializeComponent();
        }

        // --- EVENT LISTENERS FOR UC --- //
        private void AmendRequestEnterBtn_Click(object sender, RoutedEventArgs e)
        {
            loadingImg.Visibility = Visibility.Visible;

            if (Form.CheckDbForRequest(labRefNoTxtbox.Text))
            {
                ShowAmendRequestForm();
                Request existingRequest = Form.GetExistingRequestFromDb(labRefNoTxtbox.Text);
                UpdateAmendRequestForm(existingRequest);
            }
            else
            {
                MessageBox.Show($"Lab Ref No {labRefNoTxtbox.Text} does not exist.");
            }

            loadingImg.Visibility = Visibility.Hidden;
        }
        private void AmendRequestAcceptBtn_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult msgBoxResult = MessageBox.Show($"Amend Request {labRefNoTxtbox.Text}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                Request oldRequest = Form.GetExistingRequestFromDb(labRefNoTxtbox.Text);
                Request amendedRequest = GetAmendedRequest();
                Form.AmendedRequestToDb(oldRequest, amendedRequest);
                ExitForm();
            }
            
        }

        private void AmendRequestCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want to cancel and exit? (Any form entries will be lost)", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                ExitForm();
            }

        }

        // -- SHOW AMEND REQUEST FORM -- //
        private void ShowAmendRequestForm()
        {
            requestFormGrid.IsEnabled = true;
            requestFormGrid.Visibility = Visibility.Visible;

            amendRequestAcceptBtn.IsEnabled = true;
            amendRequestAcceptBtn.Visibility = Visibility.Visible;

            amendRequestCancelBtn.IsEnabled = true;
            amendRequestCancelBtn.Visibility = Visibility.Visible;

            amendRequestEnterBtn.IsEnabled = false;
            amendRequestEnterBtn.Visibility = Visibility.Hidden;

            labRefNoTxtbox.IsEnabled = false;
        }

        // -- UPDATE AMEND REQUEST FORM -- //
        private void UpdateAmendRequestForm(Request existingRequest)
        {
            labRefNoTxtbox.Text = existingRequest.LabRefNo;
            hcNoTxtbox.Text = existingRequest.HcNo;
            hospitalNoTxtbox.Text = existingRequest.HospitalNo;
            surnameTxtbox.Text = existingRequest.Surname;
            forenameTxtbox.Text = existingRequest.Forename;
            sexCombobox.Text = existingRequest.Sex;
            dobDatepicker.Text = existingRequest.Dob;
            address1Txtbox.Text = existingRequest.Address1;
            address2Txtbox.Text = existingRequest.Address2;
            address3Txtbox.Text = existingRequest.Address3;
            postCodeTxtbox.Text = existingRequest.PostCode;
            hospitalTxtbox.Text = existingRequest.Hospital;
            consGpTxtbox.Text = existingRequest.ConsGp;
            wardTxtbox.Text = existingRequest.Ward;
            specialtyTxtbox.Text = existingRequest.Specialty;
            specimenTypeCombobox.Text = existingRequest.SpecimenType;
            specimenDateDatepicker.Text = existingRequest.SpecimenDate;
            recDateDatepicker.Text = existingRequest.RecdDate;
            flagCodeCombobox.Text = existingRequest.FlagCode;
            urgencyCombobox.Text = existingRequest.Urgency;
        }

        // -- COLLECT INPUTS FROM AMEND REQUEST FORM -- //
        private Request GetAmendedRequest()
        {
            Request amendedRequest = new Request
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

            return amendedRequest;
        }

        // -- EXIT FORM -- //
        private void ExitForm()
        {
            DependencyObject form = this.Parent;
            (form as ContentControl).Content = null;
        }
    }

}
