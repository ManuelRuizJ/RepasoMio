﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepasoMio.Shared;
using RepasoMio.Shared.DTO;
using static RepasoMio.Web.Helpers.UserHelper;
using static UserHelper;

namespace RepasoMio.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;

        public AccountsController(IUserHelper userHelper, IConfiguration configuration)
        {
            this.userHelper = userHelper;
            this.configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await userHelper.LoginAsync(loginDTO);
            if (result.Succeeded)
            {
                var user = await userHelper.GetUserAsync(loginDTO.Email);
                return Ok(result);
            }
            return BadRequest("Usuario o contraseña incorrecta");
        }

        public async Task<ActionResult> Logout()
        {
            await userHelper.LogoutAsync();
            return RedirectToAction(nameof(Index), "Home")
        }
        private object? BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email!),
                new Claim(ClaimTypes.Role,user.UserType.ToString()),
                new Claim("FirstName",user.FirstName),
                new Claim("LastName",user.LastName),
                new Claim("Photo",user.Photo ?? string.Empty),
                new Claim("CityId",user.CityId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(10);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);
            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiraton = expiration
            };
        }
    }
}
