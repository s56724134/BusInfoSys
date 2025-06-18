using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.LineModel
{
    public class Stop
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string? StopName { get; set; }

        // 外鍵導覽屬性
        public Bus Bus { get; set; }

        public List<Favorite> Favorites { get; set; } = new();
        public List<Remind> Reminds { get; set; } = new();
    }
}
