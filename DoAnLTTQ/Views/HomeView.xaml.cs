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
        public List<Picture> userAvatar = new List<Picture>();
        public HomeView()
        {
            InitializeComponent();
            //User u = new User();
            //Server s = new Server();
            //s.run();

            //Client c = new Client();
            //c.sendToServer(u);

            //s.stop();
            //s.close();
            //System.Threading.Thread.Sleep(1000);
            ////var userURL = s.getResult() as string;
            //User userGet = s.getResult() as User;
            //var userURL = userGet.myProfile.avatar.url.ToString();
            //userAvatar.Add(new Picture() { url = userGet.myProfile.avatar.url });

            this.DataContext = this;
        }

        private void NavBarMain_Loaded(object sender, RoutedEventArgs e)
        {
           

        }
        
    }
}
