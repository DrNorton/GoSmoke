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
using GoSmokeMobile.ViewModels;
using GoSmokeMobileUniversal.ViewModels;

namespace GoSmokeMobileUniversal
{
    public class MainSetup:MvxApplication
    {
        public override void Initialize()
        {
        
        
            RegisterAppStart<MainViewModel>();
            Mvx.RegisterType<IUserDataService, UserDataService>();
            Mvx.RegisterType<IApiSettings,ApiSettings>();
            Mvx.RegisterType<IApiExecuter, ApiExecuter>();
            Mvx.RegisterType<IApiFacade, ApiFacade>();
           
            //Mvx.ConstructAndRegisterSingleton<IApiManager, ApiManager>();
        }
    }
}
