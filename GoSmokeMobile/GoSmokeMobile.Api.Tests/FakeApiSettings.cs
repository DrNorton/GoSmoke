using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Models;

namespace GoSmokeMobile.Api.Tests
{
    public class FakeApiSettings : IApiSettings
    {
        private Token _savedToken;

        public string BaseUrl
        {
            get { return "http://gosmoke.azurewebsites.net"; }
        }

        public Token SavedToken
        {
            get { return _savedToken; }
            set { _savedToken = value; }
        }
    }
}
