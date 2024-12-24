using expense_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ApplicationDbContext context, ILogger<TransactionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Transactions != null
                ? View(await _context.Transactions.ToListAsync())
                : Problem("Entity set is null");
        }

        public async Task<IActionResult> AddOrEdit(int? id = 0)
        {
            if (_context.Transactions == null) return Problem("Entity set is null");

            PopluateCategories();
            if (id == 0) return View(new Transaction());

            Transaction? transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound("Transation not found");

            return View(transaction);
        }

        [NonAction]
        public void PopluateCategories()
        {
            if (_context.Categories == null) return;

            List<Category> categories = _context.Categories.ToList();

            Category defaultCategory = new Category() { Id = 0, Title = "Select Category" };
            categories.Insert(0, defaultCategory);

            ViewBag.Categories = categories;
        }
    }
}
