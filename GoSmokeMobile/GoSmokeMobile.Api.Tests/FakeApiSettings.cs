using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Models;

namespace GoSmokeMobile.Api.Tests
{
    public class FakeApiSettings:IApiSettings
    {
        public string BaseUrl
        {
            get { return "http://localhost:54680/api"; }
        }

        public Token Token { get; set; }
    }
}
