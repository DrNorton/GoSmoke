using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Requests.Base;

namespace GoSmokeMobile.Api.Requests
{
    public class RegisterRequest:BaseParamRequest
    {

        public override string Controller
        {
            get { return "account"; }
        }

        public override string MethodName
        {
            get { return "register"; }
        }

        public RegisterRequest(string phone)
        {
            base.Params.Add("phone",phone);
            base.Type=HttpMethod.Post;
        }
    }
}
