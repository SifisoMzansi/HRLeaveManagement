using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence
{
    public class LeaveManagementDBContext:DbContext
    {
        public LeaveManagementDBContext(DbContextOptions<LeaveManagementDBContext> options) : base(options) 
        { }
            

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<LeaveAllocation>().HasMany(c => c)
            //               .HasForeignKey(con => con.  );

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDBContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
                {
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.DateCreated = DateTime.Now;
                    }
                }
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}
