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
    /// Interaction logic for GridMessage.xaml
    /// </summary>
    public partial class GridMessage : UserControl
    {
        
        public GridMessage()
        {

            InitializeComponent();

            List<ChatList> chatLists = new List<ChatList>();
            for (int i = 0; i < 4; i++)
            {
                chatLists.Add(new ChatList() { imgUri = "/Resources/Images/IMG_9715.png", personName = "Công Vũ", message = "Ua la sao" });
            }

            ChatList.DataContext = chatLists; 
        }

    }
    class ChatList
    {
        public string imgUri { get; set; }
        public string personName { get; set; }
        public string message { get; set; }

    }
}
