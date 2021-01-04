using System.Windows.Controls;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for HeaderTitle.xaml
    /// </summary>
    public partial class HeaderTitle : UserControl
    {
        public HeaderTitle()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string Title { get; set; }
    }
}
