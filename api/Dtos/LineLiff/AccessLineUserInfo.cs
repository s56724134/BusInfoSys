using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.LineLiff
{
    public class AccessLineUserInfo
    {
        public string Iss { get; set; }
        public string Sub { get; set; }
        public string Aud { get; set; }
        public int Exp { get; set; }
        public int Iat { get; set; }
        public string Nonce { get; set; }
        public List<string> Amr { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
    }
}
