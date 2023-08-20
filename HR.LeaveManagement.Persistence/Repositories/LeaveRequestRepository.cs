using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly LeaveManagementDBContext _dBContext;

        public LeaveRequestRepository(LeaveManagementDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool approvalStatus)
        {
            leaveRequest.Approved = approvalStatus;
            _dBContext.Entry(leaveRequest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           // await _dBContext.SaveChangesAsync();
         }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _dBContext.LeaveRequests 
                                //.Include(q => q.LeaveType)
                                 .ToListAsync();

            return leaveRequests;

         }

        public async  Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequests = await _dBContext.LeaveRequests
                                             //.Include(q => q.LeaveType)
                                              .FirstOrDefaultAsync(q => q.Id == id);

            return leaveRequests!;
        }
    }
}
