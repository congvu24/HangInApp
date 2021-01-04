using System.Windows.Controls;

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
