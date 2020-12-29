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

namespace Client
{
    /// <summary>
    /// Interaction logic for Connection.xaml
    /// </summary>
    public partial class Connection : Window
    {
        public Connection()
        {
            InitializeComponent();
            
        }

        private void GetProcessFromServer(object sender, RoutedEventArgs e)
        {
            TMNAddons.Client.SendData("GAP");
            string processes = TMNAddons.Client.GetData();
            writeData.Text = processes;
        }
        private void KillProcessById(object sender, EventArgs e) 
        {
            string id;
        }
    }
}
