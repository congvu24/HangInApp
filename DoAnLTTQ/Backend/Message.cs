using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DoAnLTTQ.Backend
{
    class Message
    {
        public int tag { get; set; }
        byte[] data;
        public Message() { }
        public Message(object o, int type)
        {
            tag = type;
            data = selfConvert(o);
        }
        public byte[] selfConvert(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                data = memoryStream.ToArray();
                return data;
            }
        }
        public byte[] convert()
        {
            return data;
        }
        public object reverse(byte[] b)
        {
            data = b;
            return this.reverse();
        }
        
        public object reverse()
        {
            using (var memoryStream = new MemoryStream(data))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }
        // usage: image i = new image()
        //Message m = new Message(i, tag);
        ////convert to send
        //m.convert();
        //// reverse to original data
        //m.reverse() as <data type>

    }

}
    // backup old code
    //public class imageMessage
    //{

    //    public byte[] encode(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
    //    {
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            byte[] encodedMessage;
    //            // Convert Image to byte[]
    //            image.Save(ms, format);
    //            encodedMessage = ms.ToArray();
    //            return encodedMessage;
    //        }
    //    }
    //    public System.Drawing.Image decode(byte[] encodedMessage)
    //    {

    //        using (var ms = new MemoryStream(encodedMessage, 0, encodedMessage.Length))
    //        {
    //            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
    //            return image;
    //        }
    //    }
    //}
    //public class stringMessage
    //{
    //    public byte[] encode(string s)
    //    {
    //        var encodedMessage = Encoding.ASCII.GetBytes(s);
    //        return encodedMessage;
    //    }
    //    public string decode(byte[] encodedMessage)
    //    {
    //        var data = Encoding.ASCII.GetString(encodedMessage, 0, encodedMessage.Length);
    //        return data;
    //    }
    //}
    //class Message
    //{
    //    public int tag { get; set; }
    //    //byte[] sender;
    //    byte[] data;
    //    object receive;

    //    public void initMessage(object o)
    //    {
    //        receive = o;
    //    }

    //    public byte[] Serialize(object anySerializableObject)
    //    {
    //        using (var memoryStream = new MemoryStream())
    //        {
    //            (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
    //            //return new Message { data = memoryStream.ToArray() };
    //            data = memoryStream.ToArray();
    //            return data;
    //        }
    //    }

    //    public object Deserialize(Message message)
    //    {
    //        using (var memoryStream = new MemoryStream(message.data))
    //            return (new BinaryFormatter()).Deserialize(memoryStream);
    //    }


    //    //public object reverse(byte[] send) // revser message from byte[]
    //    //{
    //    //    switch (tag)
    //    //    {
    //    //        case 0:
    //    //            stringMessage s = new stringMessage();

    //    //            string result = s.decode(send); // 1024 is buffer length
    //    //            receive = result;
    //    //            break;
    //    //        case 1:
    //    //            imageMessage im = new imageMessage();

    //    //            Image i = im.decode(send);
    //    //            receive = i;
    //    //            break;
    //    //        //case 2:
    //    //        //    Profile go here
    //    //        default:
    //    //            break;
    //    //    }
    //    //    return receive;
    //    //}
    //    //public byte[] convert(object receiver) // convert from object to byte[]
    //    //{
    //    //    switch (tag)
    //    //    {
    //    //        case 0:
    //    //            stringMessage s = new stringMessage();
    //    //            sender = s.encode((receiver as string));
    //    //            break;
    //    //        case 1:
    //    //            imageMessage i = new imageMessage();
    //    //            sender = i.encode(receiver as Image, (receiver as Image).RawFormat);
    //    //            break;
    //    //        case 2:
    //    //            Profile p = new Profile();
    //    //            sender = p.encode((receiver as Profile));
    //    //            break;
    //    //        default:
    //    //            break;
    //    //    }
    //    //    return sender;
    //    //}
    //}