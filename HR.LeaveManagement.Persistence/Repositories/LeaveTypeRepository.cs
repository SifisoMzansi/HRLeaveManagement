using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly LeaveManagementDBContext _dBContext;

        public LeaveTypeRepository(LeaveManagementDBContext dBContext) : base(dBContext)
        {
            this._dBContext = dBContext;
        }
    }
}
