using H82Travels.Models;
using H82Travels.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H82Travels.Services.Implementations
{
    public class HRService : IHRService
    {
        private readonly ApplicationDbContext _context;

        public HRService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllStaffAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetStaffByCountryAsync(string country)
        {
            return await _context.Users.Where(u => u.Country == country).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetStaffByProvinceAsync(string country, string province)
        {
            return await _context.Users.Where(u => u.Country == country && u.Province == province).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetStaffByCityAsync(string country, string province, string city)
        {
            return await _context.Users.Where(u => u.Country == country && u.Province == province && u.City == city).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetStaffByBranchAsync(string branchId)
        {
            return await _context.Users.Where(u => u.BranchId == branchId).ToListAsync();
        }

        public async Task<Branch> CreateBranchAsync(Branch branch)
        {
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<Branch> UpdateBranchAsync(Branch branch)
        {
            _context.Branches.Update(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<bool> DeleteBranchAsync(string branchId)
        {
            var branch = await _context.Branches.FindAsync(branchId);
            if (branch == null) return false;

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Branch> GetBranchByIdAsync(string branchId)
        {
            return await _context.Branches.FindAsync(branchId);
        }

        public async Task<IEnumerable<Branch>> GetBranchesByCountryAsync(string country)
        {
            return await _context.Branches.Where(b => b.Country == country).ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetBranchesByProvinceAsync(string country, string province)
        {
            return await _context.Branches.Where(b => b.Country == country && b.Province == province).ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetBranchesByCityAsync(string country, string province, string city)
        {
            return await _context.Branches.Where(b => b.Country == country && b.Province == province && b.City == city).ToListAsync();
        }

        public async Task<bool> AssignEmployeeToBranchAsync(string employeeId, string branchId)
        {
            var employee = await _context.Users.FindAsync(employeeId);
            if (employee == null) return false;

            employee.BranchId = branchId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveEmployeeFromBranchAsync(string employeeId, string branchId)
        {
            var employee = await _context.Users.FindAsync(employeeId);
            if (employee == null || employee.BranchId != branchId) return false;

            employee.BranchId = null; // Remove from branch
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignBranchManagerAsync(string employeeId, string branchId)
        {
            var employee = await _context.Users.FindAsync(employeeId);
            if (employee == null) return false;

            employee.ManagedBranch = await _context.Branches.FindAsync(branchId);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalEmployeeCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetEmployeeCountByCountryAsync(string country)
        {
            return await _context.Users.CountAsync(u => u.Country == country);
        }

        public async Task<int> GetEmployeeCountByProvinceAsync(string country, string province)
        {
            return await _context.Users.CountAsync(u => u.Country == country && u.Province == province);
        }

        public async Task<int> GetEmployeeCountByCityAsync(string country, string province, string city)
        {
            return await _context.Users.CountAsync(u => u.Country == country && u.Province == province && u.City == city);
        }

        public async Task<int> GetEmployeeCountByBranchAsync(string branchId)
        {
            return await _context.Users.CountAsync(u => u.BranchId == branchId);
        }

        public async Task<bool> IsBranchManagerAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user != null && user.ManagedBranch != null;
        }
    }
}
