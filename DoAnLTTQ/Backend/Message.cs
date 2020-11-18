using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DoAnLTTQ.Backend
{
    class Message
    {
        public int type { get; set; }
        //1: Connect request
        //2: New Guest Profile Comming
        //3: Message
        public byte[] data { get; set; }

        public static void sendMessege(int type, object obj = null)
        {
            switch (type)
            {
                case 1:
                    var serverEndpoint = new IPEndPoint(IPAddress.Broadcast, 1308);
                    var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                    byte[] data = Encoding.ASCII.GetBytes("Hi, add friend with me!");
                    socket.SendTo(data, serverEndpoint);
                    break;

                case 2:
                    User user = new User();
                    GuestProfile u = new GuestProfile(user);
                    var client = (TcpClient)obj;
                    client.Connect(IPAddress.Loopback, 1308);
                    var stream = client.GetStream();
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, u);
                    break;

                case 3:

                    break;
                default:
                    throw new System.ArgumentException("Type is not valid", "type");
                    break;
            }
        }

        

    }
}