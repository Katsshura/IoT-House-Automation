using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Microservices.Auth.Domain.Interfaces;
using IoT.House.Automation.Microservices.Auth.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IoT.House.Automation.Microservices.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuth authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("signin")]
        public ActionResult SignIn(LoginViewModel login)
        {
            var mapped = _mapper.Map<LoginViewModel, Login>(login);
            var result = _authService.Signin(mapped);

            if (string.IsNullOrWhiteSpace(result))
            {
                return BadRequest("Username or Password is invalid");
            }

            return new JsonResult(result);
        }

        [HttpPost("user")]
        public ActionResult GetUserFromToken([FromBody] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Token cannot be empty");
            }

            try
            {
                var result = _authService.GetSignedUser(token);

                if (result == null)
                {
                    return BadRequest("User with this token cannot be found");
                }

                return new JsonResult(result);
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return BadRequest("Invalid Token!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}