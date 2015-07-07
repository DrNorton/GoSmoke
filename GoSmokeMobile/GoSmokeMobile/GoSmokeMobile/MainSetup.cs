using System;
using System.Collections.Generic;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using GoSmokeMobile;
using GoSmokeMobile.Api;
using GoSmokeMobile.Api.Executer;
using GoSmokeMobile.Api.Facade;
using GoSmokeMobile.Services;
using GoSmokeMobileUniversal.ViewModels;

namespace GoSmokeMobileUniversal
{
    public class MainSetup:MvxApplication
    {
        public override void Initialize()
        {
        
        
            RegisterAppStart<EnterPhoneAuthViewModel>();
            
            Mvx.RegisterType<IApiSettings,ApiSettings>();
            Mvx.RegisterType<IApiExecuter, ApiExecuter>();
            Mvx.RegisterType<IApiFacade, ApiFacade>();
            Mvx.RegisterType<IUserDataService, UserDataService>();
            //Mvx.ConstructAndRegisterSingleton<IApiManager, ApiManager>();
        }
    }
}
