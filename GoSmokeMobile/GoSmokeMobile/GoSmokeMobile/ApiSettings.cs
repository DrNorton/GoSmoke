using System;
using System.Collections.Generic;
using System.Text;
using GoSmokeMobile.Api;
using GoSmokeMobile.Api.Models;

namespace GoSmokeMobile
{
    public class ApiSettings:IApiSettings
    {
        public string BaseUrl
        {
            get { throw new NotImplementedException(); }
        }

        public Token Token
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
