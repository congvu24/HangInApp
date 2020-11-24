using DoAnLTTQ.Backend;
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

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for MessageView_MessageDetails.xaml
    /// </summary>
    public partial class MessageView_MessageDetails : UserControl
    {
        public Server sv;
        public String activeIp;
        public Thread listenMessage;
        public MessageView_MessageDetails()
        {
            InitializeComponent();

            ChatList chatList = new ChatList() { imgUri = "/Resources/Images/IMG_9715.png", personName = "Công Vũ", message = "" };

            //messageNameTitle.DataContext = chatList; 
            TextBox tb = new TextBox();

            activeIp = "127.0.0.1";
            sv = new Server();

            this.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;

            sv.myDelegate = new Server.handleReceiveMessage(this.receiveMessage);

            listenMessage = new Thread(sv.ListenMessage);
            listenMessage.IsBackground = true;

            listenMessage.Start();
        }
        public void sendMessage(string content)
        {
            sv.SendMessage(IPAddress.Parse(activeIp), content);
            messagePanel.Children.Add(new MyMessage(content));
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
            //var content = TextToSend.Text;
            //sendMessage(content);
            this.listenMessage.Abort();

        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            this.listenMessage.Abort();
            this.sv.DisconnectAll();
        }
        public void Unmmount()
        {
            //this.listenMessage.

            this.sv.DisconnectAll();
        }
    }
}
