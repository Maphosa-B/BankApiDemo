using BankApiDemo.Data;
using BankApiDemo.DTOs.Requests.Bank;
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

    public class BankController : ControllerBase
    {
        private readonly DemoDbContext _db;

        public BankController(DemoDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("GetUserAccounts")]
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

            var userAccounts = await _db.Accounts.Where(x => x.IdentityUserId.Equals(userIdClaim.Value)).Include(x => x.AccountType).ToListAsync();
            return Ok(userAccounts);
        }

        [HttpGet]
        [Route("GetSignleAccount")]
        [Authorize]
        public async Task<IActionResult> GetSignleAccount(string accountNumber)
        {
            if(accountNumber.IsNullOrEmpty()) 
            { 
                return BadRequest("Account number cannot be blant");
            }


            var account = await _db.Accounts.Where(x => x.AccountNumber.Equals(accountNumber)).Include(x => x.AccountType).FirstOrDefaultAsync();
            if(account == null)
            {
                return NotFound();
            }else
            {
                return Ok(account);
            }
          
        }

        [HttpPost]
        [Route("Widraw")]
        [Authorize]
        public async Task<IActionResult> Widraw([FromBody] WidrawRequestDto data)
        {

            //Lets start by validating an account number
            if (data.AccountNumber.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Account number cannot be blant");
            }

            var account = await _db.Accounts.Where(x => x.AccountNumber.Equals(data.AccountNumber)).Include(x => x.AccountType).FirstOrDefaultAsync();
            if (account == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Invalid account number");
            }
            
            //Lets validate amount to not exceed account balance nor be less or equal to 0
            if(data.Amount <= 0 )
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Widrawal amount cannot be equal or less than 0");
            }

            if (data.Amount > account.AvailableBalance)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Widrawal amount cannot be greater than available balance");
            }

            //Lets ensure that this account is active 
            if(account.IsActive == false)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You cannot make widrawal on inactive account");
            }

            //Lets check if the is Fixed Deposit, if that is the case then we check if a user has widrawn all the amount
            if(account.AccountType.Name.Equals("Fixed Deposit"))
            {
                if(account.AvailableBalance != data.Amount)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "For a fixed deposit account, you can only wadraw all the cash");
                }
            }

            //Now that all conditions are met lets widraw
            account.AvailableBalance -= data.Amount;

            //Lets add a og for auditing
            await _db.WidrawalLogs.AddAsync(new Models.WidrawalLog
            {
                AccountId = account.Id,
                Amount = data.Amount,
                AddDate = DateTime.UtcNow,
            });

            var status = await _db.SaveChangesAsync();
            if(status>0)
            {
                return Ok("Widrawal has bee made");
            }else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
