using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.LineLiff;


namespace api.Interfaces
{
    public interface ILineIDTokenService
    {
        Task<AccessLineUserInfo> VerifyIDToken(LineIDTokenDto dto);
    }
}
