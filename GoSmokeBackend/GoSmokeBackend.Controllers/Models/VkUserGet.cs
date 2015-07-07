using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSmokeBackend.Controllers.Models
{
    public class VkUserGet
    {
        public List<VkResponse> response { get; set; }
    }

    public class VkResponse
    {
        public long uid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}
