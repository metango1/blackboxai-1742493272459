using H82Travels.Models;
using H82Travels.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace H82Travels.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Index()
        {
            var inventoryRequests = await _inventoryService.GetAllInventoryRequestsAsync();
            return View(inventoryRequests);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InventoryRequest inventoryRequest)
        {
            if (ModelState.IsValid)
            {
                await _inventoryService.CreateInventoryRequestAsync(inventoryRequest);
                return RedirectToAction("Index");
            }
            return View(inventoryRequest);
        }

        public async Task<IActionResult> Details(string id)
        {
            var inventoryRequest = await _inventoryService.GetInventoryRequestByIdAsync(id);
            if (inventoryRequest == null)
            {
                return NotFound();
            }
            return View(inventoryRequest);
        }

        public async Task<IActionResult> Approve(string id)
        {
            var inventoryRequest = await _inventoryService.GetInventoryRequestByIdAsync(id);
            if (inventoryRequest == null)
            {
                return NotFound();
            }

            // Approve the inventory request logic
            await _inventoryService.ApproveByCountryHeadAsync(id, User.Identity.Name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reject(string id)
        {
            var inventoryRequest = await _inventoryService.GetInventoryRequestByIdAsync(id);
            if (inventoryRequest == null)
            {
                return NotFound();
            }

            // Reject the inventory request logic
            await _inventoryService.RejectInventoryRequestAsync(id, User.Identity.Name, "Reason for rejection");
            return RedirectToAction("Index");
        }
    }
}