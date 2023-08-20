using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services.Base;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveAllocationService : BaseHttpService , ILeaveAllocationService
    {

        private readonly ILocalStorageService _localStorageService;
        private readonly IClient _client;

        public LeaveAllocationService(ILocalStorageService localStorageService, IClient client) : base(localStorageService, client)
        {
            _localStorageService = localStorageService;
            _client = client;
        }

        public async Task<Response<int>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveAllocationDto createLeaveAllocationDto = new CreateLeaveAllocationDto() { LeaveTypeID = leaveTypeId } ;
                AddBearerToken();
                var apiResponse = await _client.LeaveAllocationPOSTAsync(createLeaveAllocationDto);

                if (apiResponse.Success)
                {
                    response.Success = true;
                }
                else
                {
                    foreach(var error in apiResponse.Errors) 
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch(ApiException exe) 
            {
                return ConvertApiExceptions<int>(exe);
            }
         }
        protected void AddBearerToken()
        {
            if (_localStorageService.Exists("token"))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorageService.GetStorageValue<string>("token"));
            }
        }
    }
}
