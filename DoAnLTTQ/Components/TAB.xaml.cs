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
using System.Collections.ObjectModel;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for NavBarMain.xaml
    /// </summary>
    public partial class NavBarMain : UserControl, INotifyPropertyChanged
    {
        public UserControl _TabChange;
        public bool buttonActivated = false;


        private ObservableCollection<BitmapImage> _userPictureNearBy = new ObservableCollection<BitmapImage>();
        public ObservableCollection<BitmapImage> n_userPictureNearBy
        {
            get { return _userPictureNearBy; }
            set
            {
                if (value != _userPictureNearBy)
                {
                    _userPictureNearBy = value;
                }
            }
        }

        //public GridProfile gridProfile = new GridProfile();
        //public GridMessage gridMessage = new GridMessage(); 
        public event ClickOnButtonHandler ButtonSwitchViewOnClick;
        public UserControl TabChange
        {
            get { return this._TabChange; }
            set
            {
                _TabChange = value;
                OnPropertyChanged("TabChange");
            }
        }
        public User _myUser;
        public User myUser
        {
            get { return this._myUser; }
            set
            {
                _myUser = value;
                OnPropertyChanged("myUser");
            }
        }
        public NavBarMain()
        {
            InitializeComponent();

            //this.TabChange = gridProfile; 
            //this.DataContext = this; 
            gridProfile.Visibility = Visibility.Visible;
            gridMessage.Visibility = Visibility.Collapsed;
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
            // go to parent to find reload button and enable it
            FrameworkElement u = this.Parent as FrameworkElement;
            for (int i = 5; i > 0; i--)
            {
                u = u.Parent as FrameworkElement;
            }
            (u.FindName("ReloadButton1") as Button).Visibility = Visibility.Visible;

            (sender as Button).IsEnabled = false;
            buttonTinNhan.IsEnabled = true;
            gridProfile.Visibility = Visibility.Visible;
            gridMessage.Visibility = Visibility.Collapsed;
            if (ButtonSwitchViewOnClick != null)
            {
                ButtonSwitchViewOnClick(ViewEnum.QuanhDayView);
                Reload_Guest();
            }
        }
        public void Reload_Guest()
        {
            GuestProfile g = new GuestProfile();
            g.LoadArrayProfile();
            int USER_AMOUNT = g.listGuestProfile.Count;

            n_userPictureNearBy.Clear();
            for (int i = 0; i < USER_AMOUNT; i++)
            {
                n_userPictureNearBy.Add(Common.LoadImage(g.listGuestProfile[i].avatar.buffer));
            }
        }


        private void buttonTinNhan_Click(object sender, RoutedEventArgs e)
        {
            // go to parent to find reload button and disable it
            FrameworkElement u = this.Parent as FrameworkElement;
            for (int i = 5; i > 0; i--)
            {
                u = u.Parent as FrameworkElement;
            }
            (u.FindName("ReloadButton1") as Button).Visibility = Visibility.Collapsed;

            (sender as Button).IsEnabled = false;
            buttonQuanhDay.IsEnabled = true;

            gridProfile.Visibility = Visibility.Collapsed;
            gridMessage.Visibility = Visibility.Visible;
            gridMessage.reload();
            if (ButtonSwitchViewOnClick != null) { }
            ButtonSwitchViewOnClick(ViewEnum.MessageView);
        }

        private void ToSettingViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonSwitchViewOnClick != null)
                ButtonSwitchViewOnClick(ViewEnum.SettingView);
            Reload_myProfile();
        }
        public void Reload_myProfile()
        {
            myUser = new User();
            this.profile.DataContext = myUser.myProfile;
        }
    }
}
