using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;

        public IdentityDataInitializer(UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager , ILogger<IdentityDataInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        DisplayName = "Ahmed Abbas",
                        UserName = "AhmedAbbas",
                        Email = "ahmedabbas123@gmail.com",
                        PhoneNumber = "01123323556"

                    };
                   

                    var user02 = new ApplicationUser()
                    {
                        DisplayName = "Ziad Abbas",
                        UserName = "ZiadAbbas",
                        Email = "ziadabbas123@gmail.com",
                        PhoneNumber = "0101222123"
                    };
                    await _userManager.CreateAsync(user01, "P@ssw0rd");
                    await _userManager.CreateAsync(user02, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While Seeding Identity Database : Message = {ex.Message}"); 
            }


        }
    }
}
