using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Models;
using GoSmokeMobile.Api.Requests;
using GoSmokeMobile.Api.Requests.Base;
using RestSharp.Portable;
using RestSharp.Portable.Deserializers;

namespace GoSmokeMobile.Api.Executer
{
    public class ApiExecuter : IApiExecuter
    {
       

        public ApiExecuter(IApiSettings apiSettings)
        {
            
       
        }
        public async Task<ApiResponse<T>> Execute<T>(IRequest request)
        {
            var restClient = new RestSharp.Portable.RestClient();
            restClient.AddHandler("application/json", new JsonDeserializer());
            restClient.BaseUrl = new Uri(request.BaseUrl);


            var restRequest = CreateRequest(request);
            var uri = restClient.BuildUri(restRequest);
            Debug.WriteLine(uri);
            try
            {
                var response = await restClient.Execute<ApiResponse<T>>(restRequest);
                var bytes = response.RawBytes;
                var str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    var data = response.Data;
                return data;
            }
            catch (Exception e)
            {
                throw new ApiException(10000, e.Message);
            }
        
         
        }


        public async Task<T> ExecuteWithoutApiResponse<T>(IRequest request)
        {
            var restClient = new RestSharp.Portable.RestClient();
           
            restClient.BaseUrl = new Uri(request.BaseUrl);


            var restRequest = CreateRequest(request);
            foreach (var par in request.Params)
            {
                restRequest.AddParameter(par.Key, par.Value);
            }
            var uri = restClient.BuildUri(restRequest);
            Debug.WriteLine(uri);
         
                var response = await restClient.Execute<T>(restRequest);
                var bytes = response.RawBytes;
                var str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                var data = response.Data;
                return data;

        }

        private RestRequest CreateRequest(IRequest request)
        {
            var restRequest = CreateAndPrepareByUrl(request);
            restRequest.Parameters.Clear();
            if (request.Token != null) { restRequest.AddHeader("Authorization", String.Format("{0} {1}", "Bearer", request.Token.Value)); }//добавляем токен
            if (request.Type == HttpMethod.Post)
            {
                if (request is AuthRequest)
                {
                    
                }
                else
                {
                    restRequest.AddHeader("Accept", "application/json");
                    if (request.Params != null && request.Params.Any())
                    {
                        restRequest.AddBody(request.Params);
                    }
                }

               
               
            }

            if (request.Type == HttpMethod.Get)
            {
                if (request.Params != null && request.Params.Any())
                {
                    foreach (var param in request.Params)
                    {
                        restRequest.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                    }
                  
                }
            }


            return restRequest;
        }

        private  RestRequest CreateAndPrepareByUrl(IRequest request)
        {
            string url;
            if(String.IsNullOrEmpty(request.Controller))
            {
                url = request.MethodName;
            }
            else
            {
                if (String.IsNullOrEmpty(request.MethodName))
                {
                    url = request.Controller;
                }
                else
                {
                    url = String.Format("{0}/{1}", request.Controller, request.MethodName);
                }
            }
           
           
            var restRequest = new RestRequest(url, request.Type);
        
            return restRequest;
        }
      
    }
}
