using expense_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ApplicationDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Categories != null
                ? View(await _context.Categories.ToListAsync())
                : Problem("Entity set is null");
        }

        public async Task<IActionResult> AddOrEdit(int? id = 0)
        {
            if (id == 0 || _context.Categories == null) return View(new Category());

            Category? category = await _context.Categories.FindAsync(id);

            if (category == null) return NotFound("Category is not found");

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(
            [Bind("Id, Title, Description, Type")] Category category
        )
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Input");

            if (_context.Categories == null) return NotFound("Categories not found");

            if (category.Id == 0) await _context.Categories.AddAsync(category);
            else _context.Update(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id = 0)
        {
            if (id == null || _context.Categories == null) return BadRequest();

            Category? category = await _context.Categories.FindAsync(id);

            if (category == null) return NotFound("Category is not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
