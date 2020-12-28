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
using System.Threading;
using DoAnLTTQ.Backend;
using System.Windows.Threading;
using System.Drawing;
using System.IO;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for info_main.xaml
    /// </summary>
    public partial class info_main : UserControl, INotifyPropertyChanged
    {
        public event EventHandler<int> NotifyProfile;
        public bool IsSaveComplete = false;

        public byte[] avatar1;
       

        public GuestProfile specialGuest = new GuestProfile();
        private int _profileIndex; // index of profile in list profile to show on screen
        public int indexHomePicture = 0;
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

            this.DataContext = this;
            this.Loaded += Info_main_Loaded;
        }

        // Load profile khi infor-main view duoc render ra
        private void Info_main_Loaded(object sender, RoutedEventArgs e)
        {
            specialGuest.LoadArrayProfile();
            if (specialGuest.LoadArrayProfile() != null)
                ShowInformationToHomeView(0);
        }

        public void ChangeProfile(int id)
        {
            specialGuest.LoadArrayProfile();
            img.ImageSource = Common.LoadImage(specialGuest.listGuestProfile[id].avatar.buffer);
            info_name.Text = specialGuest.listGuestProfile[id].name;
            info_age.Text = specialGuest.listGuestProfile[id].age;
            info_hobby.Text = specialGuest.listGuestProfile[id].hobby;
          

            profileIp = specialGuest.listGuestProfile[id].ip;
            //this.indexHomePicture = id;
        }


        public async void reloadArrayGuest()
        {
            //this.avatar = specialGuest.listGuestProfile[0].avatar.buffer;
            //img.ImageSource = Common.LoadImage(avatar);
            //await Task.Run(() =>
            //{
            //    Thread.Sleep(500);
            //    reloadButton.IsChecked = false;
            //});
            specialGuest.LoadArrayProfile();

            await Task.Run(() => Thread.Sleep(500));
            await Dispatcher.BeginInvoke(new Action(delegate
            {
                //Task.Run(() => Thread.Sleep(500));
                if (specialGuest.listGuestProfile.Count >= 0)
                    ShowInformationToHomeView(0);

            }), DispatcherPriority.Background);

            reloadButton.IsChecked = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            reloadArrayGuest();
            this.indexHomePicture = 0;
        }

        public void ChangeProfileInHomeView(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (indexHomePicture == 0)
                        indexHomePicture = specialGuest.listGuestProfile.Count - 1;
                    else
                        indexHomePicture--;

                    e.Handled = true;
                    break;
                case Key.Right:
                    if (indexHomePicture == specialGuest.listGuestProfile.Count - 1)
                        indexHomePicture = 0;
                    else
                        indexHomePicture++;

                    e.Handled = true;
                    break;
                default:
                    break;
            }

            ShowInformationToHomeView(indexHomePicture);
        }

        private void ShowInformationToHomeView(int index)
        {
            profileIp = specialGuest.listGuestProfile[index].ip;
            try
            {
                //if (NotifyProfile != null)
                NotifyProfile((new object()) as Button, index);
            }
            catch
            {
                Console.WriteLine("bug catched");
            }

            try
            {
                this.avatar1 = specialGuest.listGuestProfile[index].avatar.buffer;
                img.ImageSource = Common.LoadImage(avatar1);
                info_name.Text = specialGuest.listGuestProfile[index].name;
                info_age.Text = specialGuest.listGuestProfile[index].age;
                info_hobby.Text = specialGuest.listGuestProfile[index].hobby;

                //            var bmImage = Common.LoadImage(avatar);
                //var bitmap = new TransformedBitmap(bmImage,
                //new ScaleTransform(
                //    383 / bmImage.PixelWidth,
                //    520 / bmImage.PixelHeight));
                //            img.ImageSource = bitmap;
            }
            catch (Exception) { }
        }
        private void nextProfile(object sender, RoutedEventArgs e)
        {
            if (indexHomePicture == specialGuest.listGuestProfile.Count - 1)
                indexHomePicture = 0;
            else
                indexHomePicture++;

            ShowInformationToHomeView(indexHomePicture);

            //if(NotifyProfile != null)
            //    NotifyProfile(sender, indexHomePicture);
        }

        private void previousProfile(object sender, RoutedEventArgs e)
        {
            if (indexHomePicture == 0)
                indexHomePicture = specialGuest.listGuestProfile.Count - 1;
            else
                indexHomePicture--;

            ShowInformationToHomeView(indexHomePicture);

            //if (NotifyProfile != null)
            //    NotifyProfile(sender, indexHomePicture);
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

        private void sendFriendRq_Click(object sender, RoutedEventArgs e)
        {
            GuestProfile guest = new GuestProfile();
            //guest.LikeProfile(profileIp);
            guest.LikeProfile(profileIp);

            animation();

        }
        private async void animation()
        {
            await Task.Run(() => Thread.Sleep(500));
            await Dispatcher.BeginInvoke(new Action(delegate
             {
                 //Task.Run(() => Thread.Sleep(500));

                 if (indexHomePicture == specialGuest.listGuestProfile.Count - 1)
                     indexHomePicture = 0;
                 else
                     indexHomePicture++;

                 ShowInformationToHomeView(indexHomePicture);
             }), DispatcherPriority.Background);
            sendFriendRq.IsChecked = false;

        }
    }
}

