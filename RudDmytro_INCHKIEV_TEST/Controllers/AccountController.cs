using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RudDmytro_INCHKIEV_TEST.Models;

namespace RudDmytro_INCHKIEV_TEST.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;

        public AccountController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        
        public IActionResult Login()
        {

            return View();
        }
        [HttpGet]
     
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Books","Home");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public  IActionResult Login(string login, string pass)
        {
            User user = _context.MyUsers.FirstOrDefault(q => q.Login == login && q.Pass == pass);
            if (user == null)
            {
                return View();
            }

            var token = GenerateJwt(user);
            HttpContext.Session.SetString("JwToken", token);
            return            RedirectToAction("Index", "Books"); Json(token);


        }

         
        


        private string GenerateJwt(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( AuthOptions.KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Login),
                new Claim(JwtRegisteredClaimNames.Email,user.Login),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims,
                signingCredentials: credentials
                );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;

        }
    }
}