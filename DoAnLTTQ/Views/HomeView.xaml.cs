using DoAnLTTQ.Backend;
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
using DoAnLTTQ.Components;
using System.ComponentModel;

namespace DoAnLTTQ.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        //public event SwitchViewHandler OnSwitchView;

        public HomeView()
        {
            InitializeComponent();
  
            TabMain.ButtonSwitchViewOnClick += TabMain_ButtonSwitchViewOnClick;
        }

        private void TabMain_ButtonSwitchViewOnClick(ViewEnum viewEnum)
        {
            //if (viewEnum == ViewEnum.SettingView)
            //{
            //    OnSwitchView();
            //}
            //else if (viewEnum == ViewEnum.MessageView)
            //{
            //    info_main.Visibility = Visibility.Collapsed;
            //    messagedetails.Visibility = Visibility.Visible;
            //}
            //else if (viewEnum == ViewEnum.QuanhDayView)
            //{
            //    info_main.Visibility = Visibility.Visible;
            //    messagedetails.Visibility = Visibility.Collapsed;
            //}

        }
    }

}
