﻿namespace DoAnLTTQ.Backend
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;

    [Serializable]
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
                        this.myProfile.picture.Add(new Picture() { name = "placeholer" + i, url = "" });
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

        public List<Profile> userList = new List<Profile>();
    }

    [Serializable]
    public class Profile
    {

        public string name { get; set; }
        public string age { get; set; }
        public string sex { get; set; }
        public string hobby { get; set; }
        public Picture avatar { get; set; }
        public List<Picture> picture = new List<Picture>();
        public Profile()
        {
        }
        public Profile(Profile profile)
        {
            name = profile.name;
            age = profile.age;
            sex = profile.sex;
            hobby = profile.hobby;
            avatar = profile.avatar;
            picture = profile.picture;
        }
    }

    [Serializable]
    public class Picture
    {

        public string name { get; set; }


        public string url { get; set; }
    }
    [Serializable]
    public class GuestProfile
    {
        private readonly string myProfileData = "\\Backend\\Database\\guest.json";
        public string name { get; set; }

        public string age { get; set; }

        public string sex { get; set; }

        public string hobby { get; set; }

        public GuestPicture avatar { get; set; }

        public List<GuestPicture> picture = new List<GuestPicture>();
        public GuestProfile() { }
        public void LoadProfile()
        {
            string _source = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + myProfileData;
            using (StreamReader r = new StreamReader(_source))
            {
                string json = r.ReadToEnd();
                GuestProfile tmp = JsonConvert.DeserializeObject<GuestProfile>(json);

                this.name = tmp.name;
                this.avatar = tmp.avatar;
                this.age = tmp.age;
                this.sex = tmp.sex;
                this.hobby = tmp.hobby;
            }
        }

        public List<GuestProfile> listGuestProfile = new List<GuestProfile>();
        public void LoadArrayProfile()
        {
            string _source = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + myProfileData;
            using (StreamReader r = new StreamReader(_source))
            {
                string json = r.ReadToEnd();
                listGuestProfile = JsonConvert.DeserializeObject<List<GuestProfile>>(json);

                GuestProfile firstProfile = listGuestProfile[0];

                this.name = firstProfile.name;
                this.avatar = firstProfile.avatar;
                this.age = firstProfile.age;
                this.sex = firstProfile.sex;
                this.hobby = firstProfile.hobby;
            }
        }

        public GuestProfile(GuestProfile profile)
        {
            name = profile.name;
            age = profile.age;
            sex = profile.sex;
            hobby = profile.hobby;
            avatar = profile.avatar;
            picture = profile.picture;
        }

        public GuestProfile(User user)
        {
            name = user.myProfile.name;
            age = user.myProfile.age;
            sex = user.myProfile.sex;
            hobby = user.myProfile.hobby;
            byte[] image = File.ReadAllBytes(user.myProfile.avatar.url);
            avatar = new GuestPicture() { name = "vc", buffer = image };
        }
        public void saveData()
        {
            string _source = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + myProfileData;
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(_source, json);
        }


    }

    [Serializable]
    public class GuestPicture
    {

        public string name { get; set; }

        public byte[] buffer { get; set; }
    }
}
