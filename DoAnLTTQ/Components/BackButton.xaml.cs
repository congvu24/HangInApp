using System;
using System.Windows;
using System.Windows.Controls;

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
