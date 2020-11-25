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
            //this.DataContext = this; 
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
            if (ButtonSwitchViewOnClick != null)
                ButtonSwitchViewOnClick(ViewEnum.QuanhDayView);
        }

        private void buttonTinNhan_Click(object sender, RoutedEventArgs e)
        {
            this.TabChange = gridMessage;
            if (ButtonSwitchViewOnClick != null)
                ButtonSwitchViewOnClick(ViewEnum.MessageView);


        }

        private void ToSettingViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonSwitchViewOnClick != null)
                ButtonSwitchViewOnClick(ViewEnum.SettingView);
           
        }
    }
}
