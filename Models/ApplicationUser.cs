using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace H82Travels.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        public string? Province { get; set; }

        public string? City { get; set; }

        [StringLength(50)]
        public string? EmployeeId { get; set; }

        public string? BranchId { get; set; }

        public virtual Branch? Branch { get; set; }

        // Navigation property for managed branch (if user is a branch manager)
        public virtual Branch? ManagedBranch { get; set; }

        // Navigation properties for leave requests
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public virtual ICollection<LeaveRequest> ApprovedLeaveRequests { get; set; } = new List<LeaveRequest>();

        // Navigation properties for inventory requests
        public virtual ICollection<InventoryRequest> InventoryRequests { get; set; } = new List<InventoryRequest>();
        public virtual ICollection<InventoryRequest> ApprovedInventoryRequests { get; set; } = new List<InventoryRequest>();
    }
}