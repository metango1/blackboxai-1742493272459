using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H82Travels.Models
{
    public class InventoryRequest
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string RequestorId { get; set; } = string.Empty;

        [ForeignKey("RequestorId")]
        public virtual ApplicationUser Requestor { get; set; } = null!;

        [Required]
        public string BranchId { get; set; } = string.Empty;

        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal EstimatedAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ApprovedAmount { get; set; }

        public string? ApprovedById { get; set; }

        [ForeignKey("ApprovedById")]
        public virtual ApplicationUser? ApprovedBy { get; set; }

        [Required]
        public InventoryRequestStatus Status { get; set; } = InventoryRequestStatus.Pending;

        public string? ReceiptPath { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }

    public enum InventoryRequestStatus
    {
        Pending,
        ApprovedByCountryHead,
        ApprovedByCompanyHead,
        FundsTransferred,
        Purchased,
        Completed,
        Rejected,
        Cancelled
    }
}