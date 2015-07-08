
using GoSmokeMobile.Services;
using GoSmokeMobileUniversal.ViewModels.Base;

namespace GoSmokeMobile.ViewModels
{
    public class MainViewModel:LoadingScreen
    {
        private readonly IUserDataService _userDataService;
     


        public MainViewModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
          
        }

     
    }
}
