﻿using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveTypeService : BaseHttpService , ILeaveTypeService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _client;

       public LeaveTypeService(IMapper mapper, IClient client , ILocalStorageService localStorageService) : base(localStorageService, client)
        {
            _mapper = mapper;
            _client = client;
            _localStorageService = localStorageService;
        }

        public async Task<Response<int>> CreateLeaveType(CreateleaveTypeVM leaveType)
        {
            try
            {
                StringBuilder errors = new StringBuilder(); 
                var response  = new Response<int>();
                CreateLeaveTypeDto createLeaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                var ApiResponse = await _client.LeaveTypePOSTAsync(createLeaveTypeDto);

                if (ApiResponse.Success)
                {
                    response.Success = true;
                    response.Data = ApiResponse.Id.ToString();
                }
                else
                {
                    foreach (var error in ApiResponse.Errors) 
                    {
                        errors.Append (error + Environment.NewLine);
                    }
                    response.ValidationErrors = errors.ToString();  
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                 await _client.LeaveTypeDELETEAsync(id);
                return new Response<int> { Success = true };
            }
            catch (ApiException ex) 
            {
                return ConvertApiExceptions<int>(ex);
            }
         }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            var leaveType = await _client.LeaveTypeGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypes = await _client.LeaveTypeAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);  
         }

        public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                leaveType.Id = id;
                var leaveTypeDTO = _mapper.Map<LeaveTypeDto>(leaveType);
                await _client.LeaveTypePUTAsync(leaveTypeDTO);
                return new Response<int> { Success = true };
            }
            catch(ApiException ex) 
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
