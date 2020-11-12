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


            //User u = new User();
            string u = "concac";
            Server s = new Server();
            s.run();

            Client c = new Client();
            c.sendToServer(u);

            s.stop();
            s.close();
            System.Threading.Thread.Sleep(1000);
            var userURL = s.getResult() as string;
            //User user = s.getResult() as User;
            //var userURL = user.myProfile.avatar.url.ToString();

            MessageBox.Show(userURL);



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
