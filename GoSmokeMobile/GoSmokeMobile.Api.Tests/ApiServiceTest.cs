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
             Assert.AreNotEqual(result.ErrorCode,0);
         }

         [Test()]
         [TestCase("+79166728879","963538",true)]
         [TestCase("+79166728879", "9635",false)]
         public async void Auth(string phone,string password,bool isCorrectPassword)
         {
             var apiSettings = new FakeApiSettings();
             var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
             var result = await facade.Auth(phone,password);
             if (isCorrectPassword)
             {
                 Assert.AreEqual(result.ErrorCode, 0);
             }
             else
             {
                 Assert.AreNotEqual(result.ErrorCode, 0);
             }
        
         }
    }
}
