using H82Travels.Models;

namespace H82Travels.Services.Interfaces
{
    public interface IInventoryService
    {
        // Inventory Request Management
        Task<InventoryRequest> CreateInventoryRequestAsync(InventoryRequest request);
        Task<InventoryRequest> UpdateInventoryRequestAsync(InventoryRequest request);
        Task<bool> DeleteInventoryRequestAsync(string requestId);
        Task<InventoryRequest> GetInventoryRequestByIdAsync(string requestId);
        
        // Inventory Request Retrieval
        Task<IEnumerable<InventoryRequest>> GetAllInventoryRequestsAsync();
        Task<IEnumerable<InventoryRequest>> GetInventoryRequestsByBranchAsync(string branchId);
        Task<IEnumerable<InventoryRequest>> GetInventoryRequestsByCountryAsync(string country);
        Task<IEnumerable<InventoryRequest>> GetInventoryRequestsByStatusAsync(InventoryRequestStatus status);
        Task<IEnumerable<InventoryRequest>> GetPendingInventoryRequestsForApprovalAsync(string approverId);
        
        // Request Processing
        Task<bool> ApproveByCountryHeadAsync(string requestId, string approverId, decimal? approvedAmount = null);
        Task<bool> ApproveByCompanyHeadAsync(string requestId, string approverId, decimal? approvedAmount = null);
        Task<bool> RejectInventoryRequestAsync(string requestId, string approverId, string rejectionReason);
        Task<bool> TransferFundsAsync(string requestId, decimal amount);
        Task<bool> MarkAsPurchasedAsync(string requestId);
        Task<bool> CompleteInventoryRequestAsync(string requestId);
        Task<bool> CancelInventoryRequestAsync(string requestId);
        
        // Receipt Management
        Task<bool> UploadReceiptAsync(string requestId, string receiptPath);
        Task<string> GetReceiptPathAsync(string requestId);
        Task<bool> ValidateReceiptAsync(string requestId);
        
        // Statistics and Reports
        Task<int> GetTotalRequestsCountAsync();
        Task<int> GetPendingRequestsCountAsync();
        Task<int> GetApprovedRequestsCountAsync();
        Task<int> GetRejectedRequestsCountAsync();
        Task<decimal> GetTotalApprovedAmountAsync(DateTime startDate, DateTime endDate);
        Task<Dictionary<string, decimal>> GetExpensesByBranchAsync(DateTime startDate, DateTime endDate);
        Task<Dictionary<string, decimal>> GetExpensesByCountryAsync(DateTime startDate, DateTime endDate);
        
        // Validation
        Task<bool> CanRequestInventoryAsync(string employeeId, string branchId);
        Task<bool> CanApproveInventoryRequestAsync(string approverId, string requestId);
        Task<bool> CanUploadReceiptAsync(string employeeId, string requestId);
        Task<bool> IsValidAmount(decimal amount);
        
        // Notifications
        Task NotifyRequestCreatedAsync(string requestId);
        Task NotifyRequestApprovedByCountryHeadAsync(string requestId);
        Task NotifyRequestApprovedByCompanyHeadAsync(string requestId);
        Task NotifyFundsTransferredAsync(string requestId);
        Task NotifyRequestCompletedAsync(string requestId);
        Task NotifyRequestRejectedAsync(string requestId);
        
        // Budget Management
        Task<decimal> GetRemainingBudgetAsync(string branchId);
        Task<decimal> GetTotalExpensesAsync(string branchId, DateTime startDate, DateTime endDate);
        Task<bool> IsWithinBudgetLimitAsync(string branchId, decimal requestAmount);
        
        // Audit
        Task LogInventoryActionAsync(string requestId, string action, string userId);
        Task<IEnumerable<string>> GetInventoryAuditLogAsync(string requestId);
    }
}