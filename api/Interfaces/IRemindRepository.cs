using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.LineModel;
using api.Dtos.LineLiff;

namespace api.Interfaces
{
    public interface IRemindRepository
    {
        Task<Remind> CreateAsync(CreateRemindRequestDto createDto, AccessLineUserInfo userInfo);
    }
}
