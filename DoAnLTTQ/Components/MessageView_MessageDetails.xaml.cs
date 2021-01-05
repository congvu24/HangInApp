using DoAnLTTQ.Backend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Net;
using System.ComponentModel;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

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
        public Thread sendThread;
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

            TextToSend.GotFocus += RemoveText;
            TextToSend.LostFocus += AddText;
            TextToSend.Document.Blocks.Clear();
            TextToSend.Document.Blocks.Add(new Paragraph(new Run("Type something")));

            TextBox tb = new TextBox();

            sv = new Server();

            this.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;

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
            if (txtName.Text.Length > 0)
                //if ((txtName.Content as string).Length > 0)
            {
                if (TextToSend.Text.Length > 0)
                {
                    sendThread = new Thread(() =>
                    {
                        try
                        {
                            sv.SendMessage(IPAddress.Parse(activeGuest.ip), content);
                    }
                        catch
                    {
                        this.sendThread.Abort();
                    }
                }
                );
                    sendThread.IsBackground = true;
                    sendThread.Start();
                    messagePanel.Children.Add(new MyMessage(content));
                }
            }
        }
        public void sendImage(string path)
        {
            if (activeGuest == null)
            {

                MessageBox.Show("Select a friend to send this message!");
                return;
            }
            if (txtName.Text.Length > 0)
            //if ((txtName.Content as string).Length > 0)
            {
                if (TextToSend.Text.Length > 0)
                {
                    sendThread = new Thread(() =>
                    {
                        try
                        {
                            sv.SendImage(IPAddress.Parse(activeGuest.ip), path);
                        }
                        catch
                        {
                            this.sendThread.Abort();
                        }
                    }
                );
                    sendThread.IsBackground = true;
                    sendThread.Start();
                    messagePanel.Children.Add(new MyImage(path));
                }
            }
        }
        public void receiveMessage(string ip, int type, byte[] content)
        {
            var guest = new GuestProfile();
            if (guest.isExist(ip) == true && ip == activeGuest.ip)
            {
                if(type == 1)
                {
                    
                    var text = Encoding.UTF8.GetString(content, 0, content.Length);
                    this.Dispatcher.Invoke(() =>
                    {
                        messagePanel.Children.Add(new InComingMessage(text));
                    });
                }
                else if(type == 2)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        messagePanel.Children.Add(new InCommingImage(content));
                    });
                }
            }

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var content = TextToSend.Text;
            sendMessage(content);
            TextToSend.Document.Blocks.Clear();
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            //this.sv.DisconnectAll();
        }
        public void Unmmount()
        {
            //this.sv.DisconnectAll();
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
            if (e.Key == Key.Return)
                try
                {
                    SendButton_Click(sender, e);
                    e.Handled = true;
                }
                catch (Exception)
                {

                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = true;
            TextToSend.Document.Blocks.Clear();
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



        public void RemoveText(object sender, EventArgs e)
        {

            if (TextToSend.Text == "Type something")
            {
                TextToSend.Document.Blocks.Clear();
                TextToSend.Document.Blocks.Add(new Paragraph(new Run("")));
            }
        }

        public void AddText(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TextToSend.Text))
            {
                TextToSend.Document.Blocks.Clear();
                TextToSend.Document.Blocks.Add(new Paragraph(new Run("" + "Type something")));
            }
        }

        private void SendImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "";

            System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                openFileDialog.Filter = String.Format("{0}{1}{2} ({3})|{3}", openFileDialog.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            openFileDialog.Filter = String.Format("{0}{1}{2} ({3})|{3}", openFileDialog.Filter, sep, "All Files", "*.*");

            openFileDialog.DefaultExt = ".png"; // Default file extension 
            if (openFileDialog.ShowDialog() == true)
                try
                {
                    var path = openFileDialog.FileName;
                    sendImage(path);
                }
                catch { }
        }

        private void Border_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as Border).Background = Brushes.LightGray;
        }

        private void Border_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as Border).Background = (Brush)(new BrushConverter().ConvertFrom("#E8E8E8"));
            
        }

        private void SendButton_GotMouseCapture(object sender, MouseEventArgs e)
        {
            (sender as PackIcon).Foreground = Brushes.Red;
            MessageBox.Show("aa");
        }

        private void packIcon1_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var color = (sender as PackIcon).Foreground;
            if (color == Brushes.White)
                color = Brushes.Red;
            else
                color = Brushes.White;

        }

        

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            packIcon1.Foreground = Brushes.Red;
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            packIcon1.Foreground = Brushes.White;

        }

        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            packIcon2.Foreground = Brushes.Red;
        }

        private void Button_MouseLeave_1(object sender, MouseEventArgs e)
        {
            packIcon2.Foreground = Brushes.White;

        }
        private void Button_MouseEnter_2(object sender, MouseEventArgs e)
        {
            packIcon3.Foreground = Brushes.Red;
        }

        private void Button_MouseLeave_2(object sender, MouseEventArgs e)
        {
            packIcon3.Foreground = Brushes.White;

        }

        private void IconButtonOnHover(object sender, MouseEventArgs e)
        {


            var b= (Brush)(new BrushConverter().ConvertFrom("#F2F1EF"));
            (sender as Button).Background = b;
        }

        private void IconButtonLostHover(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = Brushes.Transparent;

        }

        private void emojiPanel_Loaded(object sender, RoutedEventArgs e)
        {
            List<Button> childs = GetChildrenOfType<Button>(emojiPanel);

            foreach (var child in childs)
            {
                child.MouseEnter += IconButtonOnHover;
                child.MouseLeave += IconButtonLostHover;
            }
        }
        public static List<T> GetChildrenOfType<T>(DependencyObject depObj)
  where T : DependencyObject
        {
            var result = new List<T>();
            if (depObj == null) return null;
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(depObj);
            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();
                var childrenCount = VisualTreeHelper.GetChildrenCount(currentElement);
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(currentElement, i);
                    if (child is T)
                        result.Add(child as T);
                    queue.Enqueue(child);
                }
            }

            return result;
        }
    }

}
