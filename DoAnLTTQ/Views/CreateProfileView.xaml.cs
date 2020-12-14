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
            isProfileInvalid = isValidInputInformation();
            if (isButtonClicked)
            {
                if(isProfileInvalid)
                {
                CreateNewProfile();
                UpdateNotification.Text = "Create profile successful";
                }
                button.Command = MaterialDesignThemes.Wpf.DialogHost.OpenDialogCommand;
            }
            else
            {
                UpdateNotification.Text = "Create profile failed, check your profile again";
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

        private bool isValidInputInformation()
        {
            if (!isValidNameInput(nameInput.Text))
            {
                errorNameInput.Visibility = Visibility.Visible;
                errorNameInput.Text = "Vui lòng nhập tên hợp lệ! ";
            }
            else
            {
                errorNameInput.Visibility = Visibility.Hidden;
            }

            if (!isValidAgeInput(ageInput.Text))
            {
                errorAgeInput.Visibility = Visibility.Visible;
                errorAgeInput.Text = "Vui lòng nhập tuổi hợp lệ! ";
            }
            else
            {
                errorAgeInput.Visibility = Visibility.Hidden;
            }

            if (!isValidSexInput(sexSelect.Text))
            {
                errorGenderInput.Visibility = Visibility.Visible;
                errorGenderInput.Text = "Vui lòng chọn giới tính!";
            }
            else
            {
                errorGenderInput.Visibility = Visibility.Hidden;
            }

            if (!isValidHobbyInput(hobbyInput.Text))
            {
                errorHobbyInput.Visibility = Visibility.Visible;
                errorHobbyInput.Text = "Vui lòng nhập sở thích hợp lệ";
            }
            else
            {
                errorHobbyInput.Visibility = Visibility.Hidden;
            }

            if (errorNameInput.Visibility != Visibility.Visible &&
                errorAgeInput.Visibility != Visibility.Visible &&
                errorHobbyInput.Visibility != Visibility.Visible &&
                errorGenderInput.Visibility != Visibility.Visible)
            {
                return true;
            }
            return false;
        }

        private bool isValidNameInput(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
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
            if (key >= 33 && key <= 47)
                return true;
            if (key >= 58 && key <= 64)
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
                if (age < 14 || age > 110)
                    return false;
            }
            else
            {
                return false;
            }

            return true;

        }

        private bool isValidSexInput(string sexString)
        {
            if (sexString == "---Select--" || String.IsNullOrWhiteSpace(sexString))
            {
                return false;
            }
            else
            {
                return true;
            }
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
