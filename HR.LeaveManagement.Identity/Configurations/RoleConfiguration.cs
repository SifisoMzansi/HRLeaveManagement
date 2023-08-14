using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                   Id = "ae4611f3-97d5-4f8c-92e5-b49be074b522", 
                   Name = "Employee",
                   NormalizedName = "EMPLOYEE"
                },
                  new IdentityRole
                  {
                      Id = "f186efb4-e1fb-47d0-840b-54a84a0811db",
                      Name = "Administrator",
                      NormalizedName = "ADMINISTRATOR"
                  }
                );
        }
    }
}
