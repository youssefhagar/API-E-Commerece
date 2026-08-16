using E_Commerece.Application.Contracts;
using E_Commerece.Application.Dtos.AuthDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace E_Commerece.API.Controllers
{
    public class AuthController(IAuthService authService) 
        : ApiBaseController
    {


        //Login
        [HttpPost("login")]
        public async Task<ActionResult<UserDTo>> Login(LoginDto loginDto)
            => ToActionResult( await authService.Login(loginDto));

        //Register
        [HttpPost("register")]
        public async Task<ActionResult<UserDTo>> Register(RegisterDto registerDto)
            => ToActionResult( await authService.Register(registerDto));

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> Register([FromQuery]string email)
            => ToActionResult(await authService.EmailExist(email));

        /*

         {
  "email": "youssef@gmail.com",
  "password": "P@ssw0rd"
} 

         * */

    }
}
