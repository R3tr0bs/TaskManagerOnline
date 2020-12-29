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
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace Host
{
    /// <summary>
    /// Interaction logic for Connected.xaml
    /// </summary>
    public partial class Connected : Window
    {
        TcpClient clnt;
        public Connected()
        {
            InitializeComponent();
            
        }

        private void GetConnection(object sender, RoutedEventArgs e)
        {
            clnt = TMNAddons.Host.GetConnection();
            statusTxt.Content = "Got Connection From: " + clnt.Client.RemoteEndPoint.ToString();
            string command = TMNAddons.Host.GetData(clnt);
            TMNAddons.Host.ExecuteCommand(command, clnt);
        }
    }
}
