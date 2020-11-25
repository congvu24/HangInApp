namespace DoAnLTTQ.Views
{
    using DoAnLTTQ.Backend;
    using DoAnLTTQ.Components;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;


    public partial class MainView : UserControl, INotifyPropertyChanged
    {
        public string temp;

        // pass to info_main
        private ObservableCollection<string> profileIndex = new ObservableCollection<string>();
        public ObservableCollection<string> m_profileIndex
        {
            get { return profileIndex; }
            set
            {
                if (value != profileIndex)
                {
                    profileIndex = value;
                }
            }
        }

        public event SwitchViewHandler OnSwitchView;

        public UserControl _ViewContext;
        public UserControl ViewContext
        {
            get { return this._ViewContext; }
            set
            {
                _ViewContext = value;
                OnPropertyChanged("ViewContext");
            }
        }


        // pass to GridProfile
        private ObservableCollection<BitmapImage> _userPictureNearBy = new ObservableCollection<BitmapImage>();
        public ObservableCollection<BitmapImage> m_userPictureNearBy
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
        public List<Picture> _userProfile = new List<Picture>();
        public List<Picture> userProfile
        {
            get { return this._userProfile; }
            set { this._userProfile = value; this.OnPropertyChanged("userProfile"); }
        }


        public MainView()
        {
            InitializeComponent();

            //var client = new Thread(sv.SendRequestMessage);
            //client.Start();

            NavBarMain.gridProfileName.ProfileSelected += new EventHandler<string>(xinxo);
            //GuestProfile guest = new GuestProfile();
            //guest.LoadProfile();
            //m_userPictureNearBy.Add(Common.LoadImage(guest.avatar.buffer));

            this.ViewContext = new info_main();
            this.DataContext = this;


            NavBarMain.ButtonSwitchViewOnClick += NavbarMain_ButtonSwitchViewOnClick;
        }

        private void NavbarMain_ButtonSwitchViewOnClick(ViewEnum viewEnum)
        {
            if (viewEnum == ViewEnum.SettingView)
            {
                if (OnSwitchView != null)
                    OnSwitchView();
            }
            else if (viewEnum == ViewEnum.MessageView)
            {
                this.ViewContext = new MessageView_MessageDetails();
            }
            else if (viewEnum == ViewEnum.QuanhDayView)
            {
                ((MessageView_MessageDetails)this.ViewContext).Unmmount();
                this.ViewContext = new info_main();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        // add more user
        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            // count number of guest user from guestjson
            // then assign it to USER_AMOUNT

            int USER_AMOUNT = (new Random()).Next(1, 8);
            m_userPictureNearBy.Clear();
            for (int i = 0; i < USER_AMOUNT; i++)
            {
                GuestProfile guest = new GuestProfile();
                //guest.LoadProfile();
                guest.LoadArrayProfile();
                m_userPictureNearBy.Add(Common.LoadImage(guest.avatar.buffer));
            }
        }
        public void xinxo(object sender, string s)
        {
            bool isProfileSeleted = (s == null);
            string tempstring = "this is temp string in mainview";
            if (isProfileSeleted)
                temp = tempstring;
            else
                temp = s;
            profileIndex.Clear();
            profileIndex.Add(temp);

        }
        private void mainsetting_Loaded(object sender, RoutedEventArgs e) { }
        private void NavBarMain_Loaded(object sender, RoutedEventArgs e) { }

    }
}
