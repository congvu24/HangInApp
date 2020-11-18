using System;
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
            OnClickBackButton(); 
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            OnUserControlButtonClick();

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
                    avatar = new Picture() { name="avatar", url=avatarLink}
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
    }
}
