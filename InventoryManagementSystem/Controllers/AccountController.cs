using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _config;
        public AccountController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole<int>> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterDto UserFromRegister)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            ApplicationUser existingUser = await _userManager.FindByEmailAsync(UserFromRegister.Email);
            if (existingUser != null)
                return BadRequest("Email alredy exists");

            ApplicationUser user = new ApplicationUser()
            {
                UserName = UserFromRegister.FullName,
                Email = UserFromRegister.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, UserFromRegister.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await MakeRole("User");
            await _userManager.AddToRoleAsync(user, "User");

            if(user.Email.ToLower() == "admin@gmail.com")
            {
                await MakeRole("Admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return Ok("Account Create Success");
        }

        [NonAction]
        public async Task<bool> MakeRole(string role)
        {
            IdentityRole<int> identityRole = new IdentityRole<int>();
            identityRole.Name = role;
            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if(result.Succeeded)
                return true;
            return false;
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginDto UserFromLogin)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser user = await _userManager.FindByEmailAsync(UserFromLogin.Email);
            if(user == null)
                return BadRequest("Invalid Account");

            bool found = await _userManager.CheckPasswordAsync(user, UserFromLogin.Password);
            if(!found)
                return BadRequest("Invalid Password");

            //-------------------------Create Token------------------------
            string jti = Guid.NewGuid().ToString();
            var userRoles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim("Email", user.Email ?? "")
            };
            if(userRoles != null)
            {
                foreach(var role  in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            SymmetricSecurityKey SecretKey = new(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            SigningCredentials signingCredentials = new SigningCredentials(SecretKey,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _config["JWT:Iss"],
                    audience: _config["JWT:Aud"],
                    expires: DateTime.UtcNow.AddDays(1),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return Ok(new
            {
                expired = DateTime.UtcNow.AddDays(1),
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
