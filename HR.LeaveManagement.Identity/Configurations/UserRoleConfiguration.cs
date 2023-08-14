using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "ae4611f3-97d5-4f8c-92e5-b49be074b522",
                    UserId = "c31b727f-e520-4c36-8fc0-c295aedb37b2"
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "f186efb4-e1fb-47d0-840b-54a84a0811db",
                     UserId = "522e8684-5eda-461a-ab67-9a9d3abf6dd8"
                 });
         }
    }
}
