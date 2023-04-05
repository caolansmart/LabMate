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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabMate
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


        // TOOLBAR BUTTON EVENT LISTENERS //
        private void ToolbarBtn_Click(object sender, RoutedEventArgs e)
        {
            string senderName = (sender as Button).Name;
            switch (senderName)
            {
                case "homeBtn": FormContentControl.Content = null; break;
                case "newRequestBtn": FormContentControl.Content = new NewRequestForm(); break;
                case "amendRequestBtn": FormContentControl.Content = new AmendRequestForm(); break;
                case "findRequestBtn": FormContentControl.Content = new FindRequestForm(); break;
                
                default:
                    break;
            }

        }

    }
}
