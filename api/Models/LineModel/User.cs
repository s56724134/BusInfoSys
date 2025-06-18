using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.LineModel
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Profile { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastTime { get; set; }
        // 導覽屬性
        public List<Favorite> Favorites { get; set; } = new();
        public List<Remind> Reminds { get; set; } = new();
    }
}
