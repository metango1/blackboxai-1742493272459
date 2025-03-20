using H82Travels.Models;
using H82Travels.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace H82Travels.Controllers
{
    [Authorize]
    public class HRController : Controller
    {
        private readonly IHRService _hrService;

        public HRController(IHRService hrService)
        {
            _hrService = hrService;
        }

        public async Task<IActionResult> Index()
        {
            var staff = await _hrService.GetAllStaffAsync();
            return View(staff);
        }

        public async Task<IActionResult> StaffByCountry(string country)
        {
            var staff = await _hrService.GetStaffByCountryAsync(country);
            return View(staff);
        }

        public async Task<IActionResult> StaffByProvince(string country, string province)
        {
            var staff = await _hrService.GetStaffByProvinceAsync(country, province);
            return View(staff);
        }

        public async Task<IActionResult> StaffByCity(string country, string province, string city)
        {
            var staff = await _hrService.GetStaffByCityAsync(country, province, city);
            return View(staff);
        }

        public async Task<IActionResult> StaffByBranch(string branchId)
        {
            var staff = await _hrService.GetStaffByBranchAsync(branchId);
            return View(staff);
        }

        // Additional actions for branch management can be added here
    }
}