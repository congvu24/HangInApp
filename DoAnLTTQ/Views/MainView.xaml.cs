﻿namespace DoAnLTTQ.Views
{
    using DoAnLTTQ.Backend;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    

    public partial class MainView : UserControl, INotifyPropertyChanged
    {
        // Guest picture
        public List<Picture> _userProfile = new List<Picture>();
        public event SwitchViewHandler OnSwitchView;


        public List<Picture> userProfile
        {
            get { return this._userProfile; }
            set { this._userProfile = value; this.OnPropertyChanged("userProfile"); }
        }

       
        public MainView()
        {
            InitializeComponent();
            Server sv = new Server();
            var listenProfile = new Thread(sv.ListenProfile);
            listenProfile.Start();
            var server = new Thread(sv.ListenRequestMessage);
            server.Start();
            var client = new Thread(sv.SendRequestMessage);
            client.Start();
            this.DataContext = this;

            NavbarMain.ButtonSwitchViewOnClick += NavbarMain_ButtonSwitchViewOnClick;
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
                mainsetting.Visibility = Visibility.Collapsed;
                messagedetails.Visibility = Visibility.Visible;
            }
            else if (viewEnum == ViewEnum.QuanhDayView)
            {
                mainsetting.Visibility = Visibility.Visible;
                messagedetails.Visibility = Visibility.Collapsed;
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

       
        private void mainsetting_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void NavBarMain_Loaded(object sender, RoutedEventArgs e)
        {
        }
       
    }
}
