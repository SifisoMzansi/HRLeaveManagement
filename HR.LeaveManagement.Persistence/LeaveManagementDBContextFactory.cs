using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HR.LeaveManagement.Persistence
{
    public class LeaveManagementDBContextFactory : IDesignTimeDbContextFactory<LeaveManagementDBContext>
    {
        public LeaveManagementDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<LeaveManagementDBContext>();
            var connectionString = configuration.GetConnectionString
                ("LeaveManagementConnectionString");

            builder.UseSqlServer(connectionString);
            return new LeaveManagementDBContext(builder.Options);
        }
    }
}
