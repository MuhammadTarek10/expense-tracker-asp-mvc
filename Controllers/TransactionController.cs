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
            if (id == 0) return View(new Transaction());

            Transaction? transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound("Transation not found");

            return View(transaction);
        }
    }
}