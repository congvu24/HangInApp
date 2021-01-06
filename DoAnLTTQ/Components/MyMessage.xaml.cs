using System;
using System.Windows;
using System.Windows.Controls;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for MyMessage.xaml
    /// </summary>
    public partial class MyMessage : UserControl
    {
        public MyMessage(String content)
        {
            InitializeComponent();
            MyMessageContent.Text = content;
        }

       
    }
}
