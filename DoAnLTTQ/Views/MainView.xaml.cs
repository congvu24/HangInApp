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
        public event SwitchViewHandler OnSwitchView;
        Server sv;
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

            StartingServer(ref sv); 
            //Reload_Guest();

            NavBarMain.gridProfile.ProfileSelected += new EventHandler<int>(GetSelectedProileIndex);
            NavBarMain.gridMessage.ProfileSelected += new EventHandler<string>(changeActiveProfile);
            NavBarMain.ButtonSwitchViewOnClick += NavbarMain_ButtonSwitchViewOnClick;

            this.ViewContext = new info_main();
            this.DataContext = this;
        }

        private void StartingServer(ref Server sv)
        {
            sv = new Server();
            var client = new Thread(sv.ListenRequestMessage);
            client.IsBackground = true;
            client.Start();
            var server = new Thread(sv.ListenProfile);
            server.IsBackground = true;
            server.Start();

            sv.SendRequestMessage();
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
                try
                {
                    ((MessageView_MessageDetails)this.ViewContext).Unmmount();
                    Reload_Guest();
                }
                catch
                {

                }
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
        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            Reload_Guest();
            sv.SendRequestMessage();
        }
        public void Reload_Guest()
        {
            GuestProfile g = new GuestProfile();
            g.LoadArrayProfile();
            int USER_AMOUNT = g.listGuestProfile.Count;

            m_userPictureNearBy.Clear();
            for (int i = 0; i < USER_AMOUNT; i++)
            {
                m_userPictureNearBy.Add(Common.LoadImage(g.listGuestProfile[i].avatar.buffer));
            }
        }

        public void Reload_Profile()
        {
            NavBarMain.Reload_myProfile();
        }
        public void GetSelectedProileIndex(object sender, int index)
        {
            ((info_main)this.ViewContext).ChangeProfile(index);
        }

        private void ContentControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {

                ((info_main)this.ViewContext).ChangeProfileInHomeView(e);
            }
            catch
            {

            }
        }

        public void changeActiveProfile(object sender, string ip)
        {
            ((MessageView_MessageDetails)this.ViewContext).setActiveUser(ip);
        }
    }
}
