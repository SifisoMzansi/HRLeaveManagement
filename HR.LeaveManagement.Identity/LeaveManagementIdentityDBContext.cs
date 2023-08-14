using HR.LeaveManagement.Identity.Configurations;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using HR.LeaveManagement.Identity
namespace HR.LeaveManagement.Identity
{
    public class LeaveManagementIdentityDBContext :IdentityDbContext<ApplicationUser>
    {
        public LeaveManagementIdentityDBContext(DbContextOptions<LeaveManagementIdentityDBContext> options) :base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

        }
    }
}
