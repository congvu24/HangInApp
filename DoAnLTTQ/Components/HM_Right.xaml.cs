using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DoAnLTTQ.Backend;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for info_main.xaml
    /// </summary>
    public partial class info_main : UserControl
    {
        //GuestProfile guest;
        byte[] avatar;
        public GuestProfile specialGuest;

        public ObservableCollection<string> detail
        {
            get { return (ObservableCollection<string>)GetValue(detailProperty); }
            set { SetValue(detailProperty, value); }
        }
        public static readonly DependencyProperty detailProperty =
            DependencyProperty.Register("detail", typeof(ObservableCollection<string>), typeof(info_main),
                                        new PropertyMetadata(new ObservableCollection<string>(), OnChanged));


        static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as info_main).OnChanged();

        }
        void OnChanged()
        {
            if (detail != null)
                detail.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(detail_CollectionChanged);
        }
        void detail_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
           // event here
        }
   
        public info_main()
        {
            InitializeComponent();
          
            //this.DataContext = this;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void reloadGuest()
        {
            specialGuest = new GuestProfile();
            specialGuest.LoadProfile();
            this.avatar = specialGuest.avatar.buffer;
            img.ImageSource = Common.LoadImage(avatar);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            reloadGuest();

        }
        //public ObservableCollection<BitmapImage> userPictureNearBy
        //{
        //    get { return (ObservableCollection<BitmapImage>)GetValue(userPictureNearByProperty); }
        //    set { SetValue(userPictureNearByProperty, value); }
        //}

        //public static readonly DependencyProperty userPictureNearByProperty =
        //    DependencyProperty.Register("userPictureNearBy", typeof(ObservableCollection<BitmapImage>), typeof(GridProfile),
        //                                new PropertyMetadata(new ObservableCollection<BitmapImage>(), OnChanged));
        //static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    (sender as info_main).OnChanged();

        //}

        //void OnChanged()
        //{
        //    if (userPictureNearBy != null)
        //        userPictureNearBy.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(userPictureNearBy_CollectionChanged);
        //}

        ////detect changes
        //void userPictureNearBy_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    int AMOUNT = userPictureNearBy.Count();
        //    List<Source> source = new List<Source>();
        //    for (int i = 0; i < AMOUNT; i++)
        //        source.Add(new Source() { item = userPictureNearBy[i], name = i.ToString() });

        //    listImage.DataContext = source;
        //}
    }
}
