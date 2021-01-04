using System;

namespace DoAnLTTQ.Views
{
    public enum ViewEnum
    {
        HomeView,
        MessageView,
        SettingView,
        QuanhDayView,
        LoginView,
        CreateProfileView,
    }

    public delegate void SwitchViewHandler();
    public delegate void NotifyHandler(String content);
    public delegate void ClickOnButtonHandler(ViewEnum viewEnum); 
   
}
