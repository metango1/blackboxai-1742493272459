using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using H82Travels.Models;

namespace H82Travels.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<InventoryRequest> InventoryRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Branch relationships
            builder.Entity<Branch>()
                .HasOne(b => b.Manager)
                .WithOne(u => u.ManagedBranch)
                .HasForeignKey<Branch>(b => b.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Branch>()
                .HasMany(b => b.Employees)
                .WithOne(u => u.Branch)
                .HasForeignKey(u => u.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure LeaveRequest relationships
            builder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Employee)
                .WithMany(u => u.LeaveRequests)
                .HasForeignKey(lr => lr.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LeaveRequest>()
                .HasOne(lr => lr.ApprovedBy)
                .WithMany(u => u.ApprovedLeaveRequests)
                .HasForeignKey(lr => lr.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure InventoryRequest relationships
            builder.Entity<InventoryRequest>()
                .HasOne(ir => ir.Requestor)
                .WithMany(u => u.InventoryRequests)
                .HasForeignKey(ir => ir.RequestorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InventoryRequest>()
                .HasOne(ir => ir.ApprovedBy)
                .WithMany(u => u.ApprovedInventoryRequests)
                .HasForeignKey(ir => ir.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}