//using E_Commerece.Domain.Contract;
//using E_Commerece.Domain.Entites.Identity;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace E_Commerece.Infrastructure.Repository
//{
//    internal class UserRepository(UserManager<ApplicationUser> userManager)
//        : IUserRepository
//    {
//        public Task<bool> CheckEmailExist(string email)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<bool> CheckUserPasswordAsync(string password, string email)
//        {
//            var user = await userManager.FindByEmailAsync(email);
//            if (user == null) 
//                return false;
//            return await userManager.CheckPasswordAsync(user, password);
//        }

//        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user,string Password)
//        {
//            return await userManager.CreateAsync(user, Password);
//        }

//        public async Task<ApplicationUser> FindUserbyEmailAsync(string email)
//        {
//            var user = await userManager.FindByEmailAsync(email);
//            return user;
//        }


//    }
//}
