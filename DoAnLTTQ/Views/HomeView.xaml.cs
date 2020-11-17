using DoAnLTTQ.Backend;
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

namespace DoAnLTTQ.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public event SwitchViewHandler OnSwitchView; 
        public HomeView()
        {
            InitializeComponent();
            TabMain.ButtonSwitchViewOnClick += TabMain_ButtonSwitchViewOnClick;
        }

        private void TabMain_ButtonSwitchViewOnClick()
        {
            OnSwitchView();
        }

        private void NavBarMain_Loaded(object sender, RoutedEventArgs e)
        {
            Server s = new Server();
            s.run();
            
            var o = s.getResult() as string;
            MessageBox.Show(o);
            //Client c = new Client();
            //var u = new User();
            //c.sendToServer(u);
           
        }
        
    }
}
