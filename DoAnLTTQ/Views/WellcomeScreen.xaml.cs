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
using System.Threading;
//using System.Timers;

namespace DoAnLTTQ.Views
{
    /// <summary>
    /// Interaction logic for WellcomeScreen.xaml
    /// </summary>
    public partial class WellcomeScreen : UserControl
    {
       public event EventHandler OnSwitchView;
        public WellcomeScreen()
        {
            InitializeComponent();
            
           
            //Thread t = new Thread(SwitchToNextView_Elapsed);
            Button b = new Button();

            n.Children.Add(b);
            b.Click += B_Click;
            b.VerticalAlignment = VerticalAlignment.Bottom;
  
            this.DataContext = this;

        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            
            if (OnSwitchView != null)
            {
                OnSwitchView(this, e);
            }
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(tr);
            t.Start();
        }
        private void tr()
        {
            RoutedEventArgs e = new RoutedEventArgs();
            Thread.Sleep(2000);
            if (OnSwitchView != null)
            {
                OnSwitchView(this, e);
            }

        }

        
    }
}
