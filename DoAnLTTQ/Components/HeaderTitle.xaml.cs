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
