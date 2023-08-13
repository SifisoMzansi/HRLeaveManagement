﻿using HR.LeaveManagement.Application.DTO.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries
{

    public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestDto>>
    {
    }
}
