using DoAnLTTQ.Backend;
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
        public GuestProfile guest;
        public List<GuestProfile> userList;
        public event EventHandler<string> ProfileSelected;
        public GridMessage()
        {

            InitializeComponent();
            guest = new GuestProfile();
            List<GuestProfile> friends = new List<GuestProfile>();
            userList = guest.LoadArrayProfile();
            foreach(var u in userList)
            {
                if(u.isLove == true)
                {
                    friends.Add(u);
                }
            }

            ChatList.DataContext = friends;
        }
        private void select_Click(object sender, RoutedEventArgs e)
        {
            string identify = ((sender as Button).Uid).ToString();
            //profileSelecting(identify);
            if (ProfileSelected != null)
            {
                ProfileSelected(this, identify);
            }
            HightLightSelection(sender);
        }

      
        private void HightLightSelection(object sender)
        {
            Style backgroundStyle = this.FindResource("GradientBackground") as Style;
            Style hover = this.FindResource("myChatButton") as Style;
            List<Button> btnList = GetChildrenOfType<Button>(ChatList);
            foreach (var button in btnList)
            {
                button.Style = hover;
            }
            (sender as Button).Style = backgroundStyle;
        }
        public void reload()
        {
            guest = new GuestProfile();
            List<GuestProfile> friends = new List<GuestProfile>();
            userList = guest.LoadArrayProfile();
            foreach (var u in userList)
            {
                if (u.isLove == true)
                {
                    friends.Add(u);
                }
            }

            ChatList.DataContext = friends;
        }

        // get all children of a dependency object
        public static List<T> GetChildrenOfType<T>(DependencyObject depObj)
   where T : DependencyObject
        {
            var result = new List<T>();
            if (depObj == null) return null;
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(depObj);
            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();
                var childrenCount = VisualTreeHelper.GetChildrenCount(currentElement);
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(currentElement, i);
                    if (child is T)
                        result.Add(child as T);
                    queue.Enqueue(child);
                }
            }

            return result;
        }
    }
}
