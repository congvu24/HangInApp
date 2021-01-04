using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DoAnLTTQ.Backend;
using Microsoft.Win32;
using System.ComponentModel;
using System.Collections;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for MainSetting.xaml
    /// </summary>
    public partial class MainSetting : UserControl, INotifyPropertyChanged
    {
        public event EventHandler<List<Picture>> UserUpdateProfile;
        public IEnumerable picture
        {
            get { return (IEnumerable)GetValue(vclProperty); }
            set { SetValue(vclProperty, value); }
        }
        public static readonly DependencyProperty vclProperty =
             DependencyProperty.Register("picture", typeof(IEnumerable),
               typeof(MainSetting), new PropertyMetadata(""));
        public MainSetting()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private void Slot_Click(object sender, RoutedEventArgs e)
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

            var brush = new ImageBrush();
            if (openFileDialog.ShowDialog() == true)
            {
                brush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                BitmapImage image = new BitmapImage(new Uri(openFileDialog.FileName));
                ((sender as Button).FindName("brush") as ImageBrush).ImageSource = image;
                //MessageBox.Show(((sender as Button).FindName("brush") as ImageBrush).ImageSource.ToString());
                (sender as Button).Content = "";
            }
            if(brush.ImageSource != null)
            {
            Button newbutton = sender as Button;
            FrameworkElement t = newbutton.Parent as FrameworkElement;
            (t as Border).BorderBrush = Brushes.Transparent;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<Picture> newList = new List<Picture>();
            //
            var result = new List<Button>();
            if (listImage == null) result = null;
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(listImage);
            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();
                var childrenCount = VisualTreeHelper.GetChildrenCount(currentElement);
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(currentElement, i);
                    if (child is Button)
                        result.Add(child as Button);
                    queue.Enqueue(child);
                }
            }
            //
            foreach (Button item in result)
            {
                ImageSource data = (item.FindName("brush") as ImageBrush).ImageSource;
                if (data != null)
                {
                    newList.Add(new Picture() { name = "hinh", url = data.ToString() });
                }
            }
            OnUserControlButtonClick(newList);
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            //OnUserControlButtonClick();

        }
        private void OnUserControlButtonClick(List<Picture> newPicture)
        {
            if (UserUpdateProfile != null)
            {

                UserUpdateProfile(this, newPicture);
            }
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            if (((sender as Button).FindName("brush") as ImageBrush).ImageSource != null)
            {
                (sender as Button).Content = "";
            }
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Border).FindName("avt") as Button;
            if ((btn.FindName("brush") as ImageBrush).ImageSource != null)
                (sender as Border).BorderBrush = null;
        }
    }


}
