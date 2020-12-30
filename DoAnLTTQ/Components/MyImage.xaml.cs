using System;
using System.Collections.Generic;
using System.IO;
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
using DoAnLTTQ.Backend;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for InCommingImage.xaml
    /// </summary>
    public partial class MyImage : UserControl
    {
        public MyImage(String path)
        {
            InitializeComponent();
            byte[] image = File.ReadAllBytes(path);
            img.Source = Common.LoadImage(image);
        }
    }
}
