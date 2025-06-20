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
    [Route("api/lineuserverify")]
    public class LineIDTokenVerifyController : ControllerBase
    {
        private readonly ILineIDTokenService _lineIDToken;
        public LineIDTokenVerifyController(ILineIDTokenService lineIDToken)
        {
            _lineIDToken = lineIDToken;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyIdToken([FromBody] LineIDTokenDto dto)
        {
            Console.WriteLine("收到請求");
            // Call
            var legalUserId = await _lineIDToken.VerifyIDToken(dto);
            if (legalUserId != null)
            {
                // 驗證成功，回傳 200 OK 和資料
                return Ok(legalUserId);
            }
            else
            {
                // 驗證失敗，回傳 401 Unauthorized 或 400 BadRequest
                return Unauthorized(new { message = "ID Token 驗證失敗" });
            }
        }
    }
}
