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

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for BackButton.xaml
    /// </summary>
    public partial class BackButton : UserControl
    {
        public event EventHandler<String> UserControlButtonClicked;


        public delegate void ClickDelegate();
        public ClickDelegate Click { get; set; }
        public string Title = "cac";
        //public data dl = new data() { name = "vu", title="hello"};

        public BackButton()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnUserControlButtonClick();
        }
        private void OnUserControlButtonClick()
        {
            if (UserControlButtonClicked != null)
            {
                UserControlButtonClicked(this, "vcl");
            }
        }

        protected void TheButton_Click(object sender, EventArgs e)
        {
            OnUserControlButtonClick();
        }
    }
   
}
