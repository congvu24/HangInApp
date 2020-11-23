﻿namespace DoAnLTTQ.Views
{
    using DoAnLTTQ.Backend;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
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
        public static bool forceReload = false;
        public List<Picture> _userProfile = new List<Picture>();

        // binding cai nay xuong de hien thi
        //public List<BitmapImage> userPictureNearBy = new List<BitmapImage>();
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
        //
        public List<Picture> userProfile
        {
            get { return this._userProfile; }
            set { this._userProfile = value; this.OnPropertyChanged("userProfile"); }
        }


        public MainView()
        {
            InitializeComponent();
            //Server sv = new Server();
            //var listenProfile = new Thread(sv.ListenProfile);
            //listenProfile.Start();
            //var server = new Thread(sv.ListenRequestMessage);
            //server.Start();
            //var client = new Thread(sv.SendRequestMessage);
            //client.Start();

            //GuestProfile guest = new GuestProfile();
            //guest.LoadProfile();
            //m_userPictureNearBy.Add(Common.LoadImage(guest.avatar.buffer));
           
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


        private void mainsetting_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void NavBarMain_Loaded(object sender, RoutedEventArgs e)
        {
        }

        // add more user
        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            int USER_AMOUNT = 9;
            for (int i = 0; i < USER_AMOUNT; i++)
            {
                GuestProfile guest = new GuestProfile();
                guest.LoadProfile();
                m_userPictureNearBy.Add(Common.LoadImage(guest.avatar.buffer));
            }
        }
    }
}
