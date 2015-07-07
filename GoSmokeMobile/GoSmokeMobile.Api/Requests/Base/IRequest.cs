using System.Collections.Generic;
using System.Net.Http;
using GoSmokeMobile.Api.Models;

namespace GoSmokeMobile.Api.Requests.Base
{
    public interface IRequest
    {
        string BaseUrl { get; set; }
        string Controller { get; }
        string MethodName { get; }

        HttpMethod Type { get; set; }

        Token Token { get; set; }
        Dictionary<string, object> Params { get; } 
    }
}
