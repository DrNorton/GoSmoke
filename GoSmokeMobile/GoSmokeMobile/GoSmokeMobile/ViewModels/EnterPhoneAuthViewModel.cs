using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Cirrious.MvvmCross.ViewModels;
using GoSmokeMobile.Api.Facade;
using GoSmokeMobile.Services;
using GoSmokeMobile.ViewModels;
using GoSmokeMobileUniversal.ViewModels.Base;

namespace GoSmokeMobileUniversal.ViewModels
{
    public class EnterPhoneAuthViewModel:LoadingScreen
    {
        private readonly IApiFacade _facade;
        private readonly IUserDataService _userDataService;
        private string _phone="+7";
        private ICommand _tryLoginCommand;
        private ICommand _vkRegisterCommand;
        private Uri _browserUri;
        private Visibility _browserVisibility=Visibility.Collapsed;
        private string _vktoken;

        public Visibility BrowserVisibility
        {
            get { return _browserVisibility; }
            set {
                _browserVisibility = value;
                base.RaisePropertyChanged(()=>BrowserVisibility);
            }
        }


        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                base.RaisePropertyChanged(()=>Phone);
            }
        }

        public ICommand TryLoginCommand
        {
            get
            {
                if(_tryLoginCommand==null) _tryLoginCommand=new MvxCommand(async ()=>await TryLogin());
                return _tryLoginCommand;
            }
        }


        public ICommand VkRegisterCommand
        {
            get
            {
                if (_vkRegisterCommand == null) _vkRegisterCommand = new MvxCommand(VkRegister);
                return _vkRegisterCommand;
            }
        }

        private  void VkRegister()
        {
            BrowserVisibility=Visibility.Visible;
            BrowserUri = new Uri(@"https://oauth.vk.com/authorize?client_id=3383873&redirect_uri=https://oauth.vk.com/blank.html&scope=12234&display=mobile&response_type=token");
        }

        public Uri BrowserUri
        {
            get { return _browserUri; }
            set
            {
                _browserUri = value;
                base.RaisePropertyChanged(()=>BrowserUri);
            }
        }

        private async Task TryLogin()
        {
          var result=await _facade.TryAuth(_phone);
            base.ShowViewModel<EnterPasswordViewModel>(new {phone=_phone});
        }

        public EnterPhoneAuthViewModel(IApiFacade facade,IUserDataService userDataService)
        {
            _facade = facade;
            _userDataService = userDataService;
        }

        public void Init()
        {
            if (_userDataService.Profile != null)
            {
                ShowViewModel<PersonalViewModel>();
            }
        }

        public void OnReceiveToken(string vktoken)
        {
           BrowserVisibility=Visibility.Collapsed;
            _vktoken = vktoken;
        }
    }
}
