using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H82Travels.Models
{
    public class LeaveRequest
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        [ForeignKey("EmployeeId")]
        public virtual ApplicationUser Employee { get; set; } = null!;

        [Required]
        public string BranchId { get; set; } = string.Empty;

        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        public string? ApprovedById { get; set; }

        [ForeignKey("ApprovedById")]
        public virtual ApplicationUser? ApprovedBy { get; set; }

        [Required]
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        [StringLength(500)]
        public string? RejectionReason { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [NotMapped]
        public int DurationInDays => (EndDate - StartDate).Days + 1;
    }

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }
}