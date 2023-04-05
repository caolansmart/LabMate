using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace LabMate
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(HashPassword("admin"));
            string hashedPassword = HashPassword(passwordTxtbox.Password);
            User loginCredentials = new User(usernameTxtbox.Text, hashedPassword);
            bool userAuthenticated = Form.AuthenticateLogin(loginCredentials);
            if (userAuthenticated == true)
            {
                MessageBox.Show("Logged in");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
               
            }
            else
            {
                MessageBox.Show("Username and password do not match.");
            }

        }

        private string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);

            string hash = Convert.ToBase64String(hashBytes);
            Console.WriteLine("Hashed password: " + hash);

            return hash;
        }
    }
}
