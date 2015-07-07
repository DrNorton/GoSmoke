using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSmokeBackend.Controllers.Models
{
    public class AuthModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public string SocialToken { get; set; }
        public string SocialType { get; set; }

        public bool FillProfile { get; set; }

    }
}
