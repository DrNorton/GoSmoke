using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Requests.Base;

namespace GoSmokeMobile.Api.Requests
{
    public class AuthRequest:BaseParamRequest
    {

        public override string Controller
        {
            get { return "token"; }
        }

        public override string MethodName
        {
            get { return ""; }
        }

        public AuthRequest(string phone,string password)
        {
            base.Params.Add("grant_type", "password");
            base.Params.Add("userName", phone);
            base.Params.Add("password", password);
            base.Type=HttpMethod.Post;
        }
    }
}
