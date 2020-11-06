using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoAnLTTQ.Backend
{

    public class imageMessage
    {

        public byte[] encode(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] encodedMessage;
                // Convert Image to byte[]
                image.Save(ms, format);
                encodedMessage = ms.ToArray();
                return encodedMessage;
            }
        }
        public System.Drawing.Image decode(byte[] encodeMessage)
        {

            using (var ms = new MemoryStream(encodeMessage, 0, encodeMessage.Length))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                return image;
            }
        }
    }
    public class stringMessage
    {
        public byte[] encode(string s)
        {
            var encodedMessage = Encoding.ASCII.GetBytes(s);
            return encodedMessage;
        }
        public string decode(byte[] receiveBuffer)
        {
            var data = Encoding.ASCII.GetString(receiveBuffer, 0, receiveBuffer.Length);
            return data;
        }
    }
    class message
    {
        public int tag { get; set; }
        byte[] sender;
        object receive;

        public void initMessage(object o)
        {
            receive = o;
        }

        public object reverse(byte[] send) // revser message from byte[]
        {
            switch (tag)
            {
                case 0:
                    stringMessage s = new stringMessage();

                    string result = s.decode(send); // 1024 is buffer length
                    receive = result;
                    break;
                case 1:
                    imageMessage im = new imageMessage();

                    Image i = im.decode(send);
                    receive = i;
                    break;
                //case 2:
                //    Profile go here
                default:
                    break;
            }
            return receive;
        }
        public byte[] convert(object receiver) // convert from object to byte[]
        {
            switch (tag)
            {
                case 0:
                    stringMessage s = new stringMessage();
                    sender = s.encode((receiver as string));
                    break;
                case 1:
                    imageMessage i = new imageMessage();
                    sender = i.encode(receiver as Image, (receiver as Image).RawFormat);
                    break;
                //case 2:
                //    Profile go here
                default:
                    break;
            }
            return sender;
        }
    }
}