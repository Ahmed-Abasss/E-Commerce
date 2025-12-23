using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<UserDto>> LogIn(LoginDTO login)
        {
            var user =await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return Error.InvalidCredentials("user.InvalidCredintials");

            var isExistedMatchedPassword =await _userManager.CheckPasswordAsync(user,login.Password);

            if (!isExistedMatchedPassword)
                return Error.InvalidCredentials("user.InvalidCredintials");

            return new UserDto(user.DisplayName, user.Email!, "Token");
        }

        public async Task<Result<UserDto>> Register(RegisterDTO register)
        {
            var User = new ApplicationUser() { 
            Email = register.Email,
            DisplayName = register.DisplayName,
            PhoneNumber= register.PhoneNumber,
            UserName = register.UserName,
            };
          var result= await _userManager.CreateAsync(User, register.Password);

            if (result.Succeeded)
                return new UserDto(User.DisplayName, User.Email, "Token");

            return Result<UserDto>.Fail( result.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList());

        }
    }
}
