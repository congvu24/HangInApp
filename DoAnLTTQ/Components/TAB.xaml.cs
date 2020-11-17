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
        public UserControl _TabChange;
        public event ClickOnButtonHandler ButtonSwitchViewOnClick;
        public event ClickOnButtonHandler ButtonSwitchViewByGridOnClick;

        public GridProfile gridProfile = new GridProfile();
        public GridMessage gridMessage = new GridMessage(); 
        public UserControl TabChange
        {
            get { return this._TabChange; }
            set
            {
                _TabChange = value;
                OnPropertyChanged("TabChange");
            }
        }
        public NavBarMain()
        {
            InitializeComponent();

            this.TabChange = gridProfile; 
            this.DataContext = this; 
        }

        public event PropertyChangedEventHandler PropertyChanged;
     
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        private void buttonQuanhDay_Click(object sender, RoutedEventArgs e)
        {
            this.TabChange = gridProfile; 
        }

        private void buttonTinNhan_Click(object sender, RoutedEventArgs e)
        {
            this.TabChange = gridMessage;
            if (ButtonSwitchViewByGridOnClick != null)
                ButtonSwitchViewByGridOnClick(); 

        }

        private void ToHomeViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonSwitchViewOnClick != null)
                ButtonSwitchViewOnClick();
           
        }


        // code here to check connect server-client
        //Server s = new Server();
        private void Button_Click(object sender, RoutedEventArgs e) //  button start server
        {
            //s.run();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // button send message
        {
            sendMessage();
        }
        private void sendMessage()
        {
            Client c = new Client();

            //var u = new User();
            var u = "vcl";
            //var u = "check server";
            //Message m = new Message(u, 2);

            c.sendToServer(u);
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

    }
}
