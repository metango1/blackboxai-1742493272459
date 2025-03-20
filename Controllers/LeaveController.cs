using H82Travels.Models;
using H82Travels.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace H82Travels.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        public async Task<IActionResult> Index()
        {
            var leaveRequests = await _leaveService.GetAllLeaveRequestsAsync();
            return View(leaveRequests);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                await _leaveService.CreateLeaveRequestAsync(leaveRequest);
                return RedirectToAction("Index");
            }
            return View(leaveRequest);
        }

        public async Task<IActionResult> Details(string id)
        {
            var leaveRequest = await _leaveService.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        public async Task<IActionResult> Approve(string id)
        {
            var leaveRequest = await _leaveService.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            // Approve the leave request logic
            await _leaveService.ApproveLeaveRequestAsync(id, User.Identity.Name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reject(string id)
        {
            var leaveRequest = await _leaveService.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            // Reject the leave request logic
            await _leaveService.RejectLeaveRequestAsync(id, User.Identity.Name, "Reason for rejection");
            return RedirectToAction("Index");
        }
    }
}