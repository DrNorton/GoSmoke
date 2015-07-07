using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using GoSmokeMobile.Api.Facade;
using GoSmokeMobile.Services;
using GoSmokeMobileUniversal.ViewModels.Base;

namespace GoSmokeMobile.ViewModels
{
    public class EnterPasswordViewModel:LoadingScreen
    {
        private readonly IApiFacade _apiFacade;
        private readonly IUserDataService _userDataService;
        private string _password;
        private MvxCommand _authCommand;
        private string _phone;

        public EnterPasswordViewModel(IApiFacade apiFacade,IUserDataService userDataService)
        {
            _apiFacade = apiFacade;
            _userDataService = userDataService;
        }

        public void Init(string phone)
        {
            _phone = phone;
            // use the index here
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                base.RaisePropertyChanged(()=>Password);
            }
        }

        public ICommand AuthCommand
        {
            get
            {
                if (_authCommand == null) _authCommand = new MvxCommand(async () => await Auth());
                return _authCommand;
            }
        }

        private async Task Auth()
        {
            var test=await _apiFacade.AuthAndGetProfile(_phone,Password);
            _userDataService.Profile = test;
            ShowViewModel<PersonalViewModel>();
        }
    }
}
