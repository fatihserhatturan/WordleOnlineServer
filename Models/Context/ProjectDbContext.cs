using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WordleOnlineServer.Models.MsSqlModels;

namespace WordleOnlineServer.Models.Context
{
    public class ProjectDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }


    }
}
