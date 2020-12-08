﻿using System;
using System.Collections.Generic;
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
using DoAnLTTQ.Views;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
using System.Security.Policy;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for NavBarSetting.xaml
    /// </summary>
    public partial class NavBarSetting : UserControl, INotifyPropertyChanged
    {
        public event EventHandler<Profile> UserUpdateProfile;
        public event ClickOnButtonHandler OnClickBackButton;
        public string avatarLink { get; set; }

        public String myProfile
        {
            get { return (String)GetValue(profileProperty); }
            set { SetValue(profileProperty, value); }
        }
        public static readonly DependencyProperty profileProperty =
             DependencyProperty.Register("myProfile", typeof(object),
               typeof(NavBarSetting), new PropertyMetadata(""));

        public NavBarSetting()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnClickBackButton(ViewEnum.HomeView);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputInformation())
            {
                ProfileHeaderName.Content = nameInput.Text; 
                OnUserControlButtonClick();
            }

        }
        private void OnUserControlButtonClick()
        {
            if (UserUpdateProfile != null)
            {
                Profile newProfile = new Profile()
                {
                    name = nameInput.Text,
                    age = ageInput.Text,
                    sex = sexSelect.SelectedIndex.ToString(),
                    hobby = hobbyInput.Text,
                    avatar = new Picture() { name = "avatar", url = avatarLink }
                };
                
                UserUpdateProfile(this, newProfile);
            }
        }

        private void Avatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                this.avatarControl.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            this.avatarLink = openFileDialog.FileName;
        }

        private void LocaleCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        private bool isValidInputInformation()
        {
            if (!isValidNameInput(nameInput.Text))
            {
                ErrorName.Visibility = Visibility.Visible;
                ErrorName.Content = "Vui lòng nhập tên hợp lệ! ";
            }
            else
            {
                ErrorName.Visibility = Visibility.Hidden;
            }

            if (!isValidAgeInput(ageInput.Text))
            {
                ErrorAge.Visibility = Visibility.Visible;
                ErrorAge.Content = "Vui lòng nhập tuổi hợp lệ! ";
            }
            else
            {
                ErrorAge.Visibility = Visibility.Hidden; 
            }                

            if (!isValidHobbyInput(hobbyInput.Text))
            {
                ErrorHobby.Visibility = Visibility.Visible;
                ErrorHobby.Content = "Vui lòng nhập sở thích hợp lệ";
            }
            else
            {
                ErrorHobby.Visibility = Visibility.Hidden; 
            }

            if (ErrorName.Visibility != Visibility.Visible && 
                ErrorAge.Visibility != Visibility.Visible && 
                ErrorHobby.Visibility != Visibility.Visible)
            {
                return true; 
            }
            return false; 
        }

        private bool isValidNameInput(string input)
        {
            if (input.Length < 1)
            {
                return false;
            }
            else
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(input);

                for (int i = 0; i < asciiBytes.Length; i++)
                {
                    if (isSpecialKey(asciiBytes[i]))
                        return false;
                }

                return true;
            }
        }

        private bool isSpecialKey(byte key)
        {
            if (key >= 33 && key <= 64)
                return true;
            if (key == 95 || key == 96)
                return true;
            if (key >= 123 && key <= 126)
                return true;

            return false;
        }

        private bool isValidAgeInput(string ageString)
        {
            byte age = 1;
            bool checkage = byte.TryParse(ageString, out age); 

            if (checkage)
            {
                if (age < 10 || age > 110)
                    return false; 
            }
            else
            {
                return false; 
            }

            return true;

        }

        private bool isValidHobbyInput(string input)
        {
            if (input.Length < 1)
            {
                return false;
            }
            else
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(input);

                for (int i = 0; i < asciiBytes.Length; i++)
                {
                    if (isSpecialKey(asciiBytes[i]))
                        return false;
                }

                return true;
            }
        }

        
    }
}
