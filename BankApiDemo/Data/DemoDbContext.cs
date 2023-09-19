using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApiDemo.Data
{
    public class DemoDbContext : IdentityDbContext<IdentityUser>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }
    }
    
}
