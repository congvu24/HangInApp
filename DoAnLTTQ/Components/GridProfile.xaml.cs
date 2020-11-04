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
using DoAnLTTQ.Backend;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for GridProfile.xaml
    /// </summary>
    public partial class GridProfile : UserControl
    {
        public GridProfile()
        {
            InitializeComponent();

            List<Picture> data = new List<Picture>();
            //data.Add(new Picture() { imgUri = "/Resources/Images/IMG_9715.png" });
            for (int i = 0; i < 9; i++)
            {
                data.Add(new Picture() { url = "/Resources/Images/IMG_9715.png" });
            }
            //data.Add(new Picture() { imgUri = "" });
            //data.Add(new Picture() { imgUri = "" });
            //data.Add(new Picture() { imgUri = "" });
            //data.Add(new Picture() { imgUri = "" });
            //data.Add(new Picture() { imgUri = "" });
            //data.Add(new Picture() { imgUri = "" });
            //data.Add(new Picture() { imgUri = "" });
            //data.Add(new Picture() { imgUri = "" });
            listImage.DataContext = data;
        }
    }
}
