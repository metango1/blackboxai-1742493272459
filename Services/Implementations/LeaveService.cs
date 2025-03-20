using H82Travels.Models;
using H82Travels.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H82Travels.Services.Implementations
{
    public class LeaveService : ILeaveService
    {
        private readonly ApplicationDbContext _context;

        public LeaveService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
            return leaveRequest;
        }

        public async Task<LeaveRequest> UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
            return leaveRequest;
        }

        public async Task<bool> DeleteLeaveRequestAsync(string leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            if (leaveRequest == null) return false;

            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(string leaveRequestId)
        {
            return await _context.LeaveRequests.FindAsync(leaveRequestId);
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestsAsync()
        {
            return await _context.LeaveRequests.ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeAsync(string employeeId)
        {
            return await _context.LeaveRequests.Where(lr => lr.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByBranchAsync(string branchId)
        {
            return await _context.LeaveRequests.Where(lr => lr.BranchId == branchId).ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByCountryAsync(string country)
        {
            return await _context.LeaveRequests
                .Include(lr => lr.Branch)
                .Where(lr => lr.Branch.Country == country)
                .ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByStatusAsync(LeaveStatus status)
        {
            return await _context.LeaveRequests.Where(lr => lr.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetPendingLeaveRequestsForApprovalAsync(string approverId)
        {
            return await _context.LeaveRequests
                .Where(lr => lr.Status == LeaveStatus.Pending)
                .ToListAsync();
        }

        public async Task<bool> ApproveLeaveRequestAsync(string leaveRequestId, string approverId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            if (leaveRequest == null) return false;

            leaveRequest.Status = LeaveStatus.Approved;
            leaveRequest.ApprovedById = approverId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectLeaveRequestAsync(string leaveRequestId, string approverId, string rejectionReason)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            if (leaveRequest == null) return false;

            leaveRequest.Status = LeaveStatus.Rejected;
            leaveRequest.RejectionReason = rejectionReason;
            leaveRequest.ApprovedById = approverId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelLeaveRequestAsync(string leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            if (leaveRequest == null) return false;

            leaveRequest.Status = LeaveStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CanRequestLeaveAsync(string employeeId)
        {
            // Implement logic to check if the employee can request leave
            return true; // Placeholder
        }

        public async Task<bool> CanApproveLeaveAsync(string approverId, string leaveRequestId)
        {
            // Implement logic to check if the user can approve the leave request
            return true; // Placeholder
        }

        public async Task<bool> CanCancelLeaveAsync(string employeeId, string leaveRequestId)
        {
            // Implement logic to check if the user can cancel the leave request
            return true; // Placeholder
        }

        public async Task<bool> IsOnLeaveAsync(string employeeId, DateTime date)
        {
            // Implement logic to check if the employee is on leave on the given date
            return false; // Placeholder
        }

        public async Task<int> GetRemainingLeaveDaysAsync(string employeeId)
        {
            // Implement logic to calculate remaining leave days
            return 0; // Placeholder
        }

        public async Task NotifyLeaveRequestCreatedAsync(string leaveRequestId)
        {
            // Implement notification logic
        }

        public async Task NotifyLeaveRequestApprovedAsync(string leaveRequestId)
        {
            // Implement notification logic
        }

        public async Task NotifyLeaveRequestRejectedAsync(string leaveRequestId)
        {
            // Implement notification logic
        }

        public async Task NotifyLeaveRequestCancelledAsync(string leaveRequestId)
        {
            // Implement notification logic
        }

        public async Task<Dictionary<string, int>> GetLeaveStatisticsByBranchAsync(string branchId)
        {
            // Implement logic to get leave statistics by branch
            return new Dictionary<string, int>(); // Placeholder
        }

        public async Task<Dictionary<string, int>> GetLeaveStatisticsByCountryAsync(string country)
        {
            // Implement logic to get leave statistics by country
            return new Dictionary<string, int>(); // Placeholder
        }

        public async Task<Dictionary<DateTime, int>> GetLeaveStatisticsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Implement logic to get leave statistics by date range
            return new Dictionary<DateTime, int>(); // Placeholder
        }
    }
}