using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
            if(switchView != null)
            {
                switchView(this, e);
            }
        }
        //private void LoginView_SwitchToCreateProfileView(ViewEnum viewEnum)
        //{
        //    if (viewEnum == ViewEnum.HomeView)
        //        OnSwitchView();
        //}
    }
}
