using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Remind
{
    public class CreateRemindRequestDto
    {
        [Required]
        public string UserIDToken { get; set; } = string.Empty;
        [Required]
        public string UserClientId { get; set; } = string.Empty;
        [Required]
        public string UserRouteName { get; set; } = string.Empty;
        [Required]
        public string UserSelectedRemindTime { get; set; }
        [Required]
        public string UserStopName { get; set; } = string.Empty;
        public List<string>? UserRepeatWeekTime { get; set; }
        [Required]
        public BusShowUpTimeRequestDto UserBusShowUpTime { get; set; } = new();
    }
    public class BusShowUpTimeRequestDto
    {
        public string Start { get; set; } = string.Empty;
        public string End { get; set; } = string.Empty;
    }
}
