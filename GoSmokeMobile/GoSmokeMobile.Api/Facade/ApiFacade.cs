using System.Collections.Generic;
using System.Threading.Tasks;
using GoSmokeMobile.Api.ExceptionRouter;
using GoSmokeMobile.Api.Executer;
using GoSmokeMobile.Api.Models;
using GoSmokeMobile.Api.Requests;
using GoSmokeMobile.Api.Requests.Base;

namespace GoSmokeMobile.Api.Facade
{
    public class ApiFacade : IApiFacade
    {
        private readonly IApiExceptionRouter _exceptionRouter;
        private IApiExecuter _apiExecuter;
        private readonly IApiSettings _apiSettings;
        

        public ApiFacade(IApiExecuter apiExecuter,IApiSettings apiSettings)
        {
            _apiExecuter = apiExecuter;
            _apiSettings = apiSettings;
   
        }

     

        

        private Task<T> ExecuteWithErrorHandling<T>(IRequest request)
        {
            try
            {
                request.Token = _apiSettings.SavedToken;//Добавляем токен
               return _apiExecuter.Execute<T>(request);
            }
            catch (ApiException ex)
            {
             
               throw ex;
            }
        }


     
    }
}
