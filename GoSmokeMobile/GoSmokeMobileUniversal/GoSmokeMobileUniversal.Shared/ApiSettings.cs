using GoSmokeMobile.Api;
using GoSmokeMobile.Api.Models;
using GoSmokeMobile.Services;

namespace GoSmokeMobileUniversal
{
    public class ApiSettings:IApiSettings
    {
        private readonly IUserDataService _userDataService;


        public ApiSettings(IUserDataService userDataService)
        {
            _userDataService = userDataService;
      
        }

        public string BaseUrl
        {
            get { return "http://localhost:54680/api"; }
        }
        public Token SavedToken
        {
            get
            {
                if (_userDataService.Profile == null)
                {
                    return null;
                }
                return _userDataService.Profile.Token;
            }
            set
            {
                if (_userDataService.Profile != null)
                {
                    _userDataService.Profile.Token = value; 
                }
                
            }
        }
    }
}
