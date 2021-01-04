using System;
using System.Windows.Controls;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for InComingMessage.xaml
    /// </summary>
    public partial class InComingMessage : UserControl
    {
        public InComingMessage(String content)
        {
            InitializeComponent();
            PartnerMessageContent.Text = content;

        }
    }
}
