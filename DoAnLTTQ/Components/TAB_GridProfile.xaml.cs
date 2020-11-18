using System;
using System.Collections.Generic;
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
    /// Interaction logic for GridProfile.xaml
    /// </summary>
    public partial class GridProfile : UserControl, INotifyPropertyChanged
    {
        public List<Picture> _picture = new List<Picture>();
        // code thu 2
        //public List<Picture> picture { get { return this._picture; } set { this._picture = value; this.OnPropertyChanged("picture"); } }
        public Picture picture
        {
            get { return (Picture)GetValue(profilePicture); }
            set { SetValue(profilePicture, value); }
        }
        public static readonly DependencyProperty profilePicture =
             DependencyProperty.Register("_picture", typeof(Picture),
               typeof(GridProfile));
      
        // end
        public GridProfile()
        {
            InitializeComponent();

            this.DataContext = this;

            //MessageBox.Show(userURL);



            List<Picture> data = new List<Picture>();

            //for (int i = 0; i < 9; i++)
            //{
            //    data.Add(new Picture() { url = "/Resources/Images/IMG_9715.png" });

            //}

            //listImage.DataContext = data;
            listImage.DataContext = _picture;

        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

    }
}
