using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Remind
{
    public class RemindDto
    {
        public int Id { get; set; }
        public string StopName { get; set; }
        public string RouteName { get; set; }
        public RemindTimeValueDto? RemindTime { get; set; }
    }
    public class RemindTimeValueDto
    {
        public string SelectedRemindTime { get; set; } = string.Empty;
        public List<string> RepeatWeekTime { get; set; } = new();
        public BusShowUpTimeDto BusShowUpTime { get; set; } = new();
    }

    public class BusShowUpTimeDto
    {
        public string Start { get; set; } = string.Empty;
        public string End { get; set; } = string.Empty;
    }
}
