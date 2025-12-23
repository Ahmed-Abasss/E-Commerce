using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.IdentityDtos
{
    public record RegisterDTO([EmailAddress]string Email , string UserName 
        , string Password , string DisplayName , string PhoneNumber);
    
}
