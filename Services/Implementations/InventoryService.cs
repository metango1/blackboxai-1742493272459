using H82Travels.Models;
using H82Travels.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H82Travels.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InventoryRequest> CreateInventoryRequestAsync(InventoryRequest request)
        {
            _context.InventoryRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<InventoryRequest> UpdateInventoryRequestAsync(InventoryRequest request)
        {
            _context.InventoryRequests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> DeleteInventoryRequestAsync(string requestId)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            _context.InventoryRequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<InventoryRequest> GetInventoryRequestByIdAsync(string requestId)
        {
            return await _context.InventoryRequests.FindAsync(requestId);
        }

        public async Task<IEnumerable<InventoryRequest>> GetAllInventoryRequestsAsync()
        {
            return await _context.InventoryRequests.ToListAsync();
        }

        public async Task<IEnumerable<InventoryRequest>> GetInventoryRequestsByBranchAsync(string branchId)
        {
            return await _context.InventoryRequests.Where(ir => ir.BranchId == branchId).ToListAsync();
        }

        public async Task<IEnumerable<InventoryRequest>> GetInventoryRequestsByCountryAsync(string country)
        {
            return await _context.InventoryRequests
                .Include(ir => ir.Branch)
                .Where(ir => ir.Branch.Country == country)
                .ToListAsync();
        }

        public async Task<IEnumerable<InventoryRequest>> GetInventoryRequestsByStatusAsync(InventoryRequestStatus status)
        {
            return await _context.InventoryRequests.Where(ir => ir.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<InventoryRequest>> GetPendingInventoryRequestsForApprovalAsync(string approverId)
        {
            return await _context.InventoryRequests
                .Where(ir => ir.Status == InventoryRequestStatus.Pending)
                .ToListAsync();
        }

        public async Task<bool> ApproveByCountryHeadAsync(string requestId, string approverId, decimal? approvedAmount = null)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            request.Status = InventoryRequestStatus.ApprovedByCountryHead;
            request.ApprovedById = approverId;
            request.ApprovedAmount = approvedAmount;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveByCompanyHeadAsync(string requestId, string approverId, decimal? approvedAmount = null)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            request.Status = InventoryRequestStatus.ApprovedByCompanyHead;
            request.ApprovedById = approverId;
            request.ApprovedAmount = approvedAmount;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectInventoryRequestAsync(string requestId, string approverId, string rejectionReason)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            request.Status = InventoryRequestStatus.Rejected;
            request.ApprovedById = approverId;
            request.Notes = rejectionReason;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TransferFundsAsync(string requestId, decimal amount)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            request.Status = InventoryRequestStatus.FundsTransferred;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAsPurchasedAsync(string requestId)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            request.Status = InventoryRequestStatus.Purchased;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteInventoryRequestAsync(string requestId)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            request.Status = InventoryRequestStatus.Completed;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelInventoryRequestAsync(string requestId)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            request.Status = InventoryRequestStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UploadReceiptAsync(string requestId, string receiptPath)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            if (request == null) return false;

            request.ReceiptPath = receiptPath;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetReceiptPathAsync(string requestId)
        {
            var request = await _context.InventoryRequests.FindAsync(requestId);
            return request?.ReceiptPath;
        }

        public async Task<bool> ValidateReceiptAsync(string requestId)
        {
            // Implement logic to validate receipt
            return true; // Placeholder
        }

        public async Task<int> GetTotalRequestsCountAsync()
        {
            return await _context.InventoryRequests.CountAsync();
        }

        public async Task<int> GetPendingRequestsCountAsync()
        {
            return await _context.InventoryRequests.CountAsync(ir => ir.Status == InventoryRequestStatus.Pending);
        }

        public async Task<int> GetApprovedRequestsCountAsync()
        {
            return await _context.InventoryRequests.CountAsync(ir => ir.Status == InventoryRequestStatus.ApprovedByCountryHead || ir.Status == InventoryRequestStatus.ApprovedByCompanyHead);
        }

        public async Task<int> GetRejectedRequestsCountAsync()
        {
            return await _context.InventoryRequests.CountAsync(ir => ir.Status == InventoryRequestStatus.Rejected);
        }

        public async Task<decimal> GetTotalApprovedAmountAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.InventoryRequests
                .Where(ir => ir.Status == InventoryRequestStatus.ApprovedByCountryHead || ir.Status == InventoryRequestStatus.ApprovedByCompanyHead)
                .SumAsync(ir => ir.ApprovedAmount ?? 0);
        }

        public async Task<Dictionary<string, decimal>> GetExpensesByBranchAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.InventoryRequests
                .Where(ir => ir.CreatedAt >= startDate && ir.CreatedAt <= endDate)
                .GroupBy(ir => ir.BranchId)
                .ToDictionaryAsync(g => g.Key, g => g.Sum(ir => ir.ApprovedAmount ?? 0));
        }

        public async Task<Dictionary<string, decimal>> GetExpensesByCountryAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.InventoryRequests
                .Where(ir => ir.CreatedAt >= startDate && ir.CreatedAt <= endDate)
                .Include(ir => ir.Branch)
                .GroupBy(ir => ir.Branch.Country)
                .ToDictionaryAsync(g => g.Key, g => g.Sum(ir => ir.ApprovedAmount ?? 0));
        }

        public async Task<bool> CanRequestInventoryAsync(string employeeId, string branchId)
        {
            // Implement logic to check if the employee can request inventory
            return true; // Placeholder
        }

        public async Task<bool> CanApproveInventoryRequestAsync(string approverId, string requestId)
        {
            // Implement logic to check if the user can approve the inventory request
            return true; // Placeholder
        }

        public async Task<bool> CanUploadReceiptAsync(string employeeId, string requestId)
        {
            // Implement logic to check if the user can upload a receipt
            return true; // Placeholder
        }

        public async Task<bool> IsValidAmount(decimal amount)
        {
            // Implement logic to validate the amount
            return true; // Placeholder
        }

        public async Task NotifyRequestCreatedAsync(string requestId)
        {
            // Implement notification logic
        }

        public async Task NotifyRequestApprovedByCountryHeadAsync(string requestId)
        {
            // Implement notification logic
        }

        public async Task NotifyRequestApprovedByCompanyHeadAsync(string requestId)
        {
            // Implement notification logic
        }

        public async Task NotifyFundsTransferredAsync(string requestId)
        {
            // Implement notification logic
        }

        public async Task NotifyRequestCompletedAsync(string requestId)
        {
            // Implement notification logic
        }

        public async Task NotifyRequestRejectedAsync(string requestId)
        {
            // Implement notification logic
        }

        public async Task<decimal> GetRemainingBudgetAsync(string branchId)
        {
            // Implement logic to get remaining budget
            return 0; // Placeholder
        }

        public async Task<decimal> GetTotalExpensesAsync(string branchId, DateTime startDate, DateTime endDate)
        {
            // Implement logic to get total expenses
            return 0; // Placeholder
        }

        public async Task<bool> IsWithinBudgetLimitAsync(string branchId, decimal requestAmount)
        {
            // Implement logic to check if the request amount is within budget
            return true; // Placeholder
        }

        public async Task LogInventoryActionAsync(string requestId, string action, string userId)
        {
            // Implement logging logic
        }

        public async Task<IEnumerable<string>> GetInventoryAuditLogAsync(string requestId)
        {
            // Implement logic to get audit log
            return new List<string>(); // Placeholder
        }
    }
}