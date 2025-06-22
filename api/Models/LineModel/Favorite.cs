using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.LineModel
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StopId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        // JSON 資料，用字串儲存 JSON 結構
        public User User { get; set; }
        public Stop Stop { get; set; }
    }
}
