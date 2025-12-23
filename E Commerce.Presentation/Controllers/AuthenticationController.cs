using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> LogIn(LoginDTO login)
        {
            var Result = await _authenticationService.LogIn(login);
            return ResultHandler(Result);
        }


        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDTO register)
        {
            var result =await _authenticationService.Register(register);
            return ResultHandler(result);
        }

    }
}
