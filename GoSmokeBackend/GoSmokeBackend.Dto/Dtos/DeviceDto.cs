using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSmokeBackend.Dto.Dtos
{
    public class DeviceDto
    {
        public string RegistrationId { get; set; }
        public string Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }
}
