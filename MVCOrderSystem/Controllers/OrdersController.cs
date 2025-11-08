using Microsoft.AspNetCore.Mvc;
using MVCOrderSystem.Models;
using MVCOrderSystem.Services;
using System.Text.Json;

namespace MVCOrderSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IQueueSender _sender;
        private readonly IConfiguration _cfg;

        public OrdersController(IQueueSender sender, IConfiguration cfg)
        {
            _cfg = cfg;
            _sender = sender;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create() => View(new OrderInputViewModel());
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderInputViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var json = JsonSerializer.Serialize(new { vm.OrderId, vm.UserId, vm.Amount, OccurredUTC = DateTime.UtcNow });
            var q = _cfg["Storage:QueueName"] ?? "orders-queue";
            await _sender.SendAsync(q, json);
            return RedirectToAction(nameof(Sent), new { id = vm.OrderId });
        }

        public IActionResult Sent(string id) { ViewBag.OrderId = id; return View(); }
    }
}
