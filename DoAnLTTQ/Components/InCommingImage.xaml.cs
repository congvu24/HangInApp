using System.Windows.Controls;
using DoAnLTTQ.Backend;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for InCommingImage.xaml
    /// </summary>
    public partial class InCommingImage : UserControl
    {
        public InCommingImage(byte[] content)
        {
            InitializeComponent();
            img.Source = Common.LoadImage(content);
        }
    }
}
