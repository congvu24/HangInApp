using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Drawing;



namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for GridProfile.xaml
    /// </summary>
    public partial class GridProfile : UserControl, INotifyPropertyChanged
    {
        //public List<Picture> _picture = new List<Picture>();
        //// code thu 2
        ////public List<Picture> picture { get { return this._picture; } set { this._picture = value; this.OnPropertyChanged("picture"); } }
        //public Picture picture
        //{
        //    get { return (Picture)GetValue(profilePicture); }
        //    set { SetValue(profilePicture, value); }
        //}
        //public static readonly DependencyProperty profilePicture =
        //     DependencyProperty.Register("_picture", typeof(Picture),
        //       typeof(GridProfile));

        //// end
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

        void userPictureNearBy_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("grid Changed");
        }
        public class Source
        {
            public BitmapImage item { get; set; }
        }
        public GridProfile()
        {
            InitializeComponent();

            List<Source> source = new List<Source>();

            for (int i = 0; i < 9; i++)
            {
                //GuestProfile guest = new GuestProfile();
                //guest.LoadProfile();
                //userPictureNearBy.Add(Common.LoadImage(guest.avatar.buffer));
                
                //source.Add(new Source() { item = userPictureNearBy[i] });
            }
           

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //img.ImageSource = userPictureNearBy[0];
        }
    }
}
