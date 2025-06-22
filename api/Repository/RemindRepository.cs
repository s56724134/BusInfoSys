using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Models.LineModel;
using api.Dtos.LineLiff;
using api.Data;

namespace api.Repository
{
    public class RemindRepository : IRemindRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public RemindRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<Remind> CreateAsync(CreateRemindRequestDto createDto, AccessLineUserInfo userInfo)
        {
            //Check if the user exists, otherwise add a new one.

            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == userInfo.Sub);
            if (user == null)
            {
                user = new User()
                {
                    Id = userInfo.Sub,
                    Profile = userInfo.Picture
                };
                _dbcontext.Users.Add(user);
                await _dbcontext.SaveChangesAsync();
            }

            //Check if the bus route exists, otherwise add a new one.
            var route = await _dbcontext.Buses.FirstOrDefaultAsync(r => r.Route == createDto.UserRouteName);
            if (route == null)
            {
                route = new Bus()
                {
                    Route = createDto.UserRouteName
                };

                _dbcontext.Buses.Add(route);
                await _dbcontext.SaveChangesAsync();
            }

            //Check if the stop exists, otherwise add a new one.
            var stop = await _dbcontext.Stops.FirstOrDefaultAsync(s => s.StopName == createDto.UserStopName);
            if (stop == null)
            {
                stop = new Stop()
                {
                    BusId = route.Id,
                    StopName = createDto.UserStopName
                };
                _dbcontext.Stops.Add(stop);
                await _dbcontext.SaveChangesAsync();
            }
            //Check if the remind repeat, other create new remind
            bool exists = await _dbcontext.Reminds.AnyAsync(r =>
                r.UserId == userInfo.Sub &&
                r.Stop.Bus.Id == route.Id &&
                r.StopId == stop.Id);

            if (exists)
                throw new InvalidOperationException("已經設定過此提醒");

            // create remind
            var remind = new Remind()
            {
                UserId = userInfo.Sub,
                StopId = stop.Id,
                RemindTime = new RemindTimeValue()
                {
                    SelectedRemindTime = createDto.UserSelectedRemindTime,
                    RepeatWeekTime = createDto.UserRepeatWeekTime ?? new List<string>(),
                    BusShowUpTime = new BusShowUpTime()
                    {
                        Start = createDto.UserBusShowUpTime.Start,
                        End = createDto.UserBusShowUpTime.End
                    }
                }
            };

            _dbcontext.Reminds.Add(remind);
            await _dbcontext.SaveChangesAsync();

            return remind;

        }
    }
}
