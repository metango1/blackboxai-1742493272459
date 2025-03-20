using H82Travels.Models;
using Microsoft.AspNetCore.Identity;

namespace H82Travels.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(IdentityResult result, ApplicationUser user)> RegisterUserAsync(
            string email, 
            string password, 
            string fullName, 
            string country, 
            string province, 
            string city, 
            string role);

        Task<(SignInResult result, ApplicationUser user)> LoginAsync(
            string email, 
            string password, 
            bool rememberMe);

        Task<IdentityResult> AssignRoleAsync(ApplicationUser user, string role);
        
        Task<IdentityResult> AddClaimsAsync(
            ApplicationUser user, 
            string country, 
            string province = null, 
            string city = null);

        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        
        Task LogoutAsync();
        
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        
        Task<ApplicationUser> GetUserByIdAsync(string userId);
    }
}