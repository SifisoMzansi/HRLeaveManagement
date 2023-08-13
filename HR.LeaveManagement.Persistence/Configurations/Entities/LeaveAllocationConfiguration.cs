using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations.Entities
{
    public class LeaveAllocationConfiguration : IEntityTypeConfiguration<LeaveAllocationConfiguration>
    {
        public void Configure(EntityTypeBuilder<LeaveAllocationConfiguration> builder)
        {
         }
    }
}
