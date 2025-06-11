using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;


namespace api.controllers
{
    [Route("api/RichMenu")]
    [ApiController]
    public class LineRichMenuController : ControllerBase
    {
        private readonly ILineRichMenuService _lineRichMenuService;
        public LineRichMenuController(ILineRichMenuService lineRichMenuService)
        {
            _lineRichMenuService = lineRichMenuService;
        }

        // public async Task<> Get
        // {

        // }

    }
}
