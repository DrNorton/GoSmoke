using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using GoSmokeMobile.Api.Models.Dtos;
using Newtonsoft.Json;

namespace GoSmokeMobile.Services
{
    public interface IUserDataService
    {
        ProfileDto Profile { get; set; }
    }

    public class UserDataService : IUserDataService
    {
     

        public ProfileDto Profile
        {
            get
            {
                var profile = (string)ApplicationData.Current.LocalSettings.Values["Profile"];
                if (profile == null)
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<ProfileDto>(profile);
            }
            set { ApplicationData.Current.LocalSettings.Values["Profile"] =JsonConvert.SerializeObject(value); }
        }
    }
}
