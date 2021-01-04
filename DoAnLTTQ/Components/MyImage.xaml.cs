using System;
using System.IO;
using System.Windows.Controls;
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
