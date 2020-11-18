using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace DoAnLTTQ.Backend
{
    class Server
    {

        private Socket socket;
        private bool isRunning = false;
        public object objReceived;
        public void run()
        {
            var listener = new BackgroundWorker();
            // tạo thread mới
            listener.DoWork += new DoWorkEventHandler(init);
            listener.RunWorkerAsync();

            //listener.CancelAsync();
        }
        public void init(object sender, DoWorkEventArgs e)
        {
            var localIp = IPAddress.Any;
            // tiến trình server sẽ sử dụng cổng 1308
            var localPort = 1308;
            // biến này sẽ chứa "địa chỉ" của tiến trình server trên mạng
            var localEndPoint = new IPEndPoint(localIp, localPort);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEndPoint);

            this.listen();
        }
        public void listen()
        {
            isRunning = true;
            var size = 1024 * 4;
            var receiveBuffer = new byte[size];
            while (isRunning)
            {
                // biến này về sau sẽ chứa địa chỉ của tiến trình client nào gửi gói tin tới
                EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                // khi nhận được gói tin nào sẽ lưu lại địa chỉ của tiến trình client
                var length = socket.ReceiveFrom(receiveBuffer, ref remoteEndpoint);

                // tạo ra đối tượng message mới để giải mã thông tin nhận được từ client
                Message m = new Message();
                // giải mã thông tin nhận được từ client
                objReceived = m.reverse(receiveBuffer);
                Array.Clear(receiveBuffer, 0, size);
                //MessageBox.Show("user co tuoi: " + text);
                //MessageBox.Show(text);
            }
        }
        public void stop()
        {
            isRunning = false;

        }
        public void close()
        {
            this.socket.Close();
        }
        public object getResult() { return objReceived; }


    }
}
//class Server
//{
//    private UdpClient socket;
//    private bool isRunning;
//    public Server()
//    {

//        isRunning = false;
//    }
//    public void init()
//    {
//        socket = new UdpClient(6969);
//        Thread socketThread = new Thread(this.start);
//        socketThread.Start();
//    }
//    public void start()
//    {
//        this.isRunning = true;



//        while (isRunning)
//        {
//            IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
//            var buffer = socket.Receive(ref remoteEndpoint);
//            string text = Encoding.ASCII.GetString(buffer);
//            // clear buffer
//            Array.Clear(buffer, 0, buffer.Length);

//            MessageBox.Show(text);
//        }
//    }
//    public void stop()
//    {
//        this.isRunning = false;
//    }
//}
//}
