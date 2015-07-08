using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoSmokeMobile.Api.ExceptionRouter;
using GoSmokeMobile.Api.Executer;
using GoSmokeMobile.Api.Models;
using GoSmokeMobile.Api.Requests;
using GoSmokeMobile.Api.Requests.Base;
using GoSmokeMobile.Api.Response;

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

        //public Task<IsRegisteredDto> Autentificate(string phone)
        //{
        //    var tryAuthRequest = new TryAuthRequest(phone);
        //    tryAuthRequest.BaseUrl = _apiSettings.BaseUrl;
        //    return ExecuteWithErrorHandling<IsRegisteredDto>(tryAuthRequest);
        //}

        public Task<ApiResponse<EmptyResult>> Register(string phone)
        {
            var authWithGetProfileRequest = new RegisterRequest(phone);
            authWithGetProfileRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<EmptyResult>(authWithGetProfileRequest);
        }


        public async Task<ApiResponse<Token>> Auth(string phone,string password)
        {
            var authWithGetProfileRequest = new AuthRequest(phone,password);
            authWithGetProfileRequest.BaseUrl = _apiSettings.BaseUrl;
            ApiResponse<Token> response=null;
            try
            {
                 var tokenResponse =await _apiExecuter.ExecuteWithoutApiResponse<TokenResponse>(authWithGetProfileRequest);
                _apiSettings.Token=new Token(){ExpiredIn = tokenResponse.ExpiredIn,TokenType = tokenResponse.TokenType,Value = tokenResponse.Value};
           
            if (String.IsNullOrEmpty(tokenResponse.Error))
            {
                response = new ApiResponse<Token>()
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Result = _apiSettings.Token
                };
               
            }
            }
            catch (Exception e)
            {
                response = new ApiResponse<Token>()
                {
                    ErrorCode = 401,
                    ErrorMessage = e.Message,
                    Result = null
                };
            }
           

            return response;
        }

        

        private Task<ApiResponse<T>> ExecuteWithErrorHandling<T>(IRequest request)
        {
            try
            {
                request.Token = _apiSettings.Token;//Добавляем токен
               return _apiExecuter.Execute<T>(request);
            }
            catch (ApiException ex)
            {
             
               throw ex;
            }
        }


     
    }
}
