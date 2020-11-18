using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DoAnLTTQ.Backend;

namespace DoAnLTTQ.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl, INotifyPropertyChanged
    {
        //public List<User> _userProfile = new List<User>();
        //public List<User> userProfile { get { return this._userProfile; } set { this._userProfile = value; this.OnPropertyChanged("userProfile"); } }

        public List<Picture> _userProfile = new List<Picture>();
        public List<Picture> userProfile { get { return this._userProfile; } set { this._userProfile = value; this.OnPropertyChanged("userProfile"); } }
         
        public MainView()
        {
            InitializeComponent();

            User u = new User();
            //string u = "concac";
            Server s = new Server();
            s.run();

            Client c = new Client();
            c.sendToServer(u);

            s.stop();
            s.close();
            System.Threading.Thread.Sleep(1000);
            //var userURL = s.getResult() as string;
            userProfile.Add((s.getResult() as User).myProfile.avatar);
            //var userURL = user.myProfile.avatar.url.ToString();

            //MessageBox.Show(_userProfile.myProfile.age.ToString());
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //public Profile userProfile { get { return this._userProfile; } set { this._userProfile = value; this.OnPropertyChanged("_userProfile"); } }
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private void mainsetting_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NavBarMain_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
