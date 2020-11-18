using DoAnLTTQ.Views;
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
using System.ComponentModel;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for NavBarMain.xaml
    /// </summary>
    public partial class NavBarMain : UserControl, INotifyPropertyChanged
    {
        public List<Picture> _picture = new List<Picture>();
        //public List<Picture> picture { get { return this._picture; } set { this._picture = value; this.OnPropertyChanged("userProfile"); } }
        public Picture picture
        {
            get { return (Picture)GetValue(profilePicture); }
            set { SetValue(profilePicture, value); }
        }
        public static readonly DependencyProperty profilePicture =
             DependencyProperty.Register("_picture", typeof(Picture),
               typeof(NavBarMain));

        public NavBarMain()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        // code here to check connect server-client
        //Server s = new Server();
        private void Button_Click(object sender, RoutedEventArgs e) //  button start server
        {
            //s.run();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // button send message
        {
            //sendMessage();
        }
        private void sendMessage()
        {
            Client c = new Client();

            //var u = new User();

            //var u = "check server";
            //Message m = new Message(u, 2);

            //c.sendToServer(u);
            //MessageBox.Show(m.reverse() as string);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // button stop message
        {
            //s.stop();
        }
        private void updatePicture()
        {

        }
        private void sendInfo()
        {

        }
        private void receiveInfo()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
    }
}
