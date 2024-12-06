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
            return _context.Categories != null ?
              View(await _context.Categories.ToListAsync()) :
              Problem("Entity set is null");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null) return BadRequest();

            Category? category = await _context.Categories.FindAsync(id);

            if (category == null) return NotFound();

            return View(category);

        }
    }
}
