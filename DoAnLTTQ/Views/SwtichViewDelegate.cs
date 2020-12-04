using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public delegate void ClickOnButtonHandler(ViewEnum viewEnum); 
   
}
