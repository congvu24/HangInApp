﻿using DoAnLTTQ.Views;
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
    /// Interaction logic for NavBarMain.xaml
    /// </summary>
    public partial class NavBarMain : UserControl
    {
        public NavBarMain()
        {
            InitializeComponent();
        }

        // code here to check connect server-client
        //Server s = new Server();
        private void Button_Click(object sender, RoutedEventArgs e) //  button start server
        {
            //s.run();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // button send message
        {
            //sendMessage();
        }
        private void sendMessage()
        {
            Client c = new Client();

            //var u = new User();
            var u = "vcl";
            //var u = "check server";
            //Message m = new Message(u, 2);

            c.sendToServer(u);
            //MessageBox.Show(m.reverse() as string);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // button stop message
        {
            //s.stop();
        }
        private void updatePicture()
        {

        }
        private void sendInfo()
        {

        }
        private void receiveInfo()
        {

        }
    }
}
