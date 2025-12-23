using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>> LogIn(LoginDTO login);

        Task<Result<UserDto>> Register(RegisterDTO register);
    }
}
