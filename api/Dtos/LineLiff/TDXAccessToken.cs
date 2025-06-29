using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.LineLiff
{
    public class TDXAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string token_type { get; set; }
        public int notbeforepolicy { get; set; }
        public string scope { get; set; }
    }
}
