using expense_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Categories != null
                ? View(await _context.Categories.ToListAsync())
                : Problem("Entity set is null");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Description")] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input");

            if (_context.Categories == null)
                return NotFound("Categories not found");

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
                return BadRequest();

            Category? category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound("Category is not found");

            return View(category);
        }
    }
}
