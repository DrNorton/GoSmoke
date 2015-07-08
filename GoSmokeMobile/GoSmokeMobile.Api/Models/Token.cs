using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSmokeMobile.Api.Models
{
    public class Token
    {
        public string Value { get; set; }
        public int ExpiredIn { get; set; }
        public string TokenType { get; set; }
    }
}
