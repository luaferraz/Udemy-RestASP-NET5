using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginService _loginService;
        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("signing")]
        public IActionResult Signing([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalid client request");
            var token = _loginService.ValidadeCredentials(user);
            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null) return BadRequest("Invalid client request");
            var token = _loginService.ValidadeCredentials(tokenVo);
            if (token == null) return BadRequest("Invalid client request");

            return Ok(token);
        }
    }
}
