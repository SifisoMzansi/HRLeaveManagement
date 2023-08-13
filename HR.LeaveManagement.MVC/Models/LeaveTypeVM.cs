using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.MVC.Models
{
    public class LeaveTypeVM : CreateleaveTypeVM
    {
        public int Id { get; set; }
    }

    public class CreateleaveTypeVM 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Default Number Of Days")]
        public int DefaultDays { set; get; }
    }
}
