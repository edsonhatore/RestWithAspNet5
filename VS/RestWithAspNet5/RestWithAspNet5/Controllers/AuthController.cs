using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet5.Business;
using RestWithAspNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class AuthController :ControllerBase    
    {
        private ILoginBusiness _IloginBusiness;

        public AuthController(ILoginBusiness iloginBusiness)
        {
            _IloginBusiness = iloginBusiness;
        }
        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user) 
        {

            if (user == null) return BadRequest("Invalid client request");

            var token = _IloginBusiness.ValidateCredentials(user);
            if (token == null)
            {
                return Unauthorized(); 
            }

                return Ok(token);
           
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null) return BadRequest("Invalid client request");

            var token = _IloginBusiness.ValidateCredentials(tokenVo);
            if (token == null)
            {
                return BadRequest("Invalid client request");
            }

            return Ok(token);

        }


        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {

            var username = User.Identity.Name;

            var result = _IloginBusiness.RevokeToken(username);

            if (!result) return BadRequest("Invalid client request");

              return NoContent();

        }

    }
    }
