using System;
using System.Collections.Generic;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using GoSmokeMobileUniversal.ViewModels;

namespace GoSmokeMobileUniversal
{
    public class MainSetup:MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
        
            RegisterAppStart<MainViewModel>();
            // _apiExecuter = new ApiExecuter("http://hskingapi.azurewebsites.net/api");

            //Mvx.RegisterType<IRequestExecuterService, RequestExecuterService>();
            //Mvx.ConstructAndRegisterSingleton<IApiManager, ApiManager>();
        }
    }
}
