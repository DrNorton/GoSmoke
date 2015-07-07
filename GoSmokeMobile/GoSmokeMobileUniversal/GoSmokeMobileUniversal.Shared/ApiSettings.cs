using GoSmokeMobile.Api;
using GoSmokeMobile.Api.Models;

namespace GoSmokeMobileUniversal
{
    public class ApiSettings:IApiSettings
    {
        private Token _token;

        public string BaseUrl
        {
            get { return "http://localhost:54680/api"; }
        }
        public Token SavedToken
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
