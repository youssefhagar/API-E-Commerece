using E_Commerece.Domain.Contract;
using E_Commerece.Domain.Entites.Identity;
using E_Commerece.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Infrastructure.DataSeeding
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Task SeedDataAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
