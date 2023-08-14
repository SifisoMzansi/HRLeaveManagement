using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
         
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {            
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                  new ApplicationUser
                  {
                      Id = "c31b727f-e520-4c36-8fc0-c295aedb37b2",
                      Email = "admin456sfiso@gmail.com",
                      NormalizedEmail = "ADMIN456SFISO@GMAIL.COM",
                      FirstName="Sifiso",
                      LastName="Khoza", 
                      UserName= "admin456sfiso@gmail.com", 
                      NormalizedUserName= "ADMIN456SFISO@GMAIL.COM",
                      PasswordHash = hasher.HashPassword(null,"P@ssword1"),
                      EmailConfirmed=true
                  },
                    new ApplicationUser
                    {
                        Id = "522e8684-5eda-461a-ab67-9a9d3abf6dd8",
                        Email = "admin456sfiso1@gmail.com",
                        NormalizedEmail = "ADMIN456SFISO1@GMAIL.COM",
                        FirstName = "Sifiso1",
                        LastName = "Khoza1",
                        UserName = "admin456sfiso1@gmail.com",
                        NormalizedUserName = "ADMIN456SFISO1@GMAIL.COM",
                        PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                        EmailConfirmed = true
                    }
                ); 
        }
    }
}
