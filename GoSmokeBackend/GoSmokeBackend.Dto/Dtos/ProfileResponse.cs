using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoSmokeBackend.Dto.Dtos
{
    public class ProfileResponse
    {
        public Token Token { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        
    }

    public class Token
    {
        [JsonProperty("access_token")]
        public string Value { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiredIn { get; set; }
    }
}
