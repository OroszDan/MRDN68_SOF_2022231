﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MRDN68_SOF_2022231.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MRDN68_SOF_2022231.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claim = new List<Claim> { new Claim(JwtRegisteredClaimNames.Sub, user.UserName) };
                foreach (var role in await _userManager.GetRolesAsync(user))
                {
                    claim.Add(new Claim(ClaimTypes.Role, role));
                }
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nagyonhosszutitkoskodhelye"));
                var token = new JwtSecurityToken(
                                    issuer: "http://www.security.org", audience: "http://www.security.org",
                                    claims: claim, expires: DateTime.Now.AddMinutes(60),
                                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPut]
        public async Task<IActionResult> InsertUser([FromBody] RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //PhotoContentType = model.PhotoContentType,
                //PhotoData = model.PhotoData
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            var res = await _userManager.AddToRoleAsync(user, "Admin");
            ;
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInfos()
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Email == this.User.Identity.Name);
            return Ok(new
            {
                UserName = user.UserName,
                //FirstName = user.FirstName,
                //LastName = user.LastName,
                Email = user.Email,
                //PhotoData = user.PhotoData,
                //PhotoContentType = user.PhotoContentType,
                Roles = await _userManager.GetRolesAsync(user)
            });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteMyself()
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Email == this.User.Identity.Name);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
