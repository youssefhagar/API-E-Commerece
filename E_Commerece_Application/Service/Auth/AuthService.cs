using E_Commerece.Application.Common;
using E_Commerece.Application.Contracts;
using E_Commerece.Application.Dtos;
using E_Commerece.Application.Dtos.AuthDtos;
using E_Commerece.Domain.Contract;
using E_Commerece.Domain.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Application.Service.Auth
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        IUserStore userStore,
        IAcessTokenGenerator tokenService) 
        : IAuthService
    {
        //public async Task<Result<bool>> CheckUserPassword(string Password , string email)
        //{
        //    var user = await userManager.FindByEmailAsync(email);

        //    if (user == null)
        //        return Result<bool>.Fail(Error.Failure("Failure", "Check Your Password or Email And try Again"));

        //    return await userManager.CheckPasswordAsync(user,Password) ?
        //        Result<bool>.Ok(true) 
        //        : Result<bool>.Fail(Error.Failure("Failure", "Check Your Password or Email And try Again"));

        //}


        //public async Task<Result<UserDTo>> FindUserByEmail(string email)
        //{
            
        //    var user = await userManager.FindByEmailAsync(email);

        //    if (user == null)
        //        return Result<UserDTo>.Fail(Error.Failure("Failure", "Check Your Password or Email And try Again"));

        //    var mappedUser = new UserDTo()
        //    {
        //        DisplayName = user.DisplayName,
        //        Id = user.Id,
        //        Eamil = user.Email!,
        //        UserName = user.UserName!,
        //        Token = tokenService.GenerateToken(, new List<string>())
        //    };

        //    return 
        //        Result<UserDTo>.Ok(mappedUser);

        //}

        public async Task<Result<UserDTo>> Login(LoginDto loginDto)
        {
            var user = await userStore.FindUserbyEmailAsync(loginDto.Email);
            if (user == null)
                return Result<UserDTo>.Fail(Error.NotFound("User NotFound", "Please Try again"));

            var IsValidPassword = await userStore.CheckUserPasswordAsync(loginDto.Password, loginDto.Email);
            if (!IsValidPassword)
                return Result<UserDTo>.Fail(Error.NotFound("User NotFound", "Please Try again"));

            var mappedUser = new UserDTo()
            {
                DisplayName = user.DisplayName,
                Id = user.Id,
                Eamil = user.Eamil,
                UserName = user.UserName!,
                Token = tokenService.GenerateToken(user, new List<string>())
            };

            return Result<UserDTo>.Ok(mappedUser);
        }

        public async Task<Result<UserDTo>> Register(RegisterDto RegisterDto)
        {
            if(RegisterDto == null)
                return Result<UserDTo>.Fail(Error.Failure("Failure", "Please Try again"));
            var emailExist = await userStore.FindUserbyEmailAsync(RegisterDto.Email);
            if(emailExist != null)
                return Result<UserDTo>.Fail(Error.Failure("Failure", "Email Exist"));

            var user = new ApplicationUser
            {
                DisplayName = RegisterDto.Displayname,
                UserName = RegisterDto.UserName,
                Email = RegisterDto.Email,
                PhoneNumber = RegisterDto.PhoneNumber,
            };
            var result = await userStore.CreateUserAsync(user,RegisterDto.Password);
            if(result == null)
                return Result<UserDTo>.Fail(Error.Failure("Failure", "Please Try again"));

            var userDto = new UserDTo
            {
                DisplayName = result.DisplayName,
                Id = result.Id,
                Eamil = result.Eamil!,
                UserName = result.UserName!,
                Token = tokenService.GenerateToken(result, new List<string>())
            };

            return Result<UserDTo>.Ok(userDto);
        }
    }
}
