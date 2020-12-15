using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using DoAnLTTQ.Backend;
using DoAnLTTQ.Components;

namespace DoAnLTTQ.Backend
{
    public class Server
    {
        public delegate void handleReceiveMessage(string content);

        public handleReceiveMessage myDelegate;

        public Socket MessageListener;
        public TcpListener ProfileListener;


        public void DisconnectAll()
        {
            if (this.MessageListener!= null)
            { 
                MessageListener.Close();
            }
            if(this.ProfileListener != null)
            {
                ProfileListener.Stop();
            }
        }
        public void ListenProfile()
        {
            ProfileListener = new TcpListener(IPAddress.Any, 1308);
            ProfileListener.Start(20);
            while (true)
            {
                var client = ProfileListener.AcceptTcpClient();
                var stream = client.GetStream();
                var formatter = new BinaryFormatter();
                var guest = formatter.Deserialize(stream) as GuestProfile;
                var myGuest = guest;
                guest.AddNewGuest(myGuest);
                client.Close();
            }
        }
        public void SendProfile(IPAddress ip)
        {
            User user = new User();
            GuestProfile u = new GuestProfile(user);
            var localIp = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var myip in host.AddressList)
            {
                if (myip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = myip.ToString();
                }
            }
            u.ip = localIp;

            var client = new TcpClient();
            client.Connect(ip, 1308);
            var stream = client.GetStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, u);

            client.Close();
        }

        public void ListenRequestMessage()
        {
            var localIp = IPAddress.Any;
            var localPort = 1308;
            var localEndPoint = new IPEndPoint(localIp, localPort);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEndPoint);

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
                EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                var length = socket.ReceiveFrom(receiveBuffer, ref remoteEndpoint);
                var text = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                SendProfile(IPAddress.Parse(text));
                Array.Clear(receiveBuffer, 0, size);
            }
        }

        public void SendRequestMessage()
        {
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            var localIp = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                }
            }
            byte[] data = Encoding.ASCII.GetBytes(localIp);

            NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface Interface in Interfaces)
            {
                if (Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
                if (Interface.OperationalStatus != OperationalStatus.Up) continue;
                Console.WriteLine(Interface.Description);
                UnicastIPAddressInformationCollection UnicastIPInfoCol = Interface.GetIPProperties().UnicastAddresses;
                foreach (UnicastIPAddressInformation UnicatIPInfo in UnicastIPInfoCol)
                {
                    var serverEndpoint = new IPEndPoint(IPAddress.Parse(GetBroadCastIP(UnicatIPInfo.Address, UnicatIPInfo.IPv4Mask).ToString()), 1308);
                    socket.SendTo(data, serverEndpoint);
                }
            }
            socket.Close();
        }

        public void ListenMessage()
        {
            try
            {
                var localIp = IPAddress.Any;
                var localPort = 1309;
                var localEndPoint = new IPEndPoint(localIp, localPort);
                MessageListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                MessageListener.Bind(localEndPoint);
                MessageListener.Listen(10);

                var size = 1024;
                var receiveBuffer = new byte[size];

                while (true)
                {
                    var socket = MessageListener.Accept();
                    var length = socket.Receive(receiveBuffer);
                    socket.Shutdown(SocketShutdown.Receive);
                    var text = Encoding.UTF8.GetString(receiveBuffer, 0, length);
                    this.myDelegate(text);
                    socket.Close();
                    Array.Clear(receiveBuffer, 0, size); 
                }
            }
            catch 
            {
            }

    }
    
        public void SendMessage(IPAddress ip, String content)
        {
            try
            {
                var serverEndpoint = new IPEndPoint(ip, 1309);
                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(serverEndpoint);
                var sendBuffer = Encoding.UTF8.GetBytes(content);
                socket.Send(sendBuffer);
                socket.Shutdown(SocketShutdown.Send);
            }
            catch
            {
                MessageBox.Show("You two haven't connected with each other");
            }
        }
        public IPAddress GetBroadCastIP(IPAddress host, IPAddress mask)
        {
            byte[] broadcastIPBytes = new byte[4];
            byte[] hostBytes = host.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();
            for (int i = 0; i < 4; i++)
            {
                broadcastIPBytes[i] = (byte)(hostBytes[i] | (byte)~maskBytes[i]);
            }
            return new IPAddress(broadcastIPBytes);
        }
    }
}

