using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSmokeBackend.Dto.Dtos
{
    public class ProfileResponse
    {
        public string Token { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        
    }
}
