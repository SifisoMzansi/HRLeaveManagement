using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveManagementDBContext _dbContext;

        public LeaveAllocationRepository(LeaveManagementDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;             
        }

 
        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var LeaveAllaction = await _dbContext.LeaveAllocations
                                 //.Include(q => q.LeaveType)
                                 .ToListAsync();
            return LeaveAllaction;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var LeaveAllaction = await _dbContext.LeaveAllocations
                                 //.Include(q => q.LeaveType)
                                 .FirstOrDefaultAsync( q=> q.Id==id);
            return LeaveAllaction!;
        }
        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _dbContext.AddRangeAsync(allocations);
        //    await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _dbContext.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
                                        && q.LeaveTypeID == leaveTypeId
                                        && q.Period == period);
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _dbContext.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId
            && q.LeaveTypeID==leaveTypeId);
         }
    }
}
