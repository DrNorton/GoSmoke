using System;
using System.Collections.Generic;
using System.Text;
using GoSmokeMobile.Api.Models.Dtos;
using GoSmokeMobile.Services;
using GoSmokeMobileUniversal.ViewModels.Base;

namespace GoSmokeMobile.ViewModels
{
    public class PersonalViewModel:LoadingScreen
    {
        private readonly IUserDataService _userDataService;
        private ProfileDto _profile;


        public PersonalViewModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
            Profile = userDataService.Profile;
        }

        public ProfileDto Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                base.RaisePropertyChanged(()=>Profile);
            }
        }
    }
}
