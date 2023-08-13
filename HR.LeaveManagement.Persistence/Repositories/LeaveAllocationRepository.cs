using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveManagementDBContext _dBContext;

        public LeaveAllocationRepository(LeaveManagementDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;             
        }

 
        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var LeaveAllaction = await _dBContext.LeaveAllocations
                                 //.Include(q => q.LeaveType)
                                 .ToListAsync();
            return LeaveAllaction;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var LeaveAllaction = await _dBContext.LeaveAllocations
                                 //.Include(q => q.LeaveType)
                                 .FirstOrDefaultAsync( q=> q.Id==id);
            return LeaveAllaction!;
        }
    }
}
