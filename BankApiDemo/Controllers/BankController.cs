using BankApiDemo.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BankApiDemo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UserListController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserAccounts()
        {
            var userIdClaim = User.Claims.Where(x => x.Type.Equals("userId")).FirstOrDefault();
            if(userIdClaim == null)
            {
                return Unauthorized();
            }

            if(userIdClaim.Value.IsNullOrEmpty()) 
            {
                return Unauthorized();
            };


            return Ok();
        }
    }
}
