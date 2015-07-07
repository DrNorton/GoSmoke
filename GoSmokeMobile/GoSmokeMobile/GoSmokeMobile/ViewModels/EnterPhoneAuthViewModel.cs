using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

    }
}
