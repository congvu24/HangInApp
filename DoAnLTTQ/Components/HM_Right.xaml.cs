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
    public partial class info_main : UserControl, INotifyPropertyChanged
    {
        byte[] avatar;
        public GuestProfile specialGuest;
        public int _profileIndex; // index of profile in list profile to show on screen

        public int profileIndex
        {
            get { return _profileIndex; }
            set { _profileIndex = value; OnPropertyChanged("profileIndex"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        public UserControl _ViewContext;

        public UserControl ViewContext
        {
            get { return this._ViewContext; }
            set
            {
                _ViewContext = value;
                OnPropertyChanged("ViewContext");
            }
        }
        public void ChangeProfile(int id)
        {
            List<int> t = new List<int>();
            t.Add(11);
            t.Add(21); 
            t.Add(31); 
            t.Add(41);
            profileIndex = t[id];
        }
        public info_main()
        {
            InitializeComponent();
            profileIndex = 19;
            this.DataContext = this;
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
    }
}
