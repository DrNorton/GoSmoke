using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Requests.Base;

namespace GoSmokeMobile.Api.Requests
{
    public class AuthWithGetProfileRequest:BaseParamRequest
    {
        public override string Controller
        {
            get { return "account"; }
        }

        public override string MethodName
        {
            get { return "getprofile"; }
        }

        public AuthWithGetProfileRequest(string phone,string password)
        {
            base.Params.Add("Login",phone);
            base.Params.Add("Password", password);
            base.Type=HttpMethod.Post;
        }
    }
}
