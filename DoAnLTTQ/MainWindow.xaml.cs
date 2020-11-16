using DoAnLTTQ.Backend;
using DoAnLTTQ.Components;
using DoAnLTTQ.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DoAnLTTQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public UserControl _ViewContext;
        public List<Profile> friends = new List<Profile>(); 
        //==>Home view 

        public UserControl ViewContext { get { return this._ViewContext; } set {

                _ViewContext = value;
                OnPropertyChanged("ViewContext");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.ViewContext = new HomeView();
            this.DataContext = this;
           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
    }

}
