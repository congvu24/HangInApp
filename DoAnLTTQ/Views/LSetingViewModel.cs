using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DoAnLTTQ.Backend;

namespace DoAnLTTQ.Components
{
    class LSetingViewModel : INotifyPropertyChanged
    {
        public string ProfileAvatarUrl { get; set; }
        public Profile myProfile { get; set; }

        public static LSetingViewModel Instance => new LSetingViewModel();
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public LSetingViewModel()
        {
            User user = new User();
            myProfile = user.myProfile;
            ProfileAvatarUrl = user.myProfile.avatar.url;
            MessageBox.Show(ProfileAvatarUrl);


        }
    }
}
