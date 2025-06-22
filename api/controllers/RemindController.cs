using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.LineLiff;
using api.Interfaces;


namespace api.controllers
{
    [ApiController]
    [Route("api/Remind")]
    public class RemindController : ControllerBase
    {
        private readonly IRemindRepository _remindRepo;
        private readonly ILineIDTokenService _idTokenService;
        public RemindController(IRemindRepository remindRepository, ILineIDTokenService idTokenService)
        {
            _remindRepo = remindRepository;
            _idTokenService = idTokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRemindRequestDto createDto)
        {
            Console.WriteLine("===================active==================");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify UserIDToken
            var lineIDTokenDto = new LineIDTokenDto()
            {
                IdToken = createDto.UserIDToken,
                ClientId = createDto.UserClientId
            };



            // Return data is "AccessLineUserInfo" Model
            var userInfo = await _idTokenService.VerifyIDToken(lineIDTokenDto);
            // pass userInfo and remindRequest
            // Console.WriteLine(userInfo.Sub);
            var remindModel = await _remindRepo.CreateAsync(createDto, userInfo);

            return Ok(remindModel);
        }

    }
}
