using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WordleOnlineServer.Models
{
    public class ProjectDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        
    }
}
