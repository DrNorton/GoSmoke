using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoSmokeMobile.Api.Requests.Base;

namespace GoSmokeMobile.Api.Requests
{
    public class TryAuthRequest:BaseParamRequest
    {
        public override string Controller
        {
            get { return "account"; }
        }

        public override string MethodName
        {
            get { return "tryauth"; }
        }

        public TryAuthRequest(string phone)
        {
            base.Params.Add("Phone",phone);
            base.Type=HttpMethod.Post;
        }
    }
}
