using System;
using GoSmokeMobile.Api;
using GoSmokeMobile.Api.Executer;
using GoSmokeMobile.Api.Facade;
using NUnit.Framework;



namespace WherePhone.Api.Tests
{
  
    [TestFixture()]
    public class ApiServiceTest
    {
        [Test()]
        public async void GetPhones()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.TryAuth("+79166728879");
            Assert.IsFalse(result.IsRegistered);
        }
       

  
    }
}
