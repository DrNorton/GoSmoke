using GoSmokeMobile.Api.Models;

namespace GoSmokeMobile.Api
{
    public interface IApiSettings
    {
        string BaseUrl { get; }

        Token Token { get; set; }
     
    }
}
