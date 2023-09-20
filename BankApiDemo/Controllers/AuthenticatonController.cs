using BankApiDemo.Data;
using BankApiDemo.DTOs.Requests.Authentication;
using BankApiDemo.DTOs.Responses.Authentication;
using BankApiDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatonController : ControllerBase
    {
        private readonly DemoDbContext _secDb;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        private string? _key;
        public AuthenticatonController(DemoDbContext db, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _secDb = db;
            _userManager = userManager;
            _config = config;

            _key = "2b8a070cff0a4cae98c57f681eae7c51";
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            //Lets check if a request is made with valid request
            if (ModelState.IsValid == false)
            {
                return BadRequest(new LoginResponseDto
                {
                    Status = -1,
                    ErrorsList = new List<string>
                    {
                        "Invalid Dto"
                    }
                });
            }

            //lets check if a provided email is of valid user
            var isUserValid = await _userManager.FindByEmailAsync(requestDto.EmailAddress);
            if (isUserValid == null)
            {
                return Unauthorized(new LoginResponseDto
                {
                    Status = 0,
                    ErrorsList = new List<string>
                    {
                        "Invalid Credintials"
                    }
                });
            }

            //Now that we know that the email is of valid user, lets check if a provided password is valid
            var isPasswordValid = await _userManager.CheckPasswordAsync(isUserValid, requestDto.Password);
            if (isPasswordValid == false)
            {
                return Unauthorized(new LoginResponseDto
                {
                    Status = 0,
                    ErrorsList = new List<string>
                    {
                        "Invalid Credintials"
                    }
                });
            }

            //All credintials are correct, We generate 
            return Ok(new LoginResponseDto
            {
              Status = 1,
              Token = GenerateToken(isUserValid)
            });
        }


        /// <summary>
        /// This method is mainly responsible for generating a JWT token
        /// </summary>
        /// <param name="user">Takes in a user identity</param>
        /// <returns>String of a JWT token</returns>
        private string GenerateToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("userId", user.Id),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = "aa",
                Issuer = "aa",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

                  
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

    }
}
