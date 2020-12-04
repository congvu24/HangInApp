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
        public CreateProfileView()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            OnUserControlButtonClick();

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
                    url = "avatarLink",
                }
            };
            // kt profile co bi trung chua-- chua lam

            //end
            User u = new User();
            u.saveData(newProfile);
        }
    }
}
