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
    [Serializable]
    public class Message
    {
        public int type { get; set; }
        //1: Text
        //2: Image
        public byte[] data { get; set; }
    }
}