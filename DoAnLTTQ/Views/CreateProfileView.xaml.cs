using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DoAnLTTQ
{
    /// <summary>
    /// Interaction logic for CreateProfileView.xaml
    /// </summary>
    public partial class CreateProfileView : UserControl
    {
        private string avatarLink;
        private bool isButtonClicked = false, isProfileInvalid = false;
        public event EventHandler SwitchToMainView;

        public CreateProfileView()
        {
            InitializeComponent();
            UpdateNotification.Text = "Create profile failed, check your profile again";
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (isButtonClicked && isProfileInvalid)
            {
                CreateNewProfile();
                button.Command = MaterialDesignThemes.Wpf.DialogHost.OpenDialogCommand;
            }
            else
            {
                (sender as Button).Content = "Finish";
                isButtonClicked = true;
            }
        }
      
        private void CreateNewProfile()
        {

            Profile newProfile = new Profile()
            {
                name = nameInput.Text,
                age = ageInput.Text,
                sex = sexSelect.SelectedIndex.ToString(),
                hobby = hobbyInput.Text,
                avatar = new Picture()
                {
                    name = "avatar",
                    url = avatarLink,
                }
            };
            // kt profile co bi trung chua-- chua lam

            //end
            User u = new User();
            if (u != null && u.myProfile != newProfile)
                u.saveData(newProfile);
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchToMainView != null && isProfileInvalid)
                SwitchToMainView(this, e);
        }

        private void AddProfilePicure(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)

                //this.avatarControl.ImageSource = new BitmapImage(new Uri("your path", UriKind.Relative));
                //this.avatarControl.ImageSource = ne;
                this.avatarControl.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Relative));
            this.avatarLink = openFileDialog.FileName;
        }
        // kt xem profile có đúng chưa, nếu đúng thì set lại biến isProfileInvalid = true và set content của UpdateNotification thành Create Success
    }
}
