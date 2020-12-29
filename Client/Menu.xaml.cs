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
using System.Net.Sockets;
using System.Net;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Client";
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            try 
            {
                string ipAddr = ipTxt.Text;
                Int32 port = Int32.Parse(portTxt.Text);
                bool connected = TMNAddons.Client.Connect(ipAddr, port);
                if (connected) 
                {
                    //connected to the server
                    Connection win = new Connection();
                    this.Close();
                    win.Show();
                }
                else
                {
                    //no connection established
                }
            }
            catch(Exception err) 
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
