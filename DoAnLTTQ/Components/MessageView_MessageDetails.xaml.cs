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
    /// Interaction logic for MessageView_MessageDetails.xaml
    /// </summary>
    public partial class MessageView_MessageDetails : UserControl
    {
        public MessageView_MessageDetails()
        {
            InitializeComponent();

            ChatList chatList = new ChatList() { imgUri = "/Resources/Images/IMG_9715.png", personName = "Công Vũ", message = "" };

            //messageNameTitle.DataContext = chatList; 
            TextBox tb = new TextBox(); 
            
        }
    }
}
