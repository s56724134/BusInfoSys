using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.LineLiff;
using api.Interfaces;


// namespace api.controllers
// {
//     [ApiController]
//     [Route("api/Remind")]
//     public class RemindController : ControllerBase
//     {
//         private readonly IRemindRepository _remindRepo;
//         public RemindController(IRemindRepository remindRepository)
//         {
//             _remindRepo = remindRepository;
//         }

//         [HttpPost]
//         public async Task<IActionResult> Create([FromBody] CreateRemindRequestDto createDto)
//         {
//             Console.WriteLine("===================active==================");
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             var remindModel = _remindRepo.CreateAsync(createDto);
//             //User's remind can not repeat
//             // await _remindRepo.Create();

//             // 先測試因此先放資料
//             return Ok(remindModel);
//         }

//     }
// }
