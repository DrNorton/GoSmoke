using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Executer;
using GoSmokeMobile.Api.Facade;
using NUnit.Framework;

namespace GoSmokeMobile.Api.Tests
{
     [TestFixture()]
    public class ApiServiceTest
    {
         [Test()]
         public async void Register()
         {
             var apiSettings = new FakeApiSettings();
             var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
             var result = await facade.Register("+79166728879");
             Assert.Greater(result.Count, 0);
         }
    }
}
