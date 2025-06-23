using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Remind;
using api.Dtos.LineLiff;
using api.Interfaces;
using api.Mapper;


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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRemind()
        {
            // User is the HttpContext.User in ASP.NET Core.
            // It is an instance of the ClaimsPrincipal class.
            // It has a property called Identity, which represents the "identity" of the current user.
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("無法取得使用者 ID");
            }

            var remindModel = await _remindRepo.GetRemindByUserIdAsync(userId);
            var remindDto = remindModel.Select(r => r.ToRemindDto());

            return Ok(remindDto);

        }
    }
}
