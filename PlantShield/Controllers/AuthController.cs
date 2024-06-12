using Habanero.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantShield.Data;
using PlantShield.Dtos;
using PlantShield.Models;
using PlantShield.Services;
using System.Text.Encodings.Web;

namespace PlantShield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(IAuthService authService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _authService = authService;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            var UserId=_context.Users.Where(u=>u.Email==model.Email).FirstOrDefault();

            return Ok(new
            {
                IsAuthenticated = result.IsAuthenticated,
                FirstName =result.FirstName,
                LastName=result.LastName,
                Email=result.Email,
                ExpiresOn=result.ExpiresOn,
                Token=result.Token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new
            {
                IsAuthenticated = result.IsAuthenticated,
                FirstName=result.FirstName,
                LastName=result.LastName,
                Email = result.Email,
                ExpiresOn = result.ExpiresOn,
                Token = result.Token
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Retrieve token from request headers
            string token = Request.Headers["Authorization"];


            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token is missing" });
            }

            var userId = await _context.UserTokens.Where(u => u.Value == token).Select(u => u.UserId).FirstOrDefaultAsync();
            if (userId is null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }


            var user = await _userManager.FindByIdAsync(userId);

            // Invalidate token
            await _userManager.RemoveAuthenticationTokenAsync(user, "Login", "Login");

            return Ok(new { message = "Logout successful" });
        }




        //[Authorize(Roles ="Admin")]
        [HttpPost("addusertorole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddUserToRoleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            
            return Ok("User Added Successfully");
        }

        
    }
}
