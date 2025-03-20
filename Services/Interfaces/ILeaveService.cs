using H82Travels.Models;

namespace H82Travels.Services.Interfaces
{
    public interface ILeaveService
    {
        // Leave Request Management
        Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaveRequest);
        Task<LeaveRequest> UpdateLeaveRequestAsync(LeaveRequest leaveRequest);
        Task<bool> DeleteLeaveRequestAsync(string leaveRequestId);
        Task<LeaveRequest> GetLeaveRequestByIdAsync(string leaveRequestId);
        
        // Leave Request Retrieval
        Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestsAsync();
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeAsync(string employeeId);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByBranchAsync(string branchId);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByCountryAsync(string country);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByStatusAsync(LeaveStatus status);
        Task<IEnumerable<LeaveRequest>> GetPendingLeaveRequestsForApprovalAsync(string approverId);
        
        // Leave Request Processing
        Task<bool> ApproveLeaveRequestAsync(string leaveRequestId, string approverId);
        Task<bool> RejectLeaveRequestAsync(string leaveRequestId, string approverId, string rejectionReason);
        Task<bool> CancelLeaveRequestAsync(string leaveRequestId);
        
        // Leave Statistics
        Task<int> GetTotalLeaveRequestsCountAsync();
        Task<int> GetPendingLeaveRequestsCountAsync();
        Task<int> GetApprovedLeaveRequestsCountAsync();
        Task<int> GetRejectedLeaveRequestsCountAsync();
        Task<int> GetLeaveRequestsCountByBranchAsync(string branchId);
        Task<int> GetLeaveRequestsCountByCountryAsync(string country);
        
        // Validation
        Task<bool> CanRequestLeaveAsync(string employeeId);
        Task<bool> CanApproveLeaveAsync(string approverId, string leaveRequestId);
        Task<bool> CanCancelLeaveAsync(string employeeId, string leaveRequestId);
        Task<bool> IsOnLeaveAsync(string employeeId, DateTime date);
        Task<int> GetRemainingLeaveDaysAsync(string employeeId);
        
        // Notifications
        Task NotifyLeaveRequestCreatedAsync(string leaveRequestId);
        Task NotifyLeaveRequestApprovedAsync(string leaveRequestId);
        Task NotifyLeaveRequestRejectedAsync(string leaveRequestId);
        Task NotifyLeaveRequestCancelledAsync(string leaveRequestId);
        
        // Reports
        Task<Dictionary<string, int>> GetLeaveStatisticsByBranchAsync(string branchId);
        Task<Dictionary<string, int>> GetLeaveStatisticsByCountryAsync(string country);
        Task<Dictionary<DateTime, int>> GetLeaveStatisticsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}