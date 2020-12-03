using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DoAnLTTQ.Backend
{
    class Client
    {
    }


    //class Client
    //{
    //    // gửi mảng byte s được mã hoá từ đối tượng message đến server
    //    public void sendToServer(byte[] s)
    //    {
    //        //var serverIP = IPAddress.Parse("127.0.0.1");
    //        var serverIP = IPAddress.Broadcast;
    //        //var client = new UdpClient();
    //        //client.Connect(serverIP, 6969);

    //        //var buffer = Encoding.UTF8.GetBytes("vcl");

    //        var buffer = s;
    //        //client.Send(buffer, buffer.Length);

    //        var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
    //        var serverEndpoint = new IPEndPoint(serverIP, 6969);
    //        socket.SendTo(buffer, serverEndpoint);
    //        Array.Clear(buffer, 0, buffer.Length);
    //        socket.Close();




    //        //// đây là "địa chỉ" của tiến trình server trên mạng
    //        //// mỗi endpoint chứa ip của host và port của tiến trình
    //        //var serverEndpoint = new IPEndPoint(serverIp, serverPort);

    //        //var size = 1024; // kích thước của bộ đệm
    //        //var receiveBuffer = new byte[size]; // mảng byte làm bộ đệm  
    //        //var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

    //        //// gửi mảng byte trên đến tiến trình server
    //        //socket.SendTo(s, serverEndpoint);
    //        //Array.Clear(receiveBuffer, 0, size);
    //        //socket.Close();
    //    }
    //}
}
