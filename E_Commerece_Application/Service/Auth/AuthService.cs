using E_Commerece.Application.Common;
using E_Commerece.Application.Contracts;
using E_Commerece.Application.Dtos;
using E_Commerece.Application.Dtos.AuthDtos;
using E_Commerece.Domain.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Application.Service.Auth
{
    public class AuthService(UserManager<ApplicationUser> userManager) 
        : IAuthService
    {
        public async Task<Result<bool>> CheckUserPassword(string Password , string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                return Result<bool>.Fail(Error.Failure("Failure", "Check Your Password or Email And try Again"));

            return await userManager.CheckPasswordAsync(user,Password) ?
                Result<bool>.Ok(true) 
                : Result<bool>.Fail(Error.Failure("Failure", "Check Your Password or Email And try Again"));

        }

        public Task<Result<bool>> EmailExist(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<UserDTo>> FindUserByEmail(string email)
        {
            
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                return Result<UserDTo>.Fail(Error.Failure("Failure", "Check Your Password or Email And try Again"));

            var mappedUser = new UserDTo()
            {
                DisplayName = user.DisplayName,
                Id = user.Id,
                Eamil = user.Email!,
                UserName = user.UserName!,
                Token = "There is My Token"
            };

            return 
                Result<UserDTo>.Ok(mappedUser);

        }

        public async Task<Result<UserDTo>> Login(LoginDto loginDto)
        {
            var user = await FindUserByEmail(loginDto.Email);
            if (!user.IsSuccess)
                return Result<UserDTo>.Fail(Error.NotFound("User NotFound", "Please Try again"));

            var IsValidPassword = await CheckUserPassword(loginDto.Password, loginDto.Email);
            if (!IsValidPassword.IsSuccess)
                return Result<UserDTo>.Fail(Error.NotFound("User NotFound", "Please Try again"));

            return user;
        }

        public async Task<Result<UserDTo>> Register(RegisterDto RegisterDto)
        {
            if(RegisterDto == null)
                return Result<UserDTo>.Fail(Error.Failure("Failure", "Please Try again"));
            var emailExist = await userManager.FindByEmailAsync(RegisterDto.Email);
            if(emailExist != null)
                return Result<UserDTo>.Fail(Error.Failure("Failure", "Email Exist"));

            var user = new ApplicationUser
            {
                DisplayName = RegisterDto.Displayname,
                UserName = RegisterDto.UserName,
                Email = RegisterDto.Email,
                PhoneNumber = RegisterDto.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user,RegisterDto.Password);
            if(!result.Succeeded)
                return Result<UserDTo>.Fail(Error.Failure("Failure", "Please Try again"));

            var userDto = new UserDTo
            {
                DisplayName = user.DisplayName,
                Id = user.Id,
                Eamil = user.Email!,
                UserName = user.UserName!,
                Token = "There is My Token"
            };

            return Result<UserDTo>.Ok(userDto);
        }
    }
}
