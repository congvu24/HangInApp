﻿using System;
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
        private int indexHomePicture = 0;

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
        }
        public void ChangeProfile(int id)
        {
            GuestProfile g = new GuestProfile();
            g.LoadArrayProfile();
            
            img.ImageSource = Common.LoadImage(g.listGuestProfile[id].avatar.buffer);
            info_name.Text = g.listGuestProfile[id].name;
            info_age.Text = g.listGuestProfile[id].age;
            info_hobby.Text = g.listGuestProfile[id].hobby;
        }


        public void reloadArrayGuest()
        {
            specialGuest = new GuestProfile();
            specialGuest.LoadArrayProfile();
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
            specialGuest = new GuestProfile();
            specialGuest.LoadArrayProfile();

            switch (e.Key)
            {
                case Key.A:
                    if (indexHomePicture == 0)
                        indexHomePicture = specialGuest.listGuestProfile.Count - 1;
                    else
                        indexHomePicture--;

                    MessageBox.Show(specialGuest.listGuestProfile[indexHomePicture].name);
                    break;
                case Key.D:
                    if (indexHomePicture == specialGuest.listGuestProfile.Count - 1)
                        indexHomePicture = 0;
                    else
                        indexHomePicture++;

                    MessageBox.Show(specialGuest.listGuestProfile[indexHomePicture].name);
                    break;
                default:
                    MessageBox.Show("An phim A hoac W de thay doi home profile");
                    break;
            }

        }

        private void nextProfile(object sender, RoutedEventArgs e)
        {

        }

        private void previousProfile(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string identify = ((Button)sender).Uid;
            MessageBox.Show(identify);
        }
    }
}
