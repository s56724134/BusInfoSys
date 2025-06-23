using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.LineModel;
using api.Dtos.LineLiff;
using api.Dtos.Remind;

namespace api.Interfaces
{
    public interface IRemindRepository
    {
        Task<Remind> CreateAsync(CreateRemindRequestDto createDto, AccessLineUserInfo userInfo);
        Task<List<Remind>> GetRemindByUserIdAsync(string userId);
    }
}
