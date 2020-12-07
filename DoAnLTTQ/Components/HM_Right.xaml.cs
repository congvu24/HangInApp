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
        public bool IsSaveComplete = false;

        byte[] avatar;
        public GuestProfile specialGuest = new GuestProfile();
        public int _profileIndex; // index of profile in list profile to show on screen
        private int indexHomePicture = 0;
        public string profileIp;

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
        public info_main()
        {
            InitializeComponent();
            profileIndex = 19;
            this.DataContext = this;
            this.Loaded += Info_main_Loaded;
        }

        // Load profile khi infor-main view duoc render ra
        private void Info_main_Loaded(object sender, RoutedEventArgs e)
        {
            specialGuest.LoadArrayProfile();
        }

        public void ChangeProfile(int id)
        {
            //specialGuest.LoadArrayProfile();
            
            img.ImageSource = Common.LoadImage(specialGuest.listGuestProfile[id].avatar.buffer);
            info_name.Text = specialGuest.listGuestProfile[id].name;
            info_age.Text = specialGuest.listGuestProfile[id].age;
            info_hobby.Text = specialGuest.listGuestProfile[id].hobby;

            profileIp = specialGuest.listGuestProfile[id].ip;
        }


        public void reloadArrayGuest()
        {
            //specialGuest.LoadArrayProfile();

            this.avatar = specialGuest.listGuestProfile[0].avatar.buffer;
            img.ImageSource = Common.LoadImage(avatar);

            //foreach (var item in specialGuest.listGuestProfile)
            //{
            //    MessageBox.Show(item.name); 
            //}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            reloadArrayGuest();
        }

        public void ChangeProfileInHomeView(KeyEventArgs e)
        {
            //specialGuest.LoadArrayProfile();

            switch (e.Key)
            {
                case Key.Left:
                    if (indexHomePicture == 0)
                        indexHomePicture = specialGuest.listGuestProfile.Count - 1;
                    else
                        indexHomePicture--;

                    //MessageBox.Show(specialGuest.listGuestProfile[indexHomePicture].name);
                    e.Handled = true;
                    break;
                case Key.Right:
                    if (indexHomePicture == specialGuest.listGuestProfile.Count - 1)
                        indexHomePicture = 0;
                    else
                        indexHomePicture++;

                    //MessageBox.Show(specialGuest.listGuestProfile[indexHomePicture].name);
                    e.Handled = true;
                    break;
                default:
                    break;
            }

            //info_name.Text = specialGuest.listGuestProfile[indexHomePicture].name;
            //info_age.Text = specialGuest.listGuestProfile[indexHomePicture].age;
            //info_hobby.Text = specialGuest.listGuestProfile[indexHomePicture].hobby;

            ShowInformationToHomeView(indexHomePicture); 
        }

        private void ShowInformationToHomeView(int index)
        {
            try
            {
                this.avatar = specialGuest.listGuestProfile[index].avatar.buffer;
                img.ImageSource = Common.LoadImage(avatar);
                info_name.Text = specialGuest.listGuestProfile[index].name;
                info_age.Text = specialGuest.listGuestProfile[index].age;
                info_hobby.Text = specialGuest.listGuestProfile[index].hobby;
            }
            catch (Exception)
            {

              
            }
          
        }

        private void nextProfile(object sender, RoutedEventArgs e)
        {
            if (indexHomePicture == specialGuest.listGuestProfile.Count - 1)
                indexHomePicture = 0;
            else
                indexHomePicture++;

            ShowInformationToHomeView(indexHomePicture);
        }

        private void previousProfile(object sender, RoutedEventArgs e)
        {
            if (indexHomePicture == 0)
                indexHomePicture = specialGuest.listGuestProfile.Count - 1;
            else
                indexHomePicture--;

            ShowInformationToHomeView(indexHomePicture);
        }

        private void showUID(object sender, RoutedEventArgs e)
        {
            GuestProfile guest = new GuestProfile();
            guest.LikeProfile(profileIp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsSaveComplete = true;
        }
    }
}
