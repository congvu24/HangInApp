﻿using DoAnLTTQ.Backend;
using DoAnLTTQ.Components;
using DoAnLTTQ.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace DoAnLTTQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public List<Profile> friends = new List<Profile>();
        //==>Home view 
        public HomeView myHomeView = new HomeView();
        public SettingView mySettingView = new SettingView();
        public MainView myMainView = new MainView();
        public LoginView myLoginView = new LoginView();
        public CreateProfileView myCreateProfileView = new CreateProfileView();
        public WellcomeScreen myWellcomeScreen = new WellcomeScreen();

        public UserControl _ViewContext;
        public UserControl ViewContext
        {
            get { return this._ViewContext; }
            set
            {
                _ViewContext = value;
                OnPropertyChanged("ViewContext");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        

            this.ViewContext = myWellcomeScreen;

            //((MainView)this.ViewContext).NavBarMain.Reload_myProfile();
            this.DataContext = this;

            myMainView.OnSwitchView += MyHomeView_OnSwitchToSettingView;
            mySettingView.OnSwitchView += SettingView_OnSwitchViewToMainView;
            myLoginView.switchView += new EventHandler(LoginView_OnSwitchViewToCreateProileView);
            myCreateProfileView.SwitchToMainView += new EventHandler(CreateProfileView_OnSwitchViewToMainView);
            myWellcomeScreen.OnSwitchView += new EventHandler(WellcomeView_OnSwitchViewToCreateProileView);

        }
        private void WellcomeView_OnSwitchViewToCreateProileView(object sender, EventArgs e)
        {
            User u = new User();
            if (u.myProfile.name == "default")
                this.ViewContext = myCreateProfileView;
            else
                this.ViewContext = myMainView;

        }
        private void LoginView_OnSwitchViewToCreateProileView(object sender, EventArgs e)
        {
            this.ViewContext = myCreateProfileView;
        }
        private void CreateProfileView_OnSwitchViewToMainView(object sender, EventArgs e)
        {
            this.ViewContext = myMainView;
        }
        private void SettingView_OnSwitchViewToMainView()
        {
            this.ViewContext = myMainView;
            ((MainView)this.ViewContext).NavBarMain.Reload_myProfile();

        }

        private void MyHomeView_OnSwitchToSettingView()
        {
            this.ViewContext = mySettingView;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private void OnWindowclose(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode); // Prevent memory leak
                                                    // System.Windows.Application.Current.Shutdown(); // Not sure if needed
        }

    }


}
