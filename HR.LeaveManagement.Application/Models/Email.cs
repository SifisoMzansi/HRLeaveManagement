namespace HR.LeaveManagement.Application.Models
{
    public class Email
    {
        public string To { get; set; } = null!;
        public string Subject { get; set; }=null!;
        public string EmailBody { get; set; } = null!;
    }
}
