using System.Windows.Controls;

namespace DoAnLTTQ.Views
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    public partial class MessageView : UserControl
    {
        public event SwitchViewHandler OnSwitchView;
        public MessageView()
        {
            InitializeComponent();
       
        }

      
    }
}
