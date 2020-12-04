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
    public partial class CreateProfileView : UserControl, INotifyPropertyChanged
    {
        private string avatarLink;
        private bool buttonClicked = false;
        public event SwitchViewHandler OnSwitchView;
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
        public CreateProfileView()
        {
            InitializeComponent();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (buttonClicked)
                OnUserControlButtonClick();
            else
            {
                (sender as Button).Content = "Finish";
                buttonClicked = !buttonClicked;
            }

        }
        private void OnUserControlButtonClick()
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
            u.saveData(newProfile);
        }

        private void CreatePicture(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)

                //this.avatarControl.ImageSource = new BitmapImage(new Uri("your path", UriKind.Relative));
            //this.avatarControl.ImageSource = ne;
            this.avatarControl.ImageSource= new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Relative));
            this.avatarLink = openFileDialog.FileName;
        }
    }
}
