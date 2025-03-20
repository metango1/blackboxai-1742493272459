using H82Travels.Models;
using H82Travels.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace H82Travels.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<(IdentityResult result, ApplicationUser user)> RegisterUserAsync(
            string email,
            string password,
            string fullName,
            string country,
            string province,
            string city,
            string role)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                Country = country,
                Province = province,
                City = city
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Ensure role exists
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                // Assign role
                await _userManager.AddToRoleAsync(user, role);

                // Add claims
                await AddClaimsAsync(user, country, province, city);
            }

            return (result, user);
        }

        public async Task<(SignInResult result, ApplicationUser user)> LoginAsync(
            string email,
            string password,
            bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (SignInResult.Failed, null);
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
            return (result, user);
        }

        public async Task<IdentityResult> AssignRoleAsync(ApplicationUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddClaimsAsync(
            ApplicationUser user,
            string country,
            string province = null,
            string city = null)
        {
            var claims = new List<Claim>
            {
                new Claim("Country", country)
            };

            if (!string.IsNullOrEmpty(province))
            {
                claims.Add(new Claim("Province", province));
            }

            if (!string.IsNullOrEmpty(city))
            {
                claims.Add(new Claim("City", city));
            }

            return await _userManager.AddClaimsAsync(user, claims);
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}