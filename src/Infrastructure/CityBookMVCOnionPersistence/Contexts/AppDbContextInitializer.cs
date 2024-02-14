using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionDomain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBookMVCOnionPersistence.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AppDbContextInitializer(AppDbContext context, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task InitializeDbContextAsync()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task CreateUserRolesAsync()
        {
            foreach (var role in Enum.GetNames(typeof(AdminRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }
            foreach (var role in Enum.GetNames(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }
        }

        public async Task InitializeAdminAsync()
        {
            User admin = new User
            {
                Name = "admin",
                Surname = "admin",
                Email = _configuration["AdminSettings:Email"],
                UserName = _configuration["AdminSettings:UserName"],
                About = "zuzu",
                Address = "isNull",
                Face = "isNull",
                Inst= "isNull",
                Link = "link",
                Tvit = "isNull",
                WebSite= "isNull",
            };

            await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
            await _userManager.AddToRoleAsync(admin, AdminRoles.Admin.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
            await _userManager.ConfirmEmailAsync(admin, token);
        }
    }
}
