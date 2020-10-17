using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DoAnLTTQ.Backend
{
    public class User
    {
        private readonly string myProfileData = "\\Backend\\Database\\profile.json";
        public User()
        {
            string _source = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + myProfileData;
            this.myProfile = new Profile();
            using (StreamReader r = new StreamReader(_source))
            {
                string json = r.ReadToEnd();
                this.myProfile = JsonConvert.DeserializeObject<Profile>(json);
                int count = this.myProfile.picture.Count;
                if (count < 9)
                {
                    for (int i = 0; i < 9 - count; i++)
                    {
                        this.myProfile.picture.Add(new Picture() { name = "placeholer", url = "" });
                    }
                }
            }
        }

        public Profile myProfile { get; set; }
        public void saveData(Profile profile)
        {
            string _source = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + myProfileData;
            string json = JsonConvert.SerializeObject(profile);
            System.IO.File.WriteAllText(_source, json);
        }
        
        
    }
    public class Profile
    {
        public string name { get; set; }
        public string age { get; set; }
        public string sex { get; set; }
        public string hobby { get; set; }
        public Picture avatar { get; set; }
        public List<Picture> picture = new List<Picture>();
        public Profile() { }
        public Profile(Profile profile) {
            name = profile.name;
            age = profile.age;
            sex = profile.sex;
            hobby = profile.hobby;
            avatar = profile.avatar;
            picture = profile.picture;
        }
    }
    public class Picture
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
