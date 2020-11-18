using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnLTTQ.Views
{
    public enum viewEnum
    {
        HomeView,
        MessageView,
        SettingView
    }

    public delegate void SwitchViewHandler();
    public delegate void ClickOnButtonHandler(); 
   
}
