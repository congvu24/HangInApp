﻿using DoAnLTTQ.Backend;
using DoAnLTTQ.Components;
using DoAnLTTQ.Views;
using System;
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

namespace DoAnLTTQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public UserControl _ViewContext;
        public User _user;
        public Profile _profile;
        public List<Picture> _picture = new List<Picture>();
        public User user { get { return this._user; } set { this._user = value; this.OnPropertyChanged("user"); } }
        public Profile profile { get { return this._profile; } set { this._profile= value; this.OnPropertyChanged("profile"); } }
        public List<Picture> picture { get { return this._picture; } set { this._picture = value; this.OnPropertyChanged("picture"); } }
        private client cl;
        private server sv = new server();

        public UserControl ViewContext { get { return this._ViewContext; } set {

                _ViewContext = value;
                OnPropertyChanged("ViewContext");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            user = new User();
            this.profile = user.myProfile;
            navbarsetting.UserUpdateProfile += new EventHandler<Profile>(Update_User);
            mainsetting.UserUpdateProfile += new EventHandler<List<Picture>>(Save_User);
            this.picture = user.myProfile.picture;

            this.DataContext = this;
            this.ViewContext = new SettingView();
            //ButtonName.UserControlButtonClicked += new EventHandler<data>(MyUserControl_UserControlButtonClicked);

            
        }

        class client
        {
            public void sendToServer()
            {
                var ip = IPAddress.Parse("127.0.0.1");
                var client = new UdpClient();
                client.Connect(ip, 6969);
                var buffer = Encoding.UTF8.GetBytes("vcl");
                client.Send(buffer, buffer.Length);
            }
        }
        class server
        {
            private UdpClient listner;
            private bool isRunning;
            public   server()
            {
                  
                 isRunning = false;
        }
            public void run()
            {
                listner = new UdpClient(6969);
                Thread listnerThread = new Thread(this.start);
                listnerThread.Start();
            }
            public void start() 
                {
                this.isRunning = true;
                    while (isRunning)
                    {
                        var remoteEp = new IPEndPoint(0, 0);
                        var buffer = listner.Receive(ref remoteEp);
                        var text = Encoding.ASCII.GetString(buffer);
                        MessageBox.Show(text);
                    }
                } 
            public void stop()
            {
                this.isRunning = false;
            }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ViewContext = new SettingView();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.ViewContext = new HomeView();
        }
        private void Message_Click(object sender, RoutedEventArgs e)
        {
            this.ViewContext = new MessageView();
        }
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.cl = new client();
            cl.sendToServer();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.sv.run();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.sv.stop();
        }
    }

}
