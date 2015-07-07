using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSmokeBackend.Dto.Dtos
{
    public class SentPushDto
    {
        public string Pns { get; set; }
        public string Message { get; set; }
        public string ToTag { get; set; }
    }
}
