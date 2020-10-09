using DoAnLTTQ.Components;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
         
        public UserControl _ViewContext;
        public UserControl ViewContext { get { return this._ViewContext; } set {

                _ViewContext = value;
                OnPropertyChanged("ViewContext");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ButtonName.UserControlButtonClicked += new EventHandler<data>(MyUserControl_UserControlButtonClicked);

        }
        private void MyUserControl_UserControlButtonClicked(object sender, data e)
        {
            MessageBox.Show(e.name);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ViewContext = new SettingView();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.ViewContext = new HomeView();
        }
        private void Message_Click(object sender, RoutedEventArgs e)
        {
            this.ViewContext = new MessageView();
        }
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
    }
    public class data
    {
        public string title { get; set; }
        public string name { get; set; }
    }
}
