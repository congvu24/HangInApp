﻿using DoAnLTTQ.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.ComponentModel;
using MaterialDesignThemes.Wpf;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for MessageView_MessageDetails.xaml
    /// </summary>
    public partial class MessageView_MessageDetails : UserControl, INotifyPropertyChanged
    {
        public Server sv;
        public String activeIp;
        public Thread listenMessage;
        public GuestProfile _activeGuest;
        GuestProfile guests;
        List<GuestProfile> list;

        public GuestProfile activeGuest
        {
            get { return this._activeGuest; }
            set
            {
                _activeGuest = value;
                OnPropertyChanged("activeGuest");
            }
        }

        public MessageView_MessageDetails()
        {
            InitializeComponent();

            TextBox tb = new TextBox();

            //activeIp = "10.10.233.158";
            sv = new Server();

            this.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;

            sv.myDelegate = new Server.handleReceiveMessage(this.receiveMessage);

            listenMessage = new Thread(sv.ListenMessage);
            listenMessage.IsBackground = true;

            listenMessage.Start();

            guests = new GuestProfile();
            list = guests.LoadArrayProfile();
            List<GuestProfile> friends = new List<GuestProfile>();
            foreach (var u in list)
            {
                if (u.isLove == true)
                {
                    friends.Add(u);
                }
            }
            if (friends.Count > 1)
            {
                activeGuest = friends[0];
            }

            this.DataContext = activeGuest;
        }

        public void setActiveUser(string ip)
        {
            activeGuest = list.Find(x => x.ip == ip);
            this.DataContext = activeGuest;
            messagePanel.Children.Clear();
        }
        public void sendMessage(string content)
        {
            if (activeGuest == null)
            {

                MessageBox.Show("Select a friend to send this message!");
                return;
            }
            if (txtname.Text.Length > 0)
            {
                if (TextToSend.Text.Length > 0)
                {
                    sv.SendMessage(IPAddress.Parse(activeGuest.ip), content);
                    messagePanel.Children.Add(new MyMessage(content));
                    //TextToSend
                }
            }
        }
        public void receiveMessage(string content)
        {
            this.Dispatcher.Invoke(() =>
            {
                messagePanel.Children.Add(new InComingMessage(content));
            });
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var content = TextToSend.Text;
            sendMessage(content);

        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            this.sv.DisconnectAll();
        }
        public void Unmmount()
        {
            this.sv.DisconnectAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        private void TextToSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                try
                {
                    SendButton_Click(sender, e);
                }
                catch (Exception)
                {

                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = true;
            //DialogHost.Show(emojiPanel, "emojiDialog");
            //như dòng 126 mainview.cs
            //cách tắt https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Dialogs#dialoghostshow

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var currentText = TextToSend.Text;
            TextToSend.Document.Blocks.Clear();
            TextToSend.Document.Blocks.Add(new Paragraph(new Run(currentText + (((sender as Button).Content) as Emoji.Wpf.TextBlock).Text)));
        }

        private void messagePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chatField.ScrollToBottom();
        }
    }

}
