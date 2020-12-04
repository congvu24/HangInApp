using DoAnLTTQ.Views;
using System;
using System.Collections.Generic;
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

namespace DoAnLTTQ
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, INotifyPropertyChanged
    {
        public event EventHandler switchView;
        //public event SwitchViewHandler OnSwitchView;


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
        public LoginView()
        {
            InitializeComponent();
            //this.OnClickBackButton += LoginView_SwitchToCreateProfileView;
            this.DataContext = this;

        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("nextclick");
            if(switchView != null)
            {
                switchView(this, e);
            }
            //OnSwitchView();
            //this.ViewContext = new CreateProfileView();
        }
        //private void LoginView_SwitchToCreateProfileView(ViewEnum viewEnum)
        //{
        //    if (viewEnum == ViewEnum.HomeView)
        //        OnSwitchView();
        //}
    }
}
