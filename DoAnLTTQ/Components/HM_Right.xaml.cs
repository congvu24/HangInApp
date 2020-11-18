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

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for info_main.xaml
    /// </summary>
    public partial class info_main : UserControl
    {
        //GuestProfile guest;
        byte[] avatar;
        public GuestProfile specialGuest;
        public info_main()
        {
            InitializeComponent();
           
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
