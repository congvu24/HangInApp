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
    /// Interaction logic for MainSetting.xaml
    /// </summary>
    public partial class MainSetting : UserControl
    {
        public MainSetting()
        {
            InitializeComponent();
            List<imageSlot> data = new List<imageSlot>();
            //data.Add(new imageSlot() { imgUri = "/Resources/Images/IMG_9715.png" });
            for (int i = 0; i < 9; i++)
            {
                data.Add(new imageSlot() { imgUri = "/Resources/Images/IMG_9715.png" });
            }
            //data.Add(new imageSlot() { imgUri = "" });
            //data.Add(new imageSlot() { imgUri = "" });
            //data.Add(new imageSlot() { imgUri = "" });
            //data.Add(new imageSlot() { imgUri = "" });
            //data.Add(new imageSlot() { imgUri = "" });
            //data.Add(new imageSlot() { imgUri = "" });
            //data.Add(new imageSlot() { imgUri = "" });
            //data.Add(new imageSlot() { imgUri = "" });
            listImage.DataContext = data;
        }
    }
    class imageSlot
    {
        public string imgUri { get; set; }
    }
}
