using H82Travels.Models;

namespace H82Travels.Services.Interfaces
{
    public interface IHRService
    {
        // Staff Management
        Task<IEnumerable<ApplicationUser>> GetAllStaffAsync();
        Task<IEnumerable<ApplicationUser>> GetStaffByCountryAsync(string country);
        Task<IEnumerable<ApplicationUser>> GetStaffByProvinceAsync(string country, string province);
        Task<IEnumerable<ApplicationUser>> GetStaffByCityAsync(string country, string province, string city);
        Task<IEnumerable<ApplicationUser>> GetStaffByBranchAsync(string branchId);
        
        // Branch Management
        Task<Branch> CreateBranchAsync(Branch branch);
        Task<Branch> UpdateBranchAsync(Branch branch);
        Task<bool> DeleteBranchAsync(string branchId);
        Task<Branch> GetBranchByIdAsync(string branchId);
        Task<IEnumerable<Branch>> GetBranchesByCountryAsync(string country);
        Task<IEnumerable<Branch>> GetBranchesByProvinceAsync(string country, string province);
        Task<IEnumerable<Branch>> GetBranchesByCityAsync(string country, string province, string city);
        
        // Employee Assignment
        Task<bool> AssignEmployeeToBranchAsync(string employeeId, string branchId);
        Task<bool> RemoveEmployeeFromBranchAsync(string employeeId, string branchId);
        Task<bool> AssignBranchManagerAsync(string employeeId, string branchId);
        
        // Statistics and Reports
        Task<int> GetTotalEmployeeCountAsync();
        Task<int> GetEmployeeCountByCountryAsync(string country);
        Task<int> GetEmployeeCountByProvinceAsync(string country, string province);
        Task<int> GetEmployeeCountByCityAsync(string country, string province, string city);
        Task<int> GetEmployeeCountByBranchAsync(string branchId);
        
        // Validation
        Task<bool> IsBranchManagerAsync(string userId);
        Task<bool> HasAccessToCountryAsync(string userId, string country);
        Task<bool> HasAccessToProvinceAsync(string userId, string province);
        Task<bool> HasAccessToCityAsync(string userId, string city);
        Task<bool> HasAccessToBranchAsync(string userId, string branchId);
    }
}