﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DoAnLTTQ.Backend;
using System.Runtime.CompilerServices;

namespace DoAnLTTQ.Views
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl, INotifyPropertyChanged
    {
        public UserControl _ViewContext;
        public User _user;
        public Profile _profile;
        public List<Picture> _picture = new List<Picture>();
        public User user { get { return this._user; } set { this._user = value; this.OnPropertyChanged("user"); } }
        public Profile profile { get { return this._profile; } set { this._profile = value; this.OnPropertyChanged("profile"); } }
        public List<Picture> picture { get { return this._picture; } set { this._picture = value; this.OnPropertyChanged("picture"); } }

        //Delegate nhan Click tu ST_Left, sau do truyen den MainWindow
        public event SwitchViewHandler OnSwitchView;

        public SettingView()
        {
            InitializeComponent();
            user = new User();
            this.profile = user.myProfile;
            this.picture = user.myProfile.picture;
            this.DataContext = this;
            navbarsetting.UserUpdateProfile += new EventHandler<Profile>(Update_User);
            mainsetting.UserUpdateProfile += new EventHandler<List<Picture>>(Save_User);
            navbarsetting.OnClickBackButton += Navbarsetting_BackToHomeView;
        }

        private void Navbarsetting_BackToHomeView(ViewEnum viewEnum)
        {
            if (viewEnum == ViewEnum.HomeView)
                OnSwitchView();
        }


        private void Update_User(object sender, Profile newProfile)
        {
            bool isAvatarNull = (newProfile.avatar.url == null);
            Picture avatar;
            if (isAvatarNull)
            {
                avatar = new Picture() { name = "avatar", url = this.profile.avatar.url };
            }
            else
            {
                avatar = new Picture() { name = "avatar", url = newProfile.avatar.url };
            }
            this.profile = new Profile(newProfile)
            {
                picture = this.profile.picture,
                avatar = avatar
            };
            user.saveData(this.profile);
        }
        private void Save_User(object sender, List<Picture> newProfile)
        {
            user.saveData(this.profile);
            this.profile = new Profile(this.profile)
            {
                picture = newProfile,
            };
            user.saveData(this.profile);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        private void NavbarSetting_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void navbarsetting_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void mainsetting_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Reload_Profile()
        {
            user = new User();
            this.profile = user.myProfile;
            this.picture = user.myProfile.picture;
            this.DataContext = this;
        }

    }
}
