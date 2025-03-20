using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H82Travels.Models
{
    public class Branch
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        public string? Province { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string ManagerId { get; set; } = string.Empty;

        [ForeignKey("ManagerId")]
        public virtual ApplicationUser Manager { get; set; } = null!;

        public virtual ICollection<ApplicationUser> Employees { get; set; } = new List<ApplicationUser>();

        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

        public virtual ICollection<InventoryRequest> InventoryRequests { get; set; } = new List<InventoryRequest>();

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}