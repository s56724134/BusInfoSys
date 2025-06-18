using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.LineModel
{
    public class Remind
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int StopId { get; set; }
        // JSON 欄位：建議存在 JSON 字串或轉成複合型別
        public RemindTimeValue? RemindTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public User User { get; set; }
        public Stop Stop { get; set; }
    }

    public class RemindTimeValue
    {
        public string SelectedRemindTime { get; set; } = string.Empty;
        public List<string> RepeatWeekTime { get; set; } = new();
        public BusShowUpTime BusShowUpTime { get; set; } = new();
    }

    public class BusShowUpTime
    {
        public string Start { get; set; } = string.Empty;
        public string End { get; set; } = string.Empty;
    }
}
