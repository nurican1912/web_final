using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class PositionController : Controller
    {
        private readonly EmployeeManagementContext _context;
        public PositionController(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Positions.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var position = await _context.Positions.FirstOrDefaultAsync(m => m.PositionId == id);
            if (position == null) return NotFound();
            return View(position);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionId,Name")] Position position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pozisyon başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var position = await _context.Positions.FindAsync(id);
            if (position == null) return NotFound();
            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PositionId,Name")] Position position)
        {
            if (id != position.PositionId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(position);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pozisyon başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var position = await _context.Positions.FirstOrDefaultAsync(m => m.PositionId == id);
            if (position == null) return NotFound();
            return View(position);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Pozisyon başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}