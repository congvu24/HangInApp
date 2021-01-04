using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for GridProfile.xaml
    /// </summary>
    public partial class GridProfile : UserControl, INotifyPropertyChanged
    {
        Stack<Button> SelectedButtonList = new Stack<Button>();
        public event EventHandler<int> ProfileSelected;

        public ObservableCollection<BitmapImage> userPictureNearBy
        {
            get { return (ObservableCollection<BitmapImage>)GetValue(userPictureNearByProperty); }
            set { SetValue(userPictureNearByProperty, value); }
        }

        public static readonly DependencyProperty userPictureNearByProperty =
            DependencyProperty.Register("userPictureNearBy", typeof(ObservableCollection<BitmapImage>), typeof(GridProfile),
                                        new PropertyMetadata(new ObservableCollection<BitmapImage>(), OnChanged));

        static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as GridProfile).OnChanged();
        }

        void OnChanged()
        {
            if (userPictureNearBy != null)
                userPictureNearBy.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(userPictureNearBy_CollectionChanged);
        }

        //detect changes
        void userPictureNearBy_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            int AMOUNT = userPictureNearBy.Count();
            List<Source> source = new List<Source>();
            for (int i = 0; i < AMOUNT; i++)
                source.Add(new Source() { item = userPictureNearBy[i], name = i.ToString() });

            listImage.DataContext = source;
        }

        // get all children of a dependency object
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
        public class Source
        {
            public BitmapImage item { get; set; }
            public string name { get; set; }
        }
        public GridProfile()
        {
            InitializeComponent();
            List<Source> source = new List<Source>();

            listImage.DataContext = source;

        }
        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image returnImage = null;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                returnImage = System.Drawing.Image.FromStream(ms);
            }
            return returnImage;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private void select_Click(object sender, RoutedEventArgs e)
        {
            int identify = int.Parse((sender as Button).Uid);
            if (ProfileSelected != null)
            {
                ProfileSelected(this, identify);
            }

            HighlightButton(identify);
        }

        public void HighlightButton(int index)
        {
            List<Button> btnList = GetChildrenOfType<Button>(listImage);
            foreach (var item in btnList)
            {
                if (item.Uid == index.ToString())
                {
                    DropShadowEffect dropShadowEffect = new DropShadowEffect
                    {
                        Color = new System.Windows.Media.Color { R = 244, G = 238, B = 237 },
                        Direction = 320,
                        ShadowDepth = 1,
                        Opacity = 1
                    };
                    item.BorderBrush = System.Windows.Media.Brushes.Transparent;
                    item.Effect = dropShadowEffect;

                    if (item.Width == 75 && item.Height == 106)
                    {
                        item.Width += 6;
                        item.Height += 6;
                    }

                }
                else
                //item.BorderBrush = System.Windows.Media.Brushes.Transparent;
                {
                    item.BorderBrush = FindResource("ProfileBorderBrush") as System.Windows.Media.Brush;

                    if (item.Width > 75 && item.Height > 106)
                    {
                        item.Width -= 6;
                        item.Height -= 6;
                    }
                    item.Effect = null;
                }
            }

            //Button lastSlect = SelectedButtonList.Pop();
            //if (lastSlect != null)
            //{
            //    DropShadowEffect dropShadowEffect = new DropShadowEffect
            //    {
            //        Color = new System.Windows.Media.Color { R = 250, G = 135, B = 47 },
            //        Direction = 320,
            //        ShadowDepth = 1,
            //        Opacity = 1
            //    };
            //    lastSlect.Effect = dropShadowEffect;
            //}
            //Button currentSelect = 
            //if (ProfileSelected != null)
            //{
            //    ProfileSelected(this, index);
            //}

        }
    }
}
