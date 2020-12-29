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

namespace Host
{
    /// <summary>
    /// this will be the server side in the TMN project
    /// created by: EBS
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Server";
        }

        private void StartServer(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ans = MessageBox.Show("This App Will Give A Full Access On Your Computer To The User,\nAre you Sure You Want To Continue?", "ATTENTION!", MessageBoxButton.YesNo);
            try
            {
                if (ans == MessageBoxResult.Yes)
                {
                    TMNAddons.Host.StartServer(ipTxt.Text, int.Parse(portTxt.Text));
                    Connected connection = new Connected();
                    this.Close();
                    connection.Show();
                }
            }catch(Exception err) 
            {
                MessageBox.Show(err.ToString(), "ERROR");
            }
        }
    }
}
