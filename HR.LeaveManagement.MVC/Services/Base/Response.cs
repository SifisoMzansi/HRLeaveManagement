namespace HR.LeaveManagement.MVC.Services.Base
{
    public class Response<T>
    {
        public string Message { set; get; } = null!;
        public string ValidationErrors { set; get; } = null!;
        public bool Success { get; set; } = false;
        public string Data { set; get; } = null!;

    }
}
