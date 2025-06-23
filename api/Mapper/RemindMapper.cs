using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Remind;
using api.Models.LineModel;

namespace api.Mapper
{
    public static class RemindMapper
    {
        public static RemindDto ToRemindDto(this Remind remind)
        {
            return new RemindDto()
            {
                Id = remind.Id,
                StopName = remind.Stop?.StopName ?? "",
                RouteName = remind.Stop?.Bus?.Route ?? "",
                RemindTime = remind.RemindTime != null ? new RemindTimeValueDto()
                {
                    SelectedRemindTime = remind.RemindTime.SelectedRemindTime,
                    RepeatWeekTime = remind.RemindTime.RepeatWeekTime ?? new List<string>(),
                    BusShowUpTime = new BusShowUpTimeDto()
                    {
                        Start = remind.RemindTime.BusShowUpTime?.Start ?? "",
                        End = remind.RemindTime.BusShowUpTime?.End ?? ""
                    }
                } : null
            };
        }
    }
}
